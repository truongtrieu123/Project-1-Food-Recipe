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

        class DetailMealViewModel
        {
            public Food MainFood { get; set; }
            public Step CurrentStep { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DetailMeal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public DetailMeal(Food p)
        {
            InitializeComponent();

            _mainVM.MainFood = p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailMeal_Loaded(object sender, RoutedEventArgs e)
        {
            CreateFoodDetailPage();
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateFoodDetailPage()
        {
            //foodNameTextBlock.Text = Info.Name.ToString();
            //introTextBlock.Text = Info.Introduction.ToString();
            //introTextBlock.Text = Info.Ingredients.ToString();

            InitializeStepDetailInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeStepDetailInfo()
        {
            //int index = CurrentStep - 1;

            //stepDetailTextBlock.Text = Info.StepList[index].StepDetail.ToString();

            // List ảnh của step
            //HinhAnhTungStep.ItemsSource = CreateListImagePath(Info.StepList[index].StepImages);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backStepButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextStepButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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


