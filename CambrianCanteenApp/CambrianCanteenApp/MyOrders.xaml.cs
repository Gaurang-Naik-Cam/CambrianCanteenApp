using CambrianCanteenApp.ViewModels;

namespace CambrianCanteenApp;

public partial class MyOrdersPage : ContentPage
{
	public MyOrdersPage()
	{
		InitializeComponent();
		BindingContext = new MyOrderViewModel();
	}
}