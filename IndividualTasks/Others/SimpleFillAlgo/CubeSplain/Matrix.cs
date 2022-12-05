using System;
using System.Collections.Generic;
using System.Collections;

namespace CubeSplain
{
    public class Matrix : IEnumerable, ICloneable
    {
        private static Random randGenerator = new Random();
        private int width;
        private int height;
        private double[,] matrix;
        private List<double> l;

        public IEnumerator GetEnumerator()
        {
            return matrix.GetEnumerator();
        }

        public object Clone()
        {
            Matrix m = new Matrix(this.Height, this.Width);
            for (int i = 0; i < this.Height; ++i)
            {
                for (int j = 0; j < this.Width; ++j)
                {
                    m[i, j] = this[i, j];
                }
            }
            return m;
        }

        public int Width
        {
            get
            {
                return width;
            }
            private set
            {
                if (value > 0)
                {
                    width = value;
                }
                else
                {
                    Width = randGenerator.Next();
                }
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
            private set
            {
                if (value > 0)
                {
                    height = value;
                }
                else
                {
                    Height = randGenerator.Next();
                }
            }
        }

        public double this[int i, int j]
        {
            get
            {
                return matrix[i, j];
            }
            set
            {
                matrix[i, j] = value;
            }
        }

        public Matrix(int size = 1, int length = 1)
        {
            Width = length;
            Height = size;
            matrix = new double[Height, Width];
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {

            if (m1.Width == m2.Width && m1.Height == m2.Height)
            {
                Matrix m = new Matrix(m1.Height, m2.Width);
                for (int i = 0; i < m.Height; i++)
                {
                    for (int j = 0; j < m.Width; j++)
                    {
                        m[i, j] = m1[i, j] + m2[i, j];
                    }
                }
                return m;
            }
            else
            {
                throw new Exception();
            }
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.Width == m2.Width && m1.Height == m2.Height)
            {
                Matrix m = new Matrix(m1.Height, m2.Width);
                for (int i = 0; i < m.Height; i++)
                {
                    for (int j = 0; j < m.Width; j++)
                    {
                        m[i, j] = m1[i, j] - m2[i, j];
                    }
                }
                return m;
            }
            else
            {
                throw new Exception();
            }
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Width == m2.Height)
            {
                Matrix m = new Matrix(m1.Height, m2.Width);
                for (int i = 0; i < m1.Height; i++)
                {
                    for (int j = 0; j < m2.Width; j++)
                    {
                        for (int k = 0; k < m1.Width; k++)
                        {
                            m[i, j] += m1[i, k] * m2[k, j];
                        }
                    }
                }
                return m;
            }
            else
            {
                throw new Exception();
            }
        }

        public static Matrix operator *(Matrix m2, double factor)
        {
            Matrix m = new Matrix(m2.Height, m2.Width);
            for (int i = 0; i < m2.Height; i++)
            {
                for (int j = 0; j < m2.Width; j++)
                {
                    m[i, j] = factor * m2[i, j];
                }
            }
            return m;
        }

        public static Matrix operator *(double factor, Matrix m2)
        { 
                Matrix m = new Matrix(m2.Height, m2.Width);
                for (int i = 0; i < m2.Height; i++)
                {
                    for (int j = 0; j < m2.Width; j++)
                    { 
                        m[i, j] = factor * m2[i, j];
                    }
                }
                return m;  
        }

        public void Output()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write("{0}{1}", this[i, j], " ");
                }
                Console.WriteLine();
            }
        }

        public void Input()
        {
            Console.WriteLine("Enter {0}*{1} matrix: ", Height, Width);
            string s;
            for (int i = 0; i < Height; i++)
            {
                s = Console.ReadLine();
                string[] st = s.Split(' ');
                for (int j = 0; j < Width; j++)
                {
                    int t;
                    if (Int32.TryParse(st[j], out t))
                    {
                        this[i, j] = (double)t;
                    }
                    else
                    {
                        this[i, j] = Convert.ToDouble(st[j]);
                    }
                }
            }
        }

        public void Random()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    this[i, j] = randGenerator.Next(10);
                }
            }
        }

        public static Matrix Solution(Matrix a, Matrix b)
        {
            if (a.Height == a.Width && a.Height == b.Height)
            {
                int n = a.Width;

                Matrix t;
                for (int k = 0; k < n; k++)
                {
                    t = new Matrix(n, n);
                    for (int i = 0; i < n; i++)
                    {
                        t[i, i] = 1;
                        if (i == k)
                        {
                            t[i, i] = 1 / a[k, k];
                        }
                        else if (i > k)
                        {
                            t[i, k] = -a[i, k] / a[k, k];
                        }
                    }
                    a = t * a;
                    b = t * b;
                }

                for (int k = n - 1; k > 0; k--)
                {
                    t = new Matrix(n, n);
                    for (int i = 0; i < n; i++)
                    {
                        t[i, i] = 1;
                        if (i < k)
                        {
                            t[i, k] = -a[i, k];
                        }
                    }
                    b = t * b;
                }
            }
            return b;
        }
    }
}

