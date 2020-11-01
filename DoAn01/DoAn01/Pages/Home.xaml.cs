using DoAn01.Pages;
using FontAwesome.WPF;
using FoodRecipe;
using System;
using System.ComponentModel;
using System.Configuration;
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

    public partial class Home : Page, INotifyPropertyChanged
    {
        private const int TotalItemsPerPage = 12;

        public BindingList<Food> Sublist { get; set; }
        //
        public CPage _homepage = new CPage(Global.HomeSubLists.Count);
        // item index in current page;
        public int IndexCurrentPage { get; set; }
        //
        public int SelectedItemIndex { get; set; }
        //
        public int Index { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public Home()
        {
            InitializeComponent();
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            var value = ConfigurationManager.AppSettings["HomeCurrentPage"];
            var homecrtpage = int.Parse(value);
            _homepage.CurrentPage = homecrtpage;
            Sublist = Global.HomeSubLists[homecrtpage];
            
            mealListView.ItemsSource = Sublist;
            DataContext = _homepage;
        }

        private void nextPage_Click(object sender, RoutedEventArgs e)
        {
            //_homepage.CurrentPage++;
            //Sublist = Global.HomeSubLists[_homepage.CurrentPage];
        }

        private void prevPage_Click(object sender, RoutedEventArgs e)
        {
            //_homepage.CurrentPage--;
            //Sublist = Global.HomeSubLists[_homepage.CurrentPage];
        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                ImageAwesome aws = btn.Content as ImageAwesome;
                //change heart icon's color
                (aws.Foreground) = (aws.Foreground == Brushes.Red) ? Brushes.Black : Brushes.Red;

                int indexInList = Index + (_homepage.CurrentPage - 1) * TotalItemsPerPage;
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

        private void DyingHadle()
        {
            this.Visibility = Visibility.Visible;
        }

        private void ListViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Food p = (Food)mealListView.Items[Index];
            DetailMeal page = new DetailMeal(p);

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["HomeCurrentPage"].Value = _homepage.CurrentPage.ToString();
            config.Save(ConfigurationSaveMode.Minimal);

            ConfigurationManager.RefreshSection("appSettings");

            this.NavigationService.Navigate(page);
        }


    }
}
