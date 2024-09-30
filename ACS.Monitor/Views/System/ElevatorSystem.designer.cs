
namespace ACS.Monitor
{
    partial class ElevatorSystem
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
            this.ElevatorGridControl = new DevExpress.XtraGrid.GridControl();
            this.ElevatorGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.lbl_AGVON = new DevExpress.XtraEditors.LabelControl();
            this.lbl_AGVOFF = new DevExpress.XtraEditors.LabelControl();
            this.lbl_AGVMiddle = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ElevatorGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElevatorGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ElevatorGridControl
            // 
            this.ElevatorGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ElevatorGridControl.Location = new System.Drawing.Point(-1, 58);
            this.ElevatorGridControl.MainView = this.ElevatorGridView;
            this.ElevatorGridControl.Name = "ElevatorGridControl";
            this.ElevatorGridControl.Size = new System.Drawing.Size(310, 280);
            this.ElevatorGridControl.TabIndex = 312;
            this.ElevatorGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ElevatorGridView});
            // 
            // ElevatorGridView
            // 
            this.ElevatorGridView.GridControl = this.ElevatorGridControl;
            this.ElevatorGridView.Name = "ElevatorGridView";
            // 
            // DisplayTimer
            // 
            this.DisplayTimer.Enabled = true;
            this.DisplayTimer.Interval = 1000;
            this.DisplayTimer.Tick += new System.EventHandler(this.DisplayTimer_Tick);
            // 
            // lbl_AGVON
            // 
            this.lbl_AGVON.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lbl_AGVON.Appearance.Options.UseBackColor = true;
            this.lbl_AGVON.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_AGVON.Location = new System.Drawing.Point(13, 11);
            this.lbl_AGVON.Name = "lbl_AGVON";
            this.lbl_AGVON.Size = new System.Drawing.Size(72, 30);
            this.lbl_AGVON.TabIndex = 313;
            this.lbl_AGVON.Text = "AGV\r\nON";
            // 
            // lbl_AGVOFF
            // 
            this.lbl_AGVOFF.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lbl_AGVOFF.Appearance.Options.UseBackColor = true;
            this.lbl_AGVOFF.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_AGVOFF.Location = new System.Drawing.Point(115, 11);
            this.lbl_AGVOFF.Name = "lbl_AGVOFF";
            this.lbl_AGVOFF.Size = new System.Drawing.Size(72, 30);
            this.lbl_AGVOFF.TabIndex = 313;
            this.lbl_AGVOFF.Text = "AGV\r\nOFF";
            // 
            // lbl_AGVMiddle
            // 
            this.lbl_AGVMiddle.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lbl_AGVMiddle.Appearance.Options.UseBackColor = true;
            this.lbl_AGVMiddle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_AGVMiddle.Location = new System.Drawing.Point(212, 11);
            this.lbl_AGVMiddle.Name = "lbl_AGVMiddle";
            this.lbl_AGVMiddle.Size = new System.Drawing.Size(72, 30);
            this.lbl_AGVMiddle.TabIndex = 313;
            this.lbl_AGVMiddle.Text = "AGV\r\nMiddle";
            // 
            // ElevatorSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 340);
            this.Controls.Add(this.lbl_AGVMiddle);
            this.Controls.Add(this.lbl_AGVOFF);
            this.Controls.Add(this.lbl_AGVON);
            this.Controls.Add(this.ElevatorGridControl);
            this.Name = "ElevatorSystem";
            this.Text = "Elevator";
            ((System.ComponentModel.ISupportInitialize)(this.ElevatorGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElevatorGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl ElevatorGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView ElevatorGridView;
        private System.Windows.Forms.Timer DisplayTimer;
        private DevExpress.XtraEditors.LabelControl lbl_AGVON;
        private DevExpress.XtraEditors.LabelControl lbl_AGVOFF;
        private DevExpress.XtraEditors.LabelControl lbl_AGVMiddle;
    }
}