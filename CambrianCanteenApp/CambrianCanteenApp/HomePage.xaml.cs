using CambrianCanteenApp.ViewModels;
using CanteenApp.Common.Lib;
using Newtonsoft.Json;
using System.Text;

namespace CambrianCanteenApp;

public partial class HomePage : ContentPage
{
    //HttpClient _client;
	public HomePage()
	{
		InitializeComponent();
        BindingContext = new HomePageViewModel();
       // _client = new HttpClient();
       // collectionView.ItemsSource = GetMenuCard().Result;//GetFoodItems();
	}

	//private List<FoodItemVM> GetFoodItems()
	//{
	//	return new List<FoodItemVM>()
	//	{
	//		new FoodItemVM () { ItemName = "Pizza", ID = 1, Price= "$1.99", ImageURL = "pizza.jpg", CategoryName = "Fastfood" },
	//		new FoodItemVM () { ItemName = "Burito", ID = 2, Price= "$5.99", ImageURL = "pizza.jpg", CategoryName = "Main Course" },
	//		new FoodItemVM () { ItemName = "Chicken Pizza", ID = 3, Price= "$7.99", ImageURL = "pizza.jpg", CategoryName = "Breakfast" },
	//		new FoodItemVM () { ItemName = "Diet Pizza", ID = 4, Price= "$3.99", ImageURL = "pizza.jpg", CategoryName = "Desert" },
	//		new FoodItemVM () { ItemName = "Jain Pizza", ID = 16, Price= "$2.99", ImageURL = "pizza.jpg", CategoryName = "Main Course" },
	//		new FoodItemVM () { ItemName = "Spicy Pizza", ID = 19, Price= "$1.99", ImageURL = "pizza.jpg", CategoryName = "Fastfood" },
	//		new FoodItemVM () { ItemName = "Toppings Pizza", ID = 20, Price= "$4.99", ImageURL = "pizza.jpg", CategoryName = "Main Course" }
	//	};
	//}

	
}
