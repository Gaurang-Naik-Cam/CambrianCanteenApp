using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanteenApp.Common.Lib
{
    public class FoodItems
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public string FoodCategoryName { get; set; }
        public double Price { get; set; }
        DayOfWeek Day { get; set; }

    }
}
