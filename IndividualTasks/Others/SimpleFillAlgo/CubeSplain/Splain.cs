using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace CubeSplain
{
    class Splain
    {
        private Matrix points;
        private Matrix M;
        private Matrix R;
        private Matrix P;
        private Matrix B;
        private int n;
        public  double[] T;

        public Splain(Matrix p)
        {
            points = p;
            n = points.Height;
            M = new Matrix(n - 1, n - 1);
            R = new Matrix(n - 1, 2);
            P = new Matrix(n - 1, 2);
            T = new double[n - 1];
            CalculateT();
            CalculateM();
            CalculateR();
            CalculateP();
        }

        private void CalculateT()
        {
            for(int i = 0; i < n - 1;i++)
            {
                T[i] = Math.Sqrt((points[i+1,0]-points[i,0])* (points[i + 1, 0] - points[i, 0]) + (points[i + 1, 1] - points[i, 1]) * (points[i + 1, 1] - points[i, 1]));
            }
        }

        private void CalculateM()
        {
            M[0, 0] = 2 * (1 + T[n - 2] / T[0]);
            M[0, 1] = T[n - 2] / T[0];
            M[0, n - 2] = -1;
            for (int i = 1; i < n - 1; ++i)
            {
                M[i, i] = 2 * (T[i - 1] + T[i]);
                M[i, i - 1] = T[i];
                if (i < n - 2)
                    M[i, i + 1] = T[i - 1];
            }
        }

        private void CalculateR()
        {
            R[0, 0] = 3 * (T[n - 2] / (T[0] * T[0])) * (points[1, 0] - points[0, 0]);
            R[0, 1] = 3 * (T[n - 2] / (T[0] * T[0])) * (points[1, 1] - points[0, 1]);
            for (int i = 1; i < n - 1; i++)
            {
                R[i, 0] = 3 * (T[i - 1] * T[i - 1] * (points[i + 1, 0] - points[i, 0]) + T[i] * T[i] * (points[i, 0] - points[i - 1, 0])) / (T[i - 1] * T[i]);
                R[i, 1] = 3 * (T[i - 1] * T[i - 1] * (points[i + 1, 1] - points[i, 1]) + T[i] * T[i] * (points[i, 1] - points[i - 1, 1])) / (T[i - 1] * T[i]);
            }
        }

        private void CalculateB(int k)
        {
            Matrix matr = new Matrix(4, 4);
            Matrix vect = new Matrix(4, 2);
            vect[0, 0] = points[k, 0];
            vect[0, 1] = points[k, 1];
            vect[1, 0] = P[k, 0];
            vect[1, 1] = P[k, 1];
            vect[2, 0] = points[k + 1, 0];
            vect[2, 1] = points[k + 1, 1];
            vect[3, 0] = P[(k + 1)%P.Height, 0];
            vect[3, 1] = P[(k + 1)%P.Height, 1];
            matr[0, 0] = matr[1, 1] = 1;
            matr[2, 0] = -3 / (T[k] * T[k]);
            matr[2, 1] = -2 / (T[k]);
            matr[2, 2] = 3 / (T[k] * T[k]);
            matr[2, 3] = -1 / (T[k]);
            matr[3, 0] = 2 / (T[k] * T[k] * T[k]);
            matr[3, 1] = 1 / (T[k] * T[k]);
            matr[3, 2] = -2 / (T[k] * T[k] * T[k]);
            matr[3, 3] = 1 / (T[k] * T[k]);
            B = matr * vect;
        }

        private void CalculateP()
        {
            P = Matrix.Solution(M, R);
        }

        public double[] SplinePart(double t, int k)
        {
            CalculateB(k);
            double sumX = 0;
            double sumY = 0;
            for (int i = 0; i < 4; ++i)
            {
                sumX += B[i, 0] * Math.Pow(t, i);
                sumY += B[i, 1] * Math.Pow(t, i);
            }

            return new double[] { sumX,sumY};
        }
    }
}
