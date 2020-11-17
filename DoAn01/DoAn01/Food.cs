using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
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
        private int stepIndex;
        public int StepIndex
        {
            get { return this.stepIndex; }
            set
            {
                if (value != this.stepIndex)
                {
                    this.stepIndex = value;
                    OnPropertyChanged("StepIndex");
                }
                else
                {
                    return;
                }
            }
        }
        private StringBuilder stepDetail;
        public StringBuilder StepDetail
        {
            get
            {
                return this.stepDetail;
            }
            set
            {
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
        private BindingList<Image> stepImages;
        public BindingList<Image> StepImages
        {
            get { return this.stepImages; }
            set
            {
                if (value != this.stepImages)
                {
                    this.stepImages = new BindingList<Image>(value.ToList<Image>());
                    OnPropertyChanged("StepImages");
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
        public Step()
        {
            stepImages = new BindingList<Image>();
        }
        public Step(Step step)
        {
            this.stepDetail = step.StepDetail;
            this.StepImages = step.stepImages;
            this.StepIndex = step.StepIndex;
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
        private StringBuilder favorite;
        public StringBuilder Favorite
        {
            get
            { return this.favorite; }
            set
            {
                if (value != this.favorite)
                {
                    this.favorite = new StringBuilder(value.ToString());
                    OnPropertyChanged("Favorite");
                }
            }
        }
        public Food()
        {
            this.StepList = new BindingList<Step>();
        }

        public void ClearData()
        {
            this.DayIndex = 0;
            this.Name.Clear();
            this.CoverSource.Clear();
            this.VideoSource.Clear();
            this.Ingredients.Clear();
            this.Introduction.Clear();
            this.CountSteps = 0;
            this.StepList.Clear();
            this.Favorite.Clear();
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
