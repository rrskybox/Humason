namespace Humason
{
    partial class FormSessionControl
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
            this.AutoRunCheck = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.StagingEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.StartupEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ShutdownEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.OTAButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MinimumAltitudeBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimumAltitudeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AutoRunCheck
            // 
            this.AutoRunCheck.AutoSize = true;
            this.AutoRunCheck.ForeColor = System.Drawing.SystemColors.Control;
            this.AutoRunCheck.Location = new System.Drawing.Point(22, 19);
            this.AutoRunCheck.Name = "AutoRunCheck";
            this.AutoRunCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AutoRunCheck.Size = new System.Drawing.Size(93, 17);
            this.AutoRunCheck.TabIndex = 68;
            this.AutoRunCheck.Text = "Use Auto Run";
            this.AutoRunCheck.UseVisualStyleBackColor = true;
            this.AutoRunCheck.CheckedChanged += new System.EventHandler(this.AutoRunCheck_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.SkyBlue;
            this.groupBox5.Controls.Add(this.StagingEnabledCheckBox);
            this.groupBox5.ForeColor = System.Drawing.Color.Black;
            this.groupBox5.Location = new System.Drawing.Point(51, 42);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(193, 47);
            this.groupBox5.TabIndex = 69;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "At Session Start Up Time";
            // 
            // StagingEnabledCheckBox
            // 
            this.StagingEnabledCheckBox.AutoSize = true;
            this.StagingEnabledCheckBox.Location = new System.Drawing.Point(16, 22);
            this.StagingEnabledCheckBox.Name = "StagingEnabledCheckBox";
            this.StagingEnabledCheckBox.Size = new System.Drawing.Size(113, 17);
            this.StagingEnabledCheckBox.TabIndex = 1;
            this.StagingEnabledCheckBox.Text = "Enable Script/App";
            this.StagingEnabledCheckBox.UseVisualStyleBackColor = true;
            this.StagingEnabledCheckBox.CheckedChanged += new System.EventHandler(this.StagingEnabledCheckBox_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.SkyBlue;
            this.groupBox3.Controls.Add(this.StartupEnabledCheckBox);
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(51, 105);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(193, 47);
            this.groupBox3.TabIndex = 70;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "At First Target Plan Starting Time";
            // 
            // StartupEnabledCheckBox
            // 
            this.StartupEnabledCheckBox.AutoSize = true;
            this.StartupEnabledCheckBox.Location = new System.Drawing.Point(16, 22);
            this.StartupEnabledCheckBox.Name = "StartupEnabledCheckBox";
            this.StartupEnabledCheckBox.Size = new System.Drawing.Size(113, 17);
            this.StartupEnabledCheckBox.TabIndex = 2;
            this.StartupEnabledCheckBox.Text = "Enable Script/App";
            this.StartupEnabledCheckBox.UseVisualStyleBackColor = true;
            this.StartupEnabledCheckBox.CheckedChanged += new System.EventHandler(this.StartupEnabledCheckBox_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.SkyBlue;
            this.groupBox6.Controls.Add(this.ShutdownEnabledCheckBox);
            this.groupBox6.ForeColor = System.Drawing.Color.Black;
            this.groupBox6.Location = new System.Drawing.Point(51, 168);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(193, 47);
            this.groupBox6.TabIndex = 71;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "At Session End or Dawn";
            // 
            // ShutdownEnabledCheckBox
            // 
            this.ShutdownEnabledCheckBox.AutoSize = true;
            this.ShutdownEnabledCheckBox.Location = new System.Drawing.Point(16, 22);
            this.ShutdownEnabledCheckBox.Name = "ShutdownEnabledCheckBox";
            this.ShutdownEnabledCheckBox.Size = new System.Drawing.Size(113, 17);
            this.ShutdownEnabledCheckBox.TabIndex = 3;
            this.ShutdownEnabledCheckBox.Text = "Enable Script/App";
            this.ShutdownEnabledCheckBox.UseVisualStyleBackColor = true;
            this.ShutdownEnabledCheckBox.CheckedChanged += new System.EventHandler(this.ShutdownEnabledCheckBox_CheckedChanged);
            // 
            // OTAButton
            // 
            this.OTAButton.ForeColor = System.Drawing.Color.Black;
            this.OTAButton.Location = new System.Drawing.Point(80, 369);
            this.OTAButton.Name = "OTAButton";
            this.OTAButton.Size = new System.Drawing.Size(125, 21);
            this.OTAButton.TabIndex = 73;
            this.OTAButton.Text = "Check Side Of Pier";
            this.OTAButton.UseVisualStyleBackColor = true;
            this.OTAButton.Click += new System.EventHandler(this.OTAButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.AutoRunCheck);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 239);
            this.groupBox1.TabIndex = 74;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auto Run";
            // 
            // MinimumAltitudeBox
            // 
            this.MinimumAltitudeBox.Location = new System.Drawing.Point(102, 257);
            this.MinimumAltitudeBox.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.MinimumAltitudeBox.Name = "MinimumAltitudeBox";
            this.MinimumAltitudeBox.Size = new System.Drawing.Size(41, 20);
            this.MinimumAltitudeBox.TabIndex = 75;
            this.MinimumAltitudeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MinimumAltitudeBox.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.MinimumAltitudeBox.ValueChanged += new System.EventHandler(this.MinAltitudeBox_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 259);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 76;
            this.label1.Text = "Minimum Altitude";
            // 
            // FormSessionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(278, 402);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MinimumAltitudeBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.OTAButton);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSessionControl";
            this.ShowIcon = false;
            this.Text = "Session Control";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimumAltitudeBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox AutoRunCheck;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button OTAButton;
        private System.Windows.Forms.CheckBox StagingEnabledCheckBox;
        private System.Windows.Forms.CheckBox StartupEnabledCheckBox;
        private System.Windows.Forms.CheckBox ShutdownEnabledCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown MinimumAltitudeBox;
        private System.Windows.Forms.Label label1;
    }
}