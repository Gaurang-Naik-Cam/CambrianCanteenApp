using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanteenApp.Common.Lib
{
    public class InputCart
    {
        public int studentId { get; set; }
        public int foodItemId { get; set; }
        public bool fullEmptyCart { get; set; }

        public InputCart()
        {
            this.fullEmptyCart = false;
        }
    }
}
