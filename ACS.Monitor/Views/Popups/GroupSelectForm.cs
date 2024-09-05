using System;
using System.Windows.Forms;

namespace ACS.Monitor.UI
{
    public partial class GroupSelectForm : Form
    {
        private MainForm mainForm;

        private string inputValue = string.Empty;
        private string drawNo = string.Empty;

        public GroupSelectForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            subFunc_cbo_Robot_Group_Select_ListAdd();
        }

        public string InputValue
        {
            get { return inputValue; }
            set { inputValue = value; }
        }

        void subFunc_cbo_Robot_Group_Select_ListAdd()
        {
            try
            {
                cbo_Group_Select.Items.Clear();
                cbo_Group_Select.Items.Add("Hook_Rail");
                cbo_Group_Select.Items.Add("Lift_Recliner");

            }
            catch
            {
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (cbo_Group_Select.Text.Length > 0)
            {
                this.inputValue = cbo_Group_Select.Text;
                DialogResult = DialogResult.OK;
            }
            else
            {
                //mainForm.subFuncMessagePopUp("Robot Group이 선택되지 않았습니다.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.inputValue = string.Empty;
            Close();
        }
    }
}
