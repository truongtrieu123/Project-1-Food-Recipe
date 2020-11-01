using DoAn01.Pages;
using FontAwesome.WPF;
using FoodRecipe;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DoAn01
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    /// 

    public partial class Home : Page, INotifyCollectionChanged
    {
        private const int TotalItemsPerPage = 12;

        public BindingList<Food> Sublist { get; set; }
        //
        public CPage _homepage { get; set; } = new CPage(Global.HomeSubLists.Count);
        // item index in current page;
        public int IndexCurrentPage { get; set; }
        //
        public int SelectedItemIndex { get; set; }
        //
        public int Index { get; set; }
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public Home()
        {
            InitializeComponent();

            var value = ConfigurationManager.AppSettings["HomeCurrentPage"];
            var homecrtpage = int.Parse(value);
            _homepage.CurrentPage = homecrtpage;
            Sublist = Global.HomeSubLists[homecrtpage - 1];
            mealListView.ItemsSource = Sublist;
            DataContext = _homepage;
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            //if(_homepage.CurrentPage == 1)
            //{
            //    prevPageButton.IsEnabled = false;
            //}
            //else
            //{
            //    // Do nothing
            //}

            //if(_homepage.CurrentPage == _homepage.MaxPages)
            //{
            //    nextPageButton.IsEnabled = false;
            //}
            //else
            //{
            //    // Doo nothing
            //}
        }

        private void nextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_homepage.CurrentPage < _homepage.MaxPages)
            {
                _homepage.CurrentPage++;

                //if(_homepage.CurrentPage == _homepage.MaxPages)
                //{
                //    nextPageButton.IsEnabled = false;
                //}

                mealListView.ItemsSource = Global.HomeSubLists[_homepage.CurrentPage - 1];
            }
        }

        private void prevPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_homepage.CurrentPage > 1)
            {
                _homepage.CurrentPage--;

                //if(_homepage.CurrentPage == 1)
                //{
                //    prevPageButton.IsEnabled = false;
                //}

                mealListView.ItemsSource = Global.HomeSubLists[_homepage.CurrentPage - 1];
            }
        }

        private void favoriteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // index of this food in food list
                int indexInList = IndexCurrentPage + (_homepage.CurrentPage - 1) * TotalItemsPerPage;
                var food = Global.FoodList[indexInList];
                var changeCheck = 0;
                var btn = sender as Button;
                var aws = btn.Content as ImageAwesome;

                if (aws.Foreground == Brushes.Red)
                {
                    //change heart icon's color
                    aws.Foreground = Brushes.Black;
                    // update property in food list
                    Global.FoodList[indexInList].Favorite = "Black";
                    changeCheck = 1;
                }
                else if (aws.Foreground == Brushes.Black)
                {
                    //change heart icon's color
                    aws.Foreground = Brushes.Red;
                    // update property in food list
                    Global.FoodList[indexInList].Favorite = "Red";
                    changeCheck = -1;
                }
                else
                {
                    // Throw exception
                }
                if (changeCheck != 0)
                {
                    if (changeCheck == -1)
                    {
                        Global.FavoriteFoodList.Remove(food);
                    }
                    else if (changeCheck == 1)
                    {
                        food.Favorite = "Red";
                        Global.FavoriteFoodList.Add(food);
                    }
                    Global.FavorSubLists = Global.ConvertListToSubLists(Global.ItemsPerPage, Global.FoodList);
                }
                else
                { //Do nothing

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SelectCurrentItem(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
            IndexCurrentPage = mealListView.SelectedIndex;
            item.IsSelected = false;
        }

        private void DyingHadle()
        {
            this.Visibility = Visibility.Visible;
        }

        private void ListViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Food p = (Food)mealListView.Items[IndexCurrentPage];
            DetailMeal page = new DetailMeal(p);

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["HomeCurrentPage"].Value = _homepage.CurrentPage.ToString();
            config.Save(ConfigurationSaveMode.Minimal);

            ConfigurationManager.RefreshSection("appSettings");

            this.NavigationService.Navigate(page);
        }


    }
}
