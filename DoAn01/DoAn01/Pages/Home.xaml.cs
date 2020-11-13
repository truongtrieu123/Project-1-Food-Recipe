﻿using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

    public partial class Home : Page
    {
        private const int TotalItemsPerPage = 12;

        public BindingList<Food> Sublist { get; set; }
        // item index in current page;
        public int IndexCurrentPage { get; set; }
        //
        public int SelectedItemIndex { get; set; }
        //
        public class HomeViewModel : INotifyPropertyChanged
        {
            public CPage PageInfor { get; set; }
            private BindingList<Food> currentSubList = null;
            public BindingList<Food> CurrentSublist
            {
                get { return this.currentSubList; }
                set
                {
                    if (value != this.currentSubList) {
                        this.currentSubList = new BindingList<Food>(value.ToList<Food>());
                        OnPropertyChanged("CurrentSublist");
                    }
                    else { return; }
                }
            }
            private void OnPropertyChanged(string name)
            {
                var handler = PropertyChanged;

                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
                else { }
            }

            public HomeViewModel()
            {
                this.PageInfor = new CPage(Global.HomeSubLists.Count);
                this.CurrentSublist = new BindingList<Food>();
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        private HomeViewModel _mainVM;

        public Home()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            _mainVM = new HomeViewModel();

            var value = ConfigurationManager.AppSettings["HomeCurrentPage"];
            var homecrtpage = int.Parse(value);

            if (homecrtpage > 0 && homecrtpage <= _mainVM.PageInfor.MaxPages)
            {
                _mainVM.PageInfor.CurrentPage = homecrtpage;
            }
            else
            {
                // current page bằng 1
            }

            _mainVM.CurrentSublist = Global.HomeSubLists[_mainVM.PageInfor.CurrentPage - 1];

            DataContext = _mainVM;

            //if(_mainVM.PageInfor.CurrentPage == 1)
            //{
            //    prevPageButton.IsEnabled = false;
            //}
            //else
            //{
            //    // Do nothing
            //}

            //if(_mainVM.PageInfor.CurrentPage == _mainVM.PageInfor.MaxPages)
            //{
            //    nextPageButton.IsEnabled = false;
            //}
            //else
            //{
            //    // Do nothing
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mainVM.PageInfor.CurrentPage > 0 && _mainVM.PageInfor.CurrentPage < _mainVM.PageInfor.MaxPages)
            {
                _mainVM.PageInfor.CurrentPage++;

                //if(_mainVM.PageInfor.CurrentPage == _mainVM.PageInfor.MaxPages)
                //{
                //    nextPageButton.IsEnabled = false;
                //}
                _mainVM.CurrentSublist = Global.HomeSubLists[_mainVM.PageInfor.CurrentPage - 1];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void prevPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mainVM.PageInfor.CurrentPage > 1 && _mainVM.PageInfor.CurrentPage <= _mainVM.PageInfor.MaxPages)
            {
                _mainVM.PageInfor.CurrentPage--;

                //if(_mainVM.PageInfor.CurrentPage == 1)
                //{
                //    prevPageButton.IsEnabled = false;
                //}
                _mainVM.CurrentSublist = Global.HomeSubLists[_mainVM.PageInfor.CurrentPage - 1];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void favoriteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // index of this food in food list
                int indexInList = IndexCurrentPage + (_mainVM.PageInfor.CurrentPage - 1) * TotalItemsPerPage;
                var food = Global.FoodList[indexInList];
                var changeCheck = 0;
                var btn = sender as Button;
                var aws = btn.Content as ImageAwesome;

                if (aws.Foreground == Brushes.Red)
                {
                    //change heart icon's color
                    aws.Foreground = Brushes.Black;
                    // update property in food list
                    Global.FoodList[indexInList].Favorite = new StringBuilder("Black");
                    changeCheck = 1;
                }
                else if (aws.Foreground == Brushes.Black)
                {
                    //change heart icon's color
                    aws.Foreground = Brushes.Red;
                    // update property in food list
                    Global.FoodList[indexInList].Favorite = new StringBuilder("Red");
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
                        food.Favorite = new StringBuilder("Red");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectCurrentItem(object sender, KeyboardFocusChangedEventArgs e)
        {
            var item = sender as ListViewItem;
            item.IsSelected = true;
            IndexCurrentPage = mealListView.SelectedIndex;
            item.IsSelected = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DyingHadle()
        {
            this.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Food p = (Food)mealListView.Items[IndexCurrentPage];
            Debug.WriteLine(p.Name);
            Debug.WriteLine(p.Introduction);
            DetailMeal page = new DetailMeal(p);

            // Lưu lại trang hiện tại của PageInfor
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["HomeCurrentPage"].Value = _mainVM.PageInfor.CurrentPage.ToString();
            config.Save(ConfigurationSaveMode.Minimal);

            ConfigurationManager.RefreshSection("appSettings");
            // Chuyển hướng hiển thị xang oage
            this.NavigationService.Navigate(page);
        }
    }
}
