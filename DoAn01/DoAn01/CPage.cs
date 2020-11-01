using System.ComponentModel;

namespace FoodRecipe
{
    public class CPage: INotifyPropertyChanged
    {
        public int CurrentPage { get; set; }
        public int MaxPages { get; set; }
        
        public CPage()
        {
            CurrentPage = 1;
            MaxPages = 1;
        }
        public CPage(int maxpages)
        {
            MaxPages = maxpages;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
