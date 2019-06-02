using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work5
{
    //相对定向
    class RelativeOritation
    {
        private float bx;
        private EElements leftEE;//左像片的外方位元素
        private List<MyPoint> leftMCoords;//左像片的像空间辅助坐标
        private List<MyPoint> rightPCoords;//右像片的像空间坐标

        private double[,] A;//系数矩阵
        private double[,] L;//常数项

        public EElements RightEE { get; }
        public float by { get; private set; }
        public float bz { get; private set; }

        //初始化
        public RelativeOritation(List<MyPoint> leftC,List<MyPoint> rightC)
        {
            //左外方位元素
            leftEE = new EElements();
            leftEE.Phi = leftEE.Omega = leftEE.Kappa = 0;
            leftEE.S = new MyPoint(0, 0, 0);
            //右外方位元素
            RightEE = new EElements();
            RightEE.Phi = RightEE.Omega = RightEE.Kappa = 0;
            RightEE.S = new MyPoint(0, 0, 0);
            bx = leftC[0].X - rightC[0].X;
            //获取坐标
            leftMCoords = leftC;//左像片的像空间辅助坐标
            rightPCoords = rightC;//右像片的像空间坐标
            //生成系数矩阵和常数项
            A = new double[leftC.Count(),5];
            L = new double[leftC.Count(), 1];
        }

        //解算
        public void CalROE()
        {
            double[,] dX;
            double min = 1;
            float u, v;
            u = v = 0;
            do
            {
                //计算by,bz
                by = bx * u;
                bz = bx * v;
                //计算误差方程的系数矩阵和常数项
                CalFactor();
                //求解
                dX = Matrix.LeastSquaresSolve(A, L);
                //更正
                u += (float)dX[0, 0];
                v += (float)dX[1, 0];
                by = bx * u;
                bz=bx*v;
                RightEE.Phi += (float)dX[2, 0];
                RightEE.Omega += (float)dX[3, 0];
                RightEE.Kappa += (float)dX[4, 0];
                //找到最小值
                foreach (double i in dX)
                    if (Math.Abs(i) < min)
                        min = Math.Abs(i);
            } while (min > 0.00003);//精度判断
        }

        //计算误差方程的系数矩阵和常数项
        private void CalFactor()
        {
            int num = leftMCoords.Count();
            float deno, N1, N2;
            MyPoint temp;
            for(int i=0;i<num;i++)
            {
                //求解第i+1个点的像空间辅助坐标
                temp = RightEE.Photo2Model(rightPCoords[i]);
                //计算点投影系数
                deno = leftMCoords[i].X * temp.Z - temp.X * leftMCoords[i].Z;
                N1 = (bx * temp.Z - bz * temp.X) / deno;
                N2 = (bx * leftMCoords[i].Z - bz * leftMCoords[i].X) / deno;
                //为系数矩阵赋值
                A[i, 0] = bx;
                A[i, 1] = -temp.Y / temp.Z * bx;
                A[i, 2] = -temp.X * temp.Y / temp.Z * N2;
                A[i, 3] = -(temp.Z + temp.Y * temp.Y / temp.Z) * N2;
                A[i, 4] = temp.X * N2;
                L[i, 0] = N1 * leftMCoords[i].Y - N2 * temp.Y - by;
            }  
        }
    }
}
