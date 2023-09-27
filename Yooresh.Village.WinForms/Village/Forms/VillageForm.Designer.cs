using Yooresh.Village.WinForms.Temp;

namespace Yooresh.Village.WinForms.Village.Forms
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
            villageControl1 = new VillageControl();
            SuspendLayout();
            // 
            // villageControl1
            // 
            villageControl1.BackColor = Color.OliveDrab;
            villageControl1.Dock = DockStyle.Fill;
            villageControl1.Location = new Point(0, 0);
            villageControl1.Name = "villageControl1";
            villageControl1.Size = new Size(898, 684);
            villageControl1.TabIndex = 0;
            // 
            // Village
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(898, 684);
            Controls.Add(villageControl1);
            Name = "VillageForm";
            Text = "Village";
            ResumeLayout(false);
        }

        #endregion

        private VillageControl villageControl1;
    }
}