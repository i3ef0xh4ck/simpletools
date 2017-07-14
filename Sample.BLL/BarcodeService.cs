using Sample.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;

namespace Sample.BLL
{
    public class BarcodeService
    {
        /// <summary>
        /// 生成条码（ZXing）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="isTrans"></param>
        /// <returns></returns>
        public static Image GenerateStream(string code, string type, int width, int height, bool isTrans) {
            BarcodeWriter writer = new BarcodeWriter();
            BarcodeFormat t = (BarcodeFormat)Enum.Parse(typeof(BarcodeFormat), type);
            writer.Format = t;
            EncodingOptions options = new EncodingOptions() {
                Width = width > 500 ? 500 : width,
                Height = height > 500 ? 500 : height,
                Margin = 2
            };
            writer.Options = options;
            Bitmap bmap = null;
            try {
                bmap = writer.Write(code);
                if (isTrans) {
                    bmap.MakeTransparent(Color.White);
                }
            } catch {

            }
            return bmap;
        }
    }
}
