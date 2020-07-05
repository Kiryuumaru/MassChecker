using MassChecker.Models;
using MassChecker.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MassChecker.Forms
{
    public partial class Connect : Form
    {
        public string IPAddress { get; private set; }
        public bool Auto { get; private set; } = true;

        public Connect()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            IPAddress = waterMarkTextBoxIp.Text;
            if (string.IsNullOrEmpty(IPAddress)) Auto = true;
            base.OnClosed(e);
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Auto = true;
                waterMarkTextBoxIp.Enabled = false;
            }
            else
            {
                Auto = false;
                waterMarkTextBoxIp.Enabled = true;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
