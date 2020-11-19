using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;
using System.Drawing;
using System.Text.RegularExpressions;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string src = "Lắc phô mai mắm tôm";

            MessageBox.Show(ConvertToUnsignString(src));

            this.Close();
        }
        static Regex ConvertToUnsign_rg = null;

        public static string ConvertToUnsign(string strInput)
        {
            if (ReferenceEquals(ConvertToUnsign_rg, null))
            {
                ConvertToUnsign_rg = new Regex("p{IsCombiningDiacriticalMarks}+");
            }
            var temp = strInput.Normalize(NormalizationForm.FormD);
            return ConvertToUnsign_rg.Replace(temp, string.Empty).Replace("đ", "d").Replace("Đ", "D").ToLower();
        }

        private string ConvertToUnsignString(string src)
        {
            var result = src;
            var oldchars = "áàãảạăắằẳẵặâấầẩẫậđéèẻẽẹêếềểễệíìỉĩịóòõỏọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵ";
            var newchars = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyy";
            var len1 = oldchars.Length;
            var len2 = newchars.Length;
            
            for (var pos = 0; pos < len1; pos++)
            {
                result = result.Replace(oldchars[pos], newchars[pos]);
            }

            return result;
        }

    }
}
