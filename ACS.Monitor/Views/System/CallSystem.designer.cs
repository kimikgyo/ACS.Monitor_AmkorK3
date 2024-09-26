
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
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.btn_StartZoneSetting = new DevExpress.XtraEditors.SimpleButton();
            this.combobox_StartZone = new DevExpress.XtraEditors.ComboBoxEdit();
            this.CallGridControl = new DevExpress.XtraGrid.GridControl();
            this.CallGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.combobox_StartZone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CallGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CallGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // DisplayTimer
            // 
            this.DisplayTimer.Enabled = true;
            this.DisplayTimer.Interval = 1000;
            this.DisplayTimer.Tick += new System.EventHandler(this.DisplayTimer_Tick);
            // 
            // btn_StartZoneSetting
            // 
            this.btn_StartZoneSetting.Location = new System.Drawing.Point(96, 1);
            this.btn_StartZoneSetting.Name = "btn_StartZoneSetting";
            this.btn_StartZoneSetting.Size = new System.Drawing.Size(101, 23);
            this.btn_StartZoneSetting.TabIndex = 282;
            this.btn_StartZoneSetting.Text = "StartZoneSetting";
            this.btn_StartZoneSetting.Click += new System.EventHandler(this.btn_Click);
            // 
            // combobox_StartZone
            // 
            this.combobox_StartZone.Location = new System.Drawing.Point(1, 2);
            this.combobox_StartZone.Name = "combobox_StartZone";
            this.combobox_StartZone.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combobox_StartZone.Size = new System.Drawing.Size(89, 20);
            this.combobox_StartZone.TabIndex = 281;
            // 
            // CallGridControl
            // 
            this.CallGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CallGridControl.Location = new System.Drawing.Point(1, 33);
            this.CallGridControl.MainView = this.CallGridView;
            this.CallGridControl.Name = "CallGridControl";
            this.CallGridControl.Size = new System.Drawing.Size(293, 218);
            this.CallGridControl.TabIndex = 280;
            this.CallGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.CallGridView});
            // 
            // CallGridView
            // 
            this.CallGridView.GridControl = this.CallGridControl;
            this.CallGridView.Name = "CallGridView";
            // 
            // CallSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 333);
            this.Controls.Add(this.btn_StartZoneSetting);
            this.Controls.Add(this.combobox_StartZone);
            this.Controls.Add(this.CallGridControl);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "CallSystem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.combobox_StartZone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CallGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CallGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer DisplayTimer;
        private DevExpress.XtraEditors.SimpleButton btn_StartZoneSetting;
        private DevExpress.XtraEditors.ComboBoxEdit combobox_StartZone;
        private DevExpress.XtraGrid.GridControl CallGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView CallGridView;
    }
}