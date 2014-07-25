using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;
using AxosoftAPI.NET.Models;
using AxosoftAPI.NET;
using AxosoftAPI.NET.Helpers;
using WinApp.ApiFormControls;

namespace WinApp
{
	public partial class ItemsControl : UserControl
	{
		Proxy AxosoftProxy;

		SortableBindingList<Project> Projects = new SortableBindingList<Project>();
		BindingList<Item> Items = new BindingList<Item>();
        SortableBindingList<WorkLog> Work = new SortableBindingList<WorkLog>();
        public static int count = 0;
        public static string unit = "";
        public static ArrayList names = new ArrayList();
        public static ArrayList namesCount = new ArrayList();
        public static TabPage[] namesList = new TabPage[30];
        public static ArrayList namesSelected = new ArrayList();
        public static ArrayList WorksheetList = new ArrayList();
        DateTime today = DateTime.Today;
        string SD = DateTime.Today.AddDays(-1000).ToString();
        string ED = DateTime.Today.AddDays(+1).ToString();
        DateTime LastWeek = DateTime.Today.AddDays(-6);
        DateTime LastMonth = DateTime.Today.AddDays(-30);
        public static ArrayList Dates = new ArrayList();
        public static decimal TotalHours = 0;
        public static int[] tabSelect = new int[50];
        public static string firstUser = "";
        public static bool firstUserTest = false;
        public static int firstUserCount = 0;
        public static bool[] UserSelection = new bool[100];
        public static ArrayList DatesView = new ArrayList();
        public static int UserIndex = -1;
        DateTime local;
        TimeZone localZone = TimeZone.CurrentTimeZone;
        
        
        AxosoftAPI.NET.Models.Result<System.Collections.Generic.IEnumerable<AxosoftAPI.NET.Models.WorkLog>> result;
        AxosoftAPI.NET.Models.Result<System.Collections.Generic.IEnumerable<AxosoftAPI.NET.Models.WorkLog>> newResult;
		public ItemsControl()
		{
			InitializeComponent();


			// configure the items grid
			ItemsGridView.AutoGenerateColumns = false;
			ItemsGridView.DataSource = Work;
			System.Windows.Forms.Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            

            
        }
        
		void Application_ApplicationExit(object sender, EventArgs e)
		{
			if (AxosoftProxy != null && string.IsNullOrWhiteSpace(AxosoftProxy.AccessToken))
			{
				AxosoftProxy.Get<object>("oauth2/revoke");
			}
		}

		public void SetAxosoftProxy(Proxy axosoftProxy)
		{
			AxosoftProxy = axosoftProxy;

			// set Axosoft host label
			AxosoftHostLabel.Text = new Uri(Program.Settings.Url).Host;

			GetProjects();
			if (Projects.Count == 0)
			{
				// there are no projects. ask the user if they want to create a new project.
				var dialogResult = MessageBox.Show(
					"Your Axosoft database has no projects. To use this example, you will need to create a project. Would you like to create a new project called \"API Example Project\" now?",
					"Create an Axosoft scrum project",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);

				if (dialogResult == DialogResult.Yes)
				{
					var project = new Project
					{
						Name = "API Example Project"
					};

					AxosoftProxy.Projects.Create(project);
					GetProjects();
				}
				else
				{
					Application.Exit();
					return;
				}
			}
			GetItems();
		}
        //
        //
        //DataGridView
        //
        //
        //private DataGridView[] newDataGridView(int posCount, DataGridView dataGridView)
        //{
        //    List<DataGridView> dataGridViewList = new List<DataGridView>();
        //    for(int i = 0 ; i<posCount; i++)
        //    {
        //        DataGridView dgv = new DataGridView();
        //        dgv = dataGridView;
        //        dataGridViewList.Add(dgv);
        //    }
        //    return dataGridViewList.ToArray();
        //}
        
