using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace DoAn01
{
    /// <summary>
    /// Interaction logic for AddRecipe.xaml
    /// </summary>

    

    public partial class AddRecipe : Window
    {
        public delegate void DeathHandler();
        public event DeathHandler Dying;

        class Add_Recipe
        {
            public string numberStep { get; set; }
            public string stepMakeFood { get; set; }
        }
        class listImage
        {
            public string listImages { get; set; }
        }
        BindingList<Add_Recipe> listStep;
        BindingList<listImage> listImagePath;
        class stepDao
        {
            public static BindingList<Add_Recipe> GetAll()
            {
                var list = new BindingList<Add_Recipe>() { };
                return list;
            }
        }
        class stepImageDao
        {
            public static BindingList<listImage> GetAll()
            {
                var list = new BindingList<listImage>() { };
                return list;
            }
        }
       
        public AddRecipe()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listStep = stepDao.GetAll();
            listViewFullStepFood.ItemsSource = listStep;

            listImagePath = stepImageDao.GetAll();
            listViewStepFood.ItemsSource = listImagePath;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Dying?.Invoke();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Dying?.Invoke();
        }

        //Di chuyen man hinh
        private void TitleOfWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.StackPanel;
            var win = Window.GetWindow(move);
            win.DragMove();
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            Dying?.Invoke();
            this.Close();
        }       

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var bitmap = new BitmapImage(
                                    new Uri(
                                           "",
                                           UriKind.Relative)
                                    );
            if (youtubeLinkBox.Text != "" || descriptionFoodBox.Text != "" || nameFoodBox.Text != "" || stepFoodBox.Text != "" || ingradientFoodBox.Text != "" || listViewStepFood.Items.IsEmpty == false || foodImageBox.Source != bitmap)
            {
                youtubeLinkBox.Text = "";
                descriptionFoodBox.Text = "";
                nameFoodBox.Text = "";
                stepFoodBox.Text = "";
                ingradientFoodBox.Text = "";
                listImagePath.Clear();
                listImageFullStepFood.Items.Clear();
                listStep.Clear();
                foodImageBox.Source = bitmap;
            }

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var bitmap = new BitmapImage(
                                    new Uri(
                                           "",
                                           UriKind.Relative)
                                    );
            if (youtubeLinkBox.Text != "" && descriptionFoodBox.Text != "" && nameFoodBox.Text != "" && ingradientFoodBox.Text != "" && foodImageBox.Source != bitmap)
            {
                //test.nameFood = nameFoodBox.Text;
                //test.motaFood = descriptionBox.Text;
                //test.youtubeLink = youtubeLinkBox.Text;
                //test.nguyenlieuFood = ingradientBox.Text;
                
                if (listImageFullStepFood.Items.IsEmpty == false && listViewFullStepFood.Items.IsEmpty == false)
                {
                    youtubeLinkBox.Text = "";
                    descriptionFoodBox.Text = "";
                    nameFoodBox.Text = "";
                    ingradientFoodBox.Text = "";
                    foodImageBox.Source = bitmap;
                    stepFoodBox.Text = "";
                    listImagePath.Clear();
                    listImageFullStepFood.Items.Clear();
                    listStep.Clear();
                    MessageBox.Show("Thành công!");
                    Dying?.Invoke();
                    this.Close();
                }
                if (listImageFullStepFood.Items.IsEmpty == true && listViewFullStepFood.Items.IsEmpty==true && youtubeLinkBox.Text != "" && descriptionFoodBox.Text != "" && nameFoodBox.Text != "" && ingradientFoodBox.Text != "" && foodImageBox.Source != bitmap)
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
            if (screen.ShowDialog() == true)
            {
                var filepath = screen.FileName;
                Uri bitmap = new Uri(filepath);
                foodImageBox.Source = new BitmapImage(bitmap);
            }
        }

        //Dem hinh anh moi buoc
        int countImageStep;

        //add image step food
        private void add_image_stepButton_Click(object sender, RoutedEventArgs e)
        {
            countImageStep = 0;
            var screen = new OpenFileDialog();
            screen.Multiselect = true;
            if (screen.ShowDialog() == true)
            {
                foreach (var filepath in screen.FileNames)
                {
                    countImageStep++;
                    listImagePath.Add(new listImage() {listImages= filepath});
                }
                //imageTest.countImageStep = countImageStep;
            }
        }

        //add buoc nau an vao list
        private void add_stepButton_Click(object sender, RoutedEventArgs e)
        {
            if (stepFoodBox.Text != "" && listViewStepFood.Items.IsEmpty == false)
            {
                listStep.Add(new Add_Recipe()
                {
                    numberStep = $"Bước {listStep.Count + 1}",
                    stepMakeFood = stepFoodBox.Text
                });

                foreach (var item in listImagePath)
                {
                    listImageFullStepFood.Items.Add(new BitmapImage(new Uri(item.listImages)));
                }
                stepFoodBox.Text = "";
                listImagePath.Clear();
            }
            else
            {
                MessageBox.Show("Bạn phải thêm đủ thông tin bước và ảnh", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

       
    }
}

