using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddRecipe.xaml
    /// </summary>
    public partial class AddRecipe : Window
    {
        public delegate void DeathHandler();
        public event DeathHandler Dying;

        public AddRecipe()
        {
            InitializeComponent();
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            Dying?.Invoke();
            this.Close();
            //var screen = new MainWindow();
            //screen.Show();
        }

        private void outButton_Click(object sender, RoutedEventArgs e)
        {
            Dying?.Invoke();
            this.Close();
            //var screen = new MainWindow();
            //screen.Show();
        }

        //Di chuyen man hinh
        private void CanvasOfWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.Canvas;
            var win = Window.GetWindow(move);
            win.DragMove();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Dying?.Invoke();
        }
    }
}
