using Aukro.Models;
using Aukro.Services;
using Microsoft.EntityFrameworkCore;

namespace Aukro.Pages;

public partial class ItemCreatePage : ContentPage
{
    private AppDbContext dbContext;
    public User User { get; set; }
    private List<Category> categories;
    private byte[] selectedImageData;

    public ItemCreatePage(User user)
    {
		InitializeComponent();
        dbContext = new AppDbContext();
        User = user;
        
        categories = dbContext.Categories.ToList();
        foreach (var category in categories)
        {
            CategoryPicker.Items.Add(category.Name);
        }

        CategoryPicker.SelectedIndex = 0;
    }

    private async void OnCreateItemClicked(object sender, EventArgs e)
    {
        if (IsEntryValid(ItemNameEntry, "Name") && IsEntryValid(DescriptionEntry, "Description") && IsEntryValid(ItemPriceEntry, "Starting Price") && IsPickerValid(CategoryPicker, "Category"))
        {
            string itemName = ItemNameEntry.Text;
            string description = DescriptionEntry.Text;
            if (decimal.TryParse(ItemPriceEntry.Text, out decimal startingPrice))
            {
                string selectedCategoryName = CategoryPicker.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedCategoryName))
                {
                    var selectedCategory = categories.FirstOrDefault(c => c.Name == selectedCategoryName);

                    if (selectedCategory != null)
                    {
                        Item newItem = new Item
                        {
                            Name = itemName,
                            Description = description,
                            Price = startingPrice,
                            CategoryId = selectedCategory.Id,
                            CategoryName = selectedCategory.Name,
                            Date = DateTime.Now,
                            UserId = User.Id,
                            UserName = User.Username,
                            LastBidder = "-",
                            PriceAct = startingPrice,
                            ImageData = selectedImageData
                        };

                        dbContext.Items.Add(newItem);
                        dbContext.SaveChanges();

                        await DisplayAlert("Item Created", "The item has been created successfully.", "OK");

                        ItemNameEntry.Text = "";
                        DescriptionEntry.Text = "";
                        ItemPriceEntry.Text = "";

                        await Navigation.PushAsync(new MainPage(User));
                    }
                }
                else
                {
                    await DisplayAlert("Category Error", "Please select a category.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Invalid Input", "Please enter a valid starting price.", "OK");
            }
        }
    }

    private async void OnSelectImageClicked(object sender, EventArgs e)
    {
        var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
        {
            Title = "Select Images"
        });

        if (result != null)
        {
            using (var stream = await result.OpenReadAsync())
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                selectedImageData = memoryStream.ToArray();
            }
            ItemImage.Source = ImageSource.FromStream(() => new MemoryStream(selectedImageData));
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

    private bool IsPickerValid(Picker picker, string fieldName)
    {
        if (picker.SelectedIndex == -1)
        {
            DisplayAlert("Validation Error", $"{fieldName} is required.", "OK");
            picker.Focus();
            return false;
        }
        return true;
    }
}
