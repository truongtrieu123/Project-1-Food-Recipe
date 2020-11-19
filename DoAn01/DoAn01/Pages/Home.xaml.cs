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

        private List<Food> _mainList { get; set; }
        // item index in current page;
        public int IndexCurrentPage { get; set; }
        //
        public class HomeViewModel : INotifyPropertyChanged
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
            private List<BindingList<Food>> mainSublists = null;
            public List<BindingList<Food>> MainSublists
            {
                get { return this.mainSublists; }
                set
                {
                    if (value != this.mainSublists)
                    {
                        var list = new List<BindingList<Food>>();

                        foreach (var sublist in value)
                        {
                            list.Add(new BindingList<Food>(sublist.ToList<Food>()));
                        }

                        this.mainSublists = list;
                        OnPropertyChanged("MainSublists");
                    }
                    else { return; }
                }
            }
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

            public event PropertyChangedEventHandler PropertyChanged;
            public HomeViewModel()
            {
                this.PageInfor = new CPage(Global.HomeSubLists.Count);
                this.CurrentSublist = new BindingList<Food>();
                this.MainSublists = new List<BindingList<Food>>();
            }
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
            _mainList = new List<Food>();
            _mainList = Global.FoodList;

            _mainVM = new HomeViewModel();
            _mainVM.MainSublists = Global.HomeSubLists;
            _mainVM.TotalFood = Global.FoodList.Count;

            var value = ConfigurationManager.AppSettings["HomeCurrentPage"];
            var arrangemode = ConfigurationManager.AppSettings["HomeArrangeMode"];
            var homecrtpage = int.Parse(value);

            if (IsValuablePageNumber(homecrtpage) == true)
            {
                _mainVM.PageInfor.CurrentPage = homecrtpage;
            }
            else
            {
                // current page bằng 1
            }

            _mainVM.CurrentSublist = _mainVM.MainSublists[_mainVM.PageInfor.CurrentPage - 1];

            // Cập nhật sort mode
            if (arrangemode == "none")
            {

            }
            else
            {
                var index = int.Parse(arrangemode);

                if (index != 0 && index != 1 && index != 2)
                { }
                else { sortSelectionComboBox.SelectedIndex = index; }
            }

            DataContext = _mainVM;
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
                _mainVM.CurrentSublist = _mainVM.MainSublists[_mainVM.PageInfor.CurrentPage - 1];

                if (_mainList == Global.FoodList)
                {
                    UpdateAppConfig("HomeCurrentPage", _mainVM.PageInfor.CurrentPage.ToString());
                }
                else { }
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
                _mainVM.CurrentSublist = _mainVM.MainSublists[_mainVM.PageInfor.CurrentPage - 1];

                if (_mainList == Global.FoodList)
                {
                    UpdateAppConfig("HomeCurrentPage", _mainVM.PageInfor.CurrentPage.ToString());
                }
                else { }
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
                var food = _mainList[indexInList];
                var changeCheck = 0;
                var btn = sender as Button;
                var aws = btn.Content as ImageAwesome;

                if (aws.Foreground == Brushes.Red)
                {
                    //change heart icon's color
                    aws.Foreground = Brushes.Black;
                    // update property in food list
                    food.Favorite = new StringBuilder("Black");
                    changeCheck = -1;
                }
                else if (aws.Foreground == Brushes.Black)
                {
                    //change heart icon's color
                    aws.Foreground = Brushes.Red;
                    // update property in food list
                    food.Favorite = new StringBuilder("Red");
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
            var p = (Food)mealListView.Items[IndexCurrentPage];
            var page = new DetailMeal(p);
            //
            UpdateAppConfig("HomeCurrentPage", _mainVM.PageInfor.CurrentPage.ToString());
            // Chuyển hướng hiển thị xang page
            this.NavigationService.Navigate(page);
        }

        /// <summary>
        /// Hàm cập nhập file App.Config
        /// </summary>
        /// <param name="_name"></param>
        private void UpdateAppConfig(string _name, string value)
        {
            // Lưu lại trang hiện tại của PageInfor
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[_name].Value = value;
            config.Save(ConfigurationSaveMode.Minimal);

            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// Xử lí khi nhấn enter để tìm kiếm theo tên món ăn
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
        /// Khôi phục lại view cho trang Home
        /// </summary>
        private void RestoreViewModel()
        {
            Global.SearchResultList.Clear();
            Global.SearchResultSubLists.Clear();
            // Khôi phục View Model
            _mainList = Global.FoodList;
            _mainVM.TotalFood = Global.FoodList.Count;
            _mainVM.PageInfor = new CPage(Global.HomeSubLists.Count);
            _mainVM.MainSublists = Global.HomeSubLists;
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
            foreach (var ele in Global.FoodList)
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
        /// Xử lí khi nhấn Cancel Button để xóa Search TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelSearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchTextBox.Text = String.Empty;
            RestoreViewModel();
        }

        private void SelectCurrentItem(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            item.IsSelected = true;
            IndexCurrentPage = mealListView.SelectedIndex;
            item.IsSelected = false;
        }

        /// <summary>
        /// Hàm xử lí khi chế độ sort thay đổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sortSelectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = sender as ComboBox;

            if (combobox != null)
            {
                var index = combobox.SelectedIndex;

                SortListView(index);
            }
            else { }
        }

        /// <summary>
        /// Hàm xử lí sort lại listview
        /// </summary>
        /// <param name="index"></param>
        private void SortListView(int index)
        {
            var list = _mainList;

            if (index == 0)
            {
                list.Sort(new FoodNameIncComparer());
            }
            else if (index == 1)
            {
                list.Sort(new FoodNameDecComparer());
            }
            else
            {
                list.Sort(new FoodDayIndexComparer());
            }

            if (_mainList.Equals(Global.FoodList))
            {
                _mainVM.MainSublists = Global.ConvertListToSubLists(Global.ItemsPerPage, Global.FoodList);
            }
            else if (_mainList.Equals(Global.SearchResultList))
            {
                _mainVM.MainSublists = Global.ConvertListToSubLists(Global.ItemsPerPage, Global.SearchResultList);
            }

            _mainVM.CurrentSublist = _mainVM.MainSublists[_mainVM.PageInfor.CurrentPage - 1];

            UpdateAppConfig("HomeArrangeMode", index.ToString());
        }
    }
}
