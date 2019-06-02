using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work5
{
    class MyPoint
    {
        public float X;
        public float Y;
        public float Z;

        public MyPoint(float x,float y,float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public MyPoint (float x,float y)
        {
            X = x;
            Y = y;
            Z = 0;
        }
        public MyPoint()
        { }
        
    }
}
