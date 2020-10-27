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
        //
        //addRecipeTest test;
        //stepTest imageTest;

        //struct stepTest
        //{
        //    public int countStep;
        //    public string infoStep;
        //    public int countImageStep;
        //    public List<string> pathImage;
        //}
        //List<stepTest> tmpStep =new List<stepTest>();
        //struct addRecipeTest {
        //    public string nameFood;
        //    public string motaFood;
        //    public string nguyenlieuFood;
        //    public string youtubeLink;
        //    public string imagePath;
        //    public List<stepTest> stepFood;
        //};
       
        //
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

        public delegate void DeathHandler();
        public event DeathHandler Dying;

        StreamWriter writer;
        public AddRecipe()
        {
            InitializeComponent();
            var filepath = "..//..//test.txt";
            writer = File.AppendText(filepath);
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            Dying?.Invoke();
            this.Close();
        }

        private void outButton_Click(object sender, RoutedEventArgs e)
        {
            Dying?.Invoke();
            this.Close();
        }

        //Di chuyen man hinh
        private void TitleOfWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.StackPanel;
            var win = Window.GetWindow(move);
            win.DragMove();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Dying?.Invoke();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var bitmap = new BitmapImage(
                                    new Uri(
                                           "",
                                           UriKind.Relative)
                                    );
            if (youtubeLinkBox.Text != "" || descriptionBox.Text != "" || nameFoodBox.Text != "" || stepFoodBox.Text != "" || ingradientBox.Text != "" || listView.Items.IsEmpty == false || foodImage.Source != bitmap)
            {
                youtubeLinkBox.Text = "";
                descriptionBox.Text = "";
                nameFoodBox.Text = "";
                stepFoodBox.Text = "";
                ingradientBox.Text = "";
                listImagePath.Clear();
                listImageBox.Items.Clear();
                listStep.Clear();
                foodImage.Source = bitmap;
            }

        }

        int countIngradient;
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            countIngradient = 0;
            var bitmap = new BitmapImage(
                                    new Uri(
                                           "",
                                           UriKind.Relative)
                                    );
            if (youtubeLinkBox.Text != "" && descriptionBox.Text != "" && nameFoodBox.Text != "" && ingradientBox.Text != "" && foodImage.Source != bitmap)
            {
                //test.nameFood = nameFoodBox.Text;
                //test.motaFood = descriptionBox.Text;
                //test.youtubeLink = youtubeLinkBox.Text;
                //test.nguyenlieuFood = ingradientBox.Text;
                    
                //int strt = 0;
                //int cnt = -1;
                //int idx = -1;
                //while (strt != -1)
                //{
                //    strt = test.nguyenlieuFood.IndexOf("\n", idx + 1);
                //    cnt += 1;
                //    idx = strt;
                //}

                //countIngradient = cnt + 1;

                //test.imagePath = foodImage.Source.ToString();

                ////test.stepFood.Add(listStep);
             
                //writer.WriteLine(test.nameFood);
                //writer.WriteLine(test.youtubeLink);
                //writer.WriteLine(test.motaFood);
                //writer.WriteLine(countIngradient);
                //writer.WriteLine(test.nguyenlieuFood);
                //writer.WriteLine(test.imagePath);  
                //foreach(var item in tmpStep)
                //{                    
                //    writer.WriteLine(item.countStep);
                //    writer.WriteLine(item.infoStep);
                //    writer.WriteLine(item.countImageStep);
                //}
                

                //writer.WriteLine(tmp);
                if (listImageBox.Items.IsEmpty == false && listViewBox.Items.IsEmpty==false)
                {
                    youtubeLinkBox.Text = "";
                    descriptionBox.Text = "";
                    nameFoodBox.Text = "";
                    ingradientBox.Text = "";
                    foodImage.Source = bitmap;
                    stepFoodBox.Text = "";
                    listImagePath.Clear();
                    listImageBox.Items.Clear();
                    listViewBox.Items.Clear();
                    MessageBox.Show("Thành công!");
                }
                if (listImageBox.Items.IsEmpty == true && listViewBox.Items.IsEmpty==true && youtubeLinkBox.Text != "" && descriptionBox.Text != "" && nameFoodBox.Text != "" && ingradientBox.Text != "" && foodImage.Source != bitmap)
                {
                    MessageBox.Show("Bạn phải thêm bước nấu ăn (bao gồm thông tin, hình ảnh và thêm vào danh sách)!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                //stepFoodBox.Text = "";                
                //listImagePath.Clear();
                //listImageBox.Items.Clear();               
            }
            else
            {            
                MessageBox.Show("Bạn phải thêm đủ tất cả thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);              
            }
        }
        string tmp = ""; 
        private void add_image_foodButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            if (screen.ShowDialog() == true)
            {
                var filepath = screen.FileName;
                //var file = new FileInfo(filepath);
                //file.CopyTo(@"D:/Desktop/image01.jpg");
                Uri bitmap = new Uri(filepath);
                foodImage.Source = new BitmapImage(bitmap);
            }
        }

        int countImageStep;
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
                    //listView.Items.Add(new BitmapImage(new Uri(filepath)));
                    listImagePath.Add(new listImage() {listImages= filepath});
                }
                //imageTest.countImageStep = countImageStep;
            }
        }

        private void add_stepButton_Click(object sender, RoutedEventArgs e)
        {
            if (stepFoodBox.Text != "" && listView.Items.IsEmpty == false)
            {
                listStep.Add(new Add_Recipe()
                {
                    numberStep = $"Bước {listStep.Count + 1}",
                    stepMakeFood = stepFoodBox.Text
                });

                foreach (var item in listImagePath)
                {
                    listImageBox.Items.Add(new BitmapImage(new Uri(item.listImages)));
                }

                //imageTest.countStep = (listStep.Count);
                //imageTest.infoStep = stepFoodBox.Text;
                //tmpStep.Add(imageTest);

                stepFoodBox.Text = "";
                listImagePath.Clear();
            }
            else
            {
                MessageBox.Show("Bạn phải thêm đủ thông tin bước và ảnh", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listStep = stepDao.GetAll();
            listViewBox.ItemsSource = listStep;

            listImagePath = stepImageDao.GetAll();
            listView.ItemsSource = listImagePath;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            writer.Close();
        }
    }
}

