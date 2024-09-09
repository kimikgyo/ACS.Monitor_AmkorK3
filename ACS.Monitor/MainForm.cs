﻿using DevExpress.XtraBars;
using DevExpress.XtraBars.FluentDesignSystem;
using ACS.Monitor.Utilities;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Monitor.Data;
using Monitor.Common;
using Newtonsoft.Json;

namespace ACS.Monitor
{
    public partial class MainForm : FluentDesignForm
    {
        private readonly static ILog EventLogger = LogManager.GetLogger("Event"); //Function 실행관련 Log
        private readonly static ILog UserLogger = LogManager.GetLogger("User"); //버튼 및 화면조작관련 Log
        private readonly static ILog TimeoutLogger = LogManager.GetLogger("Timeout");

        private readonly UnitOfWork uow;
        private AutoScreen ChildMainForm;
        //private JobHistory JobHistory;
        //private WaitPositionTimeHistory WaitPosition;
        //private SettingsElevator elevator;
        //private SettingsCallMissions CallSystem;
        private GetDataControl getDataControl = null;

        private readonly Font textFont1 = new Font("맑은 고딕", 11, FontStyle.Bold);
        private readonly Font AmkorK5Font = new Font("고딕", 12, FontStyle.Bold);
        private readonly Font textFont2 = new Font("Arial", 10, FontStyle.Bold);
        private readonly Font textFont3 = new Font("Arial", 9, FontStyle.Bold);
        private readonly Font gridFont1 = new Font("Arial", 10, FontStyle.Bold);
        private readonly Font gridFont2 = new Font("Arial", 9, FontStyle.Bold);
        private readonly Font textFont4 = new Font("Arial Narrow", 5, FontStyle.Bold);
        private readonly Font bartextFont1 = new Font("Arial Narrow", 15, FontStyle.Bold);


        public MainForm()
        {
            //this.ClientSize = new Size(800, 600);
            // 폼의 배경 색상 설정
            InitializeComponent();

            subFunc_Public_DataInit();


            uow = new UnitOfWork();

            Init();
            //DesignFunc();

            ////부모 Form 으로 설정한다
            //this.IsMdiContainer = true; // MDI 컨테이너로 설정
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Start();
            this.BackColor = Color.White; // 원하는 색상으로 변경 가능
            //SkinManager.EnableFormSkins();
            //SkinManager.EnableMdiFormSkins();
            InitTopBarControl();
            ChildMainFormFunc();

            MapMenuLoad();
            RobotMenuLoad();
        }
        private void Init()
        {
            getDataControl = new GetDataControl(this, uow);
        }
        private void Start()
        {
            getDataControl.Start();
        }
        private void InitTopBarControl()
        {
            barButtonItem1.Size = new Size(100, 30);
            barButtonItem1.ButtonStyle = BarButtonStyle.Check;
            barButtonItem1.Caption = "UserLogin";
            barButtonItem1.ImageOptions.ImageUri.Uri = "bocontact2;Colored";
            barButtonItem1.Alignment = BarItemLinkAlignment.Right;
            barButtonItem1.PaintStyle = BarItemPaintStyle.CaptionGlyph;

            barButtonItem1.Appearance.BackColor = Color.Blue;
            barButtonItem1.Appearance.BackColor2 = Color.Red;
            barButtonItem1.Appearance.ForeColor = Color.Black;
            barButtonItem1.Appearance.Options.UseBackColor = true;
            barButtonItem1.Appearance.Options.UseForeColor = true;
            barButtonItem1.ItemClick += UserLoginButtonClick;

            //Lable
            barStaticItem1.AutoSize = BarStaticItemSize.Spring;
            barStaticItem1.Caption = "";
            barStaticItem1.Alignment = BarItemLinkAlignment.Left;
            barStaticItem1.Appearance.Font = AmkorK5Font;
            barStaticItem1.Appearance.ForeColor = Color.Blue;
        }
        /// <summary>
        ///  UserLogin  / UserLogOut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserLoginButtonClick(object sender, ItemClickEventArgs e)
        {
            //if (e.Item.Caption == "UserLogin")
            //{
            //    //사번 입력 Form을 불러옴
            //    var UserNumberForm = new UserNumberForm();
            //    DialogResult result = UserNumberForm.ShowDialog();
            //    if (result == DialogResult.Yes)
            //    {
            //        //사번 입력하여 검색
            //        var UserNumber = uow.UserNumbers.GetAll().FirstOrDefault(u => u.UserName == UserNumberForm.UserNumber);
            //        //사번이 없는경우 설정창을 Clear한후 다시 초기설정함
            //        if (UserNumber == null) MessageBox.Show("등록되지 않은 사원번호 이거나 비밀번호가 틀립니다!");
            //        else
            //        {
            //            //barStaticItem1.Caption = $"사원번호 = {UserNumber.UserNumber} / 사원이름 = {UserNumber.UserName}";
            //            L_Login.Text = $"사원번호 = {UserNumber.UserNumber} / 사원이름 = {UserNumber.UserName}";
            //            barButtonItem1.Caption = "UserLogOut";
            //            MenuItem_Setting.Visible = true;
            //        }
            //    }
            //}
            //else
            //{
            //    barButtonItem1.Caption = "UserLogin";
            //    barStaticItem1.Caption = "";
            //    MenuItem_Setting.Visible = false;
            //}
        }

