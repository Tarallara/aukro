using Aukro.Models;
using Aukro.Services;

namespace Aukro.Pages;

public partial class LoginPage : ContentPage
{
    private AppDbContext dbContext;

    public LoginPage()
	{
		InitializeComponent();
        dbContext = new AppDbContext();
    }

	private async void OnLoginButtonClicked(object sender, EventArgs e)
	{
		string username = UsernameEntry.Text;
		string password = PasswordEntry.Text;

        User user = dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user != null)
        {
            await Navigation.PushAsync(new MainPage(user));
        }
        else
        {
            await DisplayAlert("Login Error", "Invalid username or password", "OK");
        }
    }

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        UsernameEntry.Text = "";
        PasswordEntry.Text = "";
        await Navigation.PushAsync(new RegisterPage());
    }

}