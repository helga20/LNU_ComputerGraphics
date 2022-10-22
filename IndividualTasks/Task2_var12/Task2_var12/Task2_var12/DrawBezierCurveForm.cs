using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Task2_var12
{
    public partial class DrawBezierCurveForm : Form
    {
        public DrawBezierCurveForm()
        {
            InitializeComponent();
        }
        private List<PointF> Points = new List<PointF>();
        private void pictureBoxBezier_MouseClick(object sender, MouseEventArgs e)
        {
            if (Points.Count > 1 && Points.Count % 3 == 1)
            {
                Points.Add(new PointF
                {
                    X = 2 * Points[Points.Count - 1].X - Points[Points.Count - 2].X,
                    Y = 2 * Points[Points.Count - 1].Y - Points[Points.Count - 2].Y
                });
            }
            else
            {
                Points.Add(new PointF { X = e.X, Y = e.Y });
            }
            pictureBoxBezier.Refresh();
        }
        private void pictureBoxBezier_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(pictureBoxBezier.BackColor);
            if (Points.Count >= 4)
            {
                if (Points.Count % 3 == 1)
                {
                    using (Pen pen = new Pen(Color.Blue, 4))
                    {
                        e.Graphics.DrawBeziers(pen, Points.ToArray());
                    }
                }
                BezierCurve.DrawBezier(e.Graphics, Pens.DarkBlue, 0.01f, Points);
            }
            for (int i = 0; i < Points.Count; i++)
            {
                e.Graphics.FillEllipse(Brushes.Red, Points[i].X - 3, Points[i].Y - 3, 6, 6);
            }
        }
    }
}
