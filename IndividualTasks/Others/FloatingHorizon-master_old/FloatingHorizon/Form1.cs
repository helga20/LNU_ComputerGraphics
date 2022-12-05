using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloatingHorizon
{
    public partial class Form1 : Form
    {
        HorizonDrawer horizonDrawer = null;
        List<functionType> functions = new List<functionType>();
        Point capturePoint;
        bool isMouseCaptured;

        public Form1()
        {
            InitializeComponent();
            DoMyInitialization();
        }

        private void DoMyInitialization()
        {
            #region functions
            List<String> funcNames = new List<String>();
            funcNames.Add("Y1 = 8*Cos(1.2*Sqrt(x * x  + z * z ))/(Sqrt(x * x  + z * z )+1)");
            funcNames.Add("Y2 = 16*(Sin(1.2*Sqrt(x * x  + z * z ))+Cos(1.5*Sqrt(x * x  + z * z )))/(Sqrt(x * x  + z * z )+1)");

            functions.Add(Functions.Y1);
            functions.Add(Functions.Y2);

            cmbBoxFunctions.Items.AddRange(funcNames.ToArray());
            cmbBoxFunctions.SelectedIndex = 0;
            #endregion

            #region textBoxes
            txtBoxXBegin.Text = "-6";
            txtBoxXEnd.Text = "6";

            txtBoxZBegin.Text = "-6";
            txtBoxZEnd.Text = "6";
            txtBoxXStep.Text = "0,05";
            txtBoxZStep.Text = "0,2";
            #endregion

            #region colors
            btnMainColor.BackColor = Color.MediumVioletRed;
            btnBackColor.BackColor = Color.Black;
            #endregion

            #region horizontDrawer
            horizonDrawer = new HorizonDrawer(picBox.Width, picBox.Height);
            InitializeHorizonDrawer();
            #endregion

            isMouseCaptured = false;
        }

        private void InitializeHorizonDrawer()
        {
            try
            {
                horizonDrawer.SetBoundsOnX(Convert.ToDouble(txtBoxXBegin.Text), Convert.ToDouble(txtBoxXEnd.Text));
                horizonDrawer.SetBoundsOnZ(Convert.ToDouble(txtBoxZBegin.Text), Convert.ToDouble(txtBoxZEnd.Text));
                horizonDrawer.SetXZsteps(Convert.ToDouble(txtBoxXStep.Text), Convert.ToDouble(txtBoxZStep.Text));
                horizonDrawer.SetAngleX(trackBarX.Value);
                horizonDrawer.SetAngleY(trackBarY.Value);
                horizonDrawer.SetAngleZ(trackBarZ.Value);
            }
            catch (System.Exception)
            {
                MessageBox.Show("Invalid input");
            }
            horizonDrawer.SetBackColor(btnBackColor.BackColor);
            horizonDrawer.SetMainColor(btnMainColor.BackColor);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitializeHorizonDrawer();
            ReDraw();
        }

        private void trackBarX_ValueChanged(object sender, EventArgs e)
        {
            horizonDrawer.SetAngleX(trackBarX.Value);
            ReDraw();
        }

        private void trackBarY_ValueChanged(object sender, EventArgs e)
        {
            horizonDrawer.SetAngleY(trackBarY.Value);
            ReDraw();
        }

        private void trackBarZ_ValueChanged(object sender, EventArgs e)
        {
            horizonDrawer.SetAngleZ(trackBarZ.Value);
            ReDraw();
        }

        private void ReDraw()
        {
            horizonDrawer.Draw(picBox.CreateGraphics(), functions[cmbBoxFunctions.SelectedIndex]);
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            colorDlg.ShowDialog();
            horizonDrawer.SetBackColor(colorDlg.Color);
            btnBackColor.BackColor = colorDlg.Color;
        }

        private void btnMainColor_Click(object sender, EventArgs e)
        {
            colorDlg.ShowDialog();
            horizonDrawer.SetMainColor(colorDlg.Color);
            btnMainColor.BackColor = colorDlg.Color;
        }

        private void picBox_MouseDown(object sender, MouseEventArgs e)
        {
            capturePoint = e.Location;
            isMouseCaptured = true;
        }

        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (isMouseCaptured)
            {
                double deltaAngle;
                if (Math.Abs(e.X - capturePoint.X) < Math.Abs(e.Y - capturePoint.Y))
                {
                    if (e.Y > capturePoint.Y)
                    {
                        deltaAngle = 360 * (e.Y - capturePoint.Y) / (picBox.Height - capturePoint.Y);
                    }
                    else
                    {
                        deltaAngle = 360 * (1 - (e.Y - capturePoint.Y) / (capturePoint.Y - picBox.Height));
                    }
                    trackBarX.Value = Math.Abs((int)Math.Round(deltaAngle) % 361);
                }
                else
                {
                    if (e.X > capturePoint.X)
                    {
                        deltaAngle = 360 * (e.X - capturePoint.X) / (picBox.Width - capturePoint.X);
                    }
                    else
                    {
                        deltaAngle = 360 * (1 - (e.X - capturePoint.X) / (capturePoint.X - picBox.Width));
                    }
                    trackBarY.Value = Math.Abs((int)Math.Round(deltaAngle) % 361);
                }

            }
        }

        private void picBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseCaptured = false;
        }

    }

    public static class Functions
    {
        public static double Y1(double x, double z)
        {
            return 8 * Math.Cos(1.2 * Math.Sqrt(x * x + z * z)) / (Math.Sqrt(x * x + z * z) + 1);
        }

        public static double Y2(double x, double z)
        {
            return 16 * (Math.Sin(1.2 * Math.Sqrt(x * x + z * z)) + Math.Cos(1.5 * Math.Sqrt(x * x + z * z))) / (Math.Sqrt(x * x + z * z) + 1);
        }
    }
}
