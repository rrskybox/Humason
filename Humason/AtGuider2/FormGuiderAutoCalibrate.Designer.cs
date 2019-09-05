namespace AtGuider2
{
    partial class FormGuiderAutoCalibrate
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
            this.agcTextBox = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // agcTextBox
            // 
            this.agcTextBox.BackColor = System.Drawing.SystemColors.HotTrack;
            this.agcTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.agcTextBox.Location = new System.Drawing.Point(11, 11);
            this.agcTextBox.Multiline = true;
            this.agcTextBox.Name = "agcTextBox";
            this.agcTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.agcTextBox.Size = new System.Drawing.Size(283, 391);
            this.agcTextBox.TabIndex = 0;
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.CloseButton.Location = new System.Drawing.Point(114, 415);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(72, 28);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // FormGuiderAutoCalibrate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(307, 455);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.agcTextBox);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Name = "FormGuiderAutoCalibrate";
            this.Text = "Guider Auto Calibrate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox agcTextBox;
        private System.Windows.Forms.Button CloseButton;
    }
}