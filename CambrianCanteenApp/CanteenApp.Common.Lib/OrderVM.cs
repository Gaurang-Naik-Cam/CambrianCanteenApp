using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanteenApp.Common.Lib
{
    public class OrderVM
    {
        public string orderNumber { get; set; }
        public string status { get; set; }
        public List<FoodQuantityVM> foodQuantities { get; set;}
        public string orderCost { get; set; }

        public OrderVM()
        {
            foodQuantities = new List<FoodQuantityVM>();
        }
    }
}
