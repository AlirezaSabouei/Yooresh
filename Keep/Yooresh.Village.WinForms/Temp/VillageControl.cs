namespace Yooresh.Client.WinForms.Temp
{
    public partial class VillageControl : UserControl
    {
        // public Player? Owner { get; set; }
        public VillageControl()
        {
            InitializeComponent();
        }

        private void VillageControl_Load(object sender, EventArgs e)
        {
            //if (!DesignMode && Owner != null)
            // {
            // AddBindings();
            // }
        }

        public void AddBindings()
        {
            // lblVillageName.DataBindings.Add(nameof(Label.Text), Owner, nameof(Player.VillageName));
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
