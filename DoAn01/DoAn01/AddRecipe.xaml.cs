using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        }

        private void outButton_Click(object sender, RoutedEventArgs e)
        {
            Dying?.Invoke();
            this.Close();
        }

        //Di chuyen man hinh
        private void TitleOfWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.StackPanel;
            var win = Window.GetWindow(move);
            win.DragMove();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Dying?.Invoke();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            youtubeLinkBox.Text = "";
            descriptionBox.Text = "";
            nameFoodBox.Text = "";
            stepFoodBox.Text = "";
            listView.Items.Clear();
            var bitmap = new BitmapImage(
                                new Uri(
                                       "",
                                       UriKind.Relative)
                                );
            foodImage.Source = bitmap;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void add_image_foodButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            if (screen.ShowDialog() == true)
            {
                var filepath = screen.FileName;
                Uri bitmap = new Uri(filepath);
                foodImage.Source = new BitmapImage(bitmap);
            }
        }

        private void add_image_stepButton_Click(object sender, RoutedEventArgs e)
        {

            var screen = new OpenFileDialog();
            screen.Multiselect = true;
            if (screen.ShowDialog() == true)
            {
                foreach (var filepath in screen.FileNames)
                {
                    listView.Items.Add(new BitmapImage(new Uri(filepath)));
                }
            }
        }

        private void add_stepButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

