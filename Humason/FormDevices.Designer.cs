namespace Humason
{
    partial class FormDevices
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
            this.AutoguideCheck = new System.Windows.Forms.CheckBox();
            this.AutofocusCheck = new System.Windows.Forms.CheckBox();
            this.RotatorCheckBox = new System.Windows.Forms.CheckBox();
            this.FilterListBox = new System.Windows.Forms.CheckedListBox();
            this.FocusFilterNum = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ClearFilterNum = new System.Windows.Forms.NumericUpDown();
            this.FiltersGroupBox = new System.Windows.Forms.GroupBox();
            this.RefreshFiltersButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.CameraTemperatureSet = new System.Windows.Forms.NumericUpDown();
            this.FocuserGroupBox = new System.Windows.Forms.GroupBox();
            this.RefocustTemperatureChangeBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AtFocus3RadioButton = new System.Windows.Forms.RadioButton();
            this.AtFocus2RadioButton = new System.Windows.Forms.RadioButton();
            this.DitherCheck = new System.Windows.Forms.CheckBox();
            this.GuiderGroupBox = new System.Windows.Forms.GroupBox();
            this.GuiderAutoDarkCheckBox = new System.Windows.Forms.CheckBox();
            this.ResyncCheck = new System.Windows.Forms.CheckBox();
            this.CalibrateCheck = new System.Windows.Forms.CheckBox();
            this.RotatorGroupBox = new System.Windows.Forms.GroupBox();
            this.CameraGroupBox = new System.Windows.Forms.GroupBox();
            this.RecalibrateAfterFlipCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.FocusFilterNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClearFilterNum)).BeginInit();
            this.FiltersGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraTemperatureSet)).BeginInit();
            this.FocuserGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefocustTemperatureChangeBox)).BeginInit();
            this.GuiderGroupBox.SuspendLayout();
            this.RotatorGroupBox.SuspendLayout();
            this.CameraGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AutoguideCheck
            // 
            this.AutoguideCheck.AutoSize = true;
            this.AutoguideCheck.ForeColor = System.Drawing.Color.White;
            this.AutoguideCheck.Location = new System.Drawing.Point(26, 19);
            this.AutoguideCheck.Name = "AutoguideCheck";
            this.AutoguideCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AutoguideCheck.Size = new System.Drawing.Size(74, 17);
            this.AutoguideCheck.TabIndex = 67;
            this.AutoguideCheck.Text = "Autoguide";
            this.AutoguideCheck.UseVisualStyleBackColor = true;
            this.AutoguideCheck.CheckedChanged += new System.EventHandler(this.AutoguideCheck_CheckedChanged);
            // 
            // AutofocusCheck
            // 
            this.AutofocusCheck.AutoSize = true;
            this.AutofocusCheck.ForeColor = System.Drawing.Color.White;
            this.AutofocusCheck.Location = new System.Drawing.Point(26, 19);
            this.AutofocusCheck.Name = "AutofocusCheck";
            this.AutofocusCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AutofocusCheck.Size = new System.Drawing.Size(96, 17);
            this.AutofocusCheck.TabIndex = 68;
            this.AutofocusCheck.Text = "Use Autofocus";
            this.AutofocusCheck.UseVisualStyleBackColor = true;
            this.AutofocusCheck.CheckedChanged += new System.EventHandler(this.AutofocusCheck_CheckedChanged);
            // 
            // RotatorCheckBox
            // 
            this.RotatorCheckBox.AutoSize = true;
            this.RotatorCheckBox.ForeColor = System.Drawing.Color.White;
            this.RotatorCheckBox.Location = new System.Drawing.Point(27, 19);
            this.RotatorCheckBox.Name = "RotatorCheckBox";
            this.RotatorCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RotatorCheckBox.Size = new System.Drawing.Size(83, 17);
            this.RotatorCheckBox.TabIndex = 71;
            this.RotatorCheckBox.Text = "Use Rotator";
            this.RotatorCheckBox.UseVisualStyleBackColor = true;
            this.RotatorCheckBox.CheckedChanged += new System.EventHandler(this.RotatorCheckBox_CheckedChanged);
            // 
            // FilterListBox
            // 
            this.FilterListBox.CheckOnClick = true;
            this.FilterListBox.FormattingEnabled = true;
            this.FilterListBox.Location = new System.Drawing.Point(11, 19);
            this.FilterListBox.Name = "FilterListBox";
            this.FilterListBox.Size = new System.Drawing.Size(108, 94);
            this.FilterListBox.TabIndex = 84;
            this.FilterListBox.SelectedIndexChanged += new System.EventHandler(this.FilterList_SelectedIndexChanged);
            // 
            // FocusFilterNum
            // 
            this.FocusFilterNum.Location = new System.Drawing.Point(198, 93);
            this.FocusFilterNum.Name = "FocusFilterNum";
            this.FocusFilterNum.Size = new System.Drawing.Size(33, 20);
            this.FocusFilterNum.TabIndex = 85;
            this.FocusFilterNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FocusFilterNum.ValueChanged += new System.EventHandler(this.FocusFilterNum_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(136, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 86;
            this.label1.Text = "Focus Filter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(141, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 89;
            this.label3.Text = "Clear Filter";
            // 
            // ClearFilterNum
            // 
            this.ClearFilterNum.Location = new System.Drawing.Point(198, 67);
            this.ClearFilterNum.Name = "ClearFilterNum";
            this.ClearFilterNum.Size = new System.Drawing.Size(33, 20);
            this.ClearFilterNum.TabIndex = 88;
            this.ClearFilterNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ClearFilterNum.ValueChanged += new System.EventHandler(this.ClearFilterNum_ValueChanged);
            // 
            // FiltersGroupBox
            // 
            this.FiltersGroupBox.Controls.Add(this.RefreshFiltersButton);
            this.FiltersGroupBox.Controls.Add(this.label3);
            this.FiltersGroupBox.Controls.Add(this.ClearFilterNum);
            this.FiltersGroupBox.Controls.Add(this.label1);
            this.FiltersGroupBox.Controls.Add(this.FocusFilterNum);
            this.FiltersGroupBox.Controls.Add(this.FilterListBox);
            this.FiltersGroupBox.ForeColor = System.Drawing.Color.White;
            this.FiltersGroupBox.Location = new System.Drawing.Point(12, 49);
            this.FiltersGroupBox.Name = "FiltersGroupBox";
            this.FiltersGroupBox.Size = new System.Drawing.Size(254, 126);
            this.FiltersGroupBox.TabIndex = 90;
            this.FiltersGroupBox.TabStop = false;
            this.FiltersGroupBox.Text = "Filters";
            // 
            // RefreshFiltersButton
            // 
            this.RefreshFiltersButton.ForeColor = System.Drawing.Color.Black;
            this.RefreshFiltersButton.Location = new System.Drawing.Point(161, 19);
            this.RefreshFiltersButton.Name = "RefreshFiltersButton";
            this.RefreshFiltersButton.Size = new System.Drawing.Size(70, 26);
            this.RefreshFiltersButton.TabIndex = 90;
            this.RefreshFiltersButton.Text = "Refresh";
            this.RefreshFiltersButton.UseVisualStyleBackColor = true;
            this.RefreshFiltersButton.Click += new System.EventHandler(this.RefreshFiltersButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(121, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 93;
            this.label4.Text = "CCD Temp";
            // 
            // CameraTemperatureSet
            // 
            this.CameraTemperatureSet.Location = new System.Drawing.Point(186, 14);
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
            this.CameraTemperatureSet.ValueChanged += new System.EventHandler(this.CameraTemperatureSet_ValueChanged);
            // 
            // FocuserGroupBox
            // 
            this.FocuserGroupBox.Controls.Add(this.RefocustTemperatureChangeBox);
            this.FocuserGroupBox.Controls.Add(this.AutofocusCheck);
            this.FocuserGroupBox.Controls.Add(this.label5);
            this.FocuserGroupBox.Controls.Add(this.label2);
            this.FocuserGroupBox.Controls.Add(this.AtFocus3RadioButton);
            this.FocuserGroupBox.Controls.Add(this.AtFocus2RadioButton);
            this.FocuserGroupBox.ForeColor = System.Drawing.Color.White;
            this.FocuserGroupBox.Location = new System.Drawing.Point(12, 257);
            this.FocuserGroupBox.Name = "FocuserGroupBox";
            this.FocuserGroupBox.Size = new System.Drawing.Size(254, 85);
            this.FocuserGroupBox.TabIndex = 94;
            this.FocuserGroupBox.TabStop = false;
            this.FocuserGroupBox.Text = "Focuser";
            // 
            // RefocustTemperatureChangeBox
            // 
            this.RefocustTemperatureChangeBox.DecimalPlaces = 1;
            this.RefocustTemperatureChangeBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.RefocustTemperatureChangeBox.Location = new System.Drawing.Point(89, 56);
            this.RefocustTemperatureChangeBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.RefocustTemperatureChangeBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.RefocustTemperatureChangeBox.Name = "RefocustTemperatureChangeBox";
            this.RefocustTemperatureChangeBox.Size = new System.Drawing.Size(47, 20);
            this.RefocustTemperatureChangeBox.TabIndex = 73;
            this.RefocustTemperatureChangeBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RefocustTemperatureChangeBox.ValueChanged += new System.EventHandler(this.RefocusTemperatureChangeBox_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 72;
            this.label5.Text = "deg C change";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 71;
            this.label2.Text = "Refocus at";
            // 
            // AtFocus3RadioButton
            // 
            this.AtFocus3RadioButton.AutoSize = true;
            this.AtFocus3RadioButton.ForeColor = System.Drawing.Color.White;
            this.AtFocus3RadioButton.Location = new System.Drawing.Point(161, 35);
            this.AtFocus3RadioButton.Name = "AtFocus3RadioButton";
            this.AtFocus3RadioButton.Size = new System.Drawing.Size(71, 17);
            this.AtFocus3RadioButton.TabIndex = 70;
            this.AtFocus3RadioButton.Text = "@Focus3";
            this.AtFocus3RadioButton.UseVisualStyleBackColor = true;
            this.AtFocus3RadioButton.CheckedChanged += new System.EventHandler(this.AtFocus3RadioButton_CheckedChanged);
            // 
            // AtFocus2RadioButton
            // 
            this.AtFocus2RadioButton.AutoSize = true;
            this.AtFocus2RadioButton.Checked = true;
            this.AtFocus2RadioButton.ForeColor = System.Drawing.Color.White;
            this.AtFocus2RadioButton.Location = new System.Drawing.Point(161, 12);
            this.AtFocus2RadioButton.Name = "AtFocus2RadioButton";
            this.AtFocus2RadioButton.Size = new System.Drawing.Size(71, 17);
            this.AtFocus2RadioButton.TabIndex = 69;
            this.AtFocus2RadioButton.TabStop = true;
            this.AtFocus2RadioButton.Text = "@Focus2";
            this.AtFocus2RadioButton.UseVisualStyleBackColor = true;
            this.AtFocus2RadioButton.CheckedChanged += new System.EventHandler(this.AtFocus2RadioButton_CheckedChanged);
            // 
            // DitherCheck
            // 
            this.DitherCheck.AutoSize = true;
            this.DitherCheck.ForeColor = System.Drawing.Color.White;
            this.DitherCheck.Location = new System.Drawing.Point(99, 42);
            this.DitherCheck.Name = "DitherCheck";
            this.DitherCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DitherCheck.Size = new System.Drawing.Size(54, 17);
            this.DitherCheck.TabIndex = 70;
            this.DitherCheck.Text = "Dither";
            this.DitherCheck.UseVisualStyleBackColor = true;
            this.DitherCheck.CheckedChanged += new System.EventHandler(this.DitherCheck_CheckedChanged);
            // 
            // GuiderGroupBox
            // 
            this.GuiderGroupBox.Controls.Add(this.GuiderAutoDarkCheckBox);
            this.GuiderGroupBox.Controls.Add(this.ResyncCheck);
            this.GuiderGroupBox.Controls.Add(this.CalibrateCheck);
            this.GuiderGroupBox.Controls.Add(this.DitherCheck);
            this.GuiderGroupBox.Controls.Add(this.AutoguideCheck);
            this.GuiderGroupBox.ForeColor = System.Drawing.Color.White;
            this.GuiderGroupBox.Location = new System.Drawing.Point(12, 181);
            this.GuiderGroupBox.Name = "GuiderGroupBox";
            this.GuiderGroupBox.Size = new System.Drawing.Size(254, 70);
            this.GuiderGroupBox.TabIndex = 95;
            this.GuiderGroupBox.TabStop = false;
            this.GuiderGroupBox.Text = "Guider";
            // 
            // GuiderAutoDarkCheckBox
            // 
            this.GuiderAutoDarkCheckBox.AutoSize = true;
            this.GuiderAutoDarkCheckBox.ForeColor = System.Drawing.Color.White;
            this.GuiderAutoDarkCheckBox.Location = new System.Drawing.Point(99, 19);
            this.GuiderAutoDarkCheckBox.Name = "GuiderAutoDarkCheckBox";
            this.GuiderAutoDarkCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.GuiderAutoDarkCheckBox.Size = new System.Drawing.Size(71, 17);
            this.GuiderAutoDarkCheckBox.TabIndex = 73;
            this.GuiderAutoDarkCheckBox.Text = "AutoDark";
            this.GuiderAutoDarkCheckBox.UseVisualStyleBackColor = true;
            this.GuiderAutoDarkCheckBox.CheckedChanged += new System.EventHandler(this.GuiderAutoDarkCheckBox_CheckedChanged);
            // 
            // ResyncCheck
            // 
            this.ResyncCheck.AutoSize = true;
            this.ResyncCheck.ForeColor = System.Drawing.Color.White;
            this.ResyncCheck.Location = new System.Drawing.Point(169, 42);
            this.ResyncCheck.Name = "ResyncCheck";
            this.ResyncCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ResyncCheck.Size = new System.Drawing.Size(62, 17);
            this.ResyncCheck.TabIndex = 72;
            this.ResyncCheck.Text = "Resync";
            this.ResyncCheck.UseVisualStyleBackColor = true;
            this.ResyncCheck.CheckedChanged += new System.EventHandler(this.ResyncCheck_CheckedChanged);
            // 
            // CalibrateCheck
            // 
            this.CalibrateCheck.AutoSize = true;
            this.CalibrateCheck.ForeColor = System.Drawing.Color.White;
            this.CalibrateCheck.Location = new System.Drawing.Point(26, 42);
            this.CalibrateCheck.Name = "CalibrateCheck";
            this.CalibrateCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CalibrateCheck.Size = new System.Drawing.Size(67, 17);
            this.CalibrateCheck.TabIndex = 71;
            this.CalibrateCheck.Text = "Calibrate";
            this.CalibrateCheck.UseVisualStyleBackColor = true;
            this.CalibrateCheck.CheckedChanged += new System.EventHandler(this.CalibrateCheck_CheckedChanged);
            // 
            // RotatorGroupBox
            // 
            this.RotatorGroupBox.Controls.Add(this.RecalibrateAfterFlipCheckbox);
            this.RotatorGroupBox.Controls.Add(this.RotatorCheckBox);
            this.RotatorGroupBox.ForeColor = System.Drawing.Color.White;
            this.RotatorGroupBox.Location = new System.Drawing.Point(12, 348);
            this.RotatorGroupBox.Name = "RotatorGroupBox";
            this.RotatorGroupBox.Size = new System.Drawing.Size(254, 42);
            this.RotatorGroupBox.TabIndex = 96;
            this.RotatorGroupBox.TabStop = false;
            this.RotatorGroupBox.Text = "Rotator";
            // 
            // CameraGroupBox
            // 
            this.CameraGroupBox.Controls.Add(this.label4);
            this.CameraGroupBox.Controls.Add(this.CameraTemperatureSet);
            this.CameraGroupBox.ForeColor = System.Drawing.Color.White;
            this.CameraGroupBox.Location = new System.Drawing.Point(12, 3);
            this.CameraGroupBox.Name = "CameraGroupBox";
            this.CameraGroupBox.Size = new System.Drawing.Size(254, 40);
            this.CameraGroupBox.TabIndex = 97;
            this.CameraGroupBox.TabStop = false;
            this.CameraGroupBox.Text = "Camera";
            // 
            // RecalibrateAfterFlipCheckbox
            // 
            this.RecalibrateAfterFlipCheckbox.AutoSize = true;
            this.RecalibrateAfterFlipCheckbox.ForeColor = System.Drawing.Color.White;
            this.RecalibrateAfterFlipCheckbox.Location = new System.Drawing.Point(124, 19);
            this.RecalibrateAfterFlipCheckbox.Name = "RecalibrateAfterFlipCheckbox";
            this.RecalibrateAfterFlipCheckbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RecalibrateAfterFlipCheckbox.Size = new System.Drawing.Size(124, 17);
            this.RecalibrateAfterFlipCheckbox.TabIndex = 72;
            this.RecalibrateAfterFlipCheckbox.Text = "Recalibrate After Flip";
            this.RecalibrateAfterFlipCheckbox.UseVisualStyleBackColor = true;
            this.RecalibrateAfterFlipCheckbox.CheckedChanged += new System.EventHandler(this.RecalibrateAfterFlipCheckbox_CheckedChanged);
            // 
            // FormDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(278, 402);
            this.ControlBox = false;
            this.Controls.Add(this.CameraGroupBox);
            this.Controls.Add(this.RotatorGroupBox);
            this.Controls.Add(this.GuiderGroupBox);
            this.Controls.Add(this.FocuserGroupBox);
            this.Controls.Add(this.FiltersGroupBox);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDevices";
            this.ShowIcon = false;
            this.Text = "SetUpForm";
            ((System.ComponentModel.ISupportInitialize)(this.FocusFilterNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClearFilterNum)).EndInit();
            this.FiltersGroupBox.ResumeLayout(false);
            this.FiltersGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraTemperatureSet)).EndInit();
            this.FocuserGroupBox.ResumeLayout(false);
            this.FocuserGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefocustTemperatureChangeBox)).EndInit();
            this.GuiderGroupBox.ResumeLayout(false);
            this.GuiderGroupBox.PerformLayout();
            this.RotatorGroupBox.ResumeLayout(false);
            this.RotatorGroupBox.PerformLayout();
            this.CameraGroupBox.ResumeLayout(false);
            this.CameraGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.CheckBox AutoguideCheck;
        internal System.Windows.Forms.CheckBox AutofocusCheck;
        internal System.Windows.Forms.CheckBox RotatorCheckBox;
        private System.Windows.Forms.CheckedListBox FilterListBox;
        private System.Windows.Forms.NumericUpDown FocusFilterNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown ClearFilterNum;
        private System.Windows.Forms.GroupBox FiltersGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown CameraTemperatureSet;
        private System.Windows.Forms.GroupBox FocuserGroupBox;
        private System.Windows.Forms.RadioButton AtFocus3RadioButton;
        private System.Windows.Forms.RadioButton AtFocus2RadioButton;
        internal System.Windows.Forms.CheckBox DitherCheck;
        private System.Windows.Forms.GroupBox GuiderGroupBox;
        private System.Windows.Forms.GroupBox RotatorGroupBox;
        private System.Windows.Forms.GroupBox CameraGroupBox;
        private System.Windows.Forms.Button RefreshFiltersButton;
        internal System.Windows.Forms.CheckBox ResyncCheck;
        internal System.Windows.Forms.CheckBox CalibrateCheck;
        private System.Windows.Forms.NumericUpDown RefocustTemperatureChangeBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.CheckBox GuiderAutoDarkCheckBox;
        internal System.Windows.Forms.CheckBox RecalibrateAfterFlipCheckbox;
    }
}