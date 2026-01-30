namespace Humason
{
    partial class FormHumason
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHumason));
            this.HumasonTabs = new System.Windows.Forms.TabControl();
            this.SessionTab = new System.Windows.Forms.TabPage();
            this.DevicesTab = new System.Windows.Forms.TabPage();
            this.ImageTab = new System.Windows.Forms.TabPage();
            this.PlanTab = new System.Windows.Forms.TabPage();
            this.TargetTab = new System.Windows.Forms.TabPage();
            this.FlatsTab = new System.Windows.Forms.TabPage();
            this.FocusTab = new System.Windows.Forms.TabPage();
            this.GuideTab = new System.Windows.Forms.TabPage();
            this.RotatorTab = new System.Windows.Forms.TabPage();
            this.DomeTab = new System.Windows.Forms.TabPage();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.OnTopCheck = new System.Windows.Forms.CheckBox();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.HomeMountCheckBox = new System.Windows.Forms.CheckBox();
            this.ParkMountCheckBox = new System.Windows.Forms.CheckBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.AbortButton = new System.Windows.Forms.Button();
            this.AboutButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.AttendedCheckBox = new System.Windows.Forms.CheckBox();
            this.StatusBox = new System.Windows.Forms.TextBox();
            this.HumasonTabs.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // HumasonTabs
            // 
            this.HumasonTabs.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.HumasonTabs.Controls.Add(this.SessionTab);
            this.HumasonTabs.Controls.Add(this.DevicesTab);
            this.HumasonTabs.Controls.Add(this.ImageTab);
            this.HumasonTabs.Controls.Add(this.PlanTab);
            this.HumasonTabs.Controls.Add(this.TargetTab);
            this.HumasonTabs.Controls.Add(this.FlatsTab);
            this.HumasonTabs.Controls.Add(this.FocusTab);
            this.HumasonTabs.Controls.Add(this.GuideTab);
            this.HumasonTabs.Controls.Add(this.RotatorTab);
            this.HumasonTabs.Controls.Add(this.DomeTab);
            this.HumasonTabs.HotTrack = true;
            this.HumasonTabs.Location = new System.Drawing.Point(0, 1);
            this.HumasonTabs.Multiline = true;
            this.HumasonTabs.Name = "HumasonTabs";
            this.HumasonTabs.SelectedIndex = 0;
            this.HumasonTabs.Size = new System.Drawing.Size(305, 453);
            this.HumasonTabs.TabIndex = 0;
            this.HumasonTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabPageSelected_Click);
            // 
            // SessionTab
            // 
            this.SessionTab.Location = new System.Drawing.Point(23, 4);
            this.SessionTab.Name = "SessionTab";
            this.SessionTab.Size = new System.Drawing.Size(278, 445);
            this.SessionTab.TabIndex = 9;
            this.SessionTab.Text = "Session";
            this.SessionTab.UseVisualStyleBackColor = true;
            // 
            // DevicesTab
            // 
            this.DevicesTab.Location = new System.Drawing.Point(23, 4);
            this.DevicesTab.Name = "DevicesTab";
            this.DevicesTab.Size = new System.Drawing.Size(278, 445);
            this.DevicesTab.TabIndex = 4;
            this.DevicesTab.Text = "Devices";
            this.DevicesTab.UseVisualStyleBackColor = true;
            // 
            // ImageTab
            // 
            this.ImageTab.Location = new System.Drawing.Point(23, 4);
            this.ImageTab.Name = "ImageTab";
            this.ImageTab.Size = new System.Drawing.Size(278, 445);
            this.ImageTab.TabIndex = 11;
            this.ImageTab.Text = "Image";
            this.ImageTab.UseVisualStyleBackColor = true;
            // 
            // PlanTab
            // 
            this.PlanTab.Location = new System.Drawing.Point(23, 4);
            this.PlanTab.Name = "PlanTab";
            this.PlanTab.Size = new System.Drawing.Size(278, 445);
            this.PlanTab.TabIndex = 6;
            this.PlanTab.Text = "Plan ";
            this.PlanTab.UseVisualStyleBackColor = true;
            // 
            // TargetTab
            // 
            this.TargetTab.Location = new System.Drawing.Point(23, 4);
            this.TargetTab.Name = "TargetTab";
            this.TargetTab.Size = new System.Drawing.Size(278, 445);
            this.TargetTab.TabIndex = 8;
            this.TargetTab.Text = "Target";
            this.TargetTab.UseVisualStyleBackColor = true;
            // 
            // FlatsTab
            // 
            this.FlatsTab.Location = new System.Drawing.Point(23, 4);
            this.FlatsTab.Name = "FlatsTab";
            this.FlatsTab.Size = new System.Drawing.Size(278, 445);
            this.FlatsTab.TabIndex = 0;
            this.FlatsTab.Text = "Flats";
            this.FlatsTab.UseVisualStyleBackColor = true;
            // 
            // FocusTab
            // 
            this.FocusTab.Location = new System.Drawing.Point(23, 4);
            this.FocusTab.Name = "FocusTab";
            this.FocusTab.Size = new System.Drawing.Size(278, 445);
            this.FocusTab.TabIndex = 1;
            this.FocusTab.Text = "Focus";
            this.FocusTab.UseVisualStyleBackColor = true;
            // 
            // GuideTab
            // 
            this.GuideTab.Location = new System.Drawing.Point(23, 4);
            this.GuideTab.Name = "GuideTab";
            this.GuideTab.Size = new System.Drawing.Size(278, 445);
            this.GuideTab.TabIndex = 2;
            this.GuideTab.Text = "Guide";
            this.GuideTab.UseVisualStyleBackColor = true;
            // 
            // RotatorTab
            // 
            this.RotatorTab.Location = new System.Drawing.Point(23, 4);
            this.RotatorTab.Name = "RotatorTab";
            this.RotatorTab.Size = new System.Drawing.Size(278, 445);
            this.RotatorTab.TabIndex = 5;
            this.RotatorTab.Text = "Rotate";
            this.RotatorTab.UseVisualStyleBackColor = true;
            // 
            // DomeTab
            // 
            this.DomeTab.Location = new System.Drawing.Point(23, 4);
            this.DomeTab.Name = "DomeTab";
            this.DomeTab.Size = new System.Drawing.Size(278, 445);
            this.DomeTab.TabIndex = 10;
            this.DomeTab.Text = "Dome";
            this.DomeTab.UseVisualStyleBackColor = true;
            // 
            // ConnectButton
            // 
            this.ConnectButton.ForeColor = System.Drawing.Color.Black;
            this.ConnectButton.Location = new System.Drawing.Point(6, 19);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(74, 23);
            this.ConnectButton.TabIndex = 6;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.ForeColor = System.Drawing.Color.Black;
            this.CloseButton.Location = new System.Drawing.Point(253, 549);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(48, 23);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // OnTopCheck
            // 
            this.OnTopCheck.AutoSize = true;
            this.OnTopCheck.Checked = true;
            this.OnTopCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OnTopCheck.ForeColor = System.Drawing.Color.White;
            this.OnTopCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OnTopCheck.Location = new System.Drawing.Point(78, 14);
            this.OnTopCheck.Name = "OnTopCheck";
            this.OnTopCheck.Size = new System.Drawing.Size(62, 17);
            this.OnTopCheck.TabIndex = 10;
            this.OnTopCheck.Text = "On Top";
            this.OnTopCheck.UseVisualStyleBackColor = true;
            this.OnTopCheck.CheckedChanged += new System.EventHandler(this.OnTopCheck_CheckedChanged);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.ForeColor = System.Drawing.Color.Black;
            this.DisconnectButton.Location = new System.Drawing.Point(6, 61);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(74, 23);
            this.DisconnectButton.TabIndex = 11;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // HomeMountCheckBox
            // 
            this.HomeMountCheckBox.AutoSize = true;
            this.HomeMountCheckBox.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.HomeMountCheckBox.ForeColor = System.Drawing.Color.White;
            this.HomeMountCheckBox.Location = new System.Drawing.Point(83, 16);
            this.HomeMountCheckBox.Name = "HomeMountCheckBox";
            this.HomeMountCheckBox.Size = new System.Drawing.Size(39, 31);
            this.HomeMountCheckBox.TabIndex = 12;
            this.HomeMountCheckBox.Text = "Home";
            this.HomeMountCheckBox.UseVisualStyleBackColor = true;
            this.HomeMountCheckBox.CheckedChanged += new System.EventHandler(this.HomeMountCheckBox_CheckedChanged);
            // 
            // ParkMountCheckBox
            // 
            this.ParkMountCheckBox.AutoSize = true;
            this.ParkMountCheckBox.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ParkMountCheckBox.ForeColor = System.Drawing.Color.White;
            this.ParkMountCheckBox.Location = new System.Drawing.Point(86, 53);
            this.ParkMountCheckBox.Name = "ParkMountCheckBox";
            this.ParkMountCheckBox.Size = new System.Drawing.Size(33, 31);
            this.ParkMountCheckBox.TabIndex = 13;
            this.ParkMountCheckBox.Text = "Park";
            this.ParkMountCheckBox.UseVisualStyleBackColor = true;
            this.ParkMountCheckBox.CheckedChanged += new System.EventHandler(this.ParkMountCheckBox_CheckedChanged);
            // 
            // StartButton
            // 
            this.StartButton.ForeColor = System.Drawing.Color.Black;
            this.StartButton.Location = new System.Drawing.Point(6, 19);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(66, 23);
            this.StartButton.TabIndex = 14;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // AbortButton
            // 
            this.AbortButton.ForeColor = System.Drawing.Color.Black;
            this.AbortButton.Location = new System.Drawing.Point(6, 53);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(66, 23);
            this.AbortButton.TabIndex = 15;
            this.AbortButton.Text = "Stop";
            this.AbortButton.UseVisualStyleBackColor = true;
            this.AbortButton.Click += new System.EventHandler(this.AbortButton_Click);
            // 
            // AboutButton
            // 
            this.AboutButton.BackColor = System.Drawing.Color.Transparent;
            this.AboutButton.ForeColor = System.Drawing.Color.Black;
            this.AboutButton.Location = new System.Drawing.Point(146, 549);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(49, 23);
            this.AboutButton.TabIndex = 17;
            this.AboutButton.Text = "About";
            this.AboutButton.UseVisualStyleBackColor = false;
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ParkMountCheckBox);
            this.groupBox1.Controls.Add(this.DisconnectButton);
            this.groupBox1.Controls.Add(this.ConnectButton);
            this.groupBox1.Controls.Add(this.HomeMountCheckBox);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 462);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(127, 100);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Manual";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.AttendedCheckBox);
            this.groupBox3.Controls.Add(this.AbortButton);
            this.groupBox3.Controls.Add(this.StartButton);
            this.groupBox3.Controls.Add(this.OnTopCheck);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(146, 462);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(153, 84);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Auto";
            // 
            // AttendedCheckBox
            // 
            this.AttendedCheckBox.AutoSize = true;
            this.AttendedCheckBox.ForeColor = System.Drawing.Color.White;
            this.AttendedCheckBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AttendedCheckBox.Location = new System.Drawing.Point(78, 33);
            this.AttendedCheckBox.Name = "AttendedCheckBox";
            this.AttendedCheckBox.Size = new System.Drawing.Size(69, 17);
            this.AttendedCheckBox.TabIndex = 17;
            this.AttendedCheckBox.Text = "Attended";
            this.AttendedCheckBox.UseVisualStyleBackColor = true;
            this.AttendedCheckBox.CheckedChanged += new System.EventHandler(this.AttendedCheckBox_CheckedChanged);
            // 
            // StatusBox
            // 
            this.StatusBox.Location = new System.Drawing.Point(12, 578);
            this.StatusBox.Multiline = true;
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StatusBox.Size = new System.Drawing.Size(281, 105);
            this.StatusBox.TabIndex = 21;
            // 
            // FormHumason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkCyan;
            this.ClientSize = new System.Drawing.Size(305, 690);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.HumasonTabs);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormHumason";
            this.Text = "Humason V";
            this.TransparencyKey = System.Drawing.Color.Red;
            this.HumasonTabs.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl HumasonTabs;
        private System.Windows.Forms.TabPage FlatsTab;
        private System.Windows.Forms.TabPage FocusTab;
        private System.Windows.Forms.TabPage GuideTab;
        internal System.Windows.Forms.Button ConnectButton;
        internal System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.TabPage DevicesTab;
        private System.Windows.Forms.TabPage RotatorTab;
        private System.Windows.Forms.TabPage PlanTab;
        private System.Windows.Forms.TabPage TargetTab;
        private System.Windows.Forms.CheckBox OnTopCheck;
        private System.Windows.Forms.TabPage SessionTab;
        internal System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.CheckBox HomeMountCheckBox;
        private System.Windows.Forms.CheckBox ParkMountCheckBox;
        internal System.Windows.Forms.Button StartButton;
        internal System.Windows.Forms.Button AbortButton;
        internal System.Windows.Forms.Button AboutButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabPage DomeTab;
        private System.Windows.Forms.TextBox StatusBox;
        private System.Windows.Forms.CheckBox AttendedCheckBox;
        private System.Windows.Forms.TabPage ImageTab;
    }
}

