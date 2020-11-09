using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace DoAn01
{
    public class Image : INotifyPropertyChanged
    {
        private string imgPath;
        public string ImgPath
        {
            get { return this.imgPath; }
            set
            {
                if (value != this.imgPath)
                {
                    this.imgPath = value;
                    OnPropertyChanged("ImgPath");
                }
                else
                {
                    return;
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public class Step : INotifyPropertyChanged
    {
        private string stepDetail;

        public string StepDetail
        {
            get
            {
                return this.stepDetail;
            }
            set {
                if (value != this.stepDetail)
                {
                    this.stepDetail = value;
                    OnPropertyChanged("StepDetail");
                }
                else
                {
                    return;
                }
            }
        }
        public BindingList<Image> StepImages { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public class Food : INotifyPropertyChanged
    {
        public int DayIndex { get; set; }
        public string Name { get; set; }
        public string CoverSource { get; set; }
        public string VideoSource { get; set; }
        public string Introduction { get; set; }
        public string Ingredients { get; set; }
        public int CountSteps { get; set; }
        public BindingList<Step> StepList { get; set; }
        public string Favorite { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public class FoodNameComparer : IComparer<Food>
    {
        public int Compare(Food x, Food y)
        {
            return (x.Name.CompareTo(y.Name));
        }
    }

    public class FoodDayIndexComparer : IComparer<Food>
    {
        public int Compare(Food x, Food y)
        {
            return (x.DayIndex.CompareTo(y.DayIndex));
        }
    }


}
