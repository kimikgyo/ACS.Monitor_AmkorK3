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
using Monitor.Common;
using Newtonsoft.Json;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;

namespace ACS.Monitor
{
    public partial class AutoScreen : Form
    {
        public string S_UserNumber = "";
        public string S_UserName = "";

        private readonly MainForm mainForm;
        private readonly IUnitOfWork uow;
        private readonly Font textFont1 = new Font("고딕", 12);
        private readonly Font textFont2 = new Font("Arial", 10);
        private readonly Font textFont3 = new Font("Arial", 9);
        private readonly Font gridFont1 = new Font("Arial", 10, FontStyle.Bold);
        private readonly Font gridFont2 = new Font("Arial", 9, FontStyle.Bold);

        public Dictionary<string, string> Sub_DisplayMapNames = new Dictionary<string, string>();
        private DataTable RobotdataTable = new DataTable();



        #region RepositroyItem 종류 정리
        //RepositoryItemTextEdit: 텍스트 입력을 위한 편집기입니다.
        //RepositoryItemComboBox: 드롭다운 목록에서 선택할 수 있는 콤보 박스입니다.
        //RepositoryItemCheckEdit: 체크박스 편집기입니다.
        //RepositoryItemDateEdit: 날짜 선택을 위한 편집기입니다.
        //RepositoryItemSpinEdit: 스핀 박스 형태의 숫자 입력 편집기입니다.
        //RepositoryItemProgressBar: 진행 상황을 표시하는 프로그레스 바입니다.
        //RepositoryItemPictureEdit: 이미지 편집기입니다.
        //RepositoryItemLookUpEdit: 다른 데이터 소스에서 선택할 수 있는 편집기입니다.
        //RepositoryItemButtonEdit: 버튼을 포함한 편집기입니다.
        //RepositoryItemMemoExEdit: 다중 행 텍스트 입력을 위한 편집기입니다.
        #endregion

        public AutoScreen(MainForm mainForm, IUnitOfWork uow)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            this.uow = uow;

            //배경색 지정
            this.BackColor = Color.FromArgb(249, 219, 186);

            GridViewInit();
            DataTableColumnsCreate();


        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.BackColor = Color.White;
            Form parentForm = Helpers.GetParentForm(this);
            if (parentForm != null) parentForm.FormClosed += ParentForm_FormClosed;


#if MIRDEMO
            string fleetIp = "localhost:5000";
#else
            string fleetIp = ConfigurationManager.AppSettings["sFleet_IP_Address_SV"];
#endif
            #region Config Map Name 가져오기

            string MapName = ConfigurationManager.AppSettings["MapNames"];
            ConfigData.DisplayMapNames = Helpers.ConvertStringToDictionary(MapName) ?? new Dictionary<string, string>();

            #endregion

            #region Config Robot Name 가져오기

            string RobotName = ConfigurationManager.AppSettings["RobotNames"];
            ConfigData.DisplayRobotNames = Helpers.ConvertStringToDictionary(RobotName) ?? new Dictionary<string, string>();

            #endregion

        }

