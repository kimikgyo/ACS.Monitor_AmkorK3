
namespace ACS.Monitor
{
    partial class UserLoginForm
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
            this.txt_password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_UserNumber = new System.Windows.Forms.TextBox();
            this.lbl_Login = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Cancel = new DevExpress.XtraEditors.LabelControl();
            this.lbl_UserNumber = new DevExpress.XtraEditors.LabelControl();
            this.lbl_LonigImage = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // txt_password
            // 
            this.txt_password.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_password.Location = new System.Drawing.Point(108, 147);
            this.txt_password.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(55, 29);
            this.txt_password.TabIndex = 151;
            this.txt_password.UseSystemPasswordChar = true;
            this.txt_password.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-11, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 37);
            this.label1.TabIndex = 152;
            this.label1.Text = "비밀번호";
            this.label1.Visible = false;
            // 
            // txt_UserNumber
            // 
            this.txt_UserNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txt_UserNumber.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_UserNumber.Location = new System.Drawing.Point(154, 67);
            this.txt_UserNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_UserNumber.Name = "txt_UserNumber";
            this.txt_UserNumber.Size = new System.Drawing.Size(186, 29);
            this.txt_UserNumber.TabIndex = 147;
            // 
            // lbl_Login
            // 
            this.lbl_Login.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(174)))), ((int)(((byte)(234)))));
            this.lbl_Login.Appearance.Options.UseBackColor = true;
            this.lbl_Login.Appearance.Options.UseTextOptions = true;
            this.lbl_Login.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbl_Login.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_Login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Login.Location = new System.Drawing.Point(35, 118);
            this.lbl_Login.Name = "lbl_Login";
            this.lbl_Login.Size = new System.Drawing.Size(305, 40);
            this.lbl_Login.TabIndex = 153;
            this.lbl_Login.Text = "Login";
            // 
            // lbl_Cancel
            // 
            this.lbl_Cancel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(174)))), ((int)(((byte)(234)))));
            this.lbl_Cancel.Appearance.Options.UseBackColor = true;
            this.lbl_Cancel.Appearance.Options.UseTextOptions = true;
            this.lbl_Cancel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbl_Cancel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Cancel.Location = new System.Drawing.Point(35, 179);
            this.lbl_Cancel.Name = "lbl_Cancel";
            this.lbl_Cancel.Size = new System.Drawing.Size(305, 40);
            this.lbl_Cancel.TabIndex = 154;
            this.lbl_Cancel.Text = "Cancel";
            // 
            // lbl_UserNumber
            // 
            this.lbl_UserNumber.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lbl_UserNumber.Appearance.Options.UseBackColor = true;
            this.lbl_UserNumber.Appearance.Options.UseTextOptions = true;
            this.lbl_UserNumber.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbl_UserNumber.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_UserNumber.Location = new System.Drawing.Point(35, 64);
            this.lbl_UserNumber.Name = "lbl_UserNumber";
            this.lbl_UserNumber.Size = new System.Drawing.Size(113, 37);
            this.lbl_UserNumber.TabIndex = 155;
            this.lbl_UserNumber.Text = "UserNamber";
            // 
            // lbl_LonigImage
            // 
            this.lbl_LonigImage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_LonigImage.Location = new System.Drawing.Point(35, 1);
            this.lbl_LonigImage.Name = "lbl_LonigImage";
            this.lbl_LonigImage.Size = new System.Drawing.Size(305, 57);
            this.lbl_LonigImage.TabIndex = 156;
            // 
            // UserLoginForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 231);
            this.Controls.Add(this.lbl_LonigImage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_UserNumber);
            this.Controls.Add(this.lbl_Cancel);
            this.Controls.Add(this.lbl_Login);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_UserNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(30, 0);
            this.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserLoginForm";
            this.Text = "XtraForm1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_UserNumber;
        private DevExpress.XtraEditors.LabelControl lbl_Login;
        private DevExpress.XtraEditors.LabelControl lbl_Cancel;
        private DevExpress.XtraEditors.LabelControl lbl_UserNumber;
        private DevExpress.XtraEditors.LabelControl lbl_LonigImage;
    }
}