using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Work5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<MyPoint> leftPCoords;
        List<MyPoint> rightPCoords;
        List<MyPoint> result;//坐标计算结果
        List<MyPoint> PCoords;
        int scale;
        RelativeOritation ro;
        AbsoluteOrientation ao;
        string photofile;

        //计算相对定向元素
        private void 计算相对定向元素ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (GetStereopair() == false)//从文件读取同名像点坐标
                return;
            MyPoint center = new MyPoint(0, 0, 0);
            EElements leftE = new EElements(center, 0, 0, 0);
            ro = new RelativeOritation(leftPCoords, rightPCoords, leftE, scale);
            ro.CalROE();
            SaveROE();
        }

        //计算模型坐标
        private void 计算模型坐标ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            result = ro.GetModelCoords();
            Output();
        }

        //读取同名像点坐标
        private bool GetStereopair()
        {
            leftPCoords = new List<MyPoint>();
            rightPCoords = new List<MyPoint>();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.RestoreDirectory = true;
            DialogResult flag = dialog.ShowDialog();
            if (flag != DialogResult.OK)
                return false;
            else
            {
                photofile = dialog.FileName;
                float f;
                string line;
                string[] coord;
                MyPoint temp;
                using (StreamReader sr = new StreamReader(photofile))
                {
                    line = sr.ReadLine();
                    f = float.Parse(line);
                    line = sr.ReadLine();
                    scale = int.Parse(line);
                    sr.ReadLine();
                    while ((line = sr.ReadLine()) != null && line.Length != 0)
                    {
                        coord = line.Split(',');
                        temp = new MyPoint();
                        temp.X = float.Parse(coord[0]);
                        temp.Y = float.Parse(coord[1]);
                        temp.Z = -f;
                        leftPCoords.Add(temp);
                        temp = new MyPoint();
                        temp.X = float.Parse(coord[2]);
                        temp.Y = float.Parse(coord[3]);
                        temp.Z = -f;
                        rightPCoords.Add(temp);
                    }
                }
                return true;
            }
        }

        //将坐标显示到datagridview中
        private void Output()
        {
            int num = 0;
            if(dataGridView1.Rows.Count<result.Count())
            {
                foreach (MyPoint i in result)
                {
                    num = dataGridView1.Rows.Add();
                    dataGridView1.Rows[num].Cells[0].Value = num + 1;
                    dataGridView1.Rows[num].Cells[1].Value = i.X;
                    dataGridView1.Rows[num].Cells[2].Value = i.Y;
                    dataGridView1.Rows[num].Cells[3].Value = i.Z;
                }
            }
            else
            {
                foreach (MyPoint i in result)
                {
                    dataGridView1.Rows[num].Cells[0].Value = num + 1;
                    dataGridView1.Rows[num].Cells[1].Value = i.X;
                    dataGridView1.Rows[num].Cells[2].Value = i.Y;
                    dataGridView1.Rows[num].Cells[3].Value = i.Z;
                    num++;
                }
            }
        }

        //将坐标保存至文件
        private void 保存坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Filter = "文本文件(*.txt)|*.txt";
            if (dialog.ShowDialog()==DialogResult.OK)
            {
                string file = dialog.FileName;
                int num = 1;
                using(StreamWriter sw=new StreamWriter(file))
                {
                    foreach(MyPoint i in result)
                        sw.WriteLine("{0},{1},{2},{3}", num++, i.X, i.Y, i.Z);
                }
            }
        }

        //计算摄影测量坐标
        private void 计算摄影测量坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            result = ro.GetPhotogrammetryCoords();
            Output();
        }

        //保存相对定向元素
        private void SaveROE()
        {
            using (StreamWriter sw = File.AppendText(photofile))
            {
                float phi = (float)(ro.RightEE.Phi / Math.PI * 180);
                float omega = (float)(ro.RightEE.Omega / Math.PI * 180);
                float kappa = (float)(ro.RightEE.Kappa / Math.PI * 180);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("{0},{1},{2}", ro.bx, ro.by, ro.bz);
                sw.WriteLine("{0},{1},{2}", phi, omega, kappa);
            }
        }
        //保存绝对定向元素
        private void SaveAOE()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "保存绝对定向元素";
            dialog.Filter = "文本文件(*.txt)|*.txt";
            DialogResult flag = dialog.ShowDialog();
            if (flag != DialogResult.OK)
                return;
            else
            {
                string filename = dialog.FileName;
                float tran = (float)(180 / Math.PI);
                using (StreamWriter sw=new StreamWriter(filename))
                {
                    sw.WriteLine("{0}", ao.lamda);
                    sw.WriteLine("{0},{1},{2}", ao.dx, ao.dy, ao.dz);
                    sw.WriteLine("{0},{1},{2}", ao.Phi * tran, ao.Omega * tran, ao.Kappa * tran);
                }
            }
        }

        //解算绝对定向元素
        private void 解算绝对定向元素ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<MyPoint> GCP, PCP;
            string title;

            //读取控制点坐标
            bool flag;
            title= "选择控制点的地面摄影测量坐标";
            GCP = GetPoints(title, out flag);
            if (flag == false)
                return;
            title = "选择控制点的摄影测量坐标";
            PCP = GetPoints(title,out flag);
            if (flag == false)
                return;

            //解算
            ao = new AbsoluteOrientation();
            ao.CalAOE(GCP, PCP);

            //保存绝对定向元素
            SaveAOE();
        }

        //从文件读取绝对定向元素
        private void 输入绝对定向元素ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EElements ee = new EElements();
            float lamda;
            float dx, dy, dz;
            
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Title = "选择绝对定向元素";
            dialog.Filter = "文本文件(*.txt)|*.txt";
            DialogResult flag = dialog.ShowDialog();
            if (flag == DialogResult.Cancel)
                return;
            else if (flag == DialogResult.OK)
            {
                string filename = dialog.FileName;
                string[] data;
                using (StreamReader sr=new StreamReader(filename))
                {
                    lamda = float.Parse(sr.ReadLine());
                    data = sr.ReadLine().Split(',');
                    dx = float.Parse(data[0]);
                    dy = float.Parse(data[1]);
                    dz = float.Parse(data[2]);
                    data = sr.ReadLine().Split(',');
                    ee.Phi = (float)(float.Parse(data[0]) / 180 * Math.PI);
                    ee.Omega = (float)(float.Parse(data[1]) / 180 * Math.PI);
                    ee.Kappa = (float)(float.Parse(data[2]) / 180 * Math.PI);
                    ao = new AbsoluteOrientation(ee, lamda, dx, dy, dz);
                }
            }
        }

        //计算地面摄影测量坐标系
        private void 计算地面摄影测量坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //读取点的摄影测量坐标
            bool flag;
            string title = "选取点的摄影测量坐标";
            PCoords = GetPoints(title, out flag);
            if (flag == false)
                return;

            result = ao.CalGPCoords(PCoords);//计算点的地面摄影测量坐标
            Output();//显示结果
        }

        //读取坐标
        private List<MyPoint> GetPoints(string title,out bool flag)
        {
            List<MyPoint> coords = new List<MyPoint>();
            MyPoint temp;
            string filename;
            string line;
            string[] data;

            //读取控制点地面摄影测量坐标
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = title;
            dialog.Filter = "文本文件(*.txt)|*.txt";
            DialogResult result = dialog.ShowDialog();
            if(result!=DialogResult.OK)
            {
                flag = false;
                return coords;
            }
            else
            {
                flag = true;
                filename = dialog.FileName;
                using (StreamReader sr = new StreamReader(filename))
                {
                    while ((line = sr.ReadLine()) != null && line.Length != 0)
                    {
                        data = line.Split(',');
                        temp = new MyPoint();
                        temp.X = float.Parse(data[1]);
                        temp.Y = float.Parse(data[2]);
                        temp.Z = float.Parse(data[3]);
                        coords.Add(temp);
                    }
                }
            }
            return coords;
        }
    }
}
