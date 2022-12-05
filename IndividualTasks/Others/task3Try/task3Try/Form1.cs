#region lection
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task3Try
{
    public partial class Form1 : Form
    {
        #region constants
        int size = 640;
        int dimension = 32;
        int side;
        int mulucyaX = 30;
        int mulucyaY = 23 + 30; //милиці, магічні числа, третій курс
        int step, r;
        List<Unit> units = new List<Unit>(); //пікселі
        List<Point> lines = new List<Point>(); //лінії для промальовування 
        List<Color> colors = new List<Color>();
        List<Color> gColors = new List<Color>() { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.PowderBlue, Color.Blue, Color.BlueViolet };
        Random randCore = new Random();
        int counter = 0;
        #endregion
        #region bools
        bool needPolygon = true; //аби багато разів не малювати
        bool demonstrationTest = false; //Демонстраційний тест
        bool immersiveTest = true; //самі додаватимемо точки для ліній
        bool circleCentreSet = false;
        #endregion
        public Form1()
        {
            DoubleBuffered = true;
            InitializeComponent();
            side = size / dimension;
            step = 360 / int.Parse(stepTxt.Text);
            r = int.Parse(rTxt.Text);
        }
        private void DrawPixel(int i, int j, Color color)
        {
            foreach (Unit pixel in units)
            {
                if (pixel.i == i && pixel.j == j)
                {
                    pixel.Fill(color);
                }
            }

        }
        private Unit findPixel(int _x, int _y)
        {
            foreach (Unit pixel in units)
            {
                if ((_x >= pixel.x && _x < pixel.x + pixel.side + mulucyaX) && (_y >= pixel.y && _y < pixel.y + pixel.side + mulucyaY))
                {
                    return pixel;
                }
            }
            return new Unit();
        }
        private Unit findPixelIJ(int _i, int _j)
        {
            foreach (Unit pixel in units)
            {
                if (pixel.i == _i && pixel.j == _j)
                {
                    return pixel;
                }
            }
            return new Unit();
        }
        public Point middlePoint(Point _point, int _step)
        {
            return new Point((_point.X + _step) / 2, (_point.Y + _step) / 2);
        }
        private void RASTR_Paint(object sender, PaintEventArgs e)
        {
            if (needPolygon)
            {

                units.Clear();
                Random rand = new Random();
                for (int i = 0; i < 32; i++)
                {
                    for (int j = 0; j < 32; j++)
                    {
                        Unit toAdd = new Unit(i * side, j * side, side);
                        toAdd.i = i;
                        toAdd.j = j;
                        //toAdd.Fill(Color.FromArgb(rand.Next(0, 10), rand.Next(0, 10), rand.Next(0, 10)));
                        units.Add(toAdd);
                    }
                }
                foreach (var unit in units)
                {
                    unit.Draw(e);
                }

                needPolygon = false;
            }
            if (demonstrationTest && lines.Count != 0)
            {
                int iteration = 0;
                for (int i = 0; i < lines.Count - 1; i += 2)
                {

                    for (int k = 0; k < lines.Count; k++)
                    {
                        if (lines[k].X > 31)
                        {
                            lines[k] = new Point(31, lines[k].Y);
                        }
                        if (lines[k].Y > 31)
                        {
                            lines[k] = new Point(lines[k].X, 31);
                        }
                        if (lines[k].X == 0 && lines[k].Y == 0)
                        {
                            lines.RemoveAt(k);
                        }
                    }
                    int uz = 0;
                    Point p1 = lines[i];
                    Point p2 = lines[i + 1];
                    //БРЕЗЕНХЕМ ЦІЛОЧИСЕЛЬНИЙ
                    int deltaX = Math.Abs(p2.X - p1.X);
                    int deltaY = Math.Abs(p2.Y - p1.Y);
                    int signX = (p1.X < p2.X) ? 1 : -1;
                    int signY = (p1.Y < p2.Y) ? 1 : -1;
                    int x = p1.X;
                    int y = p1.Y;
                    int error = deltaX - deltaY;
                    infoTxt.Text += $"{Environment.NewLine} ЛІНІЯ {(i + 1) / 2}: ({lines[i].X};{lines[i].Y} -> {lines[i + 1].X};{lines[i + 1].Y}):  {Environment.NewLine} ";

                    for (; ; )
                    {
                        DrawPixel(x, y, Color.FromArgb(200, gColors[(iteration / 2) % 7].R, gColors[(iteration / 2) % 7].G, gColors[(iteration / 2) % 7].B));
                        infoTxt.Text += $"{uz}: {x};{y}   ";
                        if (x == p2.X && y == p2.Y)
                        {
                            break;
                        }
                        int error2 = error * 2;
                        if (error2 > (-deltaY))
                        {
                            error -= deltaY;
                            x += signX;
                        }
                        if (error2 < deltaX)
                        {
                            error += deltaX;
                            y += signY;
                        }
                        uz++;
                    }

                    DrawPixel(p1.X, p1.Y, Color.Red);
                    DrawPixel(p2.X, p2.Y, Color.FromArgb(255, gColors[(iteration / 2) % 7].R, gColors[(iteration / 2) % 7].G, gColors[(iteration / 2) % 7].G));
                    iteration += 2;

                }
            }
            if (lines.Count % 2 == 0 && !demonstrationTest)
            {

                for (int i = 0; i < lines.Count - 1; i += 2)
                {

                    int uz = 1;
                    Point p1 = lines[i];
                    Point p2 = lines[i + 1];
                    //БРЕЗЕНХЕМ ЦІЛОЧИСЕЛЬНИЙ
                    int deltaX = Math.Abs(p2.X - p1.X);
                    int deltaY = Math.Abs(p2.Y - p1.Y);
                    int signX = (p1.X < p2.X) ? 1 : -1;
                    int signY = (p1.Y < p2.Y) ? 1 : -1;
                    int x = p1.X;
                    int y = p1.Y;
                    int error = deltaX - deltaY;

                    infoTxt.Text += $"{Environment.NewLine} ЛІНІЯ {(i + 1) / 2}: ({lines[i].X};{lines[i].Y} -> {lines[i + 1].X};{lines[i + 1].Y}):  {Environment.NewLine} ";
                    
                    for (; ; )
                    {
                        Unit findByMiddle = findPixelIJ(x, y);
                        e.Graphics.FillEllipse(Brushes.Blue, findByMiddle.MiddleCoord.X - 3, findByMiddle.MiddleCoord.Y-3, 6, 6);
                        DrawPixel(findByMiddle.i, findByMiddle.j, colors[i / 2]);
                        infoTxt.Text += $"{uz}: {x};{y}   ";
                        if (x == p2.X && y == p2.Y)
                        {
                            break;
                        }
                        int error2 = error * 2;
                        if (error2 > (-deltaY))
                        {
                            error -= deltaY;
                            x += signX;
                        }
                        if (error2 < deltaX)
                        {
                            error += deltaX;
                            y += signY;
                        }
                        uz++;
                    }
                    DrawPixel(p1.X, p1.Y, Color.FromArgb(250, colors[i / 2].R, colors[i / 2].G, colors[i / 2].B));
                    DrawPixel(p2.X, p2.Y, Color.FromArgb(250, colors[i / 2].R, colors[i / 2].G, colors[i / 2].B));

                }
            }
            //  infoTxt.Text = string.Empty;
            int c = 1;
            foreach (var unit in units)
            {
                if (unit.isFilled)
                {

                }
                unit.Draw(e); //промальовування всіх пікселів
            }
            int ck = 0;
            //for (int i = 0; i < lines.Count / 2; i++)
            //{
            //    infoTxt.Text += $" ЛІНІЯ {i + 1}: ({lines[i].X};{lines[i].Y} -> {lines[i + 1].X};{lines[i + 1].Y}):  {Environment.NewLine} ";
            //    while (units[ck].x != lines[i + 1].X && units[ck].y != lines[i + 1].Y)
            //    {
            //        if (units[ck].isFilled)
            //        {
            //            infoTxt.Text += $"{units[ck].j};{units[ck].i}";
            //        }
            //        ck++;
            //    }

            //}
            Color linesColor = Color.FromArgb(140, 0, 0, 0);
            for (int i = 0; i < lines.Count - 1; i += 2) //позначаю простими лініями, аби було видно, що ми малювали 
            {
                Unit first = findPixelIJ(lines[i].X, lines[i].Y);
                Unit second = findPixelIJ(lines[i + 1].X, lines[i + 1].Y);
                e.Graphics.FillEllipse(Brushes.Black, first.MiddleCoord.X - 3, first.MiddleCoord.Y - 3, 6, 6);
                e.Graphics.FillEllipse(Brushes.Black, second.MiddleCoord.X - 3, second.MiddleCoord.Y - 3, 6, 6);
                e.Graphics.DrawLine(new Pen(linesColor, 2), first.MiddleCoord, second.MiddleCoord);
            }
        }
        public class Unit //клас пікселя
        {
            public int x { get; set; }

            public int y { get; set; }

            public int i { get; set; }

            public int j { get; set; }

            public int side { get; set; }

            public Color color;

            public bool isFilled { get; set; } = false;

            public Point MiddleCoord { get; }

            public Point connector { get; set; }
            public Unit(int _x = 0, int _y = 0, int _side = 20)
            {
                x = _x;
                y = _y;
                side = _side;
                MiddleCoord = new Point(x + side / 2, y + side / 2);
                connector = new Point(x + side / 2, y + side / 2);
            }

            public void Fill(Color _color)
            {
                color = _color;
                isFilled = true;
            }
            public void Draw(PaintEventArgs e)
            {
                if (!isFilled)
                {
                    e.Graphics.DrawRectangle(Pens.Black, x, y, side, side);
                    e.Graphics.FillEllipse(Brushes.Green, x + side / 2 - 3, y + side / 2 - 3, 6, 6);
                }
                else
                {
                    e.Graphics.FillRectangle(new Pen(color).Brush, x, y, side, side);
                    e.Graphics.FillEllipse(Brushes.Blue, x + side / 2 - 3, y + side / 2 - 3, 6, 6);
                }
                
            }
        }
        private void RASTR_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = Cursor.Position;
            xCurr.Text = (pos.X - mulucyaX).ToString();
            yCurr.Text = (pos.Y - mulucyaY).ToString();
            Unit unit = findPixel(pos.X, pos.Y);
            iCurr.Text = unit.i.ToString();
            jCurr.Text = unit.j.ToString();
        }

        private void jAxis_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 32; i++)
            {
                if (i < 10)
                    e.Graphics.DrawString(i.ToString(), new Font("Times New Roman", 12), Brushes.Black, i * side + 3, 3);
                else
                    e.Graphics.DrawString(i.ToString(), new Font("Times New Roman", 12), Brushes.Black, i * side - 1, 3);
            }
        } //малюю вісь йот

        private void iAxis_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 32; i++)
            {
                e.Graphics.DrawString(i.ToString(), new Font("Times New Roman", 14), Brushes.Black, 3, i * side);
            }
        } //вісь і

        private void immButt_Click(object sender, EventArgs e)
        {
            immersiveTest = true;
            demonstrationTest = false;
            demoButt.BackColor = Color.Red;
            immButt.BackColor = Color.Green;
        } //кнопки

        private void demoButt_Click(object sender, EventArgs e)
        {
            demonstrationTest = true;
            immersiveTest = false;
            immButt.BackColor = Color.Red;
            demoButt.BackColor = Color.Green;
        }
        private void RASTR_MouseClick(object sender, MouseEventArgs e)
        {
            if (immersiveTest)
            {
                Color currColor;
                int l = 1, h = 200;
                if (counter % 2 == 0)
                {
                    colors.Add(gColors[(counter / 2) % 7]);
                    colors[colors.Count - 1] = Color.FromArgb(160, gColors[(counter / 2) % 7].R, gColors[(counter / 2) % 7].G, gColors[(counter / 2) % 7].B);
                    pointsLabel.ForeColor = Color.Red;
                }
                else pointsLabel.ForeColor = Color.Green;
                currColor = colors[colors.Count - 1];
                Graphics gr = RASTR.CreateGraphics();
                Unit found = findPixel(e.X + mulucyaX, e.Y + mulucyaY);
                gr.FillRectangle(new Pen(Color.FromArgb(255, currColor.R, currColor.G, currColor.B)).Brush, found.x, found.y, found.side, found.side);
                lines.Add(new Point(found.i, found.j));
                counter++;
                pointsLabel.Text = counter.ToString();
            }
            if (demonstrationTest)
            {
                lines.Clear();
                // MessageBox.Show(Math.Cos(90*Math.PI/180).ToString());
                Unit found = findPixel(e.X + mulucyaX, e.Y + mulucyaY);
                Graphics gr = RASTR.CreateGraphics();
                gr.FillRectangle(new Pen(Color.Black).Brush, found.x, found.y, found.side, found.side);
                found.Fill(Color.Red);
                for (int i = 0; i < 360; i += step)
                {
                    double yot = r * Math.Cos(i * (Math.PI / 180));
                    double je = r * Math.Sin(i * (Math.PI / 180));
                    if ((int)yot + found.i > 31)
                    {
                        lines.Add(new Point(31, (int)je + found.j));
                    }
                    if ((int)yot + found.i < 0)
                    {
                        lines.Add(new Point(0, (int)je + found.j));
                    }
                    if ((int)je + found.j > 31)
                    {
                        lines.Add(new Point((int)yot + found.i, 31));
                    }
                    if ((int)je + found.j < 0)
                    {
                        lines.Add(new Point((int)yot + found.i, 0));
                    }
                    Unit inCircle = findPixelIJ((int)yot + found.i, (int)je + found.j);
                    //  inCircle.Fill(Color.Black);
                    lines.Add(new Point(found.i, found.j));
                    lines.Add(new Point(inCircle.i, inCircle.j));
                    //  gr.FillEllipse(Brushes.Red, (float)x+found.x, (float)y+found.y, 10, 10);
                }
            }
        }

        private void confirmImm_Click(object sender, EventArgs e)
        {

            if (int.Parse(pointsLabel.Text) % 2 == 0)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            r = int.Parse(rTxt.Text);
            step = 360 / int.Parse(stepTxt.Text);
            infoTxt.Text = string.Empty;
            if (int.Parse(pointsLabel.Text) % 2 == 0)
            {

                RASTR.Refresh();
            }
        }

        private void stepTxt_TextChanged(object sender, EventArgs e)
        {
            r = int.Parse(rTxt.Text);
            step = 360 / int.Parse(stepTxt.Text);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void recycler_Click(object sender, EventArgs e)
        {
            foreach (var a in units)
            {
                a.isFilled = false;
            }

            pointsLabel.Text = 0.ToString();
            counter = 0;
            lines.Clear();
            colors.Clear();
            RASTR.Refresh();
            infoTxt.Text = string.Empty;
        }
    }
}
#endregion
#region wiki
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace task3Try
//{
//    public partial class Form1 : Form
//    {
//        #region constants
//        int size = 640;
//        int dimension = 32;
//        int side;
//        int mulucyaX = 30;
//        int mulucyaY = 23 + 30; //милиці, магічні числа, третій курс
//        int step, r;
//        List<Unit> units = new List<Unit>(); //пікселі
//        List<Point> lines = new List<Point>(); //лінії для промальовування 
//        List<Color> colors = new List<Color>();
//        List<Color> gColors = new List<Color>() { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.PowderBlue, Color.Blue, Color.BlueViolet };
//        Random randCore = new Random();
//        int counter = 0;
//        #endregion
//        #region bools
//        bool needPolygon = true; //аби багато разів не малювати
//        bool demonstrationTest = false; //Демонстраційний тест
//        bool immersiveTest = true; //самі додаватимемо точки для ліній
//        bool circleCentreSet = false;
//        #endregion
//        public Form1()
//        {
//            DoubleBuffered = true;
//            InitializeComponent();
//            pict.SizeMode = PictureBoxSizeMode.StretchImage;
//            side = size / dimension;
//            step = 360 / int.Parse(stepTxt.Text);
//            r = int.Parse(rTxt.Text);
//        }
//        private void DrawPixel(int i, int j, Color color)
//        {
//            foreach (Unit pixel in units)
//            {
//                if (pixel.i == i && pixel.j == j)
//                {
//                    pixel.Fill(color);
//                }
//            }

