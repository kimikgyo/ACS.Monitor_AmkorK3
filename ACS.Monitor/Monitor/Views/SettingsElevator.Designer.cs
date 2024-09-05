
namespace ACS.Monitor
{
    partial class SettingsElevator
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
            this.btn_ElvAuto = new System.Windows.Forms.Button();
            this.btn_ElvManual = new System.Windows.Forms.Button();
            this.T_1Sec = new System.Windows.Forms.Timer(this.components);
            this.DG_View = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.DG_View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ElvAuto
            // 
            this.btn_ElvAuto.BackColor = System.Drawing.SystemColors.Control;
            this.btn_ElvAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ElvAuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ElvAuto.Location = new System.Drawing.Point(8, 12);
            this.btn_ElvAuto.Name = "btn_ElvAuto";
            this.btn_ElvAuto.Size = new System.Drawing.Size(120, 59);
            this.btn_ElvAuto.TabIndex = 307;
            this.btn_ElvAuto.Text = "Elevator \r\nAGV On";
            this.btn_ElvAuto.UseVisualStyleBackColor = false;
            this.btn_ElvAuto.Click += new System.EventHandler(this.btn_ElvAuto_Click);
            // 
            // btn_ElvManual
            // 
            this.btn_ElvManual.BackColor = System.Drawing.SystemColors.Control;
            this.btn_ElvManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ElvManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ElvManual.Location = new System.Drawing.Point(128, 12);
            this.btn_ElvManual.Name = "btn_ElvManual";
            this.btn_ElvManual.Size = new System.Drawing.Size(120, 59);
            this.btn_ElvManual.TabIndex = 308;
            this.btn_ElvManual.Text = "Elevator AGV Off";
            this.btn_ElvManual.UseVisualStyleBackColor = false;
            this.btn_ElvManual.Click += new System.EventHandler(this.btn_ElvManual_Click);
            // 
            // T_1Sec
            // 
            this.T_1Sec.Interval = 1000;
            this.T_1Sec.Tick += new System.EventHandler(this.T_1Sec_Tick);
            // 
            // DG_View
            // 
            this.DG_View.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DG_View.Location = new System.Drawing.Point(12, 77);
            this.DG_View.MainView = this.gridView1;
            this.DG_View.Name = "DG_View";
            this.DG_View.Size = new System.Drawing.Size(297, 437);
            this.DG_View.TabIndex = 310;
            this.DG_View.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.DG_View;
            this.gridView1.Name = "gridView1";
            this.gridView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanging);
            // 
            // SettingsElevator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 526);
            this.Controls.Add(this.DG_View);
            this.Controls.Add(this.btn_ElvAuto);
            this.Controls.Add(this.btn_ElvManual);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "SettingsElevator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.Activated += new System.EventHandler(this.SettingsElevator_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsElevator_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.DG_View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_ElvAuto;
        private System.Windows.Forms.Button btn_ElvManual;
        private System.Windows.Forms.Timer T_1Sec;
        private DevExpress.XtraGrid.GridControl DG_View;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}