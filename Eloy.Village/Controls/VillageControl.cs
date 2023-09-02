using Eloy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eloy.Village.Controls
{
    public partial class VillageControl : UserControl
    {
        public Player? Owner { get; set; }
        public VillageControl()
        {
            InitializeComponent();
        }

        private void VillageControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode && Owner != null)
            {
                AddBindings();
            }
        }

        public void AddBindings()
        {
            lblVillageName.DataBindings.Add(nameof(Label.Text), Owner, nameof(Player.VillageName));
        }
    }
}
