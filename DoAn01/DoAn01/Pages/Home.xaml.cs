using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        public class HomeViewModel : INotifyPropertyChanged
        {
            public CPage PageInfor { get; set; }
            private BindingList<Food> currentSubList = null;
            public BindingList<Food> CurrentSublist
            {
                get { return this.currentSubList; }
                set
                {
                    if (value != this.currentSubList)
                    {
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

            if (IsValuablePageNumber(homecrtpage) == true)
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
        /// <param name="pagenumber"></param>
        /// <returns></returns>
        private bool IsValuablePageNumber(int pagenumber)
        {
            var check = false;
            check = pagenumber >= _mainVM.PageInfor.CurrentPage && pagenumber <= _mainVM.PageInfor.MaxPages;
            return check;
        }

        /// <summary>
        /// Hàm xử lí khi nhấn Next Page Button
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
                UpdateAppConfig("HomeCurrentPage");
            }
        }

        /// <summary>
        /// Hàm xử lí khi nhấn Previous Page Button
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
                UpdateAppConfig("HomeCurrentPage");
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
                    changeCheck = -1;
                }
                else if (aws.Foreground == Brushes.Black)
                {
                    //change heart icon's color
                    aws.Foreground = Brushes.Red;
                    // update property in food list
                    Global.FoodList[indexInList].Favorite = new StringBuilder("Red");
                    changeCheck = 1;
                }
                else { }

                if (changeCheck != 0)
                {
                    if (changeCheck == -1)
                    {
                        Global.FavoriteFoodList.Remove(food);
                    }
                    else if (changeCheck == 1)
                    {
                        Global.FavoriteFoodList.Add(food);
                    }
                    Global.FavorSubLists = Global.ConvertListToSubLists(Global.ItemsPerPage, Global.FavoriteFoodList);
                }
                else { }
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
        /// Hàm xử lí khi nhấn double click vào một món ăn để xem chi tiết
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Food p = (Food)mealListView.Items[IndexCurrentPage];
            DetailMeal page = new DetailMeal(p);
            //
            UpdateAppConfig("HomeCurrentPage");
            // Chuyển hướng hiển thị xang page
            this.NavigationService.Navigate(page);
        }

        /// <summary>
        /// Hàm cập nhập file App.Config
        /// </summary>
        /// <param name="_name"></param>
        private void UpdateAppConfig(string _name)
        {
            // Lưu lại trang hiện tại của PageInfor
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[_name].Value = _mainVM.PageInfor.CurrentPage.ToString();
            config.Save(ConfigurationSaveMode.Minimal);

            ConfigurationManager.RefreshSection("appSettings");
        }

        static Regex ConvertToUnsign_rg = null;

        public static string ConvertToUnsign(string strInput)
        {
            if (ReferenceEquals(ConvertToUnsign_rg, null))
            {
                ConvertToUnsign_rg = new Regex("p{IsCombiningDiacriticalMarks}+");
            }
            var temp = strInput.Normalize(NormalizationForm.FormD);
            return ConvertToUnsign_rg.Replace(temp, string.Empty).Replace("đ", "d").Replace("Đ", "D").ToLower();
        }

        //private bool UserFilter(object item)
        //{
        //    if (String.IsNullOrEmpty(txtFilter.Text))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        ConvertToUnsign(txtFilter.Text);
        //        (item as Food).Name = ConvertToUnsign((item as Food).Name);
        //        return ((item as Food).Name.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        //    }
        //}

        //private void OnKeyDownHandler(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Return)
        //    {
        //        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(mealListView.ItemsSource);
        //        view.Filter = UserFilter;
        //        CollectionViewSource.GetDefaultView(mealListView.ItemsSource).Refresh();
        //    }

        //}
    }
}
