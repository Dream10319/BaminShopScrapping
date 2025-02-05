namespace BaeminShopScrapping
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.Lat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Lon = new System.Windows.Forms.TextBox();
            this.LocationNum = new System.Windows.Forms.Label();
            this.Category = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.ShopCounter = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CategoryUpdown = new System.Windows.Forms.NumericUpDown();
            this.OffsetUpdown = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryUpdown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetUpdown)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(78, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(224, 184);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 143);
            this.progressBar1.Maximum = 10000;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(352, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Latitude";
            // 
            // Lat
            // 
            this.Lat.Location = new System.Drawing.Point(63, 58);
            this.Lat.Name = "Lat";
            this.Lat.Size = new System.Drawing.Size(100, 20);
            this.Lat.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Longitude";
            // 
            // Lon
            // 
            this.Lon.Location = new System.Drawing.Point(264, 58);
            this.Lon.Name = "Lon";
            this.Lon.Size = new System.Drawing.Size(100, 20);
            this.Lon.TabIndex = 6;
            // 
            // LocationNum
            // 
            this.LocationNum.AutoSize = true;
            this.LocationNum.Location = new System.Drawing.Point(70, 122);
            this.LocationNum.Name = "LocationNum";
            this.LocationNum.Size = new System.Drawing.Size(44, 13);
            this.LocationNum.TabIndex = 7;
            this.LocationNum.Text = "location";
            // 
            // Category
            // 
            this.Category.AutoSize = true;
            this.Category.Location = new System.Drawing.Point(160, 122);
            this.Category.Name = "Category";
            this.Category.Size = new System.Drawing.Size(48, 13);
            this.Category.TabIndex = 8;
            this.Category.Text = "category";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 33);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Option";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(200, 10);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(60, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Manual";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(48, 10);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(64, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "FromFile";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // ShopCounter
            // 
            this.ShopCounter.AutoSize = true;
            this.ShopCounter.Location = new System.Drawing.Point(265, 122);
            this.ShopCounter.Name = "ShopCounter";
            this.ShopCounter.Size = new System.Drawing.Size(43, 13);
            this.ShopCounter.TabIndex = 10;
            this.ShopCounter.Text = "counter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Offset";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(179, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Category Index";
            // 
            // CategoryUpdown
            // 
            this.CategoryUpdown.Location = new System.Drawing.Point(264, 85);
            this.CategoryUpdown.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.CategoryUpdown.Name = "CategoryUpdown";
            this.CategoryUpdown.Size = new System.Drawing.Size(100, 20);
            this.CategoryUpdown.TabIndex = 14;
            // 
            // OffsetUpdown
            // 
            this.OffsetUpdown.Location = new System.Drawing.Point(63, 84);
            this.OffsetUpdown.Name = "OffsetUpdown";
            this.OffsetUpdown.Size = new System.Drawing.Size(100, 20);
            this.OffsetUpdown.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 219);
            this.Controls.Add(this.OffsetUpdown);
            this.Controls.Add(this.CategoryUpdown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ShopCounter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Category);
            this.Controls.Add(this.LocationNum);
            this.Controls.Add(this.Lon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Lat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "BaeminShopScrapping";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryUpdown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetUpdown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Lat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Lon;
        private System.Windows.Forms.Label LocationNum;
        private System.Windows.Forms.Label Category;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label ShopCounter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown CategoryUpdown;
        private System.Windows.Forms.NumericUpDown OffsetUpdown;
    }
}