//        }
//        private Unit findPixel(int _x, int _y)
//        {
//            foreach (Unit pixel in units)
//            {
//                if ((_x >= pixel.x && _x < pixel.x + pixel.side + mulucyaX) && (_y >= pixel.y && _y < pixel.y + pixel.side + mulucyaY))
//                {
//                    return pixel;
//                }
//            }
//            return new Unit();
//        }
//        private Unit findPixelIJ(int _i, int _j)
//        {
//            foreach (Unit pixel in units)
//            {
//                if (pixel.i == _i && pixel.j == _j)
//                {
//                    return pixel;
//                }
//            }
//            return new Unit();
//        }
//        public Point middlePoint(Point _point, int _step)
//        {
//            return new Point((_point.X + _step) / 2, (_point.Y + _step) / 2);
//        }
//        private void RASTR_Paint(object sender, PaintEventArgs e)
//        {
//            if (needPolygon)
//            {

//                units.Clear();
//                Random rand = new Random();
//                for (int i = 0; i < 32; i++)
//                {
//                    for (int j = 0; j < 32; j++)
//                    {
//                        Unit toAdd = new Unit(i * side, j * side, side);
//                        toAdd.i = i;
//                        toAdd.j = j;
//                        //toAdd.Fill(Color.FromArgb(rand.Next(0, 10), rand.Next(0, 10), rand.Next(0, 10)));
//                        units.Add(toAdd);
//                    }
//                }
//                foreach (var unit in units)
//                {
//                    unit.Draw(e);
//                }