        void GetItems()
		{
                    result = AxosoftProxy.WorkLogs.Get(new Dictionary<string, object> {
                    //{ "page_size", 5000 },
                    //{ "start_date", SD},
                    //{ "end_date", ED}
                    //{ "item_types", "" }
			        });

                    Work.Clear();
                    //foreach (var item in result.Data)
                    //{
                        
                    //    if(item.Description.ToString().Contains("10:07"))
                    //    {
                    //        if (item.Description.Contains("\n"))
                    //        {
                    //            item.Description = item.Description.Replace("\n", " ");
                    //        }
                    //        if (item.Description.Contains("\t"))
                    //        {
                    //            item.Description = item.Description.Replace("\t", "");
                    //        }
                    //    }
                    //}
                    
                    
                    foreach (var item in result.Data)
                    {

                        if ((item.Project.Name.ToString().Contains("Systems Process, Inc. [SPI]")) || (item.Project.Name.ToString().Contains("ASEC")))
                        {
                           
                            if (names.Contains(item.User.Name.ToString())) { }

                            else
                            {
                                names.Add(item.User.Name.ToString());
                                namesCount.Add(0);
                            }
                            if (Dates.Contains(item.DateTime.Value.Date.ToString("yyyy/MM/dd")))
                            {

                            }
                            else
                            {
                                Dates.Add(item.DateTime.Value.Date.ToString("yyyy/MM/dd"));
                            }
                        }
                    }
                    Dates.Sort();
                    Dates.Reverse();
                    names.Sort();
                    
                    if (StartDate.HasDropDown) { }
                    else
                    {
                        for (int i = 0; i < Dates.Count; i++)
                        {
                            StartDate.DropDownItems.Add(Dates[i].ToString());
                        }
                    }

                    if (EndDate.HasDropDown) { }
                    else
                    {
                        for (int i = 0; i < Dates.Count; i++)
                        {
                            EndDate.DropDownItems.Add(Dates[i].ToString());
                        }
                    }
                    

                if (UserSelect.HasDropDownItems)
                { }
                else
                {
                    for (int i = 0; i < names.Count; i++)
                    {
                        UserSelect.DropDownItems.Add(names[i].ToString());
                    }
                }
        }
        
        void GetProjects()
		{
			var result = AxosoftProxy.Projects.Get();

			Projects.Clear();

			foreach (var project in result.Data)
			{
				Projects.Add(project);
			}
		}
        //
        //
        //dataBinding
        //
        //
        //
        private void ItemsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((this.ItemsGridView.Rows[e.RowIndex].DataBoundItem != null) &&
                (this.ItemsGridView.Columns[e.ColumnIndex].DataPropertyName.Contains(".")))
            {
                e.Value = BindProperty(
                              this.ItemsGridView.Rows[e.RowIndex].DataBoundItem,
                              this.ItemsGridView.Columns[e.ColumnIndex].DataPropertyName
                            );
            }
        }



        private string BindProperty(object property, string propertyName)
        {
            string retValue = "";

            if (propertyName.Contains("."))
            {
                PropertyInfo[] arrayProperties;
                string leftPropertyName;

                leftPropertyName = propertyName.Substring(0, propertyName.IndexOf("."));
                arrayProperties = property.GetType().GetProperties();

                foreach (PropertyInfo propertyInfo in arrayProperties)
                {
                    if (propertyInfo.Name == leftPropertyName)
                    {
                          retValue = BindProperty(
                          propertyInfo.GetValue(property, null),
                          propertyName.Substring(propertyName.IndexOf(".") + 1));
                        break;
                    }
                }
            }
            else
            {
                Type propertyType;
                PropertyInfo propertyInfo;

                propertyType = property.GetType();
                propertyInfo = propertyType.GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    retValue = propertyInfo.GetValue(property, null).ToString();
                   
                }
            }
           
