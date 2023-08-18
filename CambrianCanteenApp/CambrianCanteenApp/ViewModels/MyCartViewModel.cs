using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CanteenApp.Common.Lib;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace CambrianCanteenApp.ViewModels
{
    public partial class MyCartViewModel : ObservableObject
    {
        //[ObservableProperty]
        private List<CartVM> _myCart;
        private HttpClient _client;

        [ObservableProperty]
        ObservableCollection<CartVM> currentCart;

        //[ObservableProperty]
        //public CartVM cartVM;


        //[ObservableProperty]
        //public List<CartVM> MyCart { get { return _myCart; } }

        public MyCartViewModel()
        {
            _myCart = new List<CartVM>();
            _client = new HttpClient();
            currentCart = new ObservableCollection<CartVM>();
            //cartVM = new CartVM();
            GetCurrentCart();
        }

        public void GetCurrentCart()
        {
            if (Preferences.ContainsKey(Constants.Cart))
            {
                string jsonCart = Preferences.Get(Constants.Cart, string.Empty);
                _myCart = JsonConvert.DeserializeObject<List<CartVM>>(jsonCart);
                foreach (var item in _myCart)
                {
                    //CartVM = item;
                    CurrentCart.Add(item);
                } //JsonConvert.DeserializeObject<List<CartVM>>(jsonCart);
            }
        }

        //[RelayCommand]
        //public async void Checkout()
        //{
        //    var checkOutPage = AppShell.Current.Items.Where(f => f.Title == "CheckOut").FirstOrDefault();
        //    if (loginPage == null)
        //    {
        //        AppShell.Current.Items.Add(new ShellContent()
        //        {
        //            Title = "Login",
        //            ContentTemplate = new DataTemplate(typeof(LoginPage)),
        //            Route = "LoginPage"
        //        });
        //    }

        //    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");

        //}

        [RelayCommand]
        public async void DecreaseQty(object obj)
        {
            int _foodId = int.Parse(obj.ToString());
            //var data = CurrentCart.Where(i => i.foodItemId == _foodId).First().qty-= 1;
            InputCart inputCart = new InputCart();
            inputCart.studentId = CurrentCart[0].studentId;
            inputCart.foodItemId = _foodId;
            inputCart.fullEmptyCart = false;

            string json = JsonConvert.SerializeObject(inputCart);

            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(string.Format(Constants.API_URI + "{0}", "Cart/DeleteCart"));

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
                    var jsonCart = JsonConvert.DeserializeObject<List<CartVM>>(result.Data.ToString());

                    if (Preferences.ContainsKey(Constants.Cart))
                        Preferences.Remove(Constants.Cart);

                    Preferences.Set(Constants.Cart, result.Data.ToString());
                    CurrentCart.Clear();
                    GetCurrentCart();
                    await App.Current.MainPage.DisplayAlert("Your Cart", result.Message, "Ok");
                }
            }

            
        }

        [RelayCommand]
        public async void IncreaseQty(object obj)
        {
            int _foodId = int.Parse(obj.ToString());
            //var data = CurrentCart.Where(i => i.foodItemId == _foodId).First().qty-= 1;
            InputCart inputCart = new InputCart();
            inputCart.studentId = CurrentCart[0].studentId;
            inputCart.foodItemId = _foodId;
            inputCart.fullEmptyCart = false;

            string json = JsonConvert.SerializeObject(inputCart);

            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(string.Format(Constants.API_URI + "{0}", "Cart/AddToCart"));

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
                    var jsonCart = JsonConvert.DeserializeObject<List<CartVM>>(result.Data.ToString());

                    if (Preferences.ContainsKey(Constants.Cart))
                        Preferences.Remove(Constants.Cart);

                    Preferences.Set(Constants.Cart, result.Data.ToString());
                    CurrentCart.Clear();
                    GetCurrentCart();
                    await App.Current.MainPage.DisplayAlert("Your Cart", result.Message, "Ok");
                }
            }
        }
    }
}
