﻿using System;
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
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            //Main.Content = new Home();
        }

        private void newRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new NewRecipe();
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Home();
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
    }
}
