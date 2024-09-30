using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACS.Monitor
{
    public partial class UserLoginForm : DevExpress.XtraEditors.XtraForm
    {
        public string UserNumber = string.Empty;
        public string UserPassword = string.Empty;
        private static string AmkorImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "LogInAmkorImage.png");
        Image AmkorImage = Image.FromFile(AmkorImagePath);

        public UserLoginForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            lbl_LonigImage.ImageOptions.Image = AmkorImage;
            lbl_Login.Click += LabelClick;
            lbl_Cancel.Click += LabelClick;

        }

        private void LabelClick(object sender, EventArgs e)
        {
            string labelName = ((LabelControl)sender).Name;
            switch (labelName)
            {
                case "lbl_Login":
                    UserNumber = txt_UserNumber.Text.Trim();
                    DialogResult = DialogResult.Yes;
                    break;
                case "lbl_Cancel":
                    DialogResult = DialogResult.Cancel;
                    break;
            }
        }
    }
}