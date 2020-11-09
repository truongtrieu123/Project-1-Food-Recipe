using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var screen = new OpenFileDialog();
            var folder = AppDomain.CurrentDomain.BaseDirectory;

            screen.InitialDirectory = folder;
            screen.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            screen.FilterIndex = 2;
            screen.RestoreDirectory = true;

            if (screen.ShowDialog() == true)
            {
                var filepath = screen.FileName;
                Uri bitmap = new Uri(filepath);
                foodImageBox.Source = new BitmapImage(bitmap);
            }
        }
    }
}
