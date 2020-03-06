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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            buttonNext.Enabled = false;
            buttonScan.Enabled = false;
            Scanner.SetConnectionChangeHandler(connected =>
            {
                Invoke(new MethodInvoker(delegate
                {
                    buttonNext.Enabled = connected;
                    buttonScan.Enabled = connected;
                    if (!connected)
                    {
                        if (MessageBox.Show("Scanner was disconnected", "Disconnected", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information) == DialogResult.Retry)
                        {
                            Scanner.Start();
                        }
                    }
                }));
            });
            Scanner.SetImageScanHandler(imgDir =>
            {
                Invoke(new MethodInvoker(delegate
                {
                    pictureBox1.Image = Image.FromFile(imgDir);
                    pictureBox1.Invalidate();
                    //MessageBox.Show("Image received.", "Image received", MessageBoxButtons.OK);
                }));
                ML.OMR.ProcessFile(imageBoxFrameGrabber1, imgDir);

            });
            Scanner.Start();
            ML.Init(Log);
        }

        private void Log(string line)
        {
            //line = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + " " + line;
            //try
            //{
            //    if (!string.IsNullOrEmpty(textBoxlog.Text)) textBoxlog.AppendText(Environment.NewLine);
            //    textBoxlog.AppendText(line);
            //}
            //catch
            //{
            //    try
            //    {
            //        Invoke(new MethodInvoker(delegate
            //        {
            //            if (!string.IsNullOrEmpty(textBoxlog.Text)) textBoxlog.AppendText(Environment.NewLine);
            //            textBoxlog.AppendText(line);
            //        }));
            //    }
            //    catch { }
            //}
        }

        private bool GetFilename(out string filename, DragEventArgs e)
        {
            bool ret = false;
            filename = string.Empty;
            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                if (e.Data.GetData("FileDrop") is Array data)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is string))
                    {
                        filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
                        if (ext == ".mp4")
                        {
                            ret = true;
                        }
                    }
                }
            }
            return ret;
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            Scanner.NextPaper();
        }

        private void ButtonScan_Click(object sender, EventArgs e)
        {
            Scanner.ScanPaper();
        }
    }
}
