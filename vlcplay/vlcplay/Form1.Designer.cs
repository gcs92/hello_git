
namespace vlcplay
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button_Mysql = new System.Windows.Forms.Button();
            this.button_flow = new System.Windows.Forms.Button();
            this.button_SQLite = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "视频测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_Mysql
            // 
            this.button_Mysql.Location = new System.Drawing.Point(12, 41);
            this.button_Mysql.Name = "button_Mysql";
            this.button_Mysql.Size = new System.Drawing.Size(114, 23);
            this.button_Mysql.TabIndex = 2;
            this.button_Mysql.Text = "mysql数据库测试";
            this.button_Mysql.UseVisualStyleBackColor = true;
            this.button_Mysql.Click += new System.EventHandler(this.button_Mysql_Click);
            // 
            // button_flow
            // 
            this.button_flow.Location = new System.Drawing.Point(12, 99);
            this.button_flow.Name = "button_flow";
            this.button_flow.Size = new System.Drawing.Size(114, 23);
            this.button_flow.TabIndex = 3;
            this.button_flow.Text = "光流数据测试";
            this.button_flow.UseVisualStyleBackColor = true;
            this.button_flow.Click += new System.EventHandler(this.button_flow_Click);
            // 
            // button_SQLite
            // 
            this.button_SQLite.Location = new System.Drawing.Point(12, 70);
            this.button_SQLite.Name = "button_SQLite";
            this.button_SQLite.Size = new System.Drawing.Size(114, 23);
            this.button_SQLite.TabIndex = 4;
            this.button_SQLite.Text = "SQLite数据库测试";
            this.button_SQLite.UseVisualStyleBackColor = true;
            this.button_SQLite.Click += new System.EventHandler(this.button_SQLite_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 170);
            this.Controls.Add(this.button_SQLite);
            this.Controls.Add(this.button_flow);
            this.Controls.Add(this.button_Mysql);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "测试主题";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_Mysql;
        private System.Windows.Forms.Button button_flow;
        private System.Windows.Forms.Button button_SQLite;
    }
}

