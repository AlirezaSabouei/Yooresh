namespace Yooresh.Client.WinForms.Villages.Forms
{
    partial class VillageForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VillageForm));
            blacksmith1 = new Temp.Buildings.Blacksmith();
            lumbermill1 = new Temp.Buildings.Lumbermill();
            magic_Center1 = new Temp.Buildings.Magic_Center();
            tower_21 = new Temp.Buildings.Tower_2();
            tower_11 = new Temp.Buildings.Tower_1();
            castle1 = new Temp.Buildings.Castle();
            barracks1 = new Temp.Buildings.Barracks();
            garden1 = new Temp.Buildings.Garden();
            tavern1 = new Temp.Buildings.Tavern();
            resourceControl1 = new Common.Controls.ResourceControl();
            btnUpgrades = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // blacksmith1
            // 
            blacksmith1.BackColor = Color.Transparent;
            blacksmith1.BackgroundImage = (Image)resources.GetObject("blacksmith1.BackgroundImage");
            blacksmith1.BackgroundImageLayout = ImageLayout.Zoom;
            blacksmith1.Location = new Point(244, 456);
            blacksmith1.Name = "blacksmith1";
            blacksmith1.Size = new Size(160, 123);
            blacksmith1.TabIndex = 19;
            // 
            // lumbermill1
            // 
            lumbermill1.BackColor = Color.Transparent;
            lumbermill1.BackgroundImage = (Image)resources.GetObject("lumbermill1.BackgroundImage");
            lumbermill1.BackgroundImageLayout = ImageLayout.Zoom;
            lumbermill1.Location = new Point(639, 322);
            lumbermill1.Name = "lumbermill1";
            lumbermill1.Size = new Size(136, 88);
            lumbermill1.TabIndex = 21;
            // 
            // magic_Center1
            // 
            magic_Center1.BackColor = Color.Transparent;
            magic_Center1.BackgroundImage = (Image)resources.GetObject("magic_Center1.BackgroundImage");
            magic_Center1.BackgroundImageLayout = ImageLayout.Zoom;
            magic_Center1.Location = new Point(399, 237);
            magic_Center1.Name = "magic_Center1";
            magic_Center1.Size = new Size(214, 155);
            magic_Center1.TabIndex = 20;
            // 
            // tower_21
            // 
            tower_21.BackColor = Color.Transparent;
            tower_21.BackgroundImage = (Image)resources.GetObject("tower_21.BackgroundImage");
            tower_21.BackgroundImageLayout = ImageLayout.None;
            tower_21.Location = new Point(390, 96);
            tower_21.Name = "tower_21";
            tower_21.Size = new Size(152, 151);
            tower_21.TabIndex = 18;
            // 
            // tower_11
            // 
            tower_11.BackColor = Color.Transparent;
            tower_11.BackgroundImage = (Image)resources.GetObject("tower_11.BackgroundImage");
            tower_11.BackgroundImageLayout = ImageLayout.None;
            tower_11.Location = new Point(548, 96);
            tower_11.Name = "tower_11";
            tower_11.Size = new Size(125, 135);
            tower_11.TabIndex = 17;
            // 
            // castle1
            // 
            castle1.BackColor = Color.Transparent;
            castle1.BackgroundImage = (Image)resources.GetObject("castle1.BackgroundImage");
            castle1.BackgroundImageLayout = ImageLayout.None;
            castle1.Location = new Point(410, 398);
            castle1.Name = "castle1";
            castle1.Size = new Size(234, 202);
            castle1.TabIndex = 16;
            // 
            // barracks1
            // 
            barracks1.BackColor = Color.Transparent;
            barracks1.BackgroundImage = (Image)resources.GetObject("barracks1.BackgroundImage");
            barracks1.BackgroundImageLayout = ImageLayout.Zoom;
            barracks1.Location = new Point(151, 75);
            barracks1.Name = "barracks1";
            barracks1.Size = new Size(221, 187);
            barracks1.TabIndex = 15;
            // 
            // garden1
            // 
            garden1.BackColor = Color.Transparent;
            garden1.BackgroundImage = (Image)resources.GetObject("garden1.BackgroundImage");
            garden1.BackgroundImageLayout = ImageLayout.Zoom;
            garden1.Location = new Point(164, 268);
            garden1.Name = "garden1";
            garden1.Size = new Size(240, 191);
            garden1.TabIndex = 14;
            // 
            // tavern1
            // 
            tavern1.BackColor = Color.Transparent;
            tavern1.BackgroundImage = (Image)resources.GetObject("tavern1.BackgroundImage");
            tavern1.BackgroundImageLayout = ImageLayout.Zoom;
            tavern1.Location = new Point(677, 129);
            tavern1.Name = "tavern1";
            tavern1.Size = new Size(150, 150);
            tavern1.TabIndex = 13;
            // 
            // resourceControl1
            // 
            resourceControl1.Location = new Point(850, 12);
            resourceControl1.Name = "resourceControl1";
            resourceControl1.Size = new Size(133, 160);
            resourceControl1.TabIndex = 22;
            // 
            // btnUpgrades
            // 
            btnUpgrades.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnUpgrades.Image = Properties.Resources.cabin;
            btnUpgrades.Location = new Point(878, 707);
            btnUpgrades.Name = "btnUpgrades";
            btnUpgrades.Size = new Size(118, 49);
            btnUpgrades.TabIndex = 26;
            btnUpgrades.Text = "Construction";
            btnUpgrades.TextAlign = ContentAlignment.MiddleRight;
            btnUpgrades.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUpgrades.UseVisualStyleBackColor = true;
            btnUpgrades.Click += btnUpgrades_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(754, 707);
            button1.Name = "button1";
            button1.Size = new Size(118, 49);
            button1.TabIndex = 27;
            button1.Text = "Troops";
            button1.TextAlign = ContentAlignment.MiddleRight;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Image = Properties.Resources.location;
            button2.Location = new Point(630, 707);
            button2.Name = "button2";
            button2.Size = new Size(118, 49);
            button2.TabIndex = 28;
            button2.Text = "World Map";
            button2.TextAlign = ContentAlignment.MiddleRight;
            button2.TextImageRelation = TextImageRelation.ImageBeforeText;
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button3.Image = Properties.Resources.team;
            button3.Location = new Point(506, 707);
            button3.Name = "button3";
            button3.Size = new Size(118, 49);
            button3.TabIndex = 29;
            button3.Text = "Guild";
            button3.TextAlign = ContentAlignment.MiddleRight;
            button3.TextImageRelation = TextImageRelation.ImageBeforeText;
            button3.UseVisualStyleBackColor = true;
            // 
            // VillageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.VillageBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1008, 768);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(btnUpgrades);
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
            Name = "VillageForm";
            Text = "Village";
            Load += VillageForm_Load;
            Shown += VillageForm_Shown;
            ResumeLayout(false);
        }

        #endregion
        private Temp.Buildings.Blacksmith blacksmith1;
        private Temp.Buildings.Lumbermill lumbermill1;
        private Temp.Buildings.Magic_Center magic_Center1;
        private Temp.Buildings.Tower_2 tower_21;
        private Temp.Buildings.Tower_1 tower_11;
        private Temp.Buildings.Castle castle1;
        private Temp.Buildings.Barracks barracks1;
        private Temp.Buildings.Garden garden1;
        private Temp.Buildings.Tavern tavern1;
        private Common.Controls.ResourceControl resourceControl1;
        private Controls.VillageToolbar villageToolbar1;
        private Button btnUpgrades;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}