using ACS.Monitor.Utilities;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Monitor.Common;
using Monitor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACS.Monitor
{
    public partial class RobotScreen : DevExpress.XtraEditors.XtraForm
    {
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

        private Color skinColor = Color.FromArgb(43, 52, 59);
        private Color backColor = Color.FromArgb(30, 39, 46);
        private Color mouseOverColor = Color.FromArgb(45, 65, 77);
        private Color mouseOverTextColor = Color.FromArgb(57, 173, 233);
        private Color nomalTextColor = Color.FromArgb(167, 168, 169);
        private Color buttonClickFlagColor = Color.FromArgb(57, 174, 234);
        private Color GridViewRowOddColor = Color.FromArgb(58, 63, 67);
        private Color GridViewRowEvenColor = Color.FromArgb(27, 37, 45);

        public RobotScreen()
        {
            InitializeComponent();
            GridViewInit();
            GridViewColumnsCreate();
            DataTableColumnsCreate();
            Init();
        }
        protected override void OnLoad(EventArgs e)
        {
            #region Config Robot Name 가져오기

            string RobotName = ConfigurationManager.AppSettings["RobotNames"];
            ConfigData.DisplayRobotNames = Helpers.ConvertStringToDictionary(RobotName) ?? new Dictionary<string, string>();

            #endregion
        }
        private void Init()
        {
            #region Form
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
            this.Text = "Robot";
            #endregion
        }


        private void GridViewInit()
        {
            try
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
                RobotGridView.Appearance.HeaderPanel.Font = new Font("Arial", 8, FontStyle.Bold);
                //행 폰트 설정: gridView1.Appearance.Row.Font에 새로운 Font 객체를 할당합니다.
                RobotGridView.Appearance.Row.Font = new Font("Arial", 8, FontStyle.Bold);
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
            catch (Exception ex)
            {
                Console.WriteLine($"GridViewInit()Error = {ex.Message} / {ex.StackTrace} / {ex.InnerException}");
            }
        }
        private void GridViewColumnsCreate()
        {
            #region GridView Columns 생성

            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_Robot_Alias", Caption = "로봇이름", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_State", Caption = "로봇상태", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_Battery_Percent", Caption = "배터리", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_PositionWaitTime", Caption = "포지션값", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_WaitTime", Caption = "대기시간", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_Product_Detail", Caption = "자재정보", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_Door_D", Caption = "Door", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_Source", Caption = "출발지", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_Dest", Caption = "목적지", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_ProgressBar", Caption = "진행률", Visible = true, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MissionText", Caption = "MissionText", Visible = true, UnboundType = UnboundColumnType.String });

            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_Product", Caption = "자재유무", Visible = false, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_CallName", Caption = "콜이름", Visible = false, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_ElevatorOrder", Caption = "elevator순서", Visible = false, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_Door_T", Caption = "Door1", Visible = false, UnboundType = UnboundColumnType.String });
            RobotGridView.Columns.Add(new GridColumn() { FieldName = "DGV_MiR_Status_Robot_Name_orderby", Caption = "순서", Visible = false, UnboundType = UnboundColumnType.String });

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
            RobotGridView.RowCellStyle += RobotGridView_RowCellStyle; //CellStyle 변경 이벤트
            RobotGridView.DoubleClick += RobotGridView_DoubleClick;     //Double Click 이벤트
            RobotGridView.CustomRowCellEdit += RobotGridView_CustomRowCellEdit; //편집컨트롤 사용하기
            #endregion
        }

        private void RobotGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //UI 요소에서 텍스트의 수평 정렬을 중앙으로 설정하는 코드입니다. 주로 그리드의 셀, 레이아웃, 또는 기타 컨트롤의 표시 스타일을 조정할 때 사용됩니다.
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //UI 요소에서 텍스트의 수직 정렬을 중앙으로 설정하는 코드입니다. 주로 그리드의 셀 또는 다른 컨트롤의 표시 스타일을 조정할 때 사용됩니다.
            e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            //UI 요소에서 셀의 배경색을 사용자 정의할 때 사용하는 속성입니다. 이 속성을 true로 설정하면 지정한 배경색이 적용되며, 기본 배경색 대신 사용자 정의 색상이 사용됩니다.
            e.Appearance.Options.UseBackColor = true;



            if (e.Column == RobotGridView.Columns["DGV_MiR_State"])
            {
                string category = RobotGridView.GetRowCellDisplayText(e.RowHandle, RobotGridView.Columns["DGV_MiR_State"]);

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

        private void RobotGridView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            // DevExpress의 GridControl에서 특정 셀의 편집 컨트롤을 사용자 정의할 수 있는 이벤트입니다.
            // 이 이벤트를 사용하면 특정 조건에 따라 셀의 편집 유형(예: 텍스트 박스, 콤보 박스 등)을 변경할 수 있습니다.

            RepositoryItemProgressBar _prbLess25 = new RepositoryItemProgressBar();
            _prbLess25.StartColor = Color.Red;
            _prbLess25.EndColor = Color.Red;
            _prbLess25.ShowTitle = true;
            _prbLess25.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            _prbLess25.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            _prbLess25.LookAndFeel.UseDefaultLookAndFeel = false;

            RepositoryItemProgressBar _prbLess50 = new RepositoryItemProgressBar();
            _prbLess50.StartColor = Color.Orange;
            _prbLess50.EndColor = Color.Orange;
            _prbLess50.ShowTitle = true;
            _prbLess50.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            _prbLess50.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            _prbLess50.LookAndFeel.UseDefaultLookAndFeel = false;

            RepositoryItemProgressBar _prbLess75 = new RepositoryItemProgressBar();
            _prbLess75.StartColor = Color.YellowGreen;
            _prbLess75.EndColor = Color.YellowGreen;
            _prbLess75.ShowTitle = true;
            _prbLess75.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            _prbLess75.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            _prbLess75.LookAndFeel.UseDefaultLookAndFeel = false;

            RepositoryItemProgressBar _prbLess100 = new RepositoryItemProgressBar();
            _prbLess100.StartColor = Color.LightGreen;
            _prbLess100.EndColor = Color.LightGreen;
            _prbLess100.ShowTitle = true;
            _prbLess100.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            _prbLess100.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            _prbLess100.LookAndFeel.UseDefaultLookAndFeel = false;

            if (e.Column == RobotGridView.Columns["DGV_ProgressBar"])
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

        private void RobotGridView_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            if (info.InRow || info.InRowCell)
            {
                string RobotAlias = RobotGridView.GetDataRow(info.RowHandle)["DGV_MiR_Status_Robot_Alias"].ToString();

                var RobotData = ConfigData.Robots.FirstOrDefault(x => x.RobotAlias == RobotAlias);

                if (RobotData != null)
                {
                    var RobotZoomMapDate = ConfigData.FloorMapIdConfigs.FirstOrDefault(x => x.MapID == RobotData.MapID);
                    if (RobotZoomMapDate != null)
                    {
                        //UCMapView uCMapView = null;
                        //if (RobotZoomMapDate.MapData.MapViewName == "ucMapView1") uCMapView = ucMapView1;
                        //else if (RobotZoomMapDate.MapData.MapViewName == "ucMapView2") uCMapView = ucMapView2;
                        //else if (RobotZoomMapDate.MapData.MapViewName == "ucMapView3") uCMapView = ucMapView3;
                        ////else if (RobotZoomMapDate.MapData.MapViewName == "ucMapView4") uCMapView = ucMapView4;
                        ////else if (RobotZoomMapDate.MapData.MapViewName == "ucMapView5") uCMapView = ucMapView5;
                        ////else if (RobotZoomMapDate.MapData.MapViewName == "ucMapView6") uCMapView = ucMapView6;

                        //if (uCMapView != null)
                        //{
                        //    uCMapView.StopLoop();
                        //    //T3F
                        //    //Test 수정진행중 [김익교]
                        //    //value.Data.mouseMoveOffset = new Point(-1375, -106);
                        //    //value.Data.mouseFirstLocation = new Point(346, 176);
                        //    if (RobotZoomMapDate.MapID == RobotData.MapID && RobotZoomMapDate.FloorIndex == "B1F")
                        //    {
                        //        //OFFSet =0 , =0 
                        //        //robot좌표 31.4 /148.8
                        //        //X는 31.4보다 작으면 +
                        //        //X는 31.4보다 크면 -
                        //        //Y는 148.8보다 작으면 -
                        //        //Y는 148.8보다 크면 +
                        //        int MapSizeWidth = (820 - uCMapView.ClientSize.Width) / 2;
                        //        int MapSizeHeight = (311 - uCMapView.ClientSize.Height) / 2;

                        //        double OffSetRobotPOSX = (RobotData.Position_X - 31.4) * -1;
                        //        double OffsetRobotPOSY = (148.8 - RobotData.Position_Y) * -1;
                        //        int OffSetx = (int)Math.Round(OffSetRobotPOSX * 9.2) - MapSizeWidth;
                        //        int Offsety = (int)Math.Round(OffsetRobotPOSY * 10.2) - MapSizeHeight;
                        //        RobotZoomMapDate.MapData.mapScale = (float)0.5;
                        //        RobotZoomMapDate.MapData.mouseMoveOffset = new Point(OffSetx, Offsety);
                        //    }

                        //    else if (RobotZoomMapDate.MapID == RobotData.MapID && RobotZoomMapDate.FloorIndex == "1F") // M3F
                        //    {
                        //        //OFFSet =0 , =0 
                        //        //robot좌표 29.650 /124.600
                        //        //X는 29.6보다 작으면 +
                        //        //X는 29.6보다 크면 -
                        //        //Y는 124.6보다 작으면 -
                        //        //Y는 124.6보다 크면 +
                        //        int MapSizeWidth = (820 - uCMapView.ClientSize.Width) / 2;
                        //        int MapSizeHeight = (311 - uCMapView.ClientSize.Height) / 2;

                        //        double OffSetRobotPOSX = (RobotData.Position_X - 30) * -1;
                        //        double OffsetRobotPOSY = (124.6 - RobotData.Position_Y) * -1;
                        //        int OffSetx = (int)Math.Round(OffSetRobotPOSX * 9.2) - MapSizeWidth;
                        //        int Offsety = (int)Math.Round(OffsetRobotPOSY * 10.2) - MapSizeHeight;
                        //        RobotZoomMapDate.MapData.mapScale = (float)0.5;
                        //        RobotZoomMapDate.MapData.mouseMoveOffset = new Point(OffSetx, Offsety);
                        //    }

                        //    else if (RobotZoomMapDate.MapID == RobotData.MapID && RobotZoomMapDate.FloorIndex == "2F") // T4F
                        //    {
                        //        //OFFSet =0 , =0 
                        //        //robot좌표 29.800 /50.300
                        //        //X는 29.8보다 작으면 +
                        //        //X는 29.8보다 크면 -
                        //        //Y는 60.3보다 작으면 -
                        //        //Y는 60.3보다 크면 +
                        //        int MapSizeWidth = (820 - uCMapView.ClientSize.Width) / 2;
                        //        int MapSizeHeight = (311 - uCMapView.ClientSize.Height) / 2;
                        //        double OffSetRobotPOSX = (RobotData.Position_X - 29.8) * -1;
                        //        double OffsetRobotPOSY = (60.3 - RobotData.Position_Y) * -1;
                        //        int OffSetx = (int)Math.Round(OffSetRobotPOSX * 9.2) - MapSizeWidth;
                        //        int Offsety = (int)Math.Round(OffsetRobotPOSY * 10.2) - MapSizeHeight;
                        //        RobotZoomMapDate.MapData.mapScale = (float)0.8;
                        //        RobotZoomMapDate.MapData.mouseMoveOffset = new Point(OffSetx, Offsety);
                        //    }

                        //    mainForm.DbDrawUCMApView(uCMapView, RobotZoomMapDate, null, false);

                    }
                }
                else
                    MessageBox.Show("로봇을 찾을 수 없습니다.");
            }
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
            var orderedRobots = ConfigData.Robots.Where(r => r.RobotID > 0 && r.ACSRobotGroup == "AMB" && ConfigData.DisplayRobotNames.ContainsKey(r.RobotName))
                                                   .OrderBy(r => r.RobotAlias).ToList();
            foreach (var robot in orderedRobots)
            {
                int GridCount = 0;

                DataRow row = RobotdataTable.NewRow();

                row["DGV_MiR_Status_Robot_Alias"] = robot.RobotAlias.ToString();
                if (robot.Fleet_State == FleetState.unavailable) row["DGV_MiR_State"] = robot.Fleet_State_Text;
                else row["DGV_MiR_State"] = robot.StateText;
                row["DGV_MiR_Status_Battery_Percent"] = robot.BatteryPercent.ToString("0.00") + "%";

                row["DGV_WaitTime"] = ConfigData.PositionWaitTimes.FirstOrDefault(x => x.RobotName == robot.RobotName && x.RobotAlias == robot.RobotAlias)?.ElapsedTime ?? "";
                row["DGV_MissionText"] = robot.MissionText ?? "";
                //Job 상태
                if (robot.JobId > 0)
                {

                    var RunJobs = ConfigData.Jobs.FirstOrDefault(j => j.Id == robot.JobId);
                    var Missions = ConfigData.Missions.Where(m => m.JobId == robot.JobId);
                    var ExecMission = Missions.FirstOrDefault(e => e.MissionState == "Executing");
                    var ElevatorOrderAll = ConfigData.ElevatorStates.ToList();
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
                //mainForm.EventLog("AutoScreen", "subFunc_MiR_Status_Display() Fail = " + ex);
                //mainForm.ACS_UI_Log("AutoScreen" + "/" + "subFunc_MiR_Status_Display() Fail = " + ex);
            }
        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {

            if (ConfigData.RobotScreenActive)
            {
                DisplayTimer.Enabled = false;
                RobotStatusDisplay();
                DisplayTimer.Interval = 1000; // 타이머 인터벌 1초로 설정!
                DisplayTimer.Enabled = true;
            }
            else
            {
                DisplayTimer.Stop();
                DisplayTimer.Enabled = false;
                DisplayTimer.Dispose();
                this.Close();
                this.Dispose();
            }

           
        }
    }
}