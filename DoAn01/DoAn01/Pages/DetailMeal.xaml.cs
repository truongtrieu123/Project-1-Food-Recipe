using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DoAn01.Pages
{
    /// <summary>
    /// Interaction logic for DetailMeal.xaml
    /// </summary>
    public partial class DetailMeal : Page, INotifyCollectionChanged
    {
        public delegate void DyingHandler();
        public event DyingHandler Dying;

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
            InitializeComponent();
            Info = p;
            CurrentStep = 1;
            TotalStep = Info.StepList.Count;
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
            guides.Text = Info.StepList[index].StepDetail;
            // Remove and clear source 
            HinhAnhTungStep.ItemsSource = null;
            HinhAnhTungStep.Items.Clear();
            // The list<> has been updated so reload the listview
            //HinhAnhTungStep.ItemsSource = CreateListImagePath(Info.StepList[index].StepImages);

        }

        public List<ImagePath> CreateListImagePath(List<string> stepImage)
        {
            List<ImagePath> _list = new List<ImagePath>();

            foreach (var img in stepImage)
            {
                ImagePath temp = new ImagePath();

                temp.path = img;
                _list.Add(temp);
            }

            return _list;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentStep <= 1)
            {
                CurrentStep = Info.StepList.Count;
                InitializeStepDetailInfo();
            }
            else if (CurrentStep > 1 && CurrentStep <= Info.StepList.Count)
            {
                CurrentStep--;
                InitializeStepDetailInfo();
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentStep >= Info.StepList.Count)
            {
                CurrentStep = 1;
                InitializeStepDetailInfo();
            }
            else if (CurrentStep < Info.StepList.Count && CurrentStep >= 1)
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

    }
}


