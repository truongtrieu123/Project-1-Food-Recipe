using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DoAn01.Pages
{
    /// <summary>
    /// Interaction logic for DetailMeal.xaml
    /// </summary>
    public partial class DetailMeal : Page, INotifyCollectionChanged
    {
        public class ImagePath
        {
            public string path { get; set; }
        }

        public int TotalImages { get; set; }
        public Food Info { get; set; }
        public int CurrentStep { get; set; }
        public int TotalStep { get; set; }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public DetailMeal()
        {
            InitializeComponent();
        }

        public DetailMeal(Food p)
        {
            Info = p;
            CurrentStep = 1;
            TotalStep = Info.Recipe.Count;
            InitializeComponent();
        }

        private void DetailMeal_Loaded(object sender, RoutedEventArgs e)
        {
            CreateMealDetailPage();
        }

        public void CreateMealDetailPage()
        {
            TenMonAn.Text = Info.Name;
            NoiDungMonAn.Text = Info.Introduction;
            NguyenLieu.Text = Info.Ingredients;
            InitializeStepDetailInfo();
        }

        private void InitializeStepDetailInfo()
        {
            int index = CurrentStep - 1;

            step.Text = $"{CurrentStep}";
            guides.Text = Info.Recipe[index].StepDetail;
            // Remove and clear source 
            HinhAnhTungStep.ItemsSource = null;
            HinhAnhTungStep.Items.Clear();
            // The list<> has been updated so reload the listview
            HinhAnhTungStep.ItemsSource = CreateListImagePath(Info.Recipe[index].StepImage);

        }

        public List<ImagePath> CreateListImagePath(List<string> StepImage)
        {
            List<ImagePath> _list = new List<ImagePath>();

            foreach (var i in StepImage)
            {
                ImagePath temp = new ImagePath();

                temp.path = i;
                _list.Add(temp);
            }

            return _list;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentStep <= 1)
            {
                CurrentStep = Info.Recipe.Count;
                InitializeStepDetailInfo();
            }
            else if (CurrentStep > 1 && CurrentStep <= Info.Recipe.Count)
            {
                CurrentStep--;
                InitializeStepDetailInfo();
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentStep >= Info.Recipe.Count)
            {
                CurrentStep = 1;
                InitializeStepDetailInfo();
            }
            else if (CurrentStep < Info.Recipe.Count && CurrentStep >= 1)
            {
                CurrentStep++;
                InitializeStepDetailInfo();
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}


