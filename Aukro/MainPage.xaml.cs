using Aukro.Converters;
using Aukro.Models;
using Aukro.Pages;
using Aukro.Services;
using Aukro.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Aukro;

public partial class MainPage : ContentPage
{
    private object dbContext;

    public User User { get; set; }

    public MainPage(User user)
    {
        InitializeComponent();
        Resources.Add("konvertor", new konvertor());
        User = user;

        using (var dbContext = new AppDbContext())
        {
            var items = dbContext.Items.ToList();
            BindingContext = new ItemViewModel(items);
        }
    }


    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private async void OnCreateClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ItemCreatePage(User));
    }

    private async void OnBidButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int itemId)
        {
            Item itemToUpdate;
            using (var dbContext = new AppDbContext())
            {
                itemToUpdate = dbContext.Items.FirstOrDefault(item => item.Id == itemId);
            }
            if (itemToUpdate.UserId == User.Id && itemToUpdate != null)
            {
                await DisplayAlert("Bid error", "You cannot bid on your Item", "OK");
            }
            else
            {
                string bidPriceString = await DisplayPromptAsync("Bid", "Enter your bid price:", "Bid", "Cancel", keyboard: Keyboard.Numeric);
                if (!string.IsNullOrEmpty(bidPriceString) && decimal.TryParse(bidPriceString, out decimal bidPrice))
                {
                    using (var dbContext = new AppDbContext())
                    {
                        itemToUpdate = dbContext.Items.FirstOrDefault(item => item.Id == itemId);

                        if(itemToUpdate != null)
                        {
                            if(bidPrice < itemToUpdate.PriceAct)
                            {
                                await DisplayAlert("You cannot bid", "Your Bid is smaller the previous bid", "OK");
                                var items = dbContext.Items.ToList();
                                BindingContext = new ItemViewModel(items);
                            }
                            else
                            {
                                itemToUpdate.PriceAct = bidPrice;
                                itemToUpdate.LastBidder = User.Username;

                                dbContext.SaveChanges();

                                var items = dbContext.Items.ToList();
                                BindingContext = new ItemViewModel(items); 
                            }
                        }
                    }
                }
            }
        }
    }
}

