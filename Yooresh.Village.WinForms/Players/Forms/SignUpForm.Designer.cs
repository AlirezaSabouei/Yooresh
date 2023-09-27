namespace Yooresh.Village.WinForms.Players.Forms
{
    partial class SignUpForm
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
            groupBox2 = new GroupBox();
            btnSignUp = new Button();
            txtConfirmation = new Common.Controls.CustomTextBox();
            txtEmail = new Common.Controls.CustomTextBox();
            txtPassword = new Common.Controls.CustomTextBox();
            txtName = new Common.Controls.CustomTextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            lblDescription = new Label();
            lblTitle = new Label();
            progressBar1 = new ProgressBar();
            bindingSource1 = new BindingSource(components);
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.None;
            groupBox2.BackColor = Color.Transparent;
            groupBox2.Controls.Add(btnSignUp);
            groupBox2.Controls.Add(txtConfirmation);
            groupBox2.Controls.Add(txtEmail);
            groupBox2.Controls.Add(txtPassword);
            groupBox2.Controls.Add(txtName);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox2.ForeColor = Color.White;
            groupBox2.Location = new Point(154, 142);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(461, 522);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Create Account";
            // 
            // btnSignUp
            // 
            btnSignUp.BackColor = Color.White;
            btnSignUp.FlatStyle = FlatStyle.System;
            btnSignUp.Location = new Point(124, 441);
            btnSignUp.Margin = new Padding(3, 15, 3, 3);
            btnSignUp.Name = "btnSignUp";
            btnSignUp.Size = new Size(213, 55);
            btnSignUp.TabIndex = 4;
            btnSignUp.Text = "Start The War!";
            btnSignUp.UseVisualStyleBackColor = false;
            btnSignUp.Click += btnSignUp_Click;
            // 
            // txtConfirmation
            // 
            txtConfirmation.FocusColor = Color.FromArgb(255, 224, 192);
            txtConfirmation.Location = new Point(43, 384);
            txtConfirmation.Margin = new Padding(40, 10, 40, 3);
            txtConfirmation.Name = "txtConfirmation";
            txtConfirmation.Size = new Size(375, 39);
            txtConfirmation.TabIndex = 3;
            // 
            // txtEmail
            // 
            txtEmail.FocusColor = Color.FromArgb(255, 224, 192);
            txtEmail.Location = new Point(43, 186);
            txtEmail.Margin = new Padding(40, 10, 40, 3);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(375, 39);
            txtEmail.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.FocusColor = Color.FromArgb(255, 224, 192);
            txtPassword.Location = new Point(43, 285);
            txtPassword.Margin = new Padding(40, 10, 40, 3);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(375, 39);
            txtPassword.TabIndex = 2;
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.FocusColor = Color.FromArgb(255, 224, 192);
            txtName.Location = new Point(43, 87);
            txtName.Margin = new Padding(40, 10, 40, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(375, 39);
            txtName.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(17, 342);
            label4.Margin = new Padding(3, 15, 3, 0);
            label4.Name = "label4";
            label4.Size = new Size(180, 25);
            label4.TabIndex = 3;
            label4.Text = "Confirm Password:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(17, 243);
            label3.Margin = new Padding(3, 15, 3, 0);
            label3.Name = "label3";
            label3.Size = new Size(149, 25);
            label3.TabIndex = 2;
            label3.Text = "Your Password:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(17, 144);
            label2.Margin = new Padding(3, 15, 3, 0);
            label2.Name = "label2";
            label2.Size = new Size(111, 25);
            label2.TabIndex = 1;
            label2.Text = "Your Email:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(17, 45);
            label1.Margin = new Padding(3, 15, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(116, 25);
            label1.TabIndex = 0;
            label1.Text = "Your Name:";
            // 
            // lblDescription
            // 
            lblDescription.BackColor = Color.Transparent;
            lblDescription.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            lblDescription.ForeColor = Color.White;
            lblDescription.Location = new Point(32, 92);
            lblDescription.Margin = new Padding(5);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(441, 45);
            lblDescription.TabIndex = 6;
            lblDescription.Text = "Create your army and start a war!";
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI Semibold", 48F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(14, 14);
            lblTitle.Margin = new Padding(5);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(311, 89);
            lblTitle.TabIndex = 5;
            lblTitle.Text = "Yooresh";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            progressBar1.ForeColor = Color.IndianRed;
            progressBar1.Location = new Point(197, 690);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(375, 34);
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 6;
            progressBar1.Value = 50;
            progressBar1.Visible = false;
            // 
            // SignUpForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.SignUp;
            ClientSize = new Size(768, 768);
            Controls.Add(progressBar1);
            Controls.Add(groupBox2);
            Controls.Add(lblDescription);
            Controls.Add(lblTitle);
            MaximizeBox = false;
            MaximumSize = new Size(1040, 1006);
            MinimumSize = new Size(784, 807);
            Name = "SignUpForm";
            ProgressBar = progressBar1;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Create Account";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox2;
        private Button btnSignUp;
        private Common.Controls.CustomTextBox txtConfirmation;
        private Common.Controls.CustomTextBox txtEmail;
        private Common.Controls.CustomTextBox txtPassword;
        private Common.Controls.CustomTextBox txtName;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label lblDescription;
        private Label lblTitle;
        private ProgressBar progressBar1;
        private BindingSource bindingSource1;
    }
}