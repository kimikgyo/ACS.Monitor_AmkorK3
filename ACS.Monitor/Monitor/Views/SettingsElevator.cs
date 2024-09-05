using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using log4net;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ACS.Monitor
{
    public partial class SettingsElevator : Form
    {
        private readonly static ILog EventLogger = LogManager.GetLogger("Event"); //Function 실행관련 Log
        private readonly static ILog UserLogger = LogManager.GetLogger("User"); //버튼 및 화면조작관련 Log
        private readonly MainForm mainForm;
        private readonly IUnitOfWork uow;
        private DataTable GridDT = new DataTable();

        public SettingsElevator(MainForm mainForm, IUnitOfWork uow)
        {
            InitializeComponent();

            this.BackColor = Color.White;
            //this.FormBorderStyle = FormBorderStyle.None; // 테두리 제거
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.WindowState = FormWindowState.Normal; // 전체 화면 설정\
            this.Text = "Elevator";

            this.mainForm = mainForm;
            this.uow = uow;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            GridInit();
            Status_Display();

            btn_ElvAuto.Text = "Elevator" + "\r\n" + "AGV ON";
            btn_ElvManual.Text = "Elevator" + "\r\n" + "AGV OFF";
        }

        private void GridInit()
        {
            try
            {
                GridDT.Columns.AddRange(new DataColumn[] {
                    new DataColumn("No"),
                    new DataColumn("Location"),
                    new DataColumn("ACSMode"),
                    new DataColumn("ElevatorMode"),
                    new DataColumn("ElevatorNumber"),
                    new DataColumn("ElevatorStatus"),
                    new DataColumn("ElevatorBtnOnOFF")
                });

                GridDT.Columns[0].Caption = "번호";
                GridDT.Columns[1].Caption = "Location";
                GridDT.Columns[2].Caption = "ACSMode";
                GridDT.Columns[3].Caption = "ElevatorMode";
                GridDT.Columns[4].Caption = "층수";
                GridDT.Columns[5].Caption = "상태";
                GridDT.Columns[6].Caption = "ON/OFF";

            }
            catch (Exception ex)
            {
                mainForm.EventLog("SettingsElevator", "GridInit() Fail = " + ex);
                mainForm.ACS_UI_Log("SettingsElevator" + "/" + "GridInit() Fail = " + ex);
            }
        }

        private void Status_Display()
        {
            DG_View.RepositoryItems.Clear();
            GridDT.Rows.Clear();

            RepositoryItemToggleSwitch ritem = new RepositoryItemToggleSwitch();
            int GridCount = 0;

            foreach (var elevatorinfo in uow.ElevatorInfo.GetAll())
            {
                DataRow newRow = GridDT.NewRow();

                newRow["No"] = elevatorinfo.Id;
                newRow["Location"] = elevatorinfo.Location;
                newRow["ACSMode"] = elevatorinfo.ACSMode;
                newRow["ElevatorMode"] = elevatorinfo.ElevatorMode;
                newRow["ElevatorNumber"] = elevatorinfo.FloorIndex;
                newRow["ElevatorStatus"] = elevatorinfo.TransportMode;

                ritem = new RepositoryItemToggleSwitch();
                ritem.ValueOn = "ON";
                ritem.ValueOff = "OFF";
                DG_View.RepositoryItems.Add(ritem);

                newRow["ElevatorBtnOnOFF"] = elevatorinfo.TransportMode;

                GridDT.Rows.InsertAt(newRow, GridCount);
                GridCount++;
            }

            DG_View.DataSource = GridDT;

            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.ShowGroupPanel = false;

            gridView1.Columns["No"].Visible = false;
            gridView1.Columns["Location"].Visible = false;
            gridView1.Columns["ACSMode"].Visible = false;
            gridView1.Columns["ElevatorMode"].Visible = false;
            gridView1.Columns["ElevatorStatus"].Visible = false;

            gridView1.Columns["ElevatorBtnOnOFF"].ColumnEdit = ritem;
        }

        private void btn_ElvAuto_Click(object sender, EventArgs e)
        {
            // 사용자 사번을 확인한다
            //var UserNumberForm = new UserNumberForm();
            //DialogResult result = UserNumberForm.ShowDialog();

            //if (result == DialogResult.Yes)
            {
                var UserNumber = UserNumberInfo.GetUserLogin(this.mainForm.S_UserNumber.UserNumber, this.mainForm.S_UserNumber.UserPassword);
                if (UserNumber != null)
                {
                    LogElvatorMode(UserNumber.UserNumber, MiRControlMode.MiRContorlMode);
                    ElevatorInfoModel infoModel = new ElevatorInfoModel();
                    infoModel.ACSMode = "MiRControlMode";
                    infoModel.Location = "Elevator1";
                    uow.ElevatorInfo.ACSModeUpdate(infoModel);// set elevator to uncontrol_mode (AGV MODE ON)

                    string AlertMessage = "사번 : " + UserNumber.UserNumber + "사원 : " + UserNumber.UserName + "이(가) 전체 엘레베이터를 ON 하였습니다.";
                    AlertInfo info = new AlertInfo("Elevator", AlertMessage);
                    AlertControl control = new AlertControl();
                    control.FormLoad += Control_FormLoad;
                    control.Show(this, info);
                    
                    //DB 이력 남기기
                    //UserChangeModeLog modeLog = new UserChangeModeLog();
                    //modeLog.CreateTime = DateTime.Now;
                    //modeLog.UserName = UserNumber.UserName;
                    //modeLog.UserNumber = UserNumber.UserNumber;
                    //modeLog.ChangeModeLog = string.Format("엘리베이터 AGV mode ON 했습니다.");

                    //UserChangeModeLog.Add_UserElvChangeModeLog(modeLog);

                    UserLogger.Info($"{UserNumber.UserNumber} / MiRContorlMode Click ");
                }
                else
                {
                    MessageBox.Show("등록되지 않은 사원번호 이거나 비밀번호가 틀립니다!");
                }
            }
        }

        private void Control_FormLoad(object sender, AlertFormLoadEventArgs e)
        {
            e.Buttons.PinButton.SetDown(true);
        }

        public void LogElvatorMode(string UserNumber, MiRControlMode mode)
        {
            try
            {
                var elvModeInfo = ACSModeInfos.GetElevatorMode();
                if (elvModeInfo != null)
                {
                    if (elvModeInfo.ACSMode == "MiRUnContorlMode" && mode == MiRControlMode.MiRContorlMode)
                    {
                        var eleChangeLog = new UserChangeModeLog
                        {
                            UserNumber = UserNumber,
                            ChangeModeLog = "MiRContorlMode변경요청",
                            CreateTime = DateTime.Now
                        };
                        UserChangeModeLog.Add_UserElvChangeModeLog(eleChangeLog);
                    }
                    else if (elvModeInfo.ACSMode == "MiRContorlMode" && mode == MiRControlMode.MiRUnContorlMode)
                    {
                        var eleChangeLog = new UserChangeModeLog
                        {
                            UserNumber = UserNumber,
                            ChangeModeLog = "MiRUnContorlMode변경요청",
                            CreateTime = DateTime.Now
                        };
                        UserChangeModeLog.Add_UserElvChangeModeLog(eleChangeLog);
                    }
                }
            }
            catch (Exception ex)
            {
                EventLogger.Info("MainForm/UpdateElevatorMode() Fail = " + ex);
            }
        }

        private void btn_ElvManual_Click(object sender, EventArgs e)
        {
            // 사용자 사번을 확인한다
            //var UserNumberForm = new UserNumberForm();
            //UserNumberForm.ShowDialog();

            //if (UserNumberForm.Update_index == 1)
            {
                var UserNumber = UserNumberInfo.GetUserLogin(this.mainForm.S_UserNumber.UserNumber, this.mainForm.S_UserNumber.UserPassword);
                if (UserNumber != null)
                {
                    LogElvatorMode(UserNumber.UserNumber, MiRControlMode.MiRUnContorlMode);

                    ElevatorInfoModel infoModel = new ElevatorInfoModel();
                    infoModel.ACSMode = "MiRUnControlMode";
                    infoModel.Location = "Elevator1";
                    uow.ElevatorInfo.ACSModeUpdate(infoModel);// set elevator to uncontrol_mode (AGV MODE OFF)
                    //ACSModeInfos.SetElevatorMode(MiRControlMode.MiRUnContorlMode);        

                    string AlertMessage = "사번 : " + UserNumber.UserNumber + "사원 : " + UserNumber.UserName + "이(가) 전체 엘레베이터를 OFF 하였습니다.";
                    AlertInfo info = new AlertInfo("Elevator", AlertMessage);
                    AlertControl control = new AlertControl();
                    control.FormLoad += Control_FormLoad;
                    control.Show(this, info);
                    
                    AppConfiguration.SetAppConfig("SetUserNumber", UserNumber.UserNumber);

                    //DB 이력 남기기
                    //UserChangeModeLog modeLog = new UserChangeModeLog();
                    //modeLog.CreateTime = DateTime.Now;
                    //modeLog.UserName = UserNumber.UserName;
                    //modeLog.UserNumber = UserNumber.UserNumber;
                    //modeLog.ChangeModeLog = string.Format("엘리베이터 AGV mode OFF 했습니다.");

                    //UserChangeModeLog.Add_UserElvChangeModeLog(modeLog);

                    UserLogger.Info($"{UserNumber.UserNumber} / MiRUnContorlMode Click ");

                }
                else
                {
                    MessageBox.Show("등록되지 않은 사원번호 이거나 비밀번호가 틀립니다!");
                }
            }
            //UserNumberForm.Close();
        }

        private void T_1Sec_Tick(object sender, EventArgs e)
        {
            T_1Sec.Enabled = false;
            DisplayElevatorMode();
            T_1Sec.Enabled = true;
        }

        private void DisplayElevatorMode()
        {
            var elvModeInfo = uow.ElevatorInfo.GetAll().FirstOrDefault(x => x.Location.StartsWith("Elevator"));
            if (elvModeInfo != null)
            {
                if (elvModeInfo.ACSMode == "MiRControlMode")
                {
                    btn_ElvAuto.BackColor = Color.Chartreuse;
                    btn_ElvManual.BackColor = Color.LightGray;
                }
                else if (elvModeInfo.ACSMode == "MiRUnControlMode")
                {
                    btn_ElvAuto.BackColor = Color.LightGray;
                    btn_ElvManual.BackColor = Color.Chartreuse;
                }
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                //사번 입력 창
                //var UserNumberForm = new UserNumberForm();
                //UserNumberForm.ShowDialog();

                //if (UserNumberForm.Update_index == 1)
                {
                    var UserNumber = UserNumberInfo.GetUserLogin(this.mainForm.S_UserNumber.UserNumber, this.mainForm.S_UserNumber.UserPassword);
                    if (UserNumber != null)
                    {
                        //버튼 ON/OFF 따로 입력
                        string TextCell = view.GetRowCellDisplayText(e.RowHandle, view.Columns["ElevatorBtnOnOFF"]);

                        if (e.Column == view.Columns["ElevatorBtnOnOFF"] && TextCell == "OFF")
                        {
                            ElevatorInfoModel elevatorInfo = new ElevatorInfoModel();
                            elevatorInfo.Id = Convert.ToInt32(view.GetRowCellDisplayText(e.RowHandle, view.Columns["No"]));
                            elevatorInfo.Location = view.GetRowCellDisplayText(e.RowHandle, view.Columns["Location"]).ToString();
                            elevatorInfo.FloorIndex = view.GetRowCellDisplayText(e.RowHandle, view.Columns["ElevatorNumber"]).ToString();
                            elevatorInfo.TransportMode = "ON";

                            uow.ElevatorInfo.Update(elevatorInfo);

                            //DB 이력 남기기
                            ElevatorInfoModel elevatorInfo2 = new ElevatorInfoModel();
                            elevatorInfo2.Id = Convert.ToInt32(view.GetRowCellDisplayText(e.RowHandle, view.Columns["No"]));
                            elevatorInfo2.UserNumber = UserNumber.UserNumber;

                            uow.ElevatorInfo.ElevatorUserNumber(elevatorInfo2);

                            Status_Display();

                            string AlertMessage = "사번 : " + UserNumber.UserNumber + "사원 : " + UserNumber.UserName + "이(가) " +
                                                   view.GetRowCellDisplayText(e.RowHandle, view.Columns["ElevatorNumber"]).ToString() + "층 엘리베이터를 ON 하였습니다.";
                            AlertInfo info = new AlertInfo("Elevator", AlertMessage);
                            AlertControl control = new AlertControl();
                            control.FormLoad += Control_FormLoad;
                            control.Show(this, info);
                        }
                        else if (e.Column == view.Columns["ElevatorBtnOnOFF"] && TextCell == "ON")
                        {
                            ElevatorInfoModel elevatorInfo = new ElevatorInfoModel();
                            elevatorInfo.Id = Convert.ToInt32(view.GetRowCellDisplayText(e.RowHandle, view.Columns["No"]));
                            elevatorInfo.Location = view.GetRowCellDisplayText(e.RowHandle, view.Columns["Location"]).ToString();
                            elevatorInfo.FloorIndex = view.GetRowCellDisplayText(e.RowHandle, view.Columns["ElevatorNumber"]).ToString();
                            elevatorInfo.TransportMode = "OFF";

                            uow.ElevatorInfo.Update(elevatorInfo);

                            //DB 이력 남기기
                            ElevatorInfoModel elevatorInfo2 = new ElevatorInfoModel();
                            elevatorInfo2.Id = Convert.ToInt32(view.GetRowCellDisplayText(e.RowHandle, view.Columns["No"]));
                            elevatorInfo2.UserNumber = UserNumber.UserNumber;

                            uow.ElevatorInfo.ElevatorUserNumber(elevatorInfo2);

                            Status_Display();

                            string AlertMessage = "사번 : " + UserNumber.UserNumber + "사원 : " + UserNumber.UserName + "이(가) " +
                                                   view.GetRowCellDisplayText(e.RowHandle, view.Columns["ElevatorNumber"]).ToString() + "층 엘리베이터를 OFF 하였습니다.";
                            AlertInfo info = new AlertInfo("Elevator", AlertMessage);
                            AlertControl control = new AlertControl();
                            control.FormLoad += Control_FormLoad;
                            control.Show(this, info);
                        }
                    }
                    else
                    {
                        MessageBox.Show("등록되지 않은 사원번호 이거나 비밀번호가 틀립니다!");
                    }
                }
            }
            catch (Exception ex)
            {
                mainForm.EventLog("SettingsElevator", "DG_View_CellClick() Fail = " + ex);
                mainForm.ACS_UI_Log("SettingsElevator" + "/" + "DG_View_CellClick() Fail = " + ex);
            }
        }

        private void SettingsElevator_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.mainForm.flyoutPanel1.HidePopup();
        }

        private void SettingsElevator_Activated(object sender, EventArgs e)
        {
            T_1Sec.Enabled = true;
        }
    }
}
