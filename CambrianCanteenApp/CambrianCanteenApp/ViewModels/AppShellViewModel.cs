using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambrianCanteenApp.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {

        [RelayCommand]
        async void SignOut()
        {
            if (Preferences.ContainsKey(nameof(App.IsLoggedIn)))
            {
                Preferences.Remove(Constants.LoggedInUser);
            }

            var loginPage = AppShell.Current.Items.Where(f => f.Title == "Login").FirstOrDefault();
            if (loginPage == null)
            {
                AppShell.Current.Items.Add(new ShellContent()
                {
                    Title = "Login",
                    ContentTemplate = new DataTemplate(typeof(LoginPage)),
                    Route = "LoginPage"
                }); 
            }

            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
           // await Navigation.PushAsync(new HomePage());
        }
    }
}
