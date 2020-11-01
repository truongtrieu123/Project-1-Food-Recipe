using System.ComponentModel;

namespace FoodRecipe
{
    public class CPage : INotifyPropertyChanged
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
            if (maxpages > 0)
            {
                MaxPages = maxpages;
            }
            else
            {
                MaxPages = 1;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
