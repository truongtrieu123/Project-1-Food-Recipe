using System;
using DoAn01.Pages;
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
using FontAwesome.WPF;

namespace DoAn01
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    /// 

    public partial class Home : Page, INotifyPropertyChanged
    {
        private const int TotalItemsPerPage = 12;
        private List<Food> SubList;
        private int _currentPage;
        public int SelectedItemIndex { get; set; }
        public int Index { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public Home()
        {
            InitializeComponent();
            _currentPage = 1;
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            mealListView.Items.Clear();
            CreateHomePage();
        }

        public void CreateHomePage()
        {
            SubList = new List<Food>();

            if (Global.FoodList.Count < TotalItemsPerPage)
                SubList = Global.FoodList.GetRange(0, TotalItemsPerPage);
            else
                SubList = Global.FoodList.GetRange(0, TotalItemsPerPage);

            pageNumber.Content = _currentPage;
            mealListView.ItemsSource = SubList;
        }
        private List<Food> SubListForNextPage()
        {
            int nextIndex = (_currentPage) * TotalItemsPerPage;
            int currentIndex = (_currentPage - 1) * TotalItemsPerPage;
            SubList = new List<Food>();

            if (nextIndex + 1 > Global.FoodList.Count)
                SubList = Global.FoodList.GetRange(currentIndex, Global.FoodList.Count - currentIndex);
            else
            {
                if (Global.FoodList.Count - nextIndex > TotalItemsPerPage)
                    SubList = Global.FoodList.GetRange(nextIndex, TotalItemsPerPage);
                else
                    SubList = Global.FoodList.GetRange(nextIndex, Global.FoodList.Count - nextIndex);

                _currentPage++;
            }

            return SubList;
        }

        private void nextPage_Click(object sender, RoutedEventArgs e)
        {
            mealListView.ItemsSource = SubListForNextPage();
            pageNumber.Content = _currentPage;
        }

        private List<Food> SubListForPrevPage()
        {
            int currentPage = (_currentPage - 1) * TotalItemsPerPage;
            int prevPage = (_currentPage - 2) * TotalItemsPerPage;
            SubList = new List<Food>();

            if (prevPage < 0)
            {
                if (Global.FoodList.Count < TotalItemsPerPage) 
                    SubList = Global.FoodList.GetRange(0, Global.FoodList.Count);
                else 
                    SubList = Global.FoodList.GetRange(0, TotalItemsPerPage); // Global.FoodList.count>=TotalItemsPerPage;
            }
            else
            {
                SubList = Global.FoodList.GetRange(prevPage, TotalItemsPerPage);
                _currentPage--;
            }

            return SubList;
        }

        private void prevPage_Click(object sender, RoutedEventArgs e)
        {
            mealListView.ItemsSource = SubListForPrevPage();
            pageNumber.Content = _currentPage;
        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                ImageAwesome aws = btn.Content as ImageAwesome;
                //change heart icon's color
                (aws.Foreground) = (aws.Foreground == Brushes.Red) ? Brushes.Black : Brushes.Red;

                int indexInList = Index + (_currentPage - 1) * TotalItemsPerPage;
                _ = (Global.FoodList[indexInList].Favorite == "Black") ? 
                    Global.FoodList[indexInList].Favorite = "Red" : 
                    Global.FoodList[indexInList].Favorite = "Black";
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
            Index = mealListView.SelectedIndex;
            item.IsSelected = false;
        }

        private void ListViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Food p = (Food)mealListView.Items[Index];
            DetailMeal page = new DetailMeal(p);
            this.NavigationService.Navigate(page);
        }


    }
}