        /// <summary>
        /// AutoScreen을 MainForm으로 Dock하는 Func
        /// </summary>
        private void ChildMainFormFunc()
        {
            ChildMainForm = new AutoScreen(this, uow);
            ChildMainForm.TopLevel = false;
            ChildMainForm.Dock = DockStyle.Fill;
            DesignPanelControl.BackColor = Color.White;
            DesignPanelControl.Controls.Add(ChildMainForm);
            ChildMainForm.Activate();
            ChildMainForm.Show();

            //한번 생성자
            //JobHistory = new JobHistory(this, uow);
            //WaitPosition = new WaitPositionTimeHistory(this, uow);
        }

        public void DbDrawUCMApView(UCMapView view, FloorMapIdConfigModel floorMapIdConfig, string fleetIp, bool Flag)
        {
            if (fleetIp != null) view.UriStr = $"http://{fleetIp}/";
            view.MapID = floorMapIdConfig.MapID;
            view.Init(floorMapIdConfig, fleetIp, Flag);
            view.StartLoop();
        }

        /// <summary>
        /// Map 메뉴 생성 후 Config 설정되어 있는 층에 Check 표시
        /// </summary>
        private void MapMenuLoad()
        {
            string tmp = ConfigurationManager.AppSettings["MapNames"];
            ConfigData.DisplayMapNames = Helpers.ConvertStringToDictionary(tmp) ?? new Dictionary<string, string>();
            foreach (var floorMapIndex in uow.FloorMapIDConfigs.GetAll())
            {
                if (!Ch_Floor1.Enabled) { Ch_Floor1.Text = $"{floorMapIndex.FloorIndex}"; Ch_Floor1.Enabled = true; Ch_Floor1.Visible = true; }
                else if (!Ch_Floor2.Enabled) { Ch_Floor2.Text = $"{floorMapIndex.FloorIndex}"; Ch_Floor2.Enabled = true; Ch_Floor2.Visible = true; }
                else if (!Ch_Floor3.Enabled) { Ch_Floor3.Text = $"{floorMapIndex.FloorIndex}"; Ch_Floor3.Enabled = true; Ch_Floor3.Visible = true; }
                else if (!Ch_Floor4.Enabled) { Ch_Floor4.Text = $"{floorMapIndex.FloorIndex}"; Ch_Floor4.Enabled = true; Ch_Floor4.Visible = true; }
                else if (!Ch_Floor5.Enabled) { Ch_Floor5.Text = $"{floorMapIndex.FloorIndex}"; Ch_Floor5.Enabled = true; Ch_Floor5.Visible = true; }
                else if (!Ch_Floor6.Enabled) { Ch_Floor6.Text = $"{floorMapIndex.FloorIndex}"; Ch_Floor6.Enabled = true; Ch_Floor6.Visible = true; }
                else if (!Ch_Floor7.Enabled) { Ch_Floor7.Text = $"{floorMapIndex.FloorIndex}"; Ch_Floor7.Enabled = true; Ch_Floor7.Visible = true; }
                var floorUserCheck = ConfigData.DisplayMapNames.FirstOrDefault(D => D.Value == floorMapIndex.FloorIndex);
                if (floorUserCheck.Value != null)
                {
                    if (floorUserCheck.Value == Ch_Floor1.Text) Ch_Floor1.Checked = true;
                    else if (floorUserCheck.Value == Ch_Floor2.Text) Ch_Floor2.Checked = true;
                    else if (floorUserCheck.Value == Ch_Floor3.Text) Ch_Floor3.Checked = true;
                    else if (floorUserCheck.Value == Ch_Floor4.Text) Ch_Floor4.Checked = true;
                    else if (floorUserCheck.Value == Ch_Floor5.Text) Ch_Floor5.Checked = true;
                    else if (floorUserCheck.Value == Ch_Floor6.Text) Ch_Floor6.Checked = true;
                    else if (floorUserCheck.Value == Ch_Floor7.Text) Ch_Floor7.Checked = true;
                }
            }
        }





