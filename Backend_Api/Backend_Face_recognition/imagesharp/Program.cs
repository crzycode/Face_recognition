using System.Drawing;
using System.Drawing.Imaging;

public class Program
{
    static void Main(String[] args)
    {


        Bitmap bt = new Bitmap("D:\\Images\\6426b023-121e-4fb5-bf13-d7907bf68247\\6426b023-121e-4fb5-bf13-d7907bf68247.jpeg");

        for (int y = 0; y < bt.Height; y++)
        {
            for (int x = 0; x < bt.Width; x++)
            {
                Color c = bt.GetPixel(x, y);

                int r = c.R;
                int g = c.G;
                int b = c.B;
                int avg = (r + g + b) / 3;
                bt.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
            }
        }

        bt.Save("D:\\ConsoleApp1\\ConsoleApp1\\m.bmp");

        var d = Program.sharpen(bt);
        d.Save("D:\\ConsoleApp1\\ConsoleApp1\\md.jpeg");
    }

    public static Bitmap sharpen(Bitmap image)
    {
        Bitmap sharpenImage = new Bitmap(image.Width, image.Height);

        int filterWidth = 3;
        int filterHeight = 3;
        int w = image.Width;
        int h = image.Height;

        double[,] filter = new double[filterWidth, filterHeight];

        filter[0, 0] = filter[0, 1] = filter[0, 2] = filter[1, 0] = filter[1, 2] = filter[2, 0] = filter[2, 1] = filter[2, 2] = -1;
        filter[1, 1] = 9;

        double factor = 1.0;
        double bias = 0.0;

        Color[,] result = new Color[image.Width, image.Height];

        for (int x = 0; x < w; ++x)
        {
            for (int y = 0; y < h; ++y)
            {
                double red = 0.0, green = 0.0, blue = 0.0;
                Color imageColor = image.GetPixel(x, y);

                for (int filterX = 0; filterX < filterWidth; filterX++)
                {
                    for (int filterY = 0; filterY < filterHeight; filterY++)
                    {
                        int imageX = (x - filterWidth / 2 + filterX + w) % w;
                        int imageY = (y - filterHeight / 2 + filterY + h) % h;
                        red += imageColor.R * filter[filterX, filterY];
                        green += imageColor.G * filter[filterX, filterY];
                        blue += imageColor.B * filter[filterX, filterY];
                    }
                    int r = Math.Min(Math.Max((int)(factor * red + bias), 0), 255);
                    int g = Math.Min(Math.Max((int)(factor * green + bias), 0), 255);
                    int b = Math.Min(Math.Max((int)(factor * blue + bias), 0), 255);

                    result[x, y] = Color.FromArgb(r, g, b);
                }
            }
        }
        for (int i = 0; i < w; ++i)
        {
            for (int j = 0; j < h; ++j)
            {
                sharpenImage.SetPixel(i, j, result[i, j]);
            }
        }
        return sharpenImage;
    }

}