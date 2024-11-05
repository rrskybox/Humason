namespace Humason
{
    partial class FormPlan
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
            this.AcquireButton = new System.Windows.Forms.Button();
            this.SelectButton = new System.Windows.Forms.Button();
            this.PlanTargetBox = new System.Windows.Forms.TextBox();
            this.AdjustButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MosaicButton = new System.Windows.Forms.Button();
            this.LoadPlanButton = new System.Windows.Forms.Button();
            this.PlanListBox = new System.Windows.Forms.ListBox();
            this.DeletePlanButton = new System.Windows.Forms.Button();
            this.ScheduleListBox = new System.Windows.Forms.ListBox();
            this.SchedulePlanButton = new System.Windows.Forms.Button();
            this.UnschedulePlanButton = new System.Windows.Forms.Button();
            this.DownScheduleButton = new System.Windows.Forms.Button();
            this.UpScheduleButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ImageForecastButton = new System.Windows.Forms.Button();
            this.SolarSystemBodyCheckBox = new System.Windows.Forms.CheckBox();
            this.ReductionFramesButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // AcquireButton
            // 
            this.AcquireButton.ForeColor = System.Drawing.Color.Black;
            this.AcquireButton.Location = new System.Drawing.Point(170, 36);
            this.AcquireButton.Name = "AcquireButton";
            this.AcquireButton.Size = new System.Drawing.Size(79, 23);
            this.AcquireButton.TabIndex = 0;
            this.AcquireButton.Text = "Read In";
            this.AcquireButton.UseVisualStyleBackColor = true;
            this.AcquireButton.Click += new System.EventHandler(this.FromTSXButton_Click);
            // 
            // SelectButton
            // 
            this.SelectButton.ForeColor = System.Drawing.Color.Black;
            this.SelectButton.Location = new System.Drawing.Point(170, 9);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(79, 23);
            this.SelectButton.TabIndex = 1;
            this.SelectButton.Text = "Look Up";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.ToTSXButton_Click);
            // 
            // PlanTargetBox
            // 
            this.PlanTargetBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.PlanTargetBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlanTargetBox.Location = new System.Drawing.Point(12, 36);
            this.PlanTargetBox.Name = "PlanTargetBox";
            this.PlanTargetBox.Size = new System.Drawing.Size(137, 22);
            this.PlanTargetBox.TabIndex = 2;
            this.PlanTargetBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PlanTargetBox.TextChanged += new System.EventHandler(this.PlanTargetBox_TextChanged);
            // 
            // AdjustButton
            // 
            this.AdjustButton.ForeColor = System.Drawing.Color.Black;
            this.AdjustButton.Location = new System.Drawing.Point(27, 104);
            this.AdjustButton.Name = "AdjustButton";
            this.AdjustButton.Size = new System.Drawing.Size(79, 38);
            this.AdjustButton.TabIndex = 3;
            this.AdjustButton.Text = "Adjust Target";
            this.AdjustButton.UseVisualStyleBackColor = true;
            this.AdjustButton.Click += new System.EventHandler(this.AdjustButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Plan Target";
            // 
            // MosaicButton
            // 
            this.MosaicButton.ForeColor = System.Drawing.Color.Black;
            this.MosaicButton.Location = new System.Drawing.Point(170, 65);
            this.MosaicButton.Name = "MosaicButton";
            this.MosaicButton.Size = new System.Drawing.Size(79, 22);
            this.MosaicButton.TabIndex = 6;
            this.MosaicButton.Text = "Build Mosaic";
            this.MosaicButton.UseVisualStyleBackColor = true;
            this.MosaicButton.Click += new System.EventHandler(this.MosaicButton_Click);
            // 
            // LoadPlanButton
            // 
            this.LoadPlanButton.ForeColor = System.Drawing.Color.Black;
            this.LoadPlanButton.Location = new System.Drawing.Point(27, 148);
            this.LoadPlanButton.Name = "LoadPlanButton";
            this.LoadPlanButton.Size = new System.Drawing.Size(79, 36);
            this.LoadPlanButton.TabIndex = 7;
            this.LoadPlanButton.Text = "Load Target";
            this.LoadPlanButton.UseVisualStyleBackColor = true;
            this.LoadPlanButton.Click += new System.EventHandler(this.LoadPlanButton_Click);
            // 
            // PlanListBox
            // 
            this.PlanListBox.FormattingEnabled = true;
            this.PlanListBox.Location = new System.Drawing.Point(6, 45);
            this.PlanListBox.Name = "PlanListBox";
            this.PlanListBox.Size = new System.Drawing.Size(100, 134);
            this.PlanListBox.TabIndex = 9;
            this.PlanListBox.DoubleClick += new System.EventHandler(this.PlanListBox_DoubleClick);
            // 
            // DeletePlanButton
            // 
            this.DeletePlanButton.ForeColor = System.Drawing.Color.Black;
            this.DeletePlanButton.Location = new System.Drawing.Point(42, 16);
            this.DeletePlanButton.Name = "DeletePlanButton";
            this.DeletePlanButton.Size = new System.Drawing.Size(30, 23);
            this.DeletePlanButton.TabIndex = 10;
            this.DeletePlanButton.Text = "X";
            this.DeletePlanButton.UseVisualStyleBackColor = true;
            this.DeletePlanButton.Click += new System.EventHandler(this.DeletePlanButton_Click);
            // 
            // ScheduleListBox
            // 
            this.ScheduleListBox.FormattingEnabled = true;
            this.ScheduleListBox.Location = new System.Drawing.Point(6, 45);
            this.ScheduleListBox.Name = "ScheduleListBox";
            this.ScheduleListBox.Size = new System.Drawing.Size(100, 134);
            this.ScheduleListBox.TabIndex = 11;
            // 
            // SchedulePlanButton
            // 
            this.SchedulePlanButton.ForeColor = System.Drawing.Color.Black;
            this.SchedulePlanButton.Location = new System.Drawing.Point(78, 16);
            this.SchedulePlanButton.Name = "SchedulePlanButton";
            this.SchedulePlanButton.Size = new System.Drawing.Size(28, 23);
            this.SchedulePlanButton.TabIndex = 12;
            this.SchedulePlanButton.Text = ">>";
            this.SchedulePlanButton.UseVisualStyleBackColor = true;
            this.SchedulePlanButton.Click += new System.EventHandler(this.SchedulePlanButton_Click);
            // 
            // UnschedulePlanButton
            // 
            this.UnschedulePlanButton.ForeColor = System.Drawing.Color.Black;
            this.UnschedulePlanButton.Location = new System.Drawing.Point(6, 16);
            this.UnschedulePlanButton.Name = "UnschedulePlanButton";
            this.UnschedulePlanButton.Size = new System.Drawing.Size(28, 23);
            this.UnschedulePlanButton.TabIndex = 13;
            this.UnschedulePlanButton.Text = "<<";
            this.UnschedulePlanButton.UseVisualStyleBackColor = true;
            this.UnschedulePlanButton.Click += new System.EventHandler(this.UnschedulePlanButton_Click);
            // 
            // DownScheduleButton
            // 
            this.DownScheduleButton.ForeColor = System.Drawing.Color.Black;
            this.DownScheduleButton.Location = new System.Drawing.Point(44, 16);
            this.DownScheduleButton.Name = "DownScheduleButton";
            this.DownScheduleButton.Size = new System.Drawing.Size(28, 23);
            this.DownScheduleButton.TabIndex = 14;
            this.DownScheduleButton.Text = "\\/";
            this.DownScheduleButton.UseVisualStyleBackColor = true;
            this.DownScheduleButton.Click += new System.EventHandler(this.DownScheduleButton_Click);
            // 
            // UpScheduleButton
            // 
            this.UpScheduleButton.ForeColor = System.Drawing.Color.Black;
            this.UpScheduleButton.Location = new System.Drawing.Point(78, 16);
            this.UpScheduleButton.Name = "UpScheduleButton";
            this.UpScheduleButton.Size = new System.Drawing.Size(28, 23);
            this.UpScheduleButton.TabIndex = 15;
            this.UpScheduleButton.Text = "/\\";
            this.UpScheduleButton.UseVisualStyleBackColor = true;
            this.UpScheduleButton.Click += new System.EventHandler(this.UpScheduleButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RefreshButton);
            this.groupBox1.Controls.Add(this.DeletePlanButton);
            this.groupBox1.Controls.Add(this.SchedulePlanButton);
            this.groupBox1.Controls.Add(this.PlanListBox);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(11, 205);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(116, 185);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plans";
            // 
            // RefreshButton
            // 
            this.RefreshButton.ForeColor = System.Drawing.Color.Black;
            this.RefreshButton.Location = new System.Drawing.Point(6, 16);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(30, 23);
            this.RefreshButton.TabIndex = 13;
            this.RefreshButton.Text = "?";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.UpScheduleButton);
            this.groupBox2.Controls.Add(this.ScheduleListBox);
            this.groupBox2.Controls.Add(this.DownScheduleButton);
            this.groupBox2.Controls.Add(this.UnschedulePlanButton);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(150, 205);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(116, 185);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Schedule";
            // 
            // ImageForecastButton
            // 
            this.ImageForecastButton.ForeColor = System.Drawing.Color.Black;
            this.ImageForecastButton.Location = new System.Drawing.Point(170, 104);
            this.ImageForecastButton.Name = "ImageForecastButton";
            this.ImageForecastButton.Size = new System.Drawing.Size(79, 38);
            this.ImageForecastButton.TabIndex = 18;
            this.ImageForecastButton.Text = "Image Forecast";
            this.ImageForecastButton.UseVisualStyleBackColor = true;
            this.ImageForecastButton.Click += new System.EventHandler(this.ImageForecastButton_Click);
            // 
            // SolarSystemBodyCheckBox
            // 
            this.SolarSystemBodyCheckBox.AutoSize = true;
            this.SolarSystemBodyCheckBox.ForeColor = System.Drawing.Color.White;
            this.SolarSystemBodyCheckBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SolarSystemBodyCheckBox.Location = new System.Drawing.Point(7, 65);
            this.SolarSystemBodyCheckBox.Name = "SolarSystemBodyCheckBox";
            this.SolarSystemBodyCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SolarSystemBodyCheckBox.Size = new System.Drawing.Size(142, 17);
            this.SolarSystemBodyCheckBox.TabIndex = 102;
            this.SolarSystemBodyCheckBox.Text = "Small Solar System Body";
            this.SolarSystemBodyCheckBox.UseVisualStyleBackColor = true;
            this.SolarSystemBodyCheckBox.CheckedChanged += new System.EventHandler(this.SolarSystemBodyCheckBox_CheckedChanged);
            // 
            // ReductionFramesButton
            // 
            this.ReductionFramesButton.ForeColor = System.Drawing.Color.Black;
            this.ReductionFramesButton.Location = new System.Drawing.Point(170, 148);
            this.ReductionFramesButton.Name = "ReductionFramesButton";
            this.ReductionFramesButton.Size = new System.Drawing.Size(79, 38);
            this.ReductionFramesButton.TabIndex = 103;
            this.ReductionFramesButton.Text = "Reduction Frames";
            this.ReductionFramesButton.UseVisualStyleBackColor = true;
            this.ReductionFramesButton.Click += new System.EventHandler(this.ReductionFramesButton_Click);
            // 
            // FormPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(278, 445);
            this.Controls.Add(this.ReductionFramesButton);
            this.Controls.Add(this.SolarSystemBodyCheckBox);
            this.Controls.Add(this.ImageForecastButton);
            this.Controls.Add(this.LoadPlanButton);
            this.Controls.Add(this.MosaicButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AdjustButton);
            this.Controls.Add(this.PlanTargetBox);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.AcquireButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPlan";
            this.Text = "Form Plan";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AcquireButton;
        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.Button AdjustButton;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox PlanTargetBox;
        private System.Windows.Forms.Button MosaicButton;
        private System.Windows.Forms.Button LoadPlanButton;
        private System.Windows.Forms.ListBox PlanListBox;
        private System.Windows.Forms.Button DeletePlanButton;
        private System.Windows.Forms.Button SchedulePlanButton;
        private System.Windows.Forms.Button UnschedulePlanButton;
        private System.Windows.Forms.Button DownScheduleButton;
        private System.Windows.Forms.Button UpScheduleButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ImageForecastButton;
        private System.Windows.Forms.Button RefreshButton;
        public System.Windows.Forms.ListBox ScheduleListBox;
        internal System.Windows.Forms.CheckBox SolarSystemBodyCheckBox;
        private System.Windows.Forms.Button ReductionFramesButton;
    }
}