namespace Yooresh.Client.WinForms.Players.Forms
{
    partial class CreateVillageForm
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
            components = new System.ComponentModel.Container();
            factionPicker1 = new Controls.FactionPicker();
            btnCreateVillage = new Button();
            listBox1 = new ListBox();
            txtVillageName = new TextBox();
            bindingSourceFactions = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)bindingSourceFactions).BeginInit();
            SuspendLayout();
            // 
            // factionPicker1
            // 
            factionPicker1.BackgroundImageLayout = ImageLayout.Stretch;
            factionPicker1.Location = new Point(12, 28);
            factionPicker1.Name = "factionPicker1";
            factionPicker1.Size = new Size(508, 551);
            factionPicker1.TabIndex = 0;
            // 
            // btnCreateVillage
            // 
            btnCreateVillage.Location = new Point(455, 666);
            btnCreateVillage.Name = "btnCreateVillage";
            btnCreateVillage.Size = new Size(75, 23);
            btnCreateVillage.TabIndex = 1;
            btnCreateVillage.Text = "button1";
            btnCreateVillage.UseVisualStyleBackColor = true;
            btnCreateVillage.Click += btnCreateVillage_Click;
            // 
            // listBox1
            // 
            listBox1.DisplayMember = "Name";
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(688, 218);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(296, 319);
            listBox1.TabIndex = 2;
            listBox1.ValueMember = "Id";
            // 
            // txtVillageName
            // 
            txtVillageName.Location = new Point(698, 601);
            txtVillageName.Name = "txtVillageName";
            txtVillageName.Size = new Size(100, 23);
            txtVillageName.TabIndex = 3;
            // 
            // CreateVillageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1093, 798);
            Controls.Add(txtVillageName);
            Controls.Add(listBox1);
            Controls.Add(btnCreateVillage);
            Controls.Add(factionPicker1);
            Name = "CreateVillageForm";
            Text = "CreateVillageForm";
            Shown += CreateVillageForm_Shown;
            ((System.ComponentModel.ISupportInitialize)bindingSourceFactions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.FactionPicker factionPicker1;
        private Button btnCreateVillage;
        private ListBox listBox1;
        private TextBox txtVillageName;
        private BindingSource bindingSourceFactions;
    }
}