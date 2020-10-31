using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            var page = new Home();
            Main.NavigationService.Navigate(page);
        }

        private void favorButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Favorite();
        }

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new About();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Search();
        }

        private void outButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void newRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new AddRecipe();
            screen.Dying += DyingHadle;
            screen.Show();
            this.Hide();
        }

        private void DyingHadle()
        {
            this.Show();
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Home();
        }

        //Di chuyen man hinh
        private void TitleOfWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.StackPanel;
            var win = Window.GetWindow(move);
            win.DragMove();
        }

        private void ExcelExportData()
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}FoodList.xlsx";
            var workbook = new Workbook(database);
            var sheet = workbook.Worksheets[0];
            sheet.AutoFitColumns();
            sheet.AutoFitRows();
            var row = 1;
            var col = 7;
            var countsteps = new int[] { };

            foreach (var value in Global.FoodList)
            {
                sheet.Cells[row, 0].Value = value.DayIndex;
                sheet.Cells[row, 1].Value = value.Name;
                sheet.Cells[row, 2].Value = value.VideoSource;
                sheet.Cells[row, 3].Value = value.Favorite.ToString();
                sheet.Cells[row, 4].Value = value.Introduction;
                sheet.Cells[row, 5].Value = value.Ingredients;
                col = 7;
                Array.Clear(countsteps, 0, countsteps.Length);
                countsteps.Append(value.CountSteps);

                foreach (var step in value.StepList)
                {
                    sheet.Cells[row, col].Value = step.StepDetail;
                    col++;
                    countsteps.Append(step.StepImages.Count);
                }

                sheet.Cells[row, col].Value = String.Join(" ", countsteps);
                row++;
            }

            workbook.Save($"{folder}data.xlsx");
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ExcelExportData();
        }
    }
}
