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
            this.DomeAddOnCheckBox = new System.Windows.Forms.CheckBox();
            this.WeatherCheckBox = new System.Windows.Forms.CheckBox();
            this.WeatherFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(116, 498);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // RotatorCheckBox
            // 
            this.RotatorCheckBox.AutoSize = true;
            this.RotatorCheckBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.RotatorCheckBox.Location = new System.Drawing.Point(12, 17);
            this.RotatorCheckBox.Name = "RotatorCheckBox";
            this.RotatorCheckBox.Size = new System.Drawing.Size(83, 17);
            this.RotatorCheckBox.TabIndex = 2;
            this.RotatorCheckBox.Text = "Has Rotator";
            this.RotatorCheckBox.UseVisualStyleBackColor = true;
            this.RotatorCheckBox.CheckedChanged += new System.EventHandler(this.Rotator_CheckedChanged);
            // 
            // DomeAddOnCheckBox
            // 
            this.DomeAddOnCheckBox.AutoSize = true;
            this.DomeAddOnCheckBox.ForeColor = System.Drawing.SystemColors.Control;
            this.DomeAddOnCheckBox.Location = new System.Drawing.Point(12, 63);
            this.DomeAddOnCheckBox.Name = "DomeAddOnCheckBox";
            this.DomeAddOnCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DomeAddOnCheckBox.Size = new System.Drawing.Size(115, 17);
            this.DomeAddOnCheckBox.TabIndex = 74;
            this.DomeAddOnCheckBox.Text = "Has Dome Add-On";
            this.DomeAddOnCheckBox.UseVisualStyleBackColor = true;
            this.DomeAddOnCheckBox.CheckedChanged += new System.EventHandler(this.DomeAddOnCheckBox_CheckedChanged);
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
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(297, 544);
            this.Controls.Add(this.DomeAddOnCheckBox);
            this.Controls.Add(this.WeatherCheckBox);
            this.Controls.Add(this.RotatorCheckBox);
            this.Controls.Add(this.SaveButton);
            this.Name = "FormOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.CheckBox RotatorCheckBox;
        internal System.Windows.Forms.CheckBox DomeAddOnCheckBox;
        internal System.Windows.Forms.CheckBox WeatherCheckBox;
        private System.Windows.Forms.OpenFileDialog WeatherFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}