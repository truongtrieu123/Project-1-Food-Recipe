using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    /// 
    public class Meal
    {
        public string imageMeal { get; set; }
        public string mealName { get; set; }

        public string favorite { get; set; }

    }

    public class MealDAO
    {
        
        public static BindingList<Meal> GetAll()
        {
            BindingList<Meal> _list = new BindingList<Meal>()
            {
                new Meal()
                {
                    imageMeal="./../../Images/food01.jpg",
                    mealName= "Thịt kho trứng",
                    favorite="red",

                },
                new Meal()
                {
                    imageMeal="./../../Images/food01.jpg",
                    mealName= "Thịt kho trứng",
                    favorite="red",

                },
                new Meal()
                {
                    imageMeal="./../../Images/food01.jpg",
                    mealName= "Thịt kho trứng",
                    favorite="red",

                },
                new Meal()
                {
                    imageMeal="./../../Images/food01.jpg",
                    mealName= "Thịt kho trứng",
                    favorite="red",

                },
                new Meal()
                {
                    imageMeal="./../../Images/food01.jpg",
                    mealName= "Thịt kho trứng",
                    favorite="red",

                },
                new Meal()
                {
                    imageMeal="./../../Images/food01.jpg",
                    mealName= "Thịt kho trứng",
                    favorite="red",

                },
                new Meal()
                {
                    imageMeal="./../../Images/food01.jpg",
                    mealName= "Thịt kho trứng",
                    favorite="red",

                },
                new Meal()
                {
                    imageMeal="./../../Images/food01.jpg",
                    mealName= "Thịt kho trứng",
                    favorite="white",

                },
                new Meal()
                {
                    imageMeal="./../Images/food01.jpg",
                    mealName= "Thịt kho trứng",
                    favorite="red",

                },
                new Meal()
                {
                    imageMeal="./../Images/food01.jpg",
                    mealName= "Thịt kho trứng",
                    favorite="red",

                },
            };
            return _list;
        }
    }
    public partial class Home : Page
    {

        #region Attribute 
        private System.Timers.Timer _timer;


        BindingList<Meal> _list;
        #endregion

        public Home()
        {
            InitializeComponent();
            
        }


        public void CreateHomePage()
        {
            _list=MealDAO.GetAll();
            mealListView.ItemsSource = _list;
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;

            MessageBox.Show($"{folder}");
            CreateHomePage();
            
        }

    }
}
