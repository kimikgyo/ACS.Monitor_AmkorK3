using Dapper;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACS.Monitor
{
    public partial class WaitPositionTimeHistory : DevExpress.XtraEditors.XtraForm
    {
        private readonly Font gridFont2 = new Font("Arial", 9, FontStyle.Bold);

        private readonly MainForm mainForm;
        private readonly IUnitOfWork uow;
        private BindingSource _bindingSource = null;
        //private bool checkButton = false;
        public WaitPositionTimeHistory(MainForm mainForm, IUnitOfWork uow)
        {
            InitializeComponent();

            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.None; // 테두리 제거
            this.WindowState = FormWindowState.Normal; // 전체 화면 설정
            this.StartPosition = FormStartPosition.CenterParent;
            //this.dataGridView1.Dock = DockStyle.Fill;

            this.mainForm = mainForm;
            this.uow = uow;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            groupControl1.Text = "WaitPositionTimeHistory SearchData";
            DG_Control.SetDoubleBuffering(true);
            DG_View.OptionsBehavior.Editable = false;

            dateTimePicker1.CustomFormat = "yyyy-MM-dd (dddd) HH:mm:ss";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Font = gridFont2;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd (dddd) HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Font = gridFont2;

            dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00"));
            dateTimePicker2.Value = Convert.ToDateTime(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd 00:00:00"));

            labelControl1.Text = "From";
            labelControl1.AutoSizeMode = LabelAutoSizeMode.None;
            labelControl1.Font = gridFont2;
            labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center; // 가로 정렬
            labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center; // 세로 정렬

            labelControl2.Text = "To";
            labelControl2.AutoSizeMode = LabelAutoSizeMode.None;
            labelControl2.Font = gridFont2;
            labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center; // 가로 정렬
            labelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center; // 세로 정렬

            checkButton1.Click += CheckButton1_Click;
            combo_robotAlias.Enabled = false;

            lbl_Count.Text = $"Count = {DG_View.DataRowCount}";
        }

        private void CheckButton1_Click(object sender, EventArgs e)
        {

            if (!checkButton1.Checked)
            {
                checkButton1.ImageOptions.ImageUri.Uri = "Apply;Office2013";
                combo_robotAlias.Enabled = true;
                var robotAlias = uow.Robots.GetAll().Select(r => r.RobotAlias).ToArray();
                combo_robotAlias.Properties.Items.AddRange(robotAlias);
            }
            else
            {
                checkButton1.ImageOptions.ImageUri.Uri = "";
                combo_robotAlias.Text = "";
                combo_robotAlias.Enabled = false;
            }
        }

        private IEnumerable<dynamic> WaitPositionTimeHistoryQueryDB(DateTime searchDate1, DateTime searchDate2)
        {
            string connectionString = ConnectionStrings.DB1; //ConfigurationManager.ConnectionStrings["Connection1"].ConnectionString;

            string SELECT_SQL = @"
                                SELECT 
                                FORMAT(CONVERT(date,FinishTime), 'yyyy-MM-dd') [FinishTimeDate]
                                ,RobotName 
                                ,RobotAlias
                                ,COUNT(*) [ToTalWaitCount] 
                                ,convert(varchar(8), dateadd(SECOND,ISNULL(SUM(datediff(SECOND,CreateTime,FinishTime)),0),0), 108) AS 'Total총합시간'
                                ,convert(varchar(8), dateadd(SECOND,ISNULL(MAX(datediff(SECOND,CreateTime,FinishTime)),0),0), 108) AS 'Total최대시간'
                                ,convert(varchar(8), dateadd(SECOND,ISNULL(MIN(datediff(SECOND,CreateTime,FinishTime)),0),0), 108) AS 'Total최소시간'
                                ,convert(varchar(8), dateadd(SECOND,ISNULL(AVG(datediff(SECOND,CreateTime,FinishTime)),0),0), 108) AS 'Total평균시간'
                                ,COUNT(case WHEN PositionName Like 'M3F%' THEN Id END) AS 'M3F대기Count'
                                ,convert(varchar(8), dateadd(second,ISNULL(SUM(DATEDIFF(SECOND, case WHEN PositionName Like 'M3F%' THEN CreateTime END, case WHEN PositionName Like 'M3F%' THEN FinishTime END)),0),0), 108) AS 'M3F총합시간'
                                ,convert(varchar(8), dateadd(second,ISNULL(MAX(DATEDIFF(SECOND, case WHEN PositionName Like 'M3F%' THEN CreateTime END, case WHEN PositionName Like 'M3F%' THEN FinishTime END)),0),0), 108) AS 'M3F최대시간'
                                ,convert(varchar(8), dateadd(second,ISNULL(MIN(DATEDIFF(SECOND, case WHEN PositionName Like 'M3F%' THEN CreateTime END, case WHEN PositionName Like 'M3F%' THEN FinishTime END)),0),0), 108) AS 'M3F최소시간'
                                ,convert(varchar(8), dateadd(second,ISNULL(AVG(DATEDIFF(SECOND, case WHEN PositionName Like 'M3F%' THEN CreateTime END, case WHEN PositionName Like 'M3F%' THEN FinishTime END)),0),0), 108) AS 'M3F평균시간'
                                ,COUNT(case WHEN PositionName Like 'T3F%' THEN Id END) AS 'T3F대기Count'
                                ,convert(varchar(8), dateadd(second,ISNULL(SUM(DATEDIFF(SECOND, case WHEN PositionName Like 'T3F%' THEN CreateTime END, case WHEN PositionName Like 'T3F%' THEN FinishTime END)),0),0), 108) AS 'T3F총합시간'
                                ,convert(varchar(8), dateadd(second,ISNULL(MAX(DATEDIFF(SECOND, case WHEN PositionName Like 'T3F%' THEN CreateTime END, case WHEN PositionName Like 'T3F%' THEN FinishTime END)),0),0), 108) AS 'T3F최대시간'
                                ,convert(varchar(8), dateadd(second,ISNULL(MIN(DATEDIFF(SECOND, case WHEN PositionName Like 'T3F%' THEN CreateTime END, case WHEN PositionName Like 'T3F%' THEN FinishTime END)),0),0), 108) AS 'T3F최소시간'
                                ,convert(varchar(8), dateadd(second,ISNULL(AVG(DATEDIFF(SECOND, case WHEN PositionName Like 'T3F%' THEN CreateTime END, case WHEN PositionName Like 'T3F%' THEN FinishTime END)),0),0), 108) AS 'T3F평균시간'
                                FROM
                                PositionWaitTimeHistory 
                                WHERE 
                                FinishTime >= @searchDate1 AND FinishTime < @searchDate2";

            if (checkButton1.Checked) SELECT_SQL += "\n AND RobotAlias LIKE @robotAlias";
            SELECT_SQL += "\n  GROUP BY CONVERT(date,FinishTime), RobotName,RobotAlias";
            SELECT_SQL += "\n ORDER BY FinishTimeDate";

            var robotAlias = "%" + combo_robotAlias.Text.Trim();

            using (var con = new SqlConnection(connectionString))
            {
                var result = con.Query(SELECT_SQL, new { searchDate1, searchDate2, robotAlias });
                var viewResult = result.Select(x => new
                {
                    FinishTimeDate = x.FinishTimeDate,
                    RobotName = x.RobotName,
                    RobotAlias = x.RobotAlias,
                    ToTalWaitCount = x.ToTalWaitCount,
                    Total총합시간 = x.Total총합시간,
                    Total최대시간 = x.Total최대시간,
                    Total최소시간 = x.Total최소시간,
                    Total평균시간 = x.Total평균시간,
                    M3F대기Count = x.M3F대기Count,
                    M3F총합시간 = x.M3F총합시간,
                    M3F최대시간 = x.M3F최대시간,
                    M3F최소시간 = x.M3F최소시간,
                    M3F평균시간 = x.M3F평균시간,
                    T3F대기Count = x.T3F대기Count,
                    T3F총합시간 = x.T3F총합시간,
                    T3F최대시간 = x.T3F최대시간,
                    T3F최소시간 = x.T3F최소시간,
                    T3F평균시간 = x.T3F평균시간,
                });
                return viewResult;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            string Name = ((SimpleButton)sender).Name;
            var today = DateTime.Today;
            switch (Name)
            {
                case "btn_yesterday":
                    dateTimePicker1.Value = today.AddDays(-1) + new TimeSpan(0, 0, 0);
                    dateTimePicker2.Value = today.AddDays(0) + new TimeSpan(0, 0, 0);
                    break;
                case "btn_today":
                    dateTimePicker1.Value = today.AddDays(0) + new TimeSpan(0, 0, 0);
                    dateTimePicker2.Value = today.AddDays(1) + new TimeSpan(0, 0, 0);
                    break;
                case "btn_lastweek":
                    dateTimePicker1.Value = today.AddDays(-7) + new TimeSpan(0, 0, 0);
                    dateTimePicker2.Value = today.AddDays(0) + new TimeSpan(0, 0, 0);
                    break;
                case "btn_monthago":
                    dateTimePicker1.Value = today.AddMonths(-1) + new TimeSpan(0, 0, 0);
                    dateTimePicker2.Value = today.AddDays(0) + new TimeSpan(0, 0, 0);
                    break;
                case "btn_Search":
                    DG_Control.DataBindings.Clear();
                    if (_bindingSource != null) _bindingSource.Clear();
                    DateTime Date1 = dateTimePicker1.Value.Date;
                    DateTime Date2 = dateTimePicker2.Value.Date;
                    var bindingList = WaitPositionTimeHistoryQueryDB(Date1, Date2).ToList();
                    _bindingSource = new BindingSource(bindingList, null);
                    DG_Control.DataSource = _bindingSource;
                    lbl_Count.Text = $"Count = {DG_View.DataRowCount}";
                    break;
                case "btn_Clear":
                    DG_Control.DataBindings.Clear();
                    if (_bindingSource != null) _bindingSource.Clear();
                    DG_Control.DataSource = _bindingSource;

                    checkButton1.Checked = false;
                    checkButton1.ImageOptions.ImageUri.Uri = "";
                    combo_robotAlias.Text = "";
                    combo_robotAlias.Enabled = false;

                    lbl_Count.Text = $"Count = {DG_View.DataRowCount}";
                    break;
            }
        }
    }
}