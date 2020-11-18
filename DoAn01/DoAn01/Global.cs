using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DoAn01
{
    public class Global : INotifyPropertyChanged
    {
        public static List<Food> FoodList { get; set; }
        public static List<Food> FavoriteFoodList { get; set; }
        public static List<Food> SearchResultList { get; set; }
        public static List<BindingList<Food>> HomeSubLists { get; set; }
        public static List<BindingList<Food>> FavorSubLists { get; set; }
        public static List<BindingList<Food>> SearchResultSubLists { get; set; }
        public static int ItemsPerPage { get; set; } = 12;

        private void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        public static void CreateFavoriteList()
        {
            var favoritelist = new List<Food>();

            if (FoodList.Count == 0)
            {
                //Do nothing
            }
            else
            {
                foreach (var food in FoodList)
                {
                    if (food.Favorite.ToString() == "Red")
                    {
                        favoritelist.Add(food);
                    }
                    else
                    {
                        //Do nothing
                    }
                }
            }

            if (FavoriteFoodList != null && FavoriteFoodList.Count != 0)
            {
                FavoriteFoodList.Clear();
            }
            FavoriteFoodList = favoritelist;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemsPerSublist"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<BindingList<Food>> ConvertListToSubLists(int itemsPerSublist, List<Food> list)
        {
            var result = new List<BindingList<Food>>();
            var maxSubLists = list.Count / itemsPerSublist + ((list.Count % itemsPerSublist == 0) ? 0 : 1);

            BindingList<Food> tempSublist;
            var leftitems = list.Count;
            var items = itemsPerSublist;

            for (var count = 0; count < maxSubLists; count++)
            {
                if (leftitems < itemsPerSublist)
                {
                    items = leftitems;
                }

                tempSublist = new BindingList<Food>(list.GetRange(count * itemsPerSublist, items));
                result.Add(tempSublist);

                leftitems -= itemsPerSublist;
            }

            return result;
        }
    }
}
