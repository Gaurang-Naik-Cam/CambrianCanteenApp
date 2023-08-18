using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanteenApp.Common.Lib
{
    public class InputOrder
    {
        public List<FoodQuantityVM> foodItems { get; set; }
        public int studentId { get; set; }
        public double total { get; set; }
        public double tax { get; set; }
        public double serviceCharge { get; set; }

        public InputOrder()
        {
            foodItems = new List<FoodQuantityVM>();
        }

    }

    public class FoodQuantityVM
    {
        public int foodItemId { get; set; }
        public string foodItemName { get; set; }
        public int Qty { get; set; }
    }
}
