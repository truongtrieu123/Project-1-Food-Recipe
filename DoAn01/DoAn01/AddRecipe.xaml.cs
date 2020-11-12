using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace DoAn01
{
    /// <summary>
    /// Interaction logic for AddRecipe.xaml
    /// </summary>

    public partial class AddRecipe : Window
    {
        public delegate void DeathHandler();
        public event DeathHandler Dying;

        private Food _temp_food;
        private int _dayindex;
        class AddRecipeViewModel : INotifyPropertyChanged
        {
            public Step TempStep { get; set; }
            public BindingList<Step> StepList { get; set; }

            public AddRecipeViewModel()
            {
                TempStep = new Step();
                TempStep.StepImages = new BindingList<Image>();
                StepList = new BindingList<Step>();
            }
            public void ClearStep()
            {
                TempStep.StepIndex = -1;
                TempStep.StepDetail = new StringBuilder();
                TempStep.StepImages.Clear();
            }
            public void ClearImgList()
            {
                TempStep.StepImages.Clear();
            }
            public void ClearView()
            {
                ClearStep();
                StepList.Clear();
            }
            public event PropertyChangedEventHandler PropertyChanged;
        }
        private string _imagesFolder = string.Empty;
        private AddRecipeViewModel _mainVM;

        /// <summary>
        /// Khởi tạo cửa sổ App Recipe
        /// </summary>
        public AddRecipe()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Hàm xử lí sau khi cửa khổ đã khởi động xong
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Khởi tạo biến
            _mainVM = new AddRecipeViewModel();
            _temp_food = new Food();
            _temp_food.DayIndex = Global.FoodList.Count;
            _dayindex = Global.FoodList.Count + 1;

            // Tạo thư mục chứa ảnh
            var appFolder = AppDomain.CurrentDomain.BaseDirectory;
            _imagesFolder = @"Data\Images\Food\" + _dayindex.ToString();
            var imageFolder = Path.Combine(appFolder, _imagesFolder);
            _imagesFolder += @"\";

            if (Directory.Exists(imageFolder))
            {
                Directory.Delete(imageFolder);
            }
            else
            {
                //Do nothing
            }

            Directory.CreateDirectory(imageFolder);

            // View Model
            DataContext = _mainVM;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Dying?.Invoke();
        }

        /// <summary>
        /// Hàm di chuyển cửa sổ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleOfWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.StackPanel;
            var win = Window.GetWindow(move);
            win.DragMove();
        }

        /// <summary>
        /// Hàm xử lí khi cập nhật dữ liệu Food Name TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void foodNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;

            if (textbox != null)
            {
                if (textbox.Text != "")
                {
                    var text = textbox.Text;
                    _temp_food.Name = new StringBuilder(text);
                }
                else { }
            }
            else { }
        }

        /// <summary>
        /// Hàm xử lí khi cập nhật dữ liệu Introduction TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void introductionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;

            if (textbox != null)
            {
                if (textbox.Text != "")
                {
                    _temp_food.Introduction = new StringBuilder(textbox.Text);
                }
                else { }
            }
            else { }
        }

        /// <summary>
        /// Hàm xử lí khi cập nhập dữ liệu Ingredients TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ingredientsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;

            if (textbox != null)
            {
                if (textbox.Text != "")
                {
                    _temp_food.Ingredients = new StringBuilder(textbox.Text);
                }
                else { }
            }
            else { }
        }

        /// <summary>
        /// Hàm xử lí khi cập nhập dữ liệu Videosource TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videoSourceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;

            if (textbox != null)
            {
                if (textbox.Text != "")
                {
                    _temp_food.VideoSource = new StringBuilder(textbox.Text);
                }
                else { }
            }
            else { }
        }

        /// <summary>
        /// Xử lí khi click thêm ảnh Cover cho món ăn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_image_foodButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            var folder = AppDomain.CurrentDomain.BaseDirectory;

            // Thiết đặt bộ lọc (filter) cho file hình ảnh
            var codecs = ImageCodecInfo.GetImageEncoders();
            var sep = string.Empty;

            foreach (var c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                screen.Filter = String.Format("{0}{1}{2} ({3})|{3}", screen.Filter, sep, codecName, c.FilenameExtension);
                sep = "|";
            }

            screen.Filter = String.Format("{0}{1}{2} ({3})|{3}", screen.Filter, sep, "All Files", "*.*");

            screen.Title = "Choose food cover image";
            screen.InitialDirectory = folder;
            screen.FilterIndex = 2;
            screen.RestoreDirectory = true;

            if (screen.ShowDialog() == true)
            {
                var filepath = screen.FileName;
                Uri bitmap = new Uri(filepath);

                foodCoverImage.Source = new BitmapImage(bitmap);
                _temp_food.CoverSource = new StringBuilder(filepath);
            }

        }

        /// <summary>
        /// Hàm xử lí khi cập nhật dữ liệu Step detail TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stepDetailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;

            if (textbox != null)
            {
                if (textbox.Text != "")
                {
                    _mainVM.TempStep.StepDetail = new StringBuilder(textbox.Text);
                }
                else { }
            }
            else { }
        }

        /// <summary>
        /// Hàm xử lí thêm hình ảnh của một bước
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_image_stepButton_Click(object sender, RoutedEventArgs e)
        {
            _mainVM.ClearImgList();

            var screen = new OpenFileDialog();
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var codecs = ImageCodecInfo.GetImageEncoders();
            var sep = string.Empty;

            foreach (var c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                screen.Filter = String.Format("{0}{1}{2} ({3})|{3}", screen.Filter, sep, codecName, c.FilenameExtension);
                sep = "|";
            }

            screen.Filter = String.Format("{0}{1}{2} ({3})|{3}", screen.Filter, sep, "All Files", "*.*");

            screen.Multiselect = true;
            screen.Title = "Choose Food Step Images";
            screen.InitialDirectory = folder;
            screen.FilterIndex = 2;
            screen.RestoreDirectory = true;

            // Nếu Dialog chọn được hình ảnh
            if (screen.ShowDialog() == true)
            {
                var paths = screen.FileNames;

                foreach (var filepath in paths)
                {
                    // Thêm hình ảnh vào list view của bươcs 
                    _mainVM.TempStep.StepImages.Add(
                        new Image { ImgPath = new StringBuilder(filepath) }
                        );
                }

            }
            else { }
        }

        /// <summary>
        /// Hàm xử lí khi nhấn Add step Button để thêm bước vào công thức nấu ăn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_stepButton_Click(object sender, RoutedEventArgs e)
        {
            if (stepDetailTextBox.Text != "")
            {
                stepDetailTextBox.Text = "";
                var step = new Step()
                {
                    StepDetail = _mainVM.TempStep.StepDetail,
                    StepImages = new BindingList<Image>(_mainVM.TempStep.StepImages.ToList<Image>()),
                    StepIndex = _mainVM.StepList.Count + 1
                };

                //foreach (var imgpath in _mainVM.TempStep.StepImages)
                //{
                //    step.StepImages.Add(imgpath);
                //}

                _mainVM.StepList.Add(step);
                // Xóa ảnh minh họa của bước
                _mainVM.ClearStep();
            }
            else
            {
                MessageBox.Show("Bạn phải bổ sung đầy đủ chi tiết bước", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Hàm xử lí khi nhấn khi nhấn Cancel Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
                foodNameTextBox.Text = "";
                introductionTextBox.Text = "";
                ingredientsTextBox.Text = "";
                videoSourceTextBox.Text = "";
                stepDetailTextBox.Text = "";
                foodCoverImage.Source = null;
                _mainVM.ClearView();
        }

        /// <summary>
        /// Hàm xử lí khi nhần Save Button để thêm công thức nấu ăn vào danh sách
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (videoSourceTextBox.Text != "" && introductionTextBox.Text != "" && foodNameTextBox.Text != "" && ingredientsTextBox.Text != "" && foodCoverImage.Source != null)
            {
                if (_mainVM.StepList.Count != 0)
                {
                    // Xóa dữ liệu hiển thị
                    foodNameTextBox.Text = "";
                    introductionTextBox.Text = "";
                    ingredientsTextBox.Text = "";
                    videoSourceTextBox.Text = "";
                    foodCoverImage.Source = null;
                    stepDetailTextBox.Text = "";
                    // Cập nhật dữ liệu trên ứng dụng và trên bộ nhớ
                    UpdateFoodList();
                    SaveImgsToData();
                    // Xóa dữ liệu View Model
                    _mainVM.ClearView();
                    var confirmBox = MessageBox.Show($"Thêm thành công món {_temp_food.Name} vào danh sách!\nBạn có muốn tiếp tục?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (confirmBox == MessageBoxResult.Yes)
                    {

                    }
                    else
                    {
                        this.Close();
                    }
                }
                if (stepsListView.Items.IsEmpty == true
                    && videoSourceTextBox.Text != "" && introductionTextBox.Text != ""
                    && foodNameTextBox.Text != "" && ingredientsTextBox.Text != "")
                {
                    MessageBox.Show("Bạn phải thêm bước nấu ăn (bao gồm thông tin, hình ảnh và thêm vào danh sách)!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Bạn phải thêm đủ tất cả thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Hàm xử lí khi nhấn Return Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            //Dying?.Invoke();
            this.Close();
        }

        /// <summary>
        ///  Hàm lưu hình ảnh vào thư mục Project
        /// </summary>
        private void SaveImgsToData()
        {

        }

        /// <summary>
        /// Hàm cập nhật Global.FoodList
        /// </summary>
        private void UpdateFoodList()
        {
            // Cập nhật đường dẫn ảnh Cover của Temp Food
            _temp_food.CoverSource = new StringBuilder($"{_imagesFolder}ava.jpg");
            // Cập nhật số bước của biến Temp Food
            _temp_food.CountSteps = _mainVM.StepList.Count;
            // Cập nhật ngày tạo cho Temp Food
            _temp_food.DayIndex = _dayindex;

            // Thêm step vào Step List
            for(var pos = 0; pos<_mainVM.StepList.Count;  pos++)
            {
                var tmp_step = new Step()
                {
                    StepDetail = _mainVM.StepList[pos].StepDetail,
                    StepIndex = _mainVM.StepList[pos].StepIndex
                };
                var pos2 = 1;

                foreach (var img in _mainVM.StepList[pos].StepImages)
                {
                    tmp_step.StepImages.Add(new Image
                    {
                        ImgPath = new StringBuilder($"{_imagesFolder}step{pos+1} ({pos2}).jpg")
                    });

                    pos2++;
                }

                _temp_food.StepList.Add(tmp_step);
            }

            // Cập nhật dự liệu
            Global.FoodList.Add(new Food()
            {
                Name = _temp_food.Name,
                Introduction = _temp_food.Introduction,
                Ingredients = _temp_food.Ingredients,
                VideoSource = _temp_food.VideoSource,
                CoverSource = _temp_food.CoverSource,
                StepList = new BindingList<Step>(_temp_food.StepList.ToList<Step>()),
                CountSteps = _temp_food.CountSteps,
                DayIndex = _temp_food.DayIndex,
                Favorite = new StringBuilder("Black")
            });
        }
    }
}

