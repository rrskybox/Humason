namespace Humason
{
    partial class FormAutoRun
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
            this.StartUpFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ShutDownFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ShutDownBrowseButton = new System.Windows.Forms.Button();
            this.ShutDownFilePathBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ShutdownWaitCheckBox = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.StageSystemFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.StartupWaitCheckBox = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.StartUpFilePathBox = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.StagingWaitCheckBox = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.StageSystemFilePathBox = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // ShutDownBrowseButton
            // 
            this.ShutDownBrowseButton.ForeColor = System.Drawing.Color.Black;
            this.ShutDownBrowseButton.Location = new System.Drawing.Point(483, 16);
            this.ShutDownBrowseButton.Name = "ShutDownBrowseButton";
            this.ShutDownBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.ShutDownBrowseButton.TabIndex = 4;
            this.ShutDownBrowseButton.Text = "Browse";
            this.ShutDownBrowseButton.UseVisualStyleBackColor = true;
            this.ShutDownBrowseButton.Click += new System.EventHandler(this.ShutDownBrowseButton_Click);
            // 
            // ShutDownFilePathBox
            // 
            this.ShutDownFilePathBox.Location = new System.Drawing.Point(105, 19);
            this.ShutDownFilePathBox.Name = "ShutDownFilePathBox";
            this.ShutDownFilePathBox.Size = new System.Drawing.Size(372, 20);
            this.ShutDownFilePathBox.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.SkyBlue;
            this.groupBox2.Controls.Add(this.ShutdownWaitCheckBox);
            this.groupBox2.Controls.Add(this.ShutDownBrowseButton);
            this.groupBox2.Controls.Add(this.ShutDownFilePathBox);
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(568, 47);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Execute at Session End or Dawn";
            // 
            // ShutdownWaitCheckBox
            // 
            this.ShutdownWaitCheckBox.AutoSize = true;
            this.ShutdownWaitCheckBox.Checked = true;
            this.ShutdownWaitCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShutdownWaitCheckBox.Location = new System.Drawing.Point(19, 19);
            this.ShutdownWaitCheckBox.Name = "ShutdownWaitCheckBox";
            this.ShutdownWaitCheckBox.Size = new System.Drawing.Size(54, 17);
            this.ShutdownWaitCheckBox.TabIndex = 7;
            this.ShutdownWaitCheckBox.Text = "Wait?";
            this.ShutdownWaitCheckBox.UseVisualStyleBackColor = true;
            this.ShutdownWaitCheckBox.CheckedChanged += new System.EventHandler(this.ShutdownWaitCheckBox_CheckedChanged);
            // 
            // OKButton
            // 
            this.OKButton.BackColor = System.Drawing.Color.LightGreen;
            this.OKButton.Location = new System.Drawing.Point(264, 171);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = false;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.SkyBlue;
            this.groupBox5.Controls.Add(this.StartupWaitCheckBox);
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Controls.Add(this.StartUpFilePathBox);
            this.groupBox5.ForeColor = System.Drawing.Color.Black;
            this.groupBox5.Location = new System.Drawing.Point(12, 65);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(568, 47);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Execute at First Target Plan Starting Time";
            // 
            // StartupWaitCheckBox
            // 
            this.StartupWaitCheckBox.AutoSize = true;
            this.StartupWaitCheckBox.Checked = true;
            this.StartupWaitCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.StartupWaitCheckBox.Location = new System.Drawing.Point(19, 21);
            this.StartupWaitCheckBox.Name = "StartupWaitCheckBox";
            this.StartupWaitCheckBox.Size = new System.Drawing.Size(54, 17);
            this.StartupWaitCheckBox.TabIndex = 6;
            this.StartupWaitCheckBox.Text = "Wait?";
            this.StartupWaitCheckBox.UseVisualStyleBackColor = true;
            this.StartupWaitCheckBox.CheckedChanged += new System.EventHandler(this.StartupWaitCheckBox_CheckedChanged);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(487, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.StartUpBrowseButton_Click);
            // 
            // StartUpFilePathBox
            // 
            this.StartUpFilePathBox.Location = new System.Drawing.Point(105, 19);
            this.StartUpFilePathBox.Name = "StartUpFilePathBox";
            this.StartUpFilePathBox.Size = new System.Drawing.Size(376, 20);
            this.StartUpFilePathBox.TabIndex = 1;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.SkyBlue;
            this.groupBox6.Controls.Add(this.StagingWaitCheckBox);
            this.groupBox6.Controls.Add(this.button3);
            this.groupBox6.Controls.Add(this.StageSystemFilePathBox);
            this.groupBox6.ForeColor = System.Drawing.Color.Black;
            this.groupBox6.Location = new System.Drawing.Point(12, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(568, 47);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Execute at Session Start Up";
            // 
            // StagingWaitCheckBox
            // 
            this.StagingWaitCheckBox.AutoSize = true;
            this.StagingWaitCheckBox.Checked = true;
            this.StagingWaitCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.StagingWaitCheckBox.Location = new System.Drawing.Point(19, 21);
            this.StagingWaitCheckBox.Name = "StagingWaitCheckBox";
            this.StagingWaitCheckBox.Size = new System.Drawing.Size(54, 17);
            this.StagingWaitCheckBox.TabIndex = 5;
            this.StagingWaitCheckBox.Text = "Wait?";
            this.StagingWaitCheckBox.UseVisualStyleBackColor = true;
            this.StagingWaitCheckBox.CheckedChanged += new System.EventHandler(this.StagingWaitCheckBox_CheckedChanged);
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(483, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Browse";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.StageSystemBrowseButton_Click);
            // 
            // StageSystemFilePathBox
            // 
            this.StageSystemFilePathBox.AccessibleDescription = "";
            this.StageSystemFilePathBox.Location = new System.Drawing.Point(105, 19);
            this.StageSystemFilePathBox.Name = "StageSystemFilePathBox";
            this.StageSystemFilePathBox.Size = new System.Drawing.Size(372, 20);
            this.StageSystemFilePathBox.TabIndex = 3;
            // 
            // FormAutoRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(594, 200);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "FormAutoRun";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoRun Configuration";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog StartUpFileDialog;
        private System.Windows.Forms.OpenFileDialog ShutDownFileDialog;
        private System.Windows.Forms.Button ShutDownBrowseButton;
        private System.Windows.Forms.TextBox ShutDownFilePathBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.OpenFileDialog StageSystemFileDialog;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox StartUpFilePathBox;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox StageSystemFilePathBox;
        private System.Windows.Forms.CheckBox ShutdownWaitCheckBox;
        private System.Windows.Forms.CheckBox StartupWaitCheckBox;
        private System.Windows.Forms.CheckBox StagingWaitCheckBox;
    }
}