namespace Task2_var12
{
    partial class DrawBezierCurveForm
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
            this.pictureBoxBezier = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBezier)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxBezier
            // 
            this.pictureBoxBezier.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxBezier.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxBezier.Name = "pictureBoxBezier";
            this.pictureBoxBezier.Size = new System.Drawing.Size(729, 455);
            this.pictureBoxBezier.TabIndex = 0;
            this.pictureBoxBezier.TabStop = false;
            this.pictureBoxBezier.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxBezier_Paint);
            this.pictureBoxBezier.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxBezier_MouseClick);
            // 
            // DrawBezierCurveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(753, 479);
            this.Controls.Add(this.pictureBoxBezier);
            this.Name = "DrawBezierCurveForm";
            this.Text = "Криві Без\'є";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBezier)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBezier;
    }
}

