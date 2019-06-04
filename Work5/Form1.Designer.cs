namespace Work5
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.计算相对定向元素ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算相对定向元素ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.计算模型坐标ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.计算摄影测量坐标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算模型坐标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.解算绝对定向元素ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.输入绝对定向元素ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算地面摄影测量坐标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存坐标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.计算相对定向元素ToolStripMenuItem,
            this.计算模型坐标ToolStripMenuItem,
            this.保存坐标ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(947, 32);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 计算相对定向元素ToolStripMenuItem
            // 
            this.计算相对定向元素ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.计算相对定向元素ToolStripMenuItem1,
            this.计算模型坐标ToolStripMenuItem1,
            this.计算摄影测量坐标ToolStripMenuItem});
            this.计算相对定向元素ToolStripMenuItem.Name = "计算相对定向元素ToolStripMenuItem";
            this.计算相对定向元素ToolStripMenuItem.Size = new System.Drawing.Size(98, 28);
            this.计算相对定向元素ToolStripMenuItem.Text = "相对定向";
            // 
            // 计算相对定向元素ToolStripMenuItem1
            // 
            this.计算相对定向元素ToolStripMenuItem1.Name = "计算相对定向元素ToolStripMenuItem1";
            this.计算相对定向元素ToolStripMenuItem1.Size = new System.Drawing.Size(254, 34);
            this.计算相对定向元素ToolStripMenuItem1.Text = "解算相对定向元素";
            this.计算相对定向元素ToolStripMenuItem1.Click += new System.EventHandler(this.计算相对定向元素ToolStripMenuItem1_Click);
            // 
            // 计算模型坐标ToolStripMenuItem1
            // 
            this.计算模型坐标ToolStripMenuItem1.Name = "计算模型坐标ToolStripMenuItem1";
            this.计算模型坐标ToolStripMenuItem1.Size = new System.Drawing.Size(254, 34);
            this.计算模型坐标ToolStripMenuItem1.Text = "计算模型坐标";
            this.计算模型坐标ToolStripMenuItem1.Click += new System.EventHandler(this.计算模型坐标ToolStripMenuItem1_Click);
            // 
            // 计算摄影测量坐标ToolStripMenuItem
            // 
            this.计算摄影测量坐标ToolStripMenuItem.Name = "计算摄影测量坐标ToolStripMenuItem";
            this.计算摄影测量坐标ToolStripMenuItem.Size = new System.Drawing.Size(254, 34);
            this.计算摄影测量坐标ToolStripMenuItem.Text = "计算摄影测量坐标";
            this.计算摄影测量坐标ToolStripMenuItem.Click += new System.EventHandler(this.计算摄影测量坐标ToolStripMenuItem_Click);
            // 
            // 计算模型坐标ToolStripMenuItem
            // 
            this.计算模型坐标ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.解算绝对定向元素ToolStripMenuItem,
            this.输入绝对定向元素ToolStripMenuItem,
            this.计算地面摄影测量坐标ToolStripMenuItem});
            this.计算模型坐标ToolStripMenuItem.Name = "计算模型坐标ToolStripMenuItem";
            this.计算模型坐标ToolStripMenuItem.Size = new System.Drawing.Size(98, 28);
            this.计算模型坐标ToolStripMenuItem.Text = "绝对定向";
            // 
            // 解算绝对定向元素ToolStripMenuItem
            // 
            this.解算绝对定向元素ToolStripMenuItem.Name = "解算绝对定向元素ToolStripMenuItem";
            this.解算绝对定向元素ToolStripMenuItem.Size = new System.Drawing.Size(290, 34);
            this.解算绝对定向元素ToolStripMenuItem.Text = "解算绝对定向元素";
            this.解算绝对定向元素ToolStripMenuItem.Click += new System.EventHandler(this.解算绝对定向元素ToolStripMenuItem_Click);
            // 
            // 输入绝对定向元素ToolStripMenuItem
            // 
            this.输入绝对定向元素ToolStripMenuItem.Name = "输入绝对定向元素ToolStripMenuItem";
            this.输入绝对定向元素ToolStripMenuItem.Size = new System.Drawing.Size(290, 34);
            this.输入绝对定向元素ToolStripMenuItem.Text = "输入绝对定向元素";
            this.输入绝对定向元素ToolStripMenuItem.Click += new System.EventHandler(this.输入绝对定向元素ToolStripMenuItem_Click);
            // 
            // 计算地面摄影测量坐标ToolStripMenuItem
            // 
            this.计算地面摄影测量坐标ToolStripMenuItem.Name = "计算地面摄影测量坐标ToolStripMenuItem";
            this.计算地面摄影测量坐标ToolStripMenuItem.Size = new System.Drawing.Size(290, 34);
            this.计算地面摄影测量坐标ToolStripMenuItem.Text = "计算地面摄影测量坐标";
            this.计算地面摄影测量坐标ToolStripMenuItem.Click += new System.EventHandler(this.计算地面摄影测量坐标ToolStripMenuItem_Click);
            // 
            // 保存坐标ToolStripMenuItem
            // 
            this.保存坐标ToolStripMenuItem.Name = "保存坐标ToolStripMenuItem";
            this.保存坐标ToolStripMenuItem.Size = new System.Drawing.Size(98, 28);
            this.保存坐标ToolStripMenuItem.Text = "保存坐标";
            this.保存坐标ToolStripMenuItem.Click += new System.EventHandler(this.保存坐标ToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.X,
            this.Y,
            this.Z});
            this.dataGridView1.Location = new System.Drawing.Point(12, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(935, 403);
            this.dataGridView1.TabIndex = 14;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 8;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 150;
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.MinimumWidth = 8;
            this.X.Name = "X";
            this.X.ReadOnly = true;
            this.X.Width = 150;
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.MinimumWidth = 8;
            this.Y.Name = "Y";
            this.Y.ReadOnly = true;
            this.Y.Width = 150;
            // 
            // Z
            // 
            this.Z.HeaderText = "Z";
            this.Z.MinimumWidth = 8;
            this.Z.Name = "Z";
            this.Z.ReadOnly = true;
            this.Z.Width = 150;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "双像解析计算";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 计算相对定向元素ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计算相对定向元素ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 计算模型坐标ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 计算摄影测量坐标ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计算模型坐标ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 解算绝对定向元素ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计算地面摄影测量坐标ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 输入绝对定向元素ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
        private System.Windows.Forms.ToolStripMenuItem 保存坐标ToolStripMenuItem;
    }
}