        private void ParentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }

        private void GridViewInit()
        {
            #region GridView 초기 Setting

            //Main GridControl 설정 값들
            RobotGridView.RowHeight = 30;
            //높이조절 활성화
            RobotGridView.OptionsView.RowAutoHeight = true;
            //인디케이터 표시: gridView1.OptionsView.ShowIndicator를 true로 설정하면 각 행의 왼쪽에 인덱스 번호가 표시됩니다.
            RobotGridView.OptionsView.ShowIndicator = false;
            //그룹 패널 표시: gridView1.OptionsView.ShowGroupPanel을 true로 설정하면 그룹 패널이 표시됩니다. 사용자는 이 패널을 드래그하여 열을 그룹화할 수 있습니다.
            RobotGridView.OptionsView.ShowGroupPanel = false;
            //편집 가능: gridView1.OptionsBehavior.Editable을 true로 설정하면 사용자가 그리드의 셀을 클릭하여 데이터를 수정할 수 있습니다.
            RobotGridView.OptionsBehavior.Editable = false;
            //수평선 표시: gridView1.OptionsView.ShowHorizontalLines를 DefaultBoolean.True로 설정하면 수평선이 표시됩니다.
            RobotGridView.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            //수직선 표시: gridView1.OptionsView.ShowVerticalLines를 DefaultBoolean.True로 설정하면 수직선이 표시됩니다.
            RobotGridView.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            //자동 높이 조정 활성화: gridView1.OptionsView.ColumnHeaderAutoHeight를 DefaultBoolean.True로 설정합니다.
            RobotGridView.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
            //테두리 없음 설정: gridView1.BorderStyle를 DevExpress.XtraEditors.Controls.BorderStyles.NoBorder로 설정합니다.
            RobotGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //포커스 사각형 비활성화: gridView1.FocusRectStyle를 DrawFocusRectStyle.None으로 설정합니다.
            RobotGridView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            // HeaderPanel의 필터 기능 비활성화
            RobotGridView.OptionsCustomization.AllowFilter = false;
            //헤더 텍스트 중앙 정렬: gridView1.Appearance.HeaderPanel.TextOptions.HAlignment를 HorzAlignment.Center로 설정합니다.
            RobotGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //헤더 패널 폰트 설정: gridView1.Appearance.HeaderPanel.Font에 새로운 Font 객체를 할당합니다.
            RobotGridView.Appearance.HeaderPanel.Font = new Font("Arial", 30, FontStyle.Bold);
            //행 폰트 설정: gridView1.Appearance.Row.Font에 새로운 Font 객체를 할당합니다.
            RobotGridView.Appearance.Row.Font = new Font("Arial", 30, FontStyle.Bold);
            //다중 선택 활성화: gridView1.OptionsSelection.MultiSelect를 true로 설정합니다.
            RobotGridView.OptionsSelection.MultiSelect = false;
            //다중 선택 모드 설정: gridView1.OptionsSelection.MultiSelectMode를 GridMultiSelectMode.RowSelect로 설정합니다.
            RobotGridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            // 검색 패널 표시하기
            //gridView1.ShowFindPanel();
            // 검색 패널 숨기기
            RobotGridView.HideFindPanel();
            // 편집 가능 설정
            RobotGridView.OptionsBehavior.Editable = false;
            // DevExpress GridView에서 행 번호(인덱스)를 표시할지 여부를 제어하는 속성입니다. 이 속성을 사용하면 각 행의 왼쪽에 인덱스 번호를 표시할 수 있습니다.
            RobotGridView.OptionsView.ShowIndicator = false;
            //DevExpress GridView에서 그룹 패널의 표시 여부를 제어합니다. 그룹 패널은 사용자가 그리드에서 컬럼을 드래그하여 데이터를 그룹화할 수 있는 영역입니다.
            RobotGridView.OptionsView.ShowGroupPanel = false;
            //읽기전용
            RobotGridView.OptionsBehavior.ReadOnly = true;

            RobotGridView.BestFitColumns();


            #endregion
        }
        private void GridViewColumnsCreate()
        {
            #region GridView Columns 생성

            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_Robot_Alias", Caption = "로봇이름", Visible = false, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_ProgressBar", Caption = "진행률", Visible = false, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_Source", Caption = "출발지", Visible = false, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_Dest", Caption = "목적지", Visible = false, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_Battery_Percent", Caption = "배터리", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_CallName", Caption = "콜이름", Visible = false, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_WaitTime", Caption = "대기시간", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_State", Caption = "로봇상태", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_ElevatorOrder", Caption = "elevator순서", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_Product", Caption = "자재유무", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_Product_Detail", Caption = "자재정보", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_Door_D", Caption = "Door", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_Door_T", Caption = "Door1", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_Robot_Name_orderby", Caption = "순서", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_PositionWaitTime", Caption = "포지션값", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MissionText", Caption = "MissionText", Visible = true, UnboundType = UnboundColumnType.String });
            #endregion

            #region GridView Columns 정렬 설정

            RobotGridView.Columns["DGV_MiR_Status_Robot_Alias"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_MiR_Status_Robot_Alias"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_ProgressBar"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_ProgressBar"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_Source"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_Source"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_Dest"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_Dest"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_MiR_Status_Battery_Percent"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_MiR_Status_Battery_Percent"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_MiR_Status_CallName"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_MiR_Status_CallName"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_WaitTime"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_WaitTime"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_MiR_State"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_MiR_State"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_ElevatorOrder"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_ElevatorOrder"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_MiR_Status_Product"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_MiR_Status_Product"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_MiR_Status_Product_Detail"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_MiR_Status_Product_Detail"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_MiR_Status_Door_D"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_MiR_Status_Door_D"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_MiR_Status_Door_T"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_MiR_Status_Door_T"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_MiR_Status_Robot_Name_orderby"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_MiR_Status_Robot_Name_orderby"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_PositionWaitTime"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_PositionWaitTime"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotGridView.Columns["DGV_MissionText"].AppearanceCell.Options.UseTextOptions = true;
            RobotGridView.Columns["DGV_MissionText"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            #endregion

            #region GridView Event 설정


            #endregion
        }

        private void DataTableColumnsCreate()
        {
            #region DataTable Columns 생성

            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_MiR_Status_Robot_Alias", Caption = "로봇이름" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_ProgressBar", Caption = "진행률" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_Source", Caption = "출발지" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_Dest", Caption = "목적지" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_MiR_Status_Battery_Percent", Caption = "배터리" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_MiR_Status_CallName", Caption = "콜이름" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_WaitTime", Caption = "대기시간" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_MiR_State", Caption = "로봇상태" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_ElevatorOrder", Caption = "elevator순서" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_MiR_Status_Product", Caption = "자재유무" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_MiR_Status_Product_Detail", Caption = "자재정보" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_MiR_Status_Door_D", Caption = "Door" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_MiR_Status_Door_T", Caption = "Door1" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_MiR_Status_Robot_Name_orderby", Caption = "순서" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_PositionWaitTime", Caption = "포지션값" });
            RobotdataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_MissionText", Caption = "MissionText" });

            #endregion
        }

        private DataTable robotData()
        {
            #region GridControl 표기하기 위한 Data // return : RobotData

            RobotdataTable.Rows.Clear();
            var orderedRobots = uow.Robots.GetAll().Where(r => r.RobotID > 0 && ConfigData.DisplayRobotNames.ContainsKey(r.RobotName))
                                                   .OrderBy(r => r.RobotAlias).ToList();
            foreach (var robot in orderedRobots)
            {
                int GridCount = 0;

                DataRow row = RobotdataTable.NewRow();

                row["DGV_MiR_Status_Robot_Alias"] = robot.RobotAlias.ToString();
                row["DGV_MiR_State"] = robot.Fleet_State == FleetState.unavailable ? robot.Fleet_State_Text : robot.StateText;
                row["DGV_MiR_Status_Battery_Percent"] = robot.BatteryPercent.ToString("0.00") + "%";

                row["DGV_WaitTime"] = uow.PositionWaitTimes.GetAll().FirstOrDefault(x => x.RobotName == robot.RobotName && x.RobotAlias == robot.RobotAlias)?.ElapsedTime ?? "";
                //Job 상태
                if (robot.JobId > 0)
                {

                    var RunJobs = uow.Jobs.GetAll().FirstOrDefault(j => j.Id == robot.JobId);
                    var Missions = uow.Missions.GetAll().Where(m => m.JobId == robot.JobId);
                    var ExecMission = Missions.FirstOrDefault(e => e.MissionState == "Executing");
                    var ElevatorOrderAll = uow.ElevatorStates.GetAll();
                    var ElevatorOrderRobotNameSelect = ElevatorOrderAll.FirstOrDefault(x => x.RobotName == robot.RobotName);

                    if (RunJobs != null)
                    {
                        row["DGV_MiR_Status_CallName"] = RunJobs.CallName;
                        row["DGV_Source"] = RunJobs.CallName.Split('_')[0];
                        row["DGV_Dest"] = RunJobs.CallName.Split('_')[1];
                    }
                    else
                    {
                        row["DGV_MiR_Status_CallName"] = "";
                        row["DGV_Source"] = "";
                        row["DGV_Dest"] = "";
                    }


                    if (ExecMission != null)
                    {
                        if (RunJobs.MissionId1 == ExecMission.Id)
                            row["DGV_ProgressBar"] = (100 / Missions.Count());
                        else if (RunJobs.MissionId2 == ExecMission.Id)
                            row["DGV_ProgressBar"] = (200 / Missions.Count());
                        else if (RunJobs.MissionId3 == ExecMission.Id)
                            row["DGV_ProgressBar"] = (270 / Missions.Count());
                        else if (RunJobs.MissionId4 == ExecMission.Id)
                            row["DGV_ProgressBar"] = (400 / Missions.Count());
                        else if (RunJobs.MissionId5 == ExecMission.Id)
                            row["DGV_ProgressBar"] = (450 / Missions.Count());

                        robot.ProgressBar = Convert.ToInt16(row["DGV_ProgressBar"]);
                    }
                    else
                        row["DGV_ProgressBar"] = robot.ProgressBar;

                    if (ElevatorOrderRobotNameSelect != null)
                    {
                        int ElevatorOrderIndex = ElevatorOrderAll.IndexOf(ElevatorOrderRobotNameSelect);
                        row["DGV_ElevatorOrder"] = Convert.ToString((ElevatorOrderIndex + 1) + "번째");

                    }
                    else row["DGV_ElevatorOrder"] = "";

                }
                else
                {
                    row["DGV_ProgressBar"] = 0;
                    row["DGV_ElevatorOrder"] = "";
                }

                //Position WaitTime 
                if (PositionValue != null)
                    row["DGV_PositionWaitTime"] = "Area";
                else
                    row["DGV_PositionWaitTime"] = "Not Area";


                row["DGV_MiR_Status_Product"] = robot.Product ?? "";

                //var products = uow.Products.Get10ProductsByRobotName(robot.RobotName);
                //var productNames = products.Select(x => x.ProductName).ToList();
                //string productInfo = string.Join("\r\n", productNames);
                //row["DGV_MiR_Status_Product_Detail"] = productInfo;


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

                RobotdataTable.Rows.InsertAt(row, GridCount);
                GridCount++;
            }
            return RobotdataTable;

            #endregion
        }

        private void RobotStatusDisplay()
        {
            try
            {
                #region GridView Data 초기화

                RobotGridView.SelectAll();
                RobotGridView.DeleteSelectedRows();
                RobotGridControl.RepositoryItems.Clear();
                RobotdataTable.Clear();

                #endregion


                //데이터 표시하기
                RobotGridControl.DataSource = robotData();

                RepositoryItemProgressBar ritem = new RepositoryItemProgressBar();
                RepositoryItemTextEdit te = new RepositoryItemTextEdit();
                RepositoryItemMemoEdit pMemo = new RepositoryItemMemoEdit();

                ritem.Minimum = 0;
                ritem.Maximum = 100;

                RobotGridControl.RepositoryItems.Add(pMemo);
                RobotGridControl.RepositoryItems.Add(ritem);
                RobotGridControl.RepositoryItems.Add(te);

                RobotGridView.Columns["DGV_MiR_Status_Product_Detail"].ColumnEdit = pMemo;
                RobotGridView.Columns["DGV_ProgressBar"].ColumnEdit = ritem;
                RobotGridView.Columns["DGV_MiR_Status_Door_T"].ColumnEdit = te;

                //열을 오름차순으로 정렬합니다.
                RobotGridView.Columns["DGV_MiR_Status_Robot_Name_orderby"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

                //선택취소
                RobotGridView.ClearSelection();


            }
            catch (Exception ex)
            {
                mainForm.EventLog("AutoScreen", "subFunc_MiR_Status_Display() Fail = " + ex);
                mainForm.ACS_UI_Log("AutoScreen" + "/" + "subFunc_MiR_Status_Display() Fail = " + ex);
            }
        }

        #region Map 관련

        private void Map_Load()
        {
            //선택한 모니터가 있는지 확인
            if (ConfigData.DisplayMapNames is Dictionary<string, string>)
            {
                //선택한 모니터랑 이전 선택한 모니터 비교
                if (ConfigData.DisplayMapNames.Except(Sub_DisplayMapNames).Any() || ConfigData.DisplayMapNames.Count != Sub_DisplayMapNames.Count)
                {
                    //값 복사
                    Sub_DisplayMapNames.Clear();

                    foreach (KeyValuePair<string, string> pair in ConfigData.DisplayMapNames)
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
        private void DrawMap()
        {
            //Map Task 끄기
            MapInit();
            //Map 그리기
            int Datacount = 1;
            foreach (var item in ConfigData.DisplayMapNames)
            {
                var FloorMapdata = uow.FloorMapIDConfigs.Find(f => f.FloorIndex == item.Value).FirstOrDefault();
                if (FloorMapdata != null)
                {
                    if (FloorMapdata.MapData.mapScale == 0)
                    {
                        if (FloorMapdata.FloorName == "M3F")
                        {
                            FloorMapdata.MapData.mapScale = 0.13333334f;
                            FloorMapdata.MapData.mouseFirstLocation = new Point(263, 266);
                            FloorMapdata.MapData.mouseMoveOffset = new Point(-150, 64);
                        }
                        else if (FloorMapdata.FloorName == "T3F")
                        {
                            FloorMapdata.MapData.mapScale = 0.2166666f;
                            FloorMapdata.MapData.mouseFirstLocation = new Point(146, 279);
                            FloorMapdata.MapData.mouseMoveOffset = new Point(-96, 113);
                        }
                        else
                        {
                            FloorMapdata.MapData.mapScale = 0.600f;
                            FloorMapdata.MapData.mouseFirstLocation = new Point(559, 420);
                            FloorMapdata.MapData.mouseMoveOffset = new Point(-0, 50);
                        }
                    }
                    if (Datacount == 1)
                    {
                        mainForm.DbDrawUCMApView(ucMapView1, FloorMapdata, null, true);
                        FloorMapdata.MapData.MapViewName = "ucMapView1";
                    }
                    else if (Datacount == 2)
                    {
                        mainForm.DbDrawUCMApView(ucMapView2, FloorMapdata, null, true);
                        FloorMapdata.MapData.MapViewName = "ucMapView2";
                    }
                    else if (Datacount == 3)
                    {
                        mainForm.DbDrawUCMApView(ucMapView3, FloorMapdata, null, true);
                        FloorMapdata.MapData.MapViewName = "ucMapView3";
                    }
                    else if (Datacount == 4)
                    {
                        mainForm.DbDrawUCMApView(ucMapView4, FloorMapdata, null, true);
                        FloorMapdata.MapData.MapViewName = "ucMapView4";
                    }
                    else if (Datacount == 5)
                    {
                        mainForm.DbDrawUCMApView(ucMapView5, FloorMapdata, null, true);
                        FloorMapdata.MapData.MapViewName = "ucMapView5";
                    }
                    else if (Datacount == 6)
                    {
                        mainForm.DbDrawUCMApView(ucMapView6, FloorMapdata, null, true);
                        FloorMapdata.MapData.MapViewName = "ucMapView6";
                    }
                }
                Datacount++;
            }
        }

        private void MapInit()
        {
            #region UCMAPView 전체 초기화

            ucMapView1.ReStartResetData();
            ucMapView2.ReStartResetData();
            ucMapView3.ReStartResetData();
            ucMapView4.ReStartResetData();
            ucMapView5.ReStartResetData();
            ucMapView6.ReStartResetData();

            #endregion
        }
        private void TableLayoutPanelRowColumn()
        {
            //GridControl 크기 조절
            splitContainerControl1.SplitterPosition = ((this.Height / 3) * 2);

            if (ConfigData.DisplayMapNames.Count == 0)
            {
                #region User Map Select Count == 0  MapViewPanel 크기조절
                //splitContainer2.IsSplitterFixed 을 true로 설정하면 사용자가 분할 바를 이동할 수 없게 되어, 두 패널의 크기가 고정됩니다.
                splitContainer2.IsSplitterFixed = false;
                //splitContainer2.SplitterPosition 속성은 SplitContainer의 분할 바 위치를 설정합니다
                splitContainer2.SplitterPosition = splitContainer2.Height;
                //splitContainer2.PanelVisibility 속성을 사용하여 하나의 패널만 표시하거나 두 패널 모두 표시할 수 있습니다.
                splitContainer2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                splitContainer3.IsSplitterFixed = false;
                splitContainer3.SplitterPosition = splitContainer3.Height;
                splitContainer3.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                //Horizontal 속성은 해당 패널이나 요소가 수평으로 배치되도록 설정하는 것일 수 있습니다.
                SP_Top.Horizontal = false;
                //SP_Top.IsSplitterFixed 을 false로 설정하면 사용자가 분할 바를 드래그하여 두 패널의 크기를 조정할 수 있습니다.
                SP_Top.IsSplitterFixed = false;
                SP_Top.SplitterPosition = SP_Top.Height;
                SP_Top.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;

                SP_Top.Visible = false;
                SP_Top.Enabled = false;
                SP_Middle.Visible = false;
                SP_Middle.Enabled = false;
                SP_Bottom.Visible = false;
                SP_Bottom.Enabled = false;

                #endregion
            }
            else if (ConfigData.DisplayMapNames.Count == 1)
            {
                #region User Map Select Count == 1  MapViewPanel 크기조절

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

                #endregion
            }
            else if (ConfigData.DisplayMapNames.Count == 2)
            {
                #region User Map Select Count == 2  MapViewPanel 크기조절

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

                #endregion
            }
            else if (ConfigData.DisplayMapNames.Count == 3)
            {
                #region User Map Select Count == 3  MapViewPanel 크기조절

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

                #endregion
            }
            else if (ConfigData.DisplayMapNames.Count == 4)
            {
                #region User Map Select Count == 4  MapViewPanel 크기조절

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

                #endregion
            }
            else if (ConfigData.DisplayMapNames.Count == 5)
            {
                #region User Map Select Count == 5  MapViewPanel 크기조절

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

                #endregion
            }
            else if (ConfigData.DisplayMapNames.Count <= 6)
            {
                #region User Map Select Count == 6  MapViewPanel 크기조절

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

                #endregion
            }
        }

        #endregion

        private void AutoDisplay_Timer_Tick(object sender, EventArgs e)
        {
            AutoDisplay_Timer.Enabled = false;
            RobotStatusDisplay();
            Map_Load();
            AutoDisplay_Timer.Interval = 1000; // 타이머 인터벌 1초로 설정!
            AutoDisplay_Timer.Enabled = true;
        }

        #region Main GridControl

        private bool ChangeColor = true;

        /// <summary>
        /// Cell 색상변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;

            //DevExpress의 GridControl에서 특정 행과 셀의 표시 텍스트를 가져오는 데 사용됩니다. 이 메서드는 주로 데이터의 시각적 표현을 사용자 정의할 때 유용합니다.
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
            //UI 요소에서 텍스트의 수평 정렬을 중앙으로 설정하는 코드입니다. 주로 그리드의 셀, 레이아웃, 또는 기타 컨트롤의 표시 스타일을 조정할 때 사용됩니다.
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //UI 요소에서 텍스트의 수직 정렬을 중앙으로 설정하는 코드입니다. 주로 그리드의 셀 또는 다른 컨트롤의 표시 스타일을 조정할 때 사용됩니다.
            e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            //UI 요소에서 셀의 배경색을 사용자 정의할 때 사용하는 속성입니다. 이 속성을 true로 설정하면 지정한 배경색이 적용되며, 기본 배경색 대신 사용자 정의 색상이 사용됩니다.
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
            // DevExpress의 GridControl에서 특정 셀의 편집 컨트롤을 사용자 정의할 수 있는 이벤트입니다.
            // 이 이벤트를 사용하면 특정 조건에 따라 셀의 편집 유형(예: 텍스트 박스, 콤보 박스 등)을 변경할 수 있습니다.

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

            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            #region 선택된 Robot 위치 Zoom

            if (info.InRow || info.InRowCell)
            {
                string RobotAlias = RobotGridView.GetDataRow(info.RowHandle)["DGV_MiR_Status_Robot_Alias"].ToString();

                var RobotData = uow.Robots.GetAll().FirstOrDefault(x => x.RobotAlias == RobotAlias);

                if (RobotData != null)
                {
                    var RobotZoomMapDate = uow.FloorMapIDConfigs.Find(x => x.MapID == RobotData.MapID).FirstOrDefault();
                    if (RobotZoomMapDate != null)
                    {
                        UCMapView uCMapView = null;
                        if (RobotZoomMapDate.MapData.MapViewName == "ucMapView1") uCMapView = ucMapView1;
                        else if (RobotZoomMapDate.MapData.MapViewName == "ucMapView2") uCMapView = ucMapView2;
                        else if (RobotZoomMapDate.MapData.MapViewName == "ucMapView3") uCMapView = ucMapView3;
                        else if (RobotZoomMapDate.MapData.MapViewName == "ucMapView4") uCMapView = ucMapView4;
                        else if (RobotZoomMapDate.MapData.MapViewName == "ucMapView5") uCMapView = ucMapView5;
                        else if (RobotZoomMapDate.MapData.MapViewName == "ucMapView6") uCMapView = ucMapView6;

                        if (uCMapView != null)
                        {
                            uCMapView.StopLoop();
                            //T3F
                            //Test 수정진행중 [김익교]
                            //value.Data.mouseMoveOffset = new Point(-1375, -106);
                            //value.Data.mouseFirstLocation = new Point(346, 176);
                            if (RobotZoomMapDate.MapID == RobotData.MapID && RobotZoomMapDate.FloorName == "B1")
                            {
                                //OFFSet =0 , =0 
                                //robot좌표 31.4 /148.8
                                //X는 31.4보다 작으면 +
                                //X는 31.4보다 크면 -
                                //Y는 148.8보다 작으면 -
                                //Y는 148.8보다 크면 +
                                int MapSizeWidth = (820 - uCMapView.ClientSize.Width) / 2;
                                int MapSizeHeight = (311 - uCMapView.ClientSize.Height) / 2;

                                double OffSetRobotPOSX = (RobotData.Position_X - 31.4) * -1;
                                double OffsetRobotPOSY = (148.8 - RobotData.Position_Y) * -1;
                                int OffSetx = (int)Math.Round(OffSetRobotPOSX * 9.2) - MapSizeWidth;
                                int Offsety = (int)Math.Round(OffsetRobotPOSY * 10.2) - MapSizeHeight;
                                RobotZoomMapDate.MapData.mapScale = (float)0.5;
                                RobotZoomMapDate.MapData.mouseMoveOffset = new Point(OffSetx, Offsety);
                            }

                            else if (RobotZoomMapDate.MapID == RobotData.MapID && RobotZoomMapDate.FloorName == "M3F") // M3F
                            {
                                //OFFSet =0 , =0 
                                //robot좌표 29.650 /124.600
                                //X는 29.6보다 작으면 +
                                //X는 29.6보다 크면 -
                                //Y는 124.6보다 작으면 -
                                //Y는 124.6보다 크면 +
                                int MapSizeWidth = (820 - uCMapView.ClientSize.Width) / 2;
                                int MapSizeHeight = (311 - uCMapView.ClientSize.Height) / 2;

                                double OffSetRobotPOSX = (RobotData.Position_X - 30) * -1;
                                double OffsetRobotPOSY = (124.6 - RobotData.Position_Y) * -1;
                                int OffSetx = (int)Math.Round(OffSetRobotPOSX * 9.2) - MapSizeWidth;
                                int Offsety = (int)Math.Round(OffsetRobotPOSY * 10.2) - MapSizeHeight;
                                RobotZoomMapDate.MapData.mapScale = (float)0.5;
                                RobotZoomMapDate.MapData.mouseMoveOffset = new Point(OffSetx, Offsety);
                            }

                            else if (RobotZoomMapDate.MapID == RobotData.MapID && RobotZoomMapDate.FloorName == "T4F") // T4F
                            {
                                //OFFSet =0 , =0 
                                //robot좌표 29.800 /50.300
                                //X는 29.8보다 작으면 +
                                //X는 29.8보다 크면 -
                                //Y는 60.3보다 작으면 -
                                //Y는 60.3보다 크면 +
                                int MapSizeWidth = (820 - uCMapView.ClientSize.Width) / 2;
                                int MapSizeHeight = (311 - uCMapView.ClientSize.Height) / 2;
                                double OffSetRobotPOSX = (RobotData.Position_X - 29.8) * -1;
                                double OffsetRobotPOSY = (60.3 - RobotData.Position_Y) * -1;
                                int OffSetx = (int)Math.Round(OffSetRobotPOSX * 9.2) - MapSizeWidth;
                                int Offsety = (int)Math.Round(OffsetRobotPOSY * 10.2) - MapSizeHeight;
                                RobotZoomMapDate.MapData.mapScale = (float)0.8;
                                RobotZoomMapDate.MapData.mouseMoveOffset = new Point(OffSetx, Offsety);
                            }

                            mainForm.DbDrawUCMApView(uCMapView, RobotZoomMapDate, null, false);

                        }
                    }
                    else
                        MessageBox.Show("로봇을 찾을 수 없습니다.");
                }
            }
            #endregion
        }
        #endregion
    }

}