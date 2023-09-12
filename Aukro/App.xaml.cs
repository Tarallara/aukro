using Aukro.Pages;
using Aukro.Services;

namespace Aukro;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        using (var dbContext = new AppDbContext())
        {
            dbContext.Database.EnsureCreated();

            dbContext.SeedData();
        }

        MainPage = new NavigationPage(new LoginPage());
    }
}
