using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace printServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Process.GetCurrentProcess().Kill();
        }

        public void ShowNotification(string msg, string title)
        {
            notifyIcon1.ShowBalloonTip(100, title, msg, ToolTipIcon.Info);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
