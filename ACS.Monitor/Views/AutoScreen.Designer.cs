namespace ACS.Monitor
{
    partial class AutoScreen
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
            this.AutoDisplay_Timer = new System.Windows.Forms.Timer(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ucMapView1 = new ACS.Monitor.UCMapView();
            this.splitContainerControl3 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ucMapView2 = new ACS.Monitor.UCMapView();
            this.ucMapView3 = new ACS.Monitor.UCMapView();
            this.RobotGridControl = new DevExpress.XtraGrid.GridControl();
            this.RobotGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).BeginInit();
            this.splitContainerControl2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).BeginInit();
            this.splitContainerControl2.Panel2.SuspendLayout();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3.Panel1)).BeginInit();
            this.splitContainerControl3.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3.Panel2)).BeginInit();
            this.splitContainerControl3.Panel2.SuspendLayout();
            this.splitContainerControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RobotGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RobotGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // AutoDisplay_Timer
            // 
            this.AutoDisplay_Timer.Enabled = true;
            this.AutoDisplay_Timer.Interval = 500;
            this.AutoDisplay_Timer.Tick += new System.EventHandler(this.AutoDisplay_Timer_Tick);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(12, 12);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.RobotGridControl);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1086, 412);
            this.splitContainerControl1.SplitterPosition = 207;
            this.splitContainerControl1.TabIndex = 9;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl2.Location = new System.Drawing.Point(3, 3);
            this.splitContainerControl2.Name = "splitContainerControl2";
            // 
            // splitContainerControl2.Panel1
            // 
            this.splitContainerControl2.Panel1.Controls.Add(this.ucMapView1);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            // 
            // splitContainerControl2.Panel2
            // 
            this.splitContainerControl2.Panel2.Controls.Add(this.splitContainerControl3);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1083, 224);
            this.splitContainerControl2.SplitterPosition = 392;
            this.splitContainerControl2.TabIndex = 0;
            // 
            // ucMapView1
            // 
            this.ucMapView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMapView1.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucMapView1.Appearance.Options.UseFont = true;
            this.ucMapView1.Location = new System.Drawing.Point(2, 5);
            this.ucMapView1.MapID = null;
            this.ucMapView1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ucMapView1.Name = "ucMapView1";
            this.ucMapView1.Size = new System.Drawing.Size(387, 215);
            this.ucMapView1.TabIndex = 11;
            this.ucMapView1.UriStr = null;
            // 
            // splitContainerControl3
            // 
            this.splitContainerControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl3.Location = new System.Drawing.Point(2, 4);
            this.splitContainerControl3.Name = "splitContainerControl3";
            // 
            // splitContainerControl3.Panel1
            // 
            this.splitContainerControl3.Panel1.Controls.Add(this.ucMapView2);
            this.splitContainerControl3.Panel1.Text = "Panel1";
            // 
            // splitContainerControl3.Panel2
            // 
            this.splitContainerControl3.Panel2.Controls.Add(this.ucMapView3);
            this.splitContainerControl3.Panel2.Text = "Panel2";
            this.splitContainerControl3.Size = new System.Drawing.Size(676, 228);
            this.splitContainerControl3.SplitterPosition = 353;
            this.splitContainerControl3.TabIndex = 0;
            // 
            // ucMapView2
            // 
            this.ucMapView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMapView2.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucMapView2.Appearance.Options.UseFont = true;
            this.ucMapView2.Location = new System.Drawing.Point(0, 0);
            this.ucMapView2.MapID = null;
            this.ucMapView2.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ucMapView2.Name = "ucMapView2";
            this.ucMapView2.Size = new System.Drawing.Size(347, 216);
            this.ucMapView2.TabIndex = 9;
            this.ucMapView2.UriStr = null;
            // 
            // ucMapView3
            // 
            this.ucMapView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMapView3.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucMapView3.Appearance.Options.UseFont = true;
            this.ucMapView3.Location = new System.Drawing.Point(2, 1);
            this.ucMapView3.MapID = null;
            this.ucMapView3.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ucMapView3.Name = "ucMapView3";
            this.ucMapView3.Size = new System.Drawing.Size(309, 215);
            this.ucMapView3.TabIndex = 10;
            this.ucMapView3.UriStr = null;
            // 
            // RobotGridControl
            // 
            this.RobotGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RobotGridControl.Location = new System.Drawing.Point(3, 16);
            this.RobotGridControl.MainView = this.RobotGridView;
            this.RobotGridControl.Name = "RobotGridControl";
            this.RobotGridControl.Size = new System.Drawing.Size(1080, 176);
            this.RobotGridControl.TabIndex = 8;
            this.RobotGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.RobotGridView});
            // 
            // RobotGridView
            // 
            this.RobotGridView.GridControl = this.RobotGridControl;
            this.RobotGridView.Name = "RobotGridView";
            // 
            // AutoScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1110, 436);
            this.Controls.Add(this.splitContainerControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AutoScreen";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).EndInit();
            this.splitContainerControl2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).EndInit();
            this.splitContainerControl2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3.Panel1)).EndInit();
            this.splitContainerControl3.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3.Panel2)).EndInit();
            this.splitContainerControl3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).EndInit();
            this.splitContainerControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RobotGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RobotGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer AutoDisplay_Timer;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl RobotGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView RobotGridView;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private UCMapView ucMapView1;
        private UCMapView ucMapView2;
        private UCMapView ucMapView3;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl3;
    }
}