using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple
{
    public class ImageUtils
    {
        #region 根据路径得到图片尺寸
        /// <summary>
        /// 根据路径得到图片尺寸
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <returns>尺寸结构</returns>
        public static Size GetImageSize(string path)
        {
            Size s = new Size(0, 0);
            if (System.IO.File.Exists(path))
            {
                Image img = Image.FromFile(path);
                s.Width = img.Width;
                s.Height = img.Height;
            }
            return s;
        }
        #endregion

        #region##对给定的一个图片路径生成一个指定大小的缩略图
        ///<summary>
        /// 对给定的一个图片路径生成一个指定大小的缩略图
        ///</summary>
        ///<param name="filename">原始图片路径</param>
        ///<param name="thumMaxWidth">缩略图的宽度</param>
        ///<param name="thumMaxHeight">缩略图的高度</param>
        ///<returns>返回缩略图的Image对象</returns>
        public static Image GetThumbNailImage(string filename, int thumMaxWidth, int thumMaxHeight)
        {
            if (!File.Exists(filename)) return null;

            Image image = Image.FromFile(filename);

            return GetThumbNailImage(image, thumMaxWidth, thumMaxHeight);
        }
        #endregion

        #region##对给定的一个图片（Image对象）生成一个指定大小的缩略图
        ///<summary>
        /// 对给定的一个图片（Image对象）生成一个指定大小的缩略图。
        ///</summary>
        ///<param name="originalImage">原始图片</param>
        ///<param name="thumMaxWidth">缩略图的宽度</param>
        ///<param name="thumMaxHeight">缩略图的高度</param>
        ///<returns>返回缩略图的Image对象</returns>
        public static Image GetThumbNailImage(Image originalImage, int thumMaxWidth, int thumMaxHeight)
        {
            Size thumRealSize = Size.Empty;
            Image newImage = originalImage;
            Graphics graphics = null;
            try
            {
                thumRealSize = GetNewSize(thumMaxWidth, thumMaxHeight, originalImage.Width, originalImage.Height);
                newImage = new Bitmap(thumRealSize.Width, thumRealSize.Height);
                graphics = Graphics.FromImage(newImage);
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(originalImage, new Rectangle(0, 0, thumRealSize.Width, thumRealSize.Height), new Rectangle(0, 0, originalImage.Width, originalImage.Height), GraphicsUnit.Pixel);
            }
            catch { }
            finally
            {
                if (graphics != null)
                {
                    graphics.Dispose();
                    graphics = null;
                }
            }
            return newImage;
        }
        #endregion

        #region##获取一个图片按等比例缩小后的大小
        ///<summary>
        /// 获取一个图片按等比例缩小后的大小。
        ///</summary>
        ///<param name="maxWidth">需要缩小到的宽度</param>
        ///<param name="maxHeight">需要缩小到的高度</param>
        ///<param name="imageOriginalWidth">图片的原始宽度</param>
        ///<param name="imageOriginalHeight">图片的原始高度</param>
        ///<returns>返回图片按等比例缩小后的实际大小</returns>
        public static Size GetNewSize(int maxWidth, int maxHeight, int imageOriginalWidth, int imageOriginalHeight)
        {
            double w = 0.0;
            double h = 0.0;
            double sw = Convert.ToDouble(imageOriginalWidth);
            double sh = Convert.ToDouble(imageOriginalHeight);
            double mw = Convert.ToDouble(maxWidth);
            double mh = Convert.ToDouble(maxHeight);
            if (sw < mw && sh < mh)
            {
                w = sw;
                h = sh;
            }
            else if ((sw / sh) > (mw / mh))
            {
                w = maxWidth;
                h = (w * sh) / sw;
            }
            else
            {
                h = maxHeight;
                w = (h * sw) / sh;
            }
            return new Size(Convert.ToInt32(w), Convert.ToInt32(h));
        }
        #endregion

        #region 压缩图片
        /// <summary>
        /// 压缩并存储图片
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="path">存储路径</param>
        public static void CompressSave(Image image, string path)
        {
            if (image == null || string.IsNullOrEmpty(path))
                return;
            try
            {
                ImageCodecInfo codeInfo = ImageUtils.GetImageCoderInfo("image/jpeg");
                System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters paramters = new EncoderParameters(1);
                EncoderParameter paramter = new EncoderParameter(encoder, 100L);
                paramters.Param[0] = paramter;
                image.Save(path, codeInfo, paramters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (image != null)
                    image.Dispose();
            }
        }

        /// <summary>
        /// 压缩并存储图片
        /// </summary>
        /// <param name="sourcePath">源图片位置</param>
        /// <param name="destPath">目标图片位置</param>
        public static void CompressSave(string sourcePath, string destPath)
        {
            if (!File.Exists(sourcePath)) return;
            Image image = Image.FromFile(sourcePath);
            CompressSave(image, destPath);
        }

        public static void Compress(string filePath, string filePath_ystp)
        {
            Bitmap bmp = null;
            ImageCodecInfo ici = null;
            System.Drawing.Imaging.Encoder ecd = null;
            EncoderParameter ept = null;
            EncoderParameters eptS = null;
            try
            {
                bmp = new Bitmap(filePath);
                ici = GetImageCoderInfo("image/jpeg");
                ecd = System.Drawing.Imaging.Encoder.Quality;
                eptS = new EncoderParameters(1);
                ept = new EncoderParameter(ecd, 100L);
                eptS.Param[0] = ept;
                bmp.Save(filePath_ystp, ici, eptS);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                bmp.Dispose();
                ept.Dispose();
                eptS.Dispose();
            }
        }

        /// <summary>
        /// 获取图片编码类型信息
        /// </summary>
        /// <param name="coderType"></param>
        /// <returns></returns>
        public static ImageCodecInfo GetImageCoderInfo(string coderType)
        {
            ImageCodecInfo[] iciS = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo retIci = null;
            foreach (ImageCodecInfo ici in iciS)
            {
                if (ici.MimeType.Equals(coderType))
                    retIci = ici;
            }
            return retIci;
        }
        #endregion

        #region 旋转图片
        /// <summary>
        /// 旋转图片
        /// </summary>
        /// <param name="image">源图片</param>
        /// <param name="angle">角度</param>
        /// <returns></returns>
        public static Bitmap RotateImage(Image image, float angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            const double pi2 = Math.PI / 2.0;

            // Why can't C# allow these to be const, or at least readonly
            // *sigh*  I'm starting to talk like Christian Graus :omg:
            double oldWidth = (double)image.Width;
            double oldHeight = (double)image.Height;

            // Convert degrees to radians
            double theta = ((double)angle) * Math.PI / 180.0;
            double locked_theta = theta;

            // Ensure theta is now [0, 2pi)
            while (locked_theta < 0.0)
                locked_theta += 2 * Math.PI;

            double newWidth, newHeight;
            int nWidth, nHeight; // The newWidth/newHeight expressed as ints


            #region Explaination of the calculations
            /**/
            /*
         * The trig involved in calculating the new width and height
         * is fairly simple; the hard part was remembering that when 
         * PI/2 <= theta <= PI and 3PI/2 <= theta < 2PI the width and 
         * height are switched.
         * 
         * When you rotate a rectangle, r, the bounding box surrounding r
         * contains for right-triangles of empty space.  Each of the 
         * triangles hypotenuse's are a known length, either the width or
         * the height of r.  Because we know the length of the hypotenuse
         * and we have a known angle of rotation, we can use the trig
         * function identities to find the length of the other two sides.
         * 
         * sine = opposite/hypotenuse
         * cosine = adjacent/hypotenuse
         * 
         * solving for the unknown we get
         * 
         * opposite = sine * hypotenuse
         * adjacent = cosine * hypotenuse
         * 
         * Another interesting point about these triangles is that there
         * are only two different triangles. The proof for which is easy
         * to see, but its been too long since I've written a proof that
         * I can't explain it well enough to want to publish it.  
         * 
         * Just trust me when I say the triangles formed by the lengths 
         * width are always the same (for a given theta) and the same 
         * goes for the height of r.
         * 
         * Rather than associate the opposite/adjacent sides with the
         * width and height of the original bitmap, I'll associate them
         * based on their position.
         * 
         * adjacent/oppositeTop will refer to the triangles making up the 
         * upper right and lower left corners
         * 
         * adjacent/oppositeBottom will refer to the triangles making up 
         * the upper left and lower right corners
         * 
         * The names are based on the right side corners, because thats 
         * where I did my work on paper (the right side).
         * 
         * Now if you draw this out, you will see that the width of the 
         * bounding box is calculated by adding together adjacentTop and 
         * oppositeBottom while the height is calculate by adding 
         * together adjacentBottom and oppositeTop.
         */
            #endregion

            double adjacentTop, oppositeTop;
            double adjacentBottom, oppositeBottom;

            // We need to calculate the sides of the triangles based
            // on how much rotation is being done to the bitmap.
            //   Refer to the first paragraph in the explaination above for 
            //   reasons why.
            if ((locked_theta >= 0.0 && locked_theta < pi2) ||
                (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2)))
            {
                adjacentTop = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
                oppositeTop = Math.Abs(Math.Sin(locked_theta)) * oldWidth;

                adjacentBottom = Math.Abs(Math.Cos(locked_theta)) * oldHeight;
                oppositeBottom = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
            }
            else
            {
                adjacentTop = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
                oppositeTop = Math.Abs(Math.Cos(locked_theta)) * oldHeight;

                adjacentBottom = Math.Abs(Math.Sin(locked_theta)) * oldWidth;
                oppositeBottom = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
            }

            newWidth = adjacentTop + oppositeBottom;
            newHeight = adjacentBottom + oppositeTop;

            nWidth = (int)Math.Ceiling(newWidth);
            nHeight = (int)Math.Ceiling(newHeight);

            Bitmap rotatedBmp = new Bitmap(nWidth, nHeight);

            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {
                // This array will be used to pass in the three points that 
                // make up the rotated image
                Point[] points;

                /**/
                /*
             * The values of opposite/adjacentTop/Bottom are referring to 
             * fixed locations instead of in relation to the
             * rotating image so I need to change which values are used
             * based on the how much the image is rotating.
             * 
             * For each point, one of the coordinates will always be 0, 
             * nWidth, or nHeight.  This because the Bitmap we are drawing on
             * is the bounding box for the rotated bitmap.  If both of the 
             * corrdinates for any of the given points wasn't in the set above
             * then the bitmap we are drawing on WOULDN'T be the bounding box
             * as required.
             */
                if (locked_theta >= 0.0 && locked_theta < pi2)
                {
                    points = new Point[] { 
                                             new Point( (int) oppositeBottom, 0 ), 
                                             new Point( nWidth, (int) oppositeTop ),
                                             new Point( 0, (int) adjacentBottom )
                                         };

                }
                else if (locked_theta >= pi2 && locked_theta < Math.PI)
                {
                    points = new Point[] { 
                                             new Point( nWidth, (int) oppositeTop ),
                                             new Point( (int) adjacentTop, nHeight ),
                                             new Point( (int) oppositeBottom, 0 )                         
                                         };
                }
                else if (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2))
                {
                    points = new Point[] { 
                                             new Point( (int) adjacentTop, nHeight ), 
                                             new Point( 0, (int) adjacentBottom ),
                                             new Point( nWidth, (int) oppositeTop )
                                         };
                }
                else
                {
                    points = new Point[] { 
                                             new Point( 0, (int) adjacentBottom ), 
                                             new Point( (int) oppositeBottom, 0 ),
                                             new Point( (int) adjacentTop, nHeight )        
                                         };
                }

                g.DrawImage(image, points);
            }

            return rotatedBmp;
        }
        #endregion

        #region 图片Exif
        /// <summary>
        /// 判断是否是苹果拍摄图片，如果是，则顺时针旋转90度
        /// </summary>
        /// <returns></returns>
        public static Image AppleImage(Image image)
        {
            Dictionary<string, string> exifs = ReadExif(image);
            if (exifs != null && exifs.Count > 0)
            {
                if (exifs.Keys.FirstOrDefault(m => m == "Make") != null)
                {
                    string make = exifs["Make"];
                    if (!string.IsNullOrEmpty(make) && make.ToLower().Contains("apple"))
                        image = RotateImage(image, 90);
                }
            }
            return image;
        }

        /// <summary>
        /// 读取图片Exif信息
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ReadExif(Image image)
        {
            var exif = new Dictionary<string, string>();
            var properties = image.PropertyItems;
            foreach (var property in properties)
            {
                switch (property.Id)
                {
                    case 0x010E:
                        exif["ImageTitle"] = ASCIIToString(property.Value);
                        break;
                    case 0x010F:
                        exif["Make"] = ASCIIToString(property.Value);
                        break;
                    case 0x0110:
                        exif["Model"] = ASCIIToString(property.Value);
                        break;
                    case 0x0112:
                        exif["Orientation"] = ShortToString(property.Value, 0);
                        break;
                    case 0x011A:
                        exif["XResolution"] = RationalToSingle(property.Value, 0);
                        break;
                    case 0x011B:
                        exif["YResolution"] = RationalToSingle(property.Value, 0);
                        break;
                    case 0x0128:
                        exif["ResolutionUnit"] = ShortToString(property.Value, 0);
                        break;
                    case 0x0131:
                        exif["Software"] = ASCIIToString(property.Value);
                        break;
                    case 0x0132:
                        exif["DateTime"] = ASCIIToString(property.Value);
                        break;
                    //GPS
                    case 0x0002:
                        exif["GPSLatitude"] = string.Format("{0}°{1}′{2}″",
                                                            RationalToSingle(property.Value, 0),
                                                            RationalToSingle(property.Value, 8),
                                                            RationalToSingle(property.Value, 16)
                                                            );
                        break;
                    case 0x0004:
                        exif["GPSLongitude"] = string.Format("{0}°{1}′{2}″",
                                                            RationalToSingle(property.Value, 0),
                                                            RationalToSingle(property.Value, 8),
                                                            RationalToSingle(property.Value, 16)
                                                            );
                        break;
                    case 0x0006:
                        exif["GPSAltitude"] = RationalToSingle(property.Value, 0);
                        break;
                }
            }
            return exif;
        }

        static string ByteToString(byte[] b, int startindex)
        {
            if (startindex + 1 <= b.Length)
                return ((char)b[startindex]).ToString();
            else
                return string.Empty;
        }

        static string ShortToString(byte[] b, int startindex)
        {
            if (startindex + 2 <= b.Length)
                return BitConverter.ToInt16(b, startindex).ToString();
            else
                return string.Empty;
        }

        static string RationalToSingle(byte[] b, int startindex)
        {
            if (startindex + 8 <= b.Length)
                return (BitConverter.ToSingle(b, startindex) / BitConverter.ToSingle(b, startindex + 4)).ToString();
            else
                return string.Empty;
        }

        static string ASCIIToString(byte[] b)
        {
            return Encoding.ASCII.GetString(b);
        }
        #endregion
    }
}
