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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.StageSystemFilePathBox = new System.Windows.Forms.TextBox();
            this.StagingBrowseButton = new System.Windows.Forms.Button();
            this.StagingEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.StartUpBrowseButton = new System.Windows.Forms.Button();
            this.StartUpFilePathBox = new System.Windows.Forms.TextBox();
            this.StartupEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ShutDownBrowseButton = new System.Windows.Forms.Button();
            this.ShutDownFilePathBox = new System.Windows.Forms.TextBox();
            this.ShutdownEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MinimumAltitudeBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.StartUpFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ShutDownFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.StageSystemFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.OTAButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.EnableMeridianFlipBox = new System.Windows.Forms.CheckBox();
            this.SessionParkCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimumAltitudeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.SkyBlue;
            this.groupBox5.Controls.Add(this.StageSystemFilePathBox);
            this.groupBox5.Controls.Add(this.StagingBrowseButton);
            this.groupBox5.Controls.Add(this.StagingEnabledCheckBox);
            this.groupBox5.ForeColor = System.Drawing.Color.Black;
            this.groupBox5.Location = new System.Drawing.Point(6, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(238, 47);
            this.groupBox5.TabIndex = 69;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "At Session Start Up Time";
            // 
            // StageSystemFilePathBox
            // 
            this.StageSystemFilePathBox.AccessibleDescription = "";
            this.StageSystemFilePathBox.Location = new System.Drawing.Point(61, 16);
            this.StageSystemFilePathBox.Name = "StageSystemFilePathBox";
            this.StageSystemFilePathBox.Size = new System.Drawing.Size(115, 20);
            this.StageSystemFilePathBox.TabIndex = 6;
            // 
            // StagingBrowseButton
            // 
            this.StagingBrowseButton.ForeColor = System.Drawing.Color.Black;
            this.StagingBrowseButton.Location = new System.Drawing.Point(181, 14);
            this.StagingBrowseButton.Name = "StagingBrowseButton";
            this.StagingBrowseButton.Size = new System.Drawing.Size(51, 23);
            this.StagingBrowseButton.TabIndex = 5;
            this.StagingBrowseButton.Text = "Browse";
            this.StagingBrowseButton.UseVisualStyleBackColor = true;
            this.StagingBrowseButton.Click += new System.EventHandler(this.StagingBrowseButton_Click);
            // 
            // StagingEnabledCheckBox
            // 
            this.StagingEnabledCheckBox.Location = new System.Drawing.Point(5, 14);
            this.StagingEnabledCheckBox.Name = "StagingEnabledCheckBox";
            this.StagingEnabledCheckBox.Size = new System.Drawing.Size(104, 24);
            this.StagingEnabledCheckBox.TabIndex = 7;
            this.StagingEnabledCheckBox.Text = "Enable";
            this.StagingEnabledCheckBox.CheckedChanged += new System.EventHandler(this.StagingEnabledCheckBox_CheckedChanged_1);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.SkyBlue;
            this.groupBox3.Controls.Add(this.StartUpBrowseButton);
            this.groupBox3.Controls.Add(this.StartUpFilePathBox);
            this.groupBox3.Controls.Add(this.StartupEnabledCheckBox);
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(6, 83);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(238, 47);
            this.groupBox3.TabIndex = 70;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "At First Target Plan Starting Time";
            // 
            // StartUpBrowseButton
            // 
            this.StartUpBrowseButton.ForeColor = System.Drawing.Color.Black;
            this.StartUpBrowseButton.Location = new System.Drawing.Point(181, 13);
            this.StartUpBrowseButton.Name = "StartUpBrowseButton";
            this.StartUpBrowseButton.Size = new System.Drawing.Size(51, 23);
            this.StartUpBrowseButton.TabIndex = 4;
            this.StartUpBrowseButton.Text = "Browse";
            this.StartUpBrowseButton.UseVisualStyleBackColor = true;
            this.StartUpBrowseButton.Click += new System.EventHandler(this.StartUpBrowseButton_Click);
            // 
            // StartUpFilePathBox
            // 
            this.StartUpFilePathBox.Location = new System.Drawing.Point(61, 16);
            this.StartUpFilePathBox.Name = "StartUpFilePathBox";
            this.StartUpFilePathBox.Size = new System.Drawing.Size(115, 20);
            this.StartUpFilePathBox.TabIndex = 3;
            // 
            // StartupEnabledCheckBox
            // 
            this.StartupEnabledCheckBox.Location = new System.Drawing.Point(6, 14);
            this.StartupEnabledCheckBox.Name = "StartupEnabledCheckBox";
            this.StartupEnabledCheckBox.Size = new System.Drawing.Size(104, 24);
            this.StartupEnabledCheckBox.TabIndex = 5;
            this.StartupEnabledCheckBox.Text = "Enable";
            this.StartupEnabledCheckBox.CheckedChanged += new System.EventHandler(this.StartupEnabledCheckBox_CheckedChanged_1);
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.SkyBlue;
            this.groupBox6.Controls.Add(this.ShutDownBrowseButton);
            this.groupBox6.Controls.Add(this.ShutDownFilePathBox);
            this.groupBox6.Controls.Add(this.ShutdownEnabledCheckBox);
            this.groupBox6.ForeColor = System.Drawing.Color.Black;
            this.groupBox6.Location = new System.Drawing.Point(6, 146);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(238, 47);
            this.groupBox6.TabIndex = 71;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "At Session End or Dawn";
            // 
            // ShutDownBrowseButton
            // 
            this.ShutDownBrowseButton.ForeColor = System.Drawing.Color.Black;
            this.ShutDownBrowseButton.Location = new System.Drawing.Point(182, 16);
            this.ShutDownBrowseButton.Name = "ShutDownBrowseButton";
            this.ShutDownBrowseButton.Size = new System.Drawing.Size(50, 23);
            this.ShutDownBrowseButton.TabIndex = 5;
            this.ShutDownBrowseButton.Text = "Browse";
            this.ShutDownBrowseButton.UseVisualStyleBackColor = true;
            this.ShutDownBrowseButton.Click += new System.EventHandler(this.ShutDownBrowseButton_Click);
            // 
            // ShutDownFilePathBox
            // 
            this.ShutDownFilePathBox.Location = new System.Drawing.Point(61, 17);
            this.ShutDownFilePathBox.Name = "ShutDownFilePathBox";
            this.ShutDownFilePathBox.Size = new System.Drawing.Size(115, 20);
            this.ShutDownFilePathBox.TabIndex = 4;
            // 
            // ShutdownEnabledCheckBox
            // 
            this.ShutdownEnabledCheckBox.Location = new System.Drawing.Point(4, 15);
            this.ShutdownEnabledCheckBox.Name = "ShutdownEnabledCheckBox";
            this.ShutdownEnabledCheckBox.Size = new System.Drawing.Size(104, 24);
            this.ShutdownEnabledCheckBox.TabIndex = 6;
            this.ShutdownEnabledCheckBox.Text = "Enable";
            this.ShutdownEnabledCheckBox.CheckedChanged += new System.EventHandler(this.ShutdownEnabledCheckBox_CheckedChanged_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 207);
            this.groupBox1.TabIndex = 74;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auto Run";
            // 
            // MinimumAltitudeBox
            // 
            this.MinimumAltitudeBox.Location = new System.Drawing.Point(107, 236);
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
            this.label1.Location = new System.Drawing.Point(15, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 76;
            this.label1.Text = "Minimum Altitude";
            // 
            // OTAButton
            // 
            this.OTAButton.ForeColor = System.Drawing.Color.Black;
            this.OTAButton.Location = new System.Drawing.Point(19, 284);
            this.OTAButton.Name = "OTAButton";
            this.OTAButton.Size = new System.Drawing.Size(111, 23);
            this.OTAButton.TabIndex = 77;
            this.OTAButton.Text = "Check Side of Pier";
            this.OTAButton.UseVisualStyleBackColor = true;
            this.OTAButton.Click += new System.EventHandler(this.OTAButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 319);
            this.label2.MaximumSize = new System.Drawing.Size(250, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 26);
            this.label2.TabIndex = 78;
            this.label2.Text = "If mount does not report Side Of Pier correctly, then do not enable Meridian Flip" +
    ".\r\n";
            // 
            // EnableMeridianFlipBox
            // 
            this.EnableMeridianFlipBox.AutoSize = true;
            this.EnableMeridianFlipBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.EnableMeridianFlipBox.Location = new System.Drawing.Point(141, 288);
            this.EnableMeridianFlipBox.Name = "EnableMeridianFlipBox";
            this.EnableMeridianFlipBox.Size = new System.Drawing.Size(121, 17);
            this.EnableMeridianFlipBox.TabIndex = 79;
            this.EnableMeridianFlipBox.Text = "Enable Meridian Flip";
            this.EnableMeridianFlipBox.UseVisualStyleBackColor = true;
            this.EnableMeridianFlipBox.CheckedChanged += new System.EventHandler(this.EnableMeridianFlipBox_CheckedChanged);
            // 
            // SessionParkCheckBox
            // 
            this.SessionParkCheckBox.AutoSize = true;
            this.SessionParkCheckBox.Location = new System.Drawing.Point(18, 365);
            this.SessionParkCheckBox.Name = "SessionParkCheckBox";
            this.SessionParkCheckBox.Size = new System.Drawing.Size(170, 17);
            this.SessionParkCheckBox.TabIndex = 80;
            this.SessionParkCheckBox.Text = "Park On Session End or Dawn";
            this.SessionParkCheckBox.UseVisualStyleBackColor = true;
            this.SessionParkCheckBox.CheckedChanged += new System.EventHandler(this.SessionParkCheckBox_CheckedChanged);
            // 
            // FormSessionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(278, 445);
            this.Controls.Add(this.SessionParkCheckBox);
            this.Controls.Add(this.EnableMeridianFlipBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OTAButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MinimumAltitudeBox);
            this.Controls.Add(this.groupBox1);
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
            ((System.ComponentModel.ISupportInitialize)(this.MinimumAltitudeBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox StagingEnabledCheckBox;
        private System.Windows.Forms.CheckBox StartupEnabledCheckBox;
        private System.Windows.Forms.CheckBox ShutdownEnabledCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown MinimumAltitudeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog StartUpFileDialog;
        private System.Windows.Forms.OpenFileDialog ShutDownFileDialog;
        private System.Windows.Forms.OpenFileDialog StageSystemFileDialog;
        private System.Windows.Forms.Button StagingBrowseButton;
        private System.Windows.Forms.TextBox StageSystemFilePathBox;
        private System.Windows.Forms.TextBox StartUpFilePathBox;
        private System.Windows.Forms.Button StartUpBrowseButton;
        private System.Windows.Forms.TextBox ShutDownFilePathBox;
        private System.Windows.Forms.Button ShutDownBrowseButton;
        private System.Windows.Forms.Button OTAButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox EnableMeridianFlipBox;
        private System.Windows.Forms.CheckBox SessionParkCheckBox;
    }
}