namespace Yooresh.Client.WinForms.Common.Controls;

public class CustomTextBox:TextBox
{
    public Color FocusColor { get; set; }    
    protected override void OnEnter(EventArgs e)
    {
        this.BackColor = FocusColor;
        base.OnEnter(e);
    }
    protected override void OnLeave(EventArgs e)
    {
        this.BackColor = Color.White;
        base.OnLeave(e);
    }
}
