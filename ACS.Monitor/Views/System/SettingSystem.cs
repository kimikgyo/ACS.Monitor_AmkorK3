using ACS.Monitor.Utilities;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Monitor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACS.Monitor
{
    public partial class SettingSystem : DevExpress.XtraEditors.XtraForm
    {
        private DataTable RobotSelectDataTable = new DataTable();
        private DataTable FloorSelectDataTable = new DataTable();

        private Color skinColor = Color.FromArgb(43, 52, 59);
        private Color backColor = Color.FromArgb(30, 39, 46);
        private Color mouseOverColor = Color.FromArgb(45, 65, 77);
        private Color mouseOverTextColor = Color.FromArgb(57, 173, 233);
        private Color nomalTextColor = Color.FromArgb(167, 168, 169);
        private Color buttonClickFlagColor = Color.FromArgb(57, 174, 234);
        private Color GridViewRowOddColor = Color.FromArgb(58, 63, 67);
        private Color GridViewRowEvenColor = Color.FromArgb(27, 37, 45);

        public SettingSystem()
        {
            InitializeComponent();
            Init();
            GridViewInit();
            GridViewColumnsCreate();
            DataTableColumnsCreate();
        }
        private void Init()
        {
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.LookAndFeel.SkinMaskColor = skinColor;
            this.LookAndFeel.SkinMaskColor2 = skinColor;
            this.BackColor = backColor;
            this.ForeColor = Color.White;

            this.FormBorderStyle = FormBorderStyle.None; // 테두리 제거
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.WindowState = FormWindowState.Normal; // 전체 화면 설정
            this.Text = "Setting System";
        }

        private void GridViewInit()
        {
            #region RobotSelect GridView 
            //Main GridControl 설정 값들
            RobotSelectGridView.RowHeight = 30;
            //높이조절 활성화
            RobotSelectGridView.OptionsView.RowAutoHeight = true;
            //인디케이터 표시: gridView1.OptionsView.ShowIndicator를 true로 설정하면 각 행의 왼쪽에 인덱스 번호가 표시됩니다.
            RobotSelectGridView.OptionsView.ShowIndicator = false;
            //그룹 패널 표시: gridView1.OptionsView.ShowGroupPanel을 true로 설정하면 그룹 패널이 표시됩니다. 사용자는 이 패널을 드래그하여 열을 그룹화할 수 있습니다.
            RobotSelectGridView.OptionsView.ShowGroupPanel = false;
            //편집 가능: gridView1.OptionsBehavior.Editable을 true로 설정하면 사용자가 그리드의 셀을 클릭하여 데이터를 수정할 수 있습니다.
            RobotSelectGridView.OptionsBehavior.Editable = false;
            //수평선 표시: gridView1.OptionsView.ShowHorizontalLines를 DefaultBoolean.True로 설정하면 수평선이 표시됩니다.
            RobotSelectGridView.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            //수직선 표시: gridView1.OptionsView.ShowVerticalLines를 DefaultBoolean.True로 설정하면 수직선이 표시됩니다.
            RobotSelectGridView.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            //자동 높이 조정 활성화: gridView1.OptionsView.ColumnHeaderAutoHeight를 DefaultBoolean.True로 설정합니다.
            RobotSelectGridView.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
            //테두리 없음 설정: gridView1.BorderStyle를 DevExpress.XtraEditors.Controls.BorderStyles.NoBorder로 설정합니다.
            RobotSelectGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //포커스 사각형 비활성화: gridView1.FocusRectStyle를 DrawFocusRectStyle.None으로 설정합니다.
            RobotSelectGridView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            // HeaderPanel의 필터 기능 비활성화
            RobotSelectGridView.OptionsCustomization.AllowFilter = false;
            //헤더 텍스트 중앙 정렬: gridView1.Appearance.HeaderPanel.TextOptions.HAlignment를 HorzAlignment.Center로 설정합니다.
            RobotSelectGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //헤더 패널 폰트 설정: gridView1.Appearance.HeaderPanel.Font에 새로운 Font 객체를 할당합니다.
            RobotSelectGridView.Appearance.HeaderPanel.Font = new Font("Arial", 8, FontStyle.Bold);
            //행 폰트 설정: gridView1.Appearance.Row.Font에 새로운 Font 객체를 할당합니다.
            RobotSelectGridView.Appearance.Row.Font = new Font("Arial", 8, FontStyle.Bold);
            //다중 선택 활성화: gridView1.OptionsSelection.MultiSelect를 true로 설정합니다.
            RobotSelectGridView.OptionsSelection.MultiSelect = false;
            //다중 선택 모드 설정: gridView1.OptionsSelection.MultiSelectMode를 GridMultiSelectMode.RowSelect로 설정합니다.
            RobotSelectGridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            // 검색 패널 표시하기
            //gridView1.ShowFindPanel();
            // 검색 패널 숨기기
            RobotSelectGridView.HideFindPanel();
            // 편집 가능 설정
            RobotSelectGridView.OptionsBehavior.Editable = false;
            // DevExpress GridView에서 행 번호(인덱스)를 표시할지 여부를 제어하는 속성입니다. 이 속성을 사용하면 각 행의 왼쪽에 인덱스 번호를 표시할 수 있습니다.
            RobotSelectGridView.OptionsView.ShowIndicator = false;
            //DevExpress GridView에서 그룹 패널의 표시 여부를 제어합니다. 그룹 패널은 사용자가 그리드에서 컬럼을 드래그하여 데이터를 그룹화할 수 있는 영역입니다.
            RobotSelectGridView.OptionsView.ShowGroupPanel = false;
            //읽기전용
            //RobotGridView.OptionsBehavior.ReadOnly = true;

            RobotSelectGridView.BestFitColumns();

            #endregion

            #region FloorSelect GridView
            //Main GridControl 설정 값들
            FloorSelectGridView.RowHeight = 30;
            //높이조절 활성화
            FloorSelectGridView.OptionsView.RowAutoHeight = true;
            //인디케이터 표시: gridView1.OptionsView.ShowIndicator를 true로 설정하면 각 행의 왼쪽에 인덱스 번호가 표시됩니다.
            FloorSelectGridView.OptionsView.ShowIndicator = false;
            //그룹 패널 표시: gridView1.OptionsView.ShowGroupPanel을 true로 설정하면 그룹 패널이 표시됩니다. 사용자는 이 패널을 드래그하여 열을 그룹화할 수 있습니다.
            FloorSelectGridView.OptionsView.ShowGroupPanel = false;
            //편집 가능: gridView1.OptionsBehavior.Editable을 true로 설정하면 사용자가 그리드의 셀을 클릭하여 데이터를 수정할 수 있습니다.
            FloorSelectGridView.OptionsBehavior.Editable = false;
            //수평선 표시: gridView1.OptionsView.ShowHorizontalLines를 DefaultBoolean.True로 설정하면 수평선이 표시됩니다.
            FloorSelectGridView.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            //수직선 표시: gridView1.OptionsView.ShowVerticalLines를 DefaultBoolean.True로 설정하면 수직선이 표시됩니다.
            FloorSelectGridView.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            //자동 높이 조정 활성화: gridView1.OptionsView.ColumnHeaderAutoHeight를 DefaultBoolean.True로 설정합니다.
            FloorSelectGridView.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
            //테두리 없음 설정: gridView1.BorderStyle를 DevExpress.XtraEditors.Controls.BorderStyles.NoBorder로 설정합니다.
            FloorSelectGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //포커스 사각형 비활성화: gridView1.FocusRectStyle를 DrawFocusRectStyle.None으로 설정합니다.
            FloorSelectGridView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            // HeaderPanel의 필터 기능 비활성화
            FloorSelectGridView.OptionsCustomization.AllowFilter = false;
            //헤더 텍스트 중앙 정렬: gridView1.Appearance.HeaderPanel.TextOptions.HAlignment를 HorzAlignment.Center로 설정합니다.
            FloorSelectGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //헤더 패널 폰트 설정: gridView1.Appearance.HeaderPanel.Font에 새로운 Font 객체를 할당합니다.
            FloorSelectGridView.Appearance.HeaderPanel.Font = new Font("Arial", 8, FontStyle.Bold);
            //행 폰트 설정: gridView1.Appearance.Row.Font에 새로운 Font 객체를 할당합니다.
            FloorSelectGridView.Appearance.Row.Font = new Font("Arial", 8, FontStyle.Bold);
            //다중 선택 활성화: gridView1.OptionsSelection.MultiSelect를 true로 설정합니다.
            FloorSelectGridView.OptionsSelection.MultiSelect = false;
            //다중 선택 모드 설정: gridView1.OptionsSelection.MultiSelectMode를 GridMultiSelectMode.RowSelect로 설정합니다.
            FloorSelectGridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            // 검색 패널 표시하기
            //gridView1.ShowFindPanel();
            // 검색 패널 숨기기
            FloorSelectGridView.HideFindPanel();
            // 편집 가능 설정
            FloorSelectGridView.OptionsBehavior.Editable = false;
            // DevExpress GridView에서 행 번호(인덱스)를 표시할지 여부를 제어하는 속성입니다. 이 속성을 사용하면 각 행의 왼쪽에 인덱스 번호를 표시할 수 있습니다.
            FloorSelectGridView.OptionsView.ShowIndicator = false;
            //DevExpress GridView에서 그룹 패널의 표시 여부를 제어합니다. 그룹 패널은 사용자가 그리드에서 컬럼을 드래그하여 데이터를 그룹화할 수 있는 영역입니다.
            FloorSelectGridView.OptionsView.ShowGroupPanel = false;
            //읽기전용
            //RobotGridView.OptionsBehavior.ReadOnly = true;

            FloorSelectGridView.BestFitColumns();

            #endregion
        }
        private void GridViewColumnsCreate()
        {
            #region GridView Columns 생성

            RobotSelectGridView.Columns.Add(new GridColumn() { FieldName = "DGV_RobotSelect_Check", Caption = "체크", Visible = true, UnboundType = UnboundColumnType.Boolean });
            RobotSelectGridView.Columns.Add(new GridColumn() { FieldName = "DGV_RobotSelect_Alias", Caption = "로봇이름", Visible = true, UnboundType = UnboundColumnType.String });
            RobotSelectGridView.Columns.Add(new GridColumn() { FieldName = "DGV_RobotSelect_Name", Caption = "Fleet로봇이름", Visible = true, UnboundType = UnboundColumnType.String });

            FloorSelectGridView.Columns.Add(new GridColumn() { FieldName = "DGV_FloorSelect_Check", Caption = "체크", Visible = true, UnboundType = UnboundColumnType.Boolean });
            FloorSelectGridView.Columns.Add(new GridColumn() { FieldName = "DGV_FloorSelect_Index", Caption = "층수", Visible = true, UnboundType = UnboundColumnType.String });
            FloorSelectGridView.Columns.Add(new GridColumn() { FieldName = "DGV_FloorSelect_Name", Caption = "이름", Visible = true, UnboundType = UnboundColumnType.String });
            #endregion

            #region GridView Columns 정렬 설정

            RobotSelectGridView.Columns["DGV_RobotSelect_Alias"].AppearanceCell.Options.UseTextOptions = true;
            RobotSelectGridView.Columns["DGV_RobotSelect_Alias"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            RobotSelectGridView.Columns["DGV_RobotSelect_Name"].AppearanceCell.Options.UseTextOptions = true;
            RobotSelectGridView.Columns["DGV_RobotSelect_Name"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;


            FloorSelectGridView.Columns["DGV_FloorSelect_Index"].AppearanceCell.Options.UseTextOptions = true;
            FloorSelectGridView.Columns["DGV_FloorSelect_Index"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            FloorSelectGridView.Columns["DGV_FloorSelect_Name"].AppearanceCell.Options.UseTextOptions = true;
            FloorSelectGridView.Columns["DGV_FloorSelect_Name"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            #endregion

            #region GridView Event 설정

            RobotSelectGridView.RowCellClick += RobotSelectGridView_RowCellClick; //CellClick 이벤트
            RobotSelectGridView.RowCellStyle += RobotSelectGridView_RowCellStyle; //CellStyle 변경 이벤트

            FloorSelectGridView.RowCellClick += FloorSelectGridView_RowCellClick; ; //CellClick 이벤트
            FloorSelectGridView.RowCellStyle += FloorSelectGridView_RowCellStyle; ; //CellStyle 변경 이벤트

            #endregion


        }

        private void FloorSelectGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "DGV_FloorSelect_Check")
            {
                // 체크된 상태에 따라 다른 색상 적용
                bool isChecked = (bool)e.CellValue;
                if (isChecked)
                {
                    e.Appearance.BackColor = buttonClickFlagColor; // 체크된 경우 배경색 변경
                }
                else
                {
                    if (e.RowHandle % 2 == 0)
                    {
                        e.Appearance.BackColor = GridViewRowEvenColor;
                    }
                    else
                    {
                        e.Appearance.BackColor = GridViewRowOddColor;
                    }

                }
            }
            else
            {
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = GridViewRowEvenColor;
                }
                else
                {
                    e.Appearance.BackColor = GridViewRowOddColor;
                }
            }
        }

        private void FloorSelectGridView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "DGV_FloorSelect_Check")
            {
                string FloorIndex = FloorSelectGridView.GetRowCellDisplayText(e.RowHandle, "DGV_FloorSelect_Index");
                string FloorName = FloorSelectGridView.GetRowCellDisplayText(e.RowHandle, "DGV_FloorSelect_Name");
                bool isChecked = !(bool)e.CellValue;


                if (isChecked && ConfigData.DisplayMapNames.ContainsKey(FloorIndex) == false) ConfigData.DisplayMapNames.Add(FloorIndex, FloorName);
                else if (!isChecked && ConfigData.DisplayMapNames.ContainsKey(FloorIndex) == true) ConfigData.DisplayMapNames.Remove(FloorIndex);

                string saveDictText = Helpers.ConvertDictionaryToString(ConfigData.DisplayMapNames);
                //ConfigData 수정
                AppConfiguration.SetAppConfig("MapNames", saveDictText);
            }
        }

        private void RobotSelectGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "DGV_RobotSelect_Check")
            {
                // 체크된 상태에 따라 다른 색상 적용
                bool isChecked = (bool)e.CellValue;
                if (isChecked)
                {
                    e.Appearance.BackColor = buttonClickFlagColor; // 체크된 경우 배경색 변경
                }
                else
                {
                    if (e.RowHandle % 2 == 0)
                    {
                        e.Appearance.BackColor = GridViewRowEvenColor;
                    }
                    else
                    {
                        e.Appearance.BackColor = GridViewRowOddColor;
                    }

                }
            }
            else
            {
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = GridViewRowEvenColor;
                }
                else
                {
                    e.Appearance.BackColor = GridViewRowOddColor;
                }
            }
        }

        private void RobotSelectGridView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "DGV_RobotSelect_Check")
            {
                string RobotName = RobotSelectGridView.GetRowCellDisplayText(e.RowHandle, "DGV_RobotSelect_Name");
                string RobotAlias = RobotSelectGridView.GetRowCellDisplayText(e.RowHandle, "DGV_RobotSelect_Alias");
                bool isChecked = !(bool)e.CellValue;


                if (isChecked && ConfigData.DisplayRobotNames.ContainsKey(RobotName) == false) ConfigData.DisplayRobotNames.Add(RobotName, RobotAlias);
                else if (!isChecked && ConfigData.DisplayRobotNames.ContainsKey(RobotName) == true) ConfigData.DisplayRobotNames.Remove(RobotName);

                string saveDictText = Helpers.ConvertDictionaryToString(ConfigData.DisplayRobotNames);
                AppConfiguration.SetAppConfig("RobotNames", saveDictText);
            }
        }


        private void DataTableColumnsCreate()
        {
            #region DataTable Columns 생성

            RobotSelectDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_RobotSelect_Check", Caption = "체크", DataType = typeof(bool) });
            RobotSelectDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_RobotSelect_Alias", Caption = "로봇이름" });
            RobotSelectDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_RobotSelect_Name", Caption = "Fleet로봇이름" });

            FloorSelectDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_FloorSelect_Check", Caption = "체크", DataType = typeof(bool) });
            FloorSelectDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_FloorSelect_Index", Caption = "층수" });
            FloorSelectDataTable.Columns.Add(new DataColumn() { ColumnName = "DGV_FloorSelect_Name", Caption = "이름" });

            #endregion
        }
        private DataTable robotselectData()
        {
            #region GridControl 표기하기 위한 Data // return : RobotData

            RobotSelectDataTable.Rows.Clear();
            var orderedRobots = ConfigData.Robots.Where(r => r.ACSRobotGroup == "AMB").OrderBy(r => r.RobotAlias).ToList();

            foreach (var robot in orderedRobots)
            {
                int GridCount = 0;
                DataRow row = RobotSelectDataTable.NewRow();

                if (ConfigData.DisplayRobotNames.ContainsKey(robot.RobotName) == true) row["DGV_RobotSelect_Check"] = true;
                else row["DGV_RobotSelect_Check"] = false;

                row["DGV_RobotSelect_Alias"] = robot.RobotAlias.ToString();
                row["DGV_RobotSelect_Name"] = robot.RobotName.ToString();

                RobotSelectDataTable.Rows.InsertAt(row, GridCount);
                GridCount++;
            }
            return RobotSelectDataTable;

            #endregion
        }

        private void FloorSelectDisplay()
        {
            #region GridView Data 초기화

            FloorSelectGridView.SelectAll();
            FloorSelectGridView.DeleteSelectedRows();
            FloorSelectGridControl.RepositoryItems.Clear();
            FloorSelectDataTable.Clear();

            #endregion

            //데이터 표시하기
            FloorSelectGridControl.DataSource = FloorselectData();

            RepositoryItemCheckEdit ritem = new RepositoryItemCheckEdit();

            FloorSelectGridControl.RepositoryItems.Add(ritem);

            FloorSelectGridView.Columns["DGV_FloorSelect_Check"].ColumnEdit = ritem;

            //선택취소
            FloorSelectGridView.ClearSelection();
        }
        private DataTable FloorselectData()
        {
            #region GridControl 표기하기 위한 Data // return : RobotData

            FloorSelectDataTable.Rows.Clear();
            var floorMaps = ConfigData.FloorMapIdConfigs.ToList();

            foreach (var floorMap in floorMaps)
            {
                int GridCount = 0;
                DataRow row = FloorSelectDataTable.NewRow();

                if (ConfigData.DisplayMapNames.ContainsKey(floorMap.FloorIndex) == true) row["DGV_FloorSelect_Check"] = true;
                else row["DGV_FloorSelect_Check"] = false;

                row["DGV_FloorSelect_Index"] = floorMap.FloorIndex.ToString();
                row["DGV_FloorSelect_Name"] = floorMap.FloorName.ToString();

                FloorSelectDataTable.Rows.InsertAt(row, GridCount);
                GridCount++;
            }
            return FloorSelectDataTable;

            #endregion
        }

        private void RobotSelectDisplay()
        {
            #region GridView Data 초기화

            RobotSelectGridView.SelectAll();
            RobotSelectGridView.DeleteSelectedRows();
            RobotSelectGridControl.RepositoryItems.Clear();
            RobotSelectDataTable.Clear();

            #endregion

            //데이터 표시하기
            RobotSelectGridControl.DataSource = robotselectData();

            RepositoryItemCheckEdit ritem = new RepositoryItemCheckEdit();

            RobotSelectGridControl.RepositoryItems.Add(ritem);

            RobotSelectGridView.Columns["DGV_RobotSelect_Check"].ColumnEdit = ritem;

            //선택취소
            RobotSelectGridView.ClearSelection();
        }


        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            if (ConfigData.SettingScreenActive)
            {
                DisplayTimer.Enabled = false;
                FloorSelectDisplay();
                RobotSelectDisplay();
                DisplayTimer.Interval = 1000; // 타이머 인터벌 1초로 설정!
                DisplayTimer.Enabled = true;
            }
        }
    }
}