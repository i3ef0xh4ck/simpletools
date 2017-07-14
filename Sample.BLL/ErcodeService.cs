using Sample.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace Sample.BLL
{
    public class ErcodeService
    {
        /// <summary>
        /// 生成二维码(ZXing)
        /// </summary>
        public static Image GenerateStream(string text, int scale, int margin, string level, bool isTrans) {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.DisableECI = true;
            options.CharacterSet = "UTF-8";
            int width, height;
            width = height = (scale >= 800 ? 800 : scale);
            options.Width = width;
            options.Height = height;
            options.Margin = margin;

            switch (level) {
                case "L":
                    options.ErrorCorrection = ErrorCorrectionLevel.L;
                    break;
                case "M":
                    options.ErrorCorrection = ErrorCorrectionLevel.M;
                    break;
                case "Q":
                    options.ErrorCorrection = ErrorCorrectionLevel.Q;
                    break;
                case "H":
                    options.ErrorCorrection = ErrorCorrectionLevel.H;
                    break;
                default:
                    options.ErrorCorrection = ErrorCorrectionLevel.L;
                    break;
            }
            writer.Options = options;

            Bitmap bmap = writer.Write(text);
            if (isTrans) {
                bmap.MakeTransparent(Color.White);
            }
            return bmap;
        }

        /// <summary>
        /// 识别二维码（ZXing）
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static string ReadErCode(string filename) {
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";
            Bitmap map = new Bitmap(filename);
            Result result = reader.Decode(map);
            return result == null ? "" : result.Text;
        }
    }
}