        /// <summary>
        /// Robot 메뉴 생성 후 Config 설정되어 있는 Robot Check 표시
        /// </summary>
        private void RobotMenuLoad()
        {
            string tmp = ConfigurationManager.AppSettings["RobotNames"];
            ConfigData.DisplayRobotNames = Helpers.ConvertStringToDictionary(tmp) ?? new Dictionary<string, string>();
            foreach (var robot in uow.Robots.GetAll())
            {
                if (!CH_Robot1.Enabled) { CH_Robot1.Text = robot.RobotAlias; CH_Robot1.Enabled = true; CH_Robot1.Visible = true; }
                else if (!CH_Robot2.Enabled) { CH_Robot2.Text = robot.RobotAlias; CH_Robot2.Enabled = true; CH_Robot2.Visible = true; }
                else if (!CH_Robot3.Enabled) { CH_Robot3.Text = robot.RobotAlias; CH_Robot3.Enabled = true; CH_Robot3.Visible = true; }
                else if (!CH_Robot4.Enabled) { CH_Robot4.Text = robot.RobotAlias; CH_Robot4.Enabled = true; CH_Robot4.Visible = true; }
                else if (!CH_Robot5.Enabled) { CH_Robot5.Text = robot.RobotAlias; CH_Robot5.Enabled = true; CH_Robot5.Visible = true; }
                else if (!CH_Robot6.Enabled) { CH_Robot6.Text = robot.RobotAlias; CH_Robot6.Enabled = true; CH_Robot6.Visible = true; }
                else if (!CH_Robot7.Enabled) { CH_Robot7.Text = robot.RobotAlias; CH_Robot7.Enabled = true; CH_Robot7.Visible = true; }
                else if (!CH_Robot8.Enabled) { CH_Robot8.Text = robot.RobotAlias; CH_Robot8.Enabled = true; CH_Robot8.Visible = true; }
                else if (!CH_Robot9.Enabled) { CH_Robot9.Text = robot.RobotAlias; CH_Robot9.Enabled = true; CH_Robot9.Visible = true; }
                else if (!CH_Robot10.Enabled) { CH_Robot10.Text = robot.RobotAlias; CH_Robot10.Enabled = true; CH_Robot10.Visible = true; }
                else if (!CH_Robot11.Enabled) { CH_Robot11.Text = robot.RobotAlias; CH_Robot11.Enabled = true; CH_Robot11.Visible = true; }
                else if (!CH_Robot12.Enabled) { CH_Robot12.Text = robot.RobotAlias; CH_Robot12.Enabled = true; CH_Robot12.Visible = true; }

                var RobotUserCheck = ConfigData.DisplayRobotNames.FirstOrDefault(D => D.Value == robot.RobotAlias);
                if (RobotUserCheck.Value != null)
                {
                    if (RobotUserCheck.Value == CH_Robot1.Text) CH_Robot1.Checked = true;
                    else if (RobotUserCheck.Value == CH_Robot2.Text) CH_Robot2.Checked = true;
                    else if (RobotUserCheck.Value == CH_Robot3.Text) CH_Robot3.Checked = true;
                    else if (RobotUserCheck.Value == CH_Robot4.Text) CH_Robot4.Checked = true;
                    else if (RobotUserCheck.Value == CH_Robot5.Text) CH_Robot5.Checked = true;
                    else if (RobotUserCheck.Value == CH_Robot6.Text) CH_Robot6.Checked = true;
                    else if (RobotUserCheck.Value == CH_Robot7.Text) CH_Robot7.Checked = true;
                    else if (RobotUserCheck.Value == CH_Robot8.Text) CH_Robot8.Checked = true;
                    else if (RobotUserCheck.Value == CH_Robot9.Text) CH_Robot9.Checked = true;
                    else if (RobotUserCheck.Value == CH_Robot10.Text) CH_Robot10.Checked = true;
                    else if (RobotUserCheck.Value == CH_Robot11.Text) CH_Robot11.Checked = true;
                    else if (RobotUserCheck.Value == CH_Robot12.Text) CH_Robot12.Checked = true;
                }

            }
        }

        #region SubFunc - Sub 모음
        public void EventLog(string ScreenName, string Comment)
        {
            EventLogger.InfoFormat("{0}, {1}", ScreenName, Comment);
        }

        public void ACS_UI_Log(string logMessage, [CallerFilePath] string file = "",
                                                  [CallerLineNumber] int line = 0,
                                                  [CallerMemberName] string member = "")
        {
            string tmp = $"{Path.GetFileName(file)}({line})";
            string logMessageFormatted = string.Format("{0:HH:mm:ss}, {1,-35}, {2}", DateTime.Now, tmp, logMessage);

            for (int i = 0; i < 100; i++)
            {
                if (ACS_Array_Log_Text[i].Length == 0)
                {
                    ACS_Array_Log_Text[i] = logMessageFormatted;
                    bACSLogRefresh = true;

                    break;
                }
            }
        }

        private void subFunc_Public_DataInit()
        {
            //MiR, Client, Log 등 ACS Program 내에서 사용하는 변수 초기화
            for (int i = 0; i < ACS_Array_Log_Text.Length; i++)
            {
                ACS_Array_Log_Text[i] = string.Empty;
            }
        }
        #endregion

        #region 메뉴 버튼 Event 모음

        private void Floor_CheckedChanged(object sender, EventArgs e)
        {
            var Chk_Btn = (ToolStripMenuItem)sender;

            if (Chk_Btn.Checked)
            {
                Chk_Btn.Image = Properties.Resources.Checked;
                Chk_Btn.BackgroundImageLayout = ImageLayout.Tile;
            }
            else
            {
                Chk_Btn.Image = null;
                Chk_Btn.BackgroundImageLayout = ImageLayout.Tile;
            }
        }
        private void Robot_CheckedChanged(object sender, EventArgs e)
        {
            var Chk_Btn = (ToolStripMenuItem)sender;

            if (Chk_Btn.Checked)
            {
                Chk_Btn.Image = Properties.Resources.Checked;
                Chk_Btn.BackgroundImageLayout = ImageLayout.Tile;
            }
            else
            {
                Chk_Btn.Image = null;
                Chk_Btn.BackgroundImageLayout = ImageLayout.Tile;
            }
        }

        private void FloorCH_Click(object sender, EventArgs e)
        {
            var Chk_Btn = (ToolStripMenuItem)sender;
            var Maps = uow.FloorMapIDConfigs.GetAll().FirstOrDefault(x => x.FloorIndex == Chk_Btn.Text);

            if (Maps != null)
            {
                if (!Chk_Btn.Checked)
                {
                    if (ConfigData.DisplayMapNames.ContainsKey(Maps.FloorIndex) == true)
                    {
                        ConfigData.DisplayMapNames.Remove(Maps.FloorIndex);
                        Chk_Btn.Checked = false;
                    }
                    else
                    {
                        //Config데이터가 없을경우
                        ConfigData.DisplayMapNames.Add(Maps.FloorIndex, Maps.FloorIndex);
                        Chk_Btn.Checked = true;
                    }
                }
                else
                {
                    if (ConfigData.DisplayMapNames.ContainsKey(Maps.FloorIndex) == false)
                    {
                        //Config 데이터에 없을경우
                        ConfigData.DisplayMapNames.Add(Maps.FloorIndex, Maps.FloorIndex);
                        Chk_Btn.Checked = true;
                    }
                    else
                    {
                        ConfigData.DisplayMapNames.Remove(Maps.FloorIndex);
                        Chk_Btn.Checked = false;
                    }
                }
            }
            string saveDictText = Helpers.ConvertDictionaryToString(ConfigData.DisplayMapNames);

            //ConfigData 수정
            AppConfiguration.SetAppConfig("MapNames", saveDictText);
        }

