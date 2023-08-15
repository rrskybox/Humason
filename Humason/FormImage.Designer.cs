namespace Humason
{
    partial class FormImage
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ImageReductionComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.FocusReductionComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GuiderReductionComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CLSReductionComboBox = new System.Windows.Forms.ComboBox();
            this.UseTSXAutoSaveCheckbox = new System.Windows.Forms.CheckBox();
            this.CameraGroupBox = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CameraTemperatureSet = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.CameraGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraTemperatureSet)).BeginInit();
            this.SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            this.folderBrowserDialog1.SelectedPath = "C:\\Users\\rick-\\Documents";
            // 
            // ImageReductionComboBox
            // 
            this.ImageReductionComboBox.FormattingEnabled = true;
            this.ImageReductionComboBox.Items.AddRange(new object[] {
            "None",
            "AutoDark",
            "Full"});
            this.ImageReductionComboBox.Location = new System.Drawing.Point(140, 19);
            this.ImageReductionComboBox.Name = "ImageReductionComboBox";
            this.ImageReductionComboBox.Size = new System.Drawing.Size(74, 21);
            this.ImageReductionComboBox.TabIndex = 107;
            this.ImageReductionComboBox.SelectedIndexChanged += new System.EventHandler(this.ImageReductionComboBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.FocusReductionComboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.GuiderReductionComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CLSReductionComboBox);
            this.groupBox1.Controls.Add(this.ImageReductionComboBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 123);
            this.groupBox1.TabIndex = 108;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image Reduction";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 114;
            this.label4.Text = "Focus Reduction";
            // 
            // FocusReductionComboBox
            // 
            this.FocusReductionComboBox.FormattingEnabled = true;
            this.FocusReductionComboBox.Items.AddRange(new object[] {
            "None",
            "AutoDark",
            "Full"});
            this.FocusReductionComboBox.Location = new System.Drawing.Point(140, 43);
            this.FocusReductionComboBox.Name = "FocusReductionComboBox";
            this.FocusReductionComboBox.Size = new System.Drawing.Size(74, 21);
            this.FocusReductionComboBox.TabIndex = 113;
            this.FocusReductionComboBox.SelectedIndexChanged += new System.EventHandler(this.FocusReductionComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 112;
            this.label3.Text = "Guider Reduction";
            // 
            // GuiderReductionComboBox
            // 
            this.GuiderReductionComboBox.FormattingEnabled = true;
            this.GuiderReductionComboBox.Items.AddRange(new object[] {
            "None",
            "AutoDark",
            "Full"});
            this.GuiderReductionComboBox.Location = new System.Drawing.Point(140, 67);
            this.GuiderReductionComboBox.Name = "GuiderReductionComboBox";
            this.GuiderReductionComboBox.Size = new System.Drawing.Size(74, 21);
            this.GuiderReductionComboBox.TabIndex = 111;
            this.GuiderReductionComboBox.SelectedIndexChanged += new System.EventHandler(this.GuiderReductionComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 110;
            this.label2.Text = "CLS Reduction";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 109;
            this.label1.Text = "Image Reduction";
            // 
            // CLSReductionComboBox
            // 
            this.CLSReductionComboBox.FormattingEnabled = true;
            this.CLSReductionComboBox.Items.AddRange(new object[] {
            "None",
            "AutoDark",
            "Full"});
            this.CLSReductionComboBox.Location = new System.Drawing.Point(140, 92);
            this.CLSReductionComboBox.Name = "CLSReductionComboBox";
            this.CLSReductionComboBox.Size = new System.Drawing.Size(74, 21);
            this.CLSReductionComboBox.TabIndex = 108;
            this.CLSReductionComboBox.SelectedIndexChanged += new System.EventHandler(this.CLSReductionComboBox_SelectedIndexChanged);
            // 
            // UseTSXAutoSaveCheckbox
            // 
            this.UseTSXAutoSaveCheckbox.AutoSize = true;
            this.UseTSXAutoSaveCheckbox.ForeColor = System.Drawing.SystemColors.Control;
            this.UseTSXAutoSaveCheckbox.Location = new System.Drawing.Point(12, 12);
            this.UseTSXAutoSaveCheckbox.Name = "UseTSXAutoSaveCheckbox";
            this.UseTSXAutoSaveCheckbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.UseTSXAutoSaveCheckbox.Size = new System.Drawing.Size(135, 17);
            this.UseTSXAutoSaveCheckbox.TabIndex = 109;
            this.UseTSXAutoSaveCheckbox.Text = "Use TheSky AutoSave";
            this.UseTSXAutoSaveCheckbox.UseVisualStyleBackColor = true;
            this.UseTSXAutoSaveCheckbox.CheckedChanged += new System.EventHandler(this.UseTSXAutoSaveCheckbox_CheckedChanged);
            // 
            // CameraGroupBox
            // 
            this.CameraGroupBox.Controls.Add(this.label5);
            this.CameraGroupBox.Controls.Add(this.CameraTemperatureSet);
            this.CameraGroupBox.ForeColor = System.Drawing.Color.White;
            this.CameraGroupBox.Location = new System.Drawing.Point(12, 175);
            this.CameraGroupBox.Name = "CameraGroupBox";
            this.CameraGroupBox.Size = new System.Drawing.Size(238, 40);
            this.CameraGroupBox.TabIndex = 112;
            this.CameraGroupBox.TabStop = false;
            this.CameraGroupBox.Text = "Camera";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(103, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 93;
            this.label5.Text = "CCD Temp";
            // 
            // CameraTemperatureSet
            // 
            this.CameraTemperatureSet.Location = new System.Drawing.Point(168, 14);
            this.CameraTemperatureSet.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CameraTemperatureSet.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            -2147483648});
            this.CameraTemperatureSet.Name = "CameraTemperatureSet";
            this.CameraTemperatureSet.Size = new System.Drawing.Size(45, 20);
            this.CameraTemperatureSet.TabIndex = 92;
            this.CameraTemperatureSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CameraTemperatureSet.Value = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.CameraTemperatureSet.ValueChanged += new System.EventHandler(this.CameraTemperatureSet_ValueChanged_1);
            // 
            // FormImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(278, 445);
            this.Controls.Add(this.CameraGroupBox);
            this.Controls.Add(this.UseTSXAutoSaveCheckbox);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.CameraGroupBox.ResumeLayout(false);
            this.CameraGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraTemperatureSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox ImageReductionComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CLSReductionComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox GuiderReductionComboBox;
        internal System.Windows.Forms.CheckBox UseTSXAutoSaveCheckbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox FocusReductionComboBox;
        private System.Windows.Forms.GroupBox CameraGroupBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown CameraTemperatureSet;
    }
}