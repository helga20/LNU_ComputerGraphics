using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace FloatingHorizon
{
    public delegate double functionType(double x, double z);

    enum VisibilityType
    {
        VisibleAndUpper,
        VisibleAndLower,
        Invisible
    }

    class HorizonDrawer
    {
        const double EMPTY_VALUE = -1;
        int imageWidth, imageHeight;
        double[] lowerHorizon, upperHorizon;
        double zMin = EMPTY_VALUE, zMax = EMPTY_VALUE,
            xMin = EMPTY_VALUE, xMax = EMPTY_VALUE;
        double xStep = 0, zStep = 0;
        double angleX = 0, angleY = 0, angleZ = 0;
        Graphics graphics;
        Bitmap bitmap;
        Color backColor, mainColor;
        Point3D bodyCenter;
        bool isInverted;
        functionType currentFunction;

        public HorizonDrawer(int imageWidth, int imageHeight)
        {
            this.imageWidth = imageWidth;
            this.imageHeight = imageWidth;
            bitmap = new Bitmap(imageWidth, imageHeight);
            lowerHorizon = new double[imageWidth];
            upperHorizon = new double[imageWidth];
            backColor = Color.White;
            mainColor = Color.Black;
        }

        private void ResetHorizons()
        {
            for (int i = 0; i < imageWidth; ++i)
            {
                lowerHorizon[i] = imageHeight;// double.MaxValue;
                upperHorizon[i] = 0; // double.MinValue;
            }
        }

        public void SetBoundsOnX(double xMin, double xMax) //перевірка меж по Х
        {
            Debug.Assert(xMin < xMax);
            this.xMax = xMax;
            this.xMin = xMin;
        }

        public void SetBoundsOnZ(double zMin, double zMax) //перевірка меж по Z
        {
            Debug.Assert(zMin < zMax);
            this.zMax = zMax;
            this.zMin = zMin;
        }

        public void SetXZsteps(double xStep, double zStep) // перевірка кроків >0
        {
            Debug.Assert(zStep > 0 && xStep > 0);
            this.zStep = zStep;
            this.xStep = xStep;
        }

        public void SetAngleX(int angle) // градуси в радіани
        {
            angleX = DegreeToRadian(angle);
        }

        public void SetAngleY(int angle)
        {
            angleY = DegreeToRadian(angle);
        }

        public void SetAngleZ(int angle)
        {
            angleZ = DegreeToRadian(angle);
        }

        public void SetMainColor(Color mainColor)
        {
            this.mainColor = mainColor;
        }

        public void SetBackColor(Color backColor)
        {
            this.backColor = backColor;
        }

        private void CalcBodyCenter(functionType function)
        {
            bodyCenter.X = (xMax + xMin) / 2;
            bodyCenter.Z = (zMin + zMax) / 2;
            bodyCenter.Y = function(bodyCenter.X, bodyCenter.Z)-5; // щоб будувало по центру
        }

        private double functionWrapper(double x, double z)
        {
            return (isInverted) ? currentFunction(-z, x) : currentFunction(x, z);
        }

        public void Draw(Graphics imageGraphics, functionType function)
        {
            #region Debug
            Debug.Assert(imageGraphics != null);
            Debug.Assert(zMin != EMPTY_VALUE && zMax != EMPTY_VALUE && xMin != EMPTY_VALUE &&
                xMax != EMPTY_VALUE);
            Debug.Assert(xStep != 0 && zStep != 0);
            #endregion

            CalcBodyCenter(function);
            StartDoubleBuffering();
            ResetHorizons();

            isInverted = false;
            currentFunction = function;
            functionType wrapperFunc = new functionType(functionWrapper);
//              if (!(Math.PI / 2.0 < angleY && angleY < 3 * Math.PI / 2.0))
//                  zStep *= -1;
            try
            {
                DrawHelperZ(imageGraphics, wrapperFunc);
                //ResetHorizons();
//                 angleY += Math.PI / 2.0;
//                 isInverted = true;
//                 DrawHelperZ(imageGraphics, wrapperFunc);
//                 isInverted = false;
//                 angleY -= Math.PI / 2.0;
            }
            catch (Exception)
            {
                Debug.Assert(false);
            }
            finally 
            {
                FinishDoubleBuffering(imageGraphics);
            }
        }

        private bool CheckPoint(Point2D point)
        {
            if ((int)Math.Round(point.X) < 0 || (int)Math.Round(point.X) >= imageWidth)
                return false;
            return true;
        }

        private void DrawHelperX(Graphics imageGraphics, functionType function)
        {
            Point2D prevPoint,
                leftPoint = new Point2D(EMPTY_VALUE, EMPTY_VALUE),
                rightPoint = new Point2D(EMPTY_VALUE, EMPTY_VALUE);

            for (double currentX = xMax; currentX >= xMin; currentX -= xStep)
            {
                double y;
                y = function(currentX, zMin);
                prevPoint = TransformView(currentX, y, zMin).ToPoint2D();
                ProcessEdge(prevPoint, ref leftPoint);
                if (!(CheckPoint(prevPoint) && CheckPoint(leftPoint)))
                    continue;

                VisibilityType prevVisibility = CheckVisibility(prevPoint);

                Point2D currentPoint = new Point2D();
                for (double currentZ = zMin; currentZ <= zMax; currentZ += zStep)
                {
                    y = function(currentX, currentZ);
                    currentPoint = TransformView(currentX, y, currentZ).ToPoint2D();
                    if (!CheckPoint(currentPoint))
                        continue;
                    VisibilityType currentVisibility = CheckVisibility(currentPoint);
                    if (currentVisibility == prevVisibility)
                    {
                        if (currentVisibility == VisibilityType.VisibleAndLower || currentVisibility == VisibilityType.VisibleAndUpper)
                        {
                            DrawLine(prevPoint, currentPoint);
                            Smooth(prevPoint, currentPoint);
                        }
                    }
                    else
                    {
                        ProcessHorizonIntersectingLine(currentVisibility, prevVisibility, prevPoint, currentPoint);
                    }
                    prevVisibility = currentVisibility;
                    prevPoint = currentPoint;
                }
                ProcessEdge(currentPoint, ref rightPoint);
            }
        }

        private void DrawHelperZ(Graphics imageGraphics, functionType function)
        {
            Point2D prevPoint,
                leftPoint = new Point2D(EMPTY_VALUE, EMPTY_VALUE),
                rightPoint = new Point2D(EMPTY_VALUE, EMPTY_VALUE);

            for (double currentZ = zMax; currentZ >= zMin; currentZ -= zStep)
            {
                double y;
                y = function(xMin, currentZ);
                prevPoint = TransformView(xMin, y, currentZ).ToPoint2D();
                ProcessEdge(prevPoint, ref leftPoint);
                if (!(CheckPoint(prevPoint) && CheckPoint(leftPoint)))
                    continue;

                VisibilityType prevVisibility = CheckVisibility(prevPoint);

                Point2D currentPoint = new Point2D();
                for (double currentX = xMin; currentX <= xMax; currentX += xStep)
                {
                    y = function(currentX, currentZ);
                    currentPoint = TransformView(currentX, y, currentZ).ToPoint2D();
                    if (!CheckPoint(currentPoint))
                        continue;
                    VisibilityType currentVisibility = CheckVisibility(currentPoint);
                    if (currentVisibility == prevVisibility)
                    {
                        if (currentVisibility == VisibilityType.VisibleAndLower || currentVisibility == VisibilityType.VisibleAndUpper)
                        {
                            DrawLine(prevPoint, currentPoint);
                            Smooth(prevPoint, currentPoint);
                        }
                    }
                    else
                    {
                        ProcessHorizonIntersectingLine(currentVisibility, prevVisibility, prevPoint, currentPoint);
                    }
                    prevVisibility = currentVisibility;
                    prevPoint = currentPoint;
                }
                ProcessEdge(currentPoint, ref rightPoint);
            }
        }

        private void StartDoubleBuffering()
        {
            this.graphics = Graphics.FromImage(bitmap);
            graphics.Clear(backColor);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; //згладжування
        }

        private void FinishDoubleBuffering(Graphics sourceGraphics)
        {
            sourceGraphics.DrawImage(bitmap, 0, 0);
        }

        private void ProcessHorizonIntersectingLine(VisibilityType currentVisibility, VisibilityType prevVisibility,
           Point2D prevPoint, Point2D currentPoint)
        {
            Point2D currentIntersection;
            if (currentVisibility == VisibilityType.Invisible)
            {
                if (prevVisibility == VisibilityType.VisibleAndUpper)
                {
                    currentIntersection = GetCurvesIntersection(prevPoint, currentPoint, upperHorizon);
                }
                else
                {
                    currentIntersection = GetCurvesIntersection(prevPoint, currentPoint, lowerHorizon);
                }
                DrawLine(prevPoint, currentIntersection);
                Smooth(prevPoint, currentIntersection);
            }
            else
            {
                bool Flag = (currentVisibility == VisibilityType.VisibleAndUpper);
                currentIntersection = ProcessVisibleNowLine(prevVisibility, currentPoint, prevPoint, Flag);
            }
        }

        private Point2D ProcessVisibleNowLine(VisibilityType prevVisibility, Point2D currentPoint, Point2D prevPoint, bool Flag)
        {
            Point2D currentIntersection;
            if (prevVisibility == VisibilityType.Invisible)
            {
                currentIntersection = GetCurvesIntersection(prevPoint, currentPoint, upperHorizon);
                DrawLine(currentIntersection, currentPoint);
                Smooth(currentIntersection, currentPoint);
            }
            else
            {
                if (Flag)
                    currentIntersection = GetCurvesIntersection(prevPoint, currentPoint, lowerHorizon);
                else
                    currentIntersection = GetCurvesIntersection(prevPoint, currentPoint, upperHorizon);
                DrawLine(prevPoint, currentIntersection);
                Smooth(prevPoint, currentIntersection);
                if (Flag)
                    currentIntersection = GetCurvesIntersection(prevPoint, currentPoint, upperHorizon);
                else
                    currentIntersection = GetCurvesIntersection(prevPoint, currentPoint, lowerHorizon);
                DrawLine(currentIntersection, currentPoint);
                Smooth(currentIntersection, currentPoint);
            }
            return currentIntersection;
        }

        private Point2D GetCurvesIntersection(Point2D firstPoint, Point2D secondPoint, double[] currentHorizon)
        {
            if (Math.Round(firstPoint.X) == Math.Round(secondPoint.X))
            {
                return new Point2D(secondPoint.X, currentHorizon[(int)Math.Round(secondPoint.X)]);
            }

            if (Math.Round(secondPoint.X) < Math.Round(firstPoint.X))
            {
                SwapPoints(ref firstPoint, ref secondPoint);
            }

            double lineCoef = (secondPoint.Y - firstPoint.Y) / (secondPoint.X - firstPoint.X);
            int currentX = (int)Math.Floor(firstPoint.X) + 1;
            double currentY = firstPoint.Y + lineCoef;
            int prevSignY = Math.Sign(firstPoint.Y + lineCoef - currentHorizon[currentX]);
            int currentSignY = prevSignY;

            while (prevSignY == currentSignY && currentX <= Math.Floor(secondPoint.X))
            {
                currentY += lineCoef;
                ++currentX;
                currentSignY = Math.Sign(currentY - currentHorizon[currentX]);
            }

            if (Math.Abs(currentY - lineCoef - currentHorizon[currentX - 1]) <= Math.Abs(currentY - currentHorizon[currentX]))
            {
                currentY -= lineCoef;
                --currentX;
            }
            return new Point2D(currentX, currentY);
        }

        private void SwapPoints<PointType>(ref PointType firstPoint, ref PointType secondPoint)
        {
            PointType tempPoint = firstPoint;
            firstPoint = secondPoint;
            secondPoint = tempPoint;
        }

        private void DrawLine(Point firstPoint, Point secondPoint)
        {
            graphics.DrawLine(new Pen(mainColor), firstPoint, secondPoint);
        }

        private VisibilityType CheckVisibility(Point2D currentPoint)
        {
            if (currentPoint.Y < upperHorizon[(int)Math.Round(currentPoint.X)] && currentPoint.Y > lowerHorizon[(int)Math.Round(currentPoint.X)])
                return VisibilityType.Invisible;
            else
                if (currentPoint.Y >= upperHorizon[(int)Math.Round(currentPoint.X)])
                    return VisibilityType.VisibleAndUpper;
                else
                    return VisibilityType.VisibleAndLower;
        }

        private void ProcessEdge(Point2D prevPoint, ref Point2D currentPoint)
        {
            if (currentPoint.X != EMPTY_VALUE)
            {

                int currentX = (int)Math.Round(currentPoint.X);
                if (Math.Round(prevPoint.Y) >= Math.Round(upperHorizon[currentX]) ||
                    Math.Round(prevPoint.Y) <= Math.Round(lowerHorizon[currentX]) ||
                    Math.Round(currentPoint.Y) >= Math.Round(upperHorizon[currentX]) ||
                    Math.Round(currentPoint.Y) <= Math.Round(lowerHorizon[currentX]))
                    DrawLine(prevPoint, currentPoint);
                     
                LineApproximate(prevPoint, currentPoint);
               // Smooth(prevPoint, currentPoint);
            }
            currentPoint = prevPoint;
        }

        private void LineApproximate(Point2D firstPoint, Point2D secondPoint)
        {
            int firstX = (int)Math.Round(firstPoint.X);
            int secondX = (int)Math.Round(secondPoint.X);
            if (firstX != secondX)
            {
                if (firstPoint.X > secondPoint.X)
                {
                    SwapPoints(ref firstPoint, ref secondPoint);
                    firstX = (int)Math.Round(firstPoint.X);
                    secondX = (int)Math.Round(secondPoint.X);
                }

                double lineCoef = (secondPoint.Y - firstPoint.Y) / (secondX - firstX);
                double currentY = firstPoint.Y;
                for (int currentX = firstX; currentX <= secondX; ++currentX)
                {
                    currentY += lineCoef;
                    upperHorizon[currentX] = Math.Max(upperHorizon[currentX], currentY);
                    lowerHorizon[currentX] = Math.Min(lowerHorizon[currentX], currentY);
                }
            }
            else
            {
                int currentX = firstX;
                double maxY = Math.Max(firstPoint.Y, secondPoint.Y);
                double minY = Math.Min(firstPoint.Y, secondPoint.Y);
                upperHorizon[currentX] = Math.Max(upperHorizon[currentX], maxY);
                lowerHorizon[currentX] = Math.Min(lowerHorizon[currentX], minY);
            }
        }

        private void Smooth(Point2D firstPoint, Point2D secondPoint)
        {
            Debug.Assert(secondPoint.X >= 0 && secondPoint.X < imageWidth);
            Debug.Assert(firstPoint.X >= 0 && firstPoint.X < imageWidth);

            if (Math.Round(firstPoint.X) == Math.Round(secondPoint.X))
            {
                upperHorizon[(int)Math.Round(secondPoint.X)] = Math.Max(upperHorizon[(int)Math.Round(secondPoint.X)], Math.Max(secondPoint.Y, firstPoint.Y));
                lowerHorizon[(int)Math.Round(secondPoint.X)] = Math.Min(lowerHorizon[(int)Math.Round(secondPoint.X)], Math.Min(secondPoint.Y, firstPoint.Y));
            }
            else
            {
                if (secondPoint.X < firstPoint.X)
                    SwapPoints(ref firstPoint, ref secondPoint);
                double lineCoef = (secondPoint.Y - firstPoint.Y) / (secondPoint.X - firstPoint.X);
                double currentY;
                // optimization here
                for (int currentX = (int)Math.Round(firstPoint.X); currentX <= Math.Round(secondPoint.X); ++currentX)
                {
                    currentY = lineCoef * (currentX - firstPoint.X) + firstPoint.Y;
                    upperHorizon[currentX] = Math.Max(upperHorizon[currentX], currentY);
                    lowerHorizon[currentX] = Math.Min(lowerHorizon[currentX], currentY);
                }
                for (int currentX = (int)Math.Round(secondPoint.X); currentX <= Math.Round(firstPoint.X); ++currentX)
                {
                    currentY = lineCoef * (currentX - secondPoint.X) + secondPoint.Y;
                    upperHorizon[currentX] = Math.Max(upperHorizon[currentX], currentY);
                    lowerHorizon[currentX] = Math.Min(lowerHorizon[currentX], currentY);
                }
            }
        }

        private Point3D TransformView(double x, double y, double z)
        {
            double AdditionalScale = 1.5;
            double scaleRatio = Math.Min(imageWidth, imageHeight) / (Math.Max(xMax - xMin, zMax - zMin) * AdditionalScale);
            x = bodyCenter.X + (x - bodyCenter.X) * scaleRatio;
            y = -(bodyCenter.Y + (y - bodyCenter.Y) * scaleRatio);
            z = bodyCenter.Z + (z - bodyCenter.Z) * scaleRatio;

            // y axis
            double temp = z * Math.Cos(angleY) + x * Math.Sin(angleY);
            x = -z * Math.Sin(angleY) + x * Math.Cos(angleY);
            z = temp;

            // x axis
            temp = y * Math.Cos(angleX) + z * Math.Sin(angleX);
            z = -y * Math.Sin(angleX) + z * Math.Cos(angleX);
            y = temp;

            // z axis
            temp = x * Math.Cos(angleZ) + y * Math.Sin(angleZ);
            y = -x * Math.Sin(angleZ) + y * Math.Cos(angleZ);
            x = temp;

            x += imageWidth / 2 - bodyCenter.X;
            y += imageHeight / 3 - bodyCenter.Y;

            return new Point3D(x, y, z);
        }

        private double DegreeToRadian(int angleInDegrees)
        {
            return Math.PI * angleInDegrees / 180;
        }

        private void DrawLine(Point2D firstPoint, Point2D secondPoint)
        {
            graphics.DrawLine(new Pen(mainColor, 1), firstPoint, secondPoint);
        }

    }

    public struct Point2D
    {
        public double X, Y;

        public Point2D(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public static implicit operator Point(Point2D pointD)
        {
            return new Point((int)Math.Round(pointD.X), (int)Math.Round(pointD.Y));
        }
    }

    public struct Point3D
    {
        public double X, Y, Z;

        public Point3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Point2D ToPoint2D()
        {
            return new Point2D(this.X, this.Y);
        }
    }
}
