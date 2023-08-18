using CanteenApp.Common.Lib;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambrianCanteenApp.ViewModels
{
    public partial class CheckoutViewModel
    {
        private List<CheckoutCart> _checkoutCart;
        
        HttpClient _client;
        List<CartVM> _myCart;
        public List<CheckoutCart> CheckoutCartList { get => _checkoutCart; set => _checkoutCart = value; }

        public float TotalOrderValue { get; set; }
        public float ServiceCharge { get; set; }
        public string ServiceChargeText = "Convenience Charge";
        public string TotalOrderValueText = "Total Payable Amount";
        public string TaxText = "Tax";
        public float TaxAmount { get; set; }
        public string SubTotalText { get; set; }
        public float SubTotal { get; set; }


        public CheckoutViewModel()
        {
            SubTotal = 0;
            _client = new HttpClient();
            _checkoutCart = new List<CheckoutCart>();
            _myCart = new List<CartVM>();
            GetCheckOutCart();
        }

        private void GetCheckOutCart()
        {
            if (Preferences.ContainsKey(Constants.Cart))
            {
                string jsonCart = Preferences.Get(Constants.Cart, string.Empty);
                _myCart = JsonConvert.DeserializeObject<List<CartVM>>(jsonCart);
                foreach (var item in _myCart)
                {
                    CheckoutCart checkoutCart = new CheckoutCart();
                    checkoutCart.FoodItemText = string.Format("{0} X {1} ", item.foodItemName, item.qty);
                    checkoutCart.TotalPriceItem = item.totalItemPrice;
                    SubTotal += item.totalItemPrice;
                    CheckoutCartList.Add(checkoutCart);
                }

                TaxAmount = (SubTotal * Constants.Tax) / 100;
                ServiceCharge = (SubTotal * Constants.ServiceCharge) / 100;
                TotalOrderValue = SubTotal + TaxAmount + ServiceCharge;
            }
        }

        [RelayCommand]
        private async void PlaceOrder()
        {
            if (Preferences.ContainsKey(Constants.Cart))
            {
                string jsonCart = Preferences.Get(Constants.Cart, string.Empty);
                _myCart = JsonConvert.DeserializeObject<List<CartVM>>(jsonCart);
                InputOrder inputOrder = new InputOrder();
                inputOrder.tax = TaxAmount;
                inputOrder.total = TotalOrderValue;
                inputOrder.studentId = _myCart.ElementAt(0).studentId;
                foreach (var item in _myCart)
                {
                    inputOrder.foodItems.Add(new FoodQuantityVM() { foodItemId = item.foodItemId, foodItemName = item.foodItemName, Qty = item.qty });
                }

                string json = JsonConvert.SerializeObject(inputOrder);

                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                Uri uri = new Uri(string.Format(Constants.API_URI + "{0}", "Order/PlaceOrder"));

                var response = await _client.PostAsync(uri, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Result>(responseContent, new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    if (result.IsSuccess)
                    {
                        
                        //Emptying in-memory cart
                        if (Preferences.ContainsKey(Constants.Cart))
                            Preferences.Remove(Constants.Cart);
                        
                        await App.Current.MainPage.DisplayAlert("Order Placed Successfully", 
                            string.Format("Order number is {0}. You may track your order in My Orders.",result.Data.ToString()), "Ok");
                    }
                }
            }
        }
    }
}
