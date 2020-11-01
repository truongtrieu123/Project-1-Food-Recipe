using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DoAn01
{
    public class Global : INotifyPropertyChanged
    {
        public static List<Food> FoodList { get; set; }
        public static List<Food> FavoriteFoodList { get; set; }
        public static List<BindingList<Food>> HomeSubLists { get; set; }
        public static List<BindingList<Food>> FavorSubLists { get; set; }
        public static int ItemsPerPage { get; set; } = 12;

        public event PropertyChangedEventHandler PropertyChanged;

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
                    if (food.Favorite == "Red")
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
