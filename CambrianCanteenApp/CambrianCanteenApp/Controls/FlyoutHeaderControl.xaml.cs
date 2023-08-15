namespace CambrianCanteenApp.Controls;

public partial class FlyoutHeaderControl : StackLayout
{
	public FlyoutHeaderControl()
	{
		InitializeComponent();

        if (App.IsLoggedIn)
        {
            lblUserName.Text = "Gaurang Naik";
            lblUserEmail.Text = "Gaurang.Naik@Cambrian.ca";
           
        }
    }
}