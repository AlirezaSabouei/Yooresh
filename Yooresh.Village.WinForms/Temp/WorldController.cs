namespace Yooresh.Village.WinForms.Temp;

public partial class WorldControl : UserControl
{
    public WorldControl()
    {
        InitializeComponent();
    }

    private void label_MouseEnter(object sender, EventArgs e)
    {
        Label lbl;
        if (sender is Panel)
        {
            lbl = ((sender as Panel)!.Controls[0] as Label)!;
        }
        else
        {
            lbl = (sender as Label)!;
        }

        lbl!.BackColor = Color.SeaShell;
        lbl!.ForeColor = Color.Orange;
    }

    private void label_MouseLeave(object sender, EventArgs e)
    {
        Label lbl;
        if (sender is Panel)
        {
            lbl = ((sender as Panel)!.Controls[0] as Label)!;
        }
        else
        {
            lbl = (sender as Label)!;
        }

        lbl!.BackColor = Color.Transparent;
        lbl!.ForeColor = Color.SeaShell;
    }
}
