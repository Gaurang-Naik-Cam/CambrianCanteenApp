using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambrianCanteenApp.Models
{
    public class Orders
    {
        double _tax = 0;
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public double Amount { get; set; }
        public List<FoodItems> FoodItems { get; set; }
        public double Tax { get; }
        public Student OrderedBy { get; set; }
        public DateTime OrderedOn { get; set; }
        public DateTime DeliveredOn { get; set; }

    }
}
