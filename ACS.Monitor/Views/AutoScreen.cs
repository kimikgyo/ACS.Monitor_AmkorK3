using Dapper;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ACS.Monitor.Utilities;
using Monitor.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ACS.Monitor
{
    public partial class AutoScreen : Form
    {
        public Dictionary<int, string> Sub_DisplayMapNames;
        public List<MapNameAlias> mapDBdatas;
        public List<TableLayoutPanel> layoutPanels;
        public bool FlagLayout = false;
        public string S_UserNumber = "";
        public string S_UserName = "";

        private readonly MainForm mainForm;
        private readonly IUnitOfWork uow;
        private readonly Font textFont1 = new Font("고딕", 12);
        private readonly Font textFont2 = new Font("Arial", 10);
        private readonly Font textFont3 = new Font("Arial", 9);
        private readonly Font gridFont1 = new Font("Arial", 10, FontStyle.Bold);
        private readonly Font gridFont2 = new Font("Arial", 9, FontStyle.Bold);
        private DataTable GridDT = new DataTable();
        private Dictionary<string, RobotZoom> RobotzoomData = new Dictionary<string, RobotZoom>();

        public AutoScreen(MainForm mainForm, IUnitOfWork uow)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            this.uow = uow;

            //배경색 지정
            this.BackColor = Color.FromArgb(249, 219, 186);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.BackColor = Color.White;
            Form parentForm = Helpers.GetParentForm(this);
            if (parentForm != null) parentForm.FormClosed += ParentForm_FormClosed;

            subFuncMiRGridInit();

#if MIRDEMO
            string fleetIp = "localhost:5000";
#else
            string fleetIp = ConfigurationManager.AppSettings["sFleet_IP_Address_SV"];
#endif
            Sub_DisplayMapNames = new Dictionary<int, string>();
            mapDBdatas = new List<MapNameAlias>();
            layoutPanels = new List<TableLayoutPanel>();
            mapDBdatas = MapNameAlias.GetAll();

            {
                string tmp = ConfigurationManager.AppSettings["MapNames"];
                ConfigData.DisplayMapNames = Helpers.ConvertintToDictionary(tmp) ?? new Dictionary<int, string>();
            }

            {
                string tmp = ConfigurationManager.AppSettings["RobotNames"];
                ConfigData.DisplayRobotNames = Helpers.ConvertStringToDictionary(tmp) ?? new Dictionary<string, string>();
            }

            AutoDisplay_Timer.Enabled = true;
        }

        private void ParentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }

        private void subFuncMiRGridInit()
        {
            try
            {
                GridDT.Columns.AddRange(new DataColumn[] {
                    new DataColumn("DGV_MiR_Status_Robot_Alias"),
                    new DataColumn("DGV_ProgressBar"),
                    new DataColumn("DGV_Source"),
                    new DataColumn("DGV_Dest"),
                    new DataColumn("DGV_MiR_Status_Battery_Percent"),
                    new DataColumn("DGV_MiR_Status_CallName"),
                    new DataColumn("DGV_WaitTime"),
                    new DataColumn("DGV_MiR_State"),
                    new DataColumn("DGV_ElevatorOrder"),
                    new DataColumn("DGV_MiR_Status_Product"),
                    new DataColumn("DGV_MiR_Status_Product_Detail"),
                    new DataColumn("DGV_MiR_Status_Door_D"),
                    new DataColumn("DGV_MiR_Status_Door_T"),
                    new DataColumn("DGV_MiR_Status_Robot_Name_orderby", typeof(Int16)),
                    new DataColumn("DGV_PositionWaitTime")
                });

                GridDT.Columns[0].Caption = "로봇이름";
                GridDT.Columns[1].Caption = "진행률";
                GridDT.Columns[2].Caption = "출발지";
                GridDT.Columns[3].Caption = "목적지";
                GridDT.Columns[4].Caption = "배터리";
                GridDT.Columns[5].Caption = "콜이름";
                GridDT.Columns[6].Caption = "대기시간";
                GridDT.Columns[7].Caption = "로봇상태";
                GridDT.Columns[8].Caption = "elevator순서";
                GridDT.Columns[9].Caption = "자재유무";
                GridDT.Columns[10].Caption = "자재정보";
                GridDT.Columns[11].Caption = "Door";
                GridDT.Columns[12].Caption = "Door";
                GridDT.Columns[13].Caption = "순서";
                GridDT.Columns[14].Caption = "포지션값";

            }
            catch (Exception ex)
            {
                mainForm.EventLog("AutoScreen", "subFuncMiRGridInit() Fail = " + ex);
                mainForm.ACS_UI_Log("AutoScreen" + "/" + "subFuncMiRGridInit() Fail = " + ex);
            }
        }

        #region Status Display 관련

        private bool blinkFlag = false;
        private bool Flag = true;
        private void subFunc_MiR_Status_Display() //MiR 상태 표기 하는 함수
        {
            blinkFlag = !blinkFlag;

            try
            {
                var orderedRobots = uow.Robots.GetAll().ToList();
                orderedRobots = orderedRobots.OrderBy(x => x.RobotAlias).ToList();
                orderedRobots = orderedRobots.Where(x => ConfigData.DisplayRobotNames.ContainsKey(x.RobotName)).ToList(); // 설정창에서 체크된 robot만 필터링

                ConfigData.Robots = orderedRobots; // 맵에서 사용하기 위해 전역변수에 저장한다!

                gridView1.SelectAll();
                gridView1.DeleteSelectedRows();

                RepositoryItemProgressBar ritem = new RepositoryItemProgressBar();
                RepositoryItemTextEdit te = new RepositoryItemTextEdit();

                int GridCount = 0;
                dtgAuto_MiR_Status.RepositoryItems.Clear();
                GridDT.Rows.Clear();
                for (int iMiR_No = 0; iMiR_No < orderedRobots.Count(); iMiR_No++)
                {
                    var robot = orderedRobots[iMiR_No];

                    bool ViewResult = false;
                    if (ConfigData.DisplayMapNames is Dictionary<int, string>)
                    {
                        foreach (var data in ConfigData.DisplayMapNames)
                        {
                            if (robot.MapID == data.Value)
                                ViewResult = true;
                        }
                    }

                    if (robot.RobotID > 0 && ViewResult)
                    {
                        DataRow row = GridDT.NewRow();
                        row["DGV_MiR_Status_Robot_Alias"] = robot.RobotAlias.ToString();
                        row["DGV_MiR_State"] = robot.Fleet_State == FleetState.unavailable ? robot.Fleet_State_Text : robot.StateText;

                        ritem = new RepositoryItemProgressBar();
                        ritem.Minimum = 0;
                        ritem.Maximum = 100;
                        dtgAuto_MiR_Status.RepositoryItems.Add(ritem);
                        if (robot.JobId != 0)
                        {
                            var Missions = uow.Missions.GetAll().Where(x => x.JobId == robot.JobId);

                            int count = Missions.Count();
                            var ExecMission = Missions.FirstOrDefault(x => x.MissionState == "Executing");
                            var JobsMissions = uow.Jobs.GetAll().FirstOrDefault(x => x.Id == robot.JobId);

                            if (ExecMission != null)
                            {
                                if (JobsMissions.MissionId1 == ExecMission.Id)
                                    row["DGV_ProgressBar"] = (100 / count);
                                else if (JobsMissions.MissionId2 == ExecMission.Id)
                                    row["DGV_ProgressBar"] = (200 / count);
                                else if (JobsMissions.MissionId3 == ExecMission.Id)
                                    row["DGV_ProgressBar"] = (270 / count);
                                else if (JobsMissions.MissionId4 == ExecMission.Id)
                                    row["DGV_ProgressBar"] = (400 / count);
                                else if (JobsMissions.MissionId5 == ExecMission.Id)
                                    row["DGV_ProgressBar"] = (450 / count);

                                robot.ProgressBar = Convert.ToInt16(row["DGV_ProgressBar"]);
                            }
                            else
                                row["DGV_ProgressBar"] = robot.ProgressBar;
                        }
                        else
                            row["DGV_ProgressBar"] = 0;

                        var PositionValue = uow.PositionAreaConfigs.GetAll().FirstOrDefault(x => x.PositionWaitTimeLog == true && x.PositionAreaName == robot.PositionZoneName);
                        if (PositionValue != null)
                            row["DGV_PositionWaitTime"] = "Area";
                        else
                            row["DGV_PositionWaitTime"] = "Not Area";

                        row["DGV_MiR_Status_Battery_Percent"] = robot.BatteryPercent.ToString("0.00") + "%";

                        var job = uow.Jobs.Find(x => x.Id == robot.JobId).FirstOrDefault();
                        if (job != null)
                            row["DGV_MiR_Status_CallName"] = job.CallName;
                        else
                            row["DGV_MiR_Status_CallName"] = "";

                        if (job != null && job.CallName != null)
                        {
                            row["DGV_Source"] = job.CallName.Split('_')[0];
                            row["DGV_Dest"] = job.CallName.Split('_')[1];
                        }
                        else
                        {
                            row["DGV_Source"] = "";
                            row["DGV_Dest"] = "";
                        }

                        row["DGV_WaitTime"] = uow.PositionWaitTimeRepository.GetAll().FirstOrDefault(x => x.RobotName == robot.RobotName && x.RobotAlias == robot.RobotAlias)?.ElapsedTime ?? "";

                        uow.ElevatorStateRepository.Load();
                        var ElevatorState = uow.ElevatorStateRepository.GetAll();
                        var RobotName = ElevatorState.FirstOrDefault(x => x.RobotName == robot.RobotName);
                        int ElevatorOrder = ElevatorState.IndexOf(RobotName);
                        if (ElevatorOrder != -1)
                            row["DGV_ElevatorOrder"] = Convert.ToString((ElevatorOrder + 1) + "번째");
                        else
                            row["DGV_ElevatorOrder"] = "";

                        row["DGV_MiR_Status_Product"] = robot.Product ?? "";

                        var products = uow.Products.Get10ProductsByRobotName(robot.RobotName);
                        var productNames = products.Select(x => x.ProductName).ToList();
                        string productInfo = string.Join("\r\n", productNames);
                        row["DGV_MiR_Status_Product_Detail"] = productInfo;

                        te = new RepositoryItemTextEdit();
                        dtgAuto_MiR_Status.RepositoryItems.Add(te);
                        row["DGV_MiR_Status_Door_T"] = robot.Door ?? "";


                        string[] Number = robot.RobotAlias.Split('#');

                        if (Number.Length >= 2)
                        {
                            bool result = Int16.TryParse(Number[1], out short number);

                            if (result)
                                row["DGV_MiR_Status_Robot_Name_orderby"] = Convert.ToInt16(Number[1]);
                            else
                                row["DGV_MiR_Status_Robot_Name_orderby"] = Convert.ToInt16(0);
                        }
                        else
                            row["DGV_MiR_Status_Robot_Name_orderby"] = Convert.ToInt16(0);

                        GridDT.Rows.InsertAt(row, GridCount);
                        GridCount++;
                    }
                }

                if (Flag)
                {
                    dtgAuto_MiR_Status.DataSource = GridDT;

                    //Main GridControl 설정 값들
                    gridView1.RowHeight = 45;
                    gridView1.OptionsView.RowAutoHeight = true;
                    gridView1.OptionsView.ShowIndicator = false;
                    gridView1.OptionsView.ShowGroupPanel = false;
                    gridView1.OptionsBehavior.Editable = false;
                    gridView1.OptionsView.ShowHorizontalLines = DefaultBoolean.False;
                    gridView1.OptionsView.ShowVerticalLines = DefaultBoolean.False;
                    gridView1.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
                    gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                    gridView1.FocusRectStyle = DrawFocusRectStyle.None;

                    RepositoryItemMemoEdit pMemo = new RepositoryItemMemoEdit();
                    gridView1.Columns["DGV_MiR_Status_Product_Detail"].ColumnEdit = pMemo;
                    gridView1.Columns["DGV_ProgressBar"].ColumnEdit = ritem;
                    gridView1.Columns["DGV_MiR_Status_Door_T"].ColumnEdit = te;

                    gridView1.Columns["DGV_MiR_Status_Robot_Name_orderby"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    gridView1.Columns["DGV_MiR_Status_Robot_Name_orderby"].Visible = false;
                    gridView1.Columns["DGV_MiR_Status_CallName"].Visible = false;
                    gridView1.Columns["DGV_ElevatorOrder"].Visible = false;
                    gridView1.Columns["DGV_MiR_Status_Product"].Visible = false;
                    gridView1.Columns["DGV_MiR_Status_Product_Detail"].Visible = false;
                    gridView1.Columns["DGV_PositionWaitTime"].Visible = false;
                    gridView1.Columns["DGV_MiR_Status_Door_T"].Visible = false;

                    gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold;
                    gridView1.Appearance.HeaderPanel.Font = textFont1;
                    gridView1.Appearance.Row.Font = textFont1;

                    gridView1.OptionsSelection.MultiSelect = true;
                    gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;

                    Flag = false;
                }

                gridView1.ClearSelection();
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                mainForm.EventLog("AutoScreen", "subFunc_MiR_Status_Display() Fail = " + ex);
                mainForm.ACS_UI_Log("AutoScreen" + "/" + "subFunc_MiR_Status_Display() Fail = " + ex);
            }
        }

        /// <summary>
        /// 버튼으로 따로 뺌. MainGrid에서 안씀
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgAuto_MiR_Status_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridView grid = sender as DataGridView;
                    DataGridViewCell clickCell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    string robotName = grid.Rows[e.RowIndex].Cells["DGV_MiR_Status_Robot_Name"].Value as string;

                    if (clickCell is DataGridViewButtonCell)
                    {
                        if (e.ColumnIndex == grid.Columns["DGV_MiR_Status_CountWeekly"].Index) // weekly count
                        {
                            var result = GetData_1Week(robotName);
                            //var f = new JobCountView("Weekly Job Count", result);
                            //f.ShowDialog(this);
                            return;
                        }
                        else if (e.ColumnIndex == grid.Columns["DGV_MiR_Status_CountMonthly"].Index) // monthly count
                        {
                            var result = GetData_1Month(robotName);
                            //var f = new JobCountView("Monthly Job Count", result);
                            //f.ShowDialog(this);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mainForm.EventLog("AutoScreen", "DtgAuto_MiR_Status_CellClick() Fail = " + ex);
                mainForm.ACS_UI_Log("AutoScreen" + "/" + "DtgAuto_MiR_Status_CellClick() Fail = " + ex);
            }
        }

        private dynamic GetData_1Day(string robotName)
        {
            var fromDate = DateTime.Today;
            var toDate = DateTime.Today.AddDays(1);
            return QueryDB(fromDate, toDate, robotName).FirstOrDefault();
        }

        private IEnumerable<dynamic> GetData_1Week(string robotName)
        {
            var fromDate = DateTime.Today.AddDays(-7);
            var toDate = DateTime.Today.AddDays(1);
            return QueryDB(fromDate, toDate, robotName);
        }

        private IEnumerable<dynamic> GetData_1Month(string robotName)
        {
            var fromDate = DateTime.Today.AddMonths(-1);
            var toDate = DateTime.Today.AddDays(1);
            return QueryDB(fromDate, toDate, robotName);
        }

        private IEnumerable<dynamic> QueryDB(DateTime searchDate1, DateTime searchDate2, string robotName, [CallerMemberName] string callerName = null)
        {
            string connectionString = ConnectionStrings.DB1; //ConfigurationManager.ConnectionStrings["Connection1"].ConnectionString;

            string SELECT_SQL = @"
                            SELECT 
                                FORMAT(CONVERT(date,JobFinishTime), 'yyyy-MM-dd') [JobFinishDate], RobotName, COUNT(*) [JobCount]
                            FROM 
                                JobHistory 
                            WHERE 
                                JobFinishTime >= @searchDate1 AND JobFinishTime < @searchDate2 
                                AND RobotName = @robotName
                            GROUP BY 
                                CONVERT(date,JobFinishTime), RobotName
                            ORDER BY JobFinishDate";

            //string SELECT_SQL = @"
            //                SELECT 
            //                    FORMAT(CONVERT(date,JobFinishTime), 'yyyy-MM-dd') [JobFinishDate], RobotName, CallName, COUNT(*) [JobCount]
            //                FROM 
            //                    JobHistory 
            //                WHERE 
            //                    JobFinishTime >= @searchDate1 AND JobFinishTime < @searchDate2 
            //                    AND RobotName = @robotName
            //                GROUP BY 
            //                    CONVERT(date,JobFinishTime), RobotName, CallName
            //                ORDER BY JobFinishDate";

            using (var con = new SqlConnection(connectionString))
            {
                var result = con.Query(SELECT_SQL, new { searchDate1, searchDate2, robotName });
                var viewResult = result.Select(x => new
                {
                    Date = x.JobFinishDate,
                    Robot = x.RobotName,
                    //JobName = x.CallName,
                    JobCount = x.JobCount,
                });
                return viewResult;
            }
        }
        #endregion

        #region Map 관련
        private void Map_Load()
        {
            //선택한 모니터가 있는지 확인
            if (ConfigData.DisplayMapNames is Dictionary<int, string>)
            {
                //선택한 모니터랑 이전 선택한 모니터 비교
                if (ConfigData.DisplayMapNames.Except(Sub_DisplayMapNames).Any() || ConfigData.DisplayMapNames.Count != Sub_DisplayMapNames.Count)
                {
                    //값 복사
                    Sub_DisplayMapNames.Clear();

                    foreach (KeyValuePair<int, string> pair in ConfigData.DisplayMapNames)
                        Sub_DisplayMapNames.Add(pair.Key, pair.Value);

                    //TableLayoutPanel 행 열 정하기
                    TableLayoutPanelRowColumn();

                    //TableLayoutPanel안에 지도 띄우기
                    DrawMap();
                }
                else if (ConfigData.DisplayMapNames.Count == 0)//선택한 화면이 없을 경우
                {
                    //TableLayoutPanel 행 열 정하기
                    TableLayoutPanelRowColumn();
                }
            }
        }

        private void TableLayoutPanelRowColumn()
        {
            //MainGrid 크기 조절
            splitContainerControl1.SplitterPosition = ((this.Height / 3) * 2);

            if (ConfigData.DisplayMapNames.Count == 0)
            {
                splitContainer2.IsSplitterFixed = false;
                splitContainer2.SplitterPosition = splitContainer2.Height;
                splitContainer2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                splitContainer3.IsSplitterFixed = false;
                splitContainer3.SplitterPosition = splitContainer3.Height;
                splitContainer3.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;

                SP_Top.Horizontal = false;
                SP_Top.IsSplitterFixed = false;
                SP_Top.SplitterPosition = SP_Top.Height;
                SP_Top.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;

                SP_Top.Visible = false;
                SP_Top.Enabled = false;
                SP_Middle.Visible = false;
                SP_Middle.Enabled = false;
                SP_Bottom.Visible = false;
                SP_Bottom.Enabled = false;
            }
            else if (ConfigData.DisplayMapNames.Count == 1)
            {
                splitContainer2.IsSplitterFixed = false;
                splitContainer2.SplitterPosition = splitContainer2.Height;
                splitContainer2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                splitContainer3.IsSplitterFixed = false;
                splitContainer3.SplitterPosition = splitContainer3.Height;
                splitContainer3.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;

                SP_Top.Horizontal = false;
                SP_Top.IsSplitterFixed = false;
                SP_Top.SplitterPosition = SP_Top.Height;
                SP_Top.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;

                SP_Top.Visible = true;
                SP_Top.Enabled = true;
                SP_Middle.Visible = false;
                SP_Middle.Enabled = false;
                SP_Bottom.Visible = false;
                SP_Bottom.Enabled = false;
            }
            else if (ConfigData.DisplayMapNames.Count == 2)
            {
                splitContainer2.IsSplitterFixed = false;
                splitContainer2.SplitterPosition = splitContainer2.Height;
                splitContainer2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                splitContainer3.IsSplitterFixed = false;
                splitContainer3.SplitterPosition = splitContainer3.Height;
                splitContainer3.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;

                SP_Top.Horizontal = false;
                SP_Top.IsSplitterFixed = false;
                SP_Top.SplitterPosition = SP_Top.Height / 2;
                SP_Top.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;

                SP_Top.Visible = true;
                SP_Top.Enabled = true;
                SP_Middle.Visible = false;
                SP_Middle.Enabled = false;
                SP_Bottom.Visible = false;
                SP_Bottom.Enabled = false;
            }
            else if (ConfigData.DisplayMapNames.Count == 3)
            {
                splitContainer2.IsSplitterFixed = false;
                splitContainer2.SplitterPosition = splitContainer2.Height;
                splitContainer2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                splitContainer3.IsSplitterFixed = false;
                splitContainer3.SplitterPosition = splitContainer3.Height / 2;
                splitContainer3.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;

                SP_Top.Horizontal = true;
                SP_Top.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                SP_Middle.IsSplitterFixed = false;
                SP_Middle.SplitterDistance = SP_Middle.Width;

                SP_Top.Visible = true;
                SP_Top.Enabled = true;
                SP_Middle.Visible = true;
                SP_Middle.Enabled = true;
                SP_Bottom.Visible = false;
                SP_Bottom.Enabled = false;
            }
            else if (ConfigData.DisplayMapNames.Count == 4)
            {
                splitContainer2.IsSplitterFixed = false;
                splitContainer2.SplitterPosition = splitContainer2.Height;
                splitContainer2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                splitContainer3.IsSplitterFixed = false;
                splitContainer3.SplitterPosition = splitContainer3.Height / 2;
                splitContainer3.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;

                SP_Top.Horizontal = true;
                SP_Top.IsSplitterFixed = false;
                SP_Top.SplitterPosition = SP_Top.Width / 2;
                SP_Top.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                SP_Middle.IsSplitterFixed = false;
                SP_Middle.SplitterDistance = SP_Middle.Width / 2;

                SP_Top.Visible = true;
                SP_Top.Enabled = true;
                SP_Middle.Visible = true;
                SP_Middle.Enabled = true;
                SP_Bottom.Visible = false;
                SP_Bottom.Enabled = false;
            }
            else if (ConfigData.DisplayMapNames.Count == 5)
            {
                splitContainer2.IsSplitterFixed = false;
                splitContainer2.SplitterPosition = Convert.ToInt32(splitContainer2.Height / 1.5);
                splitContainer2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                splitContainer3.IsSplitterFixed = false;
                splitContainer3.SplitterPosition = Convert.ToInt32(splitContainer3.Height / 3);
                splitContainer3.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;

                SP_Top.Horizontal = true;
                SP_Top.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                SP_Bottom.IsSplitterFixed = false;
                SP_Bottom.SplitterDistance = SP_Bottom.Width;

                SP_Top.Visible = true;
                SP_Top.Enabled = true;
                SP_Middle.Visible = true;
                SP_Middle.Enabled = true;
                SP_Bottom.Visible = true;
                SP_Bottom.Enabled = true;
            }
            else if (ConfigData.DisplayMapNames.Count <= 6)
            {
                splitContainer2.IsSplitterFixed = false;
                splitContainer2.SplitterPosition = Convert.ToInt32(splitContainer2.Height / 1.5);
                splitContainer2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                splitContainer3.IsSplitterFixed = false;
                splitContainer3.SplitterPosition = Convert.ToInt32(splitContainer3.Height / 3);
                splitContainer3.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;

                SP_Top.Horizontal = true;
                SP_Top.IsSplitterFixed = false;
                SP_Top.SplitterPosition = SP_Top.Width / 2;
                SP_Top.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                SP_Middle.IsSplitterFixed = false;
                SP_Middle.SplitterDistance = SP_Middle.Width / 2;
                SP_Bottom.IsSplitterFixed = false;
                SP_Bottom.SplitterDistance = SP_Bottom.Width / 2;

                SP_Top.Visible = true;
                SP_Top.Enabled = true;
                SP_Middle.Visible = true;
                SP_Middle.Enabled = true;
                SP_Bottom.Visible = true;
                SP_Bottom.Enabled = true;
            }
        }

        private void DrawMap()
        {
            string fleetIp = ConfigurationManager.AppSettings["sFleet_IP_Address_SV"];
            List<MapData> mapDatas = new List<MapData>();

            //MapData 코딩으로 넣어둠
            mapDatas.Clear();
            { MapData Data = new MapData { mapScale = 0.13333334f, mouseFirstLocation = new Point(263, 266), mouseMoveOffset = new Point(-150, 64), FloorName = "M3F" }; mapDatas.Add(Data); }
            { MapData Data = new MapData { mapScale = 0.2166666f, mouseFirstLocation = new Point(146, 279), mouseMoveOffset = new Point(-96, 113), FloorName = "T3F" }; mapDatas.Add(Data); }

            //Map Task 끄기
            MapInit();

            //Robotzoom 데이터 초기화
            RobotzoomData.Clear();

            //Map 그리기
            int Datacount = 1;
            foreach (var item in ConfigData.DisplayMapNames)
            {
                //선택한 맵 데이터 불러오기
                if (mapDBdatas.Exists(x => x.Id == item.Key))
                {
                    //DB에서 데이터 가져오기
                    var mapDBdata = mapDBdatas.Find(x => x.Id == item.Key);

                    //위에 MapData에서 데이터 가져오기
                    var Data = new MapData();
                    if (mapDatas.Exists(x => x.FloorName == mapDBdata.FloorName))
                        Data = mapDatas.Find(x => x.FloorName == mapDBdata.FloorName);
                    else
                        Data = new MapData { mapScale = 0.600f, mouseFirstLocation = new Point(559, 420), mouseMoveOffset = new Point(0, 50), FloorName = mapDBdata.FloorName };//기본값

                    if (Datacount == 1)
                    {
                        mainForm.DrawUCMapView(ucMapView1, mapDBdata, Data, fleetIp, true);

                        RobotZoom robotZoom = new RobotZoom();
                        robotZoom.view = ucMapView1;
                        robotZoom.mapDBdata = mapDBdata;
                        robotZoom.Data = Data;

                        if (!RobotzoomData.ContainsKey(mapDBdata.MapID))
                            RobotzoomData.Add(mapDBdata.MapID, robotZoom);
                    }
                    else if (Datacount == 2)
                    {
                        mainForm.DrawUCMapView(ucMapView2, mapDBdata, Data, fleetIp, true);

                        RobotZoom robotZoom = new RobotZoom();
                        robotZoom.view = ucMapView2;
                        robotZoom.mapDBdata = mapDBdata;
                        robotZoom.Data = Data;

                        if (!RobotzoomData.ContainsKey(mapDBdata.MapID))
                            RobotzoomData.Add(mapDBdata.MapID, robotZoom);
                    }
                    else if (Datacount == 3)
                    {
                        mainForm.DrawUCMapView(ucMapView3, mapDBdata, Data, fleetIp, true);

                        RobotZoom robotZoom = new RobotZoom();
                        robotZoom.view = ucMapView3;
                        robotZoom.mapDBdata = mapDBdata;
                        robotZoom.Data = Data;

                        if (!RobotzoomData.ContainsKey(mapDBdata.MapID))
                            RobotzoomData.Add(mapDBdata.MapID, robotZoom);
                    }
                    else if (Datacount == 4)
                    {
                        mainForm.DrawUCMapView(ucMapView4, mapDBdata, Data, fleetIp, true);

                        RobotZoom robotZoom = new RobotZoom();
                        robotZoom.view = ucMapView4;
                        robotZoom.mapDBdata = mapDBdata;
                        robotZoom.Data = Data;

                        if (!RobotzoomData.ContainsKey(mapDBdata.MapID))
                            RobotzoomData.Add(mapDBdata.MapID, robotZoom);
                    }
                    else if (Datacount == 5)
                    {
                        mainForm.DrawUCMapView(ucMapView5, mapDBdata, Data, fleetIp, true);

                        RobotZoom robotZoom = new RobotZoom();
                        robotZoom.view = ucMapView5;
                        robotZoom.mapDBdata = mapDBdata;
                        robotZoom.Data = Data;

                        if (!RobotzoomData.ContainsKey(mapDBdata.MapID))
                            RobotzoomData.Add(mapDBdata.MapID, robotZoom);
                    }
                    else if (Datacount == 6)
                    {
                        mainForm.DrawUCMapView(ucMapView6, mapDBdata, Data, fleetIp, true);

                        RobotZoom robotZoom = new RobotZoom();
                        robotZoom.view = ucMapView6;
                        robotZoom.mapDBdata = mapDBdata;
                        robotZoom.Data = Data;

                        if (!RobotzoomData.ContainsKey(mapDBdata.MapID))
                            RobotzoomData.Add(mapDBdata.MapID, robotZoom);
                    }
                }

                Datacount++;
            }
        }

        private void MapInit()
        {
            //초기화
            ucMapView1.ReStartResetData();
            ucMapView2.ReStartResetData();
            ucMapView3.ReStartResetData();
            ucMapView4.ReStartResetData();
            ucMapView5.ReStartResetData();
            ucMapView6.ReStartResetData();
        }
        #endregion

        #region Timer
        private void AutoDisplay_Timer_Tick(object sender, EventArgs e)
        {
            AutoDisplay_Timer.Enabled = false;
            subFunc_MiR_Status_Display();
            Map_Load();
            AutoDisplay_Timer.Interval = 1000; // 타이머 인터벌 1초로 설정!
            AutoDisplay_Timer.Enabled = true;
        }
        #endregion

        #region Main GridControl
        private bool ChangeColor = true;
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;

            string strPosition = view.GetRowCellDisplayText(e.RowHandle, "DGV_PositionWaitTime");
            string strRobotName = view.GetRowCellDisplayText(e.RowHandle, "DGV_MiR_Status_Robot_Alias");


            if (strPosition == "Area")
            {
                PositionValue.TryGetValue(strRobotName, out Stopwatch value);
                value.Start();

                if (e.Column == view.Columns["DGV_MiR_Status_Robot_Alias"])
                {
                    if (value.Elapsed.Seconds % 2 == 0)
                        ChangeColor = true;
                    else
                        ChangeColor = false;

                    if (ChangeColor)
                        e.Appearance.BackColor = Color.SkyBlue;
                    else
                        e.Appearance.BackColor = Color.Transparent;
                }
                if (e.Column == view.Columns["DGV_ProgressBar"])
                {
                    if (ChangeColor)
                        e.Appearance.BackColor = Color.SkyBlue;
                    else
                        e.Appearance.BackColor = Color.Transparent;
                }
                if (e.Column == view.Columns["DGV_Source"])
                {
                    if (ChangeColor)
                        e.Appearance.BackColor = Color.SkyBlue;
                    else
                        e.Appearance.BackColor = Color.Transparent;
                }
                if (e.Column == view.Columns["DGV_Dest"])
                {
                    if (ChangeColor)
                        e.Appearance.BackColor = Color.SkyBlue;
                    else
                        e.Appearance.BackColor = Color.Transparent;
                }
                if (e.Column == view.Columns["DGV_MiR_Status_Battery_Percent"])
                {
                    if (ChangeColor)
                        e.Appearance.BackColor = Color.SkyBlue;
                    else
                        e.Appearance.BackColor = Color.Transparent;
                }
                if (e.Column == view.Columns["DGV_WaitTime"])
                {
                    if (ChangeColor)
                        e.Appearance.BackColor = Color.SkyBlue;
                    else
                        e.Appearance.BackColor = Color.Transparent;
                }

                if (value.Elapsed.Seconds > 10)
                {
                    value.Stop();
                    value.Reset();
                }
            }
            else
                e.Appearance.BackColor = Color.Transparent;

            if (e.Column == view.Columns["DGV_MiR_State"])
            {
                string category = view.GetRowCellDisplayText(e.RowHandle, view.Columns["DGV_MiR_State"]);

                if (category == "Ready")
                    e.Appearance.BackColor = Color.LightBlue;
                else if (category == "Pause" || category == "ManualControl")
                    e.Appearance.BackColor = Color.Yellow;
                else if (category == "Executing")
                    e.Appearance.BackColor = Color.Chartreuse;
                else if (category == "Error" || category == "EmergencyStop")
                    e.Appearance.BackColor = Color.OrangeRed;
                else if (category == "unavailable")
                    e.Appearance.BackColor = Color.DimGray;
            }

            if (e.Column == view.Columns["DGV_MiR_Status_Door_D"])
            {
                string category = view.GetRowCellDisplayText(e.RowHandle, view.Columns["DGV_MiR_Status_Door_T"]);

                Point point = new Point(e.Bounds.X + (e.Column.Width / 4), e.Bounds.Y);
                if (category == "Door닫힘")
                    e.Cache.DrawImage(Properties.Resources.Door_Close, point);
                else
                    e.Cache.DrawImage(Properties.Resources.Door_Open, point);
            }
        }

        private Dictionary<string, Stopwatch> PositionValue = new Dictionary<string, Stopwatch>();
        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            e.Appearance.Options.UseBackColor = true;

            GridView view = sender as GridView;

            string strPosition = view.GetRowCellDisplayText(e.RowHandle, "DGV_PositionWaitTime");
            string strRobotName = view.GetRowCellDisplayText(e.RowHandle, "DGV_MiR_Status_Robot_Alias");

            if (!PositionValue.ContainsKey(strRobotName))
            {
                Stopwatch stopwatch = new Stopwatch();
                PositionValue.Add(strRobotName, stopwatch);
            }
        }

        private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            GridView view = sender as GridView;

            RepositoryItemProgressBar _prbLess25;
            RepositoryItemProgressBar _prbLess50;
            RepositoryItemProgressBar _prbLess75;
            RepositoryItemProgressBar _prbLess100;

            _prbLess25 = new RepositoryItemProgressBar();
            _prbLess25.StartColor = Color.Red;
            _prbLess25.EndColor = Color.Red;
            _prbLess25.ShowTitle = true;
            _prbLess25.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            _prbLess25.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            _prbLess25.LookAndFeel.UseDefaultLookAndFeel = false;

            _prbLess50 = new RepositoryItemProgressBar();
            _prbLess50.StartColor = Color.Orange;
            _prbLess50.EndColor = Color.Orange;
            _prbLess50.ShowTitle = true;
            _prbLess50.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            _prbLess50.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            _prbLess50.LookAndFeel.UseDefaultLookAndFeel = false;

            _prbLess75 = new RepositoryItemProgressBar();
            _prbLess75.StartColor = Color.YellowGreen;
            _prbLess75.EndColor = Color.YellowGreen;
            _prbLess75.ShowTitle = true;
            _prbLess75.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            _prbLess75.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            _prbLess75.LookAndFeel.UseDefaultLookAndFeel = false;

            _prbLess100 = new RepositoryItemProgressBar();
            _prbLess100.StartColor = Color.LightGreen;
            _prbLess100.EndColor = Color.LightGreen;
            _prbLess100.ShowTitle = true;
            _prbLess100.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            _prbLess100.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            _prbLess100.LookAndFeel.UseDefaultLookAndFeel = false;

            if (e.Column == view.Columns["DGV_ProgressBar"])
            {
                int percent = Convert.ToInt16(e.CellValue);
                if (percent < 25)
                    e.RepositoryItem = _prbLess25;
                else if (percent < 50)
                    e.RepositoryItem = _prbLess50;
                else if (percent < 75)
                    e.RepositoryItem = _prbLess75;
                else if (percent <= 100)
                    e.RepositoryItem = _prbLess100;
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            string fleetIp = ConfigurationManager.AppSettings["sFleet_IP_Address_SV"];

            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            if (info.InRow || info.InRowCell)
            {
                string RobotAlias = gridView1.GetDataRow(info.RowHandle)["DGV_MiR_Status_Robot_Alias"].ToString();

                var RobotData = uow.Robots.GetAll().FirstOrDefault(x => x.RobotAlias == RobotAlias);

                if (RobotData != null)
                {
                    RobotzoomData.TryGetValue(RobotData.MapID, out RobotZoom value);

                    value.view.StopLoop();

                    //T3F
                    //Test 수정진행중 [김익교]
                    //value.Data.mouseMoveOffset = new Point(-1375, -106);
                    //value.Data.mouseFirstLocation = new Point(346, 176);
                    if (RobotData.MapID == "f8b799ff-2879-11ef-b062-00012961a136")
                    {
                        //OFFSet =0 , =0 
                        //robot좌표 31.4 /148.8
                        //X는 31.4보다 작으면 +
                        //X는 31.4보다 크면 -
                        //Y는 148.8보다 작으면 -
                        //Y는 148.8보다 크면 +
                        int MapSizeWidth = (820 - value.view.ClientSize.Width) / 2;
                        int MapSizeHeight = (311 - value.view.ClientSize.Height) / 2;

                        double OffSetRobotPOSX = (RobotData.Position_X - 31.4) * -1;
                        double OffsetRobotPOSY = (148.8 - RobotData.Position_Y) * -1;
                        int OffSetx = (int)Math.Round(OffSetRobotPOSX * 9.2) - MapSizeWidth;
                        int Offsety = (int)Math.Round(OffsetRobotPOSY * 10.2) - MapSizeHeight;
                        value.Data.mapScale = (float)0.5;
                        value.Data.mouseMoveOffset = new Point(OffSetx, Offsety);
                    }

                    else if (RobotData.MapID == "55023e70-4df7-11ed-80b2-000129af8f1d") // M3F
                    {
                        //OFFSet =0 , =0 
                        //robot좌표 29.650 /124.600
                        //X는 29.6보다 작으면 +
                        //X는 29.6보다 크면 -
                        //Y는 124.6보다 작으면 -
                        //Y는 124.6보다 크면 +
                        int MapSizeWidth = (820 - value.view.ClientSize.Width) / 2;
                        int MapSizeHeight = (311 - value.view.ClientSize.Height) / 2;

                        double OffSetRobotPOSX = (RobotData.Position_X - 30) * -1;
                        double OffsetRobotPOSY = (124.6 - RobotData.Position_Y) * -1;
                        int OffSetx = (int)Math.Round(OffSetRobotPOSX * 9.2) - MapSizeWidth;
                        int Offsety = (int)Math.Round(OffsetRobotPOSY * 10.2) - MapSizeHeight;
                        value.Data.mapScale = (float)0.5;
                        value.Data.mouseMoveOffset = new Point(OffSetx, Offsety);
                    }

                    else if (RobotData.MapID == "d33ff60f-27e2-11ef-8f55-a41cb4009956") // T4F
                    {
                        //OFFSet =0 , =0 
                        //robot좌표 29.800 /50.300
                        //X는 29.8보다 작으면 +
                        //X는 29.8보다 크면 -
                        //Y는 60.3보다 작으면 -
                        //Y는 60.3보다 크면 +
                        int MapSizeWidth = (820 - value.view.ClientSize.Width) / 2;
                        int MapSizeHeight = (311 - value.view.ClientSize.Height) / 2;
                        double OffSetRobotPOSX = (RobotData.Position_X - 29.8) * -1;
                        double OffsetRobotPOSY = (60.3 - RobotData.Position_Y) * -1;
                        int OffSetx = (int)Math.Round(OffSetRobotPOSX * 9.2) - MapSizeWidth;
                        int Offsety = (int)Math.Round(OffsetRobotPOSY * 10.2) - MapSizeHeight;
                        value.Data.mapScale = (float)0.8;
                        value.Data.mouseMoveOffset = new Point(OffSetx, Offsety);
                    }

                    mainForm.DrawUCMapView(value.view, value.mapDBdata, value.Data, fleetIp, false);
                }
                else
                    MessageBox.Show("로봇을 찾을 수 없습니다.");
            }
        }
        #endregion
    }

    public class RobotZoom
    {
        public UCMapView view { get; set; }
        public MapNameAlias mapDBdata { get; set; }
        public MapData Data { get; set; }
    }
}