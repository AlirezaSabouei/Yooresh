namespace Yooresh.Client.WinForms.Villages.Controls
{
    partial class VillageToolbar
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
            button1 = new Button();
            button2 = new Button();
            btnUpgrades = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Dock = DockStyle.Bottom;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Image = Properties.Resources.anvil;
            button1.Location = new Point(0, 580);
            button1.Name = "button1";
            button1.Size = new Size(349, 79);
            button1.TabIndex = 2;
            button1.Text = "Upgrades";
            button1.TextAlign = ContentAlignment.MiddleRight;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Bottom;
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Image = Properties.Resources.anvil;
            button2.Location = new Point(0, 501);
            button2.Name = "button2";
            button2.Size = new Size(349, 79);
            button2.TabIndex = 3;
            button2.Text = "Upgrades";
            button2.TextAlign = ContentAlignment.MiddleRight;
            button2.TextImageRelation = TextImageRelation.ImageBeforeText;
            button2.UseVisualStyleBackColor = true;
            // 
            // btnUpgrades
            // 
            btnUpgrades.Dock = DockStyle.Bottom;
            btnUpgrades.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnUpgrades.Image = Properties.Resources.anvil;
            btnUpgrades.Location = new Point(0, 422);
            btnUpgrades.Name = "btnUpgrades";
            btnUpgrades.Size = new Size(349, 79);
            btnUpgrades.TabIndex = 4;
            btnUpgrades.Text = "Upgrades";
            btnUpgrades.TextAlign = ContentAlignment.MiddleRight;
            btnUpgrades.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUpgrades.UseVisualStyleBackColor = true;
            // 
            // VillageToolbar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnUpgrades);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "VillageToolbar";
            Size = new Size(349, 659);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button btnUpgrades;
    }
}
