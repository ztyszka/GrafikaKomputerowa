using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab04
{
    public class Face
    {

        public Face((Vector3 v1, Vector3 v2, Vector3 v3) vertices, (Vector3 v1, Vector3 v2, Vector3 v3) normalVectors)
        {
            NormalVectors = normalVectors;
            Vertices = vertices;
        }

        public (Vector3 v1, Vector3 v2, Vector3 v3) NormalVectors { get; set; }
        public (Vector3 v1, Vector3 v2, Vector3 v3) Vertices { get; set; }

        public void draw(DirectBitmap bitmap)
        {
            using (Graphics g = Graphics.FromImage(bitmap.Bitmap))
            {
                Pen tmpPen = new Pen(Color.Black, 1);

                g.DrawLine(tmpPen, new Point((int)Vertices.v1.X, (int)Vertices.v1.Y), new Point((int)Vertices.v2.X, (int)Vertices.v2.Y));
                g.DrawLine(tmpPen, new Point((int)Vertices.v1.X, (int)Vertices.v1.Y), new Point((int)Vertices.v3.X, (int)Vertices.v3.Y));
                g.DrawLine(tmpPen, new Point((int)Vertices.v3.X, (int)Vertices.v3.Y), new Point((int)Vertices.v2.X, (int)Vertices.v2.Y));

            }

        }
    }


}