//                needPolygon = false;
//            }
//            if (demonstrationTest && lines.Count != 0)
//            {
//                int iteration = 0;
//                for (int i = 0; i < lines.Count - 1; i += 2)
//                {

//                    for (int k = 0; k < lines.Count; k++)
//                    {
//                        if (lines[k].X > 31)
//                        {
//                            lines[k] = new Point(31, lines[k].Y);
//                        }
//                        if (lines[k].Y > 31)
//                        {
//                            lines[k] = new Point(lines[k].X, 31);
//                        }
//                        if (lines[k].X == 0 && lines[k].Y == 0)
//                        {
//                            lines.RemoveAt(k);
//                        }
//                    }
//                    int uz = 0;
//                    Point p1 = lines[i];
//                    Point p2 = lines[i + 1];
//                    //БРЕЗЕНХЕМ ЦІЛОЧИСЕЛЬНИЙ
//                    int deltaX = Math.Abs(p2.X - p1.X);
//                    int deltaY = Math.Abs(p2.Y - p1.Y);
//                    int signX = (p1.X < p2.X) ? 1 : -1;
//                    int signY = (p1.Y < p2.Y) ? 1 : -1;
//                    int x = p1.X;
//                    int y = p1.Y;
//                    int error = deltaX - deltaY;
//                    infoTxt.Text += $"{Environment.NewLine} ЛІНІЯ {(i + 1) / 2}: ({lines[i].X};{lines[i].Y} -> {lines[i + 1].X};{lines[i + 1].Y}):  {Environment.NewLine} ";

