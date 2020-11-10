using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

namespace DoAn01
{
    public class Image : INotifyPropertyChanged
    {
        private StringBuilder imgPath;
        public StringBuilder ImgPath
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
            else { }
        }
    }

    public class Step : INotifyPropertyChanged
    {
        public int StepIndex { get; set; }
        private StringBuilder stepDetail;
        public StringBuilder StepDetail
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
            else { }
        }
        public Step()
        {

        }
        public Step(Step step)
        {
            this.stepDetail = step.StepDetail;
            this.StepImages = StepImages;
        }
    }

    public class Food : INotifyPropertyChanged
    {
        public int DayIndex { get; set; }
        public StringBuilder Name { get; set; }
        public StringBuilder CoverSource { get; set; }
        public StringBuilder VideoSource { get; set; }
        public StringBuilder Introduction { get; set; }
        public StringBuilder Ingredients { get; set; }
        public int CountSteps { get; set; }
        public BindingList<Step> StepList { get; set; }
        public StringBuilder Favorite { get; set; }

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
            return (x.Name.ToString().CompareTo(y.Name.ToString()));
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
