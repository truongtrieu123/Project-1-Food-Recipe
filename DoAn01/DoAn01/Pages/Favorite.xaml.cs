using FontAwesome.WPF;
using System;
using System.Collections.Generic;
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

        private List<Food> _mainList { get; set; }
        // item index in current page;
        public int IndexCurrentPage { get; set; }

        //public delegate void LikeDislikeHandle();
        //public event LikeDislikeHandle FavorHandle;

        public class FavoriteViewModel : INotifyPropertyChanged
        {
            // Quản lí việc phân trang
            private CPage pageInfor = null;
            public CPage PageInfor
            {
                get { return this.pageInfor; }
                set
                {
                    if (value != this.pageInfor)
                    {
                        this.pageInfor = new CPage()
                        {
                            CurrentPage = value.CurrentPage,
                            MaxPages = value.MaxPages
                        };
                        OnPropertyChanged("PageInfor");
                    }
                    else { return; }
                }
            }
            // Quản lí số lượng food
            private int totalFood;
            public int TotalFood
            {
                get { return this.totalFood; }
                set
                {
                    if (value != this.totalFood)
                    {
                        this.totalFood = value;
                        OnPropertyChanged("TotalFood");
                    }
                    else { return; }
                }
            }
            // Quản lí các sublist của các trang
            public List<BindingList<Food>> MainSublists { get; set; }
            // Quản lí sublist hiện tại để hiển thị
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
                this.MainSublists = new List<BindingList<Food>>();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Favorite_Loaded(object sender, RoutedEventArgs e)
        {
            _mainList = new List<Food>();
            _mainList = Global.FavoriteFoodList;

            _mainVM = new FavoriteViewModel();
            _mainVM.MainSublists = Global.FavorSubLists;
            _mainVM.TotalFood = Global.FavoriteFoodList.Count;

            var value = ConfigurationManager.AppSettings["FavorCurrentPage"];
            var favorcrtpage = int.Parse(value);

            if (Global.FavorSubLists.Count > 0)
            {
                if (IsValuablePageNumber(favorcrtpage) == true)
                {
                    _mainVM.PageInfor.CurrentPage = favorcrtpage;
                }
                else if (favorcrtpage > _mainVM.PageInfor.MaxPages)
                {
                    _mainVM.PageInfor.CurrentPage = _mainVM.PageInfor.MaxPages;
                }

                _mainVM.CurrentSublist = _mainVM.MainSublists[_mainVM.PageInfor.CurrentPage - 1];
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
                var food = _mainList[indexInList];
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
            else
            {
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
        ///  Hàm xử lí khi click chuột trái
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectCurrentItem(object sender, MouseButtonEventArgs e)
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

        /// <summary>
        /// Hàm khôi phục lại view cho trang Home
        /// </summary>
        private void RestoreViewModel()
        {
            Global.SearchResultList.Clear();
            Global.SearchResultSubLists.Clear();
            // Khôi phục View Model
            _mainList = Global.FavoriteFoodList;
            _mainVM.TotalFood = Global.FavoriteFoodList.Count;
            _mainVM.PageInfor = new CPage(Global.FavorSubLists.Count);
            _mainVM.MainSublists = Global.FavorSubLists;
            _mainVM.CurrentSublist.Clear();

            if (_mainVM.MainSublists.Count > 0)
            {
                _mainVM.CurrentSublist = _mainVM.MainSublists[0];
            }
            else { }
        }

        /// <summary>
        /// Hàm chuẩn bị list và sublists cho việc search
        /// </summary>
        /// <param name="searchText"></param>
        private void PrePareSearchList(string searchText)
        {
            var key = ConvertToUnsignString(searchText.ToLower());
            var food = new Food();
            var searchlist = new List<Food>();

            // Duyệt và tìm kiếm
            foreach (var ele in Global.FavoriteFoodList)
            {
                var name = ele.Name.ToString().ToLower();
                var newname = ConvertToUnsignString(name);

                if (newname.Contains(key) == true)
                {
                    searchlist.Add(ele);
                }
            }

            // Cập nhật Search list của Global, chia lại sublist
            Global.SearchResultList.Clear();
            Global.SearchResultList = new List<Food>(searchlist);
            Global.SearchResultSubLists = Global.ConvertListToSubLists(Global.ItemsPerPage, Global.SearchResultList);
            // Cập nhật View Model
            _mainList = Global.SearchResultList;
            _mainVM.TotalFood = _mainList.Count;
            _mainVM.PageInfor = new CPage(Global.SearchResultSubLists.Count);
            _mainVM.MainSublists = Global.SearchResultSubLists;
            _mainVM.CurrentSublist.Clear();

            if (_mainVM.MainSublists.Count > 0)
            {
                _mainVM.CurrentSublist = _mainVM.MainSublists[0];
            }
            else { }
        }

        /// <summary>
        /// Hàm chuyển từ ký tự Unicode có dấu tiếng việt sang các chữ cái không dấu
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private string ConvertToUnsignString(string src)
        {
            var result = src;
            var oldchars = "áàãảạăắằẳẵặâấầẩẫậđéèẻẽẹêếềểễệíìỉĩịóòõỏọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵ";
            var newchars = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyy";
            var len = oldchars.Length;

            for (var pos = 0; pos < len; pos++)
            {
                result = result.Replace(oldchars[pos], newchars[pos]);
            }

            return result;
        }

        /// <summary>
        /// Hàm chuyển từ ký tự Unicode có dấu tiếng việt sang các chữ cái không dấu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var textbox = sender as TextBox;

                if (textbox != null)
                {
                    if (textbox.Text == String.Empty)
                    {
                        RestoreViewModel();
                    }
                    else
                    {
                        PrePareSearchList(textbox.Text);
                    }
                }
                else { }
            }
            else
            { //Do nothing 
            }
        }

        /// <summary>
        /// Xử lí khi nhấn Cancel Button để xóa Search TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelSearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchTextBox.Text = String.Empty;
            RestoreViewModel();
        }


    }
}
