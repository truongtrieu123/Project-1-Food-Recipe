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

    public class Step
    {
        public string StepDetail { get; set; }
        public List<string> StepImage { get; set; }
        public int StepNumber { get; set; }

    }

    public class Food : Step
    {

        public List<Step> Recipe { get; set; }
        public string Name { get; set; }
        public string CoverSource { get; set; }
        public string VideosSource { get; set; }
        public string Introduction { get; set; }
        public string Ingredients { get; set; }
        public int Index { get; set; }
        public string Favorite { get; set; }

    }

    public class FoodMenu
    {

        public static List<Food> _list;
        public bool Favorite { get; set; }
        public FoodMenu()
        {
            _list = new List<Food>();
            Food item1 = new Food
            {
                Name = "Món ăn ngày tết ",
                CoverSource = "../../Images/home.jpg",
                Introduction = "Thịt kho hột vịt (còn gọi là thịt kho tàu hay thịt kho riệu) là một món ăn phổ biến tại miền Nam Việt Nam. Món ăn này đặc biệt thường được chế biến để dùng trong các ngày Tết Nguyên đán vì có thể làm sẵn, giữ được lâu ngày. Thịt kho hột vịt (còn gọi là thịt kho tàu hay thịt kho riệu) là một món ăn phổ biến tại miền Nam Việt Nam.Món ăn này đặc biệt thường được chế biến để dùng trong các ngày Tết Nguyên đán vì có thể làm sẵn, giữ được lâu ngày.",
                Recipe = new List<Step>
                {
                    new Step() { StepNumber = 1,
                        StepDetail = "Sơ chế thịt ba chỉ: rửa sạch, cắt miếng vừa ăn. Ướp muối, tiêu, bột ngọt, hạt nêm, nước mắm và hành tím với thịt trong 15 phút cho ngấm đều gia vị.",
                        StepImage = new List<string> { "../../Images/food01.jpg", "../../Images/food01.jpg", "../../Images/food01.jpg" }
                    },
                    new Step() { StepNumber = 2,
                        StepDetail = "Sơ chế thịt ba chỉ: rửa sạch, cắt miếng vừa ăn. Ướp muối, tiêu, bột ngọt, hạt nêm, nước mắm và hành tím với thịt trong 15 phút cho ngấm đều gia vị.",
                        StepImage = new List<string> { "../../Images/food01.jpg", "../../Images/food01.jpg", "../../Images/food01.jpg" }
                    },
                    new Step() { StepNumber = 3,
                        StepDetail = "Sơ chế thịt ba chỉ: rửa sạch, cắt miếng vừa ăn. Ướp muối, tiêu, bột ngọt, hạt nêm, nước mắm và hành tím với thịt trong 15 phút cho ngấm đều gia vị.",
                        StepImage = new List<string> { "../../Images/food01.jpg", "../../Images/food01.jpg", "../../Images/food01.jpg" }
                    },
                },
                Index = 0,
                Favorite = "Red",
                VideosSource = "https://www.youtube.com/watch?v=_dK2tDK9grQ",
                Ingredients = "1kg thịt heo, 5 quả hột vịt, 500ml nước dừa,...",
            };
            _list.Add(item1);

            for (int i = 1; i <= 25; i++)
            {
                Food item = new Food
                {
                    Name = "Thịt kho hột vịt",
                    CoverSource = "../../Images/food01.jpg",
                    Introduction = "Thịt kho hột vịt (còn gọi là thịt kho tàu hay thịt kho riệu) là một món ăn phổ biến tại miền Nam Việt Nam. Món ăn này đặc biệt thường được chế biến để dùng trong các ngày Tết Nguyên đán vì có thể làm sẵn, giữ được lâu ngày. Thịt kho hột vịt (còn gọi là thịt kho tàu hay thịt kho riệu) là một món ăn phổ biến tại miền Nam Việt Nam.Món ăn này đặc biệt thường được chế biến để dùng trong các ngày Tết Nguyên đán vì có thể làm sẵn, giữ được lâu ngày.",
                    Recipe = new List<Step>
                    {
                        new Step() { StepNumber = 1,
                            StepDetail = "Sơ chế thịt ba chỉ: rửa sạch, cắt miếng vừa ăn. Ướp muối, tiêu, bột ngọt, hạt nêm, nước mắm và hành tím với thịt trong 15 phút cho ngấm đều gia vị.",
                            StepImage = new List<string> {"../../Images/food01.jpg", "../../Images/food01.jpg", "../../Images/food01.jpg" }
                        },
                        new Step() { StepNumber = 2,
                            StepDetail = "Sơ chế thịt ba chỉ: rửa sạch, cắt miếng vừa ăn. Ướp muối, tiêu, bột ngọt, hạt nêm, nước mắm và hành tím với thịt trong 15 phút cho ngấm đều gia vị.",
                            StepImage = new List<string> {"../../Images/food01.jpg", "../../Images/food01.jpg", "../../Images/food01.jpg" }
                        },
                        new Step() { StepNumber = 3,
                            StepDetail = "Sơ chế thịt ba chỉ: rửa sạch, cắt miếng vừa ăn. Ướp muối, tiêu, bột ngọt, hạt nêm, nước mắm và hành tím với thịt trong 15 phút cho ngấm đều gia vị.",
                            StepImage = new List<string> { "../../Images/food01.jpg", "../../Images/food01.jpg", "../../Images/food01.jpg" }
                        },
                    },

                    Ingredients = "1kg thịt heo, 5 quả hột vịt, 500ml nước dừa,...",
                    Favorite = (i % 5 == 0) ? "Red" : "Black",
                    Index = i,
                    VideosSource = "https://www.youtube.com/watch?v=_dK2tDK9grQ",
                };
                _list.Add(item);
            }
        }

        public List<Food> GetAll()
        {
            return _list;
        }

    }
    public partial class Home : Page, INotifyPropertyChanged
    {

        public List<Food> _list;

        public List<Food> SubList;

        private System.Timers.Timer _timer;

        public int _currentPage;

        public int SelectedItemIndex { get; set; }

        public int Index { get; set; }

        public const int TotalItemsPerPage = 12;

        public event PropertyChangedEventHandler PropertyChanged;

        public Home()
        {
            InitializeComponent();

            _currentPage = 1;
            FoodMenu p = new FoodMenu();
            _list = p.GetAll();
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            mealListView.Items.Clear();
            CreateHomePage();
            Console.WriteLine("lallaalla");
        }

        public void CreateHomePage()
        {
            SubList = new List<Food>();

            if (_list.Count < TotalItemsPerPage)
                SubList = _list.GetRange(0, _list.Count);
            else
                SubList =  _list.GetRange(0, TotalItemsPerPage);

            pageNumber.Content = _currentPage;
            mealListView.ItemsSource = SubList;
        }

        private List<Food> SubListForNextPage()
        {
            int nextIndex = (_currentPage) * TotalItemsPerPage;
            int currentIndex = (_currentPage - 1) * TotalItemsPerPage;
            SubList = new List<Food>();

            if (nextIndex + 1 > _list.Count)
                SubList = _list.GetRange(currentIndex, _list.Count - currentIndex);
            else
            {
                if (_list.Count - nextIndex > TotalItemsPerPage)
                    SubList = _list.GetRange(nextIndex, TotalItemsPerPage);
                else
                    SubList = _list.GetRange(nextIndex, _list.Count - nextIndex);

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
                if (_list.Count < TotalItemsPerPage) SubList = _list.GetRange(0, _list.Count);
                else SubList = _list.GetRange(0, TotalItemsPerPage); // _list.count>=TotalItemsPerPage;
            }
            else
            {
                SubList = _list.GetRange(prevPage, TotalItemsPerPage);
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
                _ = (_list[indexInList].Favorite == "Black") ? _list[indexInList].Favorite = "Red" : _list[indexInList].Favorite = "Black";
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
