
using BhTelekomTest.Model;
using BhTelekomTest.ViewModel.Products;
using Newtonsoft.Json;

namespace BhTelekomTest.View.Products;

public partial class ProductsPage : ContentPage
{
	private readonly ProductsViewModel viewModel;

	public ProductsPage(ProductsViewModel viewModel)
	{
        InitializeComponent();

		BindingContext = this.viewModel = viewModel;
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
		await viewModel.LoadDataAsync();
    }

    private async void OnProductSelected(object sender, SelectionChangedEventArgs e)
    {
        // Get the selected product
        var selectedProduct = e.CurrentSelection.FirstOrDefault() as Product;
        var serializedProduct = JsonConvert.SerializeObject(selectedProduct, new JsonSerializerSettings
        {
            DateFormatString = "yyyy-MM-ddTHH:mm:ss" 
        });

        if (selectedProduct != null)
        {
            // Navigate to Edit Page
            await Shell.Current.GoToAsync($"//EditProduct?product={serializedProduct}", true);
        }

        // Clear the selection (optional)
        ((CollectionView)sender).SelectedItem = null;
    }
}