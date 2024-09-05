using Dapper;
using log4net;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace ACS.Monitor
{
    public partial class ChangePasswordForm : Form
    {
        private readonly static string connectionString = ConnectionStrings.DB2;
        private readonly static ILog EventLogger = LogManager.GetLogger("Event");
        private readonly static ILog UserLogger = LogManager.GetLogger("User");
        private readonly static ILog TimeoutLogger = LogManager.GetLogger("Timeout");

        private UserNumberInfo userNumberInfo;

        public ChangePasswordForm(UserNumberInfo info)
        {
            InitializeComponent();

            userNumberInfo = info;
            Layout(info);
        }

        private void Layout(UserNumberInfo info)
        {
            if (info.UserPassword == null)
            {
                panel1.Visible = true;
                panel2.Visible = false;
            }
            else
            {
                panel1.Visible = false;
                panel2.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (panel1.Visible == true)
            {
                string UserPassword = txtSetPassword.Text.ToString();

                try
                {
                    using (var con = new SqlConnection(connectionString))
                    {
                        con.QueryFirstOrDefault<UserNumberInfo>("UPDATE UserNumber SET UserPassword = @UserPassword WHERE UserNumber = @UserNumber and ID = @ID;", 
                            param: new { UserPassword, userNumberInfo.UserNumber, userNumberInfo.Id });

                        MessageBox.Show("비밀번호가 설정되었습니다.");
                        this.Close();
                    }
                }
                catch(Exception ex)
                {
                    EventLogger.Info("비밀번호 세팅중 에러 발생" + ex.ToString());
                }
            }
            else
            {
                string confirmPassword = txtConfirmPassword.Text.ToString();
                string NewPassword = txtNewPassword.Text.ToString();
                string NewCheckPassword = txtNewCheckPassword.Text.ToString();

                if (userNumberInfo.UserPassword == confirmPassword)
                {
                    if (NewPassword == NewCheckPassword)
                    {
                        using (var con = new SqlConnection(connectionString))
                        {
                            string UserPassword = NewPassword;
                            con.QueryFirstOrDefault<UserNumberInfo>("UPDATE UserNumber SET UserPassword = @UserPassword WHERE UserNumber = @UserNumber and ID = @ID;",
                                param: new { UserPassword, userNumberInfo.UserNumber, userNumberInfo.Id });

                            MessageBox.Show("비밀번호가 변경되었습니다.");
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("새 비밀번호와 새 비밀번호 확인이 맞지 않습니다.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("현재 비밀번호가 맞지 않습니다.");
                    return;
                }
            }
        }
    }
}
