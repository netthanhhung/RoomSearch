using System;
using System.Web;
using RoomSearch.Common;
using System.Configuration;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace RoomSearch.Web.UI
{
    public partial class UtilityHelper
    {
        public static MemoryStream ResizeFromStream(int maxSideSize, Stream inputBuffer)
        {
            int intNewWidth;
            int intNewHeight;
            System.Drawing.Image imgInput = System.Drawing.Image.FromStream(inputBuffer);

            // GET IMAGE FORMAT
            ImageFormat fmtImageFormat = imgInput.RawFormat;

            // GET ORIGINAL WIDTH AND HEIGHT
            int intOldWidth = imgInput.Width;
            int intOldHeight = imgInput.Height;

            // IS LANDSCAPE OR PORTRAIT ?? 
            int intMaxSide;

            if (intOldWidth >= intOldHeight)
            {
                intMaxSide = intOldWidth;
            }
            else
            {
                intMaxSide = intOldHeight;
            }


            if (intMaxSide > maxSideSize)
            {
                // SET NEW WIDTH AND HEIGHT
                double dblCoef = maxSideSize / (double)intMaxSide;
                intNewWidth = Convert.ToInt32(dblCoef * intOldWidth);
                intNewHeight = Convert.ToInt32(dblCoef * intOldHeight);
            }
            else
            {
                intNewWidth = intOldWidth;
                intNewHeight = intOldHeight;
            }

            MemoryStream outputStream = new MemoryStream();
            using (System.Drawing.Image img = System.Drawing.Image.FromStream(inputBuffer))
            {
                using (Bitmap bitmap = new Bitmap(img, intNewWidth, intNewHeight))
                {
                    bitmap.Save(outputStream, ImageFormat.Jpeg);
                }
            }

            return outputStream;
        }

        

    }
}
