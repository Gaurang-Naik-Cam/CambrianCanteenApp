using CambrianCanteenApp.ViewModels;

namespace CambrianCanteenApp;

public partial class MyCartPage : ContentPage
{
	public MyCartPage()
	{
		InitializeComponent();
		BindingContext = new MyCartViewModel();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new CheckOutPage());
    }
}