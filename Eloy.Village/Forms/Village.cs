using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eloy.Village.Forms
{
    public partial class Village : Form
    {
        public Village()
        {
            InitializeComponent();
            this.villageControl1.Owner = new Domain.Entities.Player()
            {
                Name = "ALireza",
                VillageName = "My Village"
            };
        }
    }
}
