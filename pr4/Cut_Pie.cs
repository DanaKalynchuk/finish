using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace pr4
{
    public class Cut_Pie
    {
       
        public List<ImageSource> puzzlePieces = new List<ImageSource>();
        public Cut_Pie()
        {

        }
        //нарізка пазлів з картинок
        public static List<System.Drawing.Image> CutImage(System.Drawing.Image image)
        {
            List<System.Drawing.Image> puzzlePieces = new List<System.Drawing.Image>();

            int width = image.Width / 3;
            int height = image.Height / 3;

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, height);
                    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
                    g.DrawImage(image, new System.Drawing.Rectangle(0, 0, width, height), new System.Drawing.Rectangle(x * width, y * height, width, height), System.Drawing.GraphicsUnit.Pixel);
                    g.Dispose();
                    puzzlePieces.Add(bmp);
                }
            }

            return puzzlePieces;
        }
        //ставимо у грід кожний окремий пазл
        public void DisplayPuzzlePiecesOnGrid(Grid grid, List<System.Drawing.Image> puzzlePieces, int[,] map)
        {
            int i = 2;
            int j = 2;
            int k = 0;

            foreach (var item in puzzlePieces)
            {
                var puzzleImage = new System.Windows.Controls.Image();
                puzzleImage.Source = PuzzleCutter.ConvertImageToBitmapImage(item as System.Drawing.Image);
                Grid.SetRow(puzzleImage, i);
                Grid.SetColumn(puzzleImage, j);
                grid.Children.Add(puzzleImage);

                map[i, j] = k;
                k++;
                if (map[i, j] == 8)
                {
                    grid.Children.Remove(puzzleImage);
                }
                j--;
                if (j < 0)
                {
                    j = 2;
                    i--;
                }

            }


        }




    }
    }
