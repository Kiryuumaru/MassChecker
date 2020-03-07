using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MassChecker.Models;

namespace MassChecker.Controls
{
    public partial class KeyHolder : UserControl
    {
        public Key Key;

        public KeyHolder()
        {
            InitializeComponent();
        }

        public Key Get()
        {
            return Key;
        }

        public void Set(int num, Key key)
        {
            Key = key;
            labelNum.Text = num.ToString("00");
            switch (key)
            {
                case Key.A:
                    radioButtonA.Checked = true;
                    radioButtonB.Checked = false;
                    radioButtonC.Checked = false;
                    radioButtonD.Checked = false;
                    radioButtonE.Checked = false;
                    break;
                case Key.B:
                    radioButtonA.Checked = false;
                    radioButtonB.Checked = true;
                    radioButtonC.Checked = false;
                    radioButtonD.Checked = false;
                    radioButtonE.Checked = false;
                    break;
                case Key.C:
                    radioButtonA.Checked = false;
                    radioButtonB.Checked = false;
                    radioButtonC.Checked = true;
                    radioButtonD.Checked = false;
                    radioButtonE.Checked = false;
                    break;
                case Key.D:
                    radioButtonA.Checked = false;
                    radioButtonB.Checked = false;
                    radioButtonC.Checked = false;
                    radioButtonD.Checked = true;
                    radioButtonE.Checked = false;
                    break;
                case Key.E:
                    radioButtonA.Checked = false;
                    radioButtonB.Checked = false;
                    radioButtonC.Checked = false;
                    radioButtonD.Checked = false;
                    radioButtonE.Checked = true;
                    break;
            }
        }

        private void RadioButtonA_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonA.Checked)
            {
                Key = Key.A;
                radioButtonB.Checked = false;
                radioButtonC.Checked = false;
                radioButtonD.Checked = false;
                radioButtonE.Checked = false;
            }
        }

        private void RadioButtonB_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonB.Checked)
            {
                Key = Key.B;
                radioButtonA.Checked = false;
                radioButtonC.Checked = false;
                radioButtonD.Checked = false;
                radioButtonE.Checked = false;
            }
        }

        private void RadioButtonC_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonC.Checked)
            {
                Key = Key.C;
                radioButtonB.Checked = false;
                radioButtonA.Checked = false;
                radioButtonD.Checked = false;
                radioButtonE.Checked = false;
            }
        }

        private void RadioButtonD_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonD.Checked)
            {
                Key = Key.D;
                radioButtonB.Checked = false;
                radioButtonC.Checked = false;
                radioButtonA.Checked = false;
                radioButtonE.Checked = false;
            }
        }

        private void RadioButtonE_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonE.Checked)
            {
                Key = Key.E;
                radioButtonB.Checked = false;
                radioButtonC.Checked = false;
                radioButtonD.Checked = false;
                radioButtonA.Checked = false;
            }
        }
    }
}
