using Monitor.Common;
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


        public Bitmap DBGetRenderImage(Size drawingAreaSize, Point moveOffset, Image Image, IList<FleetPositionModel> fleetPositionsDB, IList<FleetRobot> robots,
          FloorMapIdConfigModel floorMapIdConfigs, float scaleFactorH = 1.0F, float scaleFactorV = 1.0F)
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

                        //DataBase
                        if (Image != null && fleetPositionsDB != null)
                        {
                            //DataBaseMapImage(기존 코드) 이미지 위치를 변경해서 그리려면(customImage, 0, 0) 0,0 가로 세로를 변경하면 된다
                            //g.DrawImage(customImage, 0, 0);
                            //테두리 사각형을 포함한 코드
                            //if(floorMapIdConfigs.FloorIndex == "2F") g.DrawImage(Image, new Rectangle(18, -40, Image.Width, Image.Height));

                            if (floorMapIdConfigs.FloorIndex == "2F") g.DrawImage(Image, 18, -40);
                            else if (floorMapIdConfigs.FloorIndex == "1F") g.DrawImage(Image, -12, 30);
                            else if (floorMapIdConfigs.FloorIndex == "B1F") g.DrawImage(Image, 30, 72);
                            else g.DrawImage(Image, 0, 0);

                            //============포지션 전체 그리기
                            //DBImageDrawPositions(g, fleetPositionsDB, Image, floorMapIdConfigs);

                            //=============장비 포지션 Text 그리기
                            DBImageUserText(g, fleetPositionsDB, Image, floorMapIdConfigs);
                            if (robots != null)
                            {
                                foreach (var robot in robots)
                                {
                                    DBImageDrawRobot(g, Image, robot, floorMapIdConfigs);

                                    if (robot.StateText == "Executing" && robot.MissionText.Contains("Moving to"))
                                    {
                                        foreach (var position in fleetPositionsDB)
                                        {
                                            string MissionText = $"Moving to '{position.Name}'";

                                            if (robot.MissionText.StartsWith(MissionText))
                                            {
                                                //이름으로 포지션 그리기
                                                DBImageDrawPositions(g, fleetPositionsDB, Image, position.Name);
                                                RobotPathDraw(g, RobotcenterPoint, ImagePosCenterPoint);
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


        public PointF DBMapImageGetScaledMapPoint(Point moveOffset, Image DBmapImage, PointF point, float scaleFactorH = 1.0F, float scaleFactorV = 1.0F)
        {
            if (DBmapImage == null) return Point.Empty;

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
            PointF mappingPoint = DBMapImageGetMapPoint(DBmapImage, convertedPoint);

            //Console.WriteLine($"GetScaledMapPoint = {mappingPoint.X,-6:0.00}, {mappingPoint.Y,-6:0.00}");
            return mappingPoint;
        }

        private PointF DBMapImageGetMapPoint(Image DBmapImage, PointF point)
        {
            float 좌표차이 = 0.1f;
            float Resolution = 0.05f;

            //Robot X Y 좌표[순서 변경하면 안됨]
            lock (lockObj)
            {
                float x = point.X;
                float y = point.Y;

                y = DBmapImage.Height - y;

                x *= (float)Resolution;
                y *= (float)Resolution;

                return new PointF(x, y);
            }
        }

        private void DBImageDrawPositions(Graphics g, IList<FleetPositionModel> fleetPositions, Image DataBaseImage/*, FloorMapIdConfigModel floorMapIdConfigs*/, string PositionName)
        {
            foreach (var pos in fleetPositions.Where(x => x.Name == PositionName))
            {
                Sub_DBImageDrawPosition(g, pos, DataBaseImage);
                Console.WriteLine($"{MapName} / {pos.Name}");
            }
        }
        private void DBImageDrawPositions(Graphics g, IList<FleetPositionModel> fleetPositions, Image DataBaseImage, FloorMapIdConfigModel floorMapIdConfigs/*, string PositionName*/)
        {
            foreach (var pos in fleetPositions.Where(x => x.MapID == floorMapIdConfigs.MapID && x.Name.StartsWith("ACS") == false))
            {
                Sub_DBImageDrawPosition(g, pos, DataBaseImage);
                Console.WriteLine($"{MapName} / {pos.Name}");
            }
        }

        private void Sub_DBImageDrawPosition(Graphics g, FleetPositionModel pos, Image DataBaseImage)
        {

            //float radius = 9.5f; //7.5f;
            //float halfSize = 9.5f; //7.5f;
            float radius = 11f; //7.5f;
            float halfSize = 11f; //7.5f;
            float Resolution = 0.05f;

            float x = (float)pos.PosX / (float)Resolution;
            float y = (float)pos.PosY / (float)Resolution;
            float theta = (float)pos.Orientation;

            y = DataBaseImage.Height - y;

            // point1 (center)
            var centerPoint = new PointF(x, y);

            int imgeCenterPointX = (int)centerPoint.X - 7;
            int imgeCenterPointY = (int)centerPoint.Y - 10;
            g.DrawImage(imagePosition, imgeCenterPointX, imgeCenterPointY);

            int 화살표포지션센터X = (int)imgeCenterPointX + 10;
            int 화살표포지션센터Y = (int)imgeCenterPointY + 10;

            ImagePosCenterPoint = new PointF(화살표포지션센터X, 화살표포지션센터Y);

            // point2 (direction)
            var cosX = radius * (float)Math.Cos(-theta / 180 * Math.PI);
            var sinY = radius * (float)Math.Sin(-theta / 180 * Math.PI);
            var arrowPoint1 = new PointF(centerPoint.X + cosX * 0.65f, centerPoint.Y + sinY * 0.65f);
            var arrowPoint2 = new PointF(centerPoint.X + cosX, centerPoint.Y + sinY);


            //// draw pos (circle) ==============
            var rt1 = new RectangleF((int)(centerPoint.X - halfSize), (int)(centerPoint.Y - halfSize), (int)halfSize * 2, (int)halfSize * 2);
            //g.FillEllipse(Brushes.Orange, rt1);
            //g.DrawEllipse(borderPen1, rt1);
            ////g.DrawLine(arrowPen1, arrowPoint1, arrowPoint2); // arrow
            //g.FillEllipse(Brushes.Chocolate, new RectangleF(arrowPoint1.X - 2.5f, arrowPoint1.Y - 2.5f, 5.0f, 5.0f));

            // draw pos info
            var PositionName = pos.Name;
            rt1.Inflate(10, -10);
            //g.DrawString(PositionName, font2, Brushes.GreenYellow, rt1);
            g.DrawString(PositionName, font2, Brushes.Black, rt1.X, rt1.Y);
        }
        private void DBImageDrawRobot(Graphics g, Image DataBaseImage, FleetRobot robot, FloorMapIdConfigModel floorMapIdConfigs)
        {

            float x = 0;
            float y = 0;
            float Resolution = 0.05f;
            float halfSizeX = 12.0f;
            float halfSizeY = 8.0f;

            x = (float)robot.PosX / (float)Resolution;
            y = (float)robot.PosY / (float)Resolution;

            y = DataBaseImage.Height - y;


            float theta = -(float)robot.Orientation;

            // point1 (center)
            var centerPoint = new PointF(x, y);
            RobotcenterPoint = centerPoint;


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
                g.FillPath(Brushes.DarkOrange, robotPath);
                g.DrawPath(borderPen2, robotPath);
                g.DrawLine(arrowPen2, centerPoint, centerPoint + new SizeF(halfSizeX, 0)); // arrow

            }

            // 좌표계 회전 복구
            matrix.RotateAt(-theta, centerPoint);
            g.Transform = matrix;

            // draw robot info
            var robotInfo = robot.RobotAlias;
            //var textPoint = centerPoint - new Size(20, 20);
            var textPoint = centerPoint - new Size(70, 20);


            g.DrawString(robotInfo, textFont1, Brushes.Red, textPoint + new Size(40, -10));

            //// 좌표계 회전 복구
            //matrix.RotateAt(-theta, centerPoint);
            //g.Transform = matrix;
        }

        private void DBImageUserText(Graphics g, IList<FleetPositionModel> fleetPositions, Image DataBaseImage, FloorMapIdConfigModel floorMapIdConfigs)
        {
            foreach (var UserPOSText in fleetPositions.Where(x => x.MapID == floorMapIdConfigs.MapID && x.Name.StartsWith("User")))
            {

                DBImageDrawText(g, DataBaseImage, UserPOSText, font2);
                Console.WriteLine(UserPOSText.Name);
            }
        }

        private void DBImageDrawText(Graphics g, Image DataBaseImage, FleetPositionModel pos, Font font/*, Brush brush,string text*/)
        {
            float Resolution = 0.05f;

            float x = (float)pos.PosX / (float)Resolution;
            float y = (float)pos.PosY / (float)Resolution;
            float theta = (float)pos.Orientation;

            y = DataBaseImage.Height - y;
            string posName = pos.Name.Replace("User", "");

            var centerPoint = new PointF(x, y);
            var size = g.MeasureString(posName, font);

            var TextPoint = new PointF(centerPoint.X - 28, centerPoint.Y);
            var TextBoxSize = new SizeF(size.Width, size.Height + 3);

            // Size
            var rect1 = new RectangleF(TextPoint.X, TextPoint.Y, TextBoxSize.Width, TextBoxSize.Height);
            var rect2 = new Rectangle((int)TextPoint.X, (int)TextPoint.Y, (int)TextBoxSize.Width, (int)TextBoxSize.Height);

            //그리기
            g.FillRectangle(Brushes.Yellow, rect1);
            g.DrawRectangle(Pens.DarkGray, rect2);
            g.DrawString(posName, font, Brushes.Black, rect1);
            //g.DrawString(text, font, brush, x, y);

        }
    }
}
