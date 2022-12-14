using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace lab04
{
    public class ObjReader
    {
        public Vector3[] Vertices { get; }
        public Vector3[] NormalVectors { get; }
        public Face[] Faces { get; }
        private int cubeSize;

        public ObjReader(string path, DirectBitmap bitmap)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> normalVectors = new List<Vector3>();
            List<Face> faces = new List<Face>();
            cubeSize = Math.Min(bitmap.Height, bitmap.Width);
            for (int i = 0; i < lines.Length; i++)
            {
                string c = lines[i][0..2];
                switch (c)
                {
                    case "v ":
                        string[] vertexTable = lines[i].Split(" ");
                        vertices.Add(new Vector3(
                            float.Parse(vertexTable[1], CultureInfo.InvariantCulture), 
                            float.Parse(vertexTable[2], CultureInfo.InvariantCulture),
                            float.Parse(vertexTable[3], CultureInfo.InvariantCulture)));
                        break;
                    case "vn":
                        string[] vectorTable = lines[i].Split(" ");
                        normalVectors.Add(new Vector3(float.Parse(vectorTable[1], CultureInfo.InvariantCulture), float.Parse(vectorTable[2], CultureInfo.InvariantCulture), float.Parse(vectorTable[3], CultureInfo.InvariantCulture)));
                        break;
                    case "f ":
                        string[] faceTable = lines[i].Split(" ");
                        string[] X = faceTable[1].Split("//");
                        string[] Y = faceTable[2].Split("//");
                        string[] Z = faceTable[3].Split("//");
                        faces.Add(new Face((vertices[int.Parse(X[0]) - 1], vertices[int.Parse(Y[0]) - 1], vertices[int.Parse(Z[0]) - 1]), (normalVectors[int.Parse(X[1]) - 1], normalVectors[int.Parse(Y[1]) - 1], -1 * normalVectors[int.Parse(Z[1]) - 1])));
                        break;
                    default:
                        break;

                }
            }
            Vertices = vertices.ToArray();
            NormalVectors = normalVectors.ToArray();
            Faces = faces.ToArray();
        }


        private float ConvertToScreenSize(double val)
        {
            return (float)(val + 1) / 2 * cubeSize;
        }
    }
}
