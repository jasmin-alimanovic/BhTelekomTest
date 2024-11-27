using BhTelekomTest.ViewModel.Products;

namespace BhTelekomTest.View.Products;

public partial class CreateEditProduct : ContentPage
{
	public CreateEditProduct(CreateEditProductViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}