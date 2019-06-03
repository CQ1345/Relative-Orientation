using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work5
{
    class EElements//外方位元素
    {
        private MyPoint s;//摄影中心
        private float phi;//航向倾角
        private float omega;//旁向倾角
        private float kappa;//像片旋角
        private double[,] r = new double[3, 3];
        private bool flag;

        public float Phi { get => phi; set { phi = value; flag = false; } }
        public float Omega { get => omega; set { omega = value; flag = false; } }
        public float Kappa { get => kappa; set { kappa = value; flag = false; } }

        public double[,] R
        {
            get
            {
                if (flag == false)
                {
                    float rad1 = Phi;
                    float rad2 = Omega;
                    float rad3 = Kappa;
                    r[0, 0] = Math.Cos(rad1) * Math.Cos(rad3) - Math.Sin(rad1) * Math.Sin(rad2) * Math.Sin(rad3);
                    r[0, 1] = -Math.Cos(rad1) * Math.Sin(rad3) - Math.Sin(rad1) * Math.Sin(rad2) * Math.Cos(rad3);
                    r[0, 2] = -Math.Sin(rad1) * Math.Cos(rad2);
                    r[1, 0] = Math.Cos(rad2) * Math.Sin(rad3);
                    r[1, 1] = Math.Cos(rad2) * Math.Cos(rad3);
                    r[1, 2] = -Math.Sin(rad2);
                    r[2, 0] = Math.Sin(rad1) * Math.Cos(rad3) + Math.Cos(rad1) * Math.Sin(rad2) * Math.Sin(rad3);
                    r[2, 1] = -Math.Sin(rad1) * Math.Sin(rad3) + Math.Cos(rad1) * Math.Sin(rad2) * Math.Cos(rad3);
                    r[2, 2] = Math.Cos(rad1) * Math.Cos(rad2);
                    flag = true;
                }
                return r;
            }
        }

        internal MyPoint S { get => s; set => s = value; }

        public EElements(MyPoint center, float p, float o, float k)
        {
            S = center;
            Phi = p;
            Omega = o;
            Kappa = k;
            flag = false;
        }
        public EElements(float p,float o,float k)
        {
            Phi = p;
            Omega = o;
            Kappa = k;
            flag = false;
        }
        public EElements()
        { }

        //像空间坐标到像空间辅助坐标的转换
        public MyPoint Photo2Model(MyPoint photo)
        {
            MyPoint model = new MyPoint();
            double[,] coord = new double[3, 1];
            coord[0,0] = photo.X;
            coord[1, 0] = photo.Y;
            coord[2, 0] = photo.Z;
            //解算
            coord = Matrix.Mutiply(R, coord);
            model.X = (float)coord[0, 0];
            model.Y = (float)coord[1, 0];
            model.Z = (float)coord[2, 0];
            return model;
        }
        public List<MyPoint> Photo2Model(List<MyPoint> photo)
        {
            List<MyPoint> model = new List<MyPoint>();
            int num = photo.Count();
            for(int i=0;i<num;i++)
                model.Add(Photo2Model(photo[i]));
            return model;
        }
    }
}