//                    for (; ; )
//                    {
//                        DrawPixel(x, y, Color.FromArgb(200, gColors[(iteration / 2) % 7].R, gColors[(iteration / 2) % 7].G, gColors[(iteration / 2) % 7].B));
//                        infoTxt.Text += $"{uz}: {x};{y}   ";
//                        if (x == p2.X && y == p2.Y)
//                        {
//                            break;
//                        }
//                        int error2 = error * 2;
//                        if (error2 > (-deltaY))
//                        {
//                            error -= deltaY;
//                            x += signX;
//                        }
//                        if (error2 < deltaX)
//                        {
//                            error += deltaX;
//                            y += signY;
//                        }
//                        uz++;
//                    }

//                    DrawPixel(p1.X, p1.Y, Color.Red);
//                    DrawPixel(p2.X, p2.Y, Color.FromArgb(255, gColors[(iteration / 2) % 7].R, gColors[(iteration / 2) % 7].G, gColors[(iteration / 2) % 7].G));
//                    iteration += 2;

//                }
//            }
//            if (lines.Count % 2 == 0 && !demonstrationTest)
//            {

//                for (int i = 0; i < lines.Count - 1; i += 2)
//                {

//                    //int uz = 1;
//                    //Point p1 = lines[i];
//                    //Point p2 = lines[i + 1];
//                    ////БРЕЗЕНХЕМ ЦІЛОЧИСЕЛЬНИЙ
//                    //int deltaX = Math.Abs(p2.X - p1.X);
//                    //int deltaY = Math.Abs(p2.Y - p1.Y);
//                    //int signX = (p1.X < p2.X) ? 1 : -1;
//                    //int signY = (p1.Y < p2.Y) ? 1 : -1;
//                    //int x = p1.X;
//                    //int y = p1.Y;
//                    //int error = deltaX - deltaY;
//                    //infoTxt.Text += $"{Environment.NewLine} ЛІНІЯ {(i + 1) / 2}: ({lines[i].X};{lines[i].Y} -> {lines[i + 1].X};{lines[i + 1].Y}):  {Environment.NewLine} ";
//                    //for (; ; )
//                    //{

