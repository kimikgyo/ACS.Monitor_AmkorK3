
namespace ACS.Monitor
{
    partial class UCMapView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cb_DisplayInfo = new DevExpress.XtraEditors.CheckEdit();
            this.chkCustomMap = new DevExpress.XtraEditors.CheckEdit();
            this.lbl_ClickPosInfo = new DevExpress.XtraEditors.LabelControl();
            this.btnMapDownload = new DevExpress.XtraEditors.SimpleButton();
            this.button3 = new DevExpress.XtraEditors.SimpleButton();
            this.pictureBox1 = new DevExpress.XtraEditors.PictureEdit();
            this.Map_ID = new DevExpress.XtraEditors.LabelControl();
            this.button1 = new DevExpress.XtraEditors.SimpleButton();
            this.button2 = new DevExpress.XtraEditors.SimpleButton();
            this.textBox1 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_DisplayInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCustomMap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_DisplayInfo
            // 
            this.cb_DisplayInfo.Location = new System.Drawing.Point(95, 60);
            this.cb_DisplayInfo.Name = "cb_DisplayInfo";
            this.cb_DisplayInfo.Properties.Caption = "display info.";
            this.cb_DisplayInfo.Size = new System.Drawing.Size(107, 20);
            this.cb_DisplayInfo.TabIndex = 0;
            // 
            // chkCustomMap
            // 
            this.chkCustomMap.Location = new System.Drawing.Point(208, 60);
            this.chkCustomMap.Name = "chkCustomMap";
            this.chkCustomMap.Properties.Caption = "custom map";
            this.chkCustomMap.Size = new System.Drawing.Size(107, 20);
            this.chkCustomMap.TabIndex = 1;
            // 
            // lbl_ClickPosInfo
            // 
            this.lbl_ClickPosInfo.Location = new System.Drawing.Point(105, 86);
            this.lbl_ClickPosInfo.Name = "lbl_ClickPosInfo";
            this.lbl_ClickPosInfo.Size = new System.Drawing.Size(31, 14);
            this.lbl_ClickPosInfo.TabIndex = 2;
            this.lbl_ClickPosInfo.Text = "label1";
            // 
            // btnMapDownload
            // 
            this.btnMapDownload.Location = new System.Drawing.Point(266, 3);
            this.btnMapDownload.Name = "btnMapDownload";
            this.btnMapDownload.Size = new System.Drawing.Size(95, 23);
            this.btnMapDownload.TabIndex = 3;
            this.btnMapDownload.Text = "download map";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(367, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "button3";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(6, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureBox1.Size = new System.Drawing.Size(438, 523);
            this.pictureBox1.TabIndex = 5;
            // 
            // Map_ID
            // 
            this.Map_ID.Appearance.BackColor = System.Drawing.Color.White;
            this.Map_ID.Appearance.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Map_ID.Appearance.Options.UseBackColor = true;
            this.Map_ID.Appearance.Options.UseFont = true;
            this.Map_ID.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.Map_ID.Location = new System.Drawing.Point(6, 14);
            this.Map_ID.Name = "Map_ID";
            this.Map_ID.Size = new System.Drawing.Size(93, 126);
            this.Map_ID.TabIndex = 6;
            this.Map_ID.Text = "labelControl2";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(220, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(314, 29);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Button2";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(81, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(133, 20);
            this.textBox1.TabIndex = 9;
            // 
            // UCMapView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_ClickPosInfo);
            this.Controls.Add(this.chkCustomMap);
            this.Controls.Add(this.cb_DisplayInfo);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Map_ID);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnMapDownload);
            this.Name = "UCMapView";
            this.Size = new System.Drawing.Size(446, 580);
            ((System.ComponentModel.ISupportInitialize)(this.cb_DisplayInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCustomMap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit cb_DisplayInfo;
        private DevExpress.XtraEditors.CheckEdit chkCustomMap;
        private DevExpress.XtraEditors.LabelControl lbl_ClickPosInfo;
        private DevExpress.XtraEditors.SimpleButton btnMapDownload;
        private DevExpress.XtraEditors.SimpleButton button3;
        private DevExpress.XtraEditors.PictureEdit pictureBox1;
        private DevExpress.XtraEditors.LabelControl Map_ID;
        private DevExpress.XtraEditors.SimpleButton button1;
        private DevExpress.XtraEditors.SimpleButton button2;
        private DevExpress.XtraEditors.TextEdit textBox1;
    }
}
