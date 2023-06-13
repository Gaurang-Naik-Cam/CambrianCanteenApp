namespace CambrianCanteenApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("login", typeof(LoginPage));
        Routing.RegisterRoute("main", typeof(LoginPage));
        Routing.RegisterRoute("settings", typeof(LoginPage));
    }
}
