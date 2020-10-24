using System;
using System.Collections.Generic;
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
    public partial class DetailMeal : Page
    {
        public delegate void DeathHandler();
        public event DeathHandler Dying;

        public DetailMeal()
        {
            InitializeComponent();
        }


        private void DetailMeal_Loaded(object sender, RoutedEventArgs e)
        {
            //web.Source=new Uri("https://www.google.com/");
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            Dying?.Invoke();
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {           
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}


