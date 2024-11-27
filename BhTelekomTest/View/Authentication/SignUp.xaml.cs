using BhTelekomTest.ViewModel.Authentication;

namespace BhTelekomTest.View.Authentication;

public partial class SignUp : ContentPage
{
	public SignUp(SignUpViewModel signUpViewModel)
	{
		InitializeComponent();

		BindingContext = signUpViewModel;
	}
}