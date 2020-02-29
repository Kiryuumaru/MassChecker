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
    public partial class Processor : Form
    {
        private string file = "";

        public Processor(string filename)
        {
            file = filename;

            InitializeComponent();
            StartCamera();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ML.OMR.Stop();
            base.OnClosing(e);
        }

        private void Log(string line)
        {
            line = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + " " + line;
            try
            {
                if (!string.IsNullOrEmpty(textBoxlog.Text)) textBoxlog.AppendText(Environment.NewLine);
                textBoxlog.AppendText(line);
            }
            catch
            {
                try
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        if (!string.IsNullOrEmpty(textBoxlog.Text)) textBoxlog.AppendText(Environment.NewLine);
                        textBoxlog.AppendText(line);
                    }));
                }
                catch { }
            }
        }

        private void StartCamera()
        {
            Task.Run(delegate
            {
                ML.OMR.Init();
                ML.OMR.Logger += log =>
                {
                    Log(log);
                };
                Invoke(new MethodInvoker(delegate
                {
                    ML.OMR.StartFile(imageBoxFrameGrabber, file);
                }));
            });
        }
    }
}
