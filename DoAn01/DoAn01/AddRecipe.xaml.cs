using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
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
        private int _index;
        private Step _temp_step;
        private BindingList<Step> _temp_steplist;
        private string _imagesPath = string.Empty;
        public AddRecipe()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Khởi tạo biến
            _temp_food = new Food();
            _temp_food.DayIndex = Global.FoodList.Count;
            _temp_step = new Step();
            _temp_step.StepImages = new BindingList<Image>();
            _temp_steplist = new BindingList<Step>();

            _index = Global.FoodList.Count + 1;

            // Tạo thư mục chứa ảnh
            var appFolder = AppDomain.CurrentDomain.BaseDirectory;
            var imageFolder = Path.Combine(appFolder, @"Data\Images\Food\", _index.ToString());
            _imagesPath = imageFolder;

            if (Directory.Exists(imageFolder))
            {
                Directory.Delete(imageFolder);
            }
            else
            {
                //Do nothing
            }

            Directory.CreateDirectory(imageFolder);

            //
            stepImageslListView.ItemsSource = _temp_step.StepImages;
            stepsListView.ItemsSource = _temp_steplist;
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

                    _temp_food.Name = textbox.Text;
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
                    _temp_food.Introduction = textbox.Text;
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
                    _temp_food.Ingredients = textbox.Text;
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
                    _temp_food.VideoSource = textbox.Text;
                }
                else { }
            }
            else { }
        }

        /// <summary>
        /// Xử lí khi click thêm ảnh cover cho món ăn
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
                _temp_food.CoverSource = filepath;
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
                    _temp_step.StepDetail = textbox.Text;
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
            _temp_step.StepImages.Clear();

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
            screen.Title = "Choose food cover image";
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
                    _temp_step.StepImages.Add(
                        new Image { ImgPath = filepath }
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
            if (stepDetailTextBox.Text != "" && stepImageslListView.Items.IsEmpty == false)
            {
                stepDetailTextBox.Text = "";
                _temp_steplist.Add(_temp_step);
            }
            else
            {
                MessageBox.Show("Bạn phải thêm đủ thông tin bước và ảnh", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Hàm xử lí khi nhấn khi nhấn Cancel Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var bitmap = new BitmapImage(
                                    new Uri(
                                           "",
                                           UriKind.Relative)
                                    );
            if (videoSourceTextBox.Text != "" || introductionTextBox.Text != "" || foodNameTextBox.Text != ""
                || stepDetailTextBox.Text != "" || ingredientsTextBox.Text != ""
                || stepImageslListView.Items.IsEmpty == false || foodCoverImage.Source != bitmap)
            {
                videoSourceTextBox.Text = "";
                introductionTextBox.Text = "";
                foodNameTextBox.Text = "";
                stepDetailTextBox.Text = "";
                ingredientsTextBox.Text = "";
                //listImagePath.Clear();
                stepsListView.Items.Clear();
                //listStep.Clear();
                foodCoverImage.Source = bitmap;
            }

        }

        /// <summary>
        /// Hàm xử lí khi nhần Save Button để thêm công thức nấu ăn vào danh sách
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var bitmap = new BitmapImage(
                                    new Uri(
                                           "",
                                           UriKind.Relative)
                                    );
            if (videoSourceTextBox.Text != "" && introductionTextBox.Text != "" && foodNameTextBox.Text != "" && ingredientsTextBox.Text != "" && foodCoverImage.Source != bitmap)
            {
                //test.nameFood = foodNameTextBox.Text;
                //test.motaFood = descriptionBox.Text;
                //test.youtubeLink = videoSourceTextBox.Text;
                //test.nguyenlieuFood = ingradientBox.Text;

                if (stepsListView.Items.IsEmpty == false)
                {
                    videoSourceTextBox.Text = "";
                    introductionTextBox.Text = "";
                    foodNameTextBox.Text = "";
                    ingredientsTextBox.Text = "";
                    foodCoverImage.Source = bitmap;
                    stepDetailTextBox.Text = "";
                    stepsListView.Items.Clear();
                    //listImagePath.Clear();
                    //listStep.Clear();
                    MessageBox.Show("Thành công!");
                    Dying?.Invoke();
                    this.Close();
                }
                if (stepsListView.Items.IsEmpty == true
                    && videoSourceTextBox.Text != "" && introductionTextBox.Text != ""
                    && foodNameTextBox.Text != "" && ingredientsTextBox.Text != "" && foodCoverImage.Source != bitmap)
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
            Dying?.Invoke();
            this.Close();
        }
    }
}

