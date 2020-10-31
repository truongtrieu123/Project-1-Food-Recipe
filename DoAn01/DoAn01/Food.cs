using System.Collections.Generic;
using System.Collections.Specialized;

namespace DoAn01
{
    public class Step: INotifyCollectionChanged
    {
        public string StepDetail { get; set; }
        public List<string> StepImages { get; set; }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }

    public class Food: INotifyCollectionChanged
    {
        public string Index { get; set; }
        public string Name { get; set; }
        public string CoverSource { get; set; }
        public string VideoSource { get; set; }
        public string Introduction { get; set; }
        public string Ingredients { get; set; }
        public int CountSteps { get; set; }
        public List<Step> StepList { get; set; }
        public string Favorite { get; set; }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }

    public class FoodNameComparer : IComparer<Food>
    {
        public int Compare(Food x, Food y)
        {
            return (x.Name.CompareTo(y.Name));
        }
    }


}
