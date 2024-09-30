
namespace ACS.Monitor
{
    partial class SettingSystem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingSystem));
            this.RobotSelectGridControl = new DevExpress.XtraGrid.GridControl();
            this.RobotSelectGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.FloorSelectGridControl = new DevExpress.XtraGrid.GridControl();
            this.FloorSelectGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.RobotSelectGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RobotSelectGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloorSelectGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloorSelectGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // RobotSelectGridControl
            // 
            this.RobotSelectGridControl.Location = new System.Drawing.Point(0, 35);
            this.RobotSelectGridControl.MainView = this.RobotSelectGridView;
            this.RobotSelectGridControl.Name = "RobotSelectGridControl";
            this.RobotSelectGridControl.Size = new System.Drawing.Size(309, 419);
            this.RobotSelectGridControl.TabIndex = 0;
            this.RobotSelectGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.RobotSelectGridView});
            // 
            // RobotSelectGridView
            // 
            this.RobotSelectGridView.GridControl = this.RobotSelectGridControl;
            this.RobotSelectGridView.Name = "RobotSelectGridView";
            // 
            // FloorSelectGridControl
            // 
            this.FloorSelectGridControl.Location = new System.Drawing.Point(0, 35);
            this.FloorSelectGridControl.MainView = this.FloorSelectGridView;
            this.FloorSelectGridControl.Name = "FloorSelectGridControl";
            this.FloorSelectGridControl.Size = new System.Drawing.Size(309, 419);
            this.FloorSelectGridControl.TabIndex = 0;
            this.FloorSelectGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.FloorSelectGridView});
            // 
            // FloorSelectGridView
            // 
            this.FloorSelectGridView.GridControl = this.FloorSelectGridControl;
            this.FloorSelectGridView.Name = "FloorSelectGridView";
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Options.UseForeColor = true;
            this.groupControl1.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl1.CaptionImageOptions.Image")));
            this.groupControl1.Controls.Add(this.RobotSelectGridControl);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(314, 460);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "RobotSelect";
            // 
            // groupControl2
            // 
            this.groupControl2.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl2.CaptionImageOptions.Image")));
            this.groupControl2.Controls.Add(this.FloorSelectGridControl);
            this.groupControl2.Location = new System.Drawing.Point(332, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(314, 460);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "FloorSelect";
            // 
            // DisplayTimer
            // 
            this.DisplayTimer.Enabled = true;
            this.DisplayTimer.Interval = 1000;
            this.DisplayTimer.Tick += new System.EventHandler(this.DisplayTimer_Tick);
            // 
            // SettingSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(1178, 474);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Name = "SettingSystem";
            this.Text = "SettingSystem";
            ((System.ComponentModel.ISupportInitialize)(this.RobotSelectGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RobotSelectGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloorSelectGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloorSelectGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl RobotSelectGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView RobotSelectGridView;
        private DevExpress.XtraGrid.GridControl FloorSelectGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView FloorSelectGridView;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.Timer DisplayTimer;
    }
}