namespace MassChecker.Forms
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageBoxFrameGrabber1 = new Emgu.CV.UI.ImageBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxOutputDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.numericUpDownIterations = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxlog = new System.Windows.Forms.TextBox();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonBrowse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterations)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBoxFrameGrabber1
            // 
            this.imageBoxFrameGrabber1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxFrameGrabber1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imageBoxFrameGrabber1.Location = new System.Drawing.Point(12, 12);
            this.imageBoxFrameGrabber1.Name = "imageBoxFrameGrabber1";
            this.imageBoxFrameGrabber1.Size = new System.Drawing.Size(341, 437);
            this.imageBoxFrameGrabber1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBoxFrameGrabber1.TabIndex = 13;
            this.imageBoxFrameGrabber1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(697, 355);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Scan";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBoxOutputDir
            // 
            this.textBoxOutputDir.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOutputDir.Location = new System.Drawing.Point(449, 384);
            this.textBoxOutputDir.Name = "textBoxOutputDir";
            this.textBoxOutputDir.Size = new System.Drawing.Size(269, 29);
            this.textBoxOutputDir.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(377, 387);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 21);
            this.label1.TabIndex = 16;
            this.label1.Text = "Output:";
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.Location = new System.Drawing.Point(636, 419);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(136, 30);
            this.buttonStart.TabIndex = 17;
            this.buttonStart.Text = "Start Checking";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // numericUpDownIterations
            // 
            this.numericUpDownIterations.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownIterations.Location = new System.Drawing.Point(449, 421);
            this.numericUpDownIterations.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownIterations.Name = "numericUpDownIterations";
            this.numericUpDownIterations.Size = new System.Drawing.Size(91, 29);
            this.numericUpDownIterations.TabIndex = 18;
            this.numericUpDownIterations.ThousandsSeparator = true;
            this.numericUpDownIterations.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownIterations.ValueChanged += new System.EventHandler(this.NumericUpDownIterations_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(359, 423);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 21);
            this.label2.TabIndex = 19;
            this.label2.Text = "Iterations:";
            // 
            // textBoxlog
            // 
            this.textBoxlog.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxlog.Location = new System.Drawing.Point(359, 12);
            this.textBoxlog.Multiline = true;
            this.textBoxlog.Name = "textBoxlog";
            this.textBoxlog.ReadOnly = true;
            this.textBoxlog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxlog.Size = new System.Drawing.Size(413, 366);
            this.textBoxlog.TabIndex = 20;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEdit.Location = new System.Drawing.Point(549, 419);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(81, 30);
            this.buttonEdit.TabIndex = 21;
            this.buttonEdit.Text = "Edit key";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrowse.Location = new System.Drawing.Point(724, 384);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(48, 30);
            this.buttonBrowse.TabIndex = 22;
            this.buttonBrowse.Text = ". . .";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownIterations);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxOutputDir);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.imageBoxFrameGrabber1);
            this.Controls.Add(this.textBoxlog);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Mass Checker";
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Emgu.CV.UI.ImageBox imageBoxFrameGrabber1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxOutputDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.NumericUpDown numericUpDownIterations;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxlog;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonBrowse;
    }
}