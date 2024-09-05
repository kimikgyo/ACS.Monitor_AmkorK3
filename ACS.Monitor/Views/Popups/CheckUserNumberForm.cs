using Dapper;
using log4net;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace ACS.Monitor
{
    public partial class CheckUserNumberForm : Form
    {
        private readonly static string connectionString = ConnectionStrings.DB2;
        private readonly static ILog EventLogger = LogManager.GetLogger("Event");
        private readonly static ILog UserLogger = LogManager.GetLogger("User");
        private readonly static ILog TimeoutLogger = LogManager.GetLogger("Timeout");

        private UserNumberInfo userNumberInfo;

        public CheckUserNumberForm()
        {
            InitializeComponent();
        }

        private UserNumberInfo DBLoad(string UserNumber)
        {
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    return con.QueryFirstOrDefault<UserNumberInfo>("SELECT * FROM UserNumber WHERE UserNumber=@UserNumber;", param: new { UserNumber });
                }
            }
            catch (Exception ex)
            {
                EventLogger.Info("CheckUserNumberForm() Fail = " + ex);
                return null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            UserNumberInfo info = DBLoad(txtUserNumber.Text.ToString());

            if (info != null)
            {
                ChangePasswordForm passwordForm = new ChangePasswordForm(info);
                passwordForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("해당 사원은 존재하지 않습니다.");
                return;
            }
        }
    }
}
