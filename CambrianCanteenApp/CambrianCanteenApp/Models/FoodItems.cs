using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambrianCanteenApp.Models
{
    public class FoodItems
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public FoodCategory FoodCategory { get; set; }
        public double Price { get; set; }
        DayOfWeek Day { get; set; }

    }
}
