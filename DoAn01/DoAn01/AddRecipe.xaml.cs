using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private List<BindingList<Step>> _temp_liststeps;
        private BindingList<Step> _temp_step;
        public AddRecipe()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _temp_food = new Food();
            _temp_liststeps = new List<BindingList<Step>>();
            _temp_step = new BindingList<Step>();
         
            _index = Global.FoodList.Count;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Dying?.Invoke();
        }
        ///
        ///Di chuyen man hinh
        private void TitleOfWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.StackPanel;
            var win = Window.GetWindow(move);
            win.DragMove();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            Dying?.Invoke();
            this.Close();
        }

        /// <summary>
        /// 
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
                || stepDetailTextBox.Text != "" || ingradientsTextBox.Text != "" || listViewStepFood.Items.IsEmpty == false || foodImageBox.Source != bitmap)
            {
                videoSourceTextBox.Text = "";
                introductionTextBox.Text = "";
                foodNameTextBox.Text = "";
                stepDetailTextBox.Text = "";
                ingradientsTextBox.Text = "";
                //listImagePath.Clear();
                listImageFullStepFood.Items.Clear();
                //listStep.Clear();
                foodImageBox.Source = bitmap;
            }

        }

        /// <summary>
        /// 
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
            if (videoSourceTextBox.Text != "" && introductionTextBox.Text != "" && foodNameTextBox.Text != "" && ingradientsTextBox.Text != "" && foodImageBox.Source != bitmap)
            {
                //test.nameFood = foodNameTextBox.Text;
                //test.motaFood = descriptionBox.Text;
                //test.youtubeLink = videoSourceTextBox.Text;
                //test.nguyenlieuFood = ingradientBox.Text;

                if (listImageFullStepFood.Items.IsEmpty == false && listViewFullStepFood.Items.IsEmpty == false)
                {
                    videoSourceTextBox.Text = "";
                    introductionTextBox.Text = "";
                    foodNameTextBox.Text = "";
                    ingradientsTextBox.Text = "";
                    foodImageBox.Source = bitmap;
                    stepDetailTextBox.Text = "";
                    listImageFullStepFood.Items.Clear();
                    //listImagePath.Clear();
                    //listStep.Clear();
                    MessageBox.Show("Thành công!");
                    Dying?.Invoke();
                    this.Close();
                }
                if (listImageFullStepFood.Items.IsEmpty == true && listViewFullStepFood.Items.IsEmpty == true 
                    && videoSourceTextBox.Text != "" && introductionTextBox.Text != "" 
                    && foodNameTextBox.Text != "" && ingradientsTextBox.Text != "" && foodImageBox.Source != bitmap)
                {
                    MessageBox.Show("Bạn phải thêm bước nấu ăn (bao gồm thông tin, hình ảnh và thêm vào danh sách)!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Bạn phải thêm đủ tất cả thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //add image Food 
        private void add_image_foodButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            //screen.Filter =""
            if (screen.ShowDialog() == true)
            {
                var filepath = screen.FileName;
                Uri bitmap = new Uri(filepath);
                foodImageBox.Source = new BitmapImage(bitmap);
            }
        }

        //Dem hinh anh moi buoc
        int countImageStep;

        //add image step foo
        private void add_image_stepButton_Click(object sender, RoutedEventArgs e)
        {
            countImageStep = 0;
            var screen = new OpenFileDialog();
            screen.Multiselect = true;
            if (screen.ShowDialog() == true)
            {
                var countImages = screen.FileNames;
                foreach (var filepath in screen.FileNames)
                {
                    countImageStep++;
                    //listImagePath.Add(new listImage() {listImages= filepath});
                }
                //imageTest.countImageStep = countImageStep;
            }
        }

        //add buoc nau an vao list
        private void add_stepButton_Click(object sender, RoutedEventArgs e)
        {
            if (stepDetailTextBox.Text != "" && listViewStepFood.Items.IsEmpty == false)
            {
                //listStep.Add(new Add_Recipe()
                //{
                //    numberStep = $"Bước {listStep.Count + 1}",
                //    stepMakeFood = stepDetailTextBox.Text
                //});

                //foreach (var item in listImagePath)
                //{
                //    listImageFullStepFood.Items.Add(new BitmapImage(new Uri(item.listImages)));
                //}
                //stepDetailTextBox.Text = "";
                //listImagePath.Clear();
            }
            else
            {
                MessageBox.Show("Bạn phải thêm đủ thông tin bước và ảnh", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void foodNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            
            if (textbox != null)
            {
                if (textbox.Text != "")
                    _temp_food.Name = textbox.Text;
            }
            else
            {
                // Do nothing
            }
        }

        private void introductionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ingradientsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void videoSourceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void foodImageBox_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            var img = sender as Image;

            if(img != null)
            {
                _temp_food.CoverSource = img.Source.ToString();
            }
        }

        private void stepDetailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

