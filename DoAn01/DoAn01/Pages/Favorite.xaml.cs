using DoAn01.Pages;
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
        public List<Food> _favoriteList;
        public FavoriteMenu()
        {
            _foodList = new List<Food>();
            _favoriteList = new List<Food>();

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

        public List<Food> GetAll()
        {
            return _favoriteList;
        }
    }

    public partial class Favorite : Page, INotifyPropertyChanged
    {
        public List<Food> _list;

        private System.Timers.Timer _timer;

        public int _currentPage;

        private const int TotalItemsPerPage = 12;

        public int SelectedItemIndex { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Favorite()
        {
            InitializeComponent();

            _currentPage = 1;
            FavoriteMenu p = new FavoriteMenu();
            _list = p.GetAll();
        }

        private void Favorite_Loaded(object sender, RoutedEventArgs e)
        {

            List<Food> subList = new List<Food>();

            mealListView.Items.Clear();
            if (_list.Count < TotalItemsPerPage)
                subList = _list.GetRange(0, _list.Count);
            else
                subList = _list.GetRange(0, TotalItemsPerPage);

            CreateFavoritePage(subList);
            Console.WriteLine("lallaalla");
        }

        public void CreateFavoritePage(List<Food> subList)
        {
            pageNumber.Content = _currentPage;
            mealListView.ItemsSource = subList;
        }

        private List<Food> SublistForNextPage()
        {
            int nextIndex = (_currentPage) * TotalItemsPerPage;
            int currentIndex = (_currentPage - 1) * TotalItemsPerPage;
            List<Food> sublist = new List<Food>();

            if (nextIndex + 1 > _list.Count)
                sublist = _list.GetRange(currentIndex, _list.Count - currentIndex);
            else
            {
                if (_list.Count - nextIndex > TotalItemsPerPage)
                    sublist = _list.GetRange(nextIndex, TotalItemsPerPage);
                else
                    sublist = _list.GetRange(nextIndex, _list.Count - nextIndex);

                _currentPage++;
            }

            return sublist;
        }

        private void nextPage_Click(object sender, RoutedEventArgs e)
        {
            mealListView.ItemsSource = SublistForNextPage();
            pageNumber.Content = _currentPage;
        }

        private List<Food> SublistForPrevPage()
        {
            int currentPage = (_currentPage - 1) * TotalItemsPerPage;
            int prevPage = (_currentPage - 2) * TotalItemsPerPage;
            List<Food> sublist = new List<Food>();

            if (prevPage < 0)
            {
                if (_list.Count < 12) sublist = _list.GetRange(0, _list.Count);
                else sublist = _list.GetRange(0, TotalItemsPerPage); // _list.count>=12;
            }
            else
            {
                sublist = _list.GetRange(prevPage, TotalItemsPerPage);
                _currentPage--;
            }

            return sublist;
        }
        private void prevPage_Click(object sender, RoutedEventArgs e)
        {

            mealListView.ItemsSource = SublistForPrevPage();
            pageNumber.Content = _currentPage;
        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(SelectedItemIndex);

        }

        private void mealListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int a = (int)mealListView.SelectedIndex;
            Food p = (Food)mealListView.SelectedItem;

            DetailMeal page = new DetailMeal(p);
            this.NavigationService.Navigate(page);
        }

        private void SelectCurrentItem(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
            SelectedItemIndex = mealListView.SelectedIndex;
            MessageBox.Show(SelectedItemIndex.ToString());
            Console.WriteLine(SelectedItemIndex);
        }
    }
}
