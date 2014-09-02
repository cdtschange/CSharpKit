using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace Cdts.Utility.GDI
{
    /// <summary>
    /// 图像黑白处理
    /// </summary>
    public class ImageProcessing
    {
        /// <summary>
        /// 黑白处理
        /// 最大值法: 使每个像素点的 R, G, B 值等于原像素点的 RGB (颜色值) 中最大的一个
        /// </summary>
        public static Bitmap BlackWhite(Bitmap bitmap)
        {
            Bitmap curBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            int width = bitmap.Width;
            int height = bitmap.Height;
            for (int i = 0; i < width; i++) //这里如果用i<curBitmap.Width做循环对性能有影响
            {
                for (int j = 0; j < height; j++)
                {
                    Color curColor = bitmap.GetPixel(i, j);
                    int ret = (int)(curColor.R * 0.299 + curColor.G * 0.587 + curColor.B * 0.114);
                    curBitmap.SetPixel(i, j, Color.FromArgb(ret, ret, ret));
                }
            }
            return curBitmap;
        }

        /// <summary>
        /// 逆反处理
        /// 逆反处理的原理很简单，用255减去该像素的RGB作为新的RGB值即可。
        /// g(i,j)=255-f(i,j)
        /// </summary>
        public static Bitmap Rebellious(Bitmap bitmap)
        {
            Bitmap curBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            int width = bitmap.Width;
            int height = bitmap.Height;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color c = bitmap.GetPixel(i, j);
                    int r = 255 - c.R;
                    int g = 255 - c.G;
                    int b = 255 - c.B;

                    curBitmap.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }
            return curBitmap;
        }

        /// <summary>
        /// 二值处理
        /// 二值处理，顾名思义，将图片处理后就剩下二值了，0、255就是RGB取值的极限值，
        /// 图片只剩下黑白二色
        /// 二值处理为图像灰度彩色变黑白灰度处理的一个子集
        /// 只不过值就剩下0和255了，因此处理方法有些类似。
        /// 进行加权或取平均值后进行极端化，若平均值大于等于128则255，否则0.
        /// </summary>
        public static Bitmap TwoValue(Bitmap bitmap)
        {
            Bitmap curBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            int width = bitmap.Width;
            int height = bitmap.Height;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color c = bitmap.GetPixel(i, j);
                    int iAvg = (c.R + c.G + c.B) / 3;
                    int iPixel = 0;
                    if (iAvg >= 128)
                    {
                        iPixel = 255;
                    }
                    else
                    {
                        iPixel = 0;
                    }

                    curBitmap.SetPixel(i, j, Color.FromArgb(iPixel, iPixel, iPixel));
                }
            }
            return curBitmap;
        }

        /// <summary>
        /// 雾化效果
        /// 图像的雾化处理不是基于图像中像素点之间的计算,而是给图像像素的颜色值引入一定的随机值, 
        /// 使图像具有毛玻璃带水雾般的效果
        /// 对每个像素A(i,j)进行处理，用其周围一定范围内随机点A(i+d,j+d)的像素替代
        /// 显然，以该点为圆心的圆半径越大，则雾化效果越明显
        /// </summary>
        public static Bitmap Fog(Bitmap bitmap, int delta = 7)
        {
            Bitmap curBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            int width = bitmap.Width;
            int height = bitmap.Height;
            Random rnd = new Random();
            for (int x = 0; x < width - 1; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {
                    int k = rnd.Next(-12345, 12345);
                    //像素块大小
                    int dx = x + k % delta;
                    int dy = y + k % delta;
                    //处理溢出
                    if (dx >= width)
                        dx = width - 1;
                    if (dy >= height)
                        dy = height - 1;
                    if (dx < 0)
                        dx = 0;
                    if (dy < 0)
                        dy = 0;

                    Color c1 = bitmap.GetPixel(dx, dy);
                    curBitmap.SetPixel(x, y, c1);
                }
            }
            return curBitmap;
        }

        /// <summary>
        /// 霓虹处理
        /// 霓虹处理算法：同样以3*3的点阵为例，目标像素g(i,j)应当以f(i,j)与f(i,j+1)，f(i,j)与f(i+1,j)的梯度作为R,G,B分量，我们不妨设f(i,j)的RGB分量为(r1, g1, b1), f(i,j+1)为(r2, g2, b2), f(i+1,j)为(r3, g3, b3), g(i, j)为(r, g, b),那么结果应该为
        /// r = 2 * sqrt( (r1 - r2)^2 + (r1 - r3)^2 )
        /// g = 2 * sqrt( (g1 - g2)^2 + (g1 - g3)^2 )
        /// b = 2 * sqrt( (b1 - b2)^2 + (b1 - b3)^2 )
        /// f(i,j)=2*sqrt[(f(i,j)-f(i+1,j))^2+(f(i,j)-f(,j+1))^2]
        /// </summary>
        public static Bitmap Neon(Bitmap bitmap)
        {
            Bitmap curBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            int width = bitmap.Width;
            int height = bitmap.Height;
            for (int i = 0; i < width - 1; i++)//注意边界的控制
            {
                for (int j = 0; j < height - 1; j++)
                {
                    Color cc1 = bitmap.GetPixel(i, j);
                    Color cc2 = bitmap.GetPixel(i, j + 1);
                    Color cc3 = bitmap.GetPixel(i + 1, j);

                    int rr = 2 * (int)Math.Sqrt((cc3.R - cc1.R) * (cc3.R - cc1.R) + (cc2.R - cc1.R) * (cc2.R - cc1.R));
                    int gg = 2 * (int)Math.Sqrt((cc3.G - cc1.G) * (cc3.G - cc1.G) + (cc2.G - cc1.G) * (cc2.G - cc1.G));
                    int bb = 2 * (int)Math.Sqrt((cc3.B - cc1.B) * (cc3.B - cc1.B) + (cc2.B - cc1.B) * (cc2.B - cc1.B));

                    if (rr > 255) rr = 255;
                    if (gg > 255) gg = 255;
                    if (bb > 255) bb = 255;
                    curBitmap.SetPixel(i, j, Color.FromArgb(rr, gg, bb));
                }
            }
            return curBitmap;
        }

        /// <summary>
        /// 浮雕处理
        /// 浮雕处理原理：通过对图像像素点的像素值与相邻像素点的像素值相减后加上128, 然后作为新的像素点的值
        /// g(i,j)=f(i,j)-f(i+1,j)+128
        /// </summary>
        public static Bitmap Relief(Bitmap bitmap)
        {
            Bitmap curBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            int width = bitmap.Width;
            int height = bitmap.Height;
            for (int i = 0; i < width - 1; i++)//注意控制边界  相邻元素 i+1=width
            {
                for (int j = 0; j < height; j++)
                {
                    Color c1 = bitmap.GetPixel(i, j);
                    Color c2 = bitmap.GetPixel(i + 1, j);//相邻的像素
                    int rr = c1.R - c2.R + 128;
                    int gg = c1.G - c2.G + 128;
                    int bb = c1.B - c2.B + 128;

                    //处理溢出
                    if (rr > 255) rr = 255;
                    if (rr < 0) rr = 0;
                    if (gg > 255) gg = 255;
                    if (gg < 0) gg = 0;
                    if (bb > 255) bb = 255;
                    if (bb < 0) bb = 0;

                    curBitmap.SetPixel(i, j, Color.FromArgb(rr, gg, bb));
                }
            }
            return curBitmap;
        }

        /// <summary>
        /// 马赛克
        /// 是把一张图片分割成若干个N * N像素的小区块（可能在边缘有零星的小块，但不影响整体算法）
        /// ，每个小区块的颜色都是相同的。
        /// </summary>
        public static Bitmap Mosaic(Bitmap bitmap, int size = 5)
        {
            Bitmap curBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            int width = bitmap.Width;
            int height = bitmap.Height;
            int r = 0, g = 0, b = 0;
            Color c;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    if (y % size == 0)//效果粒度，值越大码越严重
                    {
                        if (x % size == 0)//整数倍时，取像素赋值
                        {
                            c = bitmap.GetPixel(x, y);
                            r = c.R;
                            g = c.G;
                            b = c.B;
                        }
                        curBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                    else //复制上一行
                    {
                        Color colorPreLine = curBitmap.GetPixel(x, y - 1);
                        curBitmap.SetPixel(x, y, colorPreLine);
                    }
                }
            }
            return curBitmap;
        }

    }
}
