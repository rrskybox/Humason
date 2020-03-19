namespace Humason
{
    partial class FormFlats
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
            this.FlatsGroup = new System.Windows.Forms.GroupBox();
            this.ClearFlatsButton = new System.Windows.Forms.Button();
            this.FlatsRotationCheckBox = new System.Windows.Forms.CheckBox();
            this.RotatorPANum = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.MakeFlatsButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DuskRadioButton = new System.Windows.Forms.RadioButton();
            this.DawnRadioButton = new System.Windows.Forms.RadioButton();
            this.FlatManRadioButton = new System.Windows.Forms.RadioButton();
            this.FlatFlipCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.FlatsCountLabel = new System.Windows.Forms.Label();
            this.FlatsTargetADU = new System.Windows.Forms.NumericUpDown();
            this.TakeFlatsButton = new System.Windows.Forms.Button();
            this.FlatsRepetitionsBox = new System.Windows.Forms.NumericUpDown();
            this.FlatManOnButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FlatManManualSetupCheckbox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FlatManPortNum = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.FlatManExposureNum = new System.Windows.Forms.NumericUpDown();
            this.FlatManStageButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.FlatManEastCheckBox = new System.Windows.Forms.CheckBox();
            this.FlatManBrightnessNum = new System.Windows.Forms.NumericUpDown();
            this.FlatsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RotatorPANum)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlatsTargetADU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlatsRepetitionsBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlatManPortNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlatManExposureNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlatManBrightnessNum)).BeginInit();
            this.SuspendLayout();
            // 
            // FlatsGroup
            // 
            this.FlatsGroup.Controls.Add(this.ClearFlatsButton);
            this.FlatsGroup.Controls.Add(this.FlatsRotationCheckBox);
            this.FlatsGroup.Controls.Add(this.RotatorPANum);
            this.FlatsGroup.Controls.Add(this.label4);
            this.FlatsGroup.Controls.Add(this.MakeFlatsButton);
            this.FlatsGroup.Controls.Add(this.groupBox2);
            this.FlatsGroup.Controls.Add(this.FlatFlipCheckBox);
            this.FlatsGroup.Controls.Add(this.label5);
            this.FlatsGroup.Controls.Add(this.FlatsCountLabel);
            this.FlatsGroup.Controls.Add(this.FlatsTargetADU);
            this.FlatsGroup.Controls.Add(this.TakeFlatsButton);
            this.FlatsGroup.Controls.Add(this.FlatsRepetitionsBox);
            this.FlatsGroup.ForeColor = System.Drawing.Color.White;
            this.FlatsGroup.Location = new System.Drawing.Point(16, 23);
            this.FlatsGroup.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlatsGroup.Name = "FlatsGroup";
            this.FlatsGroup.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlatsGroup.Size = new System.Drawing.Size(516, 356);
            this.FlatsGroup.TabIndex = 32;
            this.FlatsGroup.TabStop = false;
            this.FlatsGroup.Text = "Flat Frame Recipe";
            // 
            // ClearFlatsButton
            // 
            this.ClearFlatsButton.ForeColor = System.Drawing.Color.Black;
            this.ClearFlatsButton.Location = new System.Drawing.Point(352, 279);
            this.ClearFlatsButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ClearFlatsButton.Name = "ClearFlatsButton";
            this.ClearFlatsButton.Size = new System.Drawing.Size(136, 48);
            this.ClearFlatsButton.TabIndex = 43;
            this.ClearFlatsButton.Text = "Dispose";
            this.ClearFlatsButton.UseVisualStyleBackColor = true;
            this.ClearFlatsButton.Click += new System.EventHandler(this.ClearFlatsButton_Click);
            // 
            // FlatsRotationCheckBox
            // 
            this.FlatsRotationCheckBox.AutoSize = true;
            this.FlatsRotationCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.FlatsRotationCheckBox.Location = new System.Drawing.Point(48, 169);
            this.FlatsRotationCheckBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlatsRotationCheckBox.Name = "FlatsRotationCheckBox";
            this.FlatsRotationCheckBox.Size = new System.Drawing.Size(107, 29);
            this.FlatsRotationCheckBox.TabIndex = 45;
            this.FlatsRotationCheckBox.Text = "Rotate";
            this.FlatsRotationCheckBox.UseVisualStyleBackColor = true;
            this.FlatsRotationCheckBox.CheckedChanged += new System.EventHandler(this.FlatsRotationCheckBox_CheckedChanged);
            // 
            // RotatorPANum
            // 
            this.RotatorPANum.Location = new System.Drawing.Point(234, 213);
            this.RotatorPANum.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.RotatorPANum.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.RotatorPANum.Name = "RotatorPANum";
            this.RotatorPANum.Size = new System.Drawing.Size(108, 31);
            this.RotatorPANum.TabIndex = 44;
            this.RotatorPANum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(52, 219);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 25);
            this.label4.TabIndex = 43;
            this.label4.Text = "Rotator PA (East)";
            // 
            // MakeFlatsButton
            // 
            this.MakeFlatsButton.ForeColor = System.Drawing.Color.Black;
            this.MakeFlatsButton.Location = new System.Drawing.Point(28, 279);
            this.MakeFlatsButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MakeFlatsButton.Name = "MakeFlatsButton";
            this.MakeFlatsButton.Size = new System.Drawing.Size(136, 48);
            this.MakeFlatsButton.TabIndex = 42;
            this.MakeFlatsButton.Text = "Compose";
            this.MakeFlatsButton.UseVisualStyleBackColor = true;
            this.MakeFlatsButton.Click += new System.EventHandler(this.MakeFlatsButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DuskRadioButton);
            this.groupBox2.Controls.Add(this.DawnRadioButton);
            this.groupBox2.Controls.Add(this.FlatManRadioButton);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(42, 33);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Size = new System.Drawing.Size(446, 69);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Source";
            // 
            // DuskRadioButton
            // 
            this.DuskRadioButton.AutoSize = true;
            this.DuskRadioButton.Location = new System.Drawing.Point(340, 25);
            this.DuskRadioButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.DuskRadioButton.Name = "DuskRadioButton";
            this.DuskRadioButton.Size = new System.Drawing.Size(92, 29);
            this.DuskRadioButton.TabIndex = 2;
            this.DuskRadioButton.Text = "Dusk";
            this.DuskRadioButton.UseVisualStyleBackColor = true;
            this.DuskRadioButton.CheckedChanged += new System.EventHandler(this.DuskRadioButton_CheckedChanged);
            // 
            // DawnRadioButton
            // 
            this.DawnRadioButton.AutoSize = true;
            this.DawnRadioButton.Location = new System.Drawing.Point(198, 25);
            this.DawnRadioButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.DawnRadioButton.Name = "DawnRadioButton";
            this.DawnRadioButton.Size = new System.Drawing.Size(97, 29);
            this.DawnRadioButton.TabIndex = 1;
            this.DawnRadioButton.Text = "Dawn";
            this.DawnRadioButton.UseVisualStyleBackColor = true;
            this.DawnRadioButton.CheckedChanged += new System.EventHandler(this.DawnRadioButton_CheckedChanged);
            // 
            // FlatManRadioButton
            // 
            this.FlatManRadioButton.AutoSize = true;
            this.FlatManRadioButton.Checked = true;
            this.FlatManRadioButton.Location = new System.Drawing.Point(22, 25);
            this.FlatManRadioButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlatManRadioButton.Name = "FlatManRadioButton";
            this.FlatManRadioButton.Size = new System.Drawing.Size(121, 29);
            this.FlatManRadioButton.TabIndex = 0;
            this.FlatManRadioButton.TabStop = true;
            this.FlatManRadioButton.Text = "FlatMan";
            this.FlatManRadioButton.UseVisualStyleBackColor = true;
            this.FlatManRadioButton.CheckedChanged += new System.EventHandler(this.FlatManCheckBox_CheckedChanged);
            // 
            // FlatFlipCheckBox
            // 
            this.FlatFlipCheckBox.AutoSize = true;
            this.FlatFlipCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.FlatFlipCheckBox.Location = new System.Drawing.Point(244, 169);
            this.FlatFlipCheckBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlatFlipCheckBox.Name = "FlatFlipCheckBox";
            this.FlatFlipCheckBox.Size = new System.Drawing.Size(79, 29);
            this.FlatFlipCheckBox.TabIndex = 39;
            this.FlatFlipCheckBox.Text = "Flip";
            this.FlatFlipCheckBox.UseVisualStyleBackColor = true;
            this.FlatFlipCheckBox.CheckedChanged += new System.EventHandler(this.FlatFlipCheckBox_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(238, 121);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 25);
            this.label5.TabIndex = 40;
            this.label5.Text = "Target ADU";
            // 
            // FlatsCountLabel
            // 
            this.FlatsCountLabel.AutoSize = true;
            this.FlatsCountLabel.Location = new System.Drawing.Point(48, 121);
            this.FlatsCountLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.FlatsCountLabel.Name = "FlatsCountLabel";
            this.FlatsCountLabel.Size = new System.Drawing.Size(62, 25);
            this.FlatsCountLabel.TabIndex = 29;
            this.FlatsCountLabel.Text = "Reps";
            // 
            // FlatsTargetADU
            // 
            this.FlatsTargetADU.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FlatsTargetADU.Location = new System.Drawing.Point(378, 117);
            this.FlatsTargetADU.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlatsTargetADU.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.FlatsTargetADU.Name = "FlatsTargetADU";
            this.FlatsTargetADU.Size = new System.Drawing.Size(108, 31);
            this.FlatsTargetADU.TabIndex = 39;
            this.FlatsTargetADU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FlatsTargetADU.Value = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.FlatsTargetADU.ValueChanged += new System.EventHandler(this.FlatsTargetADU_ValueChanged);
            // 
            // TakeFlatsButton
            // 
            this.TakeFlatsButton.ForeColor = System.Drawing.Color.Black;
            this.TakeFlatsButton.Location = new System.Drawing.Point(190, 279);
            this.TakeFlatsButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.TakeFlatsButton.Name = "TakeFlatsButton";
            this.TakeFlatsButton.Size = new System.Drawing.Size(136, 48);
            this.TakeFlatsButton.TabIndex = 9;
            this.TakeFlatsButton.Text = "Image";
            this.TakeFlatsButton.UseVisualStyleBackColor = true;
            this.TakeFlatsButton.Click += new System.EventHandler(this.TakeFlatsButton_Click);
            // 
            // FlatsRepetitionsBox
            // 
            this.FlatsRepetitionsBox.ForeColor = System.Drawing.Color.Black;
            this.FlatsRepetitionsBox.Location = new System.Drawing.Point(118, 113);
            this.FlatsRepetitionsBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlatsRepetitionsBox.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.FlatsRepetitionsBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FlatsRepetitionsBox.Name = "FlatsRepetitionsBox";
            this.FlatsRepetitionsBox.Size = new System.Drawing.Size(72, 31);
            this.FlatsRepetitionsBox.TabIndex = 28;
            this.FlatsRepetitionsBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FlatsRepetitionsBox.ValueChanged += new System.EventHandler(this.FlatsRepetitionsBox_ValueChanged);
            // 
            // FlatManOnButton
            // 
            this.FlatManOnButton.ForeColor = System.Drawing.Color.Black;
            this.FlatManOnButton.Location = new System.Drawing.Point(326, 277);
            this.FlatManOnButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlatManOnButton.Name = "FlatManOnButton";
            this.FlatManOnButton.Size = new System.Drawing.Size(150, 52);
            this.FlatManOnButton.TabIndex = 35;
            this.FlatManOnButton.Text = "Turn On";
            this.FlatManOnButton.UseVisualStyleBackColor = true;
            this.FlatManOnButton.Click += new System.EventHandler(this.FlatManOnButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FlatManManualSetupCheckbox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.FlatManPortNum);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.FlatManExposureNum);
            this.groupBox1.Controls.Add(this.FlatManStageButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.FlatManEastCheckBox);
            this.groupBox1.Controls.Add(this.FlatManBrightnessNum);
            this.groupBox1.Controls.Add(this.FlatManOnButton);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(16, 390);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(516, 360);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FlatMan Configuration";
            // 
            // FlatManManualSetupCheckbox
            // 
            this.FlatManManualSetupCheckbox.AutoSize = true;
            this.FlatManManualSetupCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.FlatManManualSetupCheckbox.Location = new System.Drawing.Point(42, 37);
            this.FlatManManualSetupCheckbox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlatManManualSetupCheckbox.Name = "FlatManManualSetupCheckbox";
            this.FlatManManualSetupCheckbox.Size = new System.Drawing.Size(177, 29);
            this.FlatManManualSetupCheckbox.TabIndex = 46;
            this.FlatManManualSetupCheckbox.Text = "Manual Setup";
            this.FlatManManualSetupCheckbox.UseVisualStyleBackColor = true;
            this.FlatManManualSetupCheckbox.CheckedChanged += new System.EventHandler(this.FlatManManualSetupCheckbox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(254, 40);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 25);
            this.label3.TabIndex = 93;
            this.label3.Text = "Com Port";
            // 
            // FlatManPortNum
            // 
            this.FlatManPortNum.Location = new System.Drawing.Point(376, 37);
            this.FlatManPortNum.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlatManPortNum.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.FlatManPortNum.Name = "FlatManPortNum";
            this.FlatManPortNum.Size = new System.Drawing.Size(100, 31);
            this.FlatManPortNum.TabIndex = 92;
            this.FlatManPortNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FlatManPortNum.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.FlatManPortNum.ValueChanged += new System.EventHandler(this.FlatManPortNum_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(202, 106);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 25);
            this.label1.TabIndex = 41;
            this.label1.Text = "Exposure";
            // 
            // FlatManExposureNum
            // 
            this.FlatManExposureNum.DecimalPlaces = 1;
            this.FlatManExposureNum.Location = new System.Drawing.Point(326, 102);
            this.FlatManExposureNum.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlatManExposureNum.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.FlatManExposureNum.Name = "FlatManExposureNum";
            this.FlatManExposureNum.Size = new System.Drawing.Size(98, 31);
            this.FlatManExposureNum.TabIndex = 40;
            this.FlatManExposureNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FlatManExposureNum.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.FlatManExposureNum.ValueChanged += new System.EventHandler(this.FlatManExposureNum_ValueChanged);
            // 
            // FlatManStageButton
            // 
            this.FlatManStageButton.ForeColor = System.Drawing.Color.Black;
            this.FlatManStageButton.Location = new System.Drawing.Point(42, 277);
            this.FlatManStageButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlatManStageButton.Name = "FlatManStageButton";
            this.FlatManStageButton.Size = new System.Drawing.Size(150, 52);
            this.FlatManStageButton.TabIndex = 39;
            this.FlatManStageButton.Text = "Stage";
            this.FlatManStageButton.UseVisualStyleBackColor = true;
            this.FlatManStageButton.Click += new System.EventHandler(this.FlatManStageButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(202, 156);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 25);
            this.label2.TabIndex = 37;
            this.label2.Text = "Brightness";
            // 
            // FlatManEastCheckBox
            // 
            this.FlatManEastCheckBox.AutoSize = true;
            this.FlatManEastCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.FlatManEastCheckBox.Checked = true;
            this.FlatManEastCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FlatManEastCheckBox.Location = new System.Drawing.Point(172, 202);
            this.FlatManEastCheckBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlatManEastCheckBox.Name = "FlatManEastCheckBox";
            this.FlatManEastCheckBox.Size = new System.Drawing.Size(251, 29);
            this.FlatManEastCheckBox.TabIndex = 38;
            this.FlatManEastCheckBox.Text = "FlatMan East of Pier?";
            this.FlatManEastCheckBox.UseVisualStyleBackColor = true;
            this.FlatManEastCheckBox.CheckedChanged += new System.EventHandler(this.FlatManEastCheckBox_CheckedChanged);
            // 
            // FlatManBrightnessNum
            // 
            this.FlatManBrightnessNum.Location = new System.Drawing.Point(326, 152);
            this.FlatManBrightnessNum.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlatManBrightnessNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.FlatManBrightnessNum.Name = "FlatManBrightnessNum";
            this.FlatManBrightnessNum.Size = new System.Drawing.Size(98, 31);
            this.FlatManBrightnessNum.TabIndex = 36;
            this.FlatManBrightnessNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FlatManBrightnessNum.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.FlatManBrightnessNum.ValueChanged += new System.EventHandler(this.FlatManBrightnessNum_ValueChanged);
            // 
            // FormFlats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(556, 773);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FlatsGroup);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "FormFlats";
            this.Text = "Set Up";
            this.FlatsGroup.ResumeLayout(false);
            this.FlatsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RotatorPANum)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlatsTargetADU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlatsRepetitionsBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlatManPortNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlatManExposureNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlatManBrightnessNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.GroupBox FlatsGroup;
        internal System.Windows.Forms.Label FlatsCountLabel;
        internal System.Windows.Forms.Button TakeFlatsButton;
        internal System.Windows.Forms.NumericUpDown FlatsRepetitionsBox;
        internal System.Windows.Forms.Button FlatManOnButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown FlatManBrightnessNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown FlatsTargetADU;
        private System.Windows.Forms.CheckBox FlatManEastCheckBox;
        private System.Windows.Forms.CheckBox FlatFlipCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton DuskRadioButton;
        private System.Windows.Forms.RadioButton DawnRadioButton;
        private System.Windows.Forms.RadioButton FlatManRadioButton;
        internal System.Windows.Forms.Button FlatManStageButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown FlatManExposureNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown FlatManPortNum;
        internal System.Windows.Forms.Button MakeFlatsButton;
        internal System.Windows.Forms.Button ClearFlatsButton;
        private System.Windows.Forms.CheckBox FlatsRotationCheckBox;
        private System.Windows.Forms.NumericUpDown RotatorPANum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox FlatManManualSetupCheckbox;
    }
}