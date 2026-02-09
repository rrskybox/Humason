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
            this.UseRotatorCheckBox = new System.Windows.Forms.CheckBox();
            this.FilterListBox = new System.Windows.Forms.CheckedListBox();
            this.FocusFilterNum = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CLSFilterNum = new System.Windows.Forms.NumericUpDown();
            this.FiltersGroupBox = new System.Windows.Forms.GroupBox();
            this.NoFilterWheelCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.LumFilterNum = new System.Windows.Forms.NumericUpDown();
            this.RefreshFiltersButton = new System.Windows.Forms.Button();
            this.FocuserGroupBox = new System.Windows.Forms.GroupBox();
            this.RefocustTemperatureChangeBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AtFocus3RadioButton = new System.Windows.Forms.RadioButton();
            this.AtFocus2RadioButton = new System.Windows.Forms.RadioButton();
            this.DitherCheck = new System.Windows.Forms.CheckBox();
            this.GuiderGroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ResyncPeriodBox = new System.Windows.Forms.NumericUpDown();
            this.ResyncCheck = new System.Windows.Forms.CheckBox();
            this.GuiderCalibrateCheck = new System.Windows.Forms.CheckBox();
            this.RotatorGroupBox = new System.Windows.Forms.GroupBox();
            this.RecalibrateAfterFlipCheckbox = new System.Windows.Forms.CheckBox();
            this.HasDomeCheckBox = new System.Windows.Forms.CheckBox();
            this.HasWeatherCheckBox = new System.Windows.Forms.CheckBox();
            this.HasRotatorCheckBox = new System.Windows.Forms.CheckBox();
            this.WeatherFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.RefocusIntervalBox = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FocusFilterNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CLSFilterNum)).BeginInit();
            this.FiltersGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LumFilterNum)).BeginInit();
            this.FocuserGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefocustTemperatureChangeBox)).BeginInit();
            this.GuiderGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResyncPeriodBox)).BeginInit();
            this.RotatorGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefocusIntervalBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AutoguideCheck
            // 
            this.AutoguideCheck.AutoSize = true;
            this.AutoguideCheck.ForeColor = System.Drawing.Color.White;
            this.AutoguideCheck.Location = new System.Drawing.Point(52, 37);
            this.AutoguideCheck.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.AutoguideCheck.Name = "AutoguideCheck";
            this.AutoguideCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AutoguideCheck.Size = new System.Drawing.Size(141, 29);
            this.AutoguideCheck.TabIndex = 67;
            this.AutoguideCheck.Text = "Autoguide";
            this.AutoguideCheck.UseVisualStyleBackColor = true;
            this.AutoguideCheck.CheckedChanged += new System.EventHandler(this.AutoguideCheck_CheckedChanged);
            // 
            // AutofocusCheck
            // 
            this.AutofocusCheck.AutoSize = true;
            this.AutofocusCheck.ForeColor = System.Drawing.Color.White;
            this.AutofocusCheck.Location = new System.Drawing.Point(52, 37);
            this.AutofocusCheck.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.AutofocusCheck.Name = "AutofocusCheck";
            this.AutofocusCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AutofocusCheck.Size = new System.Drawing.Size(184, 29);
            this.AutofocusCheck.TabIndex = 68;
            this.AutofocusCheck.Text = "Use Autofocus";
            this.AutofocusCheck.UseVisualStyleBackColor = true;
            this.AutofocusCheck.CheckedChanged += new System.EventHandler(this.AutofocusCheck_CheckedChanged);
            // 
            // UseRotatorCheckBox
            // 
            this.UseRotatorCheckBox.AutoSize = true;
            this.UseRotatorCheckBox.ForeColor = System.Drawing.Color.White;
            this.UseRotatorCheckBox.Location = new System.Drawing.Point(52, 37);
            this.UseRotatorCheckBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.UseRotatorCheckBox.Name = "UseRotatorCheckBox";
            this.UseRotatorCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.UseRotatorCheckBox.Size = new System.Drawing.Size(158, 29);
            this.UseRotatorCheckBox.TabIndex = 71;
            this.UseRotatorCheckBox.Text = "Use Rotator";
            this.UseRotatorCheckBox.UseVisualStyleBackColor = true;
            this.UseRotatorCheckBox.CheckedChanged += new System.EventHandler(this.RotatorCheckBox_CheckedChanged);
            // 
            // FilterListBox
            // 
            this.FilterListBox.CheckOnClick = true;
            this.FilterListBox.FormattingEnabled = true;
            this.FilterListBox.Location = new System.Drawing.Point(12, 81);
            this.FilterListBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.FilterListBox.Name = "FilterListBox";
            this.FilterListBox.Size = new System.Drawing.Size(212, 116);
            this.FilterListBox.TabIndex = 84;
            this.FilterListBox.SelectedIndexChanged += new System.EventHandler(this.FilterList_SelectedIndexChanged);
            // 
            // FocusFilterNum
            // 
            this.FocusFilterNum.Location = new System.Drawing.Point(396, 196);
            this.FocusFilterNum.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.FocusFilterNum.Name = "FocusFilterNum";
            this.FocusFilterNum.Size = new System.Drawing.Size(68, 31);
            this.FocusFilterNum.TabIndex = 85;
            this.FocusFilterNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FocusFilterNum.ValueChanged += new System.EventHandler(this.FocusFilterNum_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(272, 190);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 25);
            this.label1.TabIndex = 86;
            this.label1.Text = "Focus Filter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(284, 150);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 25);
            this.label3.TabIndex = 89;
            this.label3.Text = "CLS Filter";
            // 
            // CLSFilterNum
            // 
            this.CLSFilterNum.Location = new System.Drawing.Point(396, 146);
            this.CLSFilterNum.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.CLSFilterNum.Name = "CLSFilterNum";
            this.CLSFilterNum.Size = new System.Drawing.Size(68, 31);
            this.CLSFilterNum.TabIndex = 88;
            this.CLSFilterNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CLSFilterNum.ValueChanged += new System.EventHandler(this.CLSFilterNum_ValueChanged);
            // 
            // FiltersGroupBox
            // 
            this.FiltersGroupBox.Controls.Add(this.NoFilterWheelCheckBox);
            this.FiltersGroupBox.Controls.Add(this.label6);
            this.FiltersGroupBox.Controls.Add(this.LumFilterNum);
            this.FiltersGroupBox.Controls.Add(this.RefreshFiltersButton);
            this.FiltersGroupBox.Controls.Add(this.label3);
            this.FiltersGroupBox.Controls.Add(this.CLSFilterNum);
            this.FiltersGroupBox.Controls.Add(this.label1);
            this.FiltersGroupBox.Controls.Add(this.FocusFilterNum);
            this.FiltersGroupBox.Controls.Add(this.FilterListBox);
            this.FiltersGroupBox.ForeColor = System.Drawing.Color.White;
            this.FiltersGroupBox.Location = new System.Drawing.Point(24, 21);
            this.FiltersGroupBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.FiltersGroupBox.Name = "FiltersGroupBox";
            this.FiltersGroupBox.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.FiltersGroupBox.Size = new System.Drawing.Size(508, 253);
            this.FiltersGroupBox.TabIndex = 90;
            this.FiltersGroupBox.TabStop = false;
            this.FiltersGroupBox.Text = "Camera";
            // 
            // NoFilterWheelCheckBox
            // 
            this.NoFilterWheelCheckBox.AutoSize = true;
            this.NoFilterWheelCheckBox.ForeColor = System.Drawing.SystemColors.Control;
            this.NoFilterWheelCheckBox.Location = new System.Drawing.Point(20, 37);
            this.NoFilterWheelCheckBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.NoFilterWheelCheckBox.Name = "NoFilterWheelCheckBox";
            this.NoFilterWheelCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NoFilterWheelCheckBox.Size = new System.Drawing.Size(192, 29);
            this.NoFilterWheelCheckBox.TabIndex = 115;
            this.NoFilterWheelCheckBox.Text = "No Filter Wheel";
            this.NoFilterWheelCheckBox.UseVisualStyleBackColor = true;
            this.NoFilterWheelCheckBox.CheckedChanged += new System.EventHandler(this.NoFilterWheelCheckBox_CheckedChanged_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(284, 100);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 25);
            this.label6.TabIndex = 92;
            this.label6.Text = "Lum Filter";
            // 
            // LumFilterNum
            // 
            this.LumFilterNum.Location = new System.Drawing.Point(396, 96);
            this.LumFilterNum.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.LumFilterNum.Name = "LumFilterNum";
            this.LumFilterNum.Size = new System.Drawing.Size(68, 31);
            this.LumFilterNum.TabIndex = 91;
            this.LumFilterNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.LumFilterNum.ValueChanged += new System.EventHandler(this.LumFilterNum_ValueChanged);
            // 
            // RefreshFiltersButton
            // 
            this.RefreshFiltersButton.ForeColor = System.Drawing.Color.Black;
            this.RefreshFiltersButton.Location = new System.Drawing.Point(348, 34);
            this.RefreshFiltersButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.RefreshFiltersButton.Name = "RefreshFiltersButton";
            this.RefreshFiltersButton.Size = new System.Drawing.Size(140, 50);
            this.RefreshFiltersButton.TabIndex = 90;
            this.RefreshFiltersButton.Text = "Refresh";
            this.RefreshFiltersButton.UseVisualStyleBackColor = true;
            this.RefreshFiltersButton.Click += new System.EventHandler(this.RefreshFiltersButton_Click);
            // 
            // FocuserGroupBox
            // 
            this.FocuserGroupBox.Controls.Add(this.RefocusIntervalBox);
            this.FocuserGroupBox.Controls.Add(this.label7);
            this.FocuserGroupBox.Controls.Add(this.label8);
            this.FocuserGroupBox.Controls.Add(this.RefocustTemperatureChangeBox);
            this.FocuserGroupBox.Controls.Add(this.AutofocusCheck);
            this.FocuserGroupBox.Controls.Add(this.label5);
            this.FocuserGroupBox.Controls.Add(this.label2);
            this.FocuserGroupBox.Controls.Add(this.AtFocus3RadioButton);
            this.FocuserGroupBox.Controls.Add(this.AtFocus2RadioButton);
            this.FocuserGroupBox.ForeColor = System.Drawing.Color.White;
            this.FocuserGroupBox.Location = new System.Drawing.Point(24, 433);
            this.FocuserGroupBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.FocuserGroupBox.Name = "FocuserGroupBox";
            this.FocuserGroupBox.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.FocuserGroupBox.Size = new System.Drawing.Size(508, 197);
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
            this.RefocustTemperatureChangeBox.Location = new System.Drawing.Point(180, 108);
            this.RefocustTemperatureChangeBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
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
            this.RefocustTemperatureChangeBox.Size = new System.Drawing.Size(92, 31);
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
            this.label5.Location = new System.Drawing.Point(288, 112);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 25);
            this.label5.TabIndex = 72;
            this.label5.Text = "deg C change";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 112);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 25);
            this.label2.TabIndex = 71;
            this.label2.Text = "Refocus at";
            // 
            // AtFocus3RadioButton
            // 
            this.AtFocus3RadioButton.AutoSize = true;
            this.AtFocus3RadioButton.ForeColor = System.Drawing.Color.White;
            this.AtFocus3RadioButton.Location = new System.Drawing.Point(324, 67);
            this.AtFocus3RadioButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.AtFocus3RadioButton.Name = "AtFocus3RadioButton";
            this.AtFocus3RadioButton.Size = new System.Drawing.Size(135, 29);
            this.AtFocus3RadioButton.TabIndex = 70;
            this.AtFocus3RadioButton.Text = "@Focus3";
            this.AtFocus3RadioButton.UseVisualStyleBackColor = true;
            this.AtFocus3RadioButton.CheckedChanged += new System.EventHandler(this.AtFocus3RadioButton_CheckedChanged);
            // 
            // AtFocus2RadioButton
            // 
            this.AtFocus2RadioButton.AutoSize = true;
            this.AtFocus2RadioButton.ForeColor = System.Drawing.Color.White;
            this.AtFocus2RadioButton.Location = new System.Drawing.Point(324, 21);
            this.AtFocus2RadioButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.AtFocus2RadioButton.Name = "AtFocus2RadioButton";
            this.AtFocus2RadioButton.Size = new System.Drawing.Size(135, 29);
            this.AtFocus2RadioButton.TabIndex = 69;
            this.AtFocus2RadioButton.Text = "@Focus2";
            this.AtFocus2RadioButton.UseVisualStyleBackColor = true;
            this.AtFocus2RadioButton.CheckedChanged += new System.EventHandler(this.AtFocus2RadioButton_CheckedChanged);
            // 
            // DitherCheck
            // 
            this.DitherCheck.AutoSize = true;
            this.DitherCheck.ForeColor = System.Drawing.Color.White;
            this.DitherCheck.Location = new System.Drawing.Point(196, 81);
            this.DitherCheck.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.DitherCheck.Name = "DitherCheck";
            this.DitherCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DitherCheck.Size = new System.Drawing.Size(101, 29);
            this.DitherCheck.TabIndex = 70;
            this.DitherCheck.Text = "Dither";
            this.DitherCheck.UseVisualStyleBackColor = true;
            this.DitherCheck.CheckedChanged += new System.EventHandler(this.DitherCheck_CheckedChanged);
            // 
            // GuiderGroupBox
            // 
            this.GuiderGroupBox.Controls.Add(this.label4);
            this.GuiderGroupBox.Controls.Add(this.ResyncPeriodBox);
            this.GuiderGroupBox.Controls.Add(this.ResyncCheck);
            this.GuiderGroupBox.Controls.Add(this.GuiderCalibrateCheck);
            this.GuiderGroupBox.Controls.Add(this.DitherCheck);
            this.GuiderGroupBox.Controls.Add(this.AutoguideCheck);
            this.GuiderGroupBox.ForeColor = System.Drawing.Color.White;
            this.GuiderGroupBox.Location = new System.Drawing.Point(24, 286);
            this.GuiderGroupBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.GuiderGroupBox.Name = "GuiderGroupBox";
            this.GuiderGroupBox.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.GuiderGroupBox.Size = new System.Drawing.Size(508, 135);
            this.GuiderGroupBox.TabIndex = 95;
            this.GuiderGroupBox.TabStop = false;
            this.GuiderGroupBox.Text = "Guider";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(406, 79);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 25);
            this.label4.TabIndex = 74;
            this.label4.Text = "(min)";
            // 
            // ResyncPeriodBox
            // 
            this.ResyncPeriodBox.Location = new System.Drawing.Point(384, 35);
            this.ResyncPeriodBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ResyncPeriodBox.Name = "ResyncPeriodBox";
            this.ResyncPeriodBox.Size = new System.Drawing.Size(104, 31);
            this.ResyncPeriodBox.TabIndex = 73;
            this.ResyncPeriodBox.ValueChanged += new System.EventHandler(this.ResyncPeriodBox_ValueChanged);
            // 
            // ResyncCheck
            // 
            this.ResyncCheck.AutoSize = true;
            this.ResyncCheck.ForeColor = System.Drawing.Color.White;
            this.ResyncCheck.Location = new System.Drawing.Point(258, 37);
            this.ResyncCheck.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.ResyncCheck.Name = "ResyncCheck";
            this.ResyncCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ResyncCheck.Size = new System.Drawing.Size(116, 29);
            this.ResyncCheck.TabIndex = 72;
            this.ResyncCheck.Text = "Resync";
            this.ResyncCheck.UseVisualStyleBackColor = true;
            this.ResyncCheck.CheckedChanged += new System.EventHandler(this.ResyncCheck_CheckedChanged);
            // 
            // GuiderCalibrateCheck
            // 
            this.GuiderCalibrateCheck.AutoSize = true;
            this.GuiderCalibrateCheck.ForeColor = System.Drawing.Color.White;
            this.GuiderCalibrateCheck.Location = new System.Drawing.Point(52, 81);
            this.GuiderCalibrateCheck.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.GuiderCalibrateCheck.Name = "GuiderCalibrateCheck";
            this.GuiderCalibrateCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.GuiderCalibrateCheck.Size = new System.Drawing.Size(130, 29);
            this.GuiderCalibrateCheck.TabIndex = 71;
            this.GuiderCalibrateCheck.Text = "Calibrate";
            this.GuiderCalibrateCheck.UseVisualStyleBackColor = true;
            this.GuiderCalibrateCheck.CheckedChanged += new System.EventHandler(this.CalibrateCheck_CheckedChanged);
            // 
            // RotatorGroupBox
            // 
            this.RotatorGroupBox.Controls.Add(this.RecalibrateAfterFlipCheckbox);
            this.RotatorGroupBox.Controls.Add(this.UseRotatorCheckBox);
            this.RotatorGroupBox.ForeColor = System.Drawing.Color.White;
            this.RotatorGroupBox.Location = new System.Drawing.Point(24, 685);
            this.RotatorGroupBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.RotatorGroupBox.Name = "RotatorGroupBox";
            this.RotatorGroupBox.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.RotatorGroupBox.Size = new System.Drawing.Size(508, 81);
            this.RotatorGroupBox.TabIndex = 96;
            this.RotatorGroupBox.TabStop = false;
            this.RotatorGroupBox.Text = "Rotator";
            // 
            // RecalibrateAfterFlipCheckbox
            // 
            this.RecalibrateAfterFlipCheckbox.AutoSize = true;
            this.RecalibrateAfterFlipCheckbox.ForeColor = System.Drawing.Color.White;
            this.RecalibrateAfterFlipCheckbox.Location = new System.Drawing.Point(248, 37);
            this.RecalibrateAfterFlipCheckbox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.RecalibrateAfterFlipCheckbox.Name = "RecalibrateAfterFlipCheckbox";
            this.RecalibrateAfterFlipCheckbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RecalibrateAfterFlipCheckbox.Size = new System.Drawing.Size(245, 29);
            this.RecalibrateAfterFlipCheckbox.TabIndex = 72;
            this.RecalibrateAfterFlipCheckbox.Text = "Recalibrate After Flip";
            this.RecalibrateAfterFlipCheckbox.UseVisualStyleBackColor = true;
            this.RecalibrateAfterFlipCheckbox.CheckedChanged += new System.EventHandler(this.RecalibrateAfterFlipCheckbox_CheckedChanged);
            // 
            // HasDomeCheckBox
            // 
            this.HasDomeCheckBox.AutoSize = true;
            this.HasDomeCheckBox.ForeColor = System.Drawing.SystemColors.Control;
            this.HasDomeCheckBox.Location = new System.Drawing.Point(300, 800);
            this.HasDomeCheckBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.HasDomeCheckBox.Name = "HasDomeCheckBox";
            this.HasDomeCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.HasDomeCheckBox.Size = new System.Drawing.Size(223, 29);
            this.HasDomeCheckBox.TabIndex = 114;
            this.HasDomeCheckBox.Text = "Has Dome Add-On";
            this.HasDomeCheckBox.UseVisualStyleBackColor = true;
            this.HasDomeCheckBox.CheckedChanged += new System.EventHandler(this.HasDomeCheckBox_CheckedChanged);
            // 
            // HasWeatherCheckBox
            // 
            this.HasWeatherCheckBox.AutoSize = true;
            this.HasWeatherCheckBox.ForeColor = System.Drawing.SystemColors.Control;
            this.HasWeatherCheckBox.Location = new System.Drawing.Point(24, 800);
            this.HasWeatherCheckBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.HasWeatherCheckBox.Name = "HasWeatherCheckBox";
            this.HasWeatherCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.HasWeatherCheckBox.Size = new System.Drawing.Size(247, 29);
            this.HasWeatherCheckBox.TabIndex = 113;
            this.HasWeatherCheckBox.Text = "Has Weather Monitor";
            this.HasWeatherCheckBox.UseVisualStyleBackColor = true;
            this.HasWeatherCheckBox.CheckedChanged += new System.EventHandler(this.HasWeatherCheckBox_CheckedChanged);
            // 
            // HasRotatorCheckBox
            // 
            this.HasRotatorCheckBox.AutoSize = true;
            this.HasRotatorCheckBox.ForeColor = System.Drawing.Color.White;
            this.HasRotatorCheckBox.Location = new System.Drawing.Point(24, 642);
            this.HasRotatorCheckBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.HasRotatorCheckBox.Name = "HasRotatorCheckBox";
            this.HasRotatorCheckBox.Size = new System.Drawing.Size(158, 29);
            this.HasRotatorCheckBox.TabIndex = 112;
            this.HasRotatorCheckBox.Text = "Has Rotator";
            this.HasRotatorCheckBox.UseVisualStyleBackColor = true;
            this.HasRotatorCheckBox.CheckedChanged += new System.EventHandler(this.HasRotatorCheckBox_CheckedChanged);
            // 
            // WeatherFileDialog
            // 
            this.WeatherFileDialog.FileName = "CCDAP";
            // 
            // RefocusIntervalBox
            // 
            this.RefocusIntervalBox.Location = new System.Drawing.Point(180, 151);
            this.RefocusIntervalBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.RefocusIntervalBox.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.RefocusIntervalBox.Name = "RefocusIntervalBox";
            this.RefocusIntervalBox.Size = new System.Drawing.Size(92, 31);
            this.RefocusIntervalBox.TabIndex = 76;
            this.RefocusIntervalBox.ValueChanged += new System.EventHandler(this.RefocusIntervalBox_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(288, 155);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 25);
            this.label7.TabIndex = 75;
            this.label7.Text = "min (zero = off)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 155);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 25);
            this.label8.TabIndex = 74;
            this.label8.Text = "Refocus after";
            // 
            // FormDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(556, 856);
            this.ControlBox = false;
            this.Controls.Add(this.HasDomeCheckBox);
            this.Controls.Add(this.HasWeatherCheckBox);
            this.Controls.Add(this.HasRotatorCheckBox);
            this.Controls.Add(this.RotatorGroupBox);
            this.Controls.Add(this.GuiderGroupBox);
            this.Controls.Add(this.FocuserGroupBox);
            this.Controls.Add(this.FiltersGroupBox);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDevices";
            this.ShowIcon = false;
            this.Text = "SetUpForm";
            ((System.ComponentModel.ISupportInitialize)(this.FocusFilterNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CLSFilterNum)).EndInit();
            this.FiltersGroupBox.ResumeLayout(false);
            this.FiltersGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LumFilterNum)).EndInit();
            this.FocuserGroupBox.ResumeLayout(false);
            this.FocuserGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefocustTemperatureChangeBox)).EndInit();
            this.GuiderGroupBox.ResumeLayout(false);
            this.GuiderGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResyncPeriodBox)).EndInit();
            this.RotatorGroupBox.ResumeLayout(false);
            this.RotatorGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefocusIntervalBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.CheckBox AutoguideCheck;
        internal System.Windows.Forms.CheckBox AutofocusCheck;
        internal System.Windows.Forms.CheckBox UseRotatorCheckBox;
        private System.Windows.Forms.CheckedListBox FilterListBox;
        private System.Windows.Forms.NumericUpDown FocusFilterNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown CLSFilterNum;
        private System.Windows.Forms.GroupBox FiltersGroupBox;
        private System.Windows.Forms.GroupBox FocuserGroupBox;
        private System.Windows.Forms.RadioButton AtFocus3RadioButton;
        private System.Windows.Forms.RadioButton AtFocus2RadioButton;
        internal System.Windows.Forms.CheckBox DitherCheck;
        private System.Windows.Forms.GroupBox GuiderGroupBox;
        private System.Windows.Forms.GroupBox RotatorGroupBox;
        private System.Windows.Forms.Button RefreshFiltersButton;
        internal System.Windows.Forms.CheckBox ResyncCheck;
        internal System.Windows.Forms.CheckBox GuiderCalibrateCheck;
        private System.Windows.Forms.NumericUpDown RefocustTemperatureChangeBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.CheckBox RecalibrateAfterFlipCheckbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown LumFilterNum;
        internal System.Windows.Forms.CheckBox NoFilterWheelCheckBox;
        internal System.Windows.Forms.CheckBox HasDomeCheckBox;
        internal System.Windows.Forms.CheckBox HasWeatherCheckBox;
        private System.Windows.Forms.CheckBox HasRotatorCheckBox;
        private System.Windows.Forms.OpenFileDialog WeatherFileDialog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown ResyncPeriodBox;
        private System.Windows.Forms.NumericUpDown RefocusIntervalBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}