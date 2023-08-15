namespace CambrianCanteenApp;

public partial class App : Application
{

	public static bool IsLoggedIn 
	{ 
		get { return Preferences.ContainsKey(Constants.LoggedInUser); } 
	}

	public App()
	{
		InitializeComponent();
        if (Preferences.ContainsKey(Constants.LoggedInUser))
            Preferences.Remove(Constants.LoggedInUser);
        MainPage = new AppShell();
		
	}

}
