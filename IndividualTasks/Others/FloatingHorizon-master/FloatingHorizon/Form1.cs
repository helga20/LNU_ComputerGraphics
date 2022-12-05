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
        //Point capturePoint;
        //bool isMouseCaptured;

        public Form1()
        {
            InitializeComponent();
            DoMyInitialization();
        }

        private void DoMyInitialization()
        {
            List<String> funcNames = new List<String>();
            funcNames.Add("Y1 = 8*Cos(1.2*Sqrt(x * x  + z * z ))/(Sqrt(x * x  + z * z )+1)");
            funcNames.Add("Y2 = 16*(Sin(1.2*Sqrt(x * x  + z * z ))+Cos(1.5*Sqrt(x * x  + z * z )))/(Sqrt(x * x  + z * z )+1)");

            functions.Add(Functions.Y1);
            functions.Add(Functions.Y2);

            cmbBoxFunctions.Items.AddRange(funcNames.ToArray());
            cmbBoxFunctions.SelectedIndex = 0;

            txtBoxXBegin.Text = "-6,28";
            txtBoxXEnd.Text = "6,28";

            txtBoxZBegin.Text = "-6,28";
            txtBoxZEnd.Text = "6,28";

            txtBoxXStep.Text = "0,05";
            txtBoxZStep.Text = "0,2";

            horizonDrawer = new HorizonDrawer(picBox.Width, picBox.Height);
            InitializeHorizonDrawer();

            //isMouseCaptured = false;
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((Convert.ToDouble(txtBoxXBegin.Text) <= -2*Math.PI) || (Convert.ToDouble(txtBoxXEnd.Text) >= 2*Math.PI))
            {
                MessageBox.Show("Invalid input X: -2*Pi <= X <= 2*Pi !!!");
                return;
            }
            if ((Convert.ToDouble(txtBoxZBegin.Text) <= -2 * Math.PI) || (Convert.ToDouble(txtBoxZEnd.Text) >= 2 * Math.PI))
            {
                MessageBox.Show("Invalid input Z: -2*Pi <= Z <= 2*Pi !!!");
                return;
            }
            if (Convert.ToDouble(txtBoxXBegin.Text) >= Convert.ToDouble(txtBoxXEnd.Text))
            {
                MessageBox.Show("Invalid input X: X.Begin < X.End !!!");
                return;
            }
            if (Convert.ToDouble(txtBoxZBegin.Text) >= Convert.ToDouble(txtBoxZEnd.Text))
            {
                MessageBox.Show("Invalid input Z: Z.Begin < Z.End !!!");
                return;
            }

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
