using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;

namespace pr4
{

    public static class PuzzleCutter
    {
       //для конвертації зображення з одного формату в інший
        public static BitmapImage ConvertImageToBitmapImage(System.Drawing.Image image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                // Перетворює System.Drawing.Image на растрове зображення
                Bitmap bitmap = new Bitmap(image);

                // Зберегає растрове зображення в MemoryStream у форматі PNG
                bitmap.Save(stream, ImageFormat.Png);

               // Створює новий BitmapImage та встановлює його джерело на MemoryStream
                 BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(stream.ToArray());
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

              
                return bitmapImage;
            }
        }

    }
}
