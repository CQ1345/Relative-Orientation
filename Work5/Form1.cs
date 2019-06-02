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
        RelativeOritation ro;
        string filename;

        //读取数据
        private void Button1_Click(object sender, EventArgs e)
        {
            leftPCoords = new List<MyPoint>();
            rightPCoords = new List<MyPoint>();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"D:\教案\大二下 摄影测量\作业\Work5\数据";
            dialog.RestoreDirectory = true;
            if(dialog.ShowDialog()==DialogResult.OK)
            {
                filename = dialog.FileName;
                float f;
                string line;
                string[] coord;
                MyPoint temp;
                using (StreamReader sr = new StreamReader(filename))
                {
                    line = sr.ReadLine();
                    f = float.Parse(line);
                    sr.ReadLine();
                    while((line=sr.ReadLine())!=null&&line.Length!=0)
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
            }

        }

        //计算
        private void Button2_Click(object sender, EventArgs e)
        {
            ro = new RelativeOritation(leftPCoords, rightPCoords);
            ro.CalROE();
            textBox1.Text = ro.by.ToString();
            textBox2.Text = ro.bz.ToString();
            textBox3.Text = (ro.RightEE.Phi / Math.PI * 180).ToString();
            textBox4.Text = (ro.RightEE.Omega / Math.PI * 180).ToString();
            textBox5.Text = (ro.RightEE.Kappa / Math.PI * 180).ToString();
        }

        //保存
        private void Button3_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw=File.AppendText(filename))
            {
                float phi = (float)(ro.RightEE.Phi / Math.PI * 180);
                float omega = (float)(ro.RightEE.Omega / Math.PI * 180);
                float kappa = (float)(ro.RightEE.Kappa / Math.PI * 180);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("{0},{1}", ro.by, ro.bz);
                sw.WriteLine("{0},{1},{2}", phi, omega, kappa);
            }
        }
    }
}
