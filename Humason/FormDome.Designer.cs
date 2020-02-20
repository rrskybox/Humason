namespace Humason
{
    partial class FormDome
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
            this.label1 = new System.Windows.Forms.Label();
            this.DomeHomeAz = new System.Windows.Forms.NumericUpDown();
            this.HomeDomeButton = new System.Windows.Forms.Button();
            this.OpenSlitButton = new System.Windows.Forms.Button();
            this.CloseSlitButton = new System.Windows.Forms.Button();
            this.GoToAzButton = new System.Windows.Forms.Button();
            this.TargetAz = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.DomeHomeAz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetAz)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 79;
            this.label1.Text = "Home Azimuth";
            // 
            // DomeHomeAz
            // 
            this.DomeHomeAz.Location = new System.Drawing.Point(93, 39);
            this.DomeHomeAz.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.DomeHomeAz.Name = "DomeHomeAz";
            this.DomeHomeAz.Size = new System.Drawing.Size(45, 20);
            this.DomeHomeAz.TabIndex = 78;
            this.DomeHomeAz.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DomeHomeAz.Value = new decimal(new int[] {
            220,
            0,
            0,
            0});
            this.DomeHomeAz.ValueChanged += new System.EventHandler(this.DomeHomeAz_ValueChanged);
            // 
            // HomeDomeButton
            // 
            this.HomeDomeButton.ForeColor = System.Drawing.Color.Black;
            this.HomeDomeButton.Location = new System.Drawing.Point(15, 106);
            this.HomeDomeButton.Name = "HomeDomeButton";
            this.HomeDomeButton.Size = new System.Drawing.Size(83, 25);
            this.HomeDomeButton.TabIndex = 81;
            this.HomeDomeButton.Text = "Home Dome";
            this.HomeDomeButton.UseVisualStyleBackColor = true;
            this.HomeDomeButton.Click += new System.EventHandler(this.HomeDomeButton_Click);
            // 
            // OpenSlitButton
            // 
            this.OpenSlitButton.ForeColor = System.Drawing.Color.Black;
            this.OpenSlitButton.Location = new System.Drawing.Point(15, 137);
            this.OpenSlitButton.Name = "OpenSlitButton";
            this.OpenSlitButton.Size = new System.Drawing.Size(83, 25);
            this.OpenSlitButton.TabIndex = 82;
            this.OpenSlitButton.Text = "Open Slit";
            this.OpenSlitButton.UseVisualStyleBackColor = true;
            this.OpenSlitButton.Click += new System.EventHandler(this.OpenSlitButton_Click);
            // 
            // CloseSlitButton
            // 
            this.CloseSlitButton.ForeColor = System.Drawing.Color.Black;
            this.CloseSlitButton.Location = new System.Drawing.Point(15, 168);
            this.CloseSlitButton.Name = "CloseSlitButton";
            this.CloseSlitButton.Size = new System.Drawing.Size(83, 25);
            this.CloseSlitButton.TabIndex = 83;
            this.CloseSlitButton.Text = "Close Slit";
            this.CloseSlitButton.UseVisualStyleBackColor = true;
            this.CloseSlitButton.Click += new System.EventHandler(this.CloseSlitButton_Click);
            // 
            // GoToAzButton
            // 
            this.GoToAzButton.ForeColor = System.Drawing.Color.Black;
            this.GoToAzButton.Location = new System.Drawing.Point(15, 222);
            this.GoToAzButton.Name = "GoToAzButton";
            this.GoToAzButton.Size = new System.Drawing.Size(75, 23);
            this.GoToAzButton.TabIndex = 84;
            this.GoToAzButton.Text = "Go To Az";
            this.GoToAzButton.UseVisualStyleBackColor = true;
            this.GoToAzButton.Click += new System.EventHandler(this.GoToAzButton_Click);
            // 
            // TargetAz
            // 
            this.TargetAz.Location = new System.Drawing.Point(112, 223);
            this.TargetAz.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.TargetAz.Name = "TargetAz";
            this.TargetAz.Size = new System.Drawing.Size(45, 20);
            this.TargetAz.TabIndex = 85;
            this.TargetAz.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormDome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(262, 363);
            this.Controls.Add(this.TargetAz);
            this.Controls.Add(this.GoToAzButton);
            this.Controls.Add(this.CloseSlitButton);
            this.Controls.Add(this.OpenSlitButton);
            this.Controls.Add(this.HomeDomeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DomeHomeAz);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDome";
            this.Text = "Form Dome";
            ((System.ComponentModel.ISupportInitialize)(this.DomeHomeAz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetAz)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown DomeHomeAz;
        internal System.Windows.Forms.Button HomeDomeButton;
        internal System.Windows.Forms.Button OpenSlitButton;
        internal System.Windows.Forms.Button CloseSlitButton;
        private System.Windows.Forms.Button GoToAzButton;
        private System.Windows.Forms.NumericUpDown TargetAz;
    }
}