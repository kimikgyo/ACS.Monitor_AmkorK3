//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace Monitor.Map
//{
//    class Class2
//    {

//        Rectangle zoomTgtArea = new Rectangle(300, 500, 200, 200);
//        Point zoomOrigin = Point.Empty;   // updated in MouseMove when button is pressed
//        float zoomFactor = 2f;




//        private void pictureBox_Paint(object sender, PaintEventArgs e)
//        {
//            // normal drawing
//            DrawStuff(e.Graphics);

//            // for the movable zoom we want a small correction
//            Rectangle cr = pictureBox.ClientRectangle;
//            float pcw = cr.Width / (cr.Width - ZoomTgtArea.Width / 2f);
//            float pch = cr.Height / (cr.Height - ZoomTgtArea.Height / 2f);

//            // now we prepare the graphics object; note: order matters!
//            e.Graphics.SetClip(zoomTgtArea);
//            // we can either follow the mouse or keep the output area fixed:
//            if (cbx_fixed.Checked)
//                e.Graphics.TranslateTransform(ZoomTgtArea.X - zoomCenter.X * zoomFactor,
//                                                ZoomTgtArea.Y - zoomCenter.Y * zoomFactor);
//            else
//                e.Graphics.TranslateTransform(-zoomCenter.X * zoomFactor * pcw,
//                                                -zoomCenter.Y * zoomFactor * pch);
//            // finally zoom
//            e.Graphics.ScaleTransform(zoomFactor, zoomFactor);

//            // and display zoomed
//            DrawStuff(e.Graphics);
//        }




//        void DrawStuff(Graphics g)
//        {
//            bool isZoomed = g.Transform.Elements[0] != 1
//                        || g.Transform.OffsetX != 0 | g.Transform.OffsetY != 0;
//            if (isZoomed) g.Clear(Color.Gainsboro);   // pick your back color

//            // all your drawing here!
//            Rectangle r = new Rectangle(10, 10, 500, 800);  // some size
//            using (Font f = new Font("Tahoma", 11f))
//                g.DrawString(text, f, Brushes.DarkSlateBlue, r);
//        }


//    }
//}
