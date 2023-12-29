using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HecopUI_Winforms.Effects.Image
{
    public partial class BoxBlur
    {

        public static Bitmap DoBlurImage(Bitmap image, int blurRadius)
        {
            // Sao chép hình ảnh gốc để làm bản mờ
            Bitmap blurredBitmap = image;

            // Áp dụng hiệu ứng blur
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color blurredColor = CalculateBlurredColor(image, x, y, blurRadius);
                    blurredBitmap.SetPixel(x, y, blurredColor);
                }
            }

            return blurredBitmap;
        }

        private static Color CalculateBlurredColor(Bitmap image, int x, int y, int blurRadius)
        {
            int totalR = 0, totalG = 0, totalB = 0, count = 0;

            for (int offsetY = -blurRadius; offsetY <= blurRadius; offsetY++)
            {
                for (int offsetX = -blurRadius; offsetX <= blurRadius; offsetX++)
                {
                    int currentX = x + offsetX;
                    int currentY = y + offsetY;

                    if (currentX >= 0 && currentX < image.Width && currentY >= 0 && currentY < image.Height)
                    {
                        Color pixelColor = image.GetPixel(currentX, currentY);
                        totalR += pixelColor.R;
                        totalG += pixelColor.G;
                        totalB += pixelColor.B;
                        count++;
                    }
                }
            }

            int averageR = totalR / count;
            int averageG = totalG / count;
            int averageB = totalB / count;

            return Color.FromArgb(averageR, averageG, averageB);
        }
    }
}
