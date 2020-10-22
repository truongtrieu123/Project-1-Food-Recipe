using System;
﻿using DoAn01.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    /// 
    public class MealInfo
    {
        public string imageMealPath { get; set; }
        public string mealName { get; set; }

        public string favorite { get; set; }
    }

    public class MealInfoDAO
    {


        public static List<MealInfo> GetAll()
        {
            List<MealInfo> _list = new List<MealInfo>()
            {
                new MealInfo{ imageMealPath="../../Images/food01.jpg", favorite="Gray", mealName="Thịt kho hột vịt"},
                new MealInfo{ imageMealPath="../../Images/food01.jpg", favorite="Red", mealName="Thịt kho hột vịt"},
                new MealInfo{ imageMealPath="../../Images/food01.jpg", favorite="Red", mealName="Thịt kho hột vịt"},
                new MealInfo{ imageMealPath="../../Images/food01.jpg", favorite="Gray", mealName="Thịt kho hột vịt"},
                new MealInfo{ imageMealPath="../../Images/food01.jpg", favorite="Red", mealName="Thịt kho hột vịt"},
                new MealInfo{ imageMealPath="../../Images/food01.jpg", favorite="Gray", mealName="Thịt kho hột vịt"},
                new MealInfo{ imageMealPath="../../Images/food01.jpg", favorite="Red", mealName="Thịt kho hột vịt"},
                new MealInfo{ imageMealPath="../../Images/food01.jpg", favorite="Gray", mealName="Thịt kho hột vịt"},
                new MealInfo{ imageMealPath="../../Images/food01.jpg", favorite="Red", mealName="Thịt kho hột vịt"},
                new MealInfo{ imageMealPath="../../Images/food01.jpg", favorite="Gray", mealName="Thịt kho hột vịt"},
                new MealInfo{ imageMealPath="../../Images/food01.jpg", favorite="Gray", mealName="Thịt kho hột vịt"},
                new MealInfo{ imageMealPath="../../Images/food01.jpg", favorite="Red", mealName="Thịt kho hột vịt"},
            };
            return _list;
        }
    }
    public partial class Home : Page
    {

        public List<MealInfo> _list;

        private System.Timers.Timer _timer;




        public Home()
        {
            InitializeComponent();
            _list = MealInfoDAO.GetAll();
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            CreateHomePage();
        }

        public void CreateHomePage()
        {

            List<MealInfo> subList = _list.GetRange(0, 12);//Page 1
            mealListView.ItemsSource = subList;
        }


        private void nextPage_Click(object sender, RoutedEventArgs e)
        {


        }

        private void prevPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void mealListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DetailMeal page = new DetailMeal();

            int a = (int)mealListView.SelectedIndex;

            MealInfo p = (MealInfo)mealListView.SelectedItem;

            this.NavigationService.Navigate(page);
          
        }

    }
}
