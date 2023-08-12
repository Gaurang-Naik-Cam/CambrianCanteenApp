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
        static string _api_url = " https://565f-184-147-215-77.ngrok-free.app/api/account";//"http://192.168.80.1:1000/api/account";

        public static string AuthAccessToken 
        { 
            get { return _authAccessKey; } 
        }

        public static string API_URI 
        { 
            get { return _api_url; } 
        }
    }


}
