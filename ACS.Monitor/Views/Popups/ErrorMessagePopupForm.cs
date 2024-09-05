﻿using System;
using System.Windows.Forms;

namespace ACS.Monitor.Views.Popups
{
    public partial class ErrorMessagePopupForm : Form
    {
        public string Message { get; }

        public ErrorMessagePopupForm(string message)
        {
            InitializeComponent();
            this.Text = DateTime.Now.ToString();
            this.Message = message;
            lbl_Message.Text = message;
        }

        public void AutoClose()
        {
            btnClose_Click(this, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // 경광등 alarm off
            //var f = new TowerLamp(); f.AlarmOff(); f.Close();

            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
