
namespace ACS.Monitor
{
    partial class WaitPositionTimeHistory
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
            this.DG_Control = new DevExpress.XtraGrid.GridControl();
            this.DG_View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.combo_robotAlias = new DevExpress.XtraEditors.ComboBoxEdit();
            this.checkButton1 = new DevExpress.XtraEditors.CheckButton();
            this.lbl_Count = new DevExpress.XtraEditors.LabelControl();
            this.btn_Clear = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Search = new DevExpress.XtraEditors.SimpleButton();
            this.btn_monthago = new DevExpress.XtraEditors.SimpleButton();
            this.btn_lastweek = new DevExpress.XtraEditors.SimpleButton();
            this.btn_today = new DevExpress.XtraEditors.SimpleButton();
            this.btn_yesterday = new DevExpress.XtraEditors.SimpleButton();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.DG_Control)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DG_View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combo_robotAlias.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // DG_Control
            // 
            this.DG_Control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DG_Control.Location = new System.Drawing.Point(1, 83);
            this.DG_Control.MainView = this.DG_View;
            this.DG_Control.Name = "DG_Control";
            this.DG_Control.Size = new System.Drawing.Size(1230, 319);
            this.DG_Control.TabIndex = 279;
            this.DG_Control.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.DG_View});
            // 
            // DG_View
            // 
            this.DG_View.GridControl = this.DG_Control;
            this.DG_View.Name = "DG_View";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.combo_robotAlias);
            this.groupControl1.Controls.Add(this.checkButton1);
            this.groupControl1.Controls.Add(this.lbl_Count);
            this.groupControl1.Controls.Add(this.btn_Clear);
            this.groupControl1.Controls.Add(this.btn_Search);
            this.groupControl1.Controls.Add(this.btn_monthago);
            this.groupControl1.Controls.Add(this.btn_lastweek);
            this.groupControl1.Controls.Add(this.btn_today);
            this.groupControl1.Controls.Add(this.btn_yesterday);
            this.groupControl1.Controls.Add(this.dateTimePicker2);
            this.groupControl1.Controls.Add(this.dateTimePicker1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(5, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1226, 74);
            this.groupControl1.TabIndex = 280;
            this.groupControl1.Text = "groupControl1";
            // 
            // combo_robotAlias
            // 
            this.combo_robotAlias.Location = new System.Drawing.Point(518, 26);
            this.combo_robotAlias.Name = "combo_robotAlias";
            this.combo_robotAlias.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combo_robotAlias.Size = new System.Drawing.Size(141, 20);
            this.combo_robotAlias.TabIndex = 7;
            // 
            // checkButton1
            // 
            this.checkButton1.Location = new System.Drawing.Point(483, 26);
            this.checkButton1.Name = "checkButton1";
            this.checkButton1.Size = new System.Drawing.Size(29, 20);
            this.checkButton1.TabIndex = 6;
            // 
            // lbl_Count
            // 
            this.lbl_Count.Location = new System.Drawing.Point(526, 55);
            this.lbl_Count.Name = "lbl_Count";
            this.lbl_Count.Size = new System.Drawing.Size(70, 14);
            this.lbl_Count.TabIndex = 5;
            this.lbl_Count.Text = "labelControl3";
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(1133, 31);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(88, 34);
            this.btn_Clear.TabIndex = 4;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.Click += new System.EventHandler(this.Button_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(1039, 31);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(88, 34);
            this.btn_Search.TabIndex = 4;
            this.btn_Search.Text = "Search";
            this.btn_Search.Click += new System.EventHandler(this.Button_Click);
            // 
            // btn_monthago
            // 
            this.btn_monthago.Location = new System.Drawing.Point(945, 31);
            this.btn_monthago.Name = "btn_monthago";
            this.btn_monthago.Size = new System.Drawing.Size(88, 34);
            this.btn_monthago.TabIndex = 3;
            this.btn_monthago.Text = "month ago";
            this.btn_monthago.Click += new System.EventHandler(this.Button_Click);
            // 
            // btn_lastweek
            // 
            this.btn_lastweek.Location = new System.Drawing.Point(851, 31);
            this.btn_lastweek.Name = "btn_lastweek";
            this.btn_lastweek.Size = new System.Drawing.Size(88, 34);
            this.btn_lastweek.TabIndex = 3;
            this.btn_lastweek.Text = "last week";
            this.btn_lastweek.Click += new System.EventHandler(this.Button_Click);
            // 
            // btn_today
            // 
            this.btn_today.Location = new System.Drawing.Point(759, 31);
            this.btn_today.Name = "btn_today";
            this.btn_today.Size = new System.Drawing.Size(88, 34);
            this.btn_today.TabIndex = 3;
            this.btn_today.Text = "today";
            this.btn_today.Click += new System.EventHandler(this.Button_Click);
            // 
            // btn_yesterday
            // 
            this.btn_yesterday.Location = new System.Drawing.Point(665, 31);
            this.btn_yesterday.Name = "btn_yesterday";
            this.btn_yesterday.Size = new System.Drawing.Size(88, 34);
            this.btn_yesterday.TabIndex = 3;
            this.btn_yesterday.Text = "yesterday";
            this.btn_yesterday.Click += new System.EventHandler(this.Button_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(277, 38);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(39, 38);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(256, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(15, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "To";
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(6, 41);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(27, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "From";
            // 
            // WaitPositionTimeHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 404);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.DG_Control);
            this.Name = "WaitPositionTimeHistory";
            ((System.ComponentModel.ISupportInitialize)(this.DG_Control)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DG_View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combo_robotAlias.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl DG_Control;
        private DevExpress.XtraGrid.Views.Grid.GridView DG_View;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private DevExpress.XtraEditors.SimpleButton btn_yesterday;
        private DevExpress.XtraEditors.SimpleButton btn_lastweek;
        private DevExpress.XtraEditors.SimpleButton btn_today;
        private DevExpress.XtraEditors.SimpleButton btn_monthago;
        private DevExpress.XtraEditors.SimpleButton btn_Search;
        private DevExpress.XtraEditors.SimpleButton btn_Clear;
        private DevExpress.XtraEditors.LabelControl lbl_Count;
        private DevExpress.XtraEditors.CheckButton checkButton1;
        private DevExpress.XtraEditors.ComboBoxEdit combo_robotAlias;
    }
}