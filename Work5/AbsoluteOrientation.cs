using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work5
{
    class AbsoluteOrientation
    {
        EElements e;
        List<MyPoint> pCoords;//点的摄影测量坐标
        double[,] ATA;//ATA系数矩阵
        double[,] ATL;//ATL常数项向量

        public float Phi { get => e.Phi; }
        public float Omega { get => e.Omega; }
        public float Kappa { get => e.Kappa; }
        public float dx { get; private set; }
        public float dy { get; private set; }
        public float dz { get; private set; }
        public float lamda { get; private set; }

        public AbsoluteOrientation()
        { }
        public AbsoluteOrientation(EElements e, float lamda, float dx, float dy, float dz)
        {
            this.e = e;
            this.lamda = lamda;
            this.dx = dx;
            this.dy = dy;
            this.dz = dz;
        }

        //解算绝对定向元素
        public void CalAOE(List<MyPoint> GCP,List<MyPoint> PCP)
        {
            ATA = new double[7, 7];
            ATL = new double[7, 1];
            double[,] dX;
            double[,] Xtpg, Xpg, delta;

            //初始化绝对定向元素的初始值
            e = new EElements(0, 0, 0);
            lamda = 1;
            dx = dy = dz = 0;
            //坐标重心化
            GetGravityCoords(ref GCP, ref PCP, out Xtpg, out Xpg);
            do
            {
                //计算系数
                CalFactors(GCP, PCP);
                //求解改正数
                dX = Matrix.Mutiply(Matrix.Inverse(ATA), ATL);
                //改正
                lamda += (float)dX[3, 0];
                e.Phi += (float)dX[4, 0];
                e.Omega += (float)dX[5, 0];
                e.Kappa += (float)dX[6, 0];
            } while (IsQualified(dX));

            //变换为真正的绝对定向元素
            Xpg = Matrix.Mutiply(lamda, Xpg);
            delta = Matrix.Subtract(Xtpg, Matrix.Mutiply(e.R, Xpg));
            dx = (float)delta[0, 0];
            dy = (float)delta[1, 0];
            dz = (float)delta[2, 0];
        }

        //坐标重心化
        private void GetGravityCoords(ref List<MyPoint> GCP, ref List<MyPoint> PCP,
            out double[,] Xtpg, out double[,] Xpg)
        {
            int num = GCP.Count();
            //计算坐标重心
            MyPoint pCenter = new MyPoint(0, 0, 0);//摄影测量坐标的重心
            MyPoint gCenter = new MyPoint(0, 0, 0);//地面摄影测量坐标的重心
            for (int i = 0; i < num; i++)
            {
                pCenter.X += PCP[i].X;
                pCenter.Y += PCP[i].Y;
                pCenter.Z += PCP[i].Z;
                gCenter.X += GCP[i].X;
                gCenter.Y += GCP[i].Y;
                gCenter.Z += GCP[i].Z;
            }
            pCenter.X /= num;
            pCenter.Y /= num;
            pCenter.Z /= num;
            gCenter.X /= num;
            gCenter.Y /= num;
            gCenter.Z /= num;
            Xtpg = new double[3, 1] { { gCenter.X }, { gCenter.Y }, { gCenter.Z } };
            Xpg=new double[3, 1] { { pCenter.X }, { pCenter.Y }, { pCenter.Z } };

            //坐标重心化
            for (int i = 0; i < num; i++)
            {
                PCP[i].X -= pCenter.X;
                PCP[i].Y -= pCenter.Y;
                PCP[i].Z -= pCenter.Z;
                GCP[i].X -= gCenter.X;
                GCP[i].Y -= gCenter.Y;
                GCP[i].Z -= gCenter.Z;
            }
        }

        //计算系数
        private void CalFactors(List<MyPoint> GCP,List<MyPoint> PCP)
        {
            int num = GCP.Count();
            float X2, Y2, Z2;
            double[,] Ltp = new double[3, 1];
            double[,] Lp = new double[3, 1];
            double[,] L = new double[3, 1];

            ATA[0, 0] = ATA[1, 1] = ATA[2, 2] = num;
            
            for(int i=0;i<num;i++)
            {
                X2 = PCP[i].X * PCP[i].X;
                Y2 = PCP[i].Y * PCP[i].Y;
                Z2 = PCP[i].Z * PCP[i].Z;
                ATA[3, 3] += X2 + Y2 + Z2;
                ATA[4, 4] += X2 + Z2;
                ATA[5, 5] += Y2 + Z2;
                ATA[6, 6] += X2 + Y2;
                ATA[4, 5] += PCP[i].X * PCP[i].Y;
                ATA[4, 6] += PCP[i].Y * PCP[i].Z;
                ATA[5, 6] -= PCP[i].X * PCP[i].Z;

                Ltp[0, 0] = GCP[i].X;
                Ltp[1, 0] = GCP[i].Y;
                Ltp[2, 0] = GCP[i].Z;
                Lp[0, 0] = lamda * PCP[i].X;
                Lp[1, 0] = lamda * PCP[i].Y;
                Lp[2, 0] = lamda * PCP[i].Z;
                L = Matrix.Subtract(Ltp, Matrix.Mutiply(e.R, Lp));
                ATL[3, 0] += PCP[i].X * L[0, 0] + PCP[i].Y * L[1, 0] + PCP[i].Z * L[2, 0];
                ATL[4, 0] += PCP[i].X * L[2, 0] - PCP[i].Z * L[0, 0];
                ATL[5, 0] += PCP[i].Y * L[2, 0] - PCP[i].Z * L[1, 0];
                ATL[6, 0] += PCP[i].X * L[1, 0] - PCP[i].Y * L[0, 0];
            }
            ATA[5, 4] = ATA[4, 5];
            ATA[6, 4] = ATA[4, 6];
            ATA[6, 5] = ATA[5, 6];
        }

        //精度判断
        private bool IsQualified(double[,] dX)
        {
            double min = 100;
            double error = 0.00001;
            for(int i=0;i<dX.Length;i++)
                if(Math.Abs(dX[i,0])<min)
                    min = Math.Abs(dX[i, 0]);
            if (min < error)
                return true;
            else
                return false;
        }

        //计算地面摄影测量坐标
        public List<MyPoint> CalGPCoords(List<MyPoint> PCoords)
        {
            List<MyPoint> GPCoords = new List<MyPoint>();
            int num = PCoords.Count();
            double[,] Xtp;
            double[,] dX = { { dx }, { dy }, { dz } };
            double[,] Xp = new double[3, 1];
            MyPoint temp;

            for(int i=0;i<num;i++)
            {
                Xp[0, 0] = lamda * PCoords[i].X;
                Xp[1, 0] = lamda * PCoords[i].Y;
                Xp[2, 0] = lamda * PCoords[i].Z;
                Xtp = Matrix.Add(Matrix.Mutiply(e.R, Xp), dX);
                temp = new MyPoint();
                temp.X = (float)Xtp[0, 0];
                temp.Y = (float)Xtp[1, 0];
                temp.Z = (float)Xtp[2, 0];
                GPCoords.Add(temp);
            }
            return GPCoords;
        }
    }
}
