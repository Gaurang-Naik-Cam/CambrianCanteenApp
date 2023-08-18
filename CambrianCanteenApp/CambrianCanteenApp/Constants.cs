using CanteenApp.Common.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambrianCanteenApp
{
    public static class Constants 
    {
        static string _authAccessKey = "Cambrian123";
        static string _api_url = " https://565f-184-147-215-77.ngrok-free.app/api/";//"http://192.168.80.1:1000/api/account";
        static string _loggedInUser = "loggedInUser";
        static string _cart = "currentCart";

        static int _applicableTax = 13;
        static int _serviceCharge = 15;

        public static string AuthAccessToken 
        { 
            get { return _authAccessKey; } 
        }

        public static string API_URI 
        { 
            get { return _api_url; } 
        }

        public static string LoggedInUser
        {
            get { return _loggedInUser; }
        }

        public static int Tax
        {
            get { return _applicableTax; }
        }

        public static string Cart
        {
            get { return _cart; }
        }

        public static int ServiceCharge { get => _serviceCharge;}

        public static StudentVM GetLoggedInUserInfo()
        {
            if(App.IsLoggedIn)
            {
                string jsonStudentVM =  Preferences.Default.Get(LoggedInUser, string.Empty);
                if(!string.IsNullOrEmpty(jsonStudentVM))
                {
                    StudentVM student =  JsonConvert.DeserializeObject<StudentVM>(jsonStudentVM, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    return student;
                }
            }
            return null;

        }
    }


}
