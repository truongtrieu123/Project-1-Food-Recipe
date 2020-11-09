using System;
using System.IO;
using System.Windows;
using Path = System.IO.Path;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string path1 = AppDomain.CurrentDomain.BaseDirectory;
            string path2 = Path.Combine(path1 + "Demo");
            Directory.CreateDirectory(path2);
            
            //if(!Directory.Exists(path2))
            //{
            //    Directory.CreateDirectory(path2);
            //}
            //else
            //{
            //    Directory.Delete(path2);
            //}
            this.Close();
        }

    }
}