            return retValue;
        }
        //
        //
        //Print
        //
        //
        
        private void ItemsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UserSelect_Select(object sender, ToolStripItemClickedEventArgs e)
        {
            
            for (int i = 0; i < names.Count; i++)
            {
                string title = names[i].ToString();
                if (UserSelect.DropDownItems[i].Selected)
                {
                    UserIndex = i;
                    if (namesSelected.Contains(UserSelect.DropDownItems[i].Text) == false)
                    {
                    System.Windows.Forms.TabPage myTabPage = new System.Windows.Forms.TabPage(title);
                    tabControl1.TabPages.Add(myTabPage);
                    myTabPage.UseVisualStyleBackColor = true;
                    
                    namesList[i] = myTabPage;
                    tabControl1.SelectedTab = myTabPage;
                    if (tabControl1.TabCount == 1)
                    {
                        Work.Clear();
                        DatesView.Clear();
                        if (UserSelection[i] == false)
                        {

                            firstUser = tabControl1.TabPages[0].Text.ToString();
                        }

                        foreach (var item in result.Data)
                        {

                            if ((item.Project.Name.ToString().Contains("Systems Process, Inc. [SPI]")) || (item.Project.Name.ToString().Contains("ASEC")))
                            {
                                if (tabControl1.TabCount > 0)
                                {
                                    if (item.User.Name.ToString() == title)
                                    {
                                        if (item.DateTime >= DateTime.Parse(SD) && item.DateTime <= DateTime.Parse(ED).AddDays(+1))
                                        {
                                            if (item.WorkDone.TimeUnit.Name.Equals("Minutes"))
                                            {
                                                item.WorkDone.Duration = (item.WorkDone.Duration / 60);
                                                item.WorkDone.Duration = Math.Round((item.WorkDone.Duration.Value), 2);
                                                item.WorkDone.TimeUnit.Name = "Hours";
                                            }
                                            if (namesCount[names.IndexOf(tabControl1.SelectedTab.Text)].Equals(0))
                                            {
                                                localZone = TimeZone.CurrentTimeZone;
                                                item.DateTime = localZone.ToLocalTime(item.DateTime.Value);
                                            }
                                            if (item.Description.Contains("\n"))
                                            {
                                                item.Description = item.Description.Replace("\n", " ");
                                            }
                                            if (item.Description.Contains("\t"))
                                            {
                                                item.Description = item.Description.Replace("\t", "");
                                            }
                                            if (Work.Count == 0)
                                            {
                                                Work.Add(item);
                                                DatesView.Add(item.DateTime.Value.Date.ToString("yyyy/MM/dd"));

                                            }
                                            else if (DatesView.Contains(item.DateTime.Value.Date.ToString("yyyy/MM/dd")) == false)
                                            {
                                                Work.Add(item);
                                                DatesView.Add(item.DateTime.Value.Date.ToString("yyyy/MM/dd"));
                                            }
                                            else
                                            {
                                                for (int a = 0; a < Work.Count; a++)
                                                {
                                                    if (Work[a].DateTime.Value.Date.ToString("yyyy/MM/dd") == item.DateTime.Value.Date.ToString("yyyy/MM/dd"))
                                                    {
                                                       
                                                        if (namesCount[names.IndexOf(tabControl1.TabPages[0].Text)].Equals(0))
                                                        {
                                                            Work[a].Description = Work[a].Description.ToString() + Environment.NewLine +item.Description.ToString();
                                                            if (item.WorkDone.TimeUnit.Name.Equals("Hours"))
                                                                Work[a].WorkDone.Duration += item.WorkDone.Duration;
                                                            else if (item.WorkDone.TimeUnit.Name.Equals("Minutes"))
                                                                Work[a].WorkDone.Duration += (item.WorkDone.Duration / 60);
                                                            else if (item.WorkDone.TimeUnit.Name.Equals("Days"))
                                                                Work[a].WorkDone.Duration += (item.WorkDone.Duration * 24);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }

                        namesCount[names.IndexOf(UserSelect.DropDownItems[i].Text)] = 1;
                        
                        namesList[i].Controls.Add(ItemsGridView);
                        //if (UserSelect.DropDownItems[i].Text == firstUser)
                        namesCount[names.IndexOf(tabControl1.SelectedTab.Text)] = 1;
                        
                    }
                    namesSelected.Add(UserSelect.DropDownItems[i].Text);
                    
                    }
                    
                    
                }
            }
            this.ItemsGridView.Sort(ItemsGridView.Columns[0], ListSortDirection.Ascending);
            //if (UserIndex >= 0)
            //{
            //    UserSelect.DropDownItems.RemoveAt(UserIndex);
            //    names.RemoveAt(UserIndex);
            //}
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void Control1SelectedChanged(object sender, EventArgs e)
        {
            
            Work.Clear();
            DatesView.Clear();
            foreach (var item in result.Data)
            {
                if(tabControl1.TabCount>0)
                UserSelection[names.IndexOf(tabControl1.SelectedTab.Text)] = true;
                if ((item.Project.Name.ToString().Contains("Systems Process, Inc. [SPI]")) || (item.Project.Name.ToString().Contains("ASEC")))
                {
                    if (tabControl1.TabCount > 0)
                    {
                        if (item.User.Name.ToString() == tabControl1.SelectedTab.Text.ToString())
                        {
                            var theDate=DateTime.Today;
                            if (item.DateTime >= DateTime.Parse(SD) && item.DateTime <= DateTime.Parse(ED).AddDays(+1))
                            {
                                if (item.WorkDone.TimeUnit.Name.Equals("Minutes"))
                                {
                                    item.WorkDone.Duration = (item.WorkDone.Duration / 60);
                                    item.WorkDone.Duration = Math.Round((item.WorkDone.Duration.Value), 2);
                                    item.WorkDone.TimeUnit.Name = "Hours";
                                }
                                if (namesCount[names.IndexOf(tabControl1.SelectedTab.Text)].Equals(0))
                                {
                                    localZone = TimeZone.CurrentTimeZone;
                                    item.DateTime = localZone.ToLocalTime(item.DateTime.Value);
                                }
                                if (namesCount[names.IndexOf(tabControl1.SelectedTab.Text)].Equals(0))
                                {
                                    if (item.Description.Contains("\n"))
                                    {
                                        item.Description = item.Description.Replace("\n", " ");
                                    }
                                    if (item.Description.Contains("\t"))
                                    {
                                        item.Description = item.Description.Replace("\t", "");
                                    }
                                }
                                
                                if (Work.Count == 0)
                                {
                                    Work.Add(item);
                                    DatesView.Add(item.DateTime.Value.Date.ToString("yyyy/MM/dd"));

                                }
                                else if (DatesView.Contains(item.DateTime.Value.Date.ToString("yyyy/MM/dd")) == false)
                                {
                                    Work.Add(item);
                                    DatesView.Add(item.DateTime.Value.Date.ToString("yyyy/MM/dd"));
                                }
                                else
                                {
                                    for (int a = 0; a < Work.Count; a++)
                                    {
                                        if (Work[a].DateTime.Value.Date.ToString("yyyy/MM/dd") == item.DateTime.Value.Date.ToString("yyyy/MM/dd"))
                                        {
                                            if (tabControl1.SelectedTab.Text != firstUser)
                                            {
                                                {
                                                    if (namesCount[names.IndexOf(tabControl1.SelectedTab.Text)].Equals(0))
                                                    {
                                                        Work[a].Description = Work[a].Description.ToString() + Environment.NewLine + item.Description.ToString();
                                                        Work[a].WorkDone.Duration += item.WorkDone.Duration;
                                                        
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                }
                                //Work.Add(item);
                         
                        }
                    }

                }
                
            }
            if(tabControl1.TabCount == 1)
            namesCount[names.IndexOf(tabControl1.TabPages[0].Text)] = 1;
            if (tabControl1.TabCount > 0)
            {
                namesCount[names.IndexOf(tabControl1.SelectedTab.Text)] = 1;   
            }
             if (tabControl1.TabPages.Count >0)
            {
                for(int i = 0; i < names.Count; i++)
                {
                    if (tabControl1.SelectedTab.Text.ToString() == names[i].ToString())
                    {
                        namesList[i].Controls.Add(ItemsGridView);
                    }
                }
            }
             this.ItemsGridView.Sort(ItemsGridView.Columns[0], ListSortDirection.Ascending);
        }

        private void ItemsGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.ItemsGridView.Sort(ItemsGridView.Columns[0], ListSortDirection.Ascending);
        }//end ColumnClick

        private void DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            
        }

        private void tabControl1_DoubleClick(object sender, EventArgs e)
        {
            namesSelected.Remove(tabControl1.SelectedTab.Text);
            if (tabControl1.TabCount == 2)
            {
                UserSelection[names.IndexOf(tabControl1.TabPages[1].Text)] = true;
                namesCount[names.IndexOf(tabControl1.TabPages[1].Text)] = 1;   
            }
            else if (tabControl1.SelectedTab.Text != firstUser && UserSelection[names.IndexOf(tabControl1.SelectedTab.Text)] != true)
            namesCount[names.IndexOf(tabControl1.SelectedTab.Text)] = 0;
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void StartDate_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < Dates.Count; i++)
            {
                if (StartDate.DropDownItems[i].Selected == true)
                {
                    SD = StartDate.DropDownItems[i].ToString();
                    
                }

            }
            Work.Clear();
            DatesView.Clear();
            foreach (var item in result.Data)
            {
                if (tabControl1.TabCount > 0)
                    UserSelection[names.IndexOf(tabControl1.SelectedTab.Text)] = true;
                if ((item.Project.Name.ToString().Contains("Systems Process, Inc. [SPI]")) || (item.Project.Name.ToString().Contains("ASEC")))
                {
                    if (tabControl1.TabCount > 0)
                    {
                        if (item.User.Name.ToString() == tabControl1.SelectedTab.Text.ToString())
                        {
                            if (item.DateTime >= DateTime.Parse(SD) && item.DateTime <= DateTime.Parse(ED).AddDays(+1))

                            {
                                if (item.WorkDone.TimeUnit.Name.Equals("Minutes"))
                                {
                                    item.WorkDone.Duration = (item.WorkDone.Duration / 60);
                                    item.WorkDone.Duration = Math.Round((item.WorkDone.Duration.Value), 2);
                                    item.WorkDone.TimeUnit.Name = "Hours";
                                }
                                if (namesCount[names.IndexOf(tabControl1.SelectedTab.Text)].Equals(0))
                                {
                                    localZone = TimeZone.CurrentTimeZone;
                                    item.DateTime = localZone.ToLocalTime(item.DateTime.Value);
                                }
                                if (Work.Count == 0)
                                {
                                    Work.Add(item);
                                    DatesView.Add(item.DateTime.Value.Date.ToString("yyyy/MM/dd"));

                                }
                                else if (DatesView.Contains(item.DateTime.Value.Date.ToString("yyyy/MM/dd")) == false)
                                {
                                    Work.Add(item);
                                    DatesView.Add(item.DateTime.Value.Date.ToString("yyyy/MM/dd"));
                                }
                                //else
                                //{
                                //    for (int a = 0; a < Work.Count; a++)
                                //    {
                                //        if (Work[a].DateTime.Value.Date.ToString("yyyy/MM/dd") == item.DateTime.Value.Date.ToString("yyyy/MM/dd"))
                                //        {
                                //            if (tabControl1.SelectedTab.Text != firstUser)
                                //            {
                                //                {
                                //                    if (namesCount[names.IndexOf(tabControl1.SelectedTab.Text)].Equals(0))
                                //                    {
                                //                        Work[a].Description = Work[a].Description + Environment.NewLine + item.Description;
                                //                        Work[a].WorkDone.Duration += item.WorkDone.Duration;
                                //                    }
                                //                }
                                //            }
                                //        }
                                //    }
                                //}
                            }
                            //Work.Add(item);

                        }
                    }

                }

            }
            if (tabControl1.TabCount == 1)
                namesCount[names.IndexOf(tabControl1.TabPages[0].Text)] = 1;
            if (tabControl1.TabCount > 0)
            {
                //namesCount[names.IndexOf(tabControl1.SelectedTab.Text)] = 1;   
            }
            if (tabControl1.TabPages.Count > 0)
            {
                for (int i = 0; i < names.Count; i++)
                {
                    if (tabControl1.SelectedTab.Text.ToString() == names[i].ToString())
                    {
                        namesList[i].Controls.Add(ItemsGridView);
                    }
                }
            }
            if(tabControl1.TabCount>0)
            this.ItemsGridView.Sort(ItemsGridView.Columns[0], ListSortDirection.Ascending);
        }

        private void EndDate_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < Dates.Count; i++)
            {
                if (EndDate.DropDownItems[i].Selected == true)
                {
                    
                    ED = EndDate.DropDownItems[i].ToString();
                    
                }

            }

            Work.Clear();
            DatesView.Clear();
            foreach (var item in result.Data)
            {
                if (tabControl1.TabCount > 0)
                    UserSelection[names.IndexOf(tabControl1.SelectedTab.Text)] = true;
                if ((item.Project.Name.ToString().Contains("Systems Process, Inc. [SPI]")) || (item.Project.Name.ToString().Contains("ASEC")))
                {
                    if (tabControl1.TabCount > 0)
                    {
                        if (item.User.Name.ToString() == tabControl1.SelectedTab.Text.ToString())
                        {
                            if (item.DateTime >= DateTime.Parse(SD) && item.DateTime <= DateTime.Parse(ED).AddDays(+1))

                            {
                                if (item.WorkDone.TimeUnit.Name.Equals("Minutes"))
                                {
                                    item.WorkDone.Duration = (item.WorkDone.Duration / 60);
                                    item.WorkDone.Duration = Math.Round((item.WorkDone.Duration.Value), 2);
                                    item.WorkDone.TimeUnit.Name = "Hours";
                                }
                                if (namesCount[names.IndexOf(tabControl1.SelectedTab.Text)].Equals(0))
                                {
                                    localZone = TimeZone.CurrentTimeZone;
                                    item.DateTime = localZone.ToLocalTime(item.DateTime.Value);
                                }
                                if (Work.Count == 0)
                                {
                                    Work.Add(item);
                                    DatesView.Add(item.DateTime.Value.Date.ToString("yyyy/MM/dd"));

                                }
                                else if (DatesView.Contains(item.DateTime.Value.Date.ToString("yyyy/MM/dd")) == false)
                                {
                                    Work.Add(item);
                                    DatesView.Add(item.DateTime.Value.Date.ToString("yyyy/MM/dd"));
                                }
                                //else if(namesSelected.Contains(item.User.Name) == false)
                                //{
                                //    for (int a = 0; a < Work.Count; a++)
                                //    {
                                //        if (Work[a].DateTime.Value.Date.ToString("yyyy/MM/dd") == item.DateTime.Value.Date.ToString("yyyy/MM/dd"))
                                //        {
                                //            if (tabControl1.SelectedTab.Text != firstUser)
                                //            {
                                //                {
                                //                    if (namesCount[names.IndexOf(tabControl1.SelectedTab.Text)].Equals(0))
                                //                    {
                                //                        Work[a].Description = Work[a].Description + Environment.NewLine + item.Description;
                                //                        Work[a].WorkDone.Duration += item.WorkDone.Duration;
                                //                    }
                                //                }
                                //            }
                                //        }
                                //    }
                                //}
                            }
                            //Work.Add(item);

                        }
                    }

                }
            }
            if (tabControl1.TabCount == 1)
                namesCount[names.IndexOf(tabControl1.TabPages[0].Text)] = 1;
            if (tabControl1.TabCount > 0)
            {
                //namesCount[names.IndexOf(tabControl1.SelectedTab.Text)] = 1;   
            }
            if (tabControl1.TabPages.Count > 0)
            {
                for (int i = 0; i < names.Count; i++)
                {
                    if (tabControl1.SelectedTab.Text.ToString() == names[i].ToString())
                    {
                        namesList[i].Controls.Add(ItemsGridView);
                    }
                }
            }
            if(tabControl1.TabCount>0)
            this.ItemsGridView.Sort(ItemsGridView.Columns[0], ListSortDirection.Ascending);
        }

        private void Export_Click(object sender, EventArgs e)
        {
            
            // creating Excel Application
            Microsoft.Office.Interop.Excel._Application app  = new Microsoft.Office.Interop.Excel.Application();
 
 
            // creating new WorkBook within Excel application
            Microsoft.Office.Interop.Excel._Workbook workbook =  app.Workbooks.Add(Type.Missing);
           
 
            // creating new Excelsheet in workbook
             //Microsoft.Office.Interop.Excel._Worksheet worksheet = null;                   
           
           // see the excel sheet behind the program
            app.Visible = true;
            Microsoft.Office.Interop.Excel.Worksheet[] worksheet = new Microsoft.Office.Interop.Excel.Worksheet[tabControl1.TabPages.Count];
            
            for (int a = 0; a < tabControl1.TabCount; a++)
            {
                int ColumnCount = 0;
                int RowCount = 0;
                // get the reference of first sheet. By default its name is Sheet1.
                // store its reference to worksheet
                WorksheetList.Add(tabControl1.TabPages[a].Text.ToString());
                if (a == 0)
                    worksheet[a] = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                else
                    worksheet[a] = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets.Add();
                worksheet[a].Name = tabControl1.TabPages[a].Text;
                
                //worksheet[a].Columns.WrapText = false;
                //worksheet[a].Cells.WrapText = false;
                worksheet[a].Range["A1"].ColumnWidth = 30;
                worksheet[a].Range["B1"].ColumnWidth = 7;
                worksheet[a].Range["C1"].ColumnWidth = 150;

                // storing header part in Excel
                for (int i = 1; i < ItemsGridView.Columns.Count + 1; i++)
                {
                    worksheet[a].Cells[1, i] = ItemsGridView.Columns[i - 1].HeaderText;
                }

                tabControl1.SelectedTab = tabControl1.TabPages[a];

                // storing Each row and column value to excel sheet
                for (int i = 0; i < ItemsGridView.Rows.Count ; i++)
                {
                    for (int j = 0; j < ItemsGridView.Columns.Count; j++)
                    {
                        worksheet[a].Cells[i + 2, j + 1] = ItemsGridView.Rows[i].Cells[j].FormattedValue.ToString();
                        if(j==2)
                        ColumnCount++;
                    }
                }

                //worksheet[a].Range["C1:C"+ (ColumnCount+1).ToString()].WrapText = false;
                worksheet[a].Cells[ItemsGridView.Rows.Count + 2, 1] = "Total Hours";
                worksheet[a].Cells[ItemsGridView.Rows.Count + 2, 2] = "=SUM(B1:B" + (ItemsGridView.Rows.Count + 1).ToString() + ")";
            }
            // save the application
            if (Directory.Exists(@"C:\\Ontime\Work Logs") == false)
                Directory.CreateDirectory(@"C:\\Ontime\Work Logs");

                workbook.SaveAs(@"C:\Ontime\Work Logs\" + DateTime.Now.ToString("yyyy.MM.dd hh_mm_ss") + ".xlsx");
            //else
                //MessageBox.Show("A file is already created for this time Please wait one minute");
           
            // Exit from the application
          //app.Quit();
        
        
        }

        private void EndDate_Click(object sender, EventArgs e)
        {

        }

        private void UserSelect_Click(object sender, EventArgs e)
        {

        }




 
        //
        //
        //PrintExcel
        //
        //  
                
	}

	// This is used to POST an attachment, due to a bug in Axosoft that expects the content to be encoded this way
	class UTF8ByteEncoder : ICryptoTransform
	{
		public bool CanReuseTransform
		{
			get { return true; }
		}

		public bool CanTransformMultipleBlocks
		{
			get { return true; }
		}

		public int InputBlockSize
		{
			get { return 1; }
		}

		public int OutputBlockSize
		{
			get { return 2; }
		}

		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			var originalOutputOffset = outputOffset;
			for (var inputIndex = 0; inputIndex < inputCount; inputIndex++)
			{
				var b = inputBuffer[inputOffset + inputIndex];
				if ((b & 128) > 0)
				{
					outputBuffer[outputOffset++] = (byte)((b >> 6) | 0xc0);
					outputBuffer[outputOffset++] = (byte)((b & 0x3f) | 0x80);
				}
				else
					outputBuffer[outputOffset++] = b;
			}
			return outputOffset - originalOutputOffset;
		}

		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			var outputBuffer = new byte[inputBuffer.Length * 2];
			var outputCount = TransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, 0);
			return outputBuffer.Take(outputCount).ToArray();
		}

		public void Dispose()
		{
		}
	}
}
