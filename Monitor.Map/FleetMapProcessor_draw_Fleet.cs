using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Map
{
    public partial class FleetMapProcessor
    {

        public Bitmap FleetGetRenderImage(Size drawingAreaSize, Point moveOffset, FleetMap map,IList<FleetRobot> robots, float scaleFactorH = 1.0F, float scaleFactorV = 1.0F)
        {
            try
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

                        // draw all
                        g.Clear(Color.Gray);
                        if (map != null)
                        {
                            // 맵 위치 변경해서 찍는방법은 0,0 을 수치로 변경 g.DrawImage(map.Image, 0, 0);

                            g.DrawImage(map.Image, 18, -40);
                            //g.DrawRectangle(new Pen(Color.Red, 5), new Rectangle(0, 0, map.Image.Width, map.Image.Height));
                            DrawPositions(g, map);
                            Console.WriteLine($"FleetPosCenterPoint = {FleetPosCenterPoint}");

                            if (robots != null)
                            {
                                foreach (var robot in robots)
                                {
                                    DrawRobot(g, map, robot);

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
                                                RobotPathDraw(g, RobotcenterPoint, ImagePosCenterPoint);
                                                //Console.WriteLine($"RobotName = {robot.RobotName} /  / RobotCenterPoint = {RobotcenterPoint} / POSCenterPoint = {POScenterPoint}");
                                            }
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
            catch (Exception ex)
            {
                Console.WriteLine($"EX = {ex.InnerException} /  {ex.Message} / {ex.StackTrace}");
                return null;
            }
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

    }
}
