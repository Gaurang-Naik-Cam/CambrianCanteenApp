using CambrianCanteenApp.ViewModels;

namespace CambrianCanteenApp;

public partial class CheckOutPage : ContentPage
{
	public CheckOutPage()
	{
		InitializeComponent();
		BindingContext = new CheckoutViewModel();
	}
}