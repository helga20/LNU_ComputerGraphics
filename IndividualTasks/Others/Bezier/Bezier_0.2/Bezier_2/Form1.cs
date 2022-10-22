using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bezier_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics G; // ������ �������
        PointF[] Arr = new PointF[] // �������� ������ �����
        {
            new PointF(10,150),
            new PointF(5,50),
            new PointF(150,50),
            new PointF(140,140),
            new PointF(150,50),
            new PointF(150,50),
            new PointF(150,50),

        };
        int Fuctorial(int n) // ������� ���������� ����������
        {
            int res = 1;
            for (int i = 1; i <= n; i++)
                res *= i;
            return res;
        }
        float polinom(int i, int n, float t)// ������� ���������� �������� ����������
        {
            return (Fuctorial(n) / (Fuctorial(i) * Fuctorial(n - i))) * (float)Math.Pow(t, i) * (float)Math.Pow(1 - t, n - i);
        }
        void Draw()// ������� ��������� ������
        {
            int j = 0;
            float step = 0.01f;// ������� ��� 0.01 ��� ������� ��������

            PointF[] result = new PointF[101];//�������� ������ ����� ������
            for (float t = 0; t < 1; t += step)
            {
                float ytmp = 0;
                float xtmp = 0;
                for (int i = 0; i < Arr.Length; i++)
                { // �������� �� ������ �����
                    float b = polinom(i, Arr.Length - 1, t); // ��������� ��� ������� ����������
                    xtmp += Arr[i].X * b; // ���������� � ���������� ���������
                    ytmp += Arr[i].Y * b;
                }
                result[j] = new PointF(xtmp, ytmp);
                j++;

            }
            G.DrawLines(new Pen(Color.Red), result);// ������ ���������� ������ �����
        }

        private void button1_Click(object sender, EventArgs e)
        {
            G = Graphics.FromHwnd(pictureBox1.Handle);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Draw();
        }
    }
}