using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanteenApp.Common.Lib
{
    public class CartVM
    {
        public int foodItemId { get; set; }
        public string foodItemName { get; set; }
        public string imageURL { get; set; }
        public string price { get; set; }
        public int qty { get; set; }
        public int studentId { get; set; }
    }
}
