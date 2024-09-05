
namespace ACS.Monitor.MapUpload
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
            this.btnCustomMapUpload = new System.Windows.Forms.Button();
            this.btnCustomMapDownload = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMapDownload = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCustomMapUpload
            // 
            this.btnCustomMapUpload.Location = new System.Drawing.Point(459, 74);
            this.btnCustomMapUpload.Name = "btnCustomMapUpload";
            this.btnCustomMapUpload.Size = new System.Drawing.Size(160, 29);
            this.btnCustomMapUpload.TabIndex = 0;
            this.btnCustomMapUpload.Text = "Upload Custom Map";
            this.btnCustomMapUpload.UseVisualStyleBackColor = true;
            this.btnCustomMapUpload.Click += new System.EventHandler(this.btnCustomMapUpload_Click);
            // 
            // btnCustomMapDownload
            // 
            this.btnCustomMapDownload.Location = new System.Drawing.Point(459, 39);
            this.btnCustomMapDownload.Name = "btnCustomMapDownload";
            this.btnCustomMapDownload.Size = new System.Drawing.Size(160, 29);
            this.btnCustomMapDownload.TabIndex = 1;
            this.btnCustomMapDownload.Text = "Download Custom Map";
            this.btnCustomMapDownload.UseVisualStyleBackColor = true;
            this.btnCustomMapDownload.Click += new System.EventHandler(this.btnCustomMapDownload_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(16, 33);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.Text = "M3F";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(16, 68);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(44, 16);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "T3F";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(407, 120);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map Selection";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(97, 67);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(290, 21);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "ee59d352-4e1b-11ed-80b2-000129af8f1d";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(97, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(290, 21);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "55023e70-4df7-11ed-80b2-000129af8f1d";
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(798, 110);
            this.listBox1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnMapDownload);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnCustomMapUpload);
            this.panel1.Controls.Add(this.btnCustomMapDownload);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 148);
            this.panel1.TabIndex = 5;
            // 
            // btnMapDownload
            // 
            this.btnMapDownload.Location = new System.Drawing.Point(650, 39);
            this.btnMapDownload.Name = "btnMapDownload";
            this.btnMapDownload.Size = new System.Drawing.Size(140, 64);
            this.btnMapDownload.TabIndex = 0;
            this.btnMapDownload.Text = "Download Fleet Map";
            this.btnMapDownload.UseVisualStyleBackColor = true;
            this.btnMapDownload.Click += new System.EventHandler(this.btnMapDownload_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 148);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(804, 116);
            this.panel2.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 264);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Map Image Upload/Downloader";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCustomMapUpload;
        private System.Windows.Forms.Button btnCustomMapDownload;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnMapDownload;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
    }
}

