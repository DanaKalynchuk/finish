using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Images Pictures = new Images();
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            Button_1.Background = new ImageBrush(new BitmapImage(new Uri(Pictures.imagePathsForButton[0])));
            Button_2.Background = new ImageBrush(new BitmapImage(new Uri(Pictures.imagePathsForButton[1])));
            Button_3.Background = new ImageBrush(new BitmapImage(new Uri(Pictures.imagePathsForButton[2])));
            Button_4.Background = new ImageBrush(new BitmapImage(new Uri(Pictures.imagePathsForButton[3])));
            Button_5.Background = new ImageBrush(new BitmapImage(new Uri(Pictures.imagePathsForButton[4])));
            Button_6.Background = new ImageBrush(new BitmapImage(new Uri(Pictures.imagePathsForButton[5])));
            Button_7.Background = new ImageBrush(new BitmapImage(new Uri(Pictures.imagePathsForButton[6])));
            Button_8.Background = new ImageBrush(new BitmapImage(new Uri(Pictures.imagePathsForButton[7])));
        }

        // обробка кожної окремої картинки і передання шляху у інше вікно
        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1(Pictures.imagePaths[0]);
            NewWindow(window);
        }

        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1(Pictures.imagePaths[1]);
            NewWindow(window);
        }

        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1(Pictures.imagePaths[2]);
            NewWindow(window);
        }

        private void Button_4_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1(Pictures.imagePaths[3]);
            NewWindow(window);
        }

        private void Button_5_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1(Pictures.imagePaths[4]);
            NewWindow(window);
        }

        private void Button_6_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1(Pictures.imagePaths[5]);
            NewWindow(window);
        }

        private void Button_7_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1(Pictures.imagePaths[6]);
            NewWindow(window);
        }

        private void Button_8_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1(Pictures.imagePaths[7]);
            NewWindow(window);
        }
        public void NewWindow(Window1 window)
        {
            bool? dialogResult = window.ShowDialog();
            if (dialogResult == true)
            {
                this.Close();
            }
        }
    }
}

