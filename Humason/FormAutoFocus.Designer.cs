namespace Humason
{
    partial class FormAutoFocus
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
            this.AtFocus2Button = new System.Windows.Forms.Button();
            this.Presetbutton = new System.Windows.Forms.Button();
            this.ExposureLabel = new System.Windows.Forms.Label();
            this.FilterFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.AtFocus3Button = new System.Windows.Forms.Button();
            this.FocusFilterBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FocusExposureBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.FocusExposureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AtFocus2Button
            // 
            this.AtFocus2Button.Location = new System.Drawing.Point(26, 83);
            this.AtFocus2Button.Name = "AtFocus2Button";
            this.AtFocus2Button.Size = new System.Drawing.Size(65, 41);
            this.AtFocus2Button.TabIndex = 12;
            this.AtFocus2Button.Text = "Run @Focus2";
            this.AtFocus2Button.UseVisualStyleBackColor = true;
            this.AtFocus2Button.Click += new System.EventHandler(this.AtFocus2Button_Click);
            // 
            // Presetbutton
            // 
            this.Presetbutton.Location = new System.Drawing.Point(27, 29);
            this.Presetbutton.Name = "Presetbutton";
            this.Presetbutton.Size = new System.Drawing.Size(65, 37);
            this.Presetbutton.TabIndex = 11;
            this.Presetbutton.Text = "Preset Focuser";
            this.Presetbutton.UseVisualStyleBackColor = true;
            this.Presetbutton.Click += new System.EventHandler(this.Presetbutton_Click);
            // 
            // ExposureLabel
            // 
            this.ExposureLabel.AutoSize = true;
            this.ExposureLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.ExposureLabel.Location = new System.Drawing.Point(126, 65);
            this.ExposureLabel.Name = "ExposureLabel";
            this.ExposureLabel.Size = new System.Drawing.Size(51, 13);
            this.ExposureLabel.TabIndex = 10;
            this.ExposureLabel.Text = "Exposure";
            // 
            // FilterFileDialog
            // 
            this.FilterFileDialog.FileName = "openFileDialog1";
            // 
            // AtFocus3Button
            // 
            this.AtFocus3Button.Location = new System.Drawing.Point(26, 143);
            this.AtFocus3Button.Name = "AtFocus3Button";
            this.AtFocus3Button.Size = new System.Drawing.Size(65, 41);
            this.AtFocus3Button.TabIndex = 92;
            this.AtFocus3Button.Text = "Run @Focus3";
            this.AtFocus3Button.UseVisualStyleBackColor = true;
            this.AtFocus3Button.Click += new System.EventHandler(this.AtFocus3Button_Click);
            // 
            // FocusFilterBox
            // 
            this.FocusFilterBox.BackColor = System.Drawing.SystemColors.HotTrack;
            this.FocusFilterBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FocusFilterBox.Location = new System.Drawing.Point(193, 29);
            this.FocusFilterBox.Name = "FocusFilterBox";
            this.FocusFilterBox.ReadOnly = true;
            this.FocusFilterBox.Size = new System.Drawing.Size(35, 20);
            this.FocusFilterBox.TabIndex = 93;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(126, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 94;
            this.label1.Text = "Focus Filter";
            // 
            // FocusExposureBox
            // 
            this.FocusExposureBox.DecimalPlaces = 1;
            this.FocusExposureBox.Location = new System.Drawing.Point(183, 63);
            this.FocusExposureBox.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.FocusExposureBox.Name = "FocusExposureBox";
            this.FocusExposureBox.Size = new System.Drawing.Size(45, 20);
            this.FocusExposureBox.TabIndex = 97;
            this.FocusExposureBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FocusExposureBox.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.FocusExposureBox.ValueChanged += new System.EventHandler(this.FocusExposureBox_ValueChanged);
            // 
            // FormAutoFocus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(278, 391);
            this.Controls.Add(this.FocusExposureBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FocusFilterBox);
            this.Controls.Add(this.AtFocus3Button);
            this.Controls.Add(this.AtFocus2Button);
            this.Controls.Add(this.Presetbutton);
            this.Controls.Add(this.ExposureLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAutoFocus";
            this.Text = "AutoFocus";
            ((System.ComponentModel.ISupportInitialize)(this.FocusExposureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Button AtFocus2Button;
        internal System.Windows.Forms.Button Presetbutton;
        internal System.Windows.Forms.Label ExposureLabel;
        private System.Windows.Forms.OpenFileDialog FilterFileDialog;
        internal System.Windows.Forms.Button AtFocus3Button;
        private System.Windows.Forms.TextBox FocusFilterBox;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown FocusExposureBox;
    }
}