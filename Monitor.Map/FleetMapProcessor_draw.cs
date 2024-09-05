using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;

namespace Monitor.Map
{
    public partial class FleetMapProcessor
    {
        private readonly static object lockObj = new object();
        private readonly Pen borderPen1 = new Pen(Color.Red, -1);
        private readonly Pen borderPen2 = new Pen(Color.Blue, -1);
        private readonly Pen arrowPen1 = new Pen(Color.Chocolate, 5.0f);
        private readonly Pen arrowPen2 = new Pen(Color.SteelBlue, 5.0f) { EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor };
        private readonly Font font1 = new Font("맑은 고딕", 10, FontStyle.Bold);
        private readonly Font font2 = new Font("Calibri", 8, FontStyle.Regular);
        //private GraphicsPath robotPath = null;

        private readonly Font textFont1 = new Font("Calibri", 18, FontStyle.Bold);
        private readonly Font textFont2 = new Font("Calibri", 44, FontStyle.Bold);
        private readonly Font textFont3 = new Font("Calibri", 50, FontStyle.Bold);
        private readonly Font textFont4 = new Font("Calibri", 5, FontStyle.Bold);

        private PointF RobotcenterPoint = new PointF();
        private PointF ImagePosCenterPoint = new PointF();
        private PointF FleetPosCenterPoint = new PointF();

        public int MapNo { get; set; }


        public Bitmap GetRenderImage(Size drawingAreaSize, Point moveOffset, FleetMap map, IList<FleetRobot> robots, float scaleFactorH = 1.0F, float scaleFactorV = 1.0F)
        {
            return GetRenderImage(drawingAreaSize, moveOffset, map, map.Image, robots, scaleFactorH, scaleFactorV);
        }

        public Bitmap GetRenderImage(Size drawingAreaSize, Point moveOffset, FleetMap map, Image customImage, IList<FleetRobot> robots, float scaleFactorH = 1.0F, float scaleFactorV = 1.0F)
        {

            // create bitmap (for double-buffering)
            Bitmap bitmap = new Bitmap(drawingAreaSize.Width, drawingAreaSize.Height);

            lock (lockObj)
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    g.InterpolationMode = InterpolationMode.High;

                    // map 변환
                    float dx = moveOffset.X;
                    float dy = moveOffset.Y;
                    g.TranslateTransform(dx, dy); // map을 dx,dy만큼 이동시킨다
                    g.ScaleTransform(scaleFactorH, scaleFactorV); // map을 스케일링 한다

                    //Console.WriteLine($"MapName = {map.Name} / drawingAreaSize Width= {drawingAreaSize.Width} / drawingAreaSize Width= {drawingAreaSize.Height} || GetRenderImage moveOffset X= {moveOffset.X} / moveOffset Y= {moveOffset.Y}");

                    //Test[Map = Customlmage / Robot = Fleet ]
                    float 맵일치보정좌표X = 114;
                    float 맵일치보정좌표Y = 305;

                    // draw all
                    g.Clear(Color.Gray);
                    //((Bitmap)mapImage).MakeTransparent(Color.White); // white 색상을                                                                                                                                                 투명으로 설정한다

                    //DataBaseMapImage
                    //g.DrawImage(customImage, 0, 0);
                    g.DrawImage(customImage, new Rectangle(0, 0, customImage.Width, customImage.Height));
                    g.DrawRectangle(new Pen(Color.Red, 5), new Rectangle(0, 0, customImage.Width, customImage.Height));
                    //customImageDrawText(g, 352, 63, textFont4, "convertedPoint");
                    //customImageDrawText(g, 25, 23, textFont4, "mappingPoint");

                    //사각형그리기

                    //Fleet
                    g.DrawImage(map.Image, 0, 0);
                    //g.DrawRectangle(new Pen(Color.Red, 5), new Rectangle(0, 0, map.Image.Width, map.Image.Height));

                    DrawPositions(g, map);
                    Console.WriteLine($"FleetPosCenterPoint = {FleetPosCenterPoint}");

                    customImageDrawPositions(g, map.Positions, customImage, 맵일치보정좌표X, 맵일치보정좌표Y);
                    Console.WriteLine($"ImagePosCenterPoint = {ImagePosCenterPoint}");

                    //// test
                    //var rects = new List<RectangleF>();
                    //rects.Add(new RectangleF(32.4f, 7.2f, 34.45f - 32.4f, 8.25f - 7.2f));
                    //rects.Add(new RectangleF(18f, 9.8f, 30f - 18f, 13f - 9.8f));
                    //DrawRectangles(g, map, rects);

                    if (robots != null)
                    {
                        foreach (var robot in robots)
                        {
                            //FleetMap
                            DrawRobot(g, map, robot);
                            //DrawPositions(g, map);
                            //customMap
                            customImageDrawRobot(g, customImage, robot);
                            //customImageDrawPositions(g, map, 맵일치보정좌표X, 맵일치보정좌표Y);

                            //Console.WriteLine($"RobotName = {robot.RobotName} / RobotX = {robot.PosX} / RobotY = {robot.PosY} / RobotCenterPoint = {RobotcenterPoint}");
                            if (robot.StateText == "Executing" && robot.MissionText.Contains("Moving to"))
                            {

                                foreach (var position in map.Positions)
                                {

                                    string positionName = $"'{position.Name}'";

                                    string[] NameSplit = robot.MissionText.Split(' ');

                                    //string NameReplace = robot.MissionText.Replace("Moving to", "");

                                    if (NameSplit[2].StartsWith(positionName))
                                    {
                                        //customImageDrawPositions(g, map, 맵일치보정좌표X, 맵일치보정좌표Y, position.Name);
                                        Draw점선그리기(g, RobotcenterPoint, ImagePosCenterPoint);
                                        //Console.WriteLine($"RobotName = {robot.RobotName} /  / RobotCenterPoint = {RobotcenterPoint} / POSCenterPoint = {POScenterPoint}");
                                    }
                                }
                            }
                        }
                    }


                    g.ResetTransform();
                }
            }

