using System;
using System.Configuration;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DoAn01
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        private const int MaxTime = 10; // Thời gian hiển thị tối đa hiển thị

        private int MyTime = 0; // Biến đếm thời gian hiển thị của màn hình
        private System.Timers.Timer _timer; // Biến timer để đếm thời gian chạy của chương trình
        private Random _rng;
        private Food _food = new Food();
        private int _foodindex;
        public delegate void DeathHandler();
        public event DeathHandler Dying;

        public SplashScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Hàm khi màn hình khởi tạo xong.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var value = ConfigurationManager.AppSettings["ShowSplashScreen"];
            var showSplash = bool.Parse(value);
            FoodDAO dao = new ExcelFoodDAO();
            Global.FoodList = dao.GetAll();
            Global.CreateFavoriteList();

            if (showSplash == false)
            {
                var screen = new MainWindow();
                screen.Show();
                this.Close();
            }
            else
            {
                MyTime = 0;

                _rng = new Random();
                _foodindex = _rng.Next(Global.FoodList.Count);
                _food = Global.FoodList[_foodindex];
                DataContext = _food;

                _timer = new System.Timers.Timer();
                _timer.Elapsed += Timer_Elapsed;
                _timer.Interval = 1000;
                _timer.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            MyTime++;
            if (MyTime == MaxTime)
            {
                _timer.Stop();

                Dispatcher.Invoke(() =>
                {
                    var screen = new MainWindow();
                    screen.Show();

                    this.Close();
                });
            }

            Dispatcher.Invoke(() =>
            {
                progress.Value = MyTime;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void turnOffCheckBox_Click(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Minimal);

            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skipButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();           
            Dying?.Invoke();
            var screen = new MainWindow();
            screen.Show();
            this.Close();
        }
    }
}
