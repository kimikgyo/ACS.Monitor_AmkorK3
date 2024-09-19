using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Monitor.Common;
using Monitor.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ACS.Monitor
{
    public partial class CallSystem : Form
    {
        private readonly MainForm mainForm;
        private readonly IUnitOfWork uow;

        private DataTable CallDataTable = new DataTable();
        private List<JobConfigModel> jobConfigs = new List<JobConfigModel>();
        private List<MissionsSpecific> missionsSpecifics = new List<MissionsSpecific>();

        private readonly Font textFont2 = new Font("Arial", 10);

        public CallSystem(MainForm mainForm, IUnitOfWork uow)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            this.uow = uow;

            Init();
            DataTableColumnsCreate();
            GridViewColumnsCreate();
            GridViewInit();
        }

        private void Init()
        {
            #region CallSystemForm

            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.None; // 테두리 제거
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.WindowState = FormWindowState.Normal; // 전체 화면 설정
            this.Text = "Call System";

            #endregion

            #region GroupBox

            groupBox1.Text = "CallSystem";
            groupBox1.Font = textFont2;

            #endregion

            #region Button

            btn_StartZoneSetting.Text = "StartZoneSetting";
            btn_Close.Text = "Close";
            btn_Close.Visible = true;
            #endregion

            #region ComboBox

            // ComboBox Text입력 못하도록 설정
            combobox_StartZone.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            if (ConfigData.StartZone != null) combobox_StartZone.Text = ConfigData.StartZone.ToString();
            
            #endregion
        }

        private void GridViewColumnsCreate()
        {
            #region GridView Columns 생성

            //(필드이름,컬럼이름,컴럼활성화, UnboundType을 사용하여 데이터를 표시하는 방법에 대해 설명하겠습니다.
            //UnboundType은 GridView의 셀에 데이터를 바인딩하지 않고 코드에서 직접 계산하거나 생성한 데이터를 표시할 때 사용됩니다.)

            CallGridView.Columns.Add(new GridColumn() { FieldName = "DGV_RobotAlias", Caption = "로봇명칭", Visible = false, UnboundType = UnboundColumnType.String });
            CallGridView.Columns.Add(new GridColumn() { FieldName = "DGV_RobotName", Caption = "로봇이름", Visible = false, UnboundType = UnboundColumnType.String });
            CallGridView.Columns.Add(new GridColumn() { FieldName = "DGV_Source", Caption = "출발지", Visible = true, UnboundType = UnboundColumnType.String });
            CallGridView.Columns.Add(new GridColumn() { FieldName = "DGV_Dest", Caption = "목적지", Visible = true, UnboundType = UnboundColumnType.String });
            CallGridView.Columns.Add(new GridColumn() { FieldName = "DGV_CallTime", Caption = "JOb 전송시간", Visible = false, UnboundType = UnboundColumnType.String });
            CallGridView.Columns.Add(new GridColumn() { FieldName = "DGV_CallState", Caption = "JOB상태", Visible = false, UnboundType = UnboundColumnType.String });
            CallGridView.Columns.Add(new GridColumn() { FieldName = "DGV_Priority", Caption = "우선순위", Visible = false, UnboundType = UnboundColumnType.String });
            CallGridView.Columns.Add(new GridColumn() { FieldName = "DGV_CallFlag", Caption = "호출", Visible = true, UnboundType = UnboundColumnType.String });
            CallGridView.Columns.Add(new GridColumn() { FieldName = "DGV_CancelFlag", Caption = "취소", Visible = true, UnboundType = UnboundColumnType.String });

            #endregion

            #region GridView Columns 정렬 설정

            CallGridView.Columns["DGV_RobotAlias"].AppearanceCell.Options.UseTextOptions = true;
            CallGridView.Columns["DGV_RobotAlias"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            CallGridView.Columns["DGV_RobotName"].AppearanceCell.Options.UseTextOptions = true;
            CallGridView.Columns["DGV_RobotName"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            CallGridView.Columns["DGV_Source"].AppearanceCell.Options.UseTextOptions = true;
            CallGridView.Columns["DGV_Source"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            CallGridView.Columns["DGV_Dest"].AppearanceCell.Options.UseTextOptions = true;
            CallGridView.Columns["DGV_Dest"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            CallGridView.Columns["DGV_CallTime"].AppearanceCell.Options.UseTextOptions = true;
            CallGridView.Columns["DGV_CallTime"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            CallGridView.Columns["DGV_CallState"].AppearanceCell.Options.UseTextOptions = true;
            CallGridView.Columns["DGV_CallState"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            CallGridView.Columns["DGV_Priority"].AppearanceCell.Options.UseTextOptions = true;
            CallGridView.Columns["DGV_Priority"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            CallGridView.Columns["DGV_CallFlag"].AppearanceCell.Options.UseTextOptions = true;
            CallGridView.Columns["DGV_CallFlag"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            CallGridView.Columns["DGV_CancelFlag"].AppearanceCell.Options.UseTextOptions = true;
            CallGridView.Columns["DGV_CancelFlag"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            #endregion

            #region GridView Event 설정

            CallGridView.RowCellClick += CallGridView_RowCellClick; //CellClick 이벤트
            CallGridView.RowCellStyle += CallGridView_RowCellStyle; //CellStyle 변경 이벤트

            #endregion
        }



        private void DataTableColumnsCreate()
        {
            #region DataTable Columns 생성
            CallDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_RobotAlias", Caption = "로봇명칭" });
            CallDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_RobotName", Caption = "로봇이름" });
            CallDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_Source", Caption = "출발지" });
            CallDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_Dest", Caption = "목적지" });
            CallDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_CallTime", Caption = "JOb 전송시간" });
            CallDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_CallState", Caption = "JOB상태" });
            CallDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_Priority", Caption = "우선순위" });
            #endregion
        }

        private void GridViewInit()
        {
            #region GridView 초기 Setting

            //Main GridControl 설정 값들
            CallGridView.RowHeight = 30;
            //높이조절 활성화
            CallGridView.OptionsView.RowAutoHeight = true;
            //인디케이터 표시: gridView1.OptionsView.ShowIndicator를 true로 설정하면 각 행의 왼쪽에 인덱스 번호가 표시됩니다.
            CallGridView.OptionsView.ShowIndicator = false;
            //그룹 패널 표시: gridView1.OptionsView.ShowGroupPanel을 true로 설정하면 그룹 패널이 표시됩니다. 사용자는 이 패널을 드래그하여 열을 그룹화할 수 있습니다.
            CallGridView.OptionsView.ShowGroupPanel = false;
            //편집 가능: gridView1.OptionsBehavior.Editable을 true로 설정하면 사용자가 그리드의 셀을 클릭하여 데이터를 수정할 수 있습니다.
            CallGridView.OptionsBehavior.Editable = false;
            //수평선 표시: gridView1.OptionsView.ShowHorizontalLines를 DefaultBoolean.True로 설정하면 수평선이 표시됩니다.
            CallGridView.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            //수직선 표시: gridView1.OptionsView.ShowVerticalLines를 DefaultBoolean.True로 설정하면 수직선이 표시됩니다.
            CallGridView.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            //자동 높이 조정 활성화: gridView1.OptionsView.ColumnHeaderAutoHeight를 DefaultBoolean.True로 설정합니다.
            CallGridView.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
            //테두리 없음 설정: gridView1.BorderStyle를 DevExpress.XtraEditors.Controls.BorderStyles.NoBorder로 설정합니다.
            CallGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //포커스 사각형 비활성화: gridView1.FocusRectStyle를 DrawFocusRectStyle.None으로 설정합니다.
            CallGridView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            // HeaderPanel의 필터 기능 비활성화
            CallGridView.OptionsCustomization.AllowFilter = false;
            //헤더 텍스트 중앙 정렬: gridView1.Appearance.HeaderPanel.TextOptions.HAlignment를 HorzAlignment.Center로 설정합니다.
            CallGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //헤더 패널 폰트 설정: gridView1.Appearance.HeaderPanel.Font에 새로운 Font 객체를 할당합니다.
            CallGridView.Appearance.HeaderPanel.Font = new Font("Arial", 8, FontStyle.Bold);
            //행 폰트 설정: gridView1.Appearance.Row.Font에 새로운 Font 객체를 할당합니다.
            CallGridView.Appearance.Row.Font = new Font("Arial", 8, FontStyle.Bold);
            //다중 선택 활성화: gridView1.OptionsSelection.MultiSelect를 true로 설정합니다.
            CallGridView.OptionsSelection.MultiSelect = false;
            //다중 선택 모드 설정: gridView1.OptionsSelection.MultiSelectMode를 GridMultiSelectMode.RowSelect로 설정합니다.
            CallGridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            // 검색 패널 표시하기
            //gridView1.ShowFindPanel();
            // 검색 패널 숨기기
            CallGridView.HideFindPanel();
            // 편집 가능 설정
            CallGridView.OptionsBehavior.Editable = false;
            // DevExpress GridView에서 행 번호(인덱스)를 표시할지 여부를 제어하는 속성입니다. 이 속성을 사용하면 각 행의 왼쪽에 인덱스 번호를 표시할 수 있습니다.
            CallGridView.OptionsView.ShowIndicator = false;
            //DevExpress GridView에서 그룹 패널의 표시 여부를 제어합니다. 그룹 패널은 사용자가 그리드에서 컬럼을 드래그하여 데이터를 그룹화할 수 있는 영역입니다.
            CallGridView.OptionsView.ShowGroupPanel = false;
            //읽기전용
            //CallGridView.OptionsBehavior.ReadOnly = true;
            CallGridView.BestFitColumns();
            #endregion
        }

        private void combobox_StartZone_Click(object sender, EventArgs e)
        {
            #region Combobox Start Zone 생성

            combobox_StartZone.Properties.Items.Clear();
            foreach (var PositionArea in uow.PositionAreaConfigs.DBGetAll())
            {
                var StartPosition = jobConfigs.FirstOrDefault(x => x.CallName.StartsWith(PositionArea.PositionAreaName));
                if (StartPosition != null) combobox_StartZone.Properties.Items.Add(PositionArea.PositionAreaName);
            }
            //목록들을 강제로 열리게 한다.
            combobox_StartZone.ShowPopup();

            #endregion
        }
        private void btn_Click(object sender, EventArgs e)
        {
            string btnName = ((SimpleButton)sender).Name;
            switch (btnName)
            {
                case "btn_StartZoneSetting":
                    #region Combobox 내용을 전역변수 저장

                    ConfigData.StartZone = combobox_StartZone.Text;

                    #endregion
                    break;
                case "btn_Close":
                    // DevExpress의 GridView에서 팝업을 숨기는 데 사용되는 메서드입니다.
                    // 이 메서드는 일반적으로 특정 이벤트가 발생했을 때 팝업 메뉴나 편집기 등을 숨기기 위해 사용됩니다.
                    mainForm.flyoutPanel2.HidePopup();
                    mainForm.flyoutPanelControl2.Controls.Clear();
                    Close();
                    break;
            }
        }

        private void ChangedStartZoneAlram()
        {
            #region Button 색상변경
            if (combobox_StartZone.Text != ConfigData.StartZone)
            {
                //Button 배경색상변경
                btn_StartZoneSetting.Appearance.BackColor = Color.Red;
                btn_StartZoneSetting.Appearance.Options.UseBackColor = true;
            }
            else
            {
                btn_StartZoneSetting.Appearance.BackColor = Color.White;
                btn_StartZoneSetting.Appearance.Options.UseBackColor = true;

            }
            #endregion
        }


        private DataTable CallSystemData()
        {
            int CallDataTableCount = 0;
            if (ConfigData.StartZone != null)
            {

                foreach (var jobConfig in jobConfigs.Where(j => j.CallName.StartsWith(ConfigData.StartZone)))
                {

                    DataRow row = CallDataTable.NewRow();
                    string[] CallNameSplit = jobConfig.CallName.Split('_');
                    string Source = CallNameSplit[0];//출발지
                    string Dest = CallNameSplit[1]; //목적지

                    row["DGV_Source"] = Source;
                    row["DGV_Dest"] = Dest;
                    var missionsSpecific = missionsSpecifics.FirstOrDefault(m => m.CallName == jobConfig.CallName);
                    if (missionsSpecific != null)
                    {
                        row["DGV_RobotAlias"] = missionsSpecific.RobotAlias;
                        row["DGV_RobotName"] = missionsSpecific.RobotName;
                        row["DGV_CallState"] = missionsSpecific.CallState;
                        row["DGV_CallTime"] = missionsSpecific.CallTime;
                        row["DGV_Priority"] = missionsSpecific.Priority;
                    }
                    else
                    {
                        row["DGV_RobotAlias"] = "";
                        row["DGV_RobotName"] = "";
                        row["DGV_CallState"] = "";
                        row["DGV_CallTime"] = "";
                        row["DGV_Priority"] = "";
                    }

                    CallDataTable.Rows.InsertAt(row, CallDataTableCount);
                    CallDataTableCount++;
                }
                return CallDataTable;
            }
            return CallDataTable;
        }


        private void CallSystemDisplay()
        {
            #region GridView Data 초기화

            CallGridView.SelectAll();
            CallGridView.DeleteSelectedRows();
            CallGridControl.RepositoryItems.Clear();
            CallDataTable.Clear();

            #endregion
    
            #region GridView Data Binding
            CallGridControl.DataSource = CallSystemData();

            //바인딩후 처음 셀 선택 해제
            CallGridView.ClearSelection();
            #endregion

            #region Button 설정

            RepositoryItemButtonEdit CallButton = new RepositoryItemButtonEdit();
            CallButton.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;//버튼스타일
            CallButton.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor; // 텍스트 입력 불가(버튼만 보이도록)
            CallButton.Buttons[0].Caption = "Call"; // 버튼 텍스트 설정
            CallGridControl.RepositoryItems.Add(CallButton);
            CallGridView.Columns["DGV_CallFlag"].ColumnEdit = CallButton;

            RepositoryItemButtonEdit CancelButton = new RepositoryItemButtonEdit();
            CancelButton.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;//버튼스타일
            CancelButton.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor; // 텍스트 입력 불가(버튼만 보이도록)
            CancelButton.Buttons[0].Caption = "Cancel"; // 버튼 텍스트 설정
            CallGridControl.RepositoryItems.Add(CancelButton);
            CallGridView.Columns["DGV_CancelFlag"].ColumnEdit = CancelButton;

            #endregion
        }


        private void CallGridView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            #region Call 신호
            if (e.Column.FieldName == "DGV_CallFlag")
            {
                string startZone = CallGridView.GetRowCellDisplayText(e.RowHandle, "DGV_Source");
                string endZone = CallGridView.GetRowCellDisplayText(e.RowHandle, "DGV_Dest");
                string CallName = $"{startZone}_{endZone}";

                var Getmission_Specific = missionsSpecifics.FirstOrDefault(x => x.CallName == CallName);
                if (Getmission_Specific == null)
                {
                    var missionAdd = new MissionsSpecific
                    {
                        CallName = CallName,
                        CallState = "wait",
                        CallTime = DateTime.Now,

                    };
                    uow.MissionsSpecifics.Add(missionAdd);
                }
                else
                {
                    MessageBox.Show("스케줄러에 등록 되어있습니다.");
                }
            }

            #endregion

            #region Cancel 신호

            else if (e.Column.FieldName == "DGV_CancelFlag")
            {
                string startZone = CallGridView.GetRowCellDisplayText(e.RowHandle, "DGV_Source");
                string endZone = CallGridView.GetRowCellDisplayText(e.RowHandle, "DGV_Dest");
                string CallName = $"{startZone}_{endZone}";

                var Getmission_Specific = missionsSpecifics.FirstOrDefault(x => x.CallName == CallName);
                if(Getmission_Specific !=null)
                {
                    Getmission_Specific.Cancel = "cancel";
                    uow.MissionsSpecifics.Update(Getmission_Specific);
                }
                else
                {
                    MessageBox.Show("스케줄러에 등록 되어있지않습니다.");
                }
            }
            #endregion

        }
        private void CallGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            #region 상태에 따라 색상변경

            string startZone = CallGridView.GetRowCellDisplayText(e.RowHandle, "DGV_Source");
            string endZone = CallGridView.GetRowCellDisplayText(e.RowHandle, "DGV_Dest");
            string CallName = $"{startZone}_{endZone}";

            var Getmission_Specific = missionsSpecifics.FirstOrDefault(x => x.CallName == CallName);
            if (Getmission_Specific == null)
            {
                //기본 상태 색상
                if (e.Column.FieldName == "DGV_CallFlag") e.Appearance.BackColor = Color.LightBlue; // 배경색 설정
                else e.Appearance.BackColor = Color.White;
            }
            else
            {
                //미션이 있을경우
                e.Appearance.BackColor = Color.Red;
            }
            #endregion
        }

     

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            DisplayTimer.Enabled = false;
            jobConfigs = uow.JobConfigs.DBGetAll();
            missionsSpecifics = uow.MissionsSpecifics.DBGetAll();
            ChangedStartZoneAlram();
            CallSystemDisplay();
            DisplayTimer.Interval = 1000;
            DisplayTimer.Enabled = true;
        }

      
    }
}