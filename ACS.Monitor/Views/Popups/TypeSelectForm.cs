using System;
using System.Windows.Forms;

namespace ACS.Monitor.UI
{
    public partial class TypeSelectForm : Form
    {
        private string inputValue = string.Empty;
        private string drawNo = string.Empty;

        public TypeSelectForm()
        {
            InitializeComponent();
        }

        public string InputValue
        {
            get { return inputValue; }
            set { inputValue = value; }
        }

        private void Type_Select(object sender, EventArgs e)
        {
            string Type_Select = ((Button)sender).Text;
            this.inputValue = Type_Select;
            DialogResult = DialogResult.OK;
        }
    }
}
