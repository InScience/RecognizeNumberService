using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace RestService
{
    public class ImageTools
    {

        public byte[] ProcessImage(byte[] byteArray)
        {
            Bitmap Bit;
            Image img = byteArrayToImage(byteArray);


            CheckOrientation(img);

            Bit = ResizeImageStrech(img, 640, 480);

            byteArray = ConvertBitToJpeg(byteArray, Bit);


            return byteArray;
        }

        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static byte[] ConvertBitToJpeg(byte[] bytearr, Bitmap Bit)
        {

            Image img = byteArrayToImage(bytearr);


            using (MemoryStream mStream = new MemoryStream())
            {
                Bit.Save(mStream, ImageFormat.Jpeg);
                bytearr = mStream.ToArray();
            }

            return bytearr;
        }

        static void CheckOrientation(Image img)
        {
            int orient = -1;

            try
            {
                int orientation = img.GetPropertyItem(274).Value[0];
                orient = orientation;

                if (orient > -1)
                {
                    img = RemoveOrientationExifTag(img);
                    Console.WriteLine("Orientation Tag pašalintas");
                }
            }

            catch
            {
                Console.WriteLine("Orientation Tag nerastas");
            }


        }

        static Image RemoveOrientationExifTag(Image Img)
        {
            if (Array.IndexOf(Img.PropertyIdList, 274) > -1)
            {
                var orientation = (int)Img.GetPropertyItem(274).Value[0];
                switch (orientation)
                {
                    case 1:
                        break;
                    case 2:
                        Img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case 3:
                        Img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 4:
                        Img.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;
                    case 5:
                        Img.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case 6:
                        Img.RotateFlip(RotateFlipType.Rotate90FlipNone); // ištestuotas
                        break;
                    case 7:
                        Img.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case 8:
                        Img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }
                Img.RemovePropertyItem(274); //pašalinamas EXIF tagas
                                             //   Debug.WriteLine(Img.Width + " " + Img.Height);
            }
            return Img;
        }



        public static Bitmap ResizeImageStrech(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            float xDpi = image.HorizontalResolution;
            float yDpi = image.VerticalResolution;

            destImage.SetResolution(xDpi, yDpi);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }


        public static Bitmap resizeImageAspect(int newWidth, int newHeight, Image imgPhoto)
        {

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            if (sourceWidth < sourceHeight)   //  Jei vertikali
            {
                int buff = newWidth;

                newWidth = newHeight;
                newHeight = buff;
            }

            int sourceX = 0, sourceY = 0, destX = 0, destY = 0;
            float nPercent = 0, nPercentW = 0, nPercentH = 0;

            nPercentW = ((float)newWidth / (float)sourceWidth);
            nPercentH = ((float)newHeight / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((newWidth -
                          (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((newHeight -
                          (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(newWidth, newHeight,
                          System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(System.Drawing.Color.White);
            grPhoto.InterpolationMode =
            System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            imgPhoto.Dispose();

            return bmPhoto;
        }


    }
}