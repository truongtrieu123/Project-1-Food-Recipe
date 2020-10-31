using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipe
{
    public class Page: INotifyPropertyChanged
    {
        public int CurrentPage { get; set; } = 1;
        public int MaxPages { get; set; } = 1;
        
        public Page()
        {

        }
        public Page(int maxpages)
        {
            MaxPages = maxpages;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
