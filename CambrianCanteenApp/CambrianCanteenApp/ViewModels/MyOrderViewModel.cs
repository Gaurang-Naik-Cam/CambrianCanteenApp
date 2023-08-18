using CambrianCanteenApp.ViewModels;
using CanteenApp.Common.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.Media.Protection.PlayReady;

namespace CambrianCanteenApp.ViewModels
{
    public class MyOrderViewModel
    {
        List<OrderVM> _myOrders;
        public List<OrderVM> MyOrderList { get { return _myOrders; } set { _myOrders = value; } } 
        HttpClient _client;

        public MyOrderViewModel()
        {
           // _myOrders = new List<OrderVM>();
            _client = new HttpClient();
            //GetMyOrders();
            GetFakeOrders();
        }

        private void GetFakeOrders()
        {
            _myOrders = new List<OrderVM>();
            _myOrders.Add(new OrderVM() { orderNumber = "CAM-ORD-38202313495954", status = "Confirmed", orderCost = "59.12" });
            _myOrders.Add(new OrderVM() { orderNumber = "CAM-ORD-382023135310749", status = "Confirmed", orderCost = "59.12" });
            _myOrders.Add(new OrderVM() { orderNumber = "CAM-ORD-382023174519291", status = "Confirmed", orderCost = "59.12" });
            _myOrders.Add(new OrderVM() { orderNumber = "CAM-ORD-1182023133133900", status = "Confirmed", orderCost = "59.12" });
            _myOrders.Add(new OrderVM() { orderNumber = "CAM-ORD-188202311197563", status = "Confirmed", orderCost = "45.12" });
            _myOrders.Add(new OrderVM() { orderNumber = "CAM-ORD-1882023112611448", status = "Confirmed", orderCost = "59.12" });
            _myOrders.Add(new OrderVM() { orderNumber = "CAM-ORD-1882023114450768", status = "Confirmed", orderCost = "349.95" });
            _myOrders.Add(new OrderVM() { orderNumber = "CAM-ORD-188202311465448", status = "Confirmed", orderCost = "159.12" });
            _myOrders.Add(new OrderVM() { orderNumber = "CAM-ORD-1882023114745532", status = "Confirmed", orderCost = "34.12" });

        }

        private async void GetMyOrders()
        {
            if (App.IsLoggedIn)
            {
                _myOrders = new List<OrderVM>();
                string jsonUser = Preferences.Get(Constants.LoggedInUser, string.Empty);
                StudentVM student = JsonConvert.DeserializeObject<StudentVM>(jsonUser, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

                Uri uri = new Uri(string.Format(Constants.API_URI + "{0}{1}", "Order/ByStudentId?studentId=",student.ID.ToString()));

                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Result>(responseContent, new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    if (result.IsSuccess)
                    {
                        var orders = JsonConvert.DeserializeObject<List<OrderVM>>(result.Data.ToString());
                        foreach (var item in orders)
                        {
                            _myOrders.Add(item);
                        }
                    }
                    else
                        _myOrders = null;

                }
            }

        }
    }
}
