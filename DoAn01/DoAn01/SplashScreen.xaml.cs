using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace DoAn01
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public delegate void DeathHandler();
        public event DeathHandler Dying;

        public SplashScreen()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var value = ConfigurationManager.AppSettings["ShowSplashScreen"];
            var showSplash = bool.Parse(value);

            if (showSplash == false)
            {
                var screen = new MainWindow();
                screen.Show();
                this.Close();
            }
            else
            {
                test();
                timer = new System.Timers.Timer();
                timer.Elapsed += Timer_Elapsed;
                timer.Interval = 1000;
                timer.Start();
            }
        }

        System.Timers.Timer timer;
        int count = 0;
        int target = 10;

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            count++;
            if (count == target)
            {
                timer.Stop();


                Dispatcher.Invoke(() =>
                {
                    var screen = new MainWindow();
                    screen.Show();

                    this.Close();
                });

            }

            Dispatcher.Invoke(() =>
            {
                progress.Value = count;
            });
        }

        private void turnOffCheckBox_Click(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();           
            Dying?.Invoke();
            var screen = new MainWindow();
            screen.Show();
            this.Close();
        }

        private void test()
        {
            var bitmap = new BitmapImage(new Uri($"Images/food01.jpg",UriKind.Relative));
            foodImage.Source = bitmap;
            //var folder = AppDomain.CurrentDomain.BaseDirectory;
            //var filepath = $"Data/infoFood.txt";
            //using (var reader = new StreamReader("Data/infoFood.txt"))
            //{
            //    while (!reader.EndOfStream)
            //    {
            //        var line = reader.ReadLine();
            //        Dispatcher.Invoke(() =>
            //        {
            //            infoFood.Text = line;
            //        });

            //    }
            //}

            infoFood.Text = "Thịt kho hột vịt là món ăn đặc trưng của ngày Tết cổ truyền nhưng cũng rất quen thuộc trong các bữa ăn hằng ngày của mọi gia đình. Món ăn chỉ với 2 nguyên liệu chính là thịt ba rọi và trứng vịt nhưng khi được nêm nếm gia vị và thực hiện phương pháp kho đặc trưng thì trở nên hấp dẫn và ngon miệng lạ kỳ. Thịt kho hột vịt là món ăn đặc trưng của ngày Tết cổ truyền nhưng cũng rất quen thuộc trong các bữa ăn hằng ngày của mọi gia đình. Món ăn chỉ với 2 nguyên liệu chính là thịt ba rọi và trứng vịt nhưng khi được nêm nếm gia vị và thực hiện phương pháp kho đặc trưng thì trở nên hấp dẫn và ngon miệng lạ kỳ.";
        }

        
    }
}
