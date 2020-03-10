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
    public partial class Main : Form
    {
        private bool checkingStarted = false;
        private string imageFilename = "";
        private AssessmentType assessmentType;

        public Main()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            TypeChooser typeChooser = new TypeChooser();
            typeChooser.ShowDialog();
            if (!typeChooser.HasChosen)
            {
                Close();
                return;
            }
            assessmentType = typeChooser.AssessmentType;
            switch (assessmentType)
            {
                case AssessmentType.Item50:
                    Text = "Mass Checker (50 items)";
                    break;
                case AssessmentType.Item60:
                    Text = "Mass Checker (60 items)";
                    break;
                case AssessmentType.Item70:
                    Text = "Mass Checker (70 items)";
                    break;
                case AssessmentType.Item80:
                    Text = "Mass Checker (80 items)";
                    break;
                case AssessmentType.Item90:
                    Text = "Mass Checker (90 items)";
                    break;
                case AssessmentType.Item100:
                    Text = "Mass Checker (100 items)";
                    break;
            }
            buttonStart.Enabled = false;
            Scanner.SetConnectionChangeHandler(connected =>
            {
                Invoke(new MethodInvoker(delegate
                {
                    buttonStart.Enabled = connected;
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
                imageFilename = imgDir;
                Log("Image fetched");

            });
            Scanner.Start();
            ML.Init(Log);
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

        private void NumericUpDownIterations_ValueChanged(object sender, EventArgs e)
        {
            int intVal = Convert.ToInt32(numericUpDownIterations.Value);
            if (numericUpDownIterations.Value != intVal) numericUpDownIterations.Value = intVal;
        }

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                Description = "Choose folder to output checker results"
            };
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxOutputDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ML.OMR.ProcessFile(assessmentType, imageBoxFrameGrabber1, Path.Combine(Extension.TempDir, "temp0001.jpg"));
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            if (checkingStarted)
            {
                checkingStarted = false;
                buttonStart.Enabled = false;
                Log("Iteration: Cancelling . . .");
            }
            else
            {
                if (!Directory.Exists(textBoxOutputDir.Text))
                {
                    MessageBox.Show("Output folder does not exist", "Invalid Output Folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    checkingStarted = true;
                    textBoxOutputDir.Enabled = false;
                    numericUpDownIterations.Enabled = false;
                    buttonBrowse.Enabled = false;
                    buttonEdit.Enabled = false;
                    buttonStart.Text = "Cancel";
                    Task.Run(async delegate
                    {
                        try
                        {
                            for (int i = 0; i < numericUpDownIterations.Value && checkingStarted; i++)
                            {
                                Log("Iteration: " + (i + 1).ToString());
                                Scanner.NextPaper();
                                await Task.Delay(1000);
                                Scanner.ScanPaper();
                                imageFilename = "";
                                while (string.IsNullOrEmpty(imageFilename)) { }
                                Log("Procesing image . . .");
                                ML.OMR.ProcessFile(assessmentType, imageBoxFrameGrabber1, imageFilename);
                                imageBoxFrameGrabber1.Image.Save(Path.Combine(textBoxOutputDir.Text, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".jpg"));
                                Log("Image processed");
                            }
                            Log("Iteration: Done");
                        }
                        catch
                        {
                            Log("Iteration: Error");
                        }
                        checkingStarted = false;
                        Invoke(new MethodInvoker(delegate
                        {
                            textBoxOutputDir.Enabled = true;
                            numericUpDownIterations.Enabled = true;
                            buttonBrowse.Enabled = true;
                            buttonEdit.Enabled = true;
                            buttonStart.Text = "Start Checking";
                        }));
                    });
                }
            }
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            switch (assessmentType)
            {
                case AssessmentType.Item50:
                    new AnswerKeyItem50().ShowDialog();
                    break;
                case AssessmentType.Item60:
                    new AnswerKeyItem60().ShowDialog();
                    break;
                case AssessmentType.Item70:
                    new AnswerKeyItem70().ShowDialog();
                    break;
                case AssessmentType.Item80:
                    new AnswerKeyItem80().ShowDialog();
                    break;
                case AssessmentType.Item90:
                    new AnswerKeyItem90().ShowDialog();
                    break;
                case AssessmentType.Item100:
                    new AnswerKeyItem100().ShowDialog();
                    break;
            }
        }
    }
}
