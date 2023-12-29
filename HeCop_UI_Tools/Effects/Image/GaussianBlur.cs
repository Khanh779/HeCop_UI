using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HecopUI_Winforms.Effects.Image
{
    public partial class GaussianBlur
    {
        public static  Bitmap DoBlurImage(Bitmap image, int blurRadius)
        {
            Bitmap blurredBitmap = image;

            // Tạo kernel (bộ lọc) dựa trên hàm Gauss
            double[,] kernel = CalculateGaussianKernel(blurRadius);

            // Áp dụng kernel để tính toán giá trị trung bình có trọng số cho từng pixel
            for (int y = blurRadius; y < image.Height - blurRadius; y++)
            {
                for (int x = blurRadius; x < image.Width - blurRadius; x++)
                {
                    Color blurredColor = CalculateWeightedAverage(image, x, y, kernel, blurRadius);
                    blurredBitmap.SetPixel(x, y, blurredColor);
                }
            }

            return blurredBitmap;
        }

        private static  double[,] CalculateGaussianKernel(int blurRadius)
        {
            int kernelSize = 2 * blurRadius + 1;
            double[,] kernel = new double[kernelSize, kernelSize];
            double sigma = blurRadius / 3.0;

            double sigma22 = 2.0 * sigma * sigma;
            double sigma22Pi = sigma22 * Math.PI;
            double sqrtSigma22Pi = Math.Sqrt(sigma22Pi);
            double sum = 0.0;

            for (int y = -blurRadius; y <= blurRadius; y++)
            {
                for (int x = -blurRadius; x <= blurRadius; x++)
                {
                    double r = Math.Sqrt(x * x + y * y);
                    kernel[y + blurRadius, x + blurRadius] = Math.Exp(-(r * r) / sigma22) / sqrtSigma22Pi;
                    sum += kernel[y + blurRadius, x + blurRadius];
                }
            }

            // Chuẩn hóa kernel để tổng các trọng số là 1
            for (int y = 0; y < kernelSize; y++)
            {
                for (int x = 0; x < kernelSize; x++)
                {
                    kernel[y, x] /= sum;
                }
            }

            return kernel;
        }

        private static Color CalculateWeightedAverage(Bitmap image, int x, int y, double[,] kernel, int blurRadius)
        {
            double totalR = 0.0, totalG = 0.0, totalB = 0.0;

            for (int offsetY = -blurRadius; offsetY <= blurRadius; offsetY++)
            {
                for (int offsetX = -blurRadius; offsetX <= blurRadius; offsetX++)
                {
                    int currentX = x + offsetX;
                    int currentY = y + offsetY;

                    Color pixelColor = image.GetPixel(currentX, currentY);
                    double weight = kernel[offsetY + blurRadius, offsetX + blurRadius];

                    totalR += pixelColor.R * weight;
                    totalG += pixelColor.G * weight;
                    totalB += pixelColor.B * weight;
                }
            }

            return Color.FromArgb((int)totalR, (int)totalG, (int)totalB);
        }
    }
}
