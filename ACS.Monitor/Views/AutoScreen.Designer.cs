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
            this.SP_Middle = new System.Windows.Forms.SplitContainer();
            this.ucMapView3 = new ACS.Monitor.UCMapView();
            this.ucMapView4 = new ACS.Monitor.UCMapView();
            this.SP_Bottom = new System.Windows.Forms.SplitContainer();
            this.ucMapView5 = new ACS.Monitor.UCMapView();
            this.ucMapView6 = new ACS.Monitor.UCMapView();
            this.splitContainer2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainer3 = new DevExpress.XtraEditors.SplitContainerControl();
            this.SP_Top = new DevExpress.XtraEditors.SplitContainerControl();
            this.ucMapView1 = new ACS.Monitor.UCMapView();
            this.ucMapView2 = new ACS.Monitor.UCMapView();
            this.dtgAuto_MiR_Status = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.SP_Middle)).BeginInit();
            this.SP_Middle.Panel1.SuspendLayout();
            this.SP_Middle.Panel2.SuspendLayout();
            this.SP_Middle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SP_Bottom)).BeginInit();
            this.SP_Bottom.Panel1.SuspendLayout();
            this.SP_Bottom.Panel2.SuspendLayout();
            this.SP_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2.Panel1)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2.Panel2)).BeginInit();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3.Panel1)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3.Panel2)).BeginInit();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SP_Top)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_Top.Panel1)).BeginInit();
            this.SP_Top.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SP_Top.Panel2)).BeginInit();
            this.SP_Top.Panel2.SuspendLayout();
            this.SP_Top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAuto_MiR_Status)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AutoDisplay_Timer
            // 
            this.AutoDisplay_Timer.Interval = 500;
            this.AutoDisplay_Timer.Tick += new System.EventHandler(this.AutoDisplay_Timer_Tick);
            // 
            // SP_Middle
            // 
            this.SP_Middle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SP_Middle.Location = new System.Drawing.Point(4, 14);
            this.SP_Middle.Name = "SP_Middle";
            // 
            // SP_Middle.Panel1
            // 
            this.SP_Middle.Panel1.Controls.Add(this.ucMapView3);
            // 
            // SP_Middle.Panel2
            // 
            this.SP_Middle.Panel2.Controls.Add(this.ucMapView4);
            this.SP_Middle.Size = new System.Drawing.Size(1063, 36);
            this.SP_Middle.SplitterDistance = 520;
            this.SP_Middle.TabIndex = 1;
            // 
            // ucMapView3
            // 
            this.ucMapView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMapView3.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucMapView3.Appearance.Options.UseFont = true;
            this.ucMapView3.Location = new System.Drawing.Point(2, 4);
            this.ucMapView3.MapID = null;
            this.ucMapView3.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ucMapView3.Name = "ucMapView3";
            this.ucMapView3.Size = new System.Drawing.Size(513, 28);
            this.ucMapView3.TabIndex = 0;
            this.ucMapView3.UriStr = null;
            // 
            // ucMapView4
            // 
            this.ucMapView4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMapView4.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucMapView4.Appearance.Options.UseFont = true;
            this.ucMapView4.Location = new System.Drawing.Point(2, 4);
            this.ucMapView4.MapID = null;
            this.ucMapView4.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ucMapView4.Name = "ucMapView4";
            this.ucMapView4.Size = new System.Drawing.Size(522, 28);
            this.ucMapView4.TabIndex = 0;
            this.ucMapView4.UriStr = null;
            // 
            // SP_Bottom
            // 
            this.SP_Bottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SP_Bottom.Location = new System.Drawing.Point(7, 13);
            this.SP_Bottom.Name = "SP_Bottom";
            // 
            // SP_Bottom.Panel1
            // 
            this.SP_Bottom.Panel1.Controls.Add(this.ucMapView5);
            // 
            // SP_Bottom.Panel2
            // 
            this.SP_Bottom.Panel2.Controls.Add(this.ucMapView6);
            this.SP_Bottom.Size = new System.Drawing.Size(1063, 31);
            this.SP_Bottom.SplitterDistance = 522;
            this.SP_Bottom.TabIndex = 2;
            // 
            // ucMapView5
            // 
            this.ucMapView5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMapView5.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucMapView5.Appearance.Options.UseFont = true;
            this.ucMapView5.Location = new System.Drawing.Point(4, 4);
            this.ucMapView5.MapID = null;
            this.ucMapView5.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ucMapView5.Name = "ucMapView5";
            this.ucMapView5.Size = new System.Drawing.Size(514, 27);
            this.ucMapView5.TabIndex = 1;
            this.ucMapView5.UriStr = null;
            // 
            // ucMapView6
            // 
            this.ucMapView6.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucMapView6.Appearance.Options.UseFont = true;
            this.ucMapView6.Location = new System.Drawing.Point(3, 4);
            this.ucMapView6.MapID = null;
            this.ucMapView6.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ucMapView6.Name = "ucMapView6";
            this.ucMapView6.Size = new System.Drawing.Size(515, 77);
            this.ucMapView6.TabIndex = 1;
            this.ucMapView6.UriStr = null;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainer2.Horizontal = false;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            this.splitContainer2.Panel1.Text = "Panel1";
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.SP_Bottom);
            this.splitContainer2.Panel2.Text = "Panel2";
            this.splitContainer2.Size = new System.Drawing.Size(1083, 192);
            this.splitContainer2.SplitterPosition = 135;
            this.splitContainer2.TabIndex = 2;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainer3.Horizontal = false;
            this.splitContainer3.Location = new System.Drawing.Point(3, 7);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.SP_Top);
            this.splitContainer3.Panel1.Text = "Panel1";
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.SP_Middle);
            this.splitContainer3.Panel2.Text = "Panel2";
            this.splitContainer3.Size = new System.Drawing.Size(1077, 119);
            this.splitContainer3.SplitterPosition = 47;
            this.splitContainer3.TabIndex = 1;
            // 
            // SP_Top
            // 
            this.SP_Top.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SP_Top.Location = new System.Drawing.Point(6, 5);
            this.SP_Top.Name = "SP_Top";
            // 
            // SP_Top.Panel1
            // 
            this.SP_Top.Panel1.Controls.Add(this.ucMapView1);
            this.SP_Top.Panel1.Text = "Panel1";
            // 
            // SP_Top.Panel2
            // 
            this.SP_Top.Panel2.Controls.Add(this.ucMapView2);
            this.SP_Top.Panel2.Text = "Panel2";
            this.SP_Top.Size = new System.Drawing.Size(1061, 34);
            this.SP_Top.SplitterPosition = 506;
            this.SP_Top.TabIndex = 1;
            // 
            // ucMapView1
            // 
            this.ucMapView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMapView1.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucMapView1.Appearance.Options.UseFont = true;
            this.ucMapView1.Location = new System.Drawing.Point(6, 8);
            this.ucMapView1.MapID = null;
            this.ucMapView1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ucMapView1.Name = "ucMapView1";
            this.ucMapView1.Size = new System.Drawing.Size(494, 26);
            this.ucMapView1.TabIndex = 0;
            this.ucMapView1.UriStr = null;
            // 
            // ucMapView2
            // 
            this.ucMapView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMapView2.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucMapView2.Appearance.Options.UseFont = true;
            this.ucMapView2.Location = new System.Drawing.Point(1, 4);
            this.ucMapView2.MapID = null;
            this.ucMapView2.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ucMapView2.Name = "ucMapView2";
            this.ucMapView2.Size = new System.Drawing.Size(537, 26);
            this.ucMapView2.TabIndex = 0;
            this.ucMapView2.UriStr = null;
            // 
            // dtgAuto_MiR_Status
            // 
            this.dtgAuto_MiR_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgAuto_MiR_Status.Location = new System.Drawing.Point(3, 16);
            this.dtgAuto_MiR_Status.MainView = this.gridView1;
            this.dtgAuto_MiR_Status.Name = "dtgAuto_MiR_Status";
            this.dtgAuto_MiR_Status.Size = new System.Drawing.Size(1058, 176);
            this.dtgAuto_MiR_Status.TabIndex = 8;
            this.dtgAuto_MiR_Status.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dtgAuto_MiR_Status;
            this.gridView1.Name = "gridView1";
            this.gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView1_CustomDrawCell);
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
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
            this.splitContainerControl1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.dtgAuto_MiR_Status);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1086, 412);
            this.splitContainerControl1.SplitterPosition = 207;
            this.splitContainerControl1.TabIndex = 9;
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
            this.SP_Middle.Panel1.ResumeLayout(false);
            this.SP_Middle.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SP_Middle)).EndInit();
            this.SP_Middle.ResumeLayout(false);
            this.SP_Bottom.Panel1.ResumeLayout(false);
            this.SP_Bottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SP_Bottom)).EndInit();
            this.SP_Bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2.Panel1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2.Panel2)).EndInit();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3.Panel1)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3.Panel2)).EndInit();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SP_Top.Panel1)).EndInit();
            this.SP_Top.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SP_Top.Panel2)).EndInit();
            this.SP_Top.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SP_Top)).EndInit();
            this.SP_Top.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgAuto_MiR_Status)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer AutoDisplay_Timer;
        private UCMapView ucMapView1;
        private System.Windows.Forms.SplitContainer SP_Middle;
        private UCMapView ucMapView3;
        private UCMapView ucMapView4;
        private System.Windows.Forms.SplitContainer SP_Bottom;
        private UCMapView ucMapView5;
        private UCMapView ucMapView6;
        private UCMapView ucMapView2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainer2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainer3;
        private DevExpress.XtraEditors.SplitContainerControl SP_Top;
        private DevExpress.XtraGrid.GridControl dtgAuto_MiR_Status;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
    }
}