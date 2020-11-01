using DoAn01.Pages;
using FontAwesome.WPF;
using FoodRecipe;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
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

    public partial class Favorite : Page, INotifyPropertyChanged
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
        public delegate void LikeHandle();

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public Favorite()
        {
            InitializeComponent();
        }

        private void Favorite_Loaded(object sender, RoutedEventArgs e)
        {
            var value = ConfigurationManager.AppSettings["FavorCurrentPage"];
            var favorcrtpage = int.Parse(value);
            _favorpage.CurrentPage = favorcrtpage;
            Sublist = Global.FavorSubLists[favorcrtpage];

            mealListView.Items.Clear();
            mealListView.ItemsSource = Sublist;
        }

        private void nextPage_Click(object sender, RoutedEventArgs e)
        {
            _favorpage.CurrentPage++;
            Sublist = Global.FavorSubLists[_favorpage.CurrentPage];
        }

        private void prevPage_Click(object sender, RoutedEventArgs e)
        {
            _favorpage.CurrentPage--;
            Sublist = Global.FavorSubLists[_favorpage.CurrentPage];
        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                ImageAwesome aws = btn.Content as ImageAwesome;

                // change color on icon
                (aws.Foreground) = Brushes.Black;
                //(aws.Foreground) = (aws.Foreground == Brushes.Red) ? Brushes.Black : Brushes.Red;

                // remove selected meal out of  menu
                IndexInList = IndexCurrentPage + (_favorpage.CurrentPage - 1) * TotalItemsPerPage;

                Global.FavoriteFoodList[IndexInList].Favorite = "Black";
                Global.FavorSubLists.Clear();
                Global.FavorSubLists = Global.ConvertListToSubLists(TotalItemsPerPage, Global.FavoriteFoodList);

                if (_favorpage.CurrentPage == _favorpage.MaxPages)
                {
                    if (_favorpage.MaxPages > Global.FavorSubLists.Count)
                    {
                        if (Global.FavorSubLists.Count == 0)
                        {
                            _favorpage.CurrentPage = 1;
                            _favorpage.CurrentPage = 1;
                            Sublist.Clear();
                        }
                    }
                    {
                        _favorpage.CurrentPage--;
                    }
                    _favorpage.MaxPages = Global.FavorSubLists.Count;
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
        private void ListViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Food p = (Food)mealListView.Items[IndexCurrentPage];
            DetailMeal page = new DetailMeal(p);
            this.NavigationService.Navigate(page);
        }



    }
}
