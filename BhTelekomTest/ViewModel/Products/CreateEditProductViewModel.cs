using BhTelekomTest.Interfaces;
using BhTelekomTest.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BhTelekomTest.ViewModel.Products; 
public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var dateString = reader.Value.ToString();
        return DateTime.Parse(dateString, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }

    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK"));
    }
}

[QueryProperty(nameof(ProductJson), "product")]
public partial class CreateEditProductViewModel(IProductService productService, FirebaseAuthClient authClient) : ObservableObject
{
    [ObservableProperty]
    Product product = new();

    private bool isUpdate = false;

    public string ProductJson
    {
        set
        {

            if (value != null)
            {
                var format = "yyyy-MM-ddTHH:mm:ss"; // your datetime format
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
                Product = JsonConvert.DeserializeObject<Product>(value, dateTimeConverter);
                isUpdate = true;
            }
            else
            {
                Product = new Product();
            }
        }
    }

    [RelayCommand]
    public async Task CreateEditProduct()
    {
        if (isUpdate)
        {
            await EditProduct(Product);
            return;
        }

        await CreateProduct(Product);
    }

    public async Task CreateProduct(Product product)
    {
        product.EntryDate = DateTime.Now;
        product.ModifiedDate = DateTime.Now;
        product.EntryUserId = authClient.User.Info.Uid;
        product.ModifiedUserId = authClient.User.Info.Uid;

        await productService.CreateAsync(product);

        await Shell.Current.DisplayAlert("Save", "Product saved successfully.", "OK");

        Product = new();

        await Shell.Current.GoToAsync("//Products");
    }

    public async Task EditProduct(Product product)
    {
        var primaryKey = new PrimaryKey<Product>(new Product() { Id = product.Id });
        product.ModifiedDate = DateTime.Now;
        product.ModifiedUserId = authClient.User.Info.Uid;

        await productService.EditAsync(primaryKey, product);

        await Shell.Current.DisplayAlert("Update", "Product updated successfully.", "OK");

        Product = new();
        isUpdate = false;

        await Shell.Current.GoToAsync("//Products");
    }
}
