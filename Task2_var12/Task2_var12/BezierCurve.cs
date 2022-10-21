using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Task2_var12
{
    public class BezierCurve
    {
        private static int Factorial(int n)
        {
            int f = 1;
            for (int i = 1; i <= n; i++)
            {
                f *= i;
            }
            return f;
        }
        private static float Polinom(int i, int n, float t)
        {
            return (Factorial(n) / (Factorial(i) * Factorial(n - i))) * 
                (float)Math.Pow(t, i) * (float)Math.Pow(1 - t, n - i);
        }
        private static float CoordinateX(float t, List<PointF> list_points)
        {
            float x = 0;
            int n = list_points.Count - 1;
            for (int i = 0; i <= n; i++)
            {
                float a = Polinom(i, n, t);
                x += a * list_points[i].X;
            }
            return x;
        }

        private static float CoordinateY(float t, List<PointF> list_points)
        {
            float y = 0;
            int n = list_points.Count - 1;
            for (int i = 0; i <= n; i++)
            {
                float b = Polinom(i, n, t);
                y += b * list_points[i].Y;
            }
            return y;
        }

        public static void DrawBezier(Graphics gr, Pen pen, float dt, List<PointF> list_points)
        {
            List<PointF> points = new List<PointF>();
            var a = list_points.Take(4).ToList();

            int counter = 0;
            while (list_points.Count - counter >= 4)
            {
                for (float t = 0.0f; t <= 1.0; t += dt)
                {
                    points.Add(new PointF(
                        CoordinateX(t, a),
                        CoordinateY(t, a)));
                }
                counter = counter + 3;
                a = list_points.Skip(counter).Take(4).ToList();
            }

            gr.DrawLines(pen, points.ToArray());

            for (var i = 0; i < list_points.Count - 1; i++)
            {
                gr.DrawLine(Pens.Yellow, list_points[i], list_points[i + 1]);
            }
        }
    }
}
