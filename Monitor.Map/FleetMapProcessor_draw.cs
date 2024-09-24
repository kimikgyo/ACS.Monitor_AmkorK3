
using Monitor.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
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

        private static string imagePositionPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "POS16.png");
        Image imagePosition = Image.FromFile(imagePositionPath);

        public int MapNo { get; set; }

        private void RobotPathDraw(Graphics g, PointF robotCenter, PointF POSCenter)
        {
            if (robotCenter == null || POSCenter == null) return;

            #region 점선 그리기 

            float[] dashPattern = { 10, 2 }; // 점선 패턴 (10픽셀 선, 2픽셀 간격)
            Pen pen = new Pen(Color.GreenYellow);
            // 점선 스타일 설정
            pen.DashStyle = DashStyle.Dot;

            // 점선 간격 설정
            pen.DashPattern = dashPattern;

            //Line 그리기 (pen , start[robotCenter] , end[POSCenter])
            g.DrawLine(pen, robotCenter, POSCenter);

            #endregion

            #region 화살표 그리기

            // 화살표 방향 계산
            float angle = (float)Math.Atan2(POSCenter.Y - robotCenter.Y, POSCenter.X - robotCenter.X);
            float arrowLength = 10; // 화살표 길이
            float arrowAngle = (float)(Math.PI / 6); // 화살표 각도

            // 화살표 끝 점 계산
            PointF p1 = new PointF(POSCenter.X - arrowLength * (float)Math.Cos(angle - arrowAngle),
                                    POSCenter.Y - arrowLength * (float)Math.Sin(angle - arrowAngle));
            PointF p2 = new PointF(POSCenter.X - arrowLength * (float)Math.Cos(angle + arrowAngle),
                                    POSCenter.Y - arrowLength * (float)Math.Sin(angle + arrowAngle));

            // 화살표 끝 그리기
            g.DrawLine(pen, POSCenter, p1);
            g.DrawLine(pen, POSCenter, p2);

            #endregion

            // 자원 해제
            pen.Dispose();
        }
    }
}
