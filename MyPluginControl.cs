using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Documents;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MyTool
{
    public partial class MyPluginControl : PluginControlBase
    {
        private Settings mySettings;

        public MyPluginControl()
        {
            InitializeComponent();
            InitializeFlowView();
            InitializeJSView();
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

        private void GetAPIs()
        {
            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Getting APIs",
                AsyncArgument = null,
                Work = (worker, args) =>
                {
                    var query = new QueryExpression("customapi");
                    query.ColumnSet.AddColumns("customapiid", "displayname", "uniquename");
                    query.AddOrder("displayname", OrderType.Ascending);
                    args.Result = Service.RetrieveMultiple(query);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityCollection;

                    ComboboxAPIs.Items.Clear();

                    if (result != null)
                    {
                        var items = new List<ListObject>();
                        //items.Add(new ListObject()
                        //{
                        //    name = "",
                        //    value = null
                        //});

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

        private void GetFlows()
        {
            var api = ComboboxAPIs.SelectedItem as ListObject;
            if (api == null) return;

            var apiLogName = api.value;
            if (apiLogName == null) return;

            WorkAsync(new WorkAsyncInfo()
            {
                Message = "Fetching flows",
                AsyncArgument = null,
                Work = (worker, args) =>
                {
                    var fetchXml = $@"<fetch>
                              <entity name=""workflow"">
                                <attribute name=""workflowid"" />
                                <attribute name=""name"" />
                                <filter type=""and"">
                                  <condition attribute=""category"" operator=""eq"" value=""5"" />
                                  <condition attribute=""clientdata"" operator=""like"" value=""%&quot;actionName&quot;:&quot;{apiLogName}&quot;,%"" />
                                </filter>
                                <order attribute=""name"" />
                              </entity>
                            </fetch>";
                    args.Result = Service.RetrieveMultiple(new FetchExpression(fetchXml));
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityCollection;

                    ListOfFlows.Items.Clear();

                    if (result != null)
                    {   
                        foreach (var f in result.Entities)
                        {
                            var flowName = (string)f["name"];
                            ListViewItem item = new ListViewItem(flowName);
                            item.SubItems.Add("link");
                            ListOfFlows.Items.Add(item);
                        }
                    }
                }

            });
        }

        private void GetJS()
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
                    query.Criteria.AddCondition("content", ConditionOperator.NotNull);
                    query.Criteria.AddCondition("name", ConditionOperator.BeginsWith, "span_/js/");
                    query.Criteria.AddCondition("webresourcetype", ConditionOperator.Equal, 3);

                    args.Result = Service.RetrieveMultiple(query);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityCollection;

                    ListOfJS.Items.Clear();

                    if (result != null)
                    {

                        var items = new List<ListObject>();
                        foreach (var resource in result.Entities)
                        {
                            var content = (string)resource["content"];

                            byte[] data = Convert.FromBase64String(content);
                            string decodedString = System.Text.Encoding.UTF8.GetString(data);

                            if (!decodedString.Contains(apiLogName)) continue;

                            var name = (string)resource["name"];
                            var id = (Guid)resource["webresourceid"];
                            
                            ListViewItem item = new ListViewItem(name);
                            item.SubItems.Add(id.ToString());
                            ListOfJS.Items.Add(item);

                        }
                    }
                }

            });
        }
        private void LoadBtn_Click(object sender, EventArgs e)
        {
            ExecuteMethod(GetAPIs);
        }

        private void OnAPISelected(object sender, EventArgs e)
        {
            ExecuteMethod(GetFlows);
            ExecuteMethod(GetJS);
        }

        private void ListOfFlows_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void InitializeFlowView()
        {
            ListOfFlows.View = View.Details;
            ListOfFlows.FullRowSelect = true;
            ListOfFlows.GridLines = true;

            ListOfFlows.Columns.Add("Flow Name", 250);
            ListOfFlows.Columns.Add("Link", 50);
        }

        private void InitializeJSView()
        {
            ListOfJS.View = View.Details;
            ListOfJS.FullRowSelect = true;
            ListOfJS.GridLines = true;

            ListOfJS.Columns.Add("JS Name", 250);
            ListOfJS.Columns.Add("Id", 50);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            ExecuteMethod(GetAPIs);
        }

        private void ShowCode()
        {            
            var selected = ListOfJS.SelectedItems[0] as ListViewItem;
            if (selected == null) return;

            var id = selected.SubItems[1].Text;

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

                        int index = CodeText.Text.IndexOf(apiLogName);
                        if (index >= 0)
                        {
                            // Set the cursor position to the start of the word
                            CodeText.SelectionStart = index;
                            // Set the cursor length to the length of the word (select the word)
                            CodeText.SelectionLength = apiLogName.Length;
                            // Scroll to the cursor
                            CodeText.ScrollToCaret();

                            CodeText.SelectionColor = Color.Red;
                        }
                    }
                }

            });
        }

        private void OnJSSeleted2(object sender, EventArgs e)
        {
            //if (ListOfJS.SelectedItems.Count != 1) return;
            //ExecuteMethod(ShowCode);
        }

        private void Solution_Click(object sender, EventArgs e)
        {

        }

        private void OnJSSelected(object sender, EventArgs e)
        {
            if (ListOfJS.SelectedItems.Count != 1) return;
            ExecuteMethod(ShowCode);
        }
    }
}