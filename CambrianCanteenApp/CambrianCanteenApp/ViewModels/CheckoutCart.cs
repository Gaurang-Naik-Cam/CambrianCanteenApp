using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambrianCanteenApp.ViewModels
{
    public class CheckoutCart
    {
        private string _foodItemText;
        private float _totalPriceItem;
        //private string _serviceChargeText;
        //private float _serviceCharge;
        //private string _totalText;
        //private string _totalOrderValue;

        public string FoodItemText { get => _foodItemText; set => _foodItemText = value; }
        public float TotalPriceItem { get => _totalPriceItem; set => _totalPriceItem = value; }
        //public string ServiceChargeText { get => _serviceChargeText; set => _serviceChargeText = value; }
        //public float ServiceCharge { get => _serviceCharge; set => _serviceCharge = value; }
        //public string TotalText { get => _totalText; set => _totalText = value; }
        //public string TotalOrderValue { get => _totalOrderValue; set => _totalOrderValue = value; }
    }
}
