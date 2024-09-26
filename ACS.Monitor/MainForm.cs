using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
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

        private static string AmkorImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "Amkor.png");
        Image AmkorImage = Image.FromFile(AmkorImagePath);

        private static string AmkorIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "Monitoring_Icon.ico");
        Image AmkorIcon = Image.FromFile(AmkorIconPath);

        private Color skinColor = Color.FromArgb(43, 52, 59);
        private Color backColor = Color.FromArgb(30, 39, 46);
        private Color mouseOverColor = Color.FromArgb(45, 65, 77);
        private Color mouseOverTextColor = Color.FromArgb(57, 173, 233);
        private Color nomalTextColor = Color.FromArgb(167, 168, 169);

        private readonly UnitOfWork uow;
        private AutoScreen ChildMainForm;
        private ElevatorSystem elevator = null;
        private CallSystem CallSystem = null;
        private GetDataControl getDataControl = null;

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
            getDataControl = new GetDataControl(this, uow);

        }
        private void Start()
        {
            getDataControl.Start();
        }

        private void FormInit()
        {
            this.MinimizeBox = false;  // 최소화 버튼 비활성
            this.MaximizeBox = true;  // 최대화 버튼 비활성화

            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.LookAndFeel.SkinMaskColor = skinColor;
            this.LookAndFeel.SkinMaskColor2 = skinColor;
            this.BackColor = backColor;
            this.ForeColor = Color.White;
            this.IconOptions.Image = AmkorIcon;

            fluentDesignFormContainer1.BackColor = backColor;
            fluentDesignFormContainer1.ForeColor = Color.White;

            panelControl1.Visible = true;
            panelControl1.BackColor = skinColor;
            panelControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            pictureEdit1.BackColor = skinColor;
            pictureEdit1.Image = AmkorImage;

            lbl_Login.Cursor = Cursors.Hand;
            lbl_Login.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_Login.Click += Lbl_Login_Click;
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
            accordionControlMainView.Click += AccordionControl_Item_Click;
            accordionControlErrorLog.Click += AccordionControl_Item_Click;
            accordionControlJobLog.Click += AccordionControl_Item_Click;
            accordionControlCall.Click += AccordionControl_Item_Click;
            accordionControlElevator.Click += AccordionControl_Item_Click;
        }

        private void AccordionControl_Item_Click(object sender, EventArgs e)
        {
            string itemName = ((AccordionControlElement)sender).Name;

            switch (itemName)
            {
                case "accordionControlMainView":

                    break;
                case "accordionControlErrorLog":
                    break;
                case "accordionControlJobLog":
                    break;
                case "accordionControlCall":
                    if (dockPanelCall.Visibility != DevExpress.XtraBars.Docking.DockVisibility.Visible)
                    {
                        dockPanelCall.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                        ConfigData.CallScreenActive = true;
                        var CallSystem = new CallSystem(this, uow);
                        CallSystem.TopLevel = false;
                        CallSystem.Dock = DockStyle.Fill;
                        dockPanelCall.BackColor = Color.White;
                        dockPanelCall.Controls.Add(CallSystem);
                        CallSystem.Activate();
                        CallSystem.Show();
                    }
                    break;
                case "accordionControlElevator":
                    break;
                case "Setting":
                    break;

            }
        }

        private void InitDockPanel()
        {
            //dockManager1 = true;
            //dockPanel 사이즈 조절 안됨!!
            dockPanelCall.Text = "Call";
            dockPanelCall.Options.AllowDockAsTabbedDocument = false;  // 문서 탭 형태가 아닌 도킹
            dockPanelCall.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            dockPanelCall.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            dockPanelCall.ClosingPanel += DockPanelCall_ClosingPanel;
        }

        private void DockPanelCall_ClosingPanel(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            ConfigData.CallScreenActive = false;
           
        }


        /// <summary>
        /// AutoScreen을 MainForm으로 Dock하는 Func
        /// </summary>
        private void ChildMainFormFunc()
        {
            ChildMainForm = new AutoScreen(this, uow);
            ChildMainForm.TopLevel = false;
            ChildMainForm.Dock = DockStyle.Fill;
            fluentDesignFormContainer1.BackColor = Color.White;
            fluentDesignFormContainer1.Controls.Add(ChildMainForm);
            ChildMainForm.Activate();
            ChildMainForm.Show();
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

        private void Lbl_Login_Click(object sender, EventArgs e)
        {
            //사번 입력 Form을 불러옴
            var UserNumberForm = new UserNumberForm();
            DialogResult result = UserNumberForm.ShowDialog();
            if (result == DialogResult.Yes)
            {
                //사번 입력하여 검색
                var UserNumber = uow.UserNumbers.DBGetAll().FirstOrDefault(u => u.UserNumber == UserNumberForm.UserNumber);
                //사번이 없는경우 설정창을 Clear한후 다시 초기설정함
                if (UserNumber == null) MessageBox.Show("등록되지 않은 사원번호 이거나 비밀번호가 틀립니다!");
                else
                {
                    ConfigData.UserNumber = UserNumber.UserNumber;
                    ConfigData.UserName = UserNumber.UserName;
                    ConfigData.UserElevatorAuthority = UserNumber.ElevatorAuthority;
                    ConfigData.UserCallAuthority = UserNumber.CallMissionAuthority;

                    string LableText = $"사원번호 = {ConfigData.UserNumber} / 사원이름 = {ConfigData.UserName}";

                    if (ConfigData.UserElevatorAuthority == 1)
                    {
                        LableText += " / ElevatorSystem 사용가능";
                    }
                    if (ConfigData.UserCallAuthority == 1)
                    {
                        LableText += " / CallSystem 사용가능";
                    }
                }
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

    }
}
