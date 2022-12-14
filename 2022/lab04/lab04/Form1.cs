using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab04
{
    public partial class Form1 : Form
    {
        private DirectBitmap drawArea;
        private ObjReader objReader;
        private string filePath = "..//..//..//proj2_sfera.obj";

        private float fov = (float)(Math.PI / 3);
        private Display3D display3D;
        public Form1()
        {
            display3D = new Display3D(fov);
            InitializeComponent();
            drawArea = new DirectBitmap(pictureBox.Width, pictureBox.Height);
            objReader = new ObjReader(filePath, drawArea);
            pictureBox.Image = drawArea.Bitmap;
            drawArea.FillWithColor(Color.White);
            FOVtrackBar.Value = (int) (fov / (2 * Math.PI) * 100);
            
            Redraw();
        }

        private void Redraw()
        {
            foreach (Face face in objReader.Faces)
            {
                Vector4 v1 = new Vector4(face.Vertices.v1.X, face.Vertices.v1.Y, face.Vertices.v1.Z, 1);
                Vector4 v2 = new Vector4(face.Vertices.v1.X, face.Vertices.v2.Y, face.Vertices.v1.Z, 2);
                Vector4 v3 = new Vector4(face.Vertices.v1.X, face.Vertices.v3.Y, face.Vertices.v1.Z, 3);
                (double, double) p1 = display3D.GetScreenCoordinates(v1);
                (double, double) p2 = display3D.GetScreenCoordinates(v2);
                (double, double) p3 = display3D.GetScreenCoordinates(v3);
                if (Math.Abs(p1.Item1) > 1 || Math.Abs(p1.Item2) > 1 || Math.Abs(p2.Item1) > 1 || Math.Abs(p2.Item2) > 1 || Math.Abs(p3.Item1) > 1 || Math.Abs(p3.Item2) > 1)
                    break;
                Point point1 = ConvertToScreenSize(p1);
                Point point2 = ConvertToScreenSize(p2);
                Point point3 = ConvertToScreenSize(p3);

                using (Graphics g = Graphics.FromImage(drawArea.Bitmap))
                {
                    Pen tmpPen = new Pen(Color.Black, 1);
                    g.DrawLine(tmpPen, point1, point2);
                    g.DrawLine(tmpPen, point1, point3);
                    g.DrawLine(tmpPen, point3, point2);
                }
            }
        }

        private Point ConvertToScreenSize((double x, double y) pt)
        {
            return new Point((int)((pt.x + 1) / 2 * drawArea.Width), (int)((pt.y + 1) / 2 * drawArea.Height));
        }

        private void FOVtrackBar_ValueChanged(object sender, EventArgs e)
        {
            fov = (float) (FOVtrackBar.Value * 2 * Math.PI / 100);
            display3D.fieldOfView = fov;
            Redraw();
        }
    }
}