            return bitmap;
        }

        //DBImage
        public PointF customMapImageGetScaledMapPoint(Point moveOffset, Image custommapImage, PointF point, float scaleFactorH = 1.0F, float scaleFactorV = 1.0F)
        {
            if (custommapImage == null) return Point.Empty;

            float dx = moveOffset.X;
            float dy = moveOffset.Y;

            // map에 맞게 point값을 변환한다
            PointF convertedPoint = point;
            convertedPoint.X -= (int)dx;
            convertedPoint.Y -= (int)dy;
            convertedPoint.X = (int)(convertedPoint.X / scaleFactorH);
            convertedPoint.Y = (int)(convertedPoint.Y / scaleFactorV);

            Console.WriteLine($"convertedPoint = {convertedPoint} Text 좌측상단 좌표");

            // 변환한 point값으로 map에서의 좌표값을 구한다
            PointF mappingPoint = customMapImageGetMapPoint(custommapImage, convertedPoint);

            //Console.WriteLine($"GetScaledMapPoint = {mappingPoint.X,-6:0.00}, {mappingPoint.Y,-6:0.00}");
            return mappingPoint;
        }

        private PointF customMapImageGetMapPoint(Image customImage, PointF point)
        {
            float 좌표차이 = 0.1f;
            float Resolution = 0.005f;
            //Robot X Y 좌표[순서 변경하면 안됨]
            lock (lockObj)
            {

                float x = (float)point.X;
                float y = (float)point.Y;

                x = x + 305;
                y = y - 114;
                int dataBaseImageHeight = customImage.Height + 118;

                y = dataBaseImageHeight + y;

                x = (float)point.X * Resolution / 좌표차이;
                y = (float)point.Y * Resolution / 좌표차이;

                Console.WriteLine($"x = {x} , y = {y}");
                return new PointF(x, y);
            }
        }

        private void customImageDrawPositions(Graphics g, List<FleetPosition> fleetPositions, Image DataBaseImage, float 맵일치보정좌표X, float 맵일치보정좌표Y/*, string PositionName*/)
        {
            foreach (var pos in fleetPositions)
            {
                // 0: moving pos (way point?)

                // 1: target pos (cart?)   로봇 MAP 에서 보이는 초록색 포지션?

                // 2: cart pos
                // 3: cart entry pos1
                // 4: cart entry pos2

                // 7: charge pos?
                // 8: charge entry pos?

                // 11: VL marker pos
                // 12: VL marker entry pos

                // 13: L marker pos
                // 14: L marker entry pos
                // .....

                // 22: lift rack pos

                // ** position type 의 정확한 구분을 모르겠다...
                // ** position name 으로 필요한 것만 필터링하는것이 나을 듯...


                //if (pos.TypeID == "1" || pos.TypeID == "22")
                //{
                //    DrawPosition(g, map, pos);
                //}
                //else if (pos.TypeID == "20" && pos.Name.ToUpper().Contains("CHARGER"))
                //{
                //    DrawCharger(g, map, pos);
                //}

                //if (pos.Name.Contains("T3F") || pos.Name.Contains("M3F") || pos.Name.Contains("Pack") || pos.Name.Contains("Elevator"))
                //{
                //    if (!pos.Name.Contains("_X1") && !pos.Name.Contains("_X2")
                //     && !pos.Name.Contains("_X3") && !pos.Name.Contains("_X4"))
                //    {
                //        if (pos.Name.Contains("Charg"))
                //        {
                //            if (pos.TypeID == "20")         //20=충전포지션? , 21=충전진입포지션?
                //                DrawCharger(g, map, pos);
                //        }
                //        else
                //        {
                //            DrawPosition(g, map, pos);
                //        }
                //    }
                //}

                var posNames = new string[] {
                    "T3F_Main",
                    "T3F_Sub1",
                    "T3F_Sub2",
                    //"T3F_Elevator",
                    //"M3F_Elevator",
                    "M3F_Main",
                    "M3F_Sub",
                    "Packing_POS",
                    //"T3F Charging station_3399",
                    //"M3F Charging station_3399",
                };

                //if (posNames.Contains(pos.Name))
                //if (pos.Name == PositionName)
                {

                    Sub_customImageDrawPosition(g, pos, DataBaseImage, 맵일치보정좌표X, 맵일치보정좌표Y);
                }


                //{
                //    if (MapName == "T3F")
                //    {
                //        var p = new FleetPosition();

                //        p.PosX = 12; p.PosY = 9.45;
                //        DrawText(g, map, p, textFont1, Brushes.Magenta, "ELEVATOR");

                //        p.PosX = 35; p.PosY = 7.9;
                //        DrawText(g, map, p, textFont1, Brushes.Magenta, "T3F_Main");

                //        p.PosX = 15; p.PosY = 1.75;
                //        DrawText(g, map, p, textFont1, Brushes.Magenta, "T3F_Sub1");

                //        p.PosX = 21; p.PosY = 1.75;
                //        DrawText(g, map, p, textFont1, Brushes.Magenta, "T3F_Sub2");
                //    }

                //    else if (MapName == "M3F")
                //    {
                //        var p = new FleetPosition();

                //        p.PosX = 7.5; p.PosY = 10.5;
                //        DrawText(g, map, p, textFont2, Brushes.Magenta, "ELEVATOR");

                //        p.PosX = 176.5; p.PosY = 30;
                //        DrawText(g, map, p, textFont2, Brushes.Magenta, "AIR SHOWER");

                //        p.PosX = 180; p.PosY = 118;
                //        DrawText(g, map, p, textFont2, Brushes.Magenta, "M3F_Main");

                //        p.PosX = 160; p.PosY = 89;
                //        DrawText(g, map, p, textFont2, Brushes.Magenta, "M3F_Sub");

                //        //p.PosX = 206; p.PosY = 45;
                //        p.PosX = 178; p.PosY = 44.1;
                //        DrawText(g, map, p, textFont2, Brushes.Magenta, "Packing_POS");
                //    }
                //}

            }
        }

        private void Sub_customImageDrawPosition(Graphics g, FleetPosition pos, Image DataBaseImage, float 맵일치보정좌표X, float 맵일치보정좌표Y)
        {
            //float radius = 9.5f; //7.5f;
            //float halfSize = 9.5f; //7.5f;
            //int oldWidth = 443;
            //int oldHeight = 506;
            int oldWidth = 443;
            int oldHeight = 190;

            float widthRatio = (float)(DataBaseImage.Width + 128) / oldWidth;
            float heightRatio = (float)(DataBaseImage.Height + 128) / oldHeight;



            float radius = 11f; //7.5f;
            float halfSize = 22f; //7.5f;

            //float widthRatio = (float)newWidth / oldWidth;
            //float heightRatio = (float)newHeight / oldHeight;
            float x = (float)pos.PosX / 0.05f;
            float y = (float)pos.PosY / 0.05f;
            float theta = (float)pos.Orientation;

            y = oldHeight - y;


            x = x * widthRatio;
            y = y * heightRatio;


            //float x = (float)pos.PosX / (float)map.Resolution;
            //float y = (float)pos.PosY / (float)map.Resolution;
            //float theta = (float)pos.Orientation;

            //y = map.Image.Height - y;

            // point1 (center)
            var centerPoint = new PointF(x, y);
            /*if (pos.Name == positionName)*/
            ImagePosCenterPoint = centerPoint;


            // point2 (direction)
            var cosX = radius * (float)Math.Cos(-theta / 180 * Math.PI);
            var sinY = radius * (float)Math.Sin(-theta / 180 * Math.PI);
            var arrowPoint1 = new PointF(centerPoint.X + cosX * 1.65f, centerPoint.Y + sinY * 1.65f);
            var arrowPoint2 = new PointF(centerPoint.X + cosX, centerPoint.Y + sinY);


            //// draw pos (circle) ==============
            var rt1 = new RectangleF((int)(centerPoint.X - halfSize), (int)(centerPoint.Y - halfSize), (int)halfSize * 2, (int)halfSize * 2);

            rt1.X = rt1.X - 88;
            rt1.Y = rt1.Y - 64;
            arrowPoint1.X -= 88;
            arrowPoint1.Y -= 64;
            //rt1.Y = rt1.Y + DataBaseImage.Height;
            //g.FillEllipse(Brushes.DarkGray, rt1);
            g.FillEllipse(Brushes.SkyBlue, rt1);
            //g.FillEllipse(Brushes.Orange, rt1);
            g.DrawEllipse(borderPen1, rt1);
            //g.DrawLine(arrowPen1, arrowPoint1, arrowPoint2); // arrow
            g.FillEllipse(Brushes.Chocolate, new RectangleF(arrowPoint1.X - 2.5f, arrowPoint1.Y - 2.5f, 5.0f, 5.0f));

            //// draw pos info
            ////var robotInfo = "pos:" + pos.Name;
            //var robotInfo = pos.Name;
            //var textPoint = centerPoint - new Size(10, 10);
            //g.DrawLine(Pens.Black, centerPoint, textPoint);
            //g.DrawString(robotInfo, font2, Brushes.Magenta, textPoint + new Size(-16, -16));

            // draw pos info
            var PositionName = pos.Name;
            //rt1.Inflate(-7, -3);
            rt1.Inflate(7, 9);
            g.DrawString(PositionName, font2, Brushes.Blue, rt1);
            //g.DrawString(robotInfo, font2, Brushes.Magenta, rt1);
            //g.DrawString(robotInfo, font2, Brushes.Magenta, rt1.X, rt1.Y);



            //if (MapName == "T3F")
            //    g.DrawString(robotInfo, textFont1, Brushes.Magenta, rt1.X, rt1.Y);

            //if (MapName == "M3F")
            //    g.DrawString(robotInfo, textFont2, Brushes.Magenta, rt1.X, rt1.Y);




            //// draw pos (rectangle) ==============
            //var rt2 = new Rectangle((int)(centerPoint.X - halfSize), (int)(centerPoint.Y - halfSize), (int)halfSize * 2, (int)halfSize * 2);
            //g.FillRectangle(Brushes.Orange, rt2);
            //g.DrawRectangle(borderPen1, rt2);

            //// draw pos info
            //var robotInfo = pos.Name;
            //rt2.Inflate(-3, 0);
            //g.DrawString(robotInfo, font2, Brushes.Magenta, rt2);
        }

        private void customImageDrawRobot(Graphics g, Image DataBaseImage, FleetRobot robot)
        {

            int FleetMapSizeX = 443;
            int FleetMapSizeY = 506;
            float x = 0;
            float y = 0;
            float FleetimageSizeY = 506;
            float halfSizeX = 12.0f;
            float halfSizeY = 8.0f;
            int dataBaseImageHeight = 0;
            int dataBaseImageWidth = 0;
            if (MapName == "T3F")
            {
                halfSizeX = halfSizeX * 4.5f;
                halfSizeY = halfSizeY * 4.5f;
            }

            if (MapName == "M3F")
            {
                //halfSizeX = 24.0f * 1.25f;
                //halfSizeY = 16.0f * 1.25f;

                float Resolution = 0.005f;

                x = (float)robot.PosX / (float)Resolution;
                y = (float)robot.PosY / (float)Resolution;

                y = DataBaseImage.Height - y;


                //float 좌표차이 = 0.1f;

                //float newwidthRatio = (float)DataBaseImage.Width / FleetMapSizeX;
                //float newheightratio = (float)DataBaseImage.Height / FleetMapSizeY;
                ////x = robot.PosX * widthRatio;
                ////y = robot.PosY * heightratio;


                //x = (float)robot.PosX;
                //y = (float)robot.PosY;

                ////x = x * 26.24682626827844f;
                ////y = y * 51.748429907725f;
                ////y = DataBaseImage.Height - y;

                //////Robot X Y 좌표[순서 변경하면 안됨]
                //x = x / Resolution;/** 좌표차이*/;
                //y = y / Resolution;/** 좌표차이*/;

                //x = x * (float)newwidthRatio;
                //y = y * (float)newheightratio;


                //x = x * 좌표차이;
                //y = y * 좌표차이;


                //dataBaseImageHeight = DataBaseImage.Height + 118;
                //dataBaseImageWidth = DataBaseImage.Width - 512;
                //x = x - dataBaseImageWidth;
                //y = dataBaseImageHeight - y;
                //x = Math.Abs(x);
                //y = Math.Abs(y);
                //y = DataBaseImage.Height - y;

                //x = x * 2.5f;
                //y = y * 2.5f;

                // DataBaseImage VerticalResolution = 143.9926 나누기 Fleet VerticalResolution = 96.01199  = 1.48F




                ////Map위치 Robot위치
                //x = x - 125;
                //y = y + 80;

                //Console.WriteLine($"FleetRobot x = {x}, Y = {y}");



                ////Robot X Y 좌표[순서 변경하면 안됨]
                //dataBaseImageHeight = DataBaseImage.Height /*+ 118*/;
                //x = (float)robot.PosX / Resolution * 좌표차이 * 3;
                //y = (float)robot.PosY / Resolution * 좌표차이 * 3;
                //y = dataBaseImageHeight - y;

                ////Map위치 Robot위치
                //x = x + 114;
                //y = y - 305;


            }

            float theta = -(float)robot.Orientation;

            // point1 (center)
            var centerPoint = new PointF(x, y);
            RobotcenterPoint = centerPoint;
            //Console.WriteLine($"CustomrobotCenter = {centerPoint}");

            // 좌표계 회전 변환
            Matrix matrix = g.Transform;

            matrix.RotateAt(theta, centerPoint);
            g.Transform = matrix;

            // make robot path
            var robotRect = new RectangleF(centerPoint.X - halfSizeX, centerPoint.Y - halfSizeY, halfSizeX * 2, halfSizeY * 2);
            //Console.WriteLine($"robotRect = {robotRect}");

            var cornerRadius = 4.0f;

            using (GraphicsPath robotPath = Helper.GetRoundedRectanglePath(robotRect, cornerRadius, cornerRadius))
            {
                // draw robot
                g.FillPath(Brushes.DarkOrange, robotPath);
                g.DrawPath(borderPen2, robotPath);
                g.DrawLine(arrowPen2, centerPoint, centerPoint + new SizeF(halfSizeX, 0)); // arrow
            }

            // 좌표계 회전 복구
            matrix.RotateAt(-theta, centerPoint);
            g.Transform = matrix;

            // draw robot info
            var robotInfo = robot.RobotName;
            var textPoint = centerPoint - new Size(20, 20);

            if (MapName == "T3F")
                g.DrawString(robot.RobotAlias, textFont3, Brushes.Magenta, textPoint + new Size(40, -10));
            else if (MapName == "M3F")
            {
                //g.DrawString(robot.RobotAlias, textFont3, Brushes.Magenta, textPoint + new Size(45, -20));
                g.DrawString(robot.RobotName, textFont4, Brushes.Black, textPoint + new Size(-5, 30));
            }
            else
                g.DrawString(robot.RobotAlias, textFont1, Brushes.Magenta, textPoint + new Size(40, -10));

            //// 좌표계 회전 복구
            //matrix.RotateAt(-theta, centerPoint);
            //g.Transform = matrix;
        }

        private void customImageDrawText(Graphics g, float x, float y, Font font,/* Brush brush,*/ string text)
        {

            //float x = (float)pos.PosX / (float)map.Resolution;
            //float y = (float)pos.PosY / (float)map.Resolution;
            //float theta = (float)pos.Orientation;

            //y = map.Image.Height - y;

            //// draw
            var size = g.MeasureString(text, font);
            var rect1 = new RectangleF(x, y, size.Width, size.Height);
            var rect2 = new Rectangle((int)x, (int)y, (int)size.Width, (int)size.Height);
            //g.FillRectangle(Brushes.Yellow, rect1);
            g.DrawRectangle(Pens.DarkGray, rect2);
            g.DrawString(text, font, Brushes.Black, rect1);
            // //g.DrawString(text, font, brush, x, y);
        }


        //fleetMap
        public PointF GetScaledMapPoint(Point moveOffset, FleetMap map, PointF point, float scaleFactorH = 1.0F, float scaleFactorV = 1.0F)
        {
            if (map == null) return Point.Empty;

            float dx = moveOffset.X;
            float dy = moveOffset.Y;

            // map에 맞게 point값을 변환한다
            PointF convertedPoint = point;
            convertedPoint.X -= (int)dx;
            convertedPoint.Y -= (int)dy;
            convertedPoint.X = (int)(convertedPoint.X / scaleFactorH);
            convertedPoint.Y = (int)(convertedPoint.Y / scaleFactorV);

            // 변환한 point값으로 map에서의 좌표값을 구한다
            PointF mappingPoint = GetMapPoint(map, convertedPoint);

            //Console.WriteLine($"GetScaledMapPoint = {mappingPoint.X,-6:0.00}, {mappingPoint.Y,-6:0.00}");

            return mappingPoint;
        }

        //Fleet Map
        private PointF GetMapPoint(FleetMap map, PointF point)
        {
            lock (lockObj)
            {
                float x = point.X;
                float y = point.Y;

                y = map.Image.Height - y;

                x *= (float)map.Resolution;
                y *= (float)map.Resolution;

                return new PointF(x, y);
            }
        }

        private void DrawPosition(Graphics g, FleetMap map, FleetPosition pos)
        {
            //float radius = 9.5f; //7.5f;
            //float halfSize = 9.5f; //7.5f;
            float radius = 11f; //7.5f;
            float halfSize = 11f; //7.5f;

            float x = (float)pos.PosX / (float)map.Resolution;
            float y = (float)pos.PosY / (float)map.Resolution;
            float theta = (float)pos.Orientation;

            y = map.Image.Height - y;

            // point1 (center)
            var centerPoint = new PointF(x, y);
            FleetPosCenterPoint = centerPoint;
            // point2 (direction)
            var cosX = radius * (float)Math.Cos(-theta / 180 * Math.PI);
            var sinY = radius * (float)Math.Sin(-theta / 180 * Math.PI);
            var arrowPoint1 = new PointF(centerPoint.X + cosX * 0.65f, centerPoint.Y + sinY * 0.65f);
            var arrowPoint2 = new PointF(centerPoint.X + cosX, centerPoint.Y + sinY);


            //// draw pos (circle) ==============
            var rt1 = new RectangleF((int)(centerPoint.X - halfSize), (int)(centerPoint.Y - halfSize), (int)halfSize * 2, (int)halfSize * 2);
            g.FillEllipse(Brushes.Orange, rt1);
            g.DrawEllipse(borderPen1, rt1);
            //g.DrawLine(arrowPen1, arrowPoint1, arrowPoint2); // arrow
            g.FillEllipse(Brushes.Chocolate, new RectangleF(arrowPoint1.X - 2.5f, arrowPoint1.Y - 2.5f, 5.0f, 5.0f));

            //// draw pos info
            ////var robotInfo = "pos:" + pos.Name;
            //var robotInfo = pos.Name;
            //var textPoint = centerPoint - new Size(10, 10);
            //g.DrawLine(Pens.Black, centerPoint, textPoint);
            //g.DrawString(robotInfo, font2, Brushes.Magenta, textPoint + new Size(-16, -16));

            // draw pos info
            var robotInfo = pos.Name;
            rt1.Inflate(-7, -3);
            g.DrawString(robotInfo, font2, Brushes.GreenYellow, rt1);
            //g.DrawString(robotInfo, font2, Brushes.Magenta, rt1.X, rt1.Y);



            //if (MapName == "T3F")
            //    g.DrawString(robotInfo, textFont1, Brushes.Magenta, rt1.X, rt1.Y);

            //if (MapName == "M3F")
            //    g.DrawString(robotInfo, textFont2, Brushes.Magenta, rt1.X, rt1.Y);




            //// draw pos (rectangle) ==============
            //var rt2 = new Rectangle((int)(centerPoint.X - halfSize), (int)(centerPoint.Y - halfSize), (int)halfSize * 2, (int)halfSize * 2);
            //g.FillRectangle(Brushes.Orange, rt2);
            //g.DrawRectangle(borderPen1, rt2);

            //// draw pos info
            //var robotInfo = pos.Name;
            //rt2.Inflate(-3, 0);
            //g.DrawString(robotInfo, font2, Brushes.Magenta, rt2);
        }

        private void DrawMap(Graphics g, FleetMap map)
        {
            g.DrawImage(map.Image, 0, 0);
        }


        private void DrawPositions(Graphics g, FleetMap map)
        {
            foreach (var pos in map.Positions)
            {
                // 0: moving pos (way point?)

                // 1: target pos (cart?)   로봇 MAP 에서 보이는 초록색 포지션?

                // 2: cart pos
                // 3: cart entry pos1
                // 4: cart entry pos2

                // 7: charge pos?
                // 8: charge entry pos?

                // 11: VL marker pos
                // 12: VL marker entry pos

                // 13: L marker pos
                // 14: L marker entry pos
                // .....

                // 22: lift rack pos

                // ** position type 의 정확한 구분을 모르겠다...
                // ** position name 으로 필요한 것만 필터링하는것이 나을 듯...


                //if (pos.TypeID == "1" || pos.TypeID == "22")
                //{
                //    DrawPosition(g, map, pos);
                //}
                //else if (pos.TypeID == "20" && pos.Name.ToUpper().Contains("CHARGER"))
                //{
                //    DrawCharger(g, map, pos);
                //}

                //if (pos.Name.Contains("T3F") || pos.Name.Contains("M3F") || pos.Name.Contains("Pack") || pos.Name.Contains("Elevator"))
                //{
                //    if (!pos.Name.Contains("_X1") && !pos.Name.Contains("_X2")
                //     && !pos.Name.Contains("_X3") && !pos.Name.Contains("_X4"))
                //    {
                //        if (pos.Name.Contains("Charg"))
                //        {
                //            if (pos.TypeID == "20")         //20=충전포지션? , 21=충전진입포지션?
                //                DrawCharger(g, map, pos);
                //        }
                //        else
                //        {
                //            DrawPosition(g, map, pos);
                //        }
                //    }
                //}

                var posNames = new string[] {
                    "T3F_Main",
                    "T3F_Sub1",
                    "T3F_Sub2",
                    //"T3F_Elevator",
                    //"M3F_Elevator",
                    "M3F_Main",
                    "M3F_Sub",
                    "Packing_POS",
                    //"T3F Charging station_3399",
                    //"M3F Charging station_3399",
                };

                //if (posNames.Contains(pos.Name))
                {
                    DrawPosition(g, map, pos);
                }


                //{
                //    if (MapName == "T3F")
                //    {
                //        var p = new FleetPosition();

                //        p.PosX = 12; p.PosY = 9.45;
                //        DrawText(g, map, p, textFont1, Brushes.Magenta, "ELEVATOR");

                //        p.PosX = 35; p.PosY = 7.9;
                //        DrawText(g, map, p, textFont1, Brushes.Magenta, "T3F_Main");

                //        p.PosX = 15; p.PosY = 1.75;
                //        DrawText(g, map, p, textFont1, Brushes.Magenta, "T3F_Sub1");

                //        p.PosX = 21; p.PosY = 1.75;
                //        DrawText(g, map, p, textFont1, Brushes.Magenta, "T3F_Sub2");
                //    }

                //    else if (MapName == "M3F")
                //    {
                //var p = new FleetPosition();

                //p.PosX = 7.5; p.PosY = 10.5;
                //DrawText(g, map, p, textFont2, Brushes.Magenta, "ELEVATOR");

                //        p.PosX = 176.5; p.PosY = 30;
                //        DrawText(g, map, p, textFont2, Brushes.Magenta, "AIR SHOWER");

                //        p.PosX = 180; p.PosY = 118;
                //        DrawText(g, map, p, textFont2, Brushes.Magenta, "M3F_Main");

                //        p.PosX = 160; p.PosY = 89;
                //        DrawText(g, map, p, textFont2, Brushes.Magenta, "M3F_Sub");

                //        //p.PosX = 206; p.PosY = 45;
                //        p.PosX = 178; p.PosY = 44.1;
                //        DrawText(g, map, p, textFont2, Brushes.Magenta, "Packing_POS");
                //    }
                //}

            }
        }


        private void DrawPoint(Graphics g, FleetMap map, PointF point)
        {
            //float radius = 9.5f; //7.5f;
            //float halfSize = 9.5f; //7.5f;
            float radius = 11f; //7.5f;
            float halfSize = 11f; //7.5f;

            float x = point.X / (float)map.Resolution;
            float y = point.Y / (float)map.Resolution;

            y = map.Image.Height - y;

            // point1 (center)
            var mapPoint = new PointF(x, y);

            var mapPoints = new PointF[1];
            mapPoints[0] = mapPoint;

            // draw
            g.DrawPolygon(borderPen1, mapPoints);
        }


        private void DrawRectangles(Graphics g, FleetMap map, IList<RectangleF> rects)
        {
            if (rects == null || rects.Count == 0) return;

            var scaledRects = new List<RectangleF>();

            foreach (var rect in rects)
            {
                float x = rect.X / (float)map.Resolution;
                float y = rect.Y / (float)map.Resolution;
                float h = rect.Width / (float)map.Resolution;
                float w = rect.Height / (float)map.Resolution;

                y = map.Image.Height - y;

                var maprect = new RectangleF(x, y, h, w);

                scaledRects.Add(maprect);
            }

            // draw
            //g.DrawRectangles(borderPen1, scaledRects.ToArray());
            g.FillRectangles(Brushes.Red, scaledRects.ToArray());
        }


        private void DrawText(Graphics g, FleetMap map, FleetPosition pos, Font font, Brush brush, string text)
        {
            float x = (float)pos.PosX / (float)map.Resolution;
            float y = (float)pos.PosY / (float)map.Resolution;
            float theta = (float)pos.Orientation;

            y = map.Image.Height - y;

            // draw
            var size = g.MeasureString(text, font);
            var rect1 = new RectangleF(x, y, size.Width, size.Height);
            var rect2 = new Rectangle((int)x, (int)y, (int)size.Width, (int)size.Height);
            g.FillRectangle(Brushes.Yellow, rect1);
            g.DrawRectangle(Pens.DarkGray, rect2);
            g.DrawString(text, font, Brushes.Black, rect1);
            //g.DrawString(text, font, brush, x, y);
        }


        private void DrawRobot(Graphics g, FleetMap map, FleetRobot robot)
        {
            float halfSizeX = 12.0f;
            float halfSizeY = 8.0f;

            if (MapName == "T3F")
            {
                halfSizeX = halfSizeX * 4.5f;
                halfSizeY = halfSizeY * 4.5f;
            }

            if (MapName == "M3F")
            {
                //halfSizeX = 24.0f * 1.25f;
                //halfSizeY = 16.0f * 1.25f;
            }

            float x = (float)robot.PosX / (float)map.Resolution;
            float y = (float)robot.PosY / (float)map.Resolution;

            //Co

            y = map.Image.Height - y;
            //Console.WriteLine($"FleetRobot x = {x}, Y = {y}");

            float theta = -(float)robot.Orientation;
            // point1 (center)
            var centerPoint = new PointF(x, y);
            RobotcenterPoint = centerPoint;
            //Console.WriteLine($"FleetCenterPoint = {centerPoint}");

            // 좌표계 회전 변환
            Matrix matrix = g.Transform;

            matrix.RotateAt(theta, centerPoint);
            g.Transform = matrix;

            // make robot path
            var robotRect = new RectangleF(centerPoint.X - halfSizeX, centerPoint.Y - halfSizeY, halfSizeX * 2, halfSizeY * 2);
            var cornerRadius = 4.0f;
            using (GraphicsPath robotPath = Helper.GetRoundedRectanglePath(robotRect, cornerRadius, cornerRadius))
            {
                // draw robot
                g.FillPath(Brushes.Red, robotPath);
                g.DrawPath(borderPen2, robotPath);
                g.DrawLine(arrowPen2, centerPoint, centerPoint + new SizeF(halfSizeX, 0)); // arrow
            }

            // 좌표계 회전 복구
            matrix.RotateAt(-theta, centerPoint);
            g.Transform = matrix;

            // draw robot info
            var robotInfo = robot.RobotName;
            var textPoint = centerPoint - new Size(20, 20);

            if (MapName == "T3F")
                g.DrawString(robot.RobotAlias, textFont3, Brushes.Magenta, textPoint + new Size(40, -10));
            else if (MapName == "M3F")
                g.DrawString(robot.RobotAlias, textFont3, Brushes.Magenta, textPoint + new Size(45, -20));
            else
                g.DrawString(robot.RobotAlias, textFont1, Brushes.Magenta, textPoint + new Size(40, -10));
        }


        private void DrawCharger(Graphics g, FleetMap map, FleetPosition pos)
        {
            float halfSizeX = 11.0f;
            float halfSizeY = 11.0f;

            float x = (float)pos.PosX / (float)map.Resolution;
            float y = (float)pos.PosY / (float)map.Resolution;
            float theta = 0; // -(float)pos.Orientation;
            //float theta = DateTime.Now.Second * 6; // TEST

            y = map.Image.Height - y;

            // point1 (center)
            var centerPoint = new PointF(x, y);

            // 좌표계 회전 변환
            Matrix matrix = g.Transform;

            matrix.RotateAt(theta, centerPoint);
            //matrix.Scale(0.1f, 0.1f);
            g.Transform = matrix;

            // make path
            var chargerRect = new RectangleF(centerPoint.X - halfSizeX, centerPoint.Y - halfSizeY, halfSizeX * 2, halfSizeY * 2);

            using (GraphicsPath chargerPath = Helper.GetChargerPath(chargerRect, centerPoint, halfSizeX))
            {
                // draw charger
                g.FillPath(Brushes.DeepSkyBlue, chargerPath);
                g.DrawPath(borderPen2, chargerPath);
                //g.DrawEllipse(Pens.Red, new Rectangle((int)chargerRect.X, (int)chargerRect.Y, (int)chargerRect.Width, (int)chargerRect.Height));
            }

            // 좌표계 회전 복구
            //matrix.Reset();
            matrix.RotateAt(-theta, centerPoint);
            g.Transform = matrix;

            // draw pos info
            var posInfo = pos.Name;
            g.DrawString(posInfo, font2, Brushes.Magenta, chargerRect.X, chargerRect.Y);
        }


        private void Draw점선그리기(Graphics g, PointF robotCenter, PointF POSCenter)
        {
            if (robotCenter == null || POSCenter == null) return;

            float[] dashPattern = { 10, 2 }; // 점선 패턴 (10픽셀 선, 2픽셀 간격)
            DrawDashedArrow(g, robotCenter, POSCenter, Color.Blue, dashPattern);
        }

        private void DrawDashedArrow(Graphics g, PointF start, PointF end, Color color, float[] dashPattern)
        {

            Pen pen = new Pen(Color.GreenYellow);
            // 점선 스타일 설정
            pen.DashStyle = DashStyle.Dot;

            // 점선 간격 설정
            pen.DashPattern = dashPattern;

            // 점선을 그리기
            g.DrawLine(pen, start, end);

            // 화살표 끝 그리기
            DrawArrowHead(g, pen, start, end);

            // 자원 해제
            pen.Dispose();
        }
        private void DrawArrowHead(Graphics g, Pen pen, PointF start, PointF end)
        {
            // 화살표 방향 계산
            float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
            float arrowLength = 10; // 화살표 길이
            float arrowAngle = (float)(Math.PI / 6); // 화살표 각도

            // 화살표 끝 점 계산
            PointF p1 = new PointF(end.X - arrowLength * (float)Math.Cos(angle - arrowAngle),
                                    end.Y - arrowLength * (float)Math.Sin(angle - arrowAngle));
            PointF p2 = new PointF(end.X - arrowLength * (float)Math.Cos(angle + arrowAngle),
                                    end.Y - arrowLength * (float)Math.Sin(angle + arrowAngle));

            // 화살표 끝 그리기
            g.DrawLine(pen, end, p1);
            g.DrawLine(pen, end, p2);


        }

    }
}
