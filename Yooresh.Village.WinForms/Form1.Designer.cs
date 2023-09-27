using Yooresh.Village.WinForms.Temp;

namespace Yooresh.Village.WinForms;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        worldController1 = new WorldControl();
        SuspendLayout();
        // 
        // worldController1
        // 
        worldController1.BackColor = Color.White;
        worldController1.BackgroundImage = (Image)resources.GetObject("worldController1.BackgroundImage");
        worldController1.BackgroundImageLayout = ImageLayout.Zoom;
        worldController1.Location = new Point(12, 12);
        worldController1.Name = "worldController1";
        worldController1.Size = new Size(1002, 566);
        worldController1.TabIndex = 0;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1023, 588);
        Controls.Add(worldController1);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Form1";
        ResumeLayout(false);
    }

    #endregion

    private WorldControl worldController1;
}