//                    //    DrawPixel(x, y, colors[i / 2]);
//                    //    infoTxt.Text += $"{uz}: {x};{y}   ";
//                    //    if (x == p2.X && y == p2.Y)
//                    //    {
//                    //        break;
//                    //    }
//                    //    int error2 = error * 2;
//                    //    if (error2 > (-deltaY))
//                    //    {
//                    //        error -= deltaY;
//                    //        x += signX;
//                    //    }
//                    //    if (error2 < 0)
//                    //    {
//                    //        error += deltaX;
//                    //        y += signY;
//                    //    }
//                    //    uz++;
//                    //}
//                    Point p1 = lines[i];
//                    Point p2 = lines[i + 1];
//                    int x0 = p1.X;
//                    int y0 = p1.Y;
//                    int x1 = p2.X;
//                    int y1 = p2.Y;
//                        int dx = Math.Abs(x1 - x0);
//                        int dy = Math.Abs(y1 - y0);
//                        int sx = x1 >= x0 ? 1 : -1;
//                        int sy = y1 >= y0 ? 1 : -1;

//                        if (dy <= dx)
//                        {
//                            int d = (dy << 1) - dx;
//                            int d1 = dy << 1;
//                            int d2 = (dy - dx) << 1;
//                        DrawPixel(x0, y0, colors[i / 2]);
//                            for (int x = x0 + sx, y = y0, j = 1; j <= dx; j++, x += sx)
//                            {
//                                if (d > 0)
//                                {
//                                    d += d2;
//                                    y += sy;
//                                }
//                                else
//                                    d += d1;
//                                DrawPixel(x, y, colors[i / 2]);
//                            }
//                        }
//                        else
//                        {
//                            int d = (dx << 1) - dy;
//                            int d1 = dx << 1;
//                            int d2 = (dx - dy) << 1;
//                        DrawPixel(x0, y0, colors[i / 2]);
//                        for (int y = y0 + sy, x = x0, j = 1; j <= dy; j++, y += sy)
//                            {
//                                if (d > 0)
//                                {
//                                    d += d2;
//                                    x += sx;
//                                }
//                                else
//                                    d += d1;
//                            DrawPixel(x, y, colors[i / 2]);
//                        }
//                        }
//                    DrawPixel(p1.X, p1.Y, Color.FromArgb(250, colors[i / 2].R, colors[i / 2].G, colors[i / 2].B));
//                    DrawPixel(p2.X, p2.Y, Color.FromArgb(250, colors[i / 2].R, colors[i / 2].G, colors[i / 2].B));

