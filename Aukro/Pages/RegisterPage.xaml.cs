using Aukro.Models;
using Aukro.Services;

namespace Aukro.Pages;

public partial class RegisterPage : ContentPage
{
    private AppDbContext dbContext;
    public RegisterPage()
	{
		InitializeComponent();
        dbContext = new AppDbContext();
    }

	private async void OnRegisterButtonClicked(object sender, EventArgs e)
	{
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if(IsEntryValid(UsernameEntry, "Username") && IsEntryValid(PasswordEntry, "Password"))
        {

          
            if (dbContext.Users.Any(u => u.Username == username))
            {
                await DisplayAlert("Register Error", "Username already exist", "OK-MP");
            }
            else
            {
                User newUser = new User { Username = username, Password = password };

                dbContext.Users.Add(newUser);

                dbContext.SaveChanges();

                await DisplayAlert("Register complete", "registration went smoothly", "OK");

                await Navigation.PopAsync();
            }
        }
    }

    private bool IsEntryValid(Entry entry, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(entry.Text))
        {
            DisplayAlert("Validation Error", $"{fieldName} is required.", "OK");
            entry.Focus();
            return false;
        }
        return true;
    }

}