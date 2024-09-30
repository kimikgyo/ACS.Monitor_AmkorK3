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
    public partial class MapView : Form
    {
 

        private readonly MainForm mainForm;
        private readonly IUnitOfWork uow;
        private readonly Font textFont1 = new Font("고딕", 12);
        private readonly Font textFont2 = new Font("Arial", 10);
        private readonly Font textFont3 = new Font("Arial", 9);
        private readonly Font gridFont1 = new Font("Arial", 10, FontStyle.Bold);
        private readonly Font gridFont2 = new Font("Arial", 9, FontStyle.Bold);

        public Dictionary<string, string> Sub_DisplayMapNames = new Dictionary<string, string>();
       
        private Color backColor = Color.FromArgb(30, 39, 46);

        public MapView(MainForm mainForm, IUnitOfWork uow)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            this.uow = uow;
            FormInit();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.BackColor = backColor;
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

          

        }


        private void FormInit()
        {
            panelControl1.BackColor = backColor;
            panelControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        }
        private void ParentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
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
                var FloorMapdata = ConfigData.FloorMapIdConfigs.FirstOrDefault(f => f.FloorIndex == item.Key);
                if (FloorMapdata != null)
                {
                    if (FloorMapdata.MapData.mapScale == 0)
                    {
                        FloorMapdata.MapData.mapScale = 0.600f;
                        FloorMapdata.MapData.mouseFirstLocation = new Point(559, 420);
                        FloorMapdata.MapData.mouseMoveOffset = new Point(-0, 250);
                    }
                    if (Datacount == 1)
                    {
                        FloorMapdata.MapData.MapViewName = "ucMapView1";
                        mainForm.DbDrawUCMApView(ucMapView1, FloorMapdata, null, true);
                    }
                    else if (Datacount == 2)
                    {
                        FloorMapdata.MapData.MapViewName = "ucMapView2";
                        mainForm.DbDrawUCMApView(ucMapView2, FloorMapdata, null, true);
                    }
                    else if (Datacount == 3)
                    {
                        FloorMapdata.MapData.MapViewName = "ucMapView3";
                        mainForm.DbDrawUCMApView(ucMapView3, FloorMapdata, null, true);
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
            #endregion
        }
        private void TableLayoutPanelRowColumn()
        {

            if (ConfigData.DisplayMapNames.Count == 0)
            {
                #region User Map Select Count == 0  MapViewPanel 크기조절
                splitContainerControl2.Horizontal = true;
                //splitContainer2.IsSplitterFixed 을 true로 설정하면 사용자가 분할 바를 이동할 수 없게 되어, 두 패널의 크기가 고정됩니다.
                splitContainerControl2.IsSplitterFixed = false;
                //splitContainer2.SplitterPosition 속성은 SplitContainer의 분할 바 위치를 설정합니다
                splitContainerControl2.SplitterPosition = splitContainerControl2.Height;
                //splitContainer2.PanelVisibility 속성을 사용하여 하나의 패널만 표시하거나 두 패널 모두 표시할 수 있습니다.
                splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;

                splitContainerControl3.Horizontal = true;
                splitContainerControl3.IsSplitterFixed = false;
                splitContainerControl3.SplitterPosition = splitContainerControl3.Height;
                splitContainerControl3.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                //Horizontal 속성은 해당 패널이나 요소가 수평으로 배치되도록 설정하는 것일 수 있습니다.
                splitContainerControl2.Horizontal = false;
                //SP_Top.IsSplitterFixed 을 false로 설정하면 사용자가 분할 바를 드래그하여 두 패널의 크기를 조정할 수 있습니다.
                splitContainerControl2.IsSplitterFixed = false;
                splitContainerControl2.SplitterPosition = splitContainerControl2.Height;
                splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;

                splitContainerControl2.Visible = false;
                splitContainerControl2.Enabled = false;
                splitContainerControl3.Visible = false;
                splitContainerControl3.Enabled = false;


                #endregion
            }
            else if (ConfigData.DisplayMapNames.Count == 1)
            {
                #region User Map Select Count == 1  MapViewPanel 크기조절
                splitContainerControl2.Horizontal = true;
                splitContainerControl2.IsSplitterFixed = false;
                splitContainerControl2.SplitterPosition = splitContainerControl2.Height;
                splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;

                splitContainerControl3.Horizontal = true;
                splitContainerControl3.IsSplitterFixed = false;
                splitContainerControl3.SplitterPosition = splitContainerControl3.Height;
                splitContainerControl3.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;

                //splitContainerControl2.Horizontal = true;
                //splitContainerControl2.IsSplitterFixed = false;
                //splitContainerControl2.SplitterPosition = splitContainerControl2.Height;
                //splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;

                splitContainerControl2.Visible = true;
                splitContainerControl2.Enabled = true;
                splitContainerControl3.Visible = false;
                splitContainerControl3.Enabled = false;


                #endregion
            }
            else if (ConfigData.DisplayMapNames.Count == 2)
            {
                #region User Map Select Count == 2  MapViewPanel 크기조절

                splitContainerControl3.Horizontal = true;
                splitContainerControl3.IsSplitterFixed = false;
                splitContainerControl3.SplitterPosition = splitContainerControl3.Height;
                splitContainerControl3.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;

                splitContainerControl2.Horizontal = true;
                splitContainerControl2.IsSplitterFixed = false;
                splitContainerControl2.SplitterPosition = splitContainerControl2.Height;
                splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;

                splitContainerControl3.Visible = true;
                splitContainerControl3.Enabled = true;
                splitContainerControl2.Visible = true;
                splitContainerControl2.Enabled = true;

                #endregion
            }
            else if (ConfigData.DisplayMapNames.Count == 3)
            {
                #region User Map Select Count == 3  MapViewPanel 크기조절
                splitContainerControl3.Horizontal = true;
                splitContainerControl3.IsSplitterFixed = false;
                splitContainerControl3.SplitterPosition = splitContainerControl3.Height;
                splitContainerControl3.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;

                splitContainerControl2.IsSplitterFixed = false;
                splitContainerControl2.SplitterPosition = splitContainerControl2.Height;
                splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;

                //splitContainerControl3.Horizontal = true;
                //splitContainerControl3.IsSplitterFixed = false;
                splitContainerControl3.SplitterPosition = splitContainerControl3.Width / 2;
                splitContainerControl3.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;

                splitContainerControl3.Visible = true;
                splitContainerControl3.Enabled = true;
                splitContainerControl2.Visible = true;
                splitContainerControl2.Enabled = true;

                #endregion
            }
        }

        #endregion

        private void AutoDisplay_Timer_Tick(object sender, EventArgs e)
        {
            if (ConfigData.MapViewScreenActive)
            {
                AutoDisplay_Timer.Enabled = false;
                Map_Load();
                AutoDisplay_Timer.Interval = 1000; // 타이머 인터벌 1초로 설정!
                AutoDisplay_Timer.Enabled = true;
            }
        }

    }

}