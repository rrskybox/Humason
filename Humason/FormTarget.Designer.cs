namespace Humason
{
    partial class FormTarget
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
            this.LRGBRatioBox = new System.Windows.Forms.NumericUpDown();
            this.Label4 = new System.Windows.Forms.Label();
            this.ImgAnlzbutton = new System.Windows.Forms.Button();
            this.SnapShotButton = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.LimitLabel = new System.Windows.Forms.Label();
            this.SetLimitBox = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.TransitBox = new System.Windows.Forms.TextBox();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.LoopsVal = new System.Windows.Forms.NumericUpDown();
            this.LoopsLabel = new System.Windows.Forms.Label();
            this.ExposureVal = new System.Windows.Forms.NumericUpDown();
            this.ExposureLabel = new System.Windows.Forms.Label();
            this.TargetLabel = new System.Windows.Forms.Label();
            this.StartBoxLabel = new System.Windows.Forms.Label();
            this.Done = new System.Windows.Forms.Label();
            this.FlipLabel = new System.Windows.Forms.Label();
            this.DoneTimeBox = new System.Windows.Forms.TextBox();
            this.FlipTimeBox = new System.Windows.Forms.TextBox();
            this.TargetBox = new System.Windows.Forms.TextBox();
            this.DelayLabel = new System.Windows.Forms.Label();
            this.DelayVal = new System.Windows.Forms.NumericUpDown();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.StartTimeBox = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TargetRABox = new System.Windows.Forms.NumericUpDown();
            this.TargetDecBox = new System.Windows.Forms.NumericUpDown();
            this.TargetPABox = new System.Windows.Forms.NumericUpDown();
            this.MakeFlatsCheckBox = new System.Windows.Forms.CheckBox();
            this.DawnTimeBox = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.SaveDefaultButton = new System.Windows.Forms.Button();
            this.AdjustedTargetLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RiseLimitBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.PauseBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.LRGBRatioBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoopsVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExposureVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DelayVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetRABox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetDecBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetPABox)).BeginInit();
            this.SuspendLayout();
            // 
            // LRGBRatioBox
            // 
            this.LRGBRatioBox.Location = new System.Drawing.Point(190, 198);
            this.LRGBRatioBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.LRGBRatioBox.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.LRGBRatioBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LRGBRatioBox.Name = "LRGBRatioBox";
            this.LRGBRatioBox.Size = new System.Drawing.Size(90, 31);
            this.LRGBRatioBox.TabIndex = 81;
            this.LRGBRatioBox.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.LRGBRatioBox.ValueChanged += new System.EventHandler(this.LRGBRatioBox_ValueChanged);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.ForeColor = System.Drawing.Color.White;
            this.Label4.Location = new System.Drawing.Point(58, 202);
            this.Label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(125, 25);
            this.Label4.TabIndex = 80;
            this.Label4.Text = "LRGB Ratio";
            // 
            // ImgAnlzbutton
            // 
            this.ImgAnlzbutton.Location = new System.Drawing.Point(330, -54);
            this.ImgAnlzbutton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ImgAnlzbutton.Name = "ImgAnlzbutton";
            this.ImgAnlzbutton.Size = new System.Drawing.Size(150, 52);
            this.ImgAnlzbutton.TabIndex = 72;
            this.ImgAnlzbutton.Text = "Analyze";
            this.ImgAnlzbutton.UseVisualStyleBackColor = true;
            // 
            // SnapShotButton
            // 
            this.SnapShotButton.Location = new System.Drawing.Point(88, -54);
            this.SnapShotButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SnapShotButton.Name = "SnapShotButton";
            this.SnapShotButton.Size = new System.Drawing.Size(150, 52);
            this.SnapShotButton.TabIndex = 71;
            this.SnapShotButton.Text = "Snapshot\r\n";
            this.SnapShotButton.UseVisualStyleBackColor = true;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.ForeColor = System.Drawing.Color.White;
            this.Label3.Location = new System.Drawing.Point(316, 579);
            this.Label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(72, 25);
            this.Label3.TabIndex = 70;
            this.Label3.Text = "Dawn:";
            // 
            // LimitLabel
            // 
            this.LimitLabel.AutoSize = true;
            this.LimitLabel.ForeColor = System.Drawing.Color.White;
            this.LimitLabel.Location = new System.Drawing.Point(108, 479);
            this.LimitLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.LimitLabel.Name = "LimitLabel";
            this.LimitLabel.Size = new System.Drawing.Size(68, 25);
            this.LimitLabel.TabIndex = 68;
            this.LimitLabel.Text = "Limits";
            // 
            // SetLimitBox
            // 
            this.SetLimitBox.Location = new System.Drawing.Point(402, 473);
            this.SetLimitBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SetLimitBox.Name = "SetLimitBox";
            this.SetLimitBox.ReadOnly = true;
            this.SetLimitBox.Size = new System.Drawing.Size(104, 31);
            this.SetLimitBox.TabIndex = 67;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(316, 529);
            this.Label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 25);
            this.Label1.TabIndex = 66;
            this.Label1.Text = "Transit:";
            // 
            // TransitBox
            // 
            this.TransitBox.Location = new System.Drawing.Point(402, 523);
            this.TransitBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.TransitBox.Name = "TransitBox";
            this.TransitBox.ReadOnly = true;
            this.TransitBox.Size = new System.Drawing.Size(104, 31);
            this.TransitBox.TabIndex = 65;
            // 
            // UpdateButton
            // 
            this.UpdateButton.ForeColor = System.Drawing.Color.Black;
            this.UpdateButton.Location = new System.Drawing.Point(246, 631);
            this.UpdateButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(112, 44);
            this.UpdateButton.TabIndex = 64;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // LoopsVal
            // 
            this.LoopsVal.Location = new System.Drawing.Point(192, 148);
            this.LoopsVal.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.LoopsVal.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.LoopsVal.Name = "LoopsVal";
            this.LoopsVal.Size = new System.Drawing.Size(88, 31);
            this.LoopsVal.TabIndex = 62;
            this.LoopsVal.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.LoopsVal.ValueChanged += new System.EventHandler(this.LoopsVal_ValueChanged);
            // 
            // LoopsLabel
            // 
            this.LoopsLabel.AutoSize = true;
            this.LoopsLabel.ForeColor = System.Drawing.Color.White;
            this.LoopsLabel.Location = new System.Drawing.Point(58, 152);
            this.LoopsLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.LoopsLabel.Name = "LoopsLabel";
            this.LoopsLabel.Size = new System.Drawing.Size(71, 25);
            this.LoopsLabel.TabIndex = 61;
            this.LoopsLabel.Text = "Loops";
            // 
            // ExposureVal
            // 
            this.ExposureVal.Location = new System.Drawing.Point(190, 98);
            this.ExposureVal.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ExposureVal.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ExposureVal.Name = "ExposureVal";
            this.ExposureVal.Size = new System.Drawing.Size(90, 31);
            this.ExposureVal.TabIndex = 60;
            this.ExposureVal.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.ExposureVal.ValueChanged += new System.EventHandler(this.ExposureVal_ValueChanged);
            // 
            // ExposureLabel
            // 
            this.ExposureLabel.AutoSize = true;
            this.ExposureLabel.ForeColor = System.Drawing.Color.White;
            this.ExposureLabel.Location = new System.Drawing.Point(58, 102);
            this.ExposureLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.ExposureLabel.Name = "ExposureLabel";
            this.ExposureLabel.Size = new System.Drawing.Size(103, 25);
            this.ExposureLabel.TabIndex = 59;
            this.ExposureLabel.Text = "Exposure";
            // 
            // TargetLabel
            // 
            this.TargetLabel.AutoSize = true;
            this.TargetLabel.ForeColor = System.Drawing.Color.White;
            this.TargetLabel.Location = new System.Drawing.Point(38, 31);
            this.TargetLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.TargetLabel.Name = "TargetLabel";
            this.TargetLabel.Size = new System.Drawing.Size(74, 25);
            this.TargetLabel.TabIndex = 58;
            this.TargetLabel.Text = "Target";
            // 
            // StartBoxLabel
            // 
            this.StartBoxLabel.AutoSize = true;
            this.StartBoxLabel.ForeColor = System.Drawing.Color.White;
            this.StartBoxLabel.Location = new System.Drawing.Point(58, 425);
            this.StartBoxLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.StartBoxLabel.Name = "StartBoxLabel";
            this.StartBoxLabel.Size = new System.Drawing.Size(63, 25);
            this.StartBoxLabel.TabIndex = 57;
            this.StartBoxLabel.Text = "Start:";
            // 
            // Done
            // 
            this.Done.AutoSize = true;
            this.Done.ForeColor = System.Drawing.Color.White;
            this.Done.Location = new System.Drawing.Point(108, 579);
            this.Done.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Done.Name = "Done";
            this.Done.Size = new System.Drawing.Size(69, 25);
            this.Done.TabIndex = 55;
            this.Done.Text = "Done:";
            // 
            // FlipLabel
            // 
            this.FlipLabel.AutoSize = true;
            this.FlipLabel.ForeColor = System.Drawing.Color.White;
            this.FlipLabel.Location = new System.Drawing.Point(108, 529);
            this.FlipLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.FlipLabel.Name = "FlipLabel";
            this.FlipLabel.Size = new System.Drawing.Size(53, 25);
            this.FlipLabel.TabIndex = 54;
            this.FlipLabel.Text = "Flip:";
            // 
            // DoneTimeBox
            // 
            this.DoneTimeBox.Location = new System.Drawing.Point(194, 573);
            this.DoneTimeBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.DoneTimeBox.Name = "DoneTimeBox";
            this.DoneTimeBox.ReadOnly = true;
            this.DoneTimeBox.Size = new System.Drawing.Size(106, 31);
            this.DoneTimeBox.TabIndex = 53;
            // 
            // FlipTimeBox
            // 
            this.FlipTimeBox.Location = new System.Drawing.Point(194, 523);
            this.FlipTimeBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FlipTimeBox.Name = "FlipTimeBox";
            this.FlipTimeBox.ReadOnly = true;
            this.FlipTimeBox.Size = new System.Drawing.Size(106, 31);
            this.FlipTimeBox.TabIndex = 52;
            // 
            // TargetBox
            // 
            this.TargetBox.AcceptsReturn = true;
            this.TargetBox.BackColor = System.Drawing.SystemColors.Highlight;
            this.TargetBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TargetBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TargetBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TargetBox.ForeColor = System.Drawing.Color.LightPink;
            this.TargetBox.Location = new System.Drawing.Point(128, 31);
            this.TargetBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.TargetBox.Name = "TargetBox";
            this.TargetBox.ReadOnly = true;
            this.TargetBox.Size = new System.Drawing.Size(402, 44);
            this.TargetBox.TabIndex = 51;
            this.TargetBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TargetBox.TextChanged += new System.EventHandler(this.TargetBox_TextChanged);
            // 
            // DelayLabel
            // 
            this.DelayLabel.AutoSize = true;
            this.DelayLabel.ForeColor = System.Drawing.Color.White;
            this.DelayLabel.Location = new System.Drawing.Point(58, 252);
            this.DelayLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.DelayLabel.Name = "DelayLabel";
            this.DelayLabel.Size = new System.Drawing.Size(121, 25);
            this.DelayLabel.TabIndex = 50;
            this.DelayLabel.Text = "Delay (sec)";
            // 
            // DelayVal
            // 
            this.DelayVal.Location = new System.Drawing.Point(192, 248);
            this.DelayVal.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.DelayVal.Name = "DelayVal";
            this.DelayVal.Size = new System.Drawing.Size(88, 31);
            this.DelayVal.TabIndex = 49;
            this.DelayVal.ValueChanged += new System.EventHandler(this.DelayVal_ValueChanged);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(64, 687);
            this.ProgressBar.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(448, 40);
            this.ProgressBar.Step = 1;
            this.ProgressBar.TabIndex = 84;
            // 
            // StartTimeBox
            // 
            this.StartTimeBox.CustomFormat = "MMM dd @ HH:mm";
            this.StartTimeBox.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartTimeBox.Location = new System.Drawing.Point(146, 417);
            this.StartTimeBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.StartTimeBox.Name = "StartTimeBox";
            this.StartTimeBox.Size = new System.Drawing.Size(356, 31);
            this.StartTimeBox.TabIndex = 85;
            this.StartTimeBox.ValueChanged += new System.EventHandler(this.StartTimeBox_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(316, 102);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 25);
            this.label5.TabIndex = 88;
            this.label5.Text = "RA:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(316, 152);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 25);
            this.label6.TabIndex = 90;
            this.label6.Text = "Dec";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(316, 202);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 25);
            this.label7.TabIndex = 92;
            this.label7.Text = "PA";
            // 
            // TargetRABox
            // 
            this.TargetRABox.DecimalPlaces = 5;
            this.TargetRABox.Location = new System.Drawing.Point(376, 98);
            this.TargetRABox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.TargetRABox.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.TargetRABox.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.TargetRABox.Name = "TargetRABox";
            this.TargetRABox.ReadOnly = true;
            this.TargetRABox.Size = new System.Drawing.Size(134, 31);
            this.TargetRABox.TabIndex = 94;
            this.TargetRABox.ValueChanged += new System.EventHandler(this.TargetRABox_ValueChanged);
            // 
            // TargetDecBox
            // 
            this.TargetDecBox.DecimalPlaces = 5;
            this.TargetDecBox.Location = new System.Drawing.Point(374, 148);
            this.TargetDecBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.TargetDecBox.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.TargetDecBox.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.TargetDecBox.Name = "TargetDecBox";
            this.TargetDecBox.ReadOnly = true;
            this.TargetDecBox.Size = new System.Drawing.Size(134, 31);
            this.TargetDecBox.TabIndex = 95;
            this.TargetDecBox.ValueChanged += new System.EventHandler(this.TargetDecBox_ValueChanged);
            // 
            // TargetPABox
            // 
            this.TargetPABox.DecimalPlaces = 5;
            this.TargetPABox.Location = new System.Drawing.Point(372, 198);
            this.TargetPABox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.TargetPABox.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.TargetPABox.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.TargetPABox.Name = "TargetPABox";
            this.TargetPABox.ReadOnly = true;
            this.TargetPABox.Size = new System.Drawing.Size(134, 31);
            this.TargetPABox.TabIndex = 96;
            this.TargetPABox.ValueChanged += new System.EventHandler(this.TargetPABox_ValueChanged);
            // 
            // MakeFlatsCheckBox
            // 
            this.MakeFlatsCheckBox.AutoSize = true;
            this.MakeFlatsCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.MakeFlatsCheckBox.ForeColor = System.Drawing.Color.White;
            this.MakeFlatsCheckBox.Location = new System.Drawing.Point(54, 327);
            this.MakeFlatsCheckBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MakeFlatsCheckBox.Name = "MakeFlatsCheckBox";
            this.MakeFlatsCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MakeFlatsCheckBox.Size = new System.Drawing.Size(145, 29);
            this.MakeFlatsCheckBox.TabIndex = 97;
            this.MakeFlatsCheckBox.Text = "Take Flats";
            this.MakeFlatsCheckBox.UseVisualStyleBackColor = true;
            this.MakeFlatsCheckBox.CheckedChanged += new System.EventHandler(this.MakeFlatsCheckBox_CheckedChanged);
            // 
            // DawnTimeBox
            // 
            this.DawnTimeBox.CustomFormat = "HH:mm";
            this.DawnTimeBox.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DawnTimeBox.Location = new System.Drawing.Point(402, 573);
            this.DawnTimeBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.DawnTimeBox.Name = "DawnTimeBox";
            this.DawnTimeBox.Size = new System.Drawing.Size(104, 31);
            this.DawnTimeBox.TabIndex = 98;
            this.DawnTimeBox.ValueChanged += new System.EventHandler(this.DawnTimeBox_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(48, 733);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 25);
            this.label8.TabIndex = 99;
            this.label8.Text = "Progress";
            // 
            // SaveDefaultButton
            // 
            this.SaveDefaultButton.ForeColor = System.Drawing.Color.Black;
            this.SaveDefaultButton.Location = new System.Drawing.Point(320, 319);
            this.SaveDefaultButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SaveDefaultButton.Name = "SaveDefaultButton";
            this.SaveDefaultButton.Size = new System.Drawing.Size(190, 44);
            this.SaveDefaultButton.TabIndex = 100;
            this.SaveDefaultButton.Text = "Save As Default";
            this.SaveDefaultButton.UseVisualStyleBackColor = true;
            this.SaveDefaultButton.Click += new System.EventHandler(this.SaveDefaultButton_Click);
            // 
            // AdjustedTargetLabel
            // 
            this.AdjustedTargetLabel.AutoSize = true;
            this.AdjustedTargetLabel.ForeColor = System.Drawing.Color.Gold;
            this.AdjustedTargetLabel.Location = new System.Drawing.Point(392, 252);
            this.AdjustedTargetLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.AdjustedTargetLabel.Name = "AdjustedTargetLabel";
            this.AdjustedTargetLabel.Size = new System.Drawing.Size(96, 25);
            this.AdjustedTargetLabel.TabIndex = 102;
            this.AdjustedTargetLabel.Text = "Adjusted";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(240, 338);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 25);
            this.label2.TabIndex = 104;
            // 
            // RiseLimitBox
            // 
            this.RiseLimitBox.Location = new System.Drawing.Point(196, 473);
            this.RiseLimitBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.RiseLimitBox.Name = "RiseLimitBox";
            this.RiseLimitBox.ReadOnly = true;
            this.RiseLimitBox.Size = new System.Drawing.Size(104, 31);
            this.RiseLimitBox.TabIndex = 103;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(316, 479);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 25);
            this.label9.TabIndex = 105;
            this.label9.Text = "< ---- >";
            // 
            // PauseBox
            // 
            this.PauseBox.AutoSize = true;
            this.PauseBox.Location = new System.Drawing.Point(128, 781);
            this.PauseBox.Name = "PauseBox";
            this.PauseBox.Size = new System.Drawing.Size(288, 29);
            this.PauseBox.TabIndex = 106;
            this.PauseBox.Text = "Pause Before Next Image";
            this.PauseBox.UseVisualStyleBackColor = true;
            this.PauseBox.CheckedChanged += new System.EventHandler(this.PauseBox_CheckedChanged);
            // 
            // FormTarget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(556, 856);
            this.Controls.Add(this.PauseBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RiseLimitBox);
            this.Controls.Add(this.AdjustedTargetLabel);
            this.Controls.Add(this.SaveDefaultButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.DawnTimeBox);
            this.Controls.Add(this.MakeFlatsCheckBox);
            this.Controls.Add(this.TargetPABox);
            this.Controls.Add(this.TargetDecBox);
            this.Controls.Add(this.TargetRABox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.StartTimeBox);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.LRGBRatioBox);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.ImgAnlzbutton);
            this.Controls.Add(this.SnapShotButton);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.LimitLabel);
            this.Controls.Add(this.SetLimitBox);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.TransitBox);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.LoopsVal);
            this.Controls.Add(this.LoopsLabel);
            this.Controls.Add(this.ExposureVal);
            this.Controls.Add(this.ExposureLabel);
            this.Controls.Add(this.TargetLabel);
            this.Controls.Add(this.StartBoxLabel);
            this.Controls.Add(this.Done);
            this.Controls.Add(this.FlipLabel);
            this.Controls.Add(this.DoneTimeBox);
            this.Controls.Add(this.FlipTimeBox);
            this.Controls.Add(this.TargetBox);
            this.Controls.Add(this.DelayLabel);
            this.Controls.Add(this.DelayVal);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "FormTarget";
            this.Text = "SequenceBuilderForm";
            this.DoubleClick += new System.EventHandler(this.StartTimeBox_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.LRGBRatioBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoopsVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExposureVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DelayVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetRABox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetDecBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetPABox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.NumericUpDown LRGBRatioBox;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Button ImgAnlzbutton;
        internal System.Windows.Forms.Button SnapShotButton;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label LimitLabel;
        internal System.Windows.Forms.TextBox SetLimitBox;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox TransitBox;
        internal System.Windows.Forms.Button UpdateButton;
        internal System.Windows.Forms.NumericUpDown LoopsVal;
        internal System.Windows.Forms.Label LoopsLabel;
        internal System.Windows.Forms.NumericUpDown ExposureVal;
        internal System.Windows.Forms.Label ExposureLabel;
        internal System.Windows.Forms.Label TargetLabel;
        internal System.Windows.Forms.Label StartBoxLabel;
        internal System.Windows.Forms.Label Done;
        internal System.Windows.Forms.Label FlipLabel;
        internal System.Windows.Forms.TextBox DoneTimeBox;
        internal System.Windows.Forms.TextBox FlipTimeBox;
        internal System.Windows.Forms.TextBox TargetBox;
        internal System.Windows.Forms.Label DelayLabel;
        internal System.Windows.Forms.NumericUpDown DelayVal;
        internal System.Windows.Forms.ProgressBar ProgressBar;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.CheckBox MakeFlatsCheckBox;
        internal System.Windows.Forms.NumericUpDown TargetRABox;
        internal System.Windows.Forms.NumericUpDown TargetDecBox;
        internal System.Windows.Forms.NumericUpDown TargetPABox;
        public System.Windows.Forms.DateTimePicker StartTimeBox;
        public System.Windows.Forms.DateTimePicker DawnTimeBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button SaveDefaultButton;
        private System.Windows.Forms.Label AdjustedTargetLabel;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox RiseLimitBox;
        internal System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox PauseBox;
    }
}