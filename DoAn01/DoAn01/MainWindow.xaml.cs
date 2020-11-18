using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DoAn01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Global.HomeSubLists = Global.ConvertListToSubLists(Global.ItemsPerPage, Global.FoodList);
            Global.FavorSubLists = Global.ConvertListToSubLists(Global.ItemsPerPage, Global.FavoriteFoodList);

            var page = new Home();
            Main.NavigationService.Navigate(page);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void favorButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Favorite();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new About();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void outButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new AddRecipe();
            screen.Dying += DyingHadle;
            screen.Show();
            this.Hide();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DyingHadle()
        {
            this.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Home();
        }

        /// <summary>
        /// Hàm di chuyển màn hình
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
        /// Hàm xuất list food ra file excel
        /// </summary>
        private void ExcelExportData()
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}Data\\DataBase\\FoodList.xlsx";
            var workbook = new Workbook(database);
            var sheet = workbook.Worksheets[0];
            sheet.AutoFitColumns();
            sheet.AutoFitRows();
            var row = 1;
            var col = 7;
            var countsteps = new List<int>();

            foreach (var value in Global.FoodList)
            {
                sheet.Cells[row, 0].Value = value.DayIndex;
                sheet.Cells[row, 1].Value = value.Name.ToString();
                sheet.Cells[row, 2].Value = value.VideoSource.ToString();
                sheet.Cells[row, 3].Value = value.Favorite.ToString();
                sheet.Cells[row, 4].Value = value.Introduction.ToString();
                sheet.Cells[row, 5].Value = value.Ingredients.ToString();
                col = 7;
                countsteps.Clear();
                countsteps.Add(value.CountSteps);

                foreach (var step in value.StepList)
                {
                    sheet.Cells[row, col].Value = step.StepDetail.ToString();
                    col++;
                    countsteps.Add(step.StepImages.Count);
                }

                sheet.Cells[row, 6].Value = String.Join(" ", countsteps);
                row++;
            }

            workbook.Save($"{folder}Data\\DataBase\\FoodList.xlsx");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            ExcelExportData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
