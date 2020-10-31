using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn01
{
    public interface FoodDAO
    {
        List<Food> GetAll();
    }

    public class TextFooDAO : FoodDAO
    {
        public List<Food> GetAll()
        {
            return new List<Food>();
        }
    }

    public class ExcelFoodDAO : FoodDAO
    {
        const string Space = " ";

        public List<Food> GetAll()
        {
            var result = new List<Food>();
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}FoodList.xlsx";
            var workbook = new Workbook(database);
            var sheet = workbook.Worksheets[0];
            var row = 2;
            var col = 'H';
            var steplist = new List<Step>();
            var temp_step = new Step();
            temp_step.StepImages = new List<String>();
            var cell = sheet.Cells[$"B{row}"];

            while (cell.Value != null)
            {
                var dayindex = sheet.Cells[$"A{row}"].StringValue;
                var name = cell.StringValue;
                var videosorce = sheet.Cells[$"C{row}"].StringValue;
                var favorite = sheet.Cells[$"D{row}"].StringValue;
                var introduction = sheet.Cells[$"E{row}"].StringValue;
                var ingredients = sheet.Cells[$"F{row}"].StringValue;
                var coversource = $"Data\\Images\\Food\\{dayindex}\\ava.jpg";

                var countsteps_countimages = sheet.Cells[$"G{row}"].StringValue;
                var tokens = countsteps_countimages.Split(new string[] { Space }, StringSplitOptions.RemoveEmptyEntries);

                var countsteps = int.Parse(tokens[0]);
                var countimages = 0;
                var path = "";

                for (var pos = 0; pos < countsteps; pos++)
                {
                    temp_step.StepDetail = sheet.Cells[$"{char.ConvertFromUtf32(col + pos)}{row}"].StringValue;
                    temp_step.StepImages.Clear();

                    countimages = int.Parse(tokens[pos + 1]);

                    for (var pos1 = 0; pos1 < countimages; pos1++)
                    {
                        path = $"Data\\Images\\Food\\{dayindex}\\step{pos + 1} ({pos1 + 1}).jpg";
                        temp_step.StepImages.Add(path);
                    }

                    steplist.Add(temp_step);
                }

                var food = new Food()
                {
                    Name = name,
                    DayIndex = dayindex,
                    VideoSource = videosorce,
                    CoverSource = coversource,
                    Introduction = introduction,
                    Ingredients = ingredients,
                    CountSteps = countsteps,
                    Favorite = favorite,
                    StepList = steplist
                };
                result.Add(food);

                row = row + 1;
                cell = sheet.Cells[$"B{row}"];
            }
            return result;
        }
    }
}
