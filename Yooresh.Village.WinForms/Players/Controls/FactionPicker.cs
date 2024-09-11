using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yooresh.Client.WinForms.Players.Controls
{
    public partial class FactionPicker : UserControl
    {
        public FactionPicker()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //pictureBox2.BackColor = Color.DarkOliveGreen;
            // panel1.BackColor = Color.DarkOliveGreen;
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            (sender as PictureBox).SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            var pic = (sender as PictureBox);
            pic.BorderStyle = BorderStyle.None;
            pic.BackColor = Color.Khaki;
            panel1.BackColor = Color.Khaki;
            panel1.BorderStyle = BorderStyle.None;

            foreach (Control c in this.Controls)
            {
                if (c is PictureBox && c.Name != pic.Name)
                {
                    (c as PictureBox).BorderStyle = BorderStyle.None;
                }
            }
        }
    }
}
