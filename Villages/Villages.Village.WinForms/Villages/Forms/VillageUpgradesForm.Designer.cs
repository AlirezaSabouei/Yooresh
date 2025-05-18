namespace Yooresh.Client.WinForms.Villages.Forms
{
    partial class VillageUpgradesForm
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
            tabControl1 = new TabControl();
            tabPageResourceBuildings = new TabPage();
            btnUpgradeResourceBuilding = new Button();
            listBoxResourceBuildings = new ListBox();
            tabPage2 = new TabPage();
            tabControl1.SuspendLayout();
            tabPageResourceBuildings.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPageResourceBuildings);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(610, 564);
            tabControl1.TabIndex = 0;
            // 
            // tabPageResourceBuildings
            // 
            tabPageResourceBuildings.Controls.Add(btnUpgradeResourceBuilding);
            tabPageResourceBuildings.Controls.Add(listBoxResourceBuildings);
            tabPageResourceBuildings.Location = new Point(4, 24);
            tabPageResourceBuildings.Name = "tabPageResourceBuildings";
            tabPageResourceBuildings.Padding = new Padding(3);
            tabPageResourceBuildings.Size = new Size(602, 536);
            tabPageResourceBuildings.TabIndex = 0;
            tabPageResourceBuildings.Text = "Resource Buildings";
            tabPageResourceBuildings.UseVisualStyleBackColor = true;
            // 
            // btnUpgradeResourceBuilding
            // 
            btnUpgradeResourceBuilding.Location = new Point(421, 97);
            btnUpgradeResourceBuilding.Name = "btnUpgradeResourceBuilding";
            btnUpgradeResourceBuilding.Size = new Size(113, 48);
            btnUpgradeResourceBuilding.TabIndex = 1;
            btnUpgradeResourceBuilding.Text = "Upgrade";
            btnUpgradeResourceBuilding.UseVisualStyleBackColor = true;
            btnUpgradeResourceBuilding.Click += btnUpgradeResourceBuilding_Click;
            // 
            // listBoxResourceBuildings
            // 
            listBoxResourceBuildings.DisplayMember = "UpgradeName";
            listBoxResourceBuildings.FormattingEnabled = true;
            listBoxResourceBuildings.ItemHeight = 15;
            listBoxResourceBuildings.Location = new Point(6, 6);
            listBoxResourceBuildings.Name = "listBoxResourceBuildings";
            listBoxResourceBuildings.Size = new Size(334, 514);
            listBoxResourceBuildings.TabIndex = 0;
            listBoxResourceBuildings.ValueMember = "Id";
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(602, 536);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // VillageUpgradesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(634, 588);
            Controls.Add(tabControl1);
            Name = "VillageUpgradesForm";
            Text = "VillageUpgradesForm";
            Load += VillageUpgradesForm_Load;
            Shown += VillageUpgradesForm_Shown;
            tabControl1.ResumeLayout(false);
            tabPageResourceBuildings.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPageResourceBuildings;
        private TabPage tabPage2;
        private ListBox listBoxResourceBuildings;
        private Button btnUpgradeResourceBuilding;
    }
}