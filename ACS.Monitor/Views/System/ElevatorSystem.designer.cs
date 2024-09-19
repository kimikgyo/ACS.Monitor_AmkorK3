
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
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.ElevatorGridControl = new DevExpress.XtraGrid.GridControl();
            this.ElevatorGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btn_ElvAuto = new DevExpress.XtraEditors.SimpleButton();
            this.btn_ElvManual = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Close = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ElevatorGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElevatorGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // DisplayTimer
            // 
            this.DisplayTimer.Enabled = true;
            this.DisplayTimer.Interval = 1000;
            this.DisplayTimer.Tick += new System.EventHandler(this.DisplayTimer_Tick);
            // 
            // ElevatorGridControl
            // 
            this.ElevatorGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ElevatorGridControl.Location = new System.Drawing.Point(12, 77);
            this.ElevatorGridControl.MainView = this.ElevatorGridView;
            this.ElevatorGridControl.Name = "ElevatorGridControl";
            this.ElevatorGridControl.Size = new System.Drawing.Size(276, 218);
            this.ElevatorGridControl.TabIndex = 310;
            this.ElevatorGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ElevatorGridView});
            // 
            // ElevatorGridView
            // 
            this.ElevatorGridView.GridControl = this.ElevatorGridControl;
            this.ElevatorGridView.Name = "ElevatorGridView";
            // 
            // btn_ElvAuto
            // 
            this.btn_ElvAuto.Location = new System.Drawing.Point(12, 12);
            this.btn_ElvAuto.Name = "btn_ElvAuto";
            this.btn_ElvAuto.Size = new System.Drawing.Size(102, 59);
            this.btn_ElvAuto.TabIndex = 311;
            this.btn_ElvAuto.Text = "simpleButton1";
            this.btn_ElvAuto.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_ElvManual
            // 
            this.btn_ElvManual.Location = new System.Drawing.Point(120, 12);
            this.btn_ElvManual.Name = "btn_ElvManual";
            this.btn_ElvManual.Size = new System.Drawing.Size(102, 59);
            this.btn_ElvManual.TabIndex = 311;
            this.btn_ElvManual.Text = "simpleButton1";
            this.btn_ElvManual.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(3, 286);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(295, 23);
            this.btn_Close.TabIndex = 312;
            this.btn_Close.Text = "simpleButton1";
            this.btn_Close.Click += new System.EventHandler(this.btn_Click);
            // 
            // ElevatorSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 333);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_ElvManual);
            this.Controls.Add(this.btn_ElvAuto);
            this.Controls.Add(this.ElevatorGridControl);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "ElevatorSystem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ElevatorGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElevatorGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer DisplayTimer;
        private DevExpress.XtraGrid.GridControl ElevatorGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView ElevatorGridView;
        private DevExpress.XtraEditors.SimpleButton btn_ElvAuto;
        private DevExpress.XtraEditors.SimpleButton btn_ElvManual;
        private DevExpress.XtraEditors.SimpleButton btn_Close;
    }
}