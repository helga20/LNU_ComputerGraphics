namespace FloatingHorizon
{
    partial class Form1
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
        /// Необхідний метод підтримки дизайнера - не змінюйте вміст 
        /// цього методу за допомогою редактора коду
        /// </summary> 

        private void InitializeComponent()
        {
            this.picBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.trackBarX = new System.Windows.Forms.TrackBar();
            this.trackBarZ = new System.Windows.Forms.TrackBar();
            this.trackBarY = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBoxFunctions = new System.Windows.Forms.ComboBox();
            this.txtBoxXBegin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxXEnd = new System.Windows.Forms.TextBox();
            this.txtBoxXStep = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBoxZStep = new System.Windows.Forms.TextBox();
            this.txtBoxZEnd = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBoxZBegin = new System.Windows.Forms.TextBox();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnMainColor = new System.Windows.Forms.Button();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.colorDlg = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarY)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBoxOptions.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBox.Location = new System.Drawing.Point(-1, 0);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(759, 641);
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            this.picBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseDown);
            this.picBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseMove);
            this.picBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseUp);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(10, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(206, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Draw";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // trackBarX
            // 
            this.trackBarX.Location = new System.Drawing.Point(6, 127);
            this.trackBarX.Maximum = 360;
            this.trackBarX.Name = "trackBarX";
            this.trackBarX.Size = new System.Drawing.Size(210, 45);
            this.trackBarX.SmallChange = 10;
            this.trackBarX.TabIndex = 2;
            this.trackBarX.Value = 30;
            this.trackBarX.ValueChanged += new System.EventHandler(this.trackBarX_ValueChanged);
            // 
            // trackBarZ
            // 
            this.trackBarZ.Enabled = true; // блокуємо рух бігунка
            this.trackBarZ.Location = new System.Drawing.Point(6, 229);
            this.trackBarZ.Maximum = 360;
            this.trackBarZ.Name = "trackBarZ";
            this.trackBarZ.Size = new System.Drawing.Size(210, 45);
            this.trackBarZ.TabIndex = 3;
            this.trackBarZ.ValueChanged += new System.EventHandler(this.trackBarZ_ValueChanged);
            // 
            // trackBarY
            // 
            this.trackBarY.Location = new System.Drawing.Point(6, 178);
            this.trackBarY.Maximum = 360;
            this.trackBarY.Name = "trackBarY";
            this.trackBarY.Size = new System.Drawing.Size(210, 45);
            this.trackBarY.SmallChange = 10;
            this.trackBarY.TabIndex = 4;
            this.trackBarY.Value = 15;
            this.trackBarY.ValueChanged += new System.EventHandler(this.trackBarY_ValueChanged);
            // 
            // groupBox1
            // 
           this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbBoxFunctions);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.trackBarY);
            this.groupBox1.Controls.Add(this.trackBarX);
            this.groupBox1.Controls.Add(this.trackBarZ);
            this.groupBox1.Location = new System.Drawing.Point(769, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 285);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Z Rotate:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Y Rotate:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "X Rotate:";
            // 
            // cmbBoxFunctions
            // 
            this.cmbBoxFunctions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxFunctions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBoxFunctions.FormattingEnabled = true;
            this.cmbBoxFunctions.Location = new System.Drawing.Point(10, 19);
            this.cmbBoxFunctions.Name = "cmbBoxFunctions";
            this.cmbBoxFunctions.Size = new System.Drawing.Size(206, 28);
            this.cmbBoxFunctions.TabIndex = 6;
            // 
            // txtBoxXBegin
            // 
            this.txtBoxXBegin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxXBegin.Location = new System.Drawing.Point(10, 48);
            this.txtBoxXBegin.Name = "txtBoxXBegin";
            this.txtBoxXBegin.Size = new System.Drawing.Size(60, 29);
            this.txtBoxXBegin.TabIndex = 6;
            this.txtBoxXBegin.Text = "-6";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(64, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "X:";
            // 
            // txtBoxXEnd
            // 
            this.txtBoxXEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxXEnd.Location = new System.Drawing.Point(96, 48);
            this.txtBoxXEnd.Name = "txtBoxXEnd";
            this.txtBoxXEnd.Size = new System.Drawing.Size(60, 29);
            this.txtBoxXEnd.TabIndex = 8;
            this.txtBoxXEnd.Text = "6";
            // 
            // txtBoxXStep
            // 
            this.txtBoxXStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxXStep.Location = new System.Drawing.Point(176, 48);
            this.txtBoxXStep.Name = "txtBoxXStep";
            this.txtBoxXStep.Size = new System.Drawing.Size(60, 29);
            this.txtBoxXStep.TabIndex = 9;
            this.txtBoxXStep.Text = "0.05";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(76, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(179, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Step:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(179, 91);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 20);
            this.label10.TabIndex = 23;
            this.label10.Text = "Step:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(76, 129);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 20);
            this.label11.TabIndex = 22;
            this.label11.Text = "-";
            // 
            // txtBoxZStep
            // 
            this.txtBoxZStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxZStep.Location = new System.Drawing.Point(176, 123);
            this.txtBoxZStep.Name = "txtBoxZStep";
            this.txtBoxZStep.Size = new System.Drawing.Size(60, 29);
            this.txtBoxZStep.TabIndex = 21;
            this.txtBoxZStep.Text = "0.2";
            // 
            // txtBoxZEnd
            // 
            this.txtBoxZEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxZEnd.Location = new System.Drawing.Point(96, 123);
            this.txtBoxZEnd.Name = "txtBoxZEnd";
            this.txtBoxZEnd.Size = new System.Drawing.Size(60, 29);
            this.txtBoxZEnd.TabIndex = 20;
            this.txtBoxZEnd.Text = "6";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(64, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 20);
            this.label12.TabIndex = 19;
            this.label12.Text = "Z:";
            // 
            // txtBoxZBegin
            // 
            this.txtBoxZBegin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxZBegin.Location = new System.Drawing.Point(10, 123);
            this.txtBoxZBegin.Name = "txtBoxZBegin";
            this.txtBoxZBegin.Size = new System.Drawing.Size(60, 29);
            this.txtBoxZBegin.TabIndex = 18;
            this.txtBoxZBegin.Text = "-6";
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOptions.Controls.Add(this.txtBoxXBegin);
            this.groupBoxOptions.Controls.Add(this.label10);
            this.groupBoxOptions.Controls.Add(this.label4);
            this.groupBoxOptions.Controls.Add(this.label11);
            this.groupBoxOptions.Controls.Add(this.txtBoxXEnd);
            this.groupBoxOptions.Controls.Add(this.txtBoxZStep);
            this.groupBoxOptions.Controls.Add(this.txtBoxXStep);
            this.groupBoxOptions.Controls.Add(this.txtBoxZEnd);
            this.groupBoxOptions.Controls.Add(this.label5);
            this.groupBoxOptions.Controls.Add(this.label12);
            this.groupBoxOptions.Controls.Add(this.label6);
            this.groupBoxOptions.Controls.Add(this.txtBoxZBegin);
            this.groupBoxOptions.Location = new System.Drawing.Point(769, 300);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(237, 229);
            this.groupBoxOptions.TabIndex = 24;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnMainColor);
            this.groupBox2.Controls.Add(this.btnBackColor);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(769, 535);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 114);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Colors";
            // 
            // btnMainColor
            // 
            this.btnMainColor.Location = new System.Drawing.Point(140, 72);
            this.btnMainColor.Name = "btnMainColor";
            this.btnMainColor.Size = new System.Drawing.Size(76, 34);
            this.btnMainColor.TabIndex = 4;
            this.btnMainColor.UseVisualStyleBackColor = true;
            this.btnMainColor.Click += new System.EventHandler(this.btnMainColor_Click);
            // 
            // btnBackColor
            // 
            this.btnBackColor.Location = new System.Drawing.Point(140, 23);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(76, 34);
            this.btnBackColor.TabIndex = 3;
            this.btnBackColor.UseVisualStyleBackColor = true;
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(14, 77);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 24);
            this.label13.TabIndex = 2;
            this.label13.Text = "Main:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(14, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 24);
            this.label7.TabIndex = 1;
            this.label7.Text = "Background:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 653);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.picBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = true; //
            this.Name = "Form1";
            this.Text = "Floating horizon method";
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarY)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar trackBarX;
        private System.Windows.Forms.TrackBar trackBarZ;
        private System.Windows.Forms.TrackBar trackBarY;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbBoxFunctions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxXBegin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxXEnd;
        private System.Windows.Forms.TextBox txtBoxXStep;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBoxZStep;
        private System.Windows.Forms.TextBox txtBoxZEnd;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBoxZBegin;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ColorDialog colorDlg;
        private System.Windows.Forms.Button btnMainColor;
        private System.Windows.Forms.Button btnBackColor;
    }
}

