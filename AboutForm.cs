using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace RemoteDesktopShutdown
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            Font = SystemInformation.MenuFont;

            versionLabel.Text = string.Format(versionLabel.Text, Application.ProductVersion);
            nameLabel.Font = new Font(Font.FontFamily, 15);
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://rdpshutdown.codeplex.com/");
        }
    }
}