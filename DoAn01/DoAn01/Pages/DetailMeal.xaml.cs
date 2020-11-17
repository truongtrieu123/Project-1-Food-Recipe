using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace DoAn01
{
    /// <summary>
    /// Interaction logic for DetailMeal.xaml
    /// </summary>
    public partial class DetailMeal : Page
    {
        //public delegate void DyingHandler();
        //public event DyingHandler Dying;

        private DetailMealViewModel _mainVM;

        // Định nghĩa View Model cho trang Detail Meal
        class DetailMealViewModel : INotifyPropertyChanged
        {
            // Món ăn chính của màn hình
            public Food MainFood { get; set; }
            // Bước hiện tại hiển thị
            private Step currentStep = null;
            public Step CurrentStep
            {
                get
                { return this.currentStep; }
                set
                {
                    if (value != this.currentStep)
                    {
                        this.currentStep = new Step()
                        {
                            StepDetail = value.StepDetail,
                            StepIndex = value.StepIndex,
                            StepImages = value.StepImages
                        };
                        OnPropertyChanged("CurrentStep");
                    }
                }
            }
            // Số bước tối đa và tối thiểu của công thức
            public int MinStepIndex { get; } = 1;
            public int MaxStepIndex { get; set; } = 1;

            public event PropertyChangedEventHandler PropertyChanged;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="name"></param>
            private void OnPropertyChanged(string name)
            {
                var handler = PropertyChanged;

                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
                else { }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        //public DetailMeal()
        //{
        //    InitializeComponent();
        //}

        /// <summary>
        /// Hàm khởi tạo có tham số cho Page Detail Meal
        /// </summary>
        /// <param name="p"></param>
        public DetailMeal(Food p = null)
        {
            InitializeComponent();

            if (p != null)
            {
                _mainVM = new DetailMealViewModel();
                _mainVM.MainFood = p;
            }
            else { }
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
            foodVideoWebBrowser.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Hàm xử lí khi nhấn Xem Video để khởi động video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foodCoverImage.Opacity = 0;
            CreateHTMLVideo();
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateHTMLVideo()
        {
            foodVideoWebBrowser.Visibility = Visibility.Visible;
            foodCoverImage.Opacity = 0;
            try
            {
                string source = _mainVM.MainFood.VideoSource.ToString();
                string videoCode = source.Replace($".be/", "be.com/embed/");
                videoCode = videoCode.Replace("watch?=", "embed/");
                //Console.WriteLine(videoCode);
                string html = "<html><head>";
                html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
                html += "</head><body style=\"overflow: hidden;\">" ;
                html += $"<iframe width='380' height='210' src='{videoCode}' frameborder='0' allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture' style='overflow-x: hidden'>";
                html += "</body></html>";
                foodVideoWebBrowser.NavigateToString(html);
            }
            catch (Exception e)
            {
                string source = _mainVM.MainFood.VideoSource.ToString();
                foodVideoWebBrowser.Source = new Uri(source);
            }
        }

        /// <summary>
        /// Đây là hàm kfsndjfbhjd
        /// </summary>
        public void CreateFoodDetailPage()
        {
            _mainVM.MaxStepIndex = _mainVM.MainFood.CountSteps;
            _mainVM.CurrentStep = _mainVM.MainFood.StepList[_mainVM.MinStepIndex - 1];
        }

        /// <summary>
        /// Hàm xử lí khi nhấn Back Step Button để chuyển qua thông tin bước trước của món ăn
        /// </summary>
        /// <param name="sender">dffffs</param>
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
            // Dừng video
            foodVideoWebBrowser.Dispose();
            foodCoverImage.ImageSource = null;
            //
            this.NavigationService.GoBack();
        }
    }
}