//                }
//            }
//            //  infoTxt.Text = string.Empty;
//            int c = 1;
//            foreach (var unit in units)
//            {
//                if (unit.isFilled)
//                {

//                }
//                unit.Draw(e); //промальовування всіх пікселів
//            }
//            int ck = 0;
//            Color linesColor = Color.FromArgb(140, 0, 0, 0);
//            for (int i = 0; i < lines.Count - 1; i += 2) //позначаю простими лініями, аби було видно, що ми малювали 
//            {
//                Unit first = findPixelIJ(lines[i].X, lines[i].Y);
//                Unit second = findPixelIJ(lines[i + 1].X, lines[i + 1].Y);
//                //e.Graphics.FillEllipse(new Pen(linesColor).Brush, first.MiddleCoord.X - 6, first.MiddleCoord.Y - 8, 10, 10);
//                //e.Graphics.FillEllipse(new Pen(linesColor).Brush, second.MiddleCoord.X - 6, second.MiddleCoord.Y - 8, 10, 10);
//                e.Graphics.DrawLine(new Pen(linesColor, 2), first.MiddleCoord, second.MiddleCoord);
//            }
//        }
//        public class Unit //клас пікселя
//        {
//            public int x { get; set; }

//            public int y { get; set; }

//            public int i { get; set; }

