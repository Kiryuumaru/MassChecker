namespace MassChecker.Forms
{
    partial class TypeChooser
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
            this.button50 = new System.Windows.Forms.Button();
            this.button60 = new System.Windows.Forms.Button();
            this.button70 = new System.Windows.Forms.Button();
            this.button80 = new System.Windows.Forms.Button();
            this.button90 = new System.Windows.Forms.Button();
            this.button100 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button50
            // 
            this.button50.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button50.Location = new System.Drawing.Point(12, 12);
            this.button50.Name = "button50";
            this.button50.Size = new System.Drawing.Size(100, 50);
            this.button50.TabIndex = 0;
            this.button50.Text = "50 Items";
            this.button50.UseVisualStyleBackColor = true;
            this.button50.Click += new System.EventHandler(this.Button50_Click);
            // 
            // button60
            // 
            this.button60.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button60.Location = new System.Drawing.Point(118, 12);
            this.button60.Name = "button60";
            this.button60.Size = new System.Drawing.Size(100, 50);
            this.button60.TabIndex = 1;
            this.button60.Text = "60 Items";
            this.button60.UseVisualStyleBackColor = true;
            this.button60.Click += new System.EventHandler(this.Button60_Click);
            // 
            // button70
            // 
            this.button70.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button70.Location = new System.Drawing.Point(224, 12);
            this.button70.Name = "button70";
            this.button70.Size = new System.Drawing.Size(100, 50);
            this.button70.TabIndex = 2;
            this.button70.Text = "70 Items";
            this.button70.UseVisualStyleBackColor = true;
            this.button70.Click += new System.EventHandler(this.Button70_Click);
            // 
            // button80
            // 
            this.button80.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button80.Location = new System.Drawing.Point(12, 68);
            this.button80.Name = "button80";
            this.button80.Size = new System.Drawing.Size(100, 50);
            this.button80.TabIndex = 3;
            this.button80.Text = "80 Items";
            this.button80.UseVisualStyleBackColor = true;
            this.button80.Click += new System.EventHandler(this.Button80_Click);
            // 
            // button90
            // 
            this.button90.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button90.Location = new System.Drawing.Point(118, 68);
            this.button90.Name = "button90";
            this.button90.Size = new System.Drawing.Size(100, 50);
            this.button90.TabIndex = 4;
            this.button90.Text = "90 Items";
            this.button90.UseVisualStyleBackColor = true;
            this.button90.Click += new System.EventHandler(this.Button90_Click);
            // 
            // button100
            // 
            this.button100.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button100.Location = new System.Drawing.Point(224, 68);
            this.button100.Name = "button100";
            this.button100.Size = new System.Drawing.Size(100, 50);
            this.button100.TabIndex = 5;
            this.button100.Text = "100 Items";
            this.button100.UseVisualStyleBackColor = true;
            this.button100.Click += new System.EventHandler(this.Button100_Click);
            // 
            // TypeChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 131);
            this.Controls.Add(this.button100);
            this.Controls.Add(this.button90);
            this.Controls.Add(this.button80);
            this.Controls.Add(this.button70);
            this.Controls.Add(this.button60);
            this.Controls.Add(this.button50);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TypeChooser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose Item Type";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button50;
        private System.Windows.Forms.Button button60;
        private System.Windows.Forms.Button button70;
        private System.Windows.Forms.Button button80;
        private System.Windows.Forms.Button button90;
        private System.Windows.Forms.Button button100;
    }
}