using System;
using System.Collections.Generic;
using System.Text;

namespace PolygonsEditor
{
    public class MyEdge
    {
        public MyPoint From { get; set; }
        public MyPoint To { get; set; }
        public double Degree { get { return Math.Atan((To.Y - From.Y) / (To.X - From.X)); } }

        public MyEdge(MyPoint from, MyPoint to)
        {
            this.From = from;
            this.To = to;
        }
    }
}
