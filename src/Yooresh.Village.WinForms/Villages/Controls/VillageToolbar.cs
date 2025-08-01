namespace Yooresh.Client.WinForms.Villages.Controls
{
    public partial class VillageToolbar : UserControl
    {
        public VillageToolbar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Height == button1.Height)
            {
                this.Height = 100;
            }
            else
            {
                this.Height = button1.Height;

            }
        }
    }
}
