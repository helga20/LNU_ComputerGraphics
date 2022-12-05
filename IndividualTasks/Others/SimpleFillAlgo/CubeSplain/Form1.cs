using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace CubeSplain
{
    public partial class Form1 : Form
    {
        private static Point X0Y0;
        private static int numberOfPixels = 32;
        private static int scale = 40;
        private static Pen middle_pen;
        private static Pen pointPen;
        private static Brush verticesBrush;
        private static Brush sideBrush;
        private static Brush fillBrush;
        private static Graphics gfx;
        private static List<Point> setOfPoints;
        private static List<Point> points = new List<Point>();
        private static List<Point> points2 = new List<Point>();
        private static List<Point> borderPoints = new List<Point>();


        private static List<Point> pointsCustom = new List<Point>();
        private static List<Point> borderPointsCustom = new List<Point>();
        private static bool firstDone = false;
        KnownColor d = KnownColor.DarkCyan;

        public Form1()
        {
            InitializeComponent();
            gfx = panel1.CreateGraphics();
            setOfPoints = new List<Point>();
            pointPen = new Pen(Brushes.DarkBlue, 2);
            verticesBrush = Brushes.LightGray;
            sideBrush = Brushes.Gray;
            fillBrush = Brushes.Yellow;
            scale = panel1.Height / 32;
            points.Add(new Point(4, 4));
            points.Add(new Point(4, 26));
            points.Add(new Point(20, 26));
            points.Add(new Point(28, 18));
            points.Add(new Point(21, 4));
            points.Add(new Point(21, 8));
            points.Add(new Point(10, 8));
            points.Add(new Point(10, 4));
            points.Add(new Point(4, 4));
            FirstFigure(points);

            points2.Add(new Point(10, 12));
            points2.Add(new Point(10, 20));
            points2.Add(new Point(17, 20));
            points2.Add(new Point(21, 16));
            points2.Add(new Point(21, 12));
            points2.Add(new Point(10, 12));
        }
        private void FirstFigure(List<Point> list)
        {
            var heads = new List<Point>();
            heads.Add(new Point(4, 4));
            heads.Add(new Point(4, 26));
            heads.Add(new Point(20, 26));
            heads.Add(new Point(28, 18));
            heads.Add(new Point(21, 4));
            heads.Add(new Point(10, 4));
            ////////////////////
            heads.Add(new Point(21, 16));
            heads.Add(new Point(21, 13));
            foreach (var item in heads)
            {
                borderPoints.Add(new Point(item.X * scale, (31 * scale - item.Y * scale)));
            }
        }
        private void CreateGraph(ref Graphics graph)
        {
            Pen think_pen = new Pen(Brushes.Black, 1);
            middle_pen = new Pen(Brushes.Black, 2);

            X0Y0 = new Point(scale, panel1.Height - scale);

            graph.DrawLine(middle_pen, new Point(0, X0Y0.Y), new Point(panel1.Width, X0Y0.Y));

            graph.DrawLine(middle_pen, new Point(X0Y0.X, 0), new Point(X0Y0.X, panel1.Height));

            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();

            for (int i = 0; i < panel1.Width; i += scale)
            {
                graph.DrawString(((i - X0Y0.X) / scale).ToString(), drawFont, drawBrush, new Point(i - 15, X0Y0.Y + 5));
            }

            for (int i = 0; i < panel1.Height; i += scale)
            {
                if (i == X0Y0.Y)
                {
                    continue;
                }
                graph.DrawString(((-i + X0Y0.Y) / scale).ToString(), drawFont, drawBrush, new Point(X0Y0.X - 20, i - 2));
            }

            for (int i = 0; i <= panel1.Height / scale; i++)
            {
                graph.DrawLine(think_pen, new Point(0, i * scale), new Point(panel1.Width, i * scale));
            }

            for (int i = 0; i <= panel1.Width / scale; i++)
            {
                graph.DrawLine(think_pen, new Point(i * scale, 0), new Point(i * scale, panel1.Height));
            }
        }

        private void DrawRastrLine(Point a, Point b, List<Point> borderList)
        {
            int currX = a.X;
            int currY = a.Y;
            int dx = 1;
            int dy = 1;
            if (b.X == a.X)
            {
                dx = 0;
            }
            else if (dy != 0 && dy < dx)
            {
                dx = (int)Math.Round(dy * Math.Abs((double)((Math.Abs(b.X - a.X)) / Math.Abs(b.Y - a.Y))));
            }
            if (b.Y == a.Y)
            {
                dy = 0;
            }
            else if (dy >= dx && dx != 0)
            {
                dy = (int)Math.Round(dx * Math.Abs((double)(Math.Abs(b.Y - a.Y)) / Math.Abs(b.X - a.X)));
            }

            int max = (dy > dx) ? dy : dx;
            dx *= Math.Sign(b.X - a.X);
            dy *= Math.Sign(b.Y - a.Y);
            while (currX != b.X || currY != b.Y)
            {
                for (int i = max; i > 0; --i)
                {
                    if (dx < dy)
                    {
                        currX += (dx / i) * scale;
                        currY += (dy / max) * scale;
                    }
                    else
                    {
                        currX += (dx / max) * scale;
                        currY += (dy / i) * scale;
                    }
                    var p = new Point(currX, currY);
                    SetPixel(p, sideBrush);
                    if (a.Y != b.Y && (currY != b.Y))
                    {
                        borderList.Add(p);
                    }
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e) {   }

        private Point FindRastrPixel(Point real)
        {
            return new Point(real.X - real.X % scale, real.Y - real.Y % scale);
        }

        private void SetPixel(Point pixel, Brush someBrush)
        {
            gfx.FillRectangle(someBrush, new Rectangle(pixel, new Size(scale, scale)));
        }     

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            CreateGraph(ref gfx);
        }

        private void DrawFig(List<Point> fig, List<Point> borderList)
        {
            if (fig.Count > 0)
            {
                for (int i = 0; i < fig.Count - 1; ++i)
                {
                    Point start = new Point(fig[i].X * scale, X0Y0.Y - fig[i].Y * scale);
                    Point finish = new Point(fig[i + 1].X * scale, X0Y0.Y - fig[i + 1].Y * scale);
                    SetPixel(start, verticesBrush);
                    borderPointsCustom.Add(start);
                    DrawRastrLine(start, finish, borderList);
                    SetPixel(finish, verticesBrush);
                    borderPointsCustom.Add(finish);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DrawFig(points, borderPoints);
            DrawFig(points2, borderPoints);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Point rastrPixel = FindRastrPixel(new Point(e.X, e.Y));
            pointsCustom.Add(new Point((((-X0Y0.X + rastrPixel.X) + scale) / scale), (((X0Y0.Y - rastrPixel.Y)) / scale)));
            SetPixel(rastrPixel, verticesBrush);
        }
        
        private void SimpleFill(List<Point> list)
        {
            var fillList = new List<Point>();
            for (int y = 0; y < 32 * scale; y += scale)
            {
                var borders = list.Where(p => p.Y == y).OrderBy(p => p.X).ToList();
                if (borders.Count >= 1)
                {
                    if (borders.Count % 2 == 0)
                    {
                        int n = borders.Count;
                        for (int i = 0; i < n - 1; i += 2)
                        {
                            for (int x = borders[i].X; x <= borders[i + 1].X; x += scale)
                            {
                                SetPixel(new Point(x, y), fillBrush);
                                fillList.Add(new Point(x, y));
                                outputTextBox.Text += string.Format("({0},{1})  ", x / scale, 31 - y / scale);
                                Thread.Sleep(10);
                            }
                        }
                    }
                    else
                    {
                        for (int x = borders[0].X; x <= borders[1].X; x += scale)
                        {
                            SetPixel(new Point(x, y), fillBrush);
                            fillList.Add(new Point(x, y));
                            outputTextBox.Text += string.Format("({0},{1})  ", x / scale, 31 - y / scale);
                            Thread.Sleep(10);
                        }
                        SetPixel(borders[2], fillBrush);
                        fillList.Add(borders[2]);
                        outputTextBox.Text += string.Format("({0},{1})  ", borders[2].X / scale, 31 - borders[2].Y / scale);
                        Thread.Sleep(10);
                    }
                    outputTextBox.Text += Environment.NewLine + Environment.NewLine;
                }
            }
        }
        private void buttonFill_Click(object sender, EventArgs e)
        {
            borderPoints.RemoveAt(borderPoints.Count - 1);
            SimpleFill(borderPoints);
        }
    }
}