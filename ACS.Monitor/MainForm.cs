using DevExpress.Skins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.FluentDesignSystem;
using DevExpress.XtraBars.Navigation;
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

namespace ACS.Monitor
{
    public partial class MainForm : FluentDesignForm
    {
        private readonly static ILog EventLogger = LogManager.GetLogger("Event"); //Function 실행관련 Log
        private readonly static ILog UserLogger = LogManager.GetLogger("User"); //버튼 및 화면조작관련 Log
        private readonly static ILog TimeoutLogger = LogManager.GetLogger("Timeout");

        private readonly UnitOfWork uow;
        private AutoScreen ChildMainForm;
        private JobHistory JobHistory;
        private WaitPositionTimeHistory WaitPosition;
        private SettingsElevator elevator;
        private SettingsCallMissions CallSystem;

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
            //DesignFunc();

            ////부모 Form 으로 설정한다
            //this.IsMdiContainer = true; // MDI 컨테이너로 설정
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.BackColor = Color.White; // 원하는 색상으로 변경 가능
            //SkinManager.EnableFormSkins();
            //SkinManager.EnableMdiFormSkins();
            InitTopBarControl();
            ChildMainFormFunc();

            MapMenuLoad();
            RobotMenuLoad();
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

        private void UserLoginButtonClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item.Caption == "UserLogin")
            {
                //사번 입력 Form을 불러옴
                var UserNumberForm = new UserNumberForm();
                DialogResult result = UserNumberForm.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    //사번 입력하여 검색
                    var UserNumber = UserNumberInfo.GetUserLogin(UserNumberForm.UserNumber, UserNumberForm.UserPassword);
                    //사번이 없는경우 설정창을 Clear한후 다시 초기설정함
                    if (UserNumber == null) MessageBox.Show("등록되지 않은 사원번호 이거나 비밀번호가 틀립니다!");
                    else
                    {
                        S_UserNumber = UserNumber;
                        //barStaticItem1.Caption = $"사원번호 = {UserNumber.UserNumber} / 사원이름 = {UserNumber.UserName}";
                        L_Login.Text = $"사원번호 = {UserNumber.UserNumber} / 사원이름 = {UserNumber.UserName}";
                        barButtonItem1.Caption = "UserLogOut";
                        MenuItem_Setting.Visible = true;
                    }
                }
            }
            else
            {
                barButtonItem1.Caption = "UserLogin";
                barStaticItem1.Caption = "";
                MenuItem_Setting.Visible = false;
            }
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
            JobHistory = new JobHistory(this, uow);
            WaitPosition = new WaitPositionTimeHistory(this, uow);
        }

        public void DrawUCMapView(UCMapView view, MapNameAlias mapDBdata, MapData Data, string fleetIp, bool Flag)
        {
            view.UriStr = $"http://{fleetIp}/";
            view.MapID = mapDBdata.MapID;
            view.Init(Data.mapScale, Data.mouseFirstLocation, Data.mouseMoveOffset, mapDBdata.FloorName, Flag);
            view.StartLoop();
        }

        private void MapMenuLoad()
        {
            string tmp = ConfigurationManager.AppSettings["MapNames"];
            ConfigData.DisplayMapNames = Helpers.ConvertintToDictionary(tmp) ?? new Dictionary<int, string>();
            var Maps = MapNameAlias.GetAll();

            foreach (var temp in ConfigData.DisplayMapNames)
            {
                var Map = Maps.FirstOrDefault(x => x.FloorIndex == temp.Value);

                if ((Map.FloorIndex + " - " + Map.FloorName) == "2F - T2F")
                    Ch_2F.Checked = true;
                else if ((Map.FloorIndex + " - " + Map.FloorName) == "3F - T3F")
                    Ch_3F.Checked = true;
                else if ((Map.FloorIndex + " - " + Map.FloorName) == "4F - T4F")
                    Ch_4F.Checked = true;
                else if ((Map.FloorIndex + " - " + Map.FloorName) == "6F - M3F")
                    Ch_6F.Checked = true;
            }
        }

        private void RobotMenuLoad()
        {
            string tmp = ConfigurationManager.AppSettings["RobotNames"];
            ConfigData.DisplayRobotNames = Helpers.ConvertStringToDictionary(tmp) ?? new Dictionary<string, string>();
            var Robots = RobotNameAlias.GetAll();

            foreach (var temp in ConfigData.DisplayRobotNames)
            {
                var Robot = Robots.FirstOrDefault(x => x.RobotAlias == temp.Value);

                if (Robot.RobotAlias == "SAMB#1")
                    CH_SAMB1.Checked = true;
                else if (Robot.RobotAlias == "SAMB#2")
                    CH_SAMB2.Checked = true;
                else if (Robot.RobotAlias == "SAMB#3")
                    CH_SAMB3.Checked = true;
                else if (Robot.RobotAlias == "SAMB#4")
                    CH_SAMB4.Checked = true;
                else if (Robot.RobotAlias == "SAMB#5")
                    CH_SAMB5.Checked = true;
                else if (Robot.RobotAlias == "SAMB#6")
                    CH_SAMB6.Checked = true;
                else if (Robot.RobotAlias == "SAMB#7")
                    CH_SAMB7.Checked = true;
                else if (Robot.RobotAlias == "SAMB#8")
                    CH_SAMB8.Checked = true;
                else if (Robot.RobotAlias == "SAMB#9")
                    CH_SAMB9.Checked = true;
                else if (Robot.RobotAlias == "SAMB#10")
                    CH_SAMB10.Checked = true;
                else if (Robot.RobotAlias == "SAMB#11")
                    CH_SAMB11.Checked = true;
                else if (Robot.RobotAlias == "TAMB#1")
                    CH_TAMB1.Checked = true;
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
        private void ToolStrip_JobHistory_Click(object sender, EventArgs e)
        {
            JobHistory.TopLevel = false;
            JobHistory.Dock = DockStyle.Fill;
            DesignPanelControl.Controls.Clear();
            DesignPanelControl.BackColor = Color.White;
            DesignPanelControl.Controls.Add(JobHistory);
            JobHistory.Activate();
            JobHistory.Show();
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

        private void ToolStrip_WaitPositionTime_Click(object sender, EventArgs e)
        {
            WaitPosition.TopLevel = false;
            WaitPosition.Dock = DockStyle.Fill;
            DesignPanelControl.Controls.Clear();
            DesignPanelControl.BackColor = Color.White;
            DesignPanelControl.Controls.Add(WaitPosition);
            WaitPosition.Activate();
            WaitPosition.Show();
        }

        private void elevatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            elevator = new SettingsElevator(this, uow);
            elevator.TopLevel = false;
            elevator.Dock = DockStyle.Fill;
            flyoutPanelControl1.Controls.Add(elevator);
            flyoutPanel1.OwnerControl = menuStrip1;
            flyoutPanel1.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.TopRight;
            flyoutPanel1.Options.HorzIndent = 20;
            flyoutPanel1.Options.VertIndent = 45;
            flyoutPanel1.ShowPopup();
            elevator.Activate();
            elevator.Show();
        }

        private void callSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CallSystem = new SettingsCallMissions(this, uow, S_UserNumber);
            CallSystem.TopLevel = false;
            CallSystem.Dock = DockStyle.Fill;
            flyoutPanelControl2.Controls.Add(CallSystem);
            flyoutPanel2.OwnerControl = menuStrip1;
            flyoutPanel2.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.TopRight;
            flyoutPanel2.Options.HorzIndent = 20;
            flyoutPanel2.Options.VertIndent = 45;
            flyoutPanel2.ShowPopup();
            CallSystem.Activate();
            CallSystem.Show();
        }

        private void Floor_CheckedChanged(object sender, EventArgs e)
        {
            var Chk_Btn = (ToolStripMenuItem) sender;

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
            var Maps = MapNameAlias.GetAll().FirstOrDefault(x => x.FloorName == Chk_Btn.Text.Split('-')[1].Trim().ToString());

            if (Maps != null && Chk_Btn.Checked)
            {
                if (ConfigData.DisplayMapNames.ContainsKey(Maps.Id) == false)
                    ConfigData.DisplayMapNames.Add(Maps.Id, Maps.FloorIndex);
            }
            else if (Maps != null && !Chk_Btn.Checked)
            {
                if (ConfigData.DisplayMapNames.ContainsKey(Maps.Id) == true)
                    ConfigData.DisplayMapNames.Remove(Maps.Id);
            }

            string saveDictText = Helpers.ConvertDictionaryToString(ConfigData.DisplayMapNames);
            MapSaveSettings(saveDictText);
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

        private void RobotCH_Click(object sender, EventArgs e)
        {
            var Chk_Btn = (ToolStripMenuItem)sender;
            var Robot = uow.Robots.GetAll().FirstOrDefault(x => x.RobotAlias == Chk_Btn.Text);

            if (Robot != null && Chk_Btn.Checked)
            {
                if (ConfigData.DisplayRobotNames.ContainsKey(Robot.RobotName) == false)
                    ConfigData.DisplayRobotNames.Add(Robot.RobotName, Robot.RobotAlias);
            }
            else if (Robot != null && !Chk_Btn.Checked)
            {
                if (ConfigData.DisplayRobotNames.ContainsKey(Robot.RobotName) == true)
                    ConfigData.DisplayRobotNames.Remove(Robot.RobotName);
            }

            string saveDictText = Helpers.ConvertDictionaryToString(ConfigData.DisplayRobotNames);
            RobotSaveSettings(saveDictText);
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
        #endregion

        private void RobotSaveSettings(string value)
        {
            AppConfiguration.SetAppConfig("RobotNames", value);
        }

        private void MapSaveSettings(string value)
        {
            AppConfiguration.SetAppConfig("MapNames", value);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            //사이즈가 변할 때 마다 지도 및 MainGrid 크기 변경
            ChildMainForm.Sub_DisplayMapNames.Clear();
        }
    }
}
