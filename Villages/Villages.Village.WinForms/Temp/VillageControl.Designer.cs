namespace Yooresh.Client.WinForms.Temp
{
    partial class VillageControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VillageControl));
            lblVillageName = new Label();
            lblVillageOwner = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tavern1 = new Buildings.Tavern();
            garden1 = new Buildings.Garden();
            barracks1 = new Buildings.Barracks();
            castle1 = new Buildings.Castle();
            tower_11 = new Buildings.Tower_1();
            tower_21 = new Buildings.Tower_2();
            blacksmith1 = new Buildings.Blacksmith();
            magic_Center1 = new Buildings.Magic_Center();
            lumbermill1 = new Buildings.Lumbermill();
            resourceControl1 = new Common.Controls.ResourceControl();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblVillageName
            // 
            lblVillageName.AutoSize = true;
            lblVillageName.Location = new Point(3, 0);
            lblVillageName.Name = "lblVillageName";
            lblVillageName.Size = new Size(77, 15);
            lblVillageName.TabIndex = 0;
            lblVillageName.Text = "Village Name";
            // 
            // lblVillageOwner
            // 
            lblVillageOwner.AutoSize = true;
            lblVillageOwner.Location = new Point(515, 0);
            lblVillageOwner.Name = "lblVillageOwner";
            lblVillageOwner.Size = new Size(80, 15);
            lblVillageOwner.TabIndex = 1;
            lblVillageOwner.Text = "Village Owner";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lblVillageName, 0, 0);
            tableLayoutPanel1.Controls.Add(lblVillageOwner, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1024, 48);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // tavern1
            // 
            tavern1.BackColor = Color.Transparent;
            tavern1.BackgroundImage = (Image)resources.GetObject("tavern1.BackgroundImage");
            tavern1.BackgroundImageLayout = ImageLayout.Zoom;
            tavern1.Location = new Point(677, 162);
            tavern1.Name = "tavern1";
            tavern1.Size = new Size(150, 150);
            tavern1.TabIndex = 3;
            // 
            // garden1
            // 
            garden1.BackColor = Color.Transparent;
            garden1.BackgroundImage = (Image)resources.GetObject("garden1.BackgroundImage");
            garden1.BackgroundImageLayout = ImageLayout.Zoom;
            garden1.Location = new Point(150, 639);
            garden1.Name = "garden1";
            garden1.Size = new Size(240, 191);
            garden1.TabIndex = 4;
            // 
            // barracks1
            // 
            barracks1.BackColor = Color.Transparent;
            barracks1.BackgroundImage = (Image)resources.GetObject("barracks1.BackgroundImage");
            barracks1.BackgroundImageLayout = ImageLayout.Zoom;
            barracks1.Location = new Point(151, 108);
            barracks1.Name = "barracks1";
            barracks1.Size = new Size(221, 187);
            barracks1.TabIndex = 5;
            // 
            // castle1
            // 
            castle1.BackColor = Color.Transparent;
            castle1.BackgroundImage = (Image)resources.GetObject("castle1.BackgroundImage");
            castle1.BackgroundImageLayout = ImageLayout.None;
            castle1.Location = new Point(399, 355);
            castle1.Name = "castle1";
            castle1.Size = new Size(234, 202);
            castle1.TabIndex = 6;
            // 
            // tower_11
            // 
            tower_11.BackColor = Color.Transparent;
            tower_11.BackgroundImage = (Image)resources.GetObject("tower_11.BackgroundImage");
            tower_11.BackgroundImageLayout = ImageLayout.None;
            tower_11.Location = new Point(548, 129);
            tower_11.Name = "tower_11";
            tower_11.Size = new Size(125, 135);
            tower_11.TabIndex = 7;
            // 
            // tower_21
            // 
            tower_21.BackColor = Color.Transparent;
            tower_21.BackgroundImage = (Image)resources.GetObject("tower_21.BackgroundImage");
            tower_21.BackgroundImageLayout = ImageLayout.None;
            tower_21.Location = new Point(390, 129);
            tower_21.Name = "tower_21";
            tower_21.Size = new Size(152, 151);
            tower_21.TabIndex = 8;
            // 
            // blacksmith1
            // 
            blacksmith1.BackColor = Color.Transparent;
            blacksmith1.BackgroundImage = (Image)resources.GetObject("blacksmith1.BackgroundImage");
            blacksmith1.BackgroundImageLayout = ImageLayout.Zoom;
            blacksmith1.Location = new Point(699, 522);
            blacksmith1.Name = "blacksmith1";
            blacksmith1.Size = new Size(160, 123);
            blacksmith1.TabIndex = 9;
            // 
            // magic_Center1
            // 
            magic_Center1.BackColor = Color.Transparent;
            magic_Center1.BackgroundImage = (Image)resources.GetObject("magic_Center1.BackgroundImage");
            magic_Center1.BackgroundImageLayout = ImageLayout.Zoom;
            magic_Center1.Location = new Point(503, 666);
            magic_Center1.Name = "magic_Center1";
            magic_Center1.Size = new Size(214, 155);
            magic_Center1.TabIndex = 10;
            // 
            // lumbermill1
            // 
            lumbermill1.BackColor = Color.Transparent;
            lumbermill1.BackgroundImage = (Image)resources.GetObject("lumbermill1.BackgroundImage");
            lumbermill1.BackgroundImageLayout = ImageLayout.Zoom;
            lumbermill1.Location = new Point(807, 294);
            lumbermill1.Name = "lumbermill1";
            lumbermill1.Size = new Size(136, 88);
            lumbermill1.TabIndex = 11;
            // 
            // resourceControl1
            // 
            resourceControl1.Location = new Point(865, 80);
            resourceControl1.Name = "resourceControl1";
            resourceControl1.Size = new Size(108, 149);
            resourceControl1.TabIndex = 12;
            // 
            // VillageControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.None;
            Controls.Add(resourceControl1);
            Controls.Add(blacksmith1);
            Controls.Add(lumbermill1);
            Controls.Add(magic_Center1);
            Controls.Add(tower_21);
            Controls.Add(tower_11);
            Controls.Add(castle1);
            Controls.Add(barracks1);
            Controls.Add(garden1);
            Controls.Add(tavern1);
            Controls.Add(tableLayoutPanel1);
            Name = "VillageControl";
            Size = new Size(1024, 1024);
            Load += VillageControl_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblVillageName;
        private Label lblVillageOwner;
        private TableLayoutPanel tableLayoutPanel1;
        private Buildings.Tavern tavern1;
        private Buildings.Garden garden1;
        private Buildings.Barracks barracks1;
        private Buildings.Castle castle1;
        private Buildings.Tower_1 tower_11;
        private Buildings.Tower_2 tower_21;
        private Buildings.Blacksmith blacksmith1;
        private Buildings.Magic_Center magic_Center1;
        private Buildings.Lumbermill lumbermill1;
        private Common.Controls.ResourceControl resourceControl1;
    }
}
