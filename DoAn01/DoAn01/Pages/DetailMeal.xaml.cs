using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace DoAn01
{
    /// <summary>
    /// Interaction logic for DetailMeal.xaml
    /// </summary>
    public partial class DetailMeal : Page
    {
        public delegate void DyingHandler();
        public event DyingHandler Dying;

        private DetailMealViewModel _mainVM;
        
        // Định nghĩa View Model cho trang Detail Meal
        class DetailMealViewModel
        {
            // Món ăn chính của màn hình
            public Food MainFood { get; set; }
            // Bước hiện tại hiển thị
            public Step CurrentStep { get; set; } = new Step();
            // Số bước tối đa và tối thiểu của công thức
            public int MaxStepIndex { get; set; } = 1;
            public int MinStepIndex { get; } = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        public DetailMeal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Hàm khởi tạo có tham số cho Page Detail Meal
        /// </summary>
        /// <param name="p"></param>
        public DetailMeal(Food p)
        {
            InitializeComponent();

            _mainVM = new DetailMealViewModel();
            _mainVM.MainFood = p;
        }

        /// <summary>
        /// Hàm khởi tạo các tham số cần thiết sau khi màn hình đã khởi tạo xong
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailMeal_Loaded(object sender, RoutedEventArgs e)
        {
            CreateFoodDetailPage();

            DataContext = _mainVM;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateFoodDetailPage()
        {
            _mainVM.MaxStepIndex = _mainVM.MainFood.StepList.Count;

            _mainVM.CurrentStep = _mainVM.MainFood.StepList[_mainVM.MinStepIndex - 1];
        }

        /// <summary>
        /// Hàm xử lí khi nhấn Back Step Button để chuyển qua thông tin bước trước của món ăn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backStepButton_Click(object sender, RoutedEventArgs e)
        {
            var index = _mainVM.CurrentStep.StepIndex;

            if (index > _mainVM.MinStepIndex && index <= _mainVM.MaxStepIndex)
            {
                index--;
            }
            else if (index == _mainVM.MinStepIndex)
            {
                index = _mainVM.MaxStepIndex;
            }

            _mainVM.CurrentStep = _mainVM.MainFood.StepList[index - 1];
        }

        /// <summary>
        /// Hàm xử lí khi nhấn Next Step Button để chuyển qua thông tin bước kế tiếp của món ăn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextStepButton_Click(object sender, RoutedEventArgs e)
        {
            var index = _mainVM.CurrentStep.StepIndex;

            if (index >= _mainVM.MinStepIndex && index < _mainVM.MaxStepIndex)
            {
                index++;
            }
            else if (index == _mainVM.MaxStepIndex)
            {
                index = _mainVM.MinStepIndex;
            }

            _mainVM.CurrentStep = _mainVM.MainFood.StepList[index - 1];
        }

        /// <summary>
        /// Hàm xử lí khi nhấn return Button để trở về trang Home/Favorite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

    }
}


