using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ACS.Monitor
{
    public partial class SettingsCallMissions : Form
    {
        private readonly MainForm mainForm;
        private readonly IUnitOfWork uow;
        private DataTable GridDT = new DataTable();
        private UserNumberInfo S_UserNumber;
        private GridView grid = new GridView();

        public SettingsCallMissions(MainForm mainForm, IUnitOfWork uow, UserNumberInfo UserNumber)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            this.uow = uow;
            S_UserNumber = UserNumber;

            this.BackColor = Color.White;
            //this.FormBorderStyle = FormBorderStyle.None; // 테두리 제거
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.WindowState = FormWindowState.Normal; // 전체 화면 설정
            this.Text = "Call System";
            

            subFuncCallSystemGridInit();
            Status_Display();
        }

        private void subFuncCallSystemGridInit()
        {
            try
            {
                GridDT.Columns.AddRange(new DataColumn[] {
                    new DataColumn("DGV_UserNumber"),
                    new DataColumn("DGV_UserName"),
                    new DataColumn("DGV_CallName"),
                    new DataColumn("DGV_CallAllName"),
                    new DataColumn("DGV_CallStatus"),
                    new DataColumn("DGV_CallBtn")
                });

                GridDT.Columns[0].Caption = "사번";
                GridDT.Columns[1].Caption = "이름";
                GridDT.Columns[2].Caption = "미션";
                GridDT.Columns[3].Caption = "미션전체이름";
                GridDT.Columns[4].Caption = "미션상태";
                GridDT.Columns[5].Caption = "Call버튼";

                //Dtg_CallSystem.CellClick += Dtg_CallSystem_CellClick;
            }
            catch (Exception ex)
            {
                mainForm.EventLog("SettingsCallMissions", "subFuncCallSystemGridInit() Fail = " + ex);
                mainForm.ACS_UI_Log("SettingsCallMissions" + "/" + "subFuncCallSystemGridInit() Fail = " + ex);
            }
        }

        private void Status_Display()
        {
            uow.JobConfigRepository.Load();

            var Jobconfig = uow.JobConfigRepository.GetAll();
            var Jobs = Jobconfig.FindAll(x => x.ACSMissionGroup == "TAMB").Where(x => x.CallName.Split('_')[0].Contains("Wait"));

            Dtg_CallSystem.RepositoryItems.Clear();
            GridDT.Rows.Clear();

            int GridCount = 0;

            RepositoryItemButtonEdit CallItem = new RepositoryItemButtonEdit();
            CallItem.Buttons.Clear();

            foreach (var item in Jobs)
            {
                DataRow row = GridDT.NewRow();

                row["DGV_UserNumber"] = S_UserNumber.UserNumber?.ToString() ?? "";
                row["DGV_UserName"] = S_UserNumber.UserName?.ToString() ?? "";

                string str = item.CallName.Split('_')[1];

                if (str.Contains("Point1"))
                    str = "T3F";
                else if (str.Contains("Point2"))
                    str = "SLT";
                else if (str.Contains("Point3"))
                    str = "ATE";

                row["DGV_CallName"] = str.ToString();
                row["DGV_CallAllName"] = item.CallName;

                var Status = uow.Robots.GetAll().FirstOrDefault(x => x.ACSRobotGroup == "TAMB" && x.ACSRobotActive == true && x.JobId != 0);

                if (Status == null)
                {
                    row["DGV_CallStatus"] = "콜가능".ToString();
                    //row["DGV_CallStatus"].Style.BackColor = Color.LightBlue;
                }
                else
                {
                    var RobotJobs = uow.Jobs.GetAll().FirstOrDefault(x => x.ACSJobGroup == "TAMB" && x.RobotName == Status.RobotName).CallName;

                    if (RobotJobs == item.CallName)
                    {
                        row["DGV_CallStatus"] = "미션수행중".ToString();
                        //row.Cells["DGV_CallStatus"].Style.BackColor = Color.Chartreuse;
                    }
                    else
                    {
                        row["DGV_CallStatus"] = "콜불가능".ToString();
                        //row.Cells["DGV_CallStatus"].Style.BackColor = Color.OrangeRed;
                    }
                }

                CallItem = new RepositoryItemButtonEdit();
                CallItem.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
                CallItem.Buttons.Clear();
                CallItem.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Right));
                CallItem.ButtonClick += CallItem_ButtonClick;
                Dtg_CallSystem.RepositoryItems.Add(CallItem);
                row["DGV_CallBtn"] = "Call".ToString();

                GridDT.Rows.InsertAt(row, GridCount);
                GridCount++;
            }

            Dtg_CallSystem.DataSource = GridDT;

            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.ShowGroupPanel = false;

            gridView1.Columns["DGV_CallAllName"].Visible = false;

            gridView1.Columns["DGV_CallBtn"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            gridView1.Columns["DGV_CallBtn"].ColumnEdit = CallItem;

            grid = gridView1;
        }

        private void CallItem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                var rowHandle = ((ColumnView)((GridControl)((Control)sender).Parent).MainView).FocusedRowHandle;

                CallFunc(grid, rowHandle);
            }
            catch (Exception ex)
            {
                mainForm.EventLog("SettingsCallMissions", "CallItem_ButtonClick() Fail = " + ex);
                mainForm.ACS_UI_Log("SettingsCallMissions" + "/" + "CallItem_ButtonClick() Fail = " + ex);
            }
        }

        private void DeleteFunc()
        {
            MissionsSpecific missionsSpecific = new MissionsSpecific();
            missionsSpecific.ACSState = true;

            uow.MissionsSpecificRepository.Remove(missionsSpecific);
        }

        private void AddFunc(GridView dataGrid, int rowHandle)
        {
            var Robots = uow.Robots.GetAll();
            var Robot = Robots.FirstOrDefault(x => x.ACSRobotGroup == "TAMB" && x.JobId == 0);

            if (Robot != null)
            {
                string CallAllName = dataGrid.GetRowCellDisplayText(rowHandle, dataGrid.Columns["DGV_CallAllName"]);

                MissionsSpecific missionsSpecific = new MissionsSpecific();
                missionsSpecific.RobotGroup = Robot.ACSRobotGroup;
                missionsSpecific.RobotName = Robot.RobotName;
                missionsSpecific.CallName = CallAllName;
                missionsSpecific.CallState = true;
                missionsSpecific.ACSState = false;

                uow.MissionsSpecificRepository.Add(missionsSpecific);
            }
            else
                MessageBox.Show("현재 차량이 운행중입니다.");


        }

        private void CallFunc(GridView dataGrid, int rowHandle)
        {
            DeleteFunc();
            AddFunc(dataGrid, rowHandle);
        }

        private void SettingsCallMissions_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.mainForm.flyoutPanel2.HidePopup();
        }
    }
}