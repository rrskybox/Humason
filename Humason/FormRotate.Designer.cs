namespace Humason
{
    partial class FormRotate
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
            this.PlateSolveButton = new System.Windows.Forms.Button();
            this.PlateSolveExposure = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.ImagePABox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RotatorPABox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.FOVPABox = new System.Windows.Forms.TextBox();
            this.InitializeButton = new System.Windows.Forms.Button();
            this.RotatorOffsetBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CheckButton = new System.Windows.Forms.Button();
            this.TargetButton = new System.Windows.Forms.Button();
            this.RotatorDirectionBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MoveToRPANum = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.RotateToIPAButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.MoveToIPANum = new System.Windows.Forms.NumericUpDown();
            this.RotateToRPAButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PlateSolveExposure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoveToRPANum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoveToIPANum)).BeginInit();
            this.SuspendLayout();
            // 
            // PlateSolveButton
            // 
            this.PlateSolveButton.ForeColor = System.Drawing.Color.Black;
            this.PlateSolveButton.Location = new System.Drawing.Point(28, 35);
            this.PlateSolveButton.Name = "PlateSolveButton";
            this.PlateSolveButton.Size = new System.Drawing.Size(75, 23);
            this.PlateSolveButton.TabIndex = 0;
            this.PlateSolveButton.Text = "Plate Solve";
            this.PlateSolveButton.UseVisualStyleBackColor = true;
            this.PlateSolveButton.Click += new System.EventHandler(this.PlateSolveButton_Click);
            // 
            // PlateSolveExposure
            // 
            this.PlateSolveExposure.Location = new System.Drawing.Point(198, 38);
            this.PlateSolveExposure.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.PlateSolveExposure.Name = "PlateSolveExposure";
            this.PlateSolveExposure.Size = new System.Drawing.Size(35, 20);
            this.PlateSolveExposure.TabIndex = 1;
            this.PlateSolveExposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PlateSolveExposure.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.PlateSolveExposure.ValueChanged += new System.EventHandler(this.PlateSolveExposure_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Exposure (sec)";
            // 
            // ImagePABox
            // 
            this.ImagePABox.Location = new System.Drawing.Point(145, 64);
            this.ImagePABox.Name = "ImagePABox";
            this.ImagePABox.Size = new System.Drawing.Size(88, 20);
            this.ImagePABox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Image PA";
            // 
            // RotatorPABox
            // 
            this.RotatorPABox.Location = new System.Drawing.Point(145, 90);
            this.RotatorPABox.Name = "RotatorPABox";
            this.RotatorPABox.Size = new System.Drawing.Size(88, 20);
            this.RotatorPABox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Rotator PA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "FOVI PA";
            // 
            // FOVPABox
            // 
            this.FOVPABox.Location = new System.Drawing.Point(145, 116);
            this.FOVPABox.Name = "FOVPABox";
            this.FOVPABox.Size = new System.Drawing.Size(88, 20);
            this.FOVPABox.TabIndex = 9;
            // 
            // InitializeButton
            // 
            this.InitializeButton.ForeColor = System.Drawing.Color.Black;
            this.InitializeButton.Location = new System.Drawing.Point(28, 215);
            this.InitializeButton.Name = "InitializeButton";
            this.InitializeButton.Size = new System.Drawing.Size(75, 23);
            this.InitializeButton.TabIndex = 10;
            this.InitializeButton.Text = "Initialize";
            this.InitializeButton.UseVisualStyleBackColor = true;
            this.InitializeButton.Click += new System.EventHandler(this.InitializeButton_Click);
            // 
            // RotatorOffsetBox
            // 
            this.RotatorOffsetBox.Location = new System.Drawing.Point(145, 142);
            this.RotatorOffsetBox.Name = "RotatorOffsetBox";
            this.RotatorOffsetBox.Size = new System.Drawing.Size(88, 20);
            this.RotatorOffsetBox.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Rotator Offset";
            // 
            // CheckButton
            // 
            this.CheckButton.ForeColor = System.Drawing.Color.Black;
            this.CheckButton.Location = new System.Drawing.Point(158, 215);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(75, 23);
            this.CheckButton.TabIndex = 13;
            this.CheckButton.Text = "Check";
            this.CheckButton.UseVisualStyleBackColor = true;
            this.CheckButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // TargetButton
            // 
            this.TargetButton.ForeColor = System.Drawing.Color.Black;
            this.TargetButton.Location = new System.Drawing.Point(28, 244);
            this.TargetButton.Name = "TargetButton";
            this.TargetButton.Size = new System.Drawing.Size(75, 23);
            this.TargetButton.TabIndex = 14;
            this.TargetButton.Text = "Target";
            this.TargetButton.UseVisualStyleBackColor = true;
            this.TargetButton.Click += new System.EventHandler(this.TargetButton_Click);
            // 
            // RotatorDirectionBox
            // 
            this.RotatorDirectionBox.Location = new System.Drawing.Point(145, 168);
            this.RotatorDirectionBox.Name = "RotatorDirectionBox";
            this.RotatorDirectionBox.Size = new System.Drawing.Size(88, 20);
            this.RotatorDirectionBox.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Rotation Direction";
            // 
            // MoveToRPANum
            // 
            this.MoveToRPANum.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MoveToRPANum.Location = new System.Drawing.Point(176, 278);
            this.MoveToRPANum.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.MoveToRPANum.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.MoveToRPANum.Name = "MoveToRPANum";
            this.MoveToRPANum.Size = new System.Drawing.Size(57, 20);
            this.MoveToRPANum.TabIndex = 17;
            this.MoveToRPANum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(111, 280);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Rotator PA";
            // 
            // RotateToIPAButton
            // 
            this.RotateToIPAButton.ForeColor = System.Drawing.Color.Black;
            this.RotateToIPAButton.Location = new System.Drawing.Point(26, 306);
            this.RotateToIPAButton.Name = "RotateToIPAButton";
            this.RotateToIPAButton.Size = new System.Drawing.Size(75, 23);
            this.RotateToIPAButton.TabIndex = 20;
            this.RotateToIPAButton.Text = "Rotate To";
            this.RotateToIPAButton.UseVisualStyleBackColor = true;
            this.RotateToIPAButton.Click += new System.EventHandler(this.RotateToIPAButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(111, 311);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Image PA";
            // 
            // MoveToIPANum
            // 
            this.MoveToIPANum.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MoveToIPANum.Location = new System.Drawing.Point(176, 309);
            this.MoveToIPANum.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.MoveToIPANum.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.MoveToIPANum.Name = "MoveToIPANum";
            this.MoveToIPANum.Size = new System.Drawing.Size(57, 20);
            this.MoveToIPANum.TabIndex = 21;
            this.MoveToIPANum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RotateToRPAButton
            // 
            this.RotateToRPAButton.ForeColor = System.Drawing.Color.Black;
            this.RotateToRPAButton.Location = new System.Drawing.Point(28, 275);
            this.RotateToRPAButton.Name = "RotateToRPAButton";
            this.RotateToRPAButton.Size = new System.Drawing.Size(75, 23);
            this.RotateToRPAButton.TabIndex = 23;
            this.RotateToRPAButton.Text = "Rotate To";
            this.RotateToRPAButton.UseVisualStyleBackColor = true;
            this.RotateToRPAButton.Click += new System.EventHandler(this.RotateToRPAButton_Click);
            // 
            // FormRotate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(278, 445);
            this.Controls.Add(this.RotateToRPAButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.MoveToIPANum);
            this.Controls.Add(this.RotateToIPAButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.MoveToRPANum);
            this.Controls.Add(this.RotatorDirectionBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TargetButton);
            this.Controls.Add(this.CheckButton);
            this.Controls.Add(this.RotatorOffsetBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.InitializeButton);
            this.Controls.Add(this.FOVPABox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RotatorPABox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ImagePABox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PlateSolveExposure);
            this.Controls.Add(this.PlateSolveButton);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRotate";
            this.Text = "Rotate";
            ((System.ComponentModel.ISupportInitialize)(this.PlateSolveExposure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoveToRPANum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoveToIPANum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PlateSolveButton;
        private System.Windows.Forms.NumericUpDown PlateSolveExposure;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ImagePABox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox RotatorPABox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox FOVPABox;
        private System.Windows.Forms.Button InitializeButton;
        private System.Windows.Forms.TextBox RotatorOffsetBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button CheckButton;
        private System.Windows.Forms.Button TargetButton;
        private System.Windows.Forms.TextBox RotatorDirectionBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown MoveToRPANum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button RotateToIPAButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown MoveToIPANum;
        private System.Windows.Forms.Button RotateToRPAButton;
    }
}