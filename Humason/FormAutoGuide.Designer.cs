namespace Humason
{
    partial class FormAutoGuide
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
            this.GuideStarGroup = new System.Windows.Forms.GroupBox();
            this.GuideStarXBox = new System.Windows.Forms.Label();
            this.GuideStarYBox = new System.Windows.Forms.Label();
            this.AutoGuideOnButton = new System.Windows.Forms.Button();
            this.FindStarButton = new System.Windows.Forms.Button();
            this.MinimumGuideExposureTimeBox = new System.Windows.Forms.NumericUpDown();
            this.CalibrateButton = new System.Windows.Forms.Button();
            this.ExposureTime = new System.Windows.Forms.Label();
            this.XAxisMoveTime = new System.Windows.Forms.NumericUpDown();
            this.YAxisMoveTime = new System.Windows.Forms.NumericUpDown();
            this.AOCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GuideStarADUNum = new System.Windows.Forms.NumericUpDown();
            this.OptimizeExposureButton = new System.Windows.Forms.Button();
            this.GuiderCycleTimeNum = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.MaximumGuideExposureTimeBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.GuideExposureTimeBox = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Binning2x2RadioButton = new System.Windows.Forms.RadioButton();
            this.Binning1x1RadioButton = new System.Windows.Forms.RadioButton();
            this.GuiderAutoCalibrateButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.GuideStarEdgeMarginNum = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.SubframeCheckBox = new System.Windows.Forms.CheckBox();
            this.GuideStarGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimumGuideExposureTimeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XAxisMoveTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YAxisMoveTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GuideStarADUNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GuiderCycleTimeNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaximumGuideExposureTimeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GuideExposureTimeBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GuideStarEdgeMarginNum)).BeginInit();
            this.SuspendLayout();
            // 
            // GuideStarGroup
            // 
            this.GuideStarGroup.Controls.Add(this.GuideStarXBox);
            this.GuideStarGroup.Controls.Add(this.GuideStarYBox);
            this.GuideStarGroup.ForeColor = System.Drawing.Color.White;
            this.GuideStarGroup.Location = new System.Drawing.Point(136, 197);
            this.GuideStarGroup.Name = "GuideStarGroup";
            this.GuideStarGroup.Size = new System.Drawing.Size(120, 45);
            this.GuideStarGroup.TabIndex = 28;
            this.GuideStarGroup.TabStop = false;
            this.GuideStarGroup.Text = "Guide Star";
            // 
            // GuideStarXBox
            // 
            this.GuideStarXBox.AutoSize = true;
            this.GuideStarXBox.ForeColor =  System.Drawing.Color.White;
            this.GuideStarXBox.Location = new System.Drawing.Point(15, 17);
            this.GuideStarXBox.Name = "GuideStarXBox";
            this.GuideStarXBox.Size = new System.Drawing.Size(13, 13);
            this.GuideStarXBox.TabIndex = 14;
            this.GuideStarXBox.Text = "0";
            // 
            // GuideStarYBox
            // 
            this.GuideStarYBox.AutoSize = true;
            this.GuideStarYBox.ForeColor =  System.Drawing.Color.White;
            this.GuideStarYBox.Location = new System.Drawing.Point(63, 17);
            this.GuideStarYBox.Name = "GuideStarYBox";
            this.GuideStarYBox.Size = new System.Drawing.Size(13, 13);
            this.GuideStarYBox.TabIndex = 15;
            this.GuideStarYBox.Text = "0";
            // 
            // AutoGuideOnButton
            // 
            this.AutoGuideOnButton.BackColor = System.Drawing.Color.White;
            this.AutoGuideOnButton.ForeColor = System.Drawing.Color.Black;
            this.AutoGuideOnButton.Location = new System.Drawing.Point(72, 362);
            this.AutoGuideOnButton.Name = "AutoGuideOnButton";
            this.AutoGuideOnButton.Size = new System.Drawing.Size(133, 28);
            this.AutoGuideOnButton.TabIndex = 24;
            this.AutoGuideOnButton.Text = "Start Autoguiding";
            this.AutoGuideOnButton.UseVisualStyleBackColor = false;
            this.AutoGuideOnButton.Click += new System.EventHandler(this.AutoGuideOnButton_Click);
            // 
            // FindStarButton
            // 
            this.FindStarButton.BackColor = System.Drawing.Color.White;
            this.FindStarButton.ForeColor = System.Drawing.Color.Black;
            this.FindStarButton.Location = new System.Drawing.Point(21, 198);
            this.FindStarButton.Name = "FindStarButton";
            this.FindStarButton.Size = new System.Drawing.Size(71, 45);
            this.FindStarButton.TabIndex = 25;
            this.FindStarButton.Text = "Find Guide Star";
            this.FindStarButton.UseVisualStyleBackColor = false;
            this.FindStarButton.Click += new System.EventHandler(this.FindStarButton_Click);
            // 
            // MinimumGuideExposureTimeBox
            // 
            this.MinimumGuideExposureTimeBox.BackColor = System.Drawing.Color.White;
            this.MinimumGuideExposureTimeBox.DecimalPlaces = 2;
            this.MinimumGuideExposureTimeBox.ForeColor = System.Drawing.Color.Black;
            this.MinimumGuideExposureTimeBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.MinimumGuideExposureTimeBox.Location = new System.Drawing.Point(205, 64);
            this.MinimumGuideExposureTimeBox.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.MinimumGuideExposureTimeBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.MinimumGuideExposureTimeBox.Name = "MinimumGuideExposureTimeBox";
            this.MinimumGuideExposureTimeBox.Size = new System.Drawing.Size(51, 20);
            this.MinimumGuideExposureTimeBox.TabIndex = 22;
            this.MinimumGuideExposureTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MinimumGuideExposureTimeBox.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.MinimumGuideExposureTimeBox.ValueChanged += new System.EventHandler(this.MinimumGuideExposureTimeBox_ValueChanged);
            // 
            // CalibrateButton
            // 
            this.CalibrateButton.BackColor = System.Drawing.Color.White;
            this.CalibrateButton.ForeColor = System.Drawing.Color.Black;
            this.CalibrateButton.Location = new System.Drawing.Point(21, 299);
            this.CalibrateButton.Name = "CalibrateButton";
            this.CalibrateButton.Size = new System.Drawing.Size(92, 20);
            this.CalibrateButton.TabIndex = 21;
            this.CalibrateButton.Text = "Calibrate";
            this.CalibrateButton.UseVisualStyleBackColor = false;
            this.CalibrateButton.Click += new System.EventHandler(this.CalibrateButton_Click);
            // 
            // ExposureTime
            // 
            this.ExposureTime.AutoSize = true;
            this.ExposureTime.ForeColor = System.Drawing.Color.White;
            this.ExposureTime.Location = new System.Drawing.Point(21, 66);
            this.ExposureTime.Name = "ExposureTime";
            this.ExposureTime.Size = new System.Drawing.Size(165, 13);
            this.ExposureTime.TabIndex = 23;
            this.ExposureTime.Text = "Minimum Guide Camera Exposure";
            // 
            // XAxisMoveTime
            // 
            this.XAxisMoveTime.BackColor = System.Drawing.Color.White;
            this.XAxisMoveTime.Location = new System.Drawing.Point(161, 299);
            this.XAxisMoveTime.Name = "XAxisMoveTime";
            this.XAxisMoveTime.Size = new System.Drawing.Size(51, 20);
            this.XAxisMoveTime.TabIndex = 17;
            this.XAxisMoveTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.XAxisMoveTime.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.XAxisMoveTime.ValueChanged += new System.EventHandler(this.XAxisMoveTime_ValueChanged);
            // 
            // YAxisMoveTime
            // 
            this.YAxisMoveTime.BackColor = System.Drawing.Color.White;
            this.YAxisMoveTime.Location = new System.Drawing.Point(161, 325);
            this.YAxisMoveTime.Name = "YAxisMoveTime";
            this.YAxisMoveTime.Size = new System.Drawing.Size(51, 20);
            this.YAxisMoveTime.TabIndex = 18;
            this.YAxisMoveTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.YAxisMoveTime.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.YAxisMoveTime.ValueChanged += new System.EventHandler(this.YAxisMoveTime_ValueChanged);
            // 
            // AOCheckBox
            // 
            this.AOCheckBox.AutoSize = true;
            this.AOCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AOCheckBox.ForeColor = System.Drawing.Color.White;
            this.AOCheckBox.Location = new System.Drawing.Point(21, 10);
            this.AOCheckBox.Name = "AOCheckBox";
            this.AOCheckBox.Size = new System.Drawing.Size(63, 17);
            this.AOCheckBox.TabIndex = 92;
            this.AOCheckBox.Text = "Use AO";
            this.AOCheckBox.UseVisualStyleBackColor = true;
            this.AOCheckBox.CheckedChanged += new System.EventHandler(this.AOCheck_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(21, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 94;
            this.label1.Text = "Guide Star Target ADU";
            // 
            // GuideStarADUNum
            // 
            this.GuideStarADUNum.BackColor = System.Drawing.Color.White;
            this.GuideStarADUNum.ForeColor = System.Drawing.Color.Black;
            this.GuideStarADUNum.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.GuideStarADUNum.Location = new System.Drawing.Point(205, 142);
            this.GuideStarADUNum.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.GuideStarADUNum.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.GuideStarADUNum.Name = "GuideStarADUNum";
            this.GuideStarADUNum.Size = new System.Drawing.Size(51, 20);
            this.GuideStarADUNum.TabIndex = 95;
            this.GuideStarADUNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GuideStarADUNum.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.GuideStarADUNum.ValueChanged += new System.EventHandler(this.GuideStarADUNum_ValueChanged);
            // 
            // OptimizeExposureButton
            // 
            this.OptimizeExposureButton.BackColor = System.Drawing.Color.White;
            this.OptimizeExposureButton.ForeColor = System.Drawing.Color.Black;
            this.OptimizeExposureButton.Location = new System.Drawing.Point(21, 248);
            this.OptimizeExposureButton.Name = "OptimizeExposureButton";
            this.OptimizeExposureButton.Size = new System.Drawing.Size(71, 45);
            this.OptimizeExposureButton.TabIndex = 96;
            this.OptimizeExposureButton.Text = "Optimize Exposure";
            this.OptimizeExposureButton.UseVisualStyleBackColor = false;
            this.OptimizeExposureButton.Click += new System.EventHandler(this.OptimizeExposureButton_Click);
            // 
            // GuiderCycleTimeNum
            // 
            this.GuiderCycleTimeNum.BackColor = System.Drawing.Color.White;
            this.GuiderCycleTimeNum.DecimalPlaces = 1;
            this.GuiderCycleTimeNum.ForeColor = System.Drawing.Color.Black;
            this.GuiderCycleTimeNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.GuiderCycleTimeNum.Location = new System.Drawing.Point(205, 116);
            this.GuiderCycleTimeNum.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.GuiderCycleTimeNum.Name = "GuiderCycleTimeNum";
            this.GuiderCycleTimeNum.Size = new System.Drawing.Size(51, 20);
            this.GuiderCycleTimeNum.TabIndex = 98;
            this.GuiderCycleTimeNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(21, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 97;
            this.label2.Text = "Total Guider Cycle Time";
            // 
            // MaximumGuideExposureTimeBox
            // 
            this.MaximumGuideExposureTimeBox.BackColor = System.Drawing.Color.White;
            this.MaximumGuideExposureTimeBox.DecimalPlaces = 2;
            this.MaximumGuideExposureTimeBox.ForeColor = System.Drawing.Color.Black;
            this.MaximumGuideExposureTimeBox.Location = new System.Drawing.Point(205, 90);
            this.MaximumGuideExposureTimeBox.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.MaximumGuideExposureTimeBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.MaximumGuideExposureTimeBox.Name = "MaximumGuideExposureTimeBox";
            this.MaximumGuideExposureTimeBox.Size = new System.Drawing.Size(51, 20);
            this.MaximumGuideExposureTimeBox.TabIndex = 99;
            this.MaximumGuideExposureTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MaximumGuideExposureTimeBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MaximumGuideExposureTimeBox.ValueChanged += new System.EventHandler(this.MaximumGuideExposureTimeBox_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(21, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 13);
            this.label3.TabIndex = 100;
            this.label3.Text = "Maximum Guide Camera Exposure";
            // 
            // GuideExposureTimeBox
            // 
            this.GuideExposureTimeBox.BackColor = System.Drawing.Color.White;
            this.GuideExposureTimeBox.DecimalPlaces = 2;
            this.GuideExposureTimeBox.ForeColor = System.Drawing.Color.Black;
            this.GuideExposureTimeBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.GuideExposureTimeBox.Location = new System.Drawing.Point(205, 37);
            this.GuideExposureTimeBox.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.GuideExposureTimeBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.GuideExposureTimeBox.Name = "GuideExposureTimeBox";
            this.GuideExposureTimeBox.Size = new System.Drawing.Size(51, 20);
            this.GuideExposureTimeBox.TabIndex = 101;
            this.GuideExposureTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GuideExposureTimeBox.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.GuideExposureTimeBox.ValueChanged += new System.EventHandler(this.GuideExposureTimeBox_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(21, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 13);
            this.label4.TabIndex = 102;
            this.label4.Text = "Default Guide Camera Exposure";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Binning2x2RadioButton);
            this.groupBox1.Controls.Add(this.Binning1x1RadioButton);
            this.groupBox1.ForeColor =  System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(136, 248);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(120, 45);
            this.groupBox1.TabIndex = 103;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Binning";
            // 
            // Binning2x2RadioButton
            // 
            this.Binning2x2RadioButton.AutoSize = true;
            this.Binning2x2RadioButton.ForeColor =  System.Drawing.Color.White;
            this.Binning2x2RadioButton.Location = new System.Drawing.Point(66, 20);
            this.Binning2x2RadioButton.Name = "Binning2x2RadioButton";
            this.Binning2x2RadioButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Binning2x2RadioButton.Size = new System.Drawing.Size(42, 17);
            this.Binning2x2RadioButton.TabIndex = 1;
            this.Binning2x2RadioButton.Text = "2x2";
            this.Binning2x2RadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Binning2x2RadioButton.UseVisualStyleBackColor = true;
            this.Binning2x2RadioButton.CheckedChanged += new System.EventHandler(this.Binning2x2RadioButton_CheckedChanged);
            // 
            // Binning1x1RadioButton
            // 
            this.Binning1x1RadioButton.AutoSize = true;
            this.Binning1x1RadioButton.Checked = true;
            this.Binning1x1RadioButton.ForeColor =  System.Drawing.Color.White;
            this.Binning1x1RadioButton.Location = new System.Drawing.Point(6, 19);
            this.Binning1x1RadioButton.Name = "Binning1x1RadioButton";
            this.Binning1x1RadioButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Binning1x1RadioButton.Size = new System.Drawing.Size(42, 17);
            this.Binning1x1RadioButton.TabIndex = 0;
            this.Binning1x1RadioButton.TabStop = true;
            this.Binning1x1RadioButton.Text = "1x1";
            this.Binning1x1RadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Binning1x1RadioButton.UseVisualStyleBackColor = true;
            this.Binning1x1RadioButton.CheckedChanged += new System.EventHandler(this.Binning1x1RadioButton_CheckedChanged);
            // 
            // GuiderAutoCalibrateButton
            // 
            this.GuiderAutoCalibrateButton.BackColor = System.Drawing.Color.White;
            this.GuiderAutoCalibrateButton.ForeColor = System.Drawing.Color.Black;
            this.GuiderAutoCalibrateButton.Location = new System.Drawing.Point(21, 325);
            this.GuiderAutoCalibrateButton.Name = "GuiderAutoCalibrateButton";
            this.GuiderAutoCalibrateButton.Size = new System.Drawing.Size(92, 20);
            this.GuiderAutoCalibrateButton.TabIndex = 104;
            this.GuiderAutoCalibrateButton.Text = "Auto-Calibrate";
            this.GuiderAutoCalibrateButton.UseVisualStyleBackColor = false;
            this.GuiderAutoCalibrateButton.Click += new System.EventHandler(this.GuiderAutoCalibrateButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(122, 303);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 106;
            this.label5.Text = "X Sec";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(122, 329);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 107;
            this.label6.Text = "Y Sec";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(21, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 108;
            this.label7.Text = "Edge Margin (pixels)";
            // 
            // GuideStarEdgeMarginNum
            // 
            this.GuideStarEdgeMarginNum.BackColor = System.Drawing.Color.White;
            this.GuideStarEdgeMarginNum.ForeColor = System.Drawing.Color.Black;
            this.GuideStarEdgeMarginNum.Location = new System.Drawing.Point(205, 169);
            this.GuideStarEdgeMarginNum.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.GuideStarEdgeMarginNum.Name = "GuideStarEdgeMarginNum";
            this.GuideStarEdgeMarginNum.Size = new System.Drawing.Size(51, 20);
            this.GuideStarEdgeMarginNum.TabIndex = 109;
            this.GuideStarEdgeMarginNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GuideStarEdgeMarginNum.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.GuideStarEdgeMarginNum.ValueChanged += new System.EventHandler(this.GuideStarEdgeMarginNum_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(103, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 13);
            this.label8.TabIndex = 110;
            this.label8.Text = "Also must be set in TSX";
            // 
            // SubframeCheckBox
            // 
            this.SubframeCheckBox.AutoSize = true;
            this.SubframeCheckBox.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SubframeCheckBox.ForeColor = System.Drawing.Color.White;
            this.SubframeCheckBox.Location = new System.Drawing.Point(218, 303);
            this.SubframeCheckBox.Name = "SubframeCheckBox";
            this.SubframeCheckBox.Size = new System.Drawing.Size(56, 31);
            this.SubframeCheckBox.TabIndex = 111;
            this.SubframeCheckBox.Text = "Subframe";
            this.SubframeCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SubframeCheckBox.UseVisualStyleBackColor = true;
            this.SubframeCheckBox.UseWaitCursor = true;
            this.SubframeCheckBox.CheckedChanged += new System.EventHandler(this.SubframeCheckBox_CheckedChanged_1);
            // 
            // FormAutoGuide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(278, 402);
            this.Controls.Add(this.SubframeCheckBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.GuideStarEdgeMarginNum);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.GuiderAutoCalibrateButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GuideExposureTimeBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MaximumGuideExposureTimeBox);
            this.Controls.Add(this.GuiderCycleTimeNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OptimizeExposureButton);
            this.Controls.Add(this.GuideStarADUNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AOCheckBox);
            this.Controls.Add(this.GuideStarGroup);
            this.Controls.Add(this.CalibrateButton);
            this.Controls.Add(this.AutoGuideOnButton);
            this.Controls.Add(this.FindStarButton);
            this.Controls.Add(this.MinimumGuideExposureTimeBox);
            this.Controls.Add(this.ExposureTime);
            this.Controls.Add(this.XAxisMoveTime);
            this.Controls.Add(this.YAxisMoveTime);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAutoGuide";
            this.Text = "Autoguide";
            this.GuideStarGroup.ResumeLayout(false);
            this.GuideStarGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimumGuideExposureTimeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XAxisMoveTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YAxisMoveTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GuideStarADUNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GuiderCycleTimeNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaximumGuideExposureTimeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GuideExposureTimeBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GuideStarEdgeMarginNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.GroupBox GuideStarGroup;
        internal System.Windows.Forms.Label GuideStarXBox;
        internal System.Windows.Forms.Label GuideStarYBox;
        internal System.Windows.Forms.Button AutoGuideOnButton;
        internal System.Windows.Forms.Button FindStarButton;
        internal System.Windows.Forms.NumericUpDown MinimumGuideExposureTimeBox;
        internal System.Windows.Forms.Button CalibrateButton;
        internal System.Windows.Forms.Label ExposureTime;
        internal System.Windows.Forms.NumericUpDown XAxisMoveTime;
        internal System.Windows.Forms.NumericUpDown YAxisMoveTime;
        internal System.Windows.Forms.CheckBox AOCheckBox;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.NumericUpDown GuideStarADUNum;
        internal System.Windows.Forms.Button OptimizeExposureButton;
        internal System.Windows.Forms.NumericUpDown GuiderCycleTimeNum;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.NumericUpDown MaximumGuideExposureTimeBox;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.NumericUpDown GuideExposureTimeBox;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Binning2x2RadioButton;
        private System.Windows.Forms.RadioButton Binning1x1RadioButton;
        internal System.Windows.Forms.Button GuiderAutoCalibrateButton;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.NumericUpDown GuideStarEdgeMarginNum;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.CheckBox SubframeCheckBox;
    }
}