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
    public partial class JobHistory : DevExpress.XtraEditors.XtraForm
    {
        private readonly Font gridFont2 = new Font("Arial", 9, FontStyle.Bold);

        private readonly MainForm mainForm;
        private readonly IUnitOfWork uow;
        private BindingSource _bindingSource = null;
        //private bool checkButton = false;
        public JobHistory(MainForm mainForm, IUnitOfWork uow)
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
            groupControl1.Text = "JobHistory SearchData";
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

        private IEnumerable<dynamic> JobHistoryQueryDB(DateTime searchDate1, DateTime searchDate2)
        {
            string connectionString = ConnectionStrings.DB1; //ConfigurationManager.ConnectionStrings["Connection1"].ConnectionString;

            string SELECT_SQL = @"
                                SELECT 
                                FORMAT(CONVERT(date,JobFinishTime), 'yyyy-MM-dd') [JobFinishDate], 
                                RobotName, 
                                RobotAlias, 
                                COUNT(*) [JobCount], 
                                convert(varchar(8), dateadd(second, SUM(DATEDIFF(SECOND, JobCreateTime, JobFinishTime)), 0), 108) AS 'Total이동시간',
                                convert(varchar(8), MIN(JobFinishTime - JobCreateTime),  108) AS 'Total최소이동시간',
                                convert(varchar(8), dateadd(second, AVG(DATEDIFF(SECOND, JobCreateTime, JobFinishTime)), 0), 108) AS 'Total평균이동시간',
                                COUNT(case WHEN CallName Like 'T3F%_M3F%' OR CallName Like 'M3F%_M3F%' THEN Id END) AS 'JobCount목적지M3F',
                                convert(varchar(8), dateadd(second, AVG(DATEDIFF(SECOND, case WHEN CallName Like 'T3F%_M3F%' OR CallName Like 'M3F%_M3F%' THEN JobCreateTime END, case WHEN CallName Like 'T3F%_M3F%' OR CallName Like 'M3F%_M3F%' THEN JobFinishTime END)), 0), 108) AS 'Job평균이동시간M3F',
                                COUNT(case WHEN CallName Like 'M3F%_T3F%' OR CallName Like 'T3F%_T3F%' THEN Id END) AS 'JobCount목적지T3F',
                                convert(varchar(8), dateadd(second, AVG(DATEDIFF(SECOND, case WHEN CallName Like 'M3F%_T3F%' OR CallName Like 'T3F%_T3F%' THEN JobCreateTime END, case WHEN CallName Like 'M3F%_T3F%' OR CallName Like 'T3F%_T3F%' THEN JobFinishTime END)), 0), 108) AS 'Job평균이동시간T3F'
                                FROM
                                JobHistory 
                                WHERE 
                                JobFinishTime >= @searchDate1 AND JobFinishTime < @searchDate2
                                AND CallName NOT LIKE '%Cancel%'
                                AND JobState = 'JobDone'";

            if (checkButton1.Checked) SELECT_SQL += "AND RobotAlias LIKE @robotAlias";
            SELECT_SQL += "\n GROUP BY CONVERT(date,JobFinishTime), RobotName, RobotAlias";
            SELECT_SQL += "\n  ORDER BY JobFinishDate";

            var robotAlias = "%" + combo_robotAlias.Text.Trim() + "%";

            using (var con = new SqlConnection(connectionString))
            {
                var result = con.Query(SELECT_SQL, new { searchDate1, searchDate2, robotAlias });
                var viewResult = result.Select(x => new
                {
                    Date = x.JobFinishDate,
                    RobotName = x.RobotName,
                    RobotAlias = x.RobotAlias,
                    JobCount = x.JobCount,
                    Total이동시간 = x.Total이동시간,
                    Total최소이동시간 = x.Total최소이동시간,
                    Total평균이동시간 = x.Total평균이동시간,
                    JobCount목적지M3F = x.JobCount목적지M3F,
                    Job평균이동시간M3F = x.Job평균이동시간M3F,
                    JobCount목적지T3F = x.JobCount목적지T3F,
                    Job평균이동시간T3F = x.Job평균이동시간T3F,
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
                    var bindingList = JobHistoryQueryDB(Date1, Date2).ToList();
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