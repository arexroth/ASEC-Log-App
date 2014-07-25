namespace WinApp
{
	partial class ItemsControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemsControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.UserSelect = new System.Windows.Forms.ToolStripDropDownButton();
            this.StartDate = new System.Windows.Forms.ToolStripDropDownButton();
            this.EndDate = new System.Windows.Forms.ToolStripDropDownButton();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.project = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AxosoftHostLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ItemsGridView = new System.Windows.Forms.DataGridView();
            this.dateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Export = new System.Windows.Forms.Button();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UserSelect,
            this.StartDate,
            this.EndDate});
            this.toolStrip2.Location = new System.Drawing.Point(4, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(780, 33);
            this.toolStrip2.TabIndex = 18;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // UserSelect
            // 
            this.UserSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.UserSelect.Image = ((System.Drawing.Image)(resources.GetObject("UserSelect.Image")));
            this.UserSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UserSelect.Name = "UserSelect";
            this.UserSelect.Size = new System.Drawing.Size(77, 30);
            this.UserSelect.Text = "User Select";
            this.UserSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.UserSelect.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.UserSelect_Select);
            this.UserSelect.Click += new System.EventHandler(this.UserSelect_Click);
            // 
            // StartDate
            // 
            this.StartDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StartDate.Image = ((System.Drawing.Image)(resources.GetObject("StartDate.Image")));
            this.StartDate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(71, 30);
            this.StartDate.Text = "Start Date";
            this.StartDate.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.StartDate_DropDownItemClicked);
            // 
            // EndDate
            // 
            this.EndDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EndDate.Image = ((System.Drawing.Image)(resources.GetObject("EndDate.Image")));
            this.EndDate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(67, 30);
            this.EndDate.Text = "End Date";
            this.EndDate.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.EndDate_DropDownItemClicked);
            this.EndDate.Click += new System.EventHandler(this.EndDate_Click);
            // 
            // id
            // 
            this.id.DataPropertyName = "Release.Id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.Width = 30;
            // 
            // name
            // 
            this.name.DataPropertyName = "User.Name";
            this.name.HeaderText = "User Name";
            this.name.Name = "name";
            // 
            // type
            // 
            this.type.DataPropertyName = "Item.ItemType";
            this.type.HeaderText = "Type";
            this.type.Name = "type";
            this.type.Width = 50;
            // 
            // project
            // 
            this.project.DataPropertyName = "Project.Name";
            this.project.HeaderText = "Project";
            this.project.Name = "project";
            this.project.Width = 150;
            // 
            // AxosoftHostLabel
            // 
            this.AxosoftHostLabel.AutoSize = true;
            this.AxosoftHostLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AxosoftHostLabel.Location = new System.Drawing.Point(133, 20);
            this.AxosoftHostLabel.Name = "AxosoftHostLabel";
            this.AxosoftHostLabel.Size = new System.Drawing.Size(0, 13);
            this.AxosoftHostLabel.TabIndex = 17;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(4, 36);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(780, 700);
            this.tabControl1.TabIndex = 19;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.Control1SelectedChanged);
            this.tabControl1.DoubleClick += new System.EventHandler(this.tabControl1_DoubleClick);
            // 
            // ItemsGridView
            // 
            this.ItemsGridView.AllowUserToAddRows = false;
            this.ItemsGridView.AllowUserToDeleteRows = false;
            this.ItemsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ItemsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateTime,
            this.Text,
            this.description});
            this.ItemsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ItemsGridView.Location = new System.Drawing.Point(0, 0);
            this.ItemsGridView.Name = "ItemsGridView";
            this.ItemsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ItemsGridView.Size = new System.Drawing.Size(766, 248);
            this.ItemsGridView.TabIndex = 9;
            this.ItemsGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ItemsGridView_CellFormatting);
            this.ItemsGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ItemsGridView_ColumnHeaderMouseClick);
            this.ItemsGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DataBindingComplete);
            // 
            // dateTime
            // 
            this.dateTime.DataPropertyName = "dateTime";
            dataGridViewCellStyle2.Format = "D";
            this.dateTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.dateTime.HeaderText = "Date";
            this.dateTime.Name = "dateTime";
            this.dateTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dateTime.Width = 180;
            // 
            // Text
            // 
            this.Text.DataPropertyName = "WorkDone.Duration";
            this.Text.HeaderText = "Hours";
            this.Text.Name = "Text";
            // 
            // description
            // 
            this.description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.description.DataPropertyName = "description";
            this.description.HeaderText = "Daily Journal";
            this.description.Name = "description";
            // 
            // Export
            // 
            this.Export.Location = new System.Drawing.Point(392, 7);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(75, 23);
            this.Export.TabIndex = 20;
            this.Export.Text = "Export";
            this.Export.UseVisualStyleBackColor = true;
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // ItemsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Export);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.AxosoftHostLabel);
            this.Controls.Add(this.tabControl1);
            this.Name = "ItemsControl";
            this.Size = new System.Drawing.Size(787, 334);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label AxosoftHostLabel;
		private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn project;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripDropDownButton UserSelect;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn newText;
        private System.Windows.Forms.ToolStripDropDownButton StartDate;
        private System.Windows.Forms.ToolStripDropDownButton EndDate;
        private System.Windows.Forms.DataGridView ItemsGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Text;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.Button Export;
        
	}

}