        private void RobotCH_Click(object sender, EventArgs e)
        {
            var Chk_Btn = (ToolStripMenuItem)sender;
            var Robot = uow.Robots.DBGetAll().FirstOrDefault(x => x.RobotAlias == Chk_Btn.Text);

            if (Robot != null)
            {
                if (!Chk_Btn.Checked)
                {

                    if (ConfigData.DisplayRobotNames.ContainsKey(Robot.RobotName) == true)
                    {
                        ConfigData.DisplayRobotNames.Remove(Robot.RobotName);
                        Chk_Btn.Checked = false;
                    }
                    else
                    {
                        //Config데이터가 없을경우
                        ConfigData.DisplayRobotNames.Add(Robot.RobotName, Robot.RobotAlias);
                        Chk_Btn.Checked = true;
                    }
                }
                else
                {
                    if (ConfigData.DisplayRobotNames.ContainsKey(Robot.RobotName) == false)
                    {
                        //Config 데이터에 없을경우
                        ConfigData.DisplayRobotNames.Add(Robot.RobotName, Robot.RobotAlias);
                        Chk_Btn.Checked = true;
                    }
                    else
                    {
                        ConfigData.DisplayRobotNames.Remove(Robot.RobotName);
                        Chk_Btn.Checked = false;
                    }
                }
            }
            string saveDictText = Helpers.ConvertDictionaryToString(ConfigData.DisplayRobotNames);

            //ConfigData 수정
            AppConfiguration.SetAppConfig("RobotNames", saveDictText);
        }





        private void MenuItem_MainView_Click(object sender, EventArgs e)
        {
            ChildMainForm.TopLevel = false;
            ChildMainForm.Dock = DockStyle.Fill;
            DesignPanelControl.Controls.Clear();
            DesignPanelControl.BackColor = Color.White;
            DesignPanelControl.Controls.Add(ChildMainForm);
            ChildMainForm.Activate();
            ChildMainForm.Show();
        }
        /// <summary>
        /// JobHistory Click 이벤트 JobHistory Form 을 띄운다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStrip_JobHistory_Click(object sender, EventArgs e)
        {
            //JobHistory.TopLevel = false;
            //JobHistory.Dock = DockStyle.Fill;
            //DesignPanelControl.Controls.Clear();
            //DesignPanelControl.BackColor = Color.White;
            //DesignPanelControl.Controls.Add(JobHistory);
            //JobHistory.Activate();
            //JobHistory.Show();
        }
        /// <summary>
        /// WaitPositionTime Click 이벤트 WaitPositionTime Form 을 띄운다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ToolStrip_WaitPositionTime_Click(object sender, EventArgs e)
        {
            //WaitPosition.TopLevel = false;
            //WaitPosition.Dock = DockStyle.Fill;
            //DesignPanelControl.Controls.Clear();
            //DesignPanelControl.BackColor = Color.White;
            //DesignPanelControl.Controls.Add(WaitPosition);
            //WaitPosition.Activate();
            //WaitPosition.Show();
        }

        private void elevatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //elevator = new SettingsElevator(this, uow);
            //elevator.TopLevel = false;
            //elevator.Dock = DockStyle.Fill;
            //flyoutPanelControl1.Controls.Add(elevator);
            //flyoutPanel1.OwnerControl = menuStrip1;
            //flyoutPanel1.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.TopRight;
            //flyoutPanel1.Options.HorzIndent = 20;
            //flyoutPanel1.Options.VertIndent = 45;
            //flyoutPanel1.ShowPopup();
            //elevator.Activate();
            //elevator.Show();
        }

        private void callSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CallSystem = new SettingsCallMissions(this, uow);
            //CallSystem.TopLevel = false;
            //CallSystem.Dock = DockStyle.Fill;
            //flyoutPanelControl2.Controls.Add(CallSystem);
            //flyoutPanel2.OwnerControl = menuStrip1;
            //flyoutPanel2.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.TopRight;
            //flyoutPanel2.Options.HorzIndent = 20;
            //flyoutPanel2.Options.VertIndent = 45;
            //flyoutPanel2.ShowPopup();
            //CallSystem.Activate();
            //CallSystem.Show();
        }




        private void MenuItem_MouseHover(object sender, EventArgs e)
        {
            var Chk_Btn = (ToolStripMenuItem)sender;

            Chk_Btn.ForeColor = Color.Gray;
        }

        private void MenuItem_MouseLeave(object sender, EventArgs e)
        {
            var Chk_Btn = (ToolStripMenuItem)sender;

            Chk_Btn.ForeColor = Color.Black;
        }




        #endregion


        private void MainForm_Resize(object sender, EventArgs e)
        {
            //사이즈가 변할 때 마다 지도 및 MainGrid 크기 변경
            ChildMainForm.Sub_DisplayMapNames.Clear();
        }
    }
}
