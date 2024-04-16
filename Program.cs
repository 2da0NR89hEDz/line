using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConsoleApp1
{
    class xyv
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int V { get; set; }

        public xyv(int x, int y, int v)
        {
            X = x;
            Y = y;
            V = v;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Bitmap bmp = null;

            int expoTime = 100;
            for (int i = 0; i < 100; i++)
            {
                // set expo time

                // shot

                // download picture

                bmp = new Bitmap($"test.bmp");
                byte[] arr = BitmapToByteArray(bmp);
                var c = arr.Where(d => d == 255).Count();

                if (c >= 80 && c <= 120) { break; }
                if (c < 80) { expoTime += 10; }
                if (c > 120) { expoTime -= 10; }

            }

            // get width

            bmp = new Bitmap($"test.bmp");
            List<xyv> mat = new List<xyv>();

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    mat.Add(new xyv(x, y, bmp.GetPixel(x,y).G));
                }
            }

            var eachLineWidth = new List<int>();
            for (int x = 0; x < bmp.Width; x++)
            {
                int width = mat.Where(p => p.X == x && p.V >= 50).Count();
                if (width >= 2)
                {
                    eachLineWidth.Add(width);
                }
            }
            double aveLineWidth = eachLineWidth.Average();
            
        }

        static byte[] BitmapToByteArray(Bitmap src)
        {
            var bmpData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadOnly, src.PixelFormat);
            byte[] bytes = new byte[Math.Abs(bmpData.Stride) * src.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, bytes, 0, bytes.Length);
            src.UnlockBits(bmpData);
            return bytes;
        }

    }
}

