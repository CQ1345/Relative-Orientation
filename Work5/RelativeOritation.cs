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
        private EElements leftEE;//左像片的外方位元素
        private List<MyPoint> leftPCoords;//左像片的像空间坐标
        private List<MyPoint> rightPCoords;//右像片的像空间坐标
        private List<MyPoint> leftMCoords;//左像片的像空间辅助坐标
        private int scale;//航摄比例尺

        private double[,] A;//系数矩阵
        private double[,] L;//常数项

        public EElements RightEE { get; }
        public float by { get; private set; }
        public float bz { get; private set; }
        public float bx { get; private set; }


        //初始化
        public RelativeOritation(List<MyPoint> leftC,List<MyPoint> rightC,EElements leftE,int s)
        {
            scale = s;
            //左外方位元素
            leftEE = leftE;
            //右外方位元素
            RightEE = new EElements();
            RightEE.Phi = RightEE.Omega = RightEE.Kappa = 0;
            RightEE.S = new MyPoint(0, 0, 0);
            bx = leftC[0].X - rightC[0].X;
            //获取坐标
            leftPCoords = leftC;//左像片的像空间辅助坐标
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

            //计算左像片的像空间辅助坐标
            CalLeftMCoords();

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
            int num = leftPCoords.Count();
            float deno, N1, N2;
            MyPoint temp2;
            for(int i=0;i<num;i++)
            {
                //求解第i+1个点的像空间辅助坐标
                temp2 = RightEE.Photo2Model(rightPCoords[i]);//右片
                //计算点投影系数
                deno = leftMCoords[i].X * temp2.Z - temp2.X * leftMCoords[i].Z;
                N1 = (bx * temp2.Z - bz * temp2.X) / deno;
                N2 = (bx * leftMCoords[i].Z - bz * leftMCoords[i].X) / deno;
                //为系数矩阵赋值
                A[i, 0] = bx;
                A[i, 1] = -temp2.Y / temp2.Z * bx;
                A[i, 2] = -temp2.X * temp2.Y / temp2.Z * N2;
                A[i, 3] = -(temp2.Z + temp2.Y * temp2.Y / temp2.Z) * N2;
                A[i, 4] = temp2.X * N2;
                L[i, 0] = N1 * leftMCoords[i].Y - N2 * temp2.Y - by;
            }  
        }

        //计算左像片的像空间辅助坐标
        private void CalLeftMCoords()
        {
            int num = leftPCoords.Count();
            leftMCoords = new List<MyPoint>();
            for (int i = 0; i < num; i++)
                leftMCoords.Add(leftEE.Photo2Model(leftPCoords[i]));
        }

        //计算点的模型坐标
        public List<MyPoint> GetModelCoords()
        {
            List<MyPoint> modelCoords = new List<MyPoint>();
            int num = leftMCoords.Count();
            float deno, N1, N2;
            float f = -leftPCoords[0].Z;
            MyPoint temp;

            for (int i=0;i<num;i++)
            {
                //计算第i+1个点在右片中的像空间辅助坐标
                temp = RightEE.Photo2Model(rightPCoords[i]);
                //计算点投影系数
                deno = leftMCoords[i].X * temp.Z - temp.X * leftMCoords[i].Z;
                N1 = (bx * temp.Z - bz * temp.X) / deno;
                N2 = (bx * leftMCoords[i].Z - bz * leftMCoords[i].X) / deno;
                //计算点的模型坐标
                temp.X = N1 * leftMCoords[i].X;
                temp.Y = (N1 * leftMCoords[i].Y + N2 * temp.Y + by) / 2;
                temp.Z = N1 * leftMCoords[i].Z;
                modelCoords.Add(temp);
            }

            return modelCoords;

        }

        //计算点的摄影测量坐标
        public List<MyPoint> GetPhotogrammetryCoords()
        {
            List<MyPoint> photogrammtryCoords = new List<MyPoint>();
            int num = leftMCoords.Count();
            float deno, N1, N2;
            float s = (float)(scale / 1000.0);
            float f = -leftPCoords[0].Z;
            MyPoint temp;

            for (int i=0;i<num;i++)
            {
                //计算第i+1个点在右片中的像空间辅助坐标
                temp = RightEE.Photo2Model(rightPCoords[i]);
                //计算点投影系数
                deno = leftMCoords[i].X * temp.Z - temp.X * leftMCoords[i].Z;
                N1 = (bx * temp.Z - bz * temp.X) / deno;
                N2 = (bx * leftMCoords[i].Z - bz * leftMCoords[i].X) / deno;
                //计算点的模型坐标
                temp.X = N1 * leftMCoords[i].X * s;
                temp.Y = (N1 * leftMCoords[i].Y + N2 * temp.Y + by) / 2 * s;
                temp.Z = N1 * leftMCoords[i].Z * s + s * f;
                photogrammtryCoords.Add(temp);
            }

            return photogrammtryCoords;
        }
    }
}
