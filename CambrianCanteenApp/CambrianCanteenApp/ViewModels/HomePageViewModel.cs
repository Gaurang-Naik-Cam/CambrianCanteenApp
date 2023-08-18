using CanteenApp.Common.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
//using ABI.System;
using System.Text;

namespace CambrianCanteenApp.ViewModels
{
    public partial class HomePageViewModel //: ObservableObject
    {
        //public ICommand AddToCart { get; set; }
        private List<FoodItemVM> _menuCard;
        public List<FoodItemVM> MenuCard
        {
            get { return _menuCard; }
            
        }
        //public ObservableCollection<FoodItemVM> MenuCard { get; private set; } = new ObservableCollection<FoodItemVM>();
        HttpClient _client;
       

        public HomePageViewModel()
        {

            //oldMenuCard = GetMenuCard().Result;
            _client = new HttpClient();
            GetMenuCard();
            //DataContext = this;
            //AddToCart = new Command(AddToCartClick);
        }


        [RelayCommand]    
        public async void AddToCart(object obj)
        {
            if(obj !=null)
            {
                int itemId = 0; 
                if(int.TryParse(obj.ToString(), out itemId))
                {
                    var studentVM = Constants.GetLoggedInUserInfo();
                    if (studentVM == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Logged in user info", "Unable to retrieve logged in user details. Request you to re-login.", "Ok");

                        if (Preferences.ContainsKey(nameof(App.IsLoggedIn)))
                        {
                            Preferences.Remove(Constants.LoggedInUser);
                        }

                        //Navigation.PushAsync(new LoginPage());
                        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    }
                    else
                    {
                        InputCart inputCart = new InputCart();
                        inputCart.foodItemId = itemId;
                        inputCart.fullEmptyCart = false;
                        inputCart.studentId = studentVM.ID;
                        string json = JsonConvert.SerializeObject(inputCart); //JsonSerializer. Serialize<AppCredentials>(new AppCredentials() { UserName = userName, 
                                                                              //Password = pwd}, _serializerOptions);

                        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                        Uri uri = new Uri(string.Format(Constants.API_URI + "{0}", "Cart/AddToCart"));

                        var response = await _client.PostAsync(uri, content);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseContent = await response.Content.ReadAsStringAsync();

                            var result = JsonConvert.DeserializeObject<Result>(responseContent, new JsonSerializerSettings()
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

                            if(result.IsSuccess)
                            {
                                if (Preferences.ContainsKey(Constants.Cart))
                                    Preferences.Remove(Constants.Cart);

                                Preferences.Set(Constants.Cart, result.Data.ToString());
                                var carts = JsonConvert.DeserializeObject<List<CartVM>>(result.Data.ToString());
                                string itemName = carts.Where(c => c.foodItemId == itemId).First().foodItemName;

                                await App.Current.MainPage.DisplayAlert("Cart is updated", string.Format("{0} is added to the Cart.", itemName), "Ok");
                            }
                        }
                    }
                }
            }
           
        }

        private async void GetMenuCard()
        {
            _menuCard = new List<FoodItemVM>();
            Uri uri = new Uri(string.Format(Constants.API_URI + "{0}", "Menu"));
            //List<FoodItemVM> menuCardList = new List<FoodItemVM>();
           
            try
            {
                HttpResponseMessage response = null;
                response = _client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Result>(responseContent, new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    if (result.IsSuccess)
                    {
                        _menuCard = JsonConvert.DeserializeObject<List<FoodItemVM>>(result.Data.ToString(), new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        //foreach (var item in resultData)
                        //{
                        //    //MenuCard.Add(item);
                        //    _menuCard.Add
                        //}

                    }
                }
            }
            catch (Exception ex)
            {

            }

            //return menuCardList;
        }
    }
}