//            public int j { get; set; }

//            public int side { get; set; }

//            public Point determinantP { get; set; }
//            public Color color;

//            public bool isFilled { get; set; } = false;

//            public Point MiddleCoord { get; }

//            public Unit(int _x = 0, int _y = 0, int _side = 20)
//            {
//                x = _x;
//                y = _y;
//                side = _side;
//                MiddleCoord = new Point(x + side / 2, y + side / 2);
//                determinantP = new Point(x, y + side);
//            }

//            public void Fill(Color _color)
//            {
//                color = _color;
//                isFilled = true;
//            }
//            public void Draw(PaintEventArgs e)
//            {
//                if (!isFilled)
//                {
//                    e.Graphics.DrawRectangle(Pens.Black, x, y, side, side);
//                }
//                else
//                {
//                    e.Graphics.FillRectangle(new Pen(color).Brush, x, y, side, side);
//                }
//                e.Graphics.FillEllipse(Brushes.Black, determinantP.X - 3, determinantP.Y - 3, 6, 6);
//            }
//        }
//        private void RASTR_MouseMove(object sender, MouseEventArgs e)
//        {
//            Point pos = Cursor.Position;
//            xCurr.Text = (pos.X - mulucyaX).ToString();
//            yCurr.Text = (pos.Y - mulucyaY).ToString();
//            Unit unit = findPixel(pos.X, pos.Y);
//            iCurr.Text = unit.i.ToString();
//            jCurr.Text = unit.j.ToString();

//            if (unit.isFilled)
//                pict.Image = Properties.Resources.okbut;
//            else pict.Image = Properties.Resources.nobut;
//        }

//        private void jAxis_Paint(object sender, PaintEventArgs e)
//        {
//            for (int i = 0; i < 32; i++)
//            {
//                if (i < 10)
//                    e.Graphics.DrawString(i.ToString(), new Font("Times New Roman", 12), Brushes.Black, i * side + 3, 3);
//                else
//                    e.Graphics.DrawString(i.ToString(), new Font("Times New Roman", 12), Brushes.Black, i * side - 1, 3);
//            }
//        } //малюю вісь йот

//        private void iAxis_Paint(object sender, PaintEventArgs e)
//        {
//            for (int i = 0; i < 32; i++)
//            {
//                e.Graphics.DrawString(i.ToString(), new Font("Times New Roman", 14), Brushes.Black, 3, i * side);
//            }
//        } //вісь і

//        private void immButt_Click(object sender, EventArgs e)
//        {
//            immersiveTest = true;
//            demonstrationTest = false;
//            demoButt.BackColor = Color.Red;
//            immButt.BackColor = Color.Green;
//        } //кнопки

