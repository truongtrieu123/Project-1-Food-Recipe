using System.ComponentModel;

namespace DoAn01
{
    public class CPage : INotifyPropertyChanged
    {
        private int currentPage;
        public int CurrentPage
        {
            get { return this.currentPage; }
            set
            {
                if (value != this.currentPage)
                {
                    this.currentPage = value;
                    OnPropertyChanged("CurrentPage");
                }
                else
                {
                    return;
                }
            }
        }
        public int maxPages;
        public int MaxPages
        {
            get { return this.maxPages; }
            set
            {
                if (value != this.maxPages)
                {
                    this.maxPages = value;
                    OnPropertyChanged("MaxPages");
                }
                else
                {
                    return;
                }
            }
        }

        public CPage()
        {
            this.currentPage = 1;
            this.maxPages = 1;
        }

        public CPage(int maxpages)
        {
            if (maxpages > 0)
            {
                this.maxPages = maxpages;
            }
            else
            {
                this.maxPages = 1;
            }
            this.currentPage = 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
            else { }
        }
    }
}
