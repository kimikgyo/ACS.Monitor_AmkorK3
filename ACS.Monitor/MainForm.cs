using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using log4net;
using Monitor.Common;
using Monitor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace ACS.Monitor
{
    public partial class MainForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private readonly static ILog EventLogger = LogManager.GetLogger("Event"); //Function 실행관련 Log
        private readonly static ILog UserLogger = LogManager.GetLogger("User"); //버튼 및 화면조작관련 Log
        private readonly static ILog TimeoutLogger = LogManager.GetLogger("Timeout");

        private static string AmkorImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "LogInAmkorImage31.png");
        Image AmkorImage = Image.FromFile(AmkorImagePath);

        private static string AmkorIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "Monitoring_Icon.ico");
        Image AmkorIcon = Image.FromFile(AmkorIconPath);

        private Color skinColor = Color.FromArgb(43, 52, 59);
        private Color backColor = Color.FromArgb(30, 39, 46);
        private Color mouseOverColor = Color.FromArgb(45, 65, 77);
        private Color mouseOverTextColor = Color.FromArgb(57, 173, 233);
        private Color nomalTextColor = Color.FromArgb(167, 168, 169);
        private Color TopbarSkinColor = Color.FromArgb(116, 125, 132);

        private readonly UnitOfWork uow;
        private MapView ChildMainForm = null;
        private RobotScreen robotScreen = null;
        private ElevatorSystem elevatorSystem = null;
        private CallSystem callSystem = null;
        private SettingSystem settingSystem = null;

        private GetDataControl getDataControl = null;
        private AlertControl AlarmPupUpcontrol = null;


        public MainForm()
        {
            InitializeComponent();
            uow = new UnitOfWork();
            Init();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Start();
            FormInit();
            accordionControlInit();
            InitDockPanel();
            ChildMainFormFunc();

        }
        private void Init()
        {
            #region Screen Class선언

            ChildMainForm = new MapView(this, uow);
            robotScreen = new RobotScreen();
            elevatorSystem = new ElevatorSystem(this, uow);
            settingSystem = new SettingSystem();
            callSystem = new CallSystem(this, uow);
            #endregion

            getDataControl = new GetDataControl(this, uow);
            AlarmPupUpcontrol = new AlertControl();

            #region AlertControl Event
            AlarmPupUpcontrol.FormLoad += AlarmPupUpcontrol_FormLoad;
            #endregion

        }

        private void AlarmPupUpcontrol_FormLoad(object sender, AlertFormLoadEventArgs e)
        {
            e.Buttons.PinButton.SetDown(true);
        }

        private void Start()
        {
            getDataControl.Start();
        }

        private void FormInit()
        {
            this.ControlBox = false;
            //this.MinimizeBox = false;  // 최소화 버튼 비활성
            //this.MaximizeBox = true;  // 최대화 버튼 비활성화
            //this.CloseBox = false;

            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.LookAndFeel.SkinMaskColor = skinColor;
            this.LookAndFeel.SkinMaskColor2 = skinColor;
            this.BackColor = backColor;
            this.ForeColor = Color.White;
            this.IconOptions.Image = AmkorIcon;

            fluentDesignFormContainer1.BackColor = backColor;
            fluentDesignFormContainer1.ForeColor = Color.White;

            //panelControl1.Visible = true;
            //panelControl1.BackColor = skinColor;
            //panelControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            barStaticItem1.ImageOptions.Image = AmkorImage;
            lbl_Login.BackColor = Color.Transparent;
            lbl_Login.Cursor = Cursors.Hand;
            lbl_Login.Anchor = /*AnchorStyles.Top |*/ AnchorStyles.Right;
            lbl_Login.BackColor = TopbarSkinColor;
            lbl_Login.Click += labelClick;

            lbl_FormSizeChange.BackColor = Color.Transparent;
            lbl_FormSizeChange.Cursor = Cursors.Hand;
            lbl_FormSizeChange.Anchor = AnchorStyles.Right;
            lbl_FormSizeChange.BackColor = TopbarSkinColor;
            lbl_FormSizeChange.Click += labelClick;

            lbl_FormClose.BackColor = Color.Transparent;
            lbl_FormClose.Cursor = Cursors.Hand;
            lbl_FormClose.Anchor = AnchorStyles.Right;
            lbl_FormClose.BackColor = TopbarSkinColor;
            lbl_FormClose.Click += labelClick;

            lbl_UserNumberImage.BackColor = TopbarSkinColor;
            lbl_UserNumberImage.Anchor = AnchorStyles.Left;
            lbl_UserNumberImage.Visible = false;

            lbl_CallSystemImage.BackColor = TopbarSkinColor;
            lbl_CallSystemImage.Anchor = AnchorStyles.Left;
            lbl_CallSystemImage.Visible = false;

            lbl_ElevatorSystemImage.BackColor = TopbarSkinColor;
            lbl_ElevatorSystemImage.Anchor = AnchorStyles.Left;
            lbl_ElevatorSystemImage.Visible = false;

            lbl_UserNumberText.BackColor = TopbarSkinColor;
            lbl_UserNumberText.Anchor = AnchorStyles.Left;
            lbl_UserNumberText.Visible = false;

            lbl_CallSystemText.BackColor = TopbarSkinColor;
            lbl_CallSystemText.Anchor = AnchorStyles.Left;
            lbl_CallSystemText.Visible = false;

            lbl_ElevatorSystemText.BackColor = TopbarSkinColor;
            lbl_ElevatorSystemText.Anchor = AnchorStyles.Left;
            lbl_ElevatorSystemText.Visible = false;

            //lbl_UserNumberDisPlay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }



        private void accordionControlInit()
        {
            //설정창 ViewType
            accordionControl1.ViewType = AccordionControlViewType.HamburgerMenu;

            accordionControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            accordionControl1.LookAndFeel.SkinName = "DevExpress Dark Style";
            accordionControl1.BackColor = skinColor;
            accordionControl1.LookAndFeel.SkinMaskColor = skinColor;
            accordionControl1.LookAndFeel.SkinMaskColor2 = skinColor;

            accordionControl1.Cursor = Cursors.Hand;


            #region 그룹과 아이템에 대한 색상 변경
            accordionControl1.Appearance.Group.Normal.ForeColor = nomalTextColor; // 기본 텍스트 색상
            //accordionControl1.Appearance.Group.Hovered.ForeColor = Color.Yellow; // 마우스 오버 시 텍스트 색상
            //accordionControl1.Appearance.Group.Pressed.ForeColor = Color.Red; // 클릭 시 텍스트 색상

            accordionControl1.Appearance.Item.Normal.ForeColor = nomalTextColor; // 기본 텍스트 색상
                                                                                 //accordionControl1.Appearance.Item.Hovered.ForeColor = Color.Yellow; // 마우스 오버 시 텍스트 색상
                                                                                 //accordionControl1.Appearance.Item.Pressed.ForeColor = Color.Red; // 클릭 시 텍스트 색상
            #endregion

            #region accordionControl 색상 변경

            accordionControlMainView.Appearance.Normal.BackColor = skinColor; // 기본 색상
            accordionControlMainView.Appearance.Hovered.BackColor = mouseOverColor; // 마우스 오버 색상
            accordionControlMainView.Appearance.Hovered.ForeColor = mouseOverTextColor; // 마우스 오버 글자 색상

            accordionControlHistory.Appearance.Normal.BackColor = skinColor; // 기본 색상
            accordionControlHistory.Appearance.Hovered.BackColor = mouseOverColor; // 마우스 오버 색상
            accordionControlHistory.Appearance.Hovered.ForeColor = mouseOverTextColor; // 마우스 오버 글자 색상

            accordionControlErrorLog.Appearance.Normal.BackColor = skinColor; // 기본 색상
            accordionControlErrorLog.Appearance.Hovered.BackColor = mouseOverColor; // 마우스 오버 색상
            accordionControlErrorLog.Appearance.Hovered.ForeColor = mouseOverTextColor; // 마우스 오버 글자 색상

            accordionControlJobLog.Appearance.Normal.BackColor = skinColor; // 기본 색상
            accordionControlJobLog.Appearance.Hovered.BackColor = mouseOverColor; // 마우스 오버 색상
            accordionControlJobLog.Appearance.Hovered.ForeColor = mouseOverTextColor; // 마우스 오버 글자 색상

            accordionControlSystem.Appearance.Normal.BackColor = skinColor; // 기본 색상
            accordionControlSystem.Appearance.Hovered.BackColor = mouseOverColor; // 마우스 오버 색상
            accordionControlSystem.Appearance.Hovered.ForeColor = mouseOverTextColor; // 마우스 오버 글자 색상

            accordionControlCall.Appearance.Normal.BackColor = skinColor; // 기본 색상
            accordionControlCall.Appearance.Hovered.BackColor = mouseOverColor; // 마우스 오버 색상
            accordionControlCall.Appearance.Hovered.ForeColor = mouseOverTextColor; // 마우스 오버 글자 색상

            accordionControlElevator.Appearance.Normal.BackColor = skinColor; // 기본 색상
            accordionControlElevator.Appearance.Hovered.BackColor = mouseOverColor; // 마우스 오버 색상
            accordionControlElevator.Appearance.Hovered.ForeColor = mouseOverTextColor; // 마우스 오버 글자 색상

            accordionControlSetting.Appearance.Normal.BackColor = skinColor; // 기본 색상
            accordionControlSetting.Appearance.Hovered.BackColor = mouseOverColor; // 마우스 오버 색상
            accordionControlSetting.Appearance.Hovered.ForeColor = mouseOverTextColor; // 마우스 오버 글자 색상

            #endregion

            accordionControlMapView.Click += AccordionControl_Item_Click;
            accordionControlRobot.Click += AccordionControl_Item_Click;
            accordionControlErrorLog.Click += AccordionControl_Item_Click;
            accordionControlJobLog.Click += AccordionControl_Item_Click;
            accordionControlCall.Click += AccordionControl_Item_Click;
            accordionControlElevator.Click += AccordionControl_Item_Click;
            accordionControlSetting.Click += AccordionControl_Item_Click;
        }

        private void AccordionControl_Item_Click(object sender, EventArgs e)
        {
            Control formControl = null;

            if (fluentDesignFormContainer1 != null) formControl = fluentDesignFormContainer1.Controls[0];

            string itemName = ((AccordionControlElement)sender).Name;
            switch (itemName)
            {
                case "accordionControlMapView":
                    ChildMainForm.TopLevel = false;
                    ChildMainForm.Dock = DockStyle.Fill;

                    fluentDesignFormContainer1.Controls.Clear();
                    fluentDesignFormContainer1.BackColor = Color.White;
                    fluentDesignFormContainer1.Controls.Add(ChildMainForm);
                    ConfigData.MapViewScreenActive = true;
                    ChildMainForm.Activate();
                    ChildMainForm.Show();
                    ConfigData.SettingScreenActive = false;

                    break;
                case "accordionControlRobot":
                    if (dockPanelRobot.Visibility == DockVisibility.Hidden)
                    {
                        //창이 생성이 되지 않았을경우
                        dockPanelRobot.Visibility = DockVisibility.Visible;
                        ConfigData.RobotScreenActive = true;
                        robotScreen.TopLevel = false;
                        robotScreen.Dock = DockStyle.Fill;
                        dockPanelRobot.BackColor = backColor;
                        dockPanelRobot.Controls.Add(robotScreen);
                        robotScreen.Activate();
                        robotScreen.Show();
                    }
                    else if (dockPanelRobot.Visibility == DockVisibility.AutoHide)
                    {
                        //창이 숨겨져 있을경우
                        dockPanelRobot.Visibility = DockVisibility.Visible;
                    }
                    break;
                case "accordionControlErrorLog":
                    break;
                case "accordionControlJobLog":
                    break;
                case "accordionControlCall":
                    if (ConfigData.UserCallAuthority == 1)
                    {
                        if (dockPanelCall.Visibility == DockVisibility.Hidden)
                        {
                            //창이 생성이 되지 않았을경우
                            dockPanelCall.Visibility = DockVisibility.Visible;
                            ConfigData.CallScreenActive = true;
                            callSystem.TopLevel = false;
                            callSystem.Dock = DockStyle.Fill;
                            dockPanelCall.BackColor = Color.White;
                            dockPanelCall.Controls.Add(callSystem);
                            callSystem.Activate();
                            callSystem.Show();
                        }
                        else if (dockPanelCall.Visibility == DockVisibility.AutoHide)
                        {
                            //창이 숨겨져 있을경우
                            dockPanelCall.Visibility = DockVisibility.Visible;
                        }
                    }
                    else
                    {
                        MessageBox.Show("사용권한이 없습니다!!!" + "\r\n" + "관리자에게 문의해 주시기 바랍니다!!");
                    }
                    break;
                case "accordionControlElevator":
                    if (ConfigData.UserElevatorAuthority == 1)
                    {
                        if (dockPanelElevator.Visibility == DockVisibility.Hidden)
                        {
                            //창이 생성이 되지 않았을경우
                            dockPanelElevator.Visibility = DockVisibility.Visible;
                            ConfigData.ElevatorScreenActive = true;
                            elevatorSystem.TopLevel = false;
                            elevatorSystem.Dock = DockStyle.Fill;
                            dockPanelElevator.BackColor = Color.White;
                            dockPanelElevator.Controls.Add(elevatorSystem);
                            elevatorSystem.Activate();
                            elevatorSystem.Show();
                        }
                        else if (dockPanelElevator.Visibility == DockVisibility.AutoHide)
                        {
                            //창이 숨겨져 있을경우
                            dockPanelElevator.Visibility = DockVisibility.Visible;
                        }
                    } 
                    else
                    {
                        MessageBox.Show("사용권한이 없습니다!!!" + "\r\n" + "관리자에게 문의해 주시기 바랍니다!!");
                    }
                    break;
                case "accordionControlSetting":
                    settingSystem.TopLevel = false;
                    settingSystem.Dock = DockStyle.Fill;

                    fluentDesignFormContainer1.Controls.Clear();
                    fluentDesignFormContainer1.BackColor = Color.White;
                    fluentDesignFormContainer1.Controls.Add(settingSystem);
                    ConfigData.SettingScreenActive = true;
                    settingSystem.Activate();
                    settingSystem.Show();
                    ConfigData.MapViewScreenActive = false;

                    break;

            }
        }

        private void InitDockPanel()
        {
            #region Call DockPanel 
            dockPanelCall.BackColor = backColor;
            dockPanelCall.Text = "Call";
            dockPanelCall.Options.AllowDockAsTabbedDocument = false;  // 문서 탭 형태가 아닌 도킹
            dockPanelCall.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            dockPanelCall.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            #endregion

            #region Robot DockPanel
            dockPanelRobot.BackColor = backColor;
            dockPanelRobot.Text = "Robot";
            dockPanelRobot.Options.AllowDockAsTabbedDocument = false;  // 문서 탭 형태가 아닌 도킹
            dockPanelRobot.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            dockPanelRobot.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            #endregion

            #region Elevator Panel
            dockPanelElevator.BackColor = backColor;
            dockPanelElevator.Text = "Elevator";
            dockPanelElevator.Options.AllowDockAsTabbedDocument = false;  // 문서 탭 형태가 아닌 도킹
            dockPanelElevator.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            dockPanelElevator.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            #endregion

            #region DockPanel Event
            dockPanelCall.ClosingPanel += DockPanel_ClosingPanel;
            dockPanelElevator.ClosingPanel += DockPanel_ClosingPanel;
            dockPanelRobot.ClosingPanel += DockPanel_ClosingPanel;
            #endregion
        }



        private void DockPanel_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            string dockPanelName = ((DockPanel)sender).Name;
            switch (dockPanelName)
            {
                case "dockPanelRobot":
                    ConfigData.RobotScreenActive = false;
                    break;
                case "dockPanelElevator":
                    ConfigData.ElevatorScreenActive = false;
                    break;
                case "dockPanelCall":
                    ConfigData.CallScreenActive = false;
                    break;
            }
        }



        /// <summary>
        /// AutoScreen을 MainForm으로 Dock하는 Func
        /// </summary>
        private void ChildMainFormFunc()
        {
            ChildMainForm.TopLevel = false;
            ChildMainForm.Dock = DockStyle.Fill;
            fluentDesignFormContainer1.BackColor = Color.White;
            fluentDesignFormContainer1.Controls.Add(ChildMainForm);
           ConfigData.MapViewScreenActive = true;
            ChildMainForm.Activate();
            ChildMainForm.Show();
            ConfigData.SettingScreenActive = false;
        }

        public void DbDrawUCMApView(UCMapView view, FloorMapIdConfigModel floorMapIdConfig, string fleetIp, bool Flag)
        {
            if (fleetIp != null) view.UriStr = $"http://{fleetIp}/";
            view.MapID = floorMapIdConfig.MapID;

            //view.UriStr = $"http://192.168.8.50/";
            //view.MapID = "26d15903-6e46-11ef-9051-000e8eacad4f";
            view.Init(floorMapIdConfig, fleetIp, Flag);
            view.StartLoop();
        }

        private void labelClick(object sender, EventArgs e)
        {
            string labelName = ((LabelControl)sender).Name;
            switch (labelName)
            {
                case "lbl_FormClose":

                    this.Close();

                    break;
                case "lbl_FormSizeChange":

                    if (WindowState == FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
                    else this.WindowState = FormWindowState.Normal;

                    break;

                case "lbl_Login":
                    if (ConfigData.UserNumber == null)
                    {
                        var UserNumberForm = new UserLoginForm();
                        //사번 입력 Form을 불러옴
                        UserNumberForm.StartPosition = FormStartPosition.Manual;
                        UserNumberForm.Location = new Point(this.Location.X + (this.Width - UserNumberForm.Width) / 2, this.Location.Y + (this.Width - UserNumberForm.Width) / 6);
                        DialogResult result = UserNumberForm.ShowDialog();
                        if (result == DialogResult.Yes)
                        {
                            // 사번 입력하여 검색
                            var UserNumber = uow.UserNumbers.DBGetAll().FirstOrDefault(u => u.UserNumber == UserNumberForm.UserNumber);
                            //사번이 없는경우 설정창을 Clear한후 다시 초기설정함
                            if (UserNumber == null) MessageBox.Show("등록되지 않은 사원번호 이거나 비밀번호가 틀립니다!");
                            else
                            {
                                ConfigData.UserNumber = UserNumber.UserNumber;
                                ConfigData.UserName = UserNumber.UserName;
                                ConfigData.UserElevatorAuthority = UserNumber.ElevatorAuthority;
                                ConfigData.UserCallAuthority = UserNumber.CallMissionAuthority;

                                lbl_UserNumberText.Text = $"사원번호 = {ConfigData.UserNumber}{Environment.NewLine}사원이름 = {ConfigData.UserName}";

                                if (ConfigData.UserElevatorAuthority == 1)
                                {
                                    lbl_ElevatorSystemText.Text = "ElevatorSystem" + "\r\n" + "사용가능";
                                }
                                else lbl_ElevatorSystemText.Text = "ElevatorSystem" + "\r\n" + "권한없음";

                                if (ConfigData.UserCallAuthority == 1)
                                {
                                    lbl_CallSystemText.Text = "CallSystem" + "\r\n" + "사용가능";
                                }
                                else lbl_CallSystemText.Text = "CallSystem" + "\r\n" + "사용가능";

                                lbl_UserNumberImage.Visible = true;
                                lbl_CallSystemImage.Visible = true;
                                lbl_ElevatorSystemImage.Visible = true;

                                lbl_UserNumberText.Visible = true;
                                lbl_CallSystemText.Visible = true;
                                lbl_ElevatorSystemText.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("LogOut 하시겠습니까?", "LogOut", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            lbl_UserNumberImage.Visible = false;
                            lbl_CallSystemImage.Visible = false;
                            lbl_ElevatorSystemImage.Visible = false;

                            lbl_UserNumberText.Visible = false;
                            lbl_CallSystemText.Visible = false;
                            lbl_ElevatorSystemText.Visible = false;

                            ConfigData.UserNumber = null;
                            ConfigData.UserName = null;
                            ConfigData.UserElevatorAuthority = 0;
                            ConfigData.UserCallAuthority = 0;
                        }
                    }
                    break;
            }


        }

        private void AccordionControl1_ElementClick(object sender, ElementClickEventArgs e)
        {
            //if (e.Element.Style == ElementStyle.Group) return;
            //if (e.Element.Tag == null) return;

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

        private void AlarmMsgTimer_Tick(object sender, EventArgs e)
        {
            while (this.AlarmMessageQueue.Count > 0)
            {
                if (this.AlarmMessageQueue.TryDequeue(out string msg))
                {
                    AlertInfo info = new AlertInfo($"({DateTime.Now})", msg);

                    AlarmPupUpcontrol.Show(this, info);
                }

            }
        }

    }
}
