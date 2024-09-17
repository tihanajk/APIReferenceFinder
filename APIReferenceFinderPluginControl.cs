using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace APIReferenceFinder
{
    public partial class APIReferenceFinderPluginControl : PluginControlBase
    {
        private Settings mySettings;

        private string ENV_ID = "";

        public APIReferenceFinderPluginControl()
        {
            InitializeComponent();
            InitializeFlowView();
            InitializeJSView();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            //ShowInfoNotification("Hello there", new Uri("https://www.google.com/"));

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            ENV_ID = detail.EnvironmentId;

            ExecuteMethod(GetSolutions);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        private class ListObject
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        private void GetSolutions()
        {
            var managed = managedCheck.Checked;
            var unmanaged = unmanagedCheck.Checked;

            var all = (managed && unmanaged) || (!managed && !unmanaged);

            var message = $"Fetching All {(managed ? "Managed" : "")} Solutions";
            WorkAsync(new WorkAsyncInfo()
            {
                Message = message,
                AsyncArgument = null,
                Work = (worker, args) =>
                {
                    var query_ismanaged = managed;
                    var query = new QueryExpression("solution");
                    query.ColumnSet.AddColumns("friendlyname", "solutionid", "uniquename");
                    query.AddOrder("friendlyname", OrderType.Ascending);

                    if (!all) query.Criteria.AddCondition("ismanaged", ConditionOperator.Equal, query_ismanaged);

                    args.Result = Service.RetrieveMultiple(query);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityCollection;

                    ComboboxSolutions.DataSource = new List<ListObject>();

                    if (result != null)
                    {
                        var items = new List<ListObject>();

                        items.Add(new ListObject()
                        {
                            Name = "<All Solutions>",
                            Value = "1"
                        });

                        foreach (var sol in result.Entities)
                        {
                            var friendlyName = sol.Contains("friendlyname") ? (string)sol["friendlyname"] : "";
                            var name = $"{friendlyName} ({sol["uniquename"]})";
                            var solutionId = sol.Id.ToString();

                            items.Add(new ListObject()
                            {
                                Name = name,
                                Value = solutionId,
                            });
                        }

                        ComboboxSolutions.DataSource = items;
                        ComboboxSolutions.DisplayMember = "Name";
                        ComboboxSolutions.ValueMember = "Value";
                    }
                }
            });
        }

        private void GetAPIs()
        {
            ComboboxAPIs.DataSource = new List<ListObject>();

            var selectedSolution = ComboboxSolutions.SelectedItem as ListObject;

            if (selectedSolution == null) return;

            var solutionId = selectedSolution.Value;

            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Fetching APIs",
                AsyncArgument = null,
                Work = (worker, args) =>
                {
                    var query = new QueryExpression("customapi");
                    query.ColumnSet.AddColumns("customapiid", "displayname", "uniquename");
                    query.AddOrder("displayname", OrderType.Ascending);

                    if (solutionId != "1" && !allApisCheck.Checked)
                    {
                        var aa = query.AddLink("solutioncomponent", "customapiid", "objectid");
                        aa.EntityAlias = "aa";
                        aa.LinkCriteria.AddCondition("solutionid", ConditionOperator.Equal, solutionId);
                    }

                    args.Result = Service.RetrieveMultiple(query);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityCollection;

                    if (result.Entities.Count == 0)
                    {
                        ComboboxAPIs.SelectedIndex = -1;
                        ComboboxAPIs.Text = "";
                        OnAPISelected(ComboboxAPIs, EventArgs.Empty);
                    }

                    if (result != null)
                    {
                        var items = new List<ListObject>();

                        foreach (var ent in result.Entities)
                        {
                            var listObject = new ListObject();
                            listObject.Name = $"{(string)ent["displayname"]} ({(string)ent["uniquename"]})";
                            listObject.Value = (string)ent["uniquename"];

                            items.Add(listObject);
                        }

                        ComboboxAPIs.DataSource = items;
                        ComboboxAPIs.DisplayMember = "Name";
                        ComboboxAPIs.ValueMember = "Value";
                    }
                }
            });
        }

        private void GetSolutionFlows()
        {
            var fetchFlows = flowsCheck.Checked;
            if (!fetchFlows) return;

            var selectedSolution = ComboboxSolutions.SelectedItem as ListObject;

            if (selectedSolution == null) return;

            var solutionId = selectedSolution.Value;

            var api = ComboboxAPIs.SelectedItem as ListObject;
            if (api == null) { InitializeFlowView(); return; }

            var apiLogName = api.Value;
            if (apiLogName == null) return;

            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Fetching Solution Flows",
                AsyncArgument = null,
                Work = (worker, args) =>
                {
                    var solutionFilter = solutionId != "1" ?
                            $@"<link-entity name='solutioncomponent' from='objectid' to='workflowid' link-type='inner' alias='aa'>
                                  <filter>
                                    <condition attribute='solutionid' operator='eq' value='{solutionId}' />
                                  </filter>
                                </link-entity>" : "";

                    var fetchXml = $@"<fetch>
                              <entity name='workflow'>
                                <attribute name='workflowid' />
                                <attribute name='workflowidunique' />
                                <attribute name='name' />
                                <filter type='and'>
                                  <condition attribute='category' operator='eq' value='5' />
                                  <condition attribute='clientdata' operator='like' value='%&quot;actionName&quot;:&quot;{apiLogName}&quot;%' />
                                </filter>
                                {solutionFilter}
                                <order attribute='name' />
                              </entity>
                            </fetch>";
                    var flowsList = Service.RetrieveMultiple(new FetchExpression(fetchXml)).Entities.ToList();

                    args.Result = flowsList;
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as List<Entity>;
                    if (result == null || result.Count == 0)
                    {
                        InitializeFlowView();
                        return;
                    }

                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("ID", typeof(Guid));
                    dataTable.Columns.Add("Name", typeof(string));
                    dataTable.Columns.Add("Link", typeof(string));

                    foreach (var ent in result)
                    {
                        var flowId = (Guid)ent["workflowidunique"];
                        var link = $"https://make.powerapps.com/environments/{ENV_ID}/solutions/{solutionId}/objects/cloudflows/{flowId}/view";
                        var name = (string)ent["name"];
                        dataTable.Rows.Add(ent.Id, name, link);
                    }
                    FlowsGrid.DataSource = dataTable;
                }
            });
        }

        private void GetSolutionJS()
        {
            var fetchJS = wrCheck.Checked;
            if (!fetchJS) return;

            var selectedSolution = ComboboxSolutions.SelectedItem as ListObject;
            if (selectedSolution == null) return;

            var solutionId = selectedSolution.Value;

            var api = ComboboxAPIs.SelectedItem as ListObject;
            if (api == null) return;

            var apiLogName = api.Value;
            if (apiLogName == null) return;

            //if (solutionId == "1")
            //{
            //    MessageBox.Show("Not recommended to fetch webresources from all solutions - please select a solution");
            //    return;
            //}

            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Fetching Webresources",
                AsyncArgument = null,
                Work = (worker, args) =>
                {
                    var aa_solutionid = solutionId;
                    var query = new QueryExpression("webresource");
                    query.ColumnSet.AllColumns = true;
                    query.Criteria.AddCondition("content", ConditionOperator.NotNull);

                    query.AddOrder("name", OrderType.Ascending);

                    if (solutionId != "1")
                    {
                        var aa = query.AddLink("solutioncomponent", "webresourceid", "objectid");
                        aa.EntityAlias = "aa";
                        aa.LinkCriteria.AddCondition("solutionid", ConditionOperator.Equal, aa_solutionid);
                    }

                    var webresourcesList = Service.RetrieveMultiple(query).Entities.ToList();

                    args.Result = webresourcesList;
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as List<Entity>;
                    if (result == null || result.Count == 0)
                    {
                        InitializeJSView();
                        return;
                    }

                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("ID", typeof(Guid));
                    dataTable.Columns.Add("Name", typeof(string));

                    foreach (var resource in result)
                    {
                        var content = (string)resource["content"];

                        byte[] data = Convert.FromBase64String(content);
                        string decodedString = System.Text.Encoding.UTF8.GetString(data);

                        if (!decodedString.Contains(apiLogName)) continue;

                        var name = (string)resource["name"];
                        var id = (Guid)resource["webresourceid"];

                        dataTable.Rows.Add(id, name);
                    }

                    JSGrid.DataSource = dataTable;

                }
            });
        }
        private void ViewCodeClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the click is on a button cell.
            if (e.ColumnIndex == JSGrid.Columns["InfoButton"].Index && e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                var id = (Guid)JSGrid.Rows[rowIndex].Cells["ID"].Value;
                ShowCode(id);
            }
        }

        List<int> listOfReferences = new List<int>();
        int currentRef = 0;

        private void ShowCode(Guid id)
        {
            var api = ComboboxAPIs.SelectedItem as ListObject;
            if (api == null) return;

            var apiLogName = api.Value;
            if (apiLogName == null) return;

            WorkAsync(new WorkAsyncInfo()
            {
                AsyncArgument = null,
                Work = (worker, args) =>
                {
                    var query = new QueryExpression("webresource");
                    query.ColumnSet.AddColumns("webresourceid", "displayname", "name", "content");

                    query.Criteria.AddCondition("webresourceid", ConditionOperator.Equal, id);

                    args.Result = Service.RetrieveMultiple(query).Entities.FirstOrDefault();
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as Entity;

                    CodeText.Text = "";

                    if (result != null)
                    {
                        var content = (string)result["content"];

                        byte[] data = Convert.FromBase64String(content);
                        string decodedString = System.Text.Encoding.UTF8.GetString(data);

                        CodeText.Text = decodedString;

                        var name = (string)result["name"];
                        wrName.Text = name;

                        var index = 0;
                        var count = 0;
                        var first = true;
                        while (true)
                        {
                            index = CodeText.Text.IndexOf(apiLogName, index);
                            if (index < 0) break;

                            listOfReferences.Add(index);

                            CodeText.SelectionStart = index;
                            CodeText.SelectionLength = apiLogName.Length;
                            CodeText.SelectionColor = Color.Red;

                            if (first) { CodeText.ScrollToCaret(); first = false; currentRef = index; }

                            index += apiLogName.Length;

                            count++;
                        }

                        refCounter.Text = count + " references";
                    }
                }

            });
        }

        private void LoadAPIsBtn_Click(object sender, EventArgs e)
        {
            ExecuteMethod(GetAPIs);
        }

        private void OnAPISelected(object sender, EventArgs e)
        {
            ExecuteMethod(GetSolutionFlows);
            ExecuteMethod(GetSolutionJS);
        }

        private void OnSolutionSelected(object sender, EventArgs e)
        {
            var selectedSolution = ComboboxSolutions.SelectedItem as ListObject;
            if (selectedSolution?.Value == "1")
            {
                wrFetchInfo.Text = "not recommended to fetch webresources from all solutions - long running action";
                wrCheck.Checked = false;
            }
            else wrFetchInfo.Text = "";

            ExecuteMethod(GetAPIs);
        }

        private void InitializeFlowView()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(Guid));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Link", typeof(string));

            FlowsGrid.DataSource = dataTable;
            FlowsGrid.Columns["ID"].Visible = false;

            FlowsGrid.Columns["Name"].Width = 180;
            FlowsGrid.Columns["Link"].Width = 180;
            if (FlowsGrid.Columns["LinkButton"] == null)
            {
                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                buttonColumn.HeaderText = "";
                buttonColumn.Name = "LinkButton";
                buttonColumn.Text = "Copy";
                buttonColumn.UseColumnTextForButtonValue = true;
                buttonColumn.Width = 47;
                FlowsGrid.Columns.Add(buttonColumn);
            }

            FlowsGrid.CellContentClick -= HandleFlowItemClick;
            FlowsGrid.CellContentClick += HandleFlowItemClick;
        }
        private void InitializeJSView()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(Guid));
            dataTable.Columns.Add("Name", typeof(string));

            JSGrid.DataSource = dataTable;
            JSGrid.Columns["ID"].Visible = false;

            JSGrid.Columns["Name"].Width = 340;

            if (JSGrid.Columns["InfoButton"] == null)
            {
                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                buttonColumn.HeaderText = "Info";
                buttonColumn.Name = "InfoButton";
                buttonColumn.Text = "View Code";
                buttonColumn.UseColumnTextForButtonValue = true;
                buttonColumn.Width = 67;
                JSGrid.Columns.Add(buttonColumn);
            }

            JSGrid.CellContentClick -= ViewCodeClick;
            JSGrid.CellContentClick += ViewCodeClick;

            refCounter.Text = "0 references";
        }


        private void HandleFlowItemClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == FlowsGrid.Columns["LinkButton"]?.Index && e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                var link = (string)FlowsGrid.Rows[rowIndex].Cells["Link"].Value;

                Clipboard.SetText(link);
                MessageBox.Show("Flow link copied to clipboard.");
            }
            else if (e.ColumnIndex == FlowsGrid.Columns["Link"].Index && e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                var link = (string)FlowsGrid.Rows[rowIndex].Cells["Link"].Value;

                OpenLink(link);
            }
        }

        private void OpenLink(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to open the link: " + ex.Message);
            }
        }

        private void LoadSolutionsBtnClick(object sender, EventArgs e)
        {
            ExecuteMethod(GetSolutions);
        }

        private void FlowsCheck_CheckedChanged(object sender, EventArgs e)
        {
            var check = flowsCheck.Checked;

            if (check) ExecuteMethod(GetSolutionFlows);
            else InitializeFlowView();
        }

        private void WrCheck_CheckedChanged(object sender, EventArgs e)
        {
            var check = wrCheck.Checked;

            var selectedSolution = ComboboxSolutions.SelectedItem as ListObject;

            if (check) ExecuteMethod(GetSolutionJS);
            else InitializeJSView();
        }
        private void OpenSolutionButton(object sender, EventArgs e)
        {
            var selectedSolution = ComboboxSolutions.SelectedItem as ListObject;
            if (selectedSolution == null) return;

            var solutionId = selectedSolution.Value;
            string url = $"https://make.powerapps.com/environments/{ENV_ID}/solutions/{solutionId}";

            if (solutionId == "1") return;
            OpenLink(url);
        }

        private void GoToInCode(int step)
        {
            if (listOfReferences.Count == 0) return;
            var current = listOfReferences.IndexOf(currentRef);
            if (current == 0 && step == -1) current = listOfReferences.Count;
            else if (current == listOfReferences.Count - 1 && step == 1) current = -1;

            var next = current + step;
            if (next <= -1 || next >= listOfReferences.Count) return;

            currentRef = listOfReferences[next];

            CodeText.SelectionStart = currentRef;
            CodeText.ScrollToCaret();
        }

        private void PrevRefBtn_Click(object sender, EventArgs e)
        {
            if (CodeText.Text == "" || CodeText.Text == null) return;
            var step = -1;
            GoToInCode(step);
        }

        private void NextRefBtn_Click(object sender, EventArgs e)
        {
            if (CodeText.Text == "" || CodeText.Text == null) return;
            var step = 1;
            GoToInCode(step);
        }

        private void AllApisCheck_CheckedChanged(object sender, EventArgs e)
        {
            ExecuteMethod(GetAPIs);
        }
    }
}