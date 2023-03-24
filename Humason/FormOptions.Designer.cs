namespace Humason
{
    partial class FormOptions
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
            this.SaveButton = new System.Windows.Forms.Button();
            this.RotatorCheckBox = new System.Windows.Forms.CheckBox();
            this.WeatherCheckBox = new System.Windows.Forms.CheckBox();
            this.WeatherFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.DomeAddOnCheckBox = new System.Windows.Forms.CheckBox();
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
            this.NoFilterWheelCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.ForeColor = System.Drawing.Color.Black;
            this.SaveButton.Location = new System.Drawing.Point(116, 498);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Close";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // RotatorCheckBox
            // 
            this.RotatorCheckBox.AutoSize = true;
            this.RotatorCheckBox.ForeColor = System.Drawing.Color.White;
            this.RotatorCheckBox.Location = new System.Drawing.Point(12, 17);
            this.RotatorCheckBox.Name = "RotatorCheckBox";
            this.RotatorCheckBox.Size = new System.Drawing.Size(83, 17);
            this.RotatorCheckBox.TabIndex = 2;
            this.RotatorCheckBox.Text = "Has Rotator";
            this.RotatorCheckBox.UseVisualStyleBackColor = true;
            this.RotatorCheckBox.CheckedChanged += new System.EventHandler(this.Rotator_CheckedChanged);
            // 
            // WeatherCheckBox
            // 
            this.WeatherCheckBox.AutoSize = true;
            this.WeatherCheckBox.ForeColor = System.Drawing.SystemColors.Control;
            this.WeatherCheckBox.Location = new System.Drawing.Point(12, 40);
            this.WeatherCheckBox.Name = "WeatherCheckBox";
            this.WeatherCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.WeatherCheckBox.Size = new System.Drawing.Size(127, 17);
            this.WeatherCheckBox.TabIndex = 73;
            this.WeatherCheckBox.Text = "Has Weather Monitor";
            this.WeatherCheckBox.UseVisualStyleBackColor = true;
            this.WeatherCheckBox.CheckedChanged += new System.EventHandler(this.WeatherCheck_CheckedChanged);
            // 
            // WeatherFileDialog
            // 
            this.WeatherFileDialog.FileName = "CCDAP";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            this.folderBrowserDialog1.SelectedPath = "C:\\Users\\rick-\\Documents";
            // 
            // DomeAddOnCheckBox
            // 
            this.DomeAddOnCheckBox.AutoSize = true;
            this.DomeAddOnCheckBox.ForeColor = System.Drawing.SystemColors.Control;
            this.DomeAddOnCheckBox.Location = new System.Drawing.Point(12, 63);
            this.DomeAddOnCheckBox.Name = "DomeAddOnCheckBox";
            this.DomeAddOnCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DomeAddOnCheckBox.Size = new System.Drawing.Size(115, 17);
            this.DomeAddOnCheckBox.TabIndex = 78;
            this.DomeAddOnCheckBox.Text = "Has Dome Add-On";
            this.DomeAddOnCheckBox.UseVisualStyleBackColor = true;
            this.DomeAddOnCheckBox.CheckedChanged += new System.EventHandler(this.DomeAddOnCheckBox_CheckedChanged);
            // 
            // ImageReductionComboBox
            // 
            this.ImageReductionComboBox.FormattingEnabled = true;
            this.ImageReductionComboBox.Items.AddRange(new object[] {
            "None",
            "AutoDark",
            "Full"});
            this.ImageReductionComboBox.Location = new System.Drawing.Point(193, 19);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 123);
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
            this.FocusReductionComboBox.Location = new System.Drawing.Point(193, 43);
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
            this.GuiderReductionComboBox.Location = new System.Drawing.Point(193, 67);
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
            this.CLSReductionComboBox.Location = new System.Drawing.Point(193, 92);
            this.CLSReductionComboBox.Name = "CLSReductionComboBox";
            this.CLSReductionComboBox.Size = new System.Drawing.Size(74, 21);
            this.CLSReductionComboBox.TabIndex = 108;
            this.CLSReductionComboBox.SelectedIndexChanged += new System.EventHandler(this.CLSReductionComboBox_SelectedIndexChanged);
            // 
            // UseTSXAutoSaveCheckbox
            // 
            this.UseTSXAutoSaveCheckbox.AutoSize = true;
            this.UseTSXAutoSaveCheckbox.ForeColor = System.Drawing.SystemColors.Control;
            this.UseTSXAutoSaveCheckbox.Location = new System.Drawing.Point(12, 262);
            this.UseTSXAutoSaveCheckbox.Name = "UseTSXAutoSaveCheckbox";
            this.UseTSXAutoSaveCheckbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.UseTSXAutoSaveCheckbox.Size = new System.Drawing.Size(135, 17);
            this.UseTSXAutoSaveCheckbox.TabIndex = 109;
            this.UseTSXAutoSaveCheckbox.Text = "Use TheSky AutoSave";
            this.UseTSXAutoSaveCheckbox.UseVisualStyleBackColor = true;
            this.UseTSXAutoSaveCheckbox.CheckedChanged += new System.EventHandler(this.UseTSXAutoSaveCheckbox_CheckedChanged);
            // 
            // NoFilterWheelCheckBox
            // 
            this.NoFilterWheelCheckBox.AutoSize = true;
            this.NoFilterWheelCheckBox.ForeColor = System.Drawing.SystemColors.Control;
            this.NoFilterWheelCheckBox.Location = new System.Drawing.Point(12, 86);
            this.NoFilterWheelCheckBox.Name = "NoFilterWheelCheckBox";
            this.NoFilterWheelCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NoFilterWheelCheckBox.Size = new System.Drawing.Size(99, 17);
            this.NoFilterWheelCheckBox.TabIndex = 111;
            this.NoFilterWheelCheckBox.Text = "No Filter Wheel";
            this.NoFilterWheelCheckBox.UseVisualStyleBackColor = true;
            this.NoFilterWheelCheckBox.CheckedChanged += new System.EventHandler(this.NoFilterWheelCheckBox_CheckedChanged);
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(297, 544);
            this.Controls.Add(this.NoFilterWheelCheckBox);
            this.Controls.Add(this.UseTSXAutoSaveCheckbox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DomeAddOnCheckBox);
            this.Controls.Add(this.WeatherCheckBox);
            this.Controls.Add(this.RotatorCheckBox);
            this.Controls.Add(this.SaveButton);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "FormOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.CheckBox RotatorCheckBox;
        internal System.Windows.Forms.CheckBox WeatherCheckBox;
        private System.Windows.Forms.OpenFileDialog WeatherFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        internal System.Windows.Forms.CheckBox DomeAddOnCheckBox;
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
        internal System.Windows.Forms.CheckBox NoFilterWheelCheckBox;
    }
}