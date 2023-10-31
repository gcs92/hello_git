namespace vlcplay
{
    partial class Mysql
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
            this.button_search = new System.Windows.Forms.Button();
            this.textBox_cmd = new System.Windows.Forms.TextBox();
            this.button_add = new System.Windows.Forms.Button();
            this.button_del = new System.Windows.Forms.Button();
            this.button_change = new System.Windows.Forms.Button();
            this.textBox_show = new System.Windows.Forms.TextBox();
            this.textBox_add = new System.Windows.Forms.TextBox();
            this.textBox_del = new System.Windows.Forms.TextBox();
            this.textBox_change = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.序号 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.age = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.school = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.city = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.日期 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // button_search
            // 
            this.button_search.Location = new System.Drawing.Point(30, 21);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(50, 23);
            this.button_search.TabIndex = 1;
            this.button_search.Text = "查询:";
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // textBox_cmd
            // 
            this.textBox_cmd.Location = new System.Drawing.Point(86, 23);
            this.textBox_cmd.Name = "textBox_cmd";
            this.textBox_cmd.Size = new System.Drawing.Size(375, 21);
            this.textBox_cmd.TabIndex = 2;
            this.textBox_cmd.Text = "select * from gcstest";
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(30, 50);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(50, 23);
            this.button_add.TabIndex = 4;
            this.button_add.Text = "增加:";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_del
            // 
            this.button_del.Location = new System.Drawing.Point(463, 21);
            this.button_del.Name = "button_del";
            this.button_del.Size = new System.Drawing.Size(48, 23);
            this.button_del.TabIndex = 5;
            this.button_del.Text = "删除:";
            this.button_del.UseVisualStyleBackColor = true;
            this.button_del.Click += new System.EventHandler(this.button_del_Click);
            // 
            // button_change
            // 
            this.button_change.Location = new System.Drawing.Point(463, 50);
            this.button_change.Name = "button_change";
            this.button_change.Size = new System.Drawing.Size(48, 23);
            this.button_change.TabIndex = 6;
            this.button_change.Text = "改变:";
            this.button_change.UseVisualStyleBackColor = true;
            this.button_change.Click += new System.EventHandler(this.button_change_Click);
            // 
            // textBox_show
            // 
            this.textBox_show.Location = new System.Drawing.Point(468, 77);
            this.textBox_show.Multiline = true;
            this.textBox_show.Name = "textBox_show";
            this.textBox_show.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_show.Size = new System.Drawing.Size(400, 430);
            this.textBox_show.TabIndex = 3;
            // 
            // textBox_add
            // 
            this.textBox_add.Location = new System.Drawing.Point(86, 50);
            this.textBox_add.Name = "textBox_add";
            this.textBox_add.Size = new System.Drawing.Size(375, 21);
            this.textBox_add.TabIndex = 7;
            // 
            // textBox_del
            // 
            this.textBox_del.Location = new System.Drawing.Point(517, 23);
            this.textBox_del.Name = "textBox_del";
            this.textBox_del.Size = new System.Drawing.Size(350, 21);
            this.textBox_del.TabIndex = 12;
            // 
            // textBox_change
            // 
            this.textBox_change.Location = new System.Drawing.Point(517, 50);
            this.textBox_change.Name = "textBox_change";
            this.textBox_change.Size = new System.Drawing.Size(350, 21);
            this.textBox_change.TabIndex = 13;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.序号,
            this.ID,
            this.name,
            this.age,
            this.school,
            this.city,
            this.日期});
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(36, 77);
            this.listView1.Name = "listView1";
            this.listView1.RightToLeftLayout = true;
            this.listView1.Size = new System.Drawing.Size(425, 430);
            this.listView1.TabIndex = 14;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // 序号
            // 
            this.序号.Text = "序号";
            // 
            // ID
            // 
            this.ID.Text = "ID";
            // 
            // name
            // 
            this.name.Text = "name";
            // 
            // age
            // 
            this.age.Text = "age";
            // 
            // school
            // 
            this.school.Text = "school";
            // 
            // city
            // 
            this.city.Text = "city";
            // 
            // 日期
            // 
            this.日期.Text = "日期";
            // 
            // Mysql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 539);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBox_change);
            this.Controls.Add(this.textBox_del);
            this.Controls.Add(this.textBox_add);
            this.Controls.Add(this.button_change);
            this.Controls.Add(this.button_del);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.textBox_show);
            this.Controls.Add(this.textBox_cmd);
            this.Controls.Add(this.button_search);
            this.Name = "Mysql";
            this.Text = "Mysql测试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Mysql_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_search;
        private System.Windows.Forms.TextBox textBox_cmd;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_del;
        private System.Windows.Forms.Button button_change;
        private System.Windows.Forms.TextBox textBox_show;
        private System.Windows.Forms.TextBox textBox_add;
        private System.Windows.Forms.TextBox textBox_del;
        private System.Windows.Forms.TextBox textBox_change;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader age;
        private System.Windows.Forms.ColumnHeader school;
        private System.Windows.Forms.ColumnHeader city;
        private System.Windows.Forms.ColumnHeader 序号;
        private System.Windows.Forms.ColumnHeader 日期;
    }
}