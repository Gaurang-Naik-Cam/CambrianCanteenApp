using CanteenApp.Common.Lib;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net.Http.Json;
using Microsoft.Maui.Controls;
using CambrianCanteenApp.Controls;
//using GameController;

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

        Uri uri = new Uri(string.Format(Constants.API_URI+"{0}", "Account"));

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

                if(result.IsSuccess)
                {
                    //StudentVM student = new StudentVM();//(StudentVM)result.Data;
                    //student = JsonConvert.DeserializeObject<StudentVM>(result.Data.ToString(),new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore});

                    if (Preferences.ContainsKey(Constants.LoggedInUser))
                        Preferences.Remove(Constants.LoggedInUser);

                    Preferences.Set(Constants.LoggedInUser, result.Data.ToString());
                    if (App.IsLoggedIn)
                    {
                        //removing home page
                        var homePage = AppShell.Current.Items.Where(f => f.Title == "Home").FirstOrDefault();
                        if (homePage != null) 
                            AppShell.Current.Items.Remove(homePage);

                        //removing order page
                        var myOrrderPage = AppShell.Current.Items.Where(f => f.Title == "My Orders").FirstOrDefault();
                        if (homePage != null)
                            AppShell.Current.Items.Remove(myOrrderPage);


                        //Adding Flyout Item
                        var flyoutItem = new FlyoutItem()
                        {
                            Title = "Home",
                            FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                            Items =
                            {
                                new ShellContent()
                                {
                                    Title = "Home",
                                    Icon = "homeicon.jpg",
                                    FlyoutIcon = "homeicon.jpg",
                                    ContentTemplate = new DataTemplate(typeof(HomePage))
                                },

                                 new ShellContent()
                                {
                                    Title = "My Cart",
                                    Icon = "cart.jpg",
                                    FlyoutIcon = "cart.jpg",
                                    ContentTemplate = new DataTemplate(typeof(MyCartPage)),
                                },

                                new ShellContent()
                                {
                                    Title = "My Orders",
                                    Icon = "orders.jpg",
                                    FlyoutIcon = "orders.jpg",
                                    ContentTemplate = new DataTemplate(typeof(MyOrdersPage)),
                                }
                                //,
                                //new ShellContent()
                                //{
                                //   Title = "CheckOutPage",
                                //   ContentTemplate = new DataTemplate(typeof(CheckOutPage)),
                                //   IsVisible = false,
                                //   FlyoutItemIsVisible = false,
                                //   Route =nameof(CheckOutPage)
                                //}
                            }
                        };

                        if (!AppShell.Current.Items.Contains(flyoutItem))
                        {
                            AppShell.Current.Items.Add(flyoutItem);
                        }
                        AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
                        AppShell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
                    }

                    //await SecureStorage.Default.SetAsync("Name", student.Name);
                    //await SecureStorage.Default.SetAsync("StudentNumber", student.StudentNumber);
                    //await SecureStorage.Default.SetAsync("Email", student.Email);
                    //await SecureStorage.Default.SetAsync("IsActive", student.IsActive.ToString());
                    //await SecureStorage.Default.SetAsync("EnrolmentDate", student.EnrolmentDate.ToShortTimeString());
                    //await SecureStorage.Default.SetAsync("Id", student.ID.ToString());
                    //await SecureStorage.Default.SetAsync("ProgramName", student.CurrentProgramName);

                    //int number = 5 >= 31 ? 34 : 33;


                    await Navigation.PushAsync(new HomePage());
                    //await Shell.Current.GoToAsync("HomePage");

                }

            }
        }
        catch (Exception ex)
        {
          // Log.Error("Error",ex.Message);
        }


    }
}