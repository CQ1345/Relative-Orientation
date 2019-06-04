using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work5
{
    static class Matrix
    {
        static double[,] mat;
        //加法
        public static double[,] Add(double[,] a,double[,] b)
        {
            double[,] result;
            int m = a.GetLength(0);
            int n = b.GetLength(1);
            result =new double [m,n];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    result[i, j] = a[i, j] + b[i, j];
            return result;
        }

        //减法
        public static double[,] Subtract(double[,] a, double[,] b)
        {
            double[,] result;
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            result = new double[m, n];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    result[i, j] = a[i, j] - b[i, j];
            return result;
        }

        //乘法
        public static double[,] Mutiply(double[,] a, double[,] b)
        {
            int m = a.GetLength(0);
            int h = a.GetLength(1);
            int n = b.GetLength(1);
            double[,] result = new double[m, n];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = 0;
                    for (int q = 0; q < h; q++)
                        result[i, j] += a[i, q] * b[q, j];
                }
            return result;
        }
        public static double[,] Mutiply(double lamda,double[,] matrix)
        {
            int r = matrix.GetLength(0);
            int c = matrix.GetLength(1);
            double[,] result = new double[r, c];
            for (int i = 0; i < r; i++)
                for (int j = 0; j < c; j++)
                    result[i, j] = matrix[i, j] * lamda;
            return result;
        }
        
        //求逆
        public static double[,] Inverse(double[,] a)
        {
            int r = a.GetLength(0);
            mat = a;
            for (int i = 0; i < r; i++)
                for (int j = 0; j < r; j++)
                    mat[i, j] = a[i, j];
            double[,] result = new double[r, r];
            double det = Det(a);
            if (det == 0)
                return result;
            for (int i=0; i < r; i++)
                for (int j = 0; j < r; j++)
                    result[i,j] = Confactor(j, i)/det;
            return result;
        }

        //行列式
        public static double Det(double[,] input)
        {
            double result = 1;
            double inter;
            int linechange;
            int n;//用来记录特定行的位置
            int r = input.GetLength(0);
            double[,] a = new double[r, r];

            //将原始矩阵的数据转移到临时矩阵中，避免改变原始矩阵
            for (int i = 0; i < r; i++)
                for (int j = 0; j < r; j++)
                    a[i, j] = input[i, j];

            //将矩阵通过行变换，变为上三角方阵
            for (int j = 0; j < r - 1; j++)//控制列
            {
                linechange = 1;
                inter = a[j, j];//当前列的对角线元素
                n = j;//记录当前的行号
                while (inter == 0 && n < r - 1)//检验是否为零
                {
                    n++;
                    if (n == r)
                        return 0;
                    inter = a[n, j];//依次取对角线元素所在列的剩余元素，直到有非零项
                }
                //换行
                if (n != j)
                {
                    for (int i = j; i < r; i++)//控制列
                    {
                        inter = a[j, i];
                        a[j, i] = a[n, i];
                        a[n, i] = inter;
                    }
                    linechange = -1;
                }
                //将当前所在列剩余的元素变为0，并调整对应行其他元素的值
                for (int i = j + 1; i < r; i++)//控制行
                {
                    if (a[i, j] == 0)//遇到0，直接跳过
                        continue;
                    for (int h = j + 1; h < r; h++)//控制列
                    {
                        a[i, h] = a[i, h] - a[j, h] * a[i, j] / a[j, j];
                    }

                }
                result = result * a[j, j] * linechange;//记录当前主对角线元素的乘积
            }
            return result * a[r - 1, r - 1];
        }

        //计算代数余子式
        static double Confactor(int i, int j)
        { 
            int n=mat.GetLength (0);
            double result;
            double[,] factor=new double[n-1,n-1];
            int c=0;
            int d=0;
            for (int row = 0; row < n; row++ )
            {
                if (row == i) continue;
                for (int col = 0; col < n; col++ )
                {
                    if (col == j) continue;
                    factor[c, d++] = mat[row, col];
                }
                c++;
                d = 0;
            }
            result = Det(factor);
            i = i + j;
            if (i % 2 != 0)
                result = -result;
            return result;
        }

        //矩阵转置
        public static double[,] Transpose(double[,] a)
        {
            int r = a.GetLength(0);
            int c = a.GetLength(1);
            double[,] result = new double[c, r];
            for (int i = 0; i < c; i++)
                for (int j = 0; j < r; j++)
                    result[i, j] = a[j, i];     
            return result;
        }

        //最小二乘解算
        public static double[,] LeastSquaresSolve(double[,] A,double[,] L)
        {
            double[,] AT = Transpose(A);
            double[,] ATA = Mutiply(AT, A);
            double[,] ATL = Mutiply(AT, L);
            ATA = Inverse(ATA);
            return Mutiply(ATA, ATL);
        }
    }
}
 