namespace CambrianCanteenApp;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		if(count <= 20)
			count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else if (count == 20)
			CounterBtn.Text = $"You have reached the max count";
		else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

