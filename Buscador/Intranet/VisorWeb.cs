using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Intranet
{
    public partial class VisorWeb : Form
    {
        public string url;
        public VisorWeb(string urlE)
        {
            url = urlE;

            InitializeComponent();
        }

        private void VisorWeb_Load(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri(url);
        }
    }
}
