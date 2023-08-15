using CanteenApp.Common.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CambrianCanteenApp.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        //public ICommand AddToCart { get; set; }
        //public List<FoodItemVM> oldMenuCard = new List<FoodItemVM>();
        public ObservableCollection<FoodItemVM> MenuCard { get; private set; } = new ObservableCollection<FoodItemVM>();
        HttpClient _client;

        public HomePageViewModel()
        {
            //oldMenuCard = GetMenuCard().Result;
            GetMenuCard();
            //AddToCart = new Command(AddToCartClick);
        }


        [RelayCommand]
        public void AddToCart(object obj)
        {
            App.Current.MainPage.DisplayAlert("Hello", "Product ID is ", "Ok");
        }

        private async void GetMenuCard()
        {
            Uri uri = new Uri(string.Format(Constants.API_URI + "{0}", "Menu"));
            //List<FoodItemVM> menuCardList = new List<FoodItemVM>();
            _client = new HttpClient();
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
                        var resultData = JsonConvert.DeserializeObject<List<FoodItemVM>>(result.Data.ToString(), new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        foreach (var item in resultData)
                        {
                            MenuCard.Add(item);
                        }

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

