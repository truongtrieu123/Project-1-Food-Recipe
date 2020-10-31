using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn01
{
    public class Global : INotifyPropertyChanged
    {
        public static List<Food> FoodList { get; set; }
        public static List<Food> FavoriteFoodList { get; set; }
        public static List<BindingList<Food>> HomeSubLists { get; set; }
        public static List<BindingList<Food>> FavorSubLists { get; set; }

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

            FavoriteFoodList = favoritelist;
        }

        public static void ConvertListToSubLists(int itemsPerSublist, ref List<BindingList<Food>> sublists, List<Food> list)
        {
            var maxSubLists = list.Count / itemsPerSublist + ((list.Count % itemsPerSublist == 0) ? 0 : 1);
            sublists.Clear(); // Xóa hết để cập nhật lại

            if (sublists.Count == 0)
            {
                var tempSublist = new BindingList<Food>();

                for (var count = 0; count < maxSubLists; count++)
                {
                    tempSublist.Clear();
                    tempSublist = new BindingList<Food>(list.GetRange(count * itemsPerSublist, itemsPerSublist));
                    sublists.Add(tempSublist);
                }
            }
            else if (sublists.Last().Count == itemsPerSublist)
            {

            }
            else
            {

            }
        }
    }
}
