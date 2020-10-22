using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace DoAn01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Main.Content = new Home();
            Home page = new Home();
            Main.NavigationService.Navigate(page);
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            //Main.Content = new Home();
        }


        private void favorButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Favorite();
        }

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new About();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Search();
        }

        private void outButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void newRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new AddRecipe();
            screen.Dying += XuLiHapHoi;
            screen.Show();
            this.Hide();
        }
        private void XuLiHapHoi()
        {
            this.Show();
        }
        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Home();
        }

        //Di chuyen man hinh
        private void CanvasOfWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.Canvas;
            var win = Window.GetWindow(move);
            win.DragMove();
        }

    }
}
