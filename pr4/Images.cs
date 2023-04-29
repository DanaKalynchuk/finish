using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr4
{
    public class Images
    {
        //зберігає шлях файлів  у двох колекціях тому що кнопки хотіли інший шлях
        public ObservableCollection<string> imagePaths = new ObservableCollection<string>();
        public ObservableCollection<string> imagePathsForButton = new ObservableCollection<string>();
        public Images()
        {
            imagePathsForButton.Add(@"pack://application:,,,/Resources/bear.jpg");
            imagePathsForButton.Add(@"pack://application:,,,/Resources/Coronela.jpg");
            imagePathsForButton.Add(@"pack://application:,,,/Resources/Ichthyosaura.jpg");
            imagePathsForButton.Add(@"pack://application:,,,/Resources/Lissotriton.jpg");
            imagePathsForButton.Add(@"pack://application:,,,/Resources/Mustela.png");
            imagePathsForButton.Add(@"pack://application:,,,/Resources/Pic1.jpg");
            imagePathsForButton.Add(@"pack://application:,,,/Resources/Picus.jpg");
            imagePathsForButton.Add(@"pack://application:,,,/Resources/Strix.jpg");

            imagePaths.Add(@"/pr4;component/Resources/bear.jpg");
            imagePaths.Add(@"/pr4;component/Resources/Coronela.jpg");
            imagePaths.Add(@"/pr4;component/Resources/Ichthyosaura.jpg");
            imagePaths.Add(@"/pr4;component/Resources/Lissotriton.jpg");
            imagePaths.Add(@"/pr4;component/Resources/Mustela.png");
            imagePaths.Add(@"/pr4;component/Resources/Pic1.jpg");
            imagePaths.Add(@"/pr4;component/Resources/Picus.jpg");
            imagePaths.Add(@"/pr4;component/Resources/Strix.jpg");
        }
    }
}
