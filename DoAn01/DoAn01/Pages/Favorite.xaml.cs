using FontAwesome.WPF;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DoAn01
{
    /// <summary>
    /// Interaction logic for Favorite.xaml
    /// </summary>
    /// 

    public partial class Favorite : Page
    {
        #region Constant
        private const int TotalItemsPerPage = 12;
        #endregion

        #region Properties
        // control the number of current page and maximum page
        public CPage _favorpage = new CPage(Global.FavorSubLists.Count);
        // list to binding with Favorite ListView pre Page
        public BindingList<Food> Sublist { get; set; }
        // item index in current page;
        public int IndexCurrentPage { get; set; }
        // element index in _list
        public int IndexInList { get; set; }
        //
        public delegate void LikeDislikeHandle();
        public event LikeDislikeHandle FavorHandle;
        #endregion


        public Favorite()
        {
            InitializeComponent();
           
            DataContext = _favorpage;
        }

        private bool IsValuablePageNumber(int pagenumber)
        {
                var check = false;
                check = pagenumber >= _favorpage.CurrentPage && pagenumber <= _favorpage.MaxPages;
                return check;
        }

        private void Favorite_Loaded(object sender, RoutedEventArgs e)
        {
            var value = ConfigurationManager.AppSettings["FavorCurrentPage"];
            var favorcrtpage = int.Parse(value);

            if (Global.FavorSubLists.Count > 0)
            {
                if (IsValuablePageNumber(favorcrtpage) == true)
                {
                    _favorpage.CurrentPage = favorcrtpage;

                }
                mealListView.ItemsSource = Global.FavorSubLists[favorcrtpage - 1];
            }


            //if (_favorpage.CurrentPage == 1)
            //{
            //    prevPageButton.IsEnabled = false;
            //}
            //else
            //{
            //    // Do nothing
            //}

            //if (_favorpage.CurrentPage == _favorpage.MaxPages)
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
            if (_favorpage.CurrentPage < _favorpage.MaxPages)
            {
                _favorpage.CurrentPage++;

                //if(_favorpage.CurrentPage == _favorpage.MaxPages)
                //{
                //    nextPageButton.IsEnabled = false;
                //}

                mealListView.ItemsSource = Global.FavorSubLists[_favorpage.CurrentPage - 1];
            }
        }

        private void prevPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_favorpage.CurrentPage > 1)
            {
                _favorpage.CurrentPage--;

                //if(_favorpage.CurrentPage == 1)
                //{
                //    prevPageButton.IsEnabled = false;
                //}

                mealListView.ItemsSource = Global.FavorSubLists[_favorpage.CurrentPage - 1];
            }
        }

        private void dislikeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // remove selected meal out of  menu
                IndexInList = IndexCurrentPage + (_favorpage.CurrentPage - 1) * TotalItemsPerPage;
                var food = Global.FoodList[IndexCurrentPage];
                Button btn = sender as Button;
                ImageAwesome aws = btn.Content as ImageAwesome;

                // change color on icon
                (aws.Foreground) = Brushes.Black;

                Global.FoodList[IndexInList].Favorite = new StringBuilder("Black");
                Global.FavoriteFoodList.Remove(food);
                Global.FavorSubLists = Global.ConvertListToSubLists(TotalItemsPerPage, Global.FavoriteFoodList);
                PageViewPrePare();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PageViewPrePare()
        {
            _favorpage.MaxPages = Global.FavorSubLists.Count;

            if (_favorpage.CurrentPage > Global.FavorSubLists.Count)
            {
                _favorpage.CurrentPage = Global.FavorSubLists.Count;
            }
            else
            {
                // Do nothing
            }
            mealListView.ItemsSource = Global.FavorSubLists[_favorpage.CurrentPage - 1];

        }

        private void SelectCurrentItem(object sender, KeyboardFocusChangedEventArgs e)
        {

            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
            IndexCurrentPage = mealListView.SelectedIndex;
            item.IsSelected = false;
        }
        private void ListViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Food p = (Food)mealListView.Items[IndexCurrentPage];
            DetailMeal page = new DetailMeal(p);
            this.NavigationService.Navigate(page);
        }



    }
}
