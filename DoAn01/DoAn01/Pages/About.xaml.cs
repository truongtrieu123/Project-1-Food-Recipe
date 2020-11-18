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
    /// Interaction logic for About.xaml
    /// </summary>
    /// 
    public class MemberInfo
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Gmail { get; set; }
        public string Git { get; set; }
        public string Phone { get; set; }
        public string ImagePath { get; set; }

    }
    public partial class About : Page
    {

        private List<MemberInfo> _list = new List<MemberInfo>()
        {
            new MemberInfo(){Name="Trương Đại Triều",ID="18120096", Gmail= "truongdaitrieu2109@gmail.com",
                Git="truongtrieu123",Phone="0906947217", ImagePath="Data\\Images\\AnhDaiDien\\TrieuNe.jpg"},
            new MemberInfo(){Name="Nguyễn Hoàng Khang",ID="18120039", Gmail= "n.hoangkhang23122015@gmail.com",
                Git="n.hoangkhang23122015@gmail.com",Phone="0906947217", ImagePath="Data\\Images\\AnhDaiDien\\KhangNe.jpg"},
            new MemberInfo(){Name="Bùi Huỳnh Trung Tín",ID="18120092", Gmail= "bhtt190800@gmail.com",
                Git="bhtt190800@gmail.com",Phone="0906947217", ImagePath="Data\\Images\\AnhDaiDien\\TinNe.jpg"},
        };

        public About()
        {

            InitializeComponent();
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            string absolutePath = $"{folder}{_list[1].ImagePath}";
            Console.WriteLine(_list[1].ImagePath);
            Console.WriteLine(absolutePath);
            InfoList.ItemsSource = _list;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
