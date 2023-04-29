using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace pr4
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    ///   //path = _path;

    public partial class Window1 : Window
    {

        public int[,] map = new int[3, 3];
        public List<System.Drawing.Image> puzzlePieces;
        public Cut_Pie cutPie = new Cut_Pie();
        public string path;
        public string file = "/minTime.txt";
        public System.Drawing.Image image;
        BitmapImage FullImage = new BitmapImage();
        Image Full_Image = new Image();
        public TimeSpan TimePlayer = new TimeSpan();
        public TimeSpan TimeBest = new TimeSpan();
        private DispatcherTimer timer;
        private bool isPaused = true;
       

        public Window1(string _path)
        {
            InitializeComponent();
            if (!File.Exists(file))
            {
                using (File.Create(file)) ;
            }
            WithFile();
            path = _path;
            FullImage.BeginInit();
            FullImage.UriSource = new Uri(_path, UriKind.RelativeOrAbsolute);
            FullImage.EndInit();
            Full_Image.Source = FullImage;
            Grid.SetRow(Full_Image, 0);
            Grid.SetColumn(Full_Image, 1);
            Full_Grid.Children.Add(Full_Image);
            InitializeTimer();
            
            using (MemoryStream ms = new MemoryStream())
            {
                var stream = Application.GetResourceStream(new Uri(_path, UriKind.Relative)).Stream;
                stream.CopyTo(ms);
                image = System.Drawing.Image.FromStream(ms);
            }
       
            puzzlePieces = Cut_Pie.CutImage(image);
            cutPie.DisplayPuzzlePiecesOnGrid(Grid_pie, puzzlePieces, map);
           
        }
      //для ініціалізації таймера
        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
           
        }
        //відображення таймера на екрані
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimePlayer += TimeSpan.FromSeconds(1);
            TimeNaw.Text = TimePlayer.ToString(@"hh\:mm\:ss");
        }
      
        //обробка руху пазлів
        private void Grid_pie_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isPaused)
            {

                // Отримати позицію курсора миші відносно гріда
                Point position = e.GetPosition(Grid_pie);

                // Отримати номери рядка і колонки, що відповідають позиції курсора
                int row = (int)position.Y / (int)Grid_pie.RowDefinitions[0].ActualHeight;
                int column = (int)position.X / (int)Grid_pie.ColumnDefinitions[0].ActualWidth;

                // Використовує номери рядка і колонки за потреби

                Image imageInCell = Grid_pie.Children
                .Cast<Image>()
                .Where(m => Grid.GetRow(m) == row && Grid.GetColumn(m) == column)
                .FirstOrDefault();
                if (imageInCell != null)
                {
                    // Виконувати операції з зображенням
                    if (Way(row, column, imageInCell) != false)
                    {
                        map[row, column] = 8;
                        Check();
                    }
                    else
                    {
                        return;
                    }

                }
            }

        }

        //отримуємо сторону де немає пазла
        public bool Way(int row, int column, Image imageInCell)
        {

            if (row - 1 > -1 && map[row - 1, column] == 8)
            {
                Grid_pie.Children.Remove(imageInCell);
                Grid.SetRow(imageInCell, row - 1);
                Grid.SetColumn(imageInCell, column);
                Grid_pie.Children.Add(imageInCell);
                map[row - 1, column] = map[row, column];

                return true;
            }
            else if (row + 1 < 3 && map[row + 1, column] == 8)
            {
                Grid_pie.Children.Remove(imageInCell);
                Grid.SetRow(imageInCell, row + 1);
                Grid.SetColumn(imageInCell, column);
                Grid_pie.Children.Add(imageInCell);
                map[row + 1, column] = map[row, column];
                return true;
            }
            else if (column - 1 > -1 && map[row, column - 1] == 8)
            {
                Grid_pie.Children.Remove(imageInCell);
                Grid.SetRow(imageInCell, row);
                Grid.SetColumn(imageInCell, column - 1);
                Grid_pie.Children.Add(imageInCell);
                map[row, column - 1] = map[row, column];
                return true;
            }
            else if (column + 1 < 3 && map[row, column + 1] == 8)
            {
                Grid_pie.Children.Remove(imageInCell);
                Grid.SetRow(imageInCell, row);
                Grid.SetColumn(imageInCell, column + 1);
                Grid_pie.Children.Add(imageInCell);
                map[row, column + 1] = map[row, column];
                return true;
            }
            return false;

        }
        //перевірка чи всі пазли на місці
        public void Check()
        {
            int i = 0;
            int j = 0;
            int p = 0;
            for (int k = 0; k < puzzlePieces.Count; k++)
            {
                if (map[i, j] == k)
                {
                    p++;
                }
                j++;
                if (j > 2)
                {
                    j = 0;
                    i++;
                }
            }
            if (p == 9)
            {
              
                Full_Grid.Children.Remove(Full_Image);
                Grid.SetRow(Full_Image, 1);
                Grid.SetColumn(Full_Image, 1);
                Grid_image.Children.Add(Full_Image);
                timer.Stop();
                MessageBox.Show($"You won, your result {TimeNaw.Text}", "Congratulation", MessageBoxButton.OK, MessageBoxImage.Information);
                //запис у файл
                if (TimeBest == TimeSpan.Zero || TimePlayer < TimeBest)
                {
                    InFile();
                }

                //завершити або продовжити
                MessageBoxResult result = MessageBox.Show("To return to the main menu click yes, to end the game click no",
                                                     "Warning", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.No)
                {
                    DialogResult = true;
                }
                this.Close();
            }

        }
        //зчитування рекорду з файлу
        public void WithFile()
        {
            using (StreamReader fileReader = new StreamReader(file))
            {
                string timeString = fileReader.ReadLine();
                if (!string.IsNullOrEmpty(timeString))
                {
                    TimeBest = TimeSpan.Parse(timeString);
                }
                else
                {
                    // обробка неправильного формату рядка
                    TimeBest = new TimeSpan(00, 00, 00);
                }
            }
            TheBest.Text = TimeBest.ToString();
        }
        //помістити рекорд  у файл
        public void InFile()
        {
            using (StreamWriter fileWriter = new StreamWriter(file))
            {
                fileWriter.WriteLine(TimePlayer.ToString());
            }
        }

        //обробка виходу
        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit? The result will not be saved.",
                                                     "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        
        //пауза, старт
        private void Paused_Click(object sender, RoutedEventArgs e)
        {
            
            if(isPaused == false)
            {
                timer.Start();
                isPaused = true;
                Paused.Content = " Pause";
            }
            else
            {
                timer.Stop();
                isPaused =false;
                Paused.Content = "Start";
            }
        }
    }
}

