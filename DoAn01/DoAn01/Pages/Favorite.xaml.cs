using FontAwesome.WPF;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
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
        private const int TotalItemsPerPage = 12;

        // item index in current page;
        public int IndexCurrentPage { get; set; }

        //public delegate void LikeDislikeHandle();
        //public event LikeDislikeHandle FavorHandle;

        public class FavoriteViewModel : INotifyPropertyChanged
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

            public FavoriteViewModel()
            {
                this.PageInfor = new CPage(Global.FavorSubLists.Count);
                this.CurrentSublist = new BindingList<Food>();
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        private FavoriteViewModel _mainVM;

        public Favorite()
        {
            InitializeComponent();
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

        private void Favorite_Loaded(object sender, RoutedEventArgs e)
        {
            _mainVM = new FavoriteViewModel();

            var value = ConfigurationManager.AppSettings["FavorCurrentPage"];
            var favorcrtpage = int.Parse(value);

            if (Global.FavorSubLists.Count > 0)
            {
                if (IsValuablePageNumber(favorcrtpage) == true)
                {
                    _mainVM.PageInfor.CurrentPage = favorcrtpage;
                }
                else if(favorcrtpage > _mainVM.PageInfor.MaxPages)
                {
                    _mainVM.PageInfor.CurrentPage = _mainVM.PageInfor.MaxPages;
                }

                _mainVM.CurrentSublist = Global.FavorSubLists[_mainVM.PageInfor.CurrentPage - 1];
            }

            DataContext = _mainVM;
            //if (_mainVM.PageInfor.CurrentPage == 1)
            //{
            //    prevPageButton.IsEnabled = false;
            //}
            //else
            //{
            //    // Do nothing
            //}

            //if (_mainVM.PageInfor.CurrentPage == _mainVM.PageInfor.MaxPages)
            //{
            //    nextPageButton.IsEnabled = false;
            //}
            //else
            //{
            //    // Doo nothing
            //}
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

                _mainVM.CurrentSublist = Global.FavorSubLists[_mainVM.PageInfor.CurrentPage - 1];
                UpdateAppConfig("FavorCurrentPage");
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

                _mainVM.CurrentSublist = Global.FavorSubLists[_mainVM.PageInfor.CurrentPage - 1];
                UpdateAppConfig("FavorCurrentPage");

            }
        }

        /// <summary>
        /// Hàm xử lí khi nhấn bỏ thích món ăn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dislikeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var indexInList = IndexCurrentPage + (_mainVM.PageInfor.CurrentPage - 1) * TotalItemsPerPage;
                var food = Global.FavoriteFoodList[indexInList];
                var btn = sender as Button;
                var aws = btn.Content as ImageAwesome;
                // Gán thuộc tính yêu thích là Black
                food.Favorite = new StringBuilder("Black");
                // Gỡ bỏ món ăn khỏi danh sách yêu thích
                Global.FavoriteFoodList.Remove(food);
                // Chia lại sub list 
                Global.FavorSubLists = Global.ConvertListToSubLists(TotalItemsPerPage, Global.FavoriteFoodList);
                // Cập nhật List View
                PreparePageContent();            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Hàm cập nhật lại trang hiển thị sau khi đã bỏ yêu thích món ăn
        /// </summary>
        private void PreparePageContent()
        {
            if (Global.FavorSubLists.Count > 0)
            {
                _mainVM.PageInfor.MaxPages = Global.FavorSubLists.Count;

                if (_mainVM.PageInfor.CurrentPage > Global.FavorSubLists.Count)
                {
                    _mainVM.PageInfor.CurrentPage = _mainVM.PageInfor.MaxPages;
                }
                else { }

                _mainVM.CurrentSublist = Global.FavorSubLists[_mainVM.PageInfor.CurrentPage - 1];
            }
            else {
                _mainVM.CurrentSublist.Clear();
                _mainVM.PageInfor.CurrentPage = 1;
                _mainVM.PageInfor.MaxPages = 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectCurrentItem(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
            IndexCurrentPage = mealListView.SelectedIndex;
            item.IsSelected = false;
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
            UpdateAppConfig("FavorCurrentPage");
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
    }
}