//        private void demoButt_Click(object sender, EventArgs e)
//        {
//            demonstrationTest = true;
//            immersiveTest = false;
//            immButt.BackColor = Color.Red;
//            demoButt.BackColor = Color.Green;
//        }
//        private void RASTR_MouseClick(object sender, MouseEventArgs e)
//        {
//            if (immersiveTest)
//            {
//                Color currColor;
//                int l = 1, h = 200;
//                if (counter % 2 == 0)
//                {
//                    colors.Add(gColors[(counter / 2) % 7]);
//                    colors[colors.Count - 1] = Color.FromArgb(160, gColors[(counter / 2) % 7].R, gColors[(counter / 2) % 7].G, gColors[(counter / 2) % 7].B);
//                    pointsLabel.ForeColor = Color.Red;
//                }
//                else pointsLabel.ForeColor = Color.Green;
//                currColor = colors[colors.Count - 1];
//                Graphics gr = RASTR.CreateGraphics();
//                Unit found = findPixel(e.X + mulucyaX, e.Y + mulucyaY);
//                gr.FillRectangle(new Pen(Color.FromArgb(255, currColor.R, currColor.G, currColor.B)).Brush, found.x, found.y, found.side, found.side);
//                lines.Add(new Point(found.i, found.j));
//                counter++;
//                pointsLabel.Text = counter.ToString();
//            }
//            if (demonstrationTest)
//            {
//                lines.Clear();
//                // MessageBox.Show(Math.Cos(90*Math.PI/180).ToString());
//                Unit found = findPixel(e.X + mulucyaX, e.Y + mulucyaY);
//                Graphics gr = RASTR.CreateGraphics();
//                gr.FillRectangle(new Pen(Color.Black).Brush, found.x, found.y, found.side, found.side);
//                found.Fill(Color.Red);
//                for (int i = 0; i < 360; i += step)
//                {
//                    double yot = r * Math.Cos(i * (Math.PI / 180));
//                    double je = r * Math.Sin(i * (Math.PI / 180));
//                    if ((int)yot + found.i > 31)
//                    {
//                        lines.Add(new Point(31, (int)je + found.j));
//                    }
//                    if ((int)yot + found.i < 0)
//                    {
//                        lines.Add(new Point(0, (int)je + found.j));
//                    }
//                    if ((int)je + found.j > 31)
//                    {
//                        lines.Add(new Point((int)yot + found.i, 31));
//                    }
//                    if ((int)je + found.j < 0)
//                    {
//                        lines.Add(new Point((int)yot + found.i, 0));
//                    }
//                    Unit inCircle = findPixelIJ((int)yot + found.i, (int)je + found.j);
//                    //  inCircle.Fill(Color.Black);
//                    lines.Add(new Point(found.i, found.j));
//                    lines.Add(new Point(inCircle.i, inCircle.j));
//                    //  gr.FillEllipse(Brushes.Red, (float)x+found.x, (float)y+found.y, 10, 10);
//                }
//            }
//        }

//        private void confirmImm_Click(object sender, EventArgs e)
//        {

//            if (int.Parse(pointsLabel.Text) % 2 == 0)
//            {

//            }
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {

//            r = int.Parse(rTxt.Text);
//            step = 360 / int.Parse(stepTxt.Text);
//            infoTxt.Text = string.Empty;
//            if (int.Parse(pointsLabel.Text) % 2 == 0)
//            {

//                RASTR.Refresh();
//            }
//        }

//        private void stepTxt_TextChanged(object sender, EventArgs e)
//        {
//            r = int.Parse(rTxt.Text);
//            step = 360 / int.Parse(stepTxt.Text);
//        }

//        private void recycler_Click(object sender, EventArgs e)
//        {
//            foreach (var a in units)
//            {
//                a.isFilled = false;
//            }

//            pointsLabel.Text = 0.ToString();
//            counter = 0;
//            lines.Clear();
//            colors.Clear();
//            RASTR.Refresh();
//            infoTxt.Text = string.Empty;
//        }
//    }
//}

#endregion