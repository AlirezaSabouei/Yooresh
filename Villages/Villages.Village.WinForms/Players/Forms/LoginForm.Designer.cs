using System.ComponentModel;

namespace Yooresh.Client.WinForms.Players.Forms;

partial class LoginForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        groupBox2 = new GroupBox();
        checkBox1 = new CheckBox();
        lblSignUp = new Label();
        btnLogin = new Button();
        txtPassword = new Common.Controls.CustomTextBox();
        txtEmail = new Common.Controls.CustomTextBox();
        label2 = new Label();
        label1 = new Label();
        progressBar1 = new ProgressBar();
        lblDescription = new Label();
        lblTitle = new Label();
        groupBox2.SuspendLayout();
        SuspendLayout();
        // 
        // groupBox2
        // 
        groupBox2.Anchor = AnchorStyles.None;
        groupBox2.BackColor = Color.Transparent;
        groupBox2.Controls.Add(checkBox1);
        groupBox2.Controls.Add(lblSignUp);
        groupBox2.Controls.Add(btnLogin);
        groupBox2.Controls.Add(txtPassword);
        groupBox2.Controls.Add(txtEmail);
        groupBox2.Controls.Add(label2);
        groupBox2.Controls.Add(label1);
        groupBox2.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
        groupBox2.ForeColor = Color.White;
        groupBox2.Location = new Point(124, 138);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(461, 359);
        groupBox2.TabIndex = 10;
        groupBox2.TabStop = false;
        groupBox2.Text = "Login";
        // 
        // checkBox1
        // 
        checkBox1.AutoSize = true;
        checkBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
        checkBox1.Location = new Point(303, 319);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new Size(152, 29);
        checkBox1.TabIndex = 12;
        checkBox1.Text = "Remember me";
        checkBox1.UseVisualStyleBackColor = true;
        // 
        // lblSignUp
        // 
        lblSignUp.BackColor = Color.Transparent;
        lblSignUp.Cursor = Cursors.Hand;
        lblSignUp.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
        lblSignUp.ForeColor = Color.White;
        lblSignUp.Location = new Point(8, 319);
        lblSignUp.Margin = new Padding(5);
        lblSignUp.Name = "lblSignUp";
        lblSignUp.Size = new Size(201, 35);
        lblSignUp.TabIndex = 11;
        lblSignUp.Text = "Have no account yet?";
        lblSignUp.Click += lblSignUp_Click;
        // 
        // btnLogin
        // 
        btnLogin.BackColor = Color.White;
        btnLogin.FlatStyle = FlatStyle.System;
        btnLogin.Location = new Point(119, 243);
        btnLogin.Margin = new Padding(3, 15, 3, 3);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(213, 55);
        btnLogin.TabIndex = 4;
        btnLogin.Text = "Enter the village!";
        btnLogin.UseVisualStyleBackColor = false;
        btnLogin.Click += btnLogin_Click;
        // 
        // txtPassword
        // 
        txtPassword.FocusColor = Color.FromArgb(255, 224, 192);
        txtPassword.Location = new Point(43, 186);
        txtPassword.Margin = new Padding(40, 10, 40, 3);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(375, 39);
        txtPassword.TabIndex = 1;
        // 
        // txtEmail
        // 
        txtEmail.BorderStyle = BorderStyle.FixedSingle;
        txtEmail.FocusColor = Color.FromArgb(255, 224, 192);
        txtEmail.Location = new Point(43, 87);
        txtEmail.Margin = new Padding(40, 10, 40, 3);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(375, 39);
        txtEmail.TabIndex = 0;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        label2.Location = new Point(17, 144);
        label2.Margin = new Padding(3, 15, 3, 0);
        label2.Name = "label2";
        label2.Size = new Size(154, 25);
        label2.TabIndex = 1;
        label2.Text = "Your Passwordl:";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.BackColor = Color.Transparent;
        label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        label1.Location = new Point(17, 45);
        label1.Margin = new Padding(3, 15, 3, 0);
        label1.Name = "label1";
        label1.Size = new Size(111, 25);
        label1.TabIndex = 0;
        label1.Text = "Your Email:";
        // 
        // progressBar1
        // 
        progressBar1.ForeColor = Color.IndianRed;
        progressBar1.Location = new Point(167, 524);
        progressBar1.Name = "progressBar1";
        progressBar1.Size = new Size(375, 34);
        progressBar1.Style = ProgressBarStyle.Marquee;
        progressBar1.TabIndex = 5;
        progressBar1.Value = 50;
        progressBar1.Visible = false;
        // 
        // lblDescription
        // 
        lblDescription.BackColor = Color.Transparent;
        lblDescription.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
        lblDescription.ForeColor = Color.White;
        lblDescription.Location = new Point(46, 95);
        lblDescription.Margin = new Padding(5);
        lblDescription.Name = "lblDescription";
        lblDescription.Size = new Size(441, 45);
        lblDescription.TabIndex = 9;
        lblDescription.Text = "Create your army and start a war!";
        // 
        // lblTitle
        // 
        lblTitle.BackColor = Color.Transparent;
        lblTitle.Font = new Font("Segoe UI Semibold", 48F, FontStyle.Bold, GraphicsUnit.Point);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(14, 5);
        lblTitle.Margin = new Padding(5);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(311, 89);
        lblTitle.TabIndex = 8;
        lblTitle.Text = "Yooresh";
        lblTitle.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // LoginForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackgroundImage = Properties.Resources.Login1;
        BackgroundImageLayout = ImageLayout.Stretch;
        ClientSize = new Size(708, 580);
        Controls.Add(groupBox2);
        Controls.Add(progressBar1);
        Controls.Add(lblDescription);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MaximumSize = new Size(724, 619);
        MinimizeBox = false;
        MinimumSize = new Size(724, 619);
        Name = "LoginForm";
        ProgressBar = progressBar1;
        Text = "Login";
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private GroupBox groupBox2;
    private ProgressBar progressBar1;
    private Button btnLogin;
    private Common.Controls.CustomTextBox txtPassword;
    private Common.Controls.CustomTextBox txtEmail;
    private Label label2;
    private Label label1;
    private Label lblDescription;
    private Label lblTitle;
    private Label lblSignUp;
    private CheckBox checkBox1;
}