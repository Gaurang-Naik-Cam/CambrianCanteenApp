using CambrianCanteenApp.ViewModels;

namespace CambrianCanteenApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		//Routing.RegisterRoute("LoginPage", typeof(LoginPage));
  //      Routing.RegisterRoute("HomePage", typeof(HomePage));
  //      Routing.RegisterRoute("MainPage", typeof(MainPage));
  //      Routing.RegisterRoute("SettingsPage", typeof(SettingsPage));

        this.BindingContext = new AppShellViewModel();

    }
}
