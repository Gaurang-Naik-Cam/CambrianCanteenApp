using CanteenApp.Common.Lib;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net.Http.Json;

namespace CambrianCanteenApp;

public partial class LoginPage : ContentPage
{
    HttpClient _client;
    //JsonSerializerOptions _serializerOptions;

    public LoginPage()
	{
		InitializeComponent();
        _client = new HttpClient();
        //_serializerOptions = new JsonSerializerOptions
        //{
        //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //    //NumberHandling = JsonNumberHandling.AllowReadingFromString,
        //    WriteIndented = true
            
        //};
    }



    private async void Button_Clicked(object sender, EventArgs e)
    {
		string userName = txtbxUser.Text.Trim();
		string pwd = txtbxPwd.Text.Trim();

        Uri uri = new Uri(string.Format(Constants.API_URI, string.Empty));

        try
        {
            HttpResponseMessage response = null;
            AppCredentials appCredentials = new AppCredentials() { UserName = userName, Password = pwd };
            string json = JsonConvert.SerializeObject(appCredentials); //JsonSerializer. Serialize<AppCredentials>(new AppCredentials() { UserName = userName, 
            //Password = pwd}, _serializerOptions);
           
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
               // var responseContent = await response.Content.ReadFromJsonAsync<Result>()
                //Log.Debug("Response", responseContent);
                
                var result = JsonConvert.DeserializeObject<Result>(responseContent, new JsonSerializerSettings()
                {
                     NullValueHandling = NullValueHandling.Ignore
                });
            }
        }
        catch (Exception ex)
        {
          // Log.Error("Error",ex.Message);
        }


    }
}