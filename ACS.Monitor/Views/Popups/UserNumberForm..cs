using System;
using System.Windows.Forms;



namespace ACS.Monitor
{
    public partial class UserNumberForm : Form
    {
        public string UserNumber = string.Empty;
        public string UserPassword = string.Empty;
        public int Update_index = 0;

        public UserNumberForm()
        {
            InitializeComponent();

            this.AcceptButton = btnLogin;
            this.CancelButton = btnCancel;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserNumber = txt_UserNumber.Text.Trim();
            UserPassword = txt_password.Text.Trim();
            Update_index = 1;
            DialogResult = DialogResult.Yes;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnpassword_Click(object sender, EventArgs e)
        {
            CheckUserNumberForm checkUser = new CheckUserNumberForm();
            checkUser.ShowDialog();
        }
    }
}
