using DoAn01.Pages;
using FontAwesome.WPF;
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
    /// Interaction logic for Favorite.xaml
    /// </summary>
    /// 

    public class FavoriteMenu
    {
        public List<Food> _foodList;
        public BindingList<Food> _favoriteList;
        public FavoriteMenu()
        {
            _foodList = new List<Food>();
            _favoriteList = new BindingList<Food>();

            FoodMenu p = new FoodMenu();
            _foodList = p.GetAll();

            for (int i = 0; i < _foodList.Count; i++)
            {
                if (_foodList[i].Favorite == "Red")
                {
                    Food a = _foodList[i];
                    _favoriteList.Add(a);
                }
            }
        }

        public BindingList<Food> GetAll()
        {
            return _favoriteList;
        }
    }

    public partial class Favorite : Page, INotifyPropertyChanged
    {
        public BindingList<Food> _list;

        public BindingList<Food> Sublist { get; set; }

        //List contains elements which have black heart icon were deleted from _list
        public List<Food> BlackList { get; set; }

        private System.Timers.Timer _timer;

        public int _currentPage;

        private const int TotalItemsPerPage = 12;

        //item index in current page;
        public int IndexCurrentPage { get; set; }

        //element index in _list
        public int IndexInList { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public Favorite()
        {
            InitializeComponent();

            _currentPage = 1;
            FavoriteMenu p = new FavoriteMenu();
            _list = p.GetAll();
            BlackList = new List<Food>();
        }

        public BindingList<Food> GetRange(BindingList<Food> p, int start, int count)
        {
            BindingList<Food> curList = new BindingList<Food>();
            for (int i = start; i < start + count; i++)
            {
                Food temp = p[i];
                curList.Add(temp);
            }
            return curList;
        }

        private void Favorite_Loaded(object sender, RoutedEventArgs e)
        {

            mealListView.Items.Clear();

            CreateFavoritePage();
        }

        public void CreateFavoritePage()
        {
            Sublist = new BindingList<Food>();

            if (_list.Count < TotalItemsPerPage)
                Sublist = GetRange(_list, 0, _list.Count);
            else
                Sublist = GetRange(_list, 0, TotalItemsPerPage);

            pageNumber.Content = _currentPage;
            mealListView.ItemsSource = Sublist;
        }

        private BindingList<Food> SublistForNextPage()
        {
            int nextIndex = (_currentPage) * TotalItemsPerPage;
            int currentIndex = (_currentPage - 1) * TotalItemsPerPage;
            Sublist.Clear();
            Sublist = new BindingList<Food>();

            if (nextIndex + 1 > _list.Count)
                Sublist = GetRange(_list, currentIndex, _list.Count - currentIndex);
            else
            {
                if (_list.Count - nextIndex > TotalItemsPerPage)
                    Sublist = GetRange(_list, nextIndex, TotalItemsPerPage);
                else
                    Sublist = GetRange(_list, nextIndex, _list.Count - nextIndex);

                _currentPage++;
            }

            return Sublist;
        }

        private void nextPage_Click(object sender, RoutedEventArgs e)
        {
            mealListView.ItemsSource = SublistForNextPage();
            pageNumber.Content = _currentPage;
        }

        private BindingList<Food> SublistForPrevPage()
        {
            int currentPage = (_currentPage - 1) * TotalItemsPerPage;
            int prevPage = (_currentPage - 2) * TotalItemsPerPage;

            Sublist.Clear();
            Sublist = new BindingList<Food>();

            if (prevPage < 0)
            {
                if (_list.Count < 12) Sublist = GetRange(_list, 0, _list.Count);
                else Sublist = GetRange(_list, 0, TotalItemsPerPage);// _list.count>=12;
            }
            else
            {
                Sublist = GetRange(_list, prevPage, TotalItemsPerPage);
                _currentPage--;
            }

            return Sublist;
        }

        private void prevPage_Click(object sender, RoutedEventArgs e)
        {

            mealListView.ItemsSource = SublistForPrevPage();
            pageNumber.Content = _currentPage;
        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                ImageAwesome aws = btn.Content as ImageAwesome;

                // change color on icon
                (aws.Foreground) = (aws.Foreground == Brushes.Red) ? Brushes.Black : Brushes.Red;

                // remove selected meal out of  menu
                //RemoveSelectedItem(IndexCurrentPage);
                IndexInList = IndexCurrentPage + (_currentPage - 1) * TotalItemsPerPage;

                _list[IndexInList].Favorite = "Black";

                //BlackList.Add(_list[IndexInList]);

                Sublist.RemoveAt(IndexCurrentPage);
                _list.RemoveAt(IndexInList);
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
