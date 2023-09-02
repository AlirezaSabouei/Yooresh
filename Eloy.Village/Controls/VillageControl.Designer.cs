namespace Eloy.Village.Controls
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
            lblVillageName = new Label();
            lblVillageOwner = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
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
            lblVillageOwner.Location = new Point(406, 0);
            lblVillageOwner.Name = "lblVillageOwner";
            lblVillageOwner.Size = new Size(80, 15);
            lblVillageOwner.TabIndex = 1;
            lblVillageOwner.Text = "Village Owner";
            // 
            // tableLayoutPanel1
            // 
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
            tableLayoutPanel1.Size = new Size(807, 48);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // VillageControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.OliveDrab;
            Controls.Add(tableLayoutPanel1);
            Name = "VillageControl";
            Size = new Size(807, 711);
            Load += VillageControl_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblVillageName;
        private Label lblVillageOwner;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
