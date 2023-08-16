using CanteenApp.Common.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
            
            GetMenuCard();
            //DataContext = this;
            //AddToCart = new Command(AddToCartClick);
        }


        [RelayCommand]    
        public void AddToCart(object obj)
        {
            App.Current.MainPage.DisplayAlert("Cart is updated", string.Format("Product {0} is added to the Cart.",obj.ToString()), "Ok");
        }

        private async void GetMenuCard()
        {
            _menuCard = new List<FoodItemVM>();
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

