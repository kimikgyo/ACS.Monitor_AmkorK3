
namespace ACS.Monitor
{
    partial class RobotScreen
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
            this.components = new System.ComponentModel.Container();
            this.RobotGridControl = new DevExpress.XtraGrid.GridControl();
            this.RobotGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.RobotGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RobotGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // RobotGridControl
            // 
            this.RobotGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RobotGridControl.Location = new System.Drawing.Point(-1, 0);
            this.RobotGridControl.MainView = this.RobotGridView;
            this.RobotGridControl.Name = "RobotGridControl";
            this.RobotGridControl.Size = new System.Drawing.Size(783, 314);
            this.RobotGridControl.TabIndex = 9;
            this.RobotGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.RobotGridView});
            // 
            // RobotGridView
            // 
            this.RobotGridView.GridControl = this.RobotGridControl;
            this.RobotGridView.Name = "RobotGridView";
            // 
            // DisplayTimer
            // 
            this.DisplayTimer.Enabled = true;
            this.DisplayTimer.Interval = 1000;
            this.DisplayTimer.Tick += new System.EventHandler(this.DisplayTimer_Tick);
            // 
            // RobotScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 314);
            this.Controls.Add(this.RobotGridControl);
            this.Name = "RobotScreen";
            this.Text = "RobotScreen";
            ((System.ComponentModel.ISupportInitialize)(this.RobotGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RobotGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl RobotGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView RobotGridView;
        private System.Windows.Forms.Timer DisplayTimer;
    }
}