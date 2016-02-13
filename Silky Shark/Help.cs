using System.IO;
using System.Windows.Forms;

namespace Silky_Shark
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
            try
            {
                string html = File.ReadAllText("Silky Shark Help.html");
                webBrowser.DocumentText = html;
            }
            catch
            {
                webBrowser.Visible = false;
                label1.Visible = true;
            }
        }
    }
}
