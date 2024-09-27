
namespace ACS.Monitor
{
    partial class CallSystem
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
            this.combobox_StartZone = new DevExpress.XtraEditors.ComboBoxEdit();
            this.CallGridControl = new DevExpress.XtraGrid.GridControl();
            this.CallGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.lbl_StartZone = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.combobox_StartZone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CallGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CallGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // combobox_StartZone
            // 
            this.combobox_StartZone.Location = new System.Drawing.Point(3, 8);
            this.combobox_StartZone.Name = "combobox_StartZone";
            this.combobox_StartZone.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combobox_StartZone.Size = new System.Drawing.Size(125, 20);
            this.combobox_StartZone.TabIndex = 284;
            // 
            // CallGridControl
            // 
            this.CallGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CallGridControl.Location = new System.Drawing.Point(0, 39);
            this.CallGridControl.MainView = this.CallGridView;
            this.CallGridControl.Name = "CallGridControl";
            this.CallGridControl.Size = new System.Drawing.Size(306, 302);
            this.CallGridControl.TabIndex = 283;
            this.CallGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.CallGridView});
            // 
            // CallGridView
            // 
            this.CallGridView.GridControl = this.CallGridControl;
            this.CallGridView.Name = "CallGridView";
            // 
            // DisplayTimer
            // 
            this.DisplayTimer.Enabled = true;
            this.DisplayTimer.Interval = 1000;
            this.DisplayTimer.Tick += new System.EventHandler(this.DisplayTimer_Tick);
            // 
            // lbl_StartZone
            // 
            this.lbl_StartZone.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_StartZone.Location = new System.Drawing.Point(150, 7);
            this.lbl_StartZone.Name = "lbl_StartZone";
            this.lbl_StartZone.Size = new System.Drawing.Size(101, 23);
            this.lbl_StartZone.TabIndex = 286;
            this.lbl_StartZone.Text = "StartZone";
            // 
            // CallSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 340);
            this.Controls.Add(this.lbl_StartZone);
            this.Controls.Add(this.combobox_StartZone);
            this.Controls.Add(this.CallGridControl);
            this.Name = "CallSystem";
            this.Text = "XtraForm1";
            ((System.ComponentModel.ISupportInitialize)(this.combobox_StartZone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CallGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CallGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.ComboBoxEdit combobox_StartZone;
        private DevExpress.XtraGrid.GridControl CallGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView CallGridView;
        private System.Windows.Forms.Timer DisplayTimer;
        private DevExpress.XtraEditors.LabelControl lbl_StartZone;
    }
}