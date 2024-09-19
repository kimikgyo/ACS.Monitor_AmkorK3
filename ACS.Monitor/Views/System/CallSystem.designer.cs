
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_StartZoneSetting = new DevExpress.XtraEditors.SimpleButton();
            this.combobox_StartZone = new DevExpress.XtraEditors.ComboBoxEdit();
            this.CallGridControl = new DevExpress.XtraGrid.GridControl();
            this.CallGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.btn_Close = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combobox_StartZone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CallGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CallGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btn_Close);
            this.groupBox1.Controls.Add(this.btn_StartZoneSetting);
            this.groupBox1.Controls.Add(this.combobox_StartZone);
            this.groupBox1.Controls.Add(this.CallGridControl);
            this.groupBox1.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 330);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Call System";
            // 
            // btn_StartZoneSetting
            // 
            this.btn_StartZoneSetting.Location = new System.Drawing.Point(113, 30);
            this.btn_StartZoneSetting.Name = "btn_StartZoneSetting";
            this.btn_StartZoneSetting.Size = new System.Drawing.Size(125, 23);
            this.btn_StartZoneSetting.TabIndex = 279;
            this.btn_StartZoneSetting.Text = "StartZoneSetting";
            this.btn_StartZoneSetting.Click += new System.EventHandler(this.btn_Click);
            // 
            // combobox_StartZone
            // 
            this.combobox_StartZone.Location = new System.Drawing.Point(6, 31);
            this.combobox_StartZone.Name = "combobox_StartZone";
            this.combobox_StartZone.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combobox_StartZone.Size = new System.Drawing.Size(101, 20);
            this.combobox_StartZone.TabIndex = 278;
            this.combobox_StartZone.Click += new System.EventHandler(this.combobox_StartZone_Click);
            // 
            // CallGridControl
            // 
            this.CallGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CallGridControl.Location = new System.Drawing.Point(6, 62);
            this.CallGridControl.MainView = this.CallGridView;
            this.CallGridControl.Name = "CallGridControl";
            this.CallGridControl.Size = new System.Drawing.Size(276, 218);
            this.CallGridControl.TabIndex = 277;
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
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(3, 286);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(295, 23);
            this.btn_Close.TabIndex = 2;
            this.btn_Close.Text = "simpleButton1";
            this.btn_Close.Click += new System.EventHandler(this.btn_Click);
            // 
            // CallSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 333);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "CallSystem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.combobox_StartZone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CallGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CallGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.GridControl CallGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView CallGridView;
        private System.Windows.Forms.Timer DisplayTimer;
        private DevExpress.XtraEditors.SimpleButton btn_StartZoneSetting;
        private DevExpress.XtraEditors.ComboBoxEdit combobox_StartZone;
        private DevExpress.XtraEditors.SimpleButton btn_Close;
    }
}