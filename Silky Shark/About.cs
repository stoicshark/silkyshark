using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Silky_Shark
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        // To the GitHub page
        private void linkLabel_toGit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/stoicshark/silkyshark");
        }
    }
}
