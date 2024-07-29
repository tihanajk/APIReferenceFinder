﻿using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

            GetSolutions();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("Hello there", new Uri("https://www.google.com/"));

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

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbSample_Click(object sender, EventArgs e)
        {
            // The ExecuteMethod method handles connecting to an
            // organization if XrmToolBox is not yet connected
            ExecuteMethod(GetAccounts);
        }

        private void GetAccounts()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting accounts",
                Work = (worker, args) =>
                {
                    args.Result = Service.RetrieveMultiple(new QueryExpression("th_dev")
                    {
                        TopCount = 50
                    });
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityCollection;
                    if (result != null)
                    {
                        MessageBox.Show($"Found {result.Entities.Count} devs");
                    }
                }
            });
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

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        private class ListObject
        {
            public string name { get; set; }
            public string value { get; set; }
        }

        private void GetSolutions()
        {
            var managed = managedCheck.Checked;
            var unmanaged = unmanagedCheck.Checked;

            var all = (managed && unmanaged) || (!managed && !unmanaged);

            var message = $"Getting All {(managed ? "Managed" : "")} Solutions";
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
                            name = "<All Solutions>",
                            value = "1"
                        });

                        foreach (var sol in result.Entities)
                        {
                            var friendlyName = sol.Contains("friendlyname") ? (string)sol["friendlyname"] : "";
                            var name = $"{friendlyName} ({sol["uniquename"]})";
                            var solutionId = sol.Id.ToString();

                            items.Add(new ListObject()
                            {
                                name = name,
                                value = solutionId,
                            });
                        }

                        ComboboxSolutions.DataSource = items;
                        ComboboxSolutions.DisplayMember = "name";
                        ComboboxSolutions.ValueMember = "value";
                    }
                }
            });
        }

        private void GetAPIs()
        {
            ComboboxAPIs.DataSource = new List<ListObject>();

            var selectedSolution = ComboboxSolutions.SelectedItem as ListObject;

            if (selectedSolution == null) return;

            var solutionId = selectedSolution.value;

            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Getting APIs",
                AsyncArgument = null,
                Work = (worker, args) =>
                {
                    var query = new QueryExpression("customapi");
                    query.ColumnSet.AddColumns("customapiid", "displayname", "uniquename");
                    query.AddOrder("displayname", OrderType.Ascending);

                    if (solutionId != "1")
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
                            listObject.name = $"{(string)ent["displayname"]} ({(string)ent["uniquename"]})";
                            listObject.value = (string)ent["uniquename"];

                            items.Add(listObject);
                        }

                        ComboboxAPIs.DataSource = items;
                        ComboboxAPIs.DisplayMember = "name";
                        ComboboxAPIs.ValueMember = "value";
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

            var solutionId = selectedSolution.value;

            var api = ComboboxAPIs.SelectedItem as ListObject;
            if (api == null) { InitializeFlowView(); return; }

            var apiLogName = api.value;
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
                                  <condition attribute='clientdata' operator='like' value='%&quot;actionName&quot;:&quot;{apiLogName}&quot;,%' />
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

                    // Define the columns.
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

                    // Bind the DataTable to the DataGridView.
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

            var solutionId = selectedSolution.value;

            var api = ComboboxAPIs.SelectedItem as ListObject;
            if (api == null) return;

            var apiLogName = api.value;
            if (apiLogName == null) return;

            if (solutionId == "1")
            {
                MessageBox.Show("not recommended to fetch webresources from all solutions");
                return;
            }

            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Fetching webresources",
                AsyncArgument = null,
                Work = (worker, args) =>
                {
                    var aa_solutionid = solutionId;
                    var query = new QueryExpression("webresource");
                    query.ColumnSet.AllColumns = true;
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

                    // Define the columns.
                    dataTable.Columns.Add("ID", typeof(Guid));
                    dataTable.Columns.Add("Name", typeof(string));
                    dataTable.Columns.Add("Link", typeof(string));

                    foreach (var resource in result)
                    {
                        var content = (string)resource["content"];

                        byte[] data = Convert.FromBase64String(content);
                        string decodedString = System.Text.Encoding.UTF8.GetString(data);

                        if (!decodedString.Contains(apiLogName)) continue;

                        var name = (string)resource["name"];
                        var id = (Guid)resource["webresourceid"];

                        dataTable.Rows.Add(id, name, "link");
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

        private void ShowCode(Guid id)
        {
            var api = ComboboxAPIs.SelectedItem as ListObject;
            if (api == null) return;

            var apiLogName = api.value;
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

                        var index = 0;
                        var count = 0;
                        var first = true;
                        while (true)
                        {
                            index = CodeText.Text.IndexOf(apiLogName, index);
                            if (index < 0) break;

                            CodeText.SelectionStart = index;
                            CodeText.SelectionLength = apiLogName.Length;
                            CodeText.SelectionColor = Color.Red;

                            if (first) { CodeText.ScrollToCaret(); first = false; }

                            index += apiLogName.Length;

                            count++;
                        }

                        refCounter.Text = count + " references";

                        //MessageBox.Show(count + " occurances");

                        //int index = CodeText.Text.IndexOf(apiLogName);
                        //if (index >= 0)
                        //{
                        //    // Set the cursor position to the start of the word
                        //    CodeText.SelectionStart = index;
                        //    // Set the cursor length to the length of the word (select the word)
                        //    CodeText.SelectionLength = apiLogName.Length;
                        //    // Scroll to the cursor
                        //    CodeText.ScrollToCaret();

                        //    CodeText.SelectionColor = Color.Red;
                        //}
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
            ExecuteMethod(GetAPIs);
            //ExecuteMethod(GetSolutionFlows);
            //ExecuteMethod(GetSolutionJS);
        }

        private void InitializeFlowView()
        {
            DataTable dataTable = new DataTable();
            // Define the columns.
            dataTable.Columns.Add("ID", typeof(Guid));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Link", typeof(string));

            FlowsGrid.DataSource = dataTable;
            FlowsGrid.Columns["ID"].Visible = false;
            FlowsGrid.Columns["Link"].Visible = false;

            FlowsGrid.Columns["Name"].Width = 250;
            if (FlowsGrid.Columns["LinkButton"] == null)
            {
                // Add a button column to the DataGridView
                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                buttonColumn.HeaderText = "Link";
                buttonColumn.Name = "LinkButton";
                buttonColumn.Text = "Get Link";
                buttonColumn.UseColumnTextForButtonValue = true;
                buttonColumn.Width = 100;
                FlowsGrid.Columns.Add(buttonColumn);
            }

            // Handle the CellContentClick event
            FlowsGrid.CellContentClick -= CopyFlowLink;

            // Handle the CellContentClick event.
            FlowsGrid.CellContentClick += CopyFlowLink;
        }

        private void CopyFlowLink(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == FlowsGrid.Columns["LinkButton"].Index && e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                var link = (string)FlowsGrid.Rows[rowIndex].Cells["Link"].Value;

                if (!string.IsNullOrEmpty(link))
                {
                    Clipboard.SetText(link);
                    MessageBox.Show("Link copied to clipboard.");
                }
                else
                {
                    MessageBox.Show("No text to copy.");
                }
            }
        }

        private void InitializeJSView()
        {
            DataTable dataTable = new DataTable();

            // Define the columns.
            dataTable.Columns.Add("ID", typeof(Guid));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Link", typeof(string));

            JSGrid.DataSource = dataTable;
            JSGrid.Columns["ID"].Visible = false;

            JSGrid.Columns["Name"].Width = 250;
            JSGrid.Columns["Link"].Width = 50;

            if (JSGrid.Columns["InfoButton"] == null)
            {
                // Add a button column to the DataGridView
                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                buttonColumn.HeaderText = "Info";
                buttonColumn.Name = "InfoButton";
                buttonColumn.Text = "Code";
                buttonColumn.UseColumnTextForButtonValue = true;
                buttonColumn.Width = 50;
                JSGrid.Columns.Add(buttonColumn);
            }

            // Handle the CellContentClick event
            JSGrid.CellContentClick -= ViewCodeClick;

            // Handle the CellContentClick event.
            JSGrid.CellContentClick += ViewCodeClick;

            refCounter.Text = "0 references";
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

            if (check) ExecuteMethod(GetSolutionJS);
            else InitializeJSView();
        }

        private void refCounter_Click(object sender, EventArgs e)
        {

        }

        private void CodeText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}