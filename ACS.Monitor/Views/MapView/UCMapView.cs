
using ACS.Monitor.Properties;
using ACS.Monitor.Utilities;
using DevExpress.XtraEditors;
using log4net;
using Monitor.Common;
using Monitor.Data;
using Monitor.Map;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACS.Monitor
{
    public partial class UCMapView : XtraUserControl
    {
        private readonly static ILog EventLogger = LogManager.GetLogger("Event"); //Function 실행관련 Log
        private readonly static object lockObj = new object();

        public string MapID { get; set; }
        public string UriStr { get; set; }

        // map 관련 변수
        private FleetMapProcessor mapProcessor = null;
        private FleetMap map = null;
        private List<int> robotIdList = null;
        private List<FleetRobot> robotsInfo = null;
        private Image backgroundImage = null;
        private float mapScale = 1.0f;
        private string mapName = null;
        private string mapGuid = null;
        // mouse 관련 변수
        private Point mouseFirstLocation = Point.Empty;
        private Point mouseMoveOffset = Point.Empty;

        // custom map 관련 변수
        private bool customMapMode = false;      // map mode (0=fleet / 1=custom)
        private bool customMapUpdate = true;    // db map image를 다시 로드해야 할때 true. (기동시에는 무조건 로드해야하므로 true)
        private bool ACSDbMapMode = true;      // map mode (0=fleet / 1=FloorMapId DateBase)
        private bool ACSDbMapUpdate = true;     // db map image를 다시 로드해야 할때 true. (기동시에는 무조건 로드해야하므로 true)
        private bool FleetMapMode = false;      // map mode (0=fleet / 1=FloorMapId DateBase)
        private bool FleetMapUpdate = true;     // db map image를 다시 로드해야 할때 true. (기동시에는 무조건 로드해야하므로 true)
        private Image DbMapImage = null;        // db map image
        private DateTime oldDbUpdatedTime = default; // keep map UpdateDime
        private readonly Bitmap blankBitmap = new Bitmap(2000, 2000);
        private Color skinColor = Color.FromArgb(43, 52, 59);
        private Color backColor = Color.FromArgb(30, 39, 46);
        private Color mouseOverColor = Color.FromArgb(45, 65, 77);
        private Color mouseOverTextColor = Color.FromArgb(57, 173, 233);
        private Color nomalTextColor = Color.FromArgb(167, 168, 169);
        public UCMapView()
        {
            InitializeComponent();
            this.BackColor = backColor;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Form parentForm = Helpers.GetParentForm(this);
            if (parentForm != null) parentForm.FormClosed += ParentForm_FormClosed;
        }

        private void ParentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //AppConfiguration.SetAppConfig($"MapScale_{mapName}", this.mapScale.ToString());
            //AppConfiguration.SetAppConfig($"MapLocation_{mapName}", $"{this.mouseFirstLocation.X},{this.mouseFirstLocation.Y}");
            //AppConfiguration.SetAppConfig($"MapOffset_{mapName}", $"{this.mouseMoveOffset.X},{this.mouseMoveOffset.Y}");
            AppConfiguration.SetAppConfig($"MapScale_{mapName}", JsonConvert.SerializeObject(this.mapScale));
            AppConfiguration.SetAppConfig($"MapLocation_{mapName}", JsonConvert.SerializeObject(this.mouseFirstLocation));
            AppConfiguration.SetAppConfig($"MapOffset_{mapName}", JsonConvert.SerializeObject(this.mouseMoveOffset));
        }

        public void Init(FloorMapIdConfigModel floorMapIdConfig, string fleetIp, bool Flag)
        {
            // init map handler
            var logger = LogManager.GetLogger("Event");

            try
            {
                if (Flag)
                {
                    floorMapIdConfig.MapData.mapScale = JsonConvert.DeserializeObject<float>(ConfigurationManager.AppSettings[$"MapScale_{floorMapIdConfig.FloorName}"]);
                    floorMapIdConfig.MapData.mouseFirstLocation = JsonConvert.DeserializeObject<Point>(ConfigurationManager.AppSettings[$"MapLocation_{floorMapIdConfig.FloorName}"]);
                    floorMapIdConfig.MapData.mouseMoveOffset = JsonConvert.DeserializeObject<Point>(ConfigurationManager.AppSettings[$"MapOffset_{floorMapIdConfig.FloorName}"]);

                }
            }
            catch
            {

            }

            this.backColor = Color.Black;
            this.Map_ID.Text = floorMapIdConfig.FloorName;
            this.mapName = floorMapIdConfig.FloorName;
            this.mapScale = floorMapIdConfig.MapData.mapScale;
            this.mouseFirstLocation = floorMapIdConfig.MapData.mouseFirstLocation;
            this.mouseMoveOffset = floorMapIdConfig.MapData.mouseMoveOffset;
            this.mapGuid = floorMapIdConfig.MapID;

            mapProcessor = new FleetMapProcessor(logger, UriStr, mapName);

            this.pictureBox1.MouseClick += PictureBox1_MouseClick;
            this.pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            this.pictureBox1.MouseDown += PictureBox1_MouseDown;
            this.pictureBox1.MouseMove += PictureBox1_MouseMove;
            this.pictureBox1.Resize += PictureBox1_Resize;

            if (backgroundImage == null) PictureBox1_Resize(this, null);
            this.pictureBox1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch; // 또는 다른 모드;// 변경 금지!!
            this.pictureBox1.Dock = DockStyle.Fill;
            this.pictureBox1.BackColor = backColor;
            this.pictureBox1.Visible = true;
            this.Map_ID.BackColor = Color.Black;
            this.Map_ID.ForeColor = Color.White;
            this.Map_ID.Anchor = AnchorStyles.Left | AnchorStyles.Top;



            // FleetMap
            chkFleetMap.CheckState = FleetMapMode ? CheckState.Checked : CheckState.Unchecked;
            chkFleetMap.Click += (s, e) =>
            {
                FleetMapMode = !chkFleetMap.Checked;
                FleetMapUpdate = !chkFleetMap.Checked;
            };
            // custom map 체크박스 설정
            chkCustomMap.CheckState = customMapMode ? CheckState.Checked : CheckState.Unchecked;
            chkCustomMap.Click += (s, e) =>
            {
                customMapMode = !chkCustomMap.Checked;
                customMapUpdate = !chkCustomMap.Checked;

            };
            //ACSMap 체크박스 설정
            chkACSDbMap.CheckState = ACSDbMapMode ? CheckState.Checked : CheckState.Unchecked;
            chkACSDbMap.Click += (s, e) =>
            {
                ACSDbMapMode = !chkACSDbMap.Checked;
                ACSDbMapUpdate = !chkACSDbMap.Checked;
            };
            if (FleetMapMode) { customMapMode = false; ACSDbMapMode = false; }
            if (customMapMode) { ACSDbMapMode = false; FleetMapMode = false; }
            if (ACSDbMapMode) { customMapMode = false; FleetMapMode = false; }

            // display info 체크박스 설정
            cb_DisplayInfo.Click += (s, e) => btnMapDownload.Visible = cb_DisplayInfo.Checked;

        }


        // 픽쳐박스 사이즈변경시 배경 이미지 크기 재설정
        private void PictureBox1_Resize(object sender, EventArgs e)
        {
            if (backgroundImage != null)
            {
                backgroundImage.Dispose();
                backgroundImage = null;
            }

            if (pictureBox1.ClientSize.Width <= 0 || pictureBox1.ClientSize.Height <= 0)
                return;

            // 배경 이미지 설정
            //backgroundImage = Image.FromFile("layout.png"); // 화일에서 불러온 이미지 사용
            //backgroundImage = (Image)pictureBox1.Image.Clone(); // 폼에 설정된 이미지 복제해서 사용
            backgroundImage = Resources._70_Auto; // 폼에 설정된 이미지 복제해서 사용

            // 배경 이미지 투명도 설정
            //((Bitmap)backgroundImage).MakeTransparent();
            backgroundImage = ImageUtils.ImageTransparency.ChangeOpacity(backgroundImage, 0.3f);

            // 배경 이미지 크기 조정
            backgroundImage = ImageUtils.ImageTransparency.ResizeImage(backgroundImage, pictureBox1.ClientSize);


        }



        private Task task1;
        private Task task2;

        private bool stopRequest = false;

        public void StopLoop()
        {
            stopRequest = true;
            if (task1 is Task && task2 is Task)
                Task.WaitAll(task1, task2);
        }

        public void StartLoop()
        {
            stopRequest = false;


            // 통신 스레드
            task1 = Task.Run(() =>
            {
                EventLogger.Info($"comm thread start! ({mapName})");

                while (!stopRequest)
                {
                    MapHandlerProc1();
                    RobotHanlerProc1();
                    Thread.Sleep(100);
                }

                EventLogger.Info($"comm thread stop! ({mapName})");
            });


            // 렌더링 스레드
            task2 = Task.Run(() =>
            {
                EventLogger.Info($"rendering thread start! ({mapName})");

                var pre_mouseFirstLocation = Point.Empty;
                var pre_mapScale = 0.0f;
                var sw = new Stopwatch();
                sw.Start();

                while (!stopRequest)
                {
                    // 변경전: 주기적으로 렌더링 처리
                    //MapHandlerProc2(null, null);
                    //Thread.Sleep(100);

                    // 변경후: 주기적으로 렌더링 처리, 맵조작시에는 바로 렌더링 처리
                    if (sw.ElapsedMilliseconds > 500 || pre_mouseFirstLocation != mouseFirstLocation || pre_mapScale != mapScale)
                    {
                        pre_mouseFirstLocation = mouseFirstLocation;
                        pre_mapScale = mapScale;
                        sw.Reset();
                        sw.Start();


                        MapHandlerProc2();
                    }
                    Thread.Sleep(100);
                }

                EventLogger.Info($"rendering thread stop! ({mapName})");
            });

        }




        private void MapHandlerProc1()
        {
            //Fleet
            if (FleetMapMode && FleetMapUpdate)
            {
                // get map data
                map = mapProcessor.GetMap(MapID);
                if (map == null) return;

                FleetMapUpdate = false;
            }

            //Custom
            else if (customMapMode)
            {
                lock (lockObj)
                {
                    if (customMapUpdate)
                    {
                        DbMapImage?.Dispose();
                        DbMapImage = null;
                        var MapData = ConfigData.CustomMaps.FirstOrDefault(x => x.MapName == mapName);
                        if (MapData != null)
                        {
                            string imageData = MapData.MapImageData;
                            DbMapImage = _GetImageFromDB(imageData);
                        }
                    }

                    else
                    {
                        var customMaps = ConfigData.CustomMaps.FirstOrDefault(x => x.MapName == mapName);
                        if (customMaps != null)
                        {
                            if (customMaps.UpdateTime != oldDbUpdatedTime)
                            {
                                customMapUpdate = false;
                                oldDbUpdatedTime = customMaps.UpdateTime;
                            }
                        }
                    }
                }
            }

            //DB
            if (ACSDbMapMode && ACSDbMapUpdate)
            {
                lock (lockObj)
                {
                    DbMapImage?.Dispose();
                    DbMapImage = null;
                    var MapData = ConfigData.FloorMapIdConfigs.FirstOrDefault(x => x.MapID == MapID);
                    if (MapData != null)
                    {
                        DbMapImage = _GetImageFromDB(MapData.MapImageData);
                        if (DbMapImage == null) return;
                        ACSDbMapUpdate = false;
                    }
                }
            }

        }

        private void RobotHanlerProc1()
        {
            //Fleet ================ robot 정보를 읽어와서 map 위에 표시한다
            //robotIdList = mapProcessor.GetRobotIdList(); // fleet 에 요청해서 구하기

            ////get robots info
            //if (robotIdList != null)
            //{
            //    robotsInfo = mapProcessor.GetRobots(robotIdList);
            //}
            //else
            //{
            //    robotsInfo = null;
            //}


            //DB ================ get robots info (로봇 전체[DataBase])
            robotsInfo = ConfigData.Robots.Where(r=>r.ACSRobotGroup == "AMB").Select(r => new FleetRobot()
            {
                RobotID = r.RobotID,
                RobotName = r.RobotName,
                MapID = r.MapID,
                PosX = r.Position_X,
                PosY = r.Position_Y,
                Orientation = r.Position_Orientation,
                DistanceToTarget = r.DistanceToNextTarget,
                BatteryPercent = r.BatteryPercent,
                MissionQueueID = Convert.ToString(r.MissionQueueID),
                MissionText = r.MissionText,
                StateID = Convert.ToString(r.StateID),
                StateText = r.StateText,
                IP = r.RobotIp,
                FleetState = Convert.ToString(r.Fleet_State),
                FleetStateText = r.Fleet_State_Text,
                RobotAlias = r.RobotAlias,  // <=== DB에만 존재하는 데이터
            }).ToList();


            // ================ filter robots (화면에 표시될 로봇만 필터링)
            if (robotsInfo != null && ConfigData.DisplayRobotNames != null)
            {
                var filteredRobots = new List<FleetRobot>();

                foreach (var robot in robotsInfo)
                {
                    if (ConfigData.DisplayRobotNames.ContainsKey(robot.RobotName) && robot.MapID == this.MapID)
                    {
                        filteredRobots.Add(robot);
                    }
                    //else
                    //{
                    //    EventLogger.Info($"Not Chacked {robot}");
                    //}
                }
                robotsInfo.Clear();
                robotsInfo = filteredRobots;
            }

            // ================ filter robots (mapid가 일치하는 로봇만 필터링)
            //if (robotsInfo != null)
            //{
            //    var filteredRobots = new List<FleetRobot>();
            //    foreach (var robot in robotsInfo)
            //    {
            //        if (robot.MapID == this.MapID)
            //        {
            //            filteredRobots.Add(robot);
            //        }
            //    }
            //    robotsInfo.Clear();
            //    robotsInfo = filteredRobots;
            //}
        }

        private void MapHandlerProc2()
        {
            if (robotsInfo == null) return;

            Bitmap renderImage = null;
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // 맵을 픽쳐박스사이즈로 스케일링하여 오프셋만큼 이동시켜 렌더링한 이미지 생성
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            lock (lockObj)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        button1.Visible = false;
                        button2.Visible = false;
                        button3.Visible = false;
                        btnMapDownload.Visible = false;
                        cb_DisplayInfo.Visible = false;
                        chkCustomMap.Visible = false;
                        chkACSDbMap.Visible = false;
                        chkFleetMap.Visible = false;
                        textBox1.Visible = false;
                        lbl_ClickPosInfo.Visible = false;
                    }));
                }

                if (ACSDbMapMode || customMapMode) renderImage = Make_RenderImage(DbMapImage);        // DB Image
                else if (FleetMapMode) renderImage = Make_RenderImage(map.Image);  // fleet image

            }

            if (renderImage == null) return;
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


            // ================ 최종 이미지를 ui 에 반영시킨다

            try
            {
                if (this.Disposing || this.IsDisposed) return;

                // 렌더링된 이미지 투명 설정
                //renderImage.MakeTransparent(Color.FromArgb(252, 254, 252)); // 맵 내부 투명하게
                renderImage.MakeTransparent(Color.Gray); // 맵 외부 투명하게

                //===================== 배경이미지 + 맵이미지
                if (true)
                {

                    //// 배경 이미지 설정 (변하지 않으므로 한번만 설정)
                    //if (backgroundImage == null)
                    //{
                    //    //backgroundImage = Image.FromFile("layout.png"); // 화일에서 불러온 이미지 사용
                    //    backgroundImage = (Image)pictureBox1.Image.Clone(); // 폼에 설정된 이미지 복제해서 사용

                    //    //((Bitmap)backgroundImage).MakeTransparent();
                    //    backgroundImage = ImageUtils.ImageTransparency.ChangeOpacity(backgroundImage, 0.3f);
                    //}

                    //// 배경 이미지 크기 조정
                    //if (backgroundImage != null && backgroundImage.Size != pictureBox1.Image.Size)
                    //{
                    //    Image resizedBackgroundImage = ImageUtils.ImageTransparency.ResizeImage(backgroundImage, pictureBox1.ClientSize);
                    //    backgroundImage.Dispose();
                    //    backgroundImage = null;
                    //    backgroundImage = resizedBackgroundImage;
                    //}


                    // 배경 이미지 위에 렌더링된 맵이미지를 오버레이한다
                    //Image img = new Bitmap(backgroundImage.Width, backgroundImage.Height);
                    Image img = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
                    using (Graphics g = Graphics.FromImage(img))
                    {
                        //g.DrawImage(backgroundImage, 0, 0);
                        g.DrawImage(renderImage, 0, 0);

                        //============================================= 로봇 정보 오버레이
                        if (cb_DisplayInfo.Checked)
                        {
                            using (Font textFont = new Font("Courier New", 10, FontStyle.Bold))
                            {
                                int y = 0;
                                Brush stateTextBrush;
                                foreach (var r in robotsInfo)
                                {
                                    y += 15; g.DrawString($"Robot     : {r.RobotName}", textFont, Brushes.Blue, 0, y);

                                    if (r.StateText.ToLower().Contains("abort")) stateTextBrush = Brushes.Red;
                                    else if (r.StateText.ToLower().Contains("pause")) stateTextBrush = Brushes.Violet;
                                    else if (r.StateText.ToLower().Contains("manual")) stateTextBrush = Brushes.Red;
                                    else if (r.StateText.ToLower().Contains("error")) stateTextBrush = Brushes.Red;
                                    else if (r.StateText.ToLower().Contains("emergency")) stateTextBrush = Brushes.Red;
                                    else if (r.FleetStateText.ToLower().Contains("unavailable")) stateTextBrush = Brushes.Red;
                                    else stateTextBrush = Brushes.Blue;

                                    y += 15; g.DrawString($"State     : {r.StateText}({r.StateID})", textFont, stateTextBrush, 0, y);
                                    y += 15; g.DrawString($"FleetState: {r.FleetStateText}({r.FleetState})", textFont, stateTextBrush, 0, y);
                                    y += 15; g.DrawString($"QueueID   : {r.MissionQueueID}", textFont, Brushes.Blue, 0, y);
                                    y += 15; g.DrawString($"Mission   : {r.MissionText}", textFont, Brushes.Blue, 0, y);
                                    y += 15; g.DrawString($"Battery   : {r.BatteryPercent:0.00}%", textFont, Brushes.Blue, 0, y);
                                    y += 30;
                                }
                            }
                        }
                        //=============================================
                    }

                    // UI 처리 (배경이미지 위에 맵 이미지를 오버레이한 이미지 표시)
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            this.pictureBox1.Image?.Dispose();
                            this.pictureBox1.Image = img;
                            this.pictureBox1.Refresh();
                        }));
                    }
                }
                //===================== 맵이미지
                else
                {
                    //UI 처리 (맵 이미지 그대로 표시)
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            this.pictureBox1.Image?.Dispose();
                            this.pictureBox1.Image = renderImage;
                            this.pictureBox1.Refresh();
                        }));
                    }
                }
            }
            catch (ObjectDisposedException) { }
            catch (Exception ex)
            {
                var msg = ex.GetFullMessage() + Environment.NewLine + ex.StackTrace;
                Debug.WriteLine(msg);
                EventLogger.Info(msg);
            }
        }


        private Image _GetImageFromDB(string imageData)
        {
            try
            {
                Image image = null;
                // 새로운 이미지 생성한다 (DB에서 가져온 이미지)
                byte[] mapDecodedBytes = Convert.FromBase64String(imageData);
                using (var ms = new MemoryStream(mapDecodedBytes))
                {
                    image = Image.FromStream(ms);
                }
                EventLogger.Info($"map image loaded! ({mapName})");
                return image;
            }
            catch (Exception ex) { EventLogger.Info($"_GetImageFromDB() Exception: MapName={mapName}     {ex}"); }
            return null;
        }

        private Bitmap Make_RenderImage(Image image)
        {
            Bitmap renderImage;

            // DB 이미지가 있을때 렌더링 이미지 생성
            if (image != null)
            {
                if (FleetMapMode) renderImage = mapProcessor.FleetGetRenderImage(pictureBox1.ClientSize, mouseMoveOffset, map, robotsInfo, mapScale, mapScale);
                else
                {
                    var FoorData = ConfigData.FloorMapIdConfigs.FirstOrDefault(f => f.MapID == MapID);
                    if (FoorData != null)
                        renderImage = mapProcessor.DBGetRenderImage(pictureBox1.ClientSize, mouseMoveOffset, image, ConfigData.FleetPositions, robotsInfo, FoorData, mapScale, mapScale);
                    else
                    {
                        renderImage = blankBitmap;
                        using (Graphics g = Graphics.FromImage(renderImage))
                        using (Font textFont = new Font("Courier New", 20))
                        {
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                            g.Clear(Color.Gray);
                            g.DrawString("NO IMAGE", textFont, Brushes.Red, 50, 50);
                        }
                    }
                }
            }
            // DB 이미지가 없을때는 빈 이미지 생성
            else
            {
                renderImage = blankBitmap;
                using (Graphics g = Graphics.FromImage(renderImage))
                using (Font textFont = new Font("Courier New", 20))
                {
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                    g.Clear(Color.Gray);
                    g.DrawString("NO IMAGE", textFont, Brushes.Red, 50, 50);
                }
            }

            return renderImage;
        }


        // 마우스 클릭으로 로봇 위치 확인하기
        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Image MapImage = null;
            if (robotsInfo == null) return;
            Point clickPoint = e.Location;
            PointF scaledClickPoint = new PointF();
            var sb = new StringBuilder();
            if (customMapMode)
            {
                var MapData = ConfigData.CustomMaps.FirstOrDefault(x => x.MapName == mapName);
                if (MapData != null) MapImage = _GetImageFromDB(MapData.MapImageData);
            }
            else if (ACSDbMapMode)
            {
                var MapData = ConfigData.FloorMapIdConfigs.FirstOrDefault(x => x.MapID == MapID);
                if (MapData != null) MapImage = _GetImageFromDB(MapData.MapImageData);
            }

            if (map != null)
            {
                scaledClickPoint = mapProcessor.GetScaledMapPoint(mouseMoveOffset, map, clickPoint, mapScale, mapScale);

                var posList = map.Positions.Where(p =>
                {
                    return
                        p.PosX > (scaledClickPoint.X - .6) &&
                        p.PosX < (scaledClickPoint.X + .6) &&
                        p.PosY > (scaledClickPoint.Y - .6) &&
                        p.PosY < (scaledClickPoint.Y + .6);
                });
                Console.WriteLine($"ClickPoint = {clickPoint} , scaledClickPointX = {scaledClickPoint.X} , scaledClickPointY = {scaledClickPoint.Y}");


                foreach (var pos in posList)
                {
                    Debug.WriteLine($"{clickPoint}  mapXY={scaledClickPoint}   type={pos.TypeID,-2}    pos={pos.Name}");
                    sb.AppendLine($"{clickPoint}  mapXY={scaledClickPoint}   type={pos.TypeID,-2}    pos={pos.Name}");
                }
                var robotList = robotsInfo.Where(r =>
                {
                    return
                        r.PosX > (scaledClickPoint.X - .6) &&
                        r.PosX < (scaledClickPoint.X + .6) &&
                        r.PosY > (scaledClickPoint.Y - .6) &&
                        r.PosY < (scaledClickPoint.Y + .6);
                });

                foreach (var robot in robotList)
                {
                    Debug.WriteLine($"{clickPoint}  mapXY={scaledClickPoint}  robot={robot.RobotName}");
                    sb.AppendLine($"{clickPoint}  mapXY={scaledClickPoint}  robot={robot.RobotName}");
                }

                if (posList.Count() == 0 && robotList.Count() == 0)
                {
                    Debug.WriteLine($"{clickPoint}  mapXY={scaledClickPoint}");
                    sb.AppendLine($"{clickPoint}  mapXY={scaledClickPoint}");
                }


            }
            else if (MapImage != null)
            {
                scaledClickPoint = mapProcessor.DBMapImageGetScaledMapPoint(mouseMoveOffset, MapImage, clickPoint, mapScale, mapScale);

                var posList = ConfigData.FleetPositions.Where(p =>
                {
                    return
                        p.PosX > (scaledClickPoint.X - .6) &&
                        p.PosX < (scaledClickPoint.X + .6) &&
                        p.PosY > (scaledClickPoint.Y - .6) &&
                        p.PosY < (scaledClickPoint.Y + .6);
                });
                Console.WriteLine($"ClickPoint = {clickPoint} , scaledClickPointX = {scaledClickPoint.X} , scaledClickPointY = {scaledClickPoint.Y}");


                foreach (var pos in posList)
                {
                    Debug.WriteLine($"{clickPoint}  mapXY={scaledClickPoint}   type={pos.TypeID,-2}    pos={pos.Name}");
                    sb.AppendLine($"{clickPoint}  mapXY={scaledClickPoint}   type={pos.TypeID,-2}    pos={pos.Name}");
                }

                var robotList = robotsInfo.Where(r =>
                {
                    return
                        r.PosX > (scaledClickPoint.X - .6) &&
                        r.PosX < (scaledClickPoint.X + .6) &&
                        r.PosY > (scaledClickPoint.Y - .6) &&
                        r.PosY < (scaledClickPoint.Y + .6);
                });

                foreach (var robot in robotList)
                {
                    Debug.WriteLine($"{clickPoint}  mapXY={scaledClickPoint}  robot={robot.RobotName}");
                    sb.AppendLine($"{clickPoint}  mapXY={scaledClickPoint}  robot={robot.RobotName}");
                }

                if (posList.Count() == 0 && robotList.Count() == 0)
                {
                    Debug.WriteLine($"{clickPoint}  mapXY={scaledClickPoint}");
                    sb.AppendLine($"{clickPoint}  mapXY={scaledClickPoint}");
                }
            }

            if (cb_DisplayInfo.Checked)
            {
                lbl_ClickPosInfo.Text = sb.ToString();
                Console.WriteLine(sb.ToString());
                lbl_ClickPosInfo.Visible = true;
            }
            else
            {
                lbl_ClickPosInfo.Visible = false;
            }
        }


        // 마우스 휠로 맵을 줌인/줌아웃하기
        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            // The amount by which we adjust scale per wheel click.
            //const float scale_per_delta = 0.1f / 120;
            //const float scale_per_delta = 0.1f / 240;
            const float scale_per_delta = 0.1f / 720;

            // Update the drawing based upon the mouse wheel scrolling.
            mapScale += e.Delta * scale_per_delta;

            if (mapScale < 0.1f) mapScale = 0.1f;
            if (mapScale > 4.0f) mapScale = 4.0f;

            // Display the new scale.
            Debug.WriteLine(mapScale.ToString("p0"));
        }


        // 마우스 Down/Move로 맵을 이동시키기
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseFirstLocation = e.Location; //Control.MousePosition;
            }
        }


        // 마우스 Down/Move로 맵을 이동시키기
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mouseCurrentLocation = e.Location; //Control.MousePosition;

                Point deltaPoint = new Point(mouseFirstLocation.X - mouseCurrentLocation.X, mouseFirstLocation.Y - mouseCurrentLocation.Y);

                mouseMoveOffset.X -= deltaPoint.X;
                mouseMoveOffset.Y -= deltaPoint.Y;

                mouseFirstLocation = mouseCurrentLocation;

                Console.WriteLine($"mouseMoveOffset = X = {mouseMoveOffset.X} / Y = {mouseMoveOffset.Y} || mouseFirstLocation X = {mouseFirstLocation.X} / Y = {mouseFirstLocation.Y}");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;

            MapID = textBox1.Text;
            StartLoop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;

            StopLoop();
        }


        private void btnMapDownload_Click(object sender, EventArgs e)
        {
            if (map == null || map.Image == null)
                return;

            var dlg = new SaveFileDialog { Title = "save image", DefaultExt = "png", Filter = "PNG File(*.png)|*.png" };
            dlg.FileName = $"fleet_map_{mapName}";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                btnMapDownload.Enabled = false;
                try
                {
                    Image image = (Image)map.Image.Clone();
                    image.Save(dlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    EventLogger.Info($"map downloaded ({mapName})");
                }
                catch (Exception ex)
                {
                    EventLogger.Info($"{ex} ({mapName})");
                }
                btnMapDownload.Enabled = true;
            }
        }


        private void btnMapReload_Click(object sender, EventArgs e)
        {
            //btnMapReload.Enabled = false;

            // reload map image
            StopLoop();
            if (ACSDbMapMode) ACSDbMapUpdate = true;
            else if (FleetMapMode) FleetMapUpdate = true;
            else if (customMapMode) customMapUpdate = true;
            StartLoop();

            //btnMapReload.Enabled = true;
        }

        public void ReStartResetData()
        {
            StopLoop();
            if (ACSDbMapMode) ACSDbMapUpdate = true;
            else if (FleetMapMode) FleetMapUpdate = true;
            else if (customMapMode) customMapUpdate = true;
            this.BeginInvoke(new Action(() =>
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                btnMapDownload.Visible = false;
                cb_DisplayInfo.Visible = false;
                chkCustomMap.Visible = false;
                chkACSDbMap.Visible = false;
                chkFleetMap.Visible = false;
                textBox1.Visible = false;
                lbl_ClickPosInfo.Visible = false;
            }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mapScale = (float)0.5;
            this.mouseFirstLocation = new Point(60 + 218, 135 + 48);
            this.mouseMoveOffset = new Point(60 - 189, 135 - 175);
        }
    }
}
