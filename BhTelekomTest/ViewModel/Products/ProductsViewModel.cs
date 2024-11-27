using System.Collections.ObjectModel;
using BhTelekomTest.Interfaces;
using BhTelekomTest.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BhTelekomTest.ViewModel.Products;

public partial class ProductsViewModel : ObservableObject
{
    public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

    private readonly IProductService productService;

    public ProductsViewModel(IProductService productService)
    {
        this.productService = productService;

        Products = new ObservableCollection<Product>()
        {
            new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Product 1",
                Description = "Description of Product 1",
                EntryDate = DateTime.Now.AddDays(-10),
                ModifiedDate = DateTime.Now,
                ManufacturerName = "Manufacturer 1"
            },
            new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Product 2",
                Description = "Description of Product 2",
                EntryDate = DateTime.Now.AddDays(-5),
                ModifiedDate = DateTime.Now,
                ManufacturerName = "Manufacturer 2"
            }
        };
    }

    public async Task LoadDataAsync()
    {
        Products.Clear();
        (await productService.GetAllAsync()).ForEach(product =>
        {
            Products.Add(product);
        });
    }

    [RelayCommand]
    public async Task DeleteProduct(string id)
    {
        var primaryKey = new PrimaryKey<Product>(new Product
        {
            Id = id
        });

        await productService.DeleteAsync(primaryKey);

        await Shell.Current.DisplayAlert("Delete", "Product deleted successfully.", "OK");
        await LoadDataAsync();
    }

    [RelayCommand]
    public async Task NavigateCreateProduct()
    {
        await Shell.Current.GoToAsync("//CreateProduct");
    }

}
