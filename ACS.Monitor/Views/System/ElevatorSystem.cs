﻿using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using log4net;
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
    public partial class ElevatorSystem : Form
    {

        private readonly static ILog EventLogger = LogManager.GetLogger("Event"); //Function 실행관련 Log
        private readonly static ILog UserLogger = LogManager.GetLogger("User"); //버튼 및 화면조작관련 Log
        private readonly MainForm mainForm;
        private readonly IUnitOfWork uow;
        private List<ElevatorInfoModel> elevatorInfos = new List<ElevatorInfoModel>();
        private DataTable ElevatorDataTable = new DataTable();

        AlertControl control = new AlertControl();
        public ElevatorSystem(MainForm mainForm, IUnitOfWork uow)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            this.uow = uow;

            Init();
            GridViewColumnsCreate();
            DataTableColumnsCreate();
            GridViewInit();
        }

        private void Init()
        {
            #region ElevatorSystemForm

            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.None; // 테두리 제거
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.WindowState = FormWindowState.Normal; // 전체 화면 설정
            this.Text = "Elevator";

            #endregion

            #region Button
            btn_ElvAuto.Text = "엘리베이터" + "\r\n" + "AGV Auto";
            btn_ElvManual.Text = "엘리베이터" + "\r\n" + "AGV Manual";
            btn_Close.Text = "Close";
            #endregion

            #region AlertControl Event
            control.FormLoad += Control_FormLoad;
            #endregion


        }
        private void GridViewInit()
        {
            #region GridView 초기 Setting

            //Main GridControl 설정 값들
            ElevatorGridView.RowHeight = 30;
            //높이조절 활성화
            ElevatorGridView.OptionsView.RowAutoHeight = true;
            //인디케이터 표시: gridView1.OptionsView.ShowIndicator를 true로 설정하면 각 행의 왼쪽에 인덱스 번호가 표시됩니다.
            ElevatorGridView.OptionsView.ShowIndicator = false;
            //그룹 패널 표시: gridView1.OptionsView.ShowGroupPanel을 true로 설정하면 그룹 패널이 표시됩니다. 사용자는 이 패널을 드래그하여 열을 그룹화할 수 있습니다.
            ElevatorGridView.OptionsView.ShowGroupPanel = false;
            //편집 가능: gridView1.OptionsBehavior.Editable을 true로 설정하면 사용자가 그리드의 셀을 클릭하여 데이터를 수정할 수 있습니다.
            ElevatorGridView.OptionsBehavior.Editable = false;
            //수평선 표시: gridView1.OptionsView.ShowHorizontalLines를 DefaultBoolean.True로 설정하면 수평선이 표시됩니다.
            ElevatorGridView.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            //수직선 표시: gridView1.OptionsView.ShowVerticalLines를 DefaultBoolean.True로 설정하면 수직선이 표시됩니다.
            ElevatorGridView.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            //자동 높이 조정 활성화: gridView1.OptionsView.ColumnHeaderAutoHeight를 DefaultBoolean.True로 설정합니다.
            ElevatorGridView.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
            //테두리 없음 설정: gridView1.BorderStyle를 DevExpress.XtraEditors.Controls.BorderStyles.NoBorder로 설정합니다.
            ElevatorGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //포커스 사각형 비활성화: gridView1.FocusRectStyle를 DrawFocusRectStyle.None으로 설정합니다.
            ElevatorGridView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            // HeaderPanel의 필터 기능 비활성화
            ElevatorGridView.OptionsCustomization.AllowFilter = false;
            //헤더 텍스트 중앙 정렬: gridView1.Appearance.HeaderPanel.TextOptions.HAlignment를 HorzAlignment.Center로 설정합니다.
            ElevatorGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //헤더 패널 폰트 설정: gridView1.Appearance.HeaderPanel.Font에 새로운 Font 객체를 할당합니다.
            ElevatorGridView.Appearance.HeaderPanel.Font = new Font("Arial", 8, FontStyle.Bold);
            //행 폰트 설정: gridView1.Appearance.Row.Font에 새로운 Font 객체를 할당합니다.
            ElevatorGridView.Appearance.Row.Font = new Font("Arial", 8, FontStyle.Bold);
            //다중 선택 활성화: gridView1.OptionsSelection.MultiSelect를 true로 설정합니다.
            ElevatorGridView.OptionsSelection.MultiSelect = false;
            //다중 선택 모드 설정: gridView1.OptionsSelection.MultiSelectMode를 GridMultiSelectMode.RowSelect로 설정합니다.
            ElevatorGridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            // 검색 패널 표시하기
            //gridView1.ShowFindPanel();
            // 검색 패널 숨기기
            ElevatorGridView.HideFindPanel();
            // 편집 가능 설정
            ElevatorGridView.OptionsBehavior.Editable = false;
            // DevExpress GridView에서 행 번호(인덱스)를 표시할지 여부를 제어하는 속성입니다. 이 속성을 사용하면 각 행의 왼쪽에 인덱스 번호를 표시할 수 있습니다.
            ElevatorGridView.OptionsView.ShowIndicator = false;
            //DevExpress GridView에서 그룹 패널의 표시 여부를 제어합니다. 그룹 패널은 사용자가 그리드에서 컬럼을 드래그하여 데이터를 그룹화할 수 있는 영역입니다.
            ElevatorGridView.OptionsView.ShowGroupPanel = false;
            //읽기전용
            //CallGridView.OptionsBehavior.ReadOnly = true;
            ElevatorGridView.BestFitColumns();
            #endregion
        }

        private void GridViewColumnsCreate()
        {
            #region GridView Columns 생성

            ElevatorGridView.Columns.Add(new GridColumn() { FieldName = "DGV_No", Caption = "No", Visible = false, UnboundType = UnboundColumnType.String });
            ElevatorGridView.Columns.Add(new GridColumn() { FieldName = "DGV_Location", Caption = "Location", Visible = false, UnboundType = UnboundColumnType.String });
            ElevatorGridView.Columns.Add(new GridColumn() { FieldName = "DGV_ACSMode", Caption = "ACSMode", Visible = false, UnboundType = UnboundColumnType.String });
            ElevatorGridView.Columns.Add(new GridColumn() { FieldName = "DGV_ElevatorMode", Caption = "ElevatorMode", Visible = false, UnboundType = UnboundColumnType.String });
            ElevatorGridView.Columns.Add(new GridColumn() { FieldName = "DGV_ElevatorNumber", Caption = "층수", Visible = true, UnboundType = UnboundColumnType.String });
            ElevatorGridView.Columns.Add(new GridColumn() { FieldName = "DGV_ElevatorStatus", Caption = "상태", Visible = false, UnboundType = UnboundColumnType.String });
            ElevatorGridView.Columns.Add(new GridColumn() { FieldName = "DGV_ElevatorBtnOnOFF", Caption = "OFF/ON", Visible = true, UnboundType = UnboundColumnType.String });
            #endregion

            #region GridView Columns 정렬 설정

            ElevatorGridView.Columns["DGV_No"].AppearanceCell.Options.UseTextOptions = true;
            ElevatorGridView.Columns["DGV_No"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            ElevatorGridView.Columns["DGV_Location"].AppearanceCell.Options.UseTextOptions = true;
            ElevatorGridView.Columns["DGV_Location"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            ElevatorGridView.Columns["DGV_ACSMode"].AppearanceCell.Options.UseTextOptions = true;
            ElevatorGridView.Columns["DGV_ACSMode"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            ElevatorGridView.Columns["DGV_ElevatorMode"].AppearanceCell.Options.UseTextOptions = true;
            ElevatorGridView.Columns["DGV_ElevatorMode"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            ElevatorGridView.Columns["DGV_ElevatorNumber"].AppearanceCell.Options.UseTextOptions = true;
            ElevatorGridView.Columns["DGV_ElevatorNumber"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            ElevatorGridView.Columns["DGV_ElevatorStatus"].AppearanceCell.Options.UseTextOptions = true;
            ElevatorGridView.Columns["DGV_ElevatorStatus"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            ElevatorGridView.Columns["DGV_ElevatorBtnOnOFF"].AppearanceCell.Options.UseTextOptions = true;
            ElevatorGridView.Columns["DGV_ElevatorBtnOnOFF"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            #endregion

            #region GridView Event 설정

            ElevatorGridView.RowCellClick += ElevatorGridView_RowCellClick; ; //CellClick 이벤트
            ElevatorGridView.RowCellStyle += ElevatorGridView_RowCellStyle; ; //CellStyle 변경 이벤트

            #endregion
        }

        private void ElevatorGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void ElevatorGridView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            string Message = null;
            if (e.Column.FieldName == "DGV_ElevatorBtnOnOFF")
            {
                string No = ElevatorGridView.GetRowCellDisplayText(e.RowHandle, "DGV_No");
                string FloorIndex = ElevatorGridView.GetRowCellDisplayText(e.RowHandle, "DGV_ElevatorNumber");
                string TransportMode = ElevatorGridView.GetRowCellDisplayText(e.RowHandle, "DGV_ElevatorBtnOnOFF");

                var ElevatorInfoData = elevatorInfos.FirstOrDefault(v => v.FloorIndex == FloorIndex);
                if (ElevatorInfoData != null)
                {
                    if (TransportMode == "ON")
                    {
                        ElevatorInfoData.TransportMode = "OFF";
                        ElevatorInfoData.UserNumber = ConfigData.UserNumber;
                        Message = $"사번 : {ConfigData.UserNumber} , 사원 : {ConfigData.UserName} ,{FloorIndex} 층 엘리베이터를 OFF 요청 하였습니다";
                    }
                    else
                    {
                        ElevatorInfoData.TransportMode = "ON";
                        ElevatorInfoData.UserNumber = ConfigData.UserNumber;
                        Message = $"사번 : {ConfigData.UserNumber} , 사원 : {ConfigData.UserName} ,{FloorIndex} 층 엘리베이터를 ON 요청 하였습니다";
                    }

                    if(Message!=null)
                    {
                        uow.ElevatorInfos.Update(ElevatorInfoData);
                        UserLogger.Info($"{Message}");
                        AlertInfo info = new AlertInfo($"Elevator({DateTime.Now})", Message);
                        control.Show(this, info);
                    }
                }
                
            }
        }

        private void DataTableColumnsCreate()
        {
            #region DataTable Columns 생성

            ElevatorDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_No", Caption = "No" });
            ElevatorDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_Location", Caption = "Location" });
            ElevatorDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_ACSMode", Caption = "ACSMode" });
            ElevatorDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_ElevatorMode", Caption = "ElevatorMode" });
            ElevatorDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_ElevatorNumber", Caption = "층수" });
            ElevatorDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_ElevatorStatus", Caption = "상태" });
            ElevatorDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_ElevatorBtnOnOFF", Caption = "ON/OFF" });

            #endregion
        }

        private void ElevatorSystemDisplay()
        {
            #region GridView Data 초기화

            ElevatorGridView.SelectAll();
            ElevatorGridView.DeleteSelectedRows();
            ElevatorGridControl.RepositoryItems.Clear();
            ElevatorDataTable.Clear();

            #endregion

            #region GridView Data Binding
            
            ElevatorGridControl.DataSource = ElevatorSystemData();

            #endregion

            //바인딩후 처음 셀 선택 해제
            ElevatorGridView.ClearSelection();

            #region Button 생성
            RepositoryItemToggleSwitch ritem = new RepositoryItemToggleSwitch();
            ritem.ValueOn = "ON";
            ritem.ValueOff = "OFF";
            ElevatorGridControl.RepositoryItems.Add(ritem);
            ElevatorGridView.Columns["DGV_ElevatorBtnOnOFF"].ColumnEdit = ritem;
            #endregion

        }
        private DataTable ElevatorSystemData()
        {
            int GridCount = 0;

            foreach (var elevatorInfo in elevatorInfos)
            {
                DataRow row = ElevatorDataTable.NewRow();
                row["DGV_No"] = elevatorInfo.Id;
                row["DGV_Location"] = elevatorInfo.Location;
                row["DGV_ACSMode"] = elevatorInfo.ACSMode;
                row["DGV_ElevatorMode"] = elevatorInfo.ElevatorMode;
                row["DGV_ElevatorNumber"] = elevatorInfo.FloorIndex;
                row["DGV_ElevatorBtnOnOFF"] = elevatorInfo.TransportMode;
                ElevatorDataTable.Rows.InsertAt(row, GridCount);
                GridCount++;
            }
            return ElevatorDataTable;

        }

        private void btn_Click(object sender, EventArgs e)
        {
            string Message = null;
            string btnName = ((SimpleButton)sender).Name;
            var GetElevator = elevatorInfos.FirstOrDefault(r => r.Location == "Elevator1");
            if (GetElevator != null)
            {
                switch (btnName)
                {
                    case "btn_ElvAuto":
                        GetElevator.ACSMode = "MiRControlMode";
                        GetElevator.UserNumber = ConfigData.UserNumber;
                        Message = $"사번 : {ConfigData.UserNumber} , 사원 : {ConfigData.UserName}이(가) 전체 엘레베이터를 ON 요청 하였습니다.";
                        break;
                    case "btn_ElvManual":
                        GetElevator.ACSMode = "MiRUnControlMode";
                        GetElevator.UserNumber = ConfigData.UserNumber;
                        Message = $"사번 : {ConfigData.UserNumber} , 사원 : {ConfigData.UserName}이(가) 전체 엘레베이터를 OFF 요청 하였습니다.";
                        break;
                    case "btn_Close":
                        mainForm.flyoutPanel1.HidePopup();
                        mainForm.flyoutPanelControl1.Controls.Clear();
                        Close();
                        break;
                }
                if (Message != null)
                {
                    uow.ElevatorInfos.ACSModeUpdate(GetElevator);
                    UserLogger.Info($"{Message}");
                    AlertInfo info = new AlertInfo($"Elevator({DateTime.Now})", Message);
                    control.Show(this, info);
                }
            }

        }


        private void Control_FormLoad(object sender, AlertFormLoadEventArgs e)
        {
            e.Buttons.PinButton.SetDown(true);
        }



        private void DisplayElevatorMode()
        {
            var elvModeInfo = elevatorInfos.FirstOrDefault(x => x.Location.StartsWith("Elevator"));
            if (elvModeInfo != null)
            {
                if (elvModeInfo.ACSMode == "MiRControlMode")
                {
                    btn_ElvAuto.Appearance.BackColor = Color.Chartreuse;
                    btn_ElvAuto.Appearance.Options.UseBackColor = true;
                    btn_ElvManual.Appearance.BackColor = Color.LightGray;
                    btn_ElvManual.Appearance.Options.UseBackColor = true;
                }
                else if (elvModeInfo.ACSMode == "MiRUnControlMode")
                {
                    btn_ElvAuto.Appearance.BackColor = Color.LightGray;
                    btn_ElvAuto.Appearance.Options.UseBackColor = true;
                    btn_ElvManual.Appearance.BackColor = Color.Chartreuse;
                    btn_ElvManual.Appearance.Options.UseBackColor = true;
                }
            }
        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            DisplayTimer.Enabled = false;
            elevatorInfos = uow.ElevatorInfos.DBGetAll();
            ElevatorSystemDisplay();
            DisplayElevatorMode();
            DisplayTimer.Interval = 1000;
            DisplayTimer.Enabled = true;
        }


    }
}
