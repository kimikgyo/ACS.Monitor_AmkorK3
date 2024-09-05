using System.Drawing;
using System.Drawing.Imaging;

namespace ImageUtils
{
    class ImageTransparency
    {
        public static Bitmap ChangeOpacity(Image img, float opacityvalue)
        {

            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityvalue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();
            return bmp;
        }

        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        //public static void MakeTransparent_Example(Bitmap img, Graphics graphics)
        //{
        //    //Bitmap bmp = new Bitmap("Grapes.gif");

        //    Color backColor = img.GetPixel(1, 1);
        //    img.MakeTransparent(backColor);
        //    graphics.DrawImage(img, 0, 0, img.Width, img.Height);
        //}

    }
}
