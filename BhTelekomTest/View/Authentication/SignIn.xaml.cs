using BhTelekomTest.ViewModel.Authentication;
using Firebase.Auth;

namespace BhTelekomTest.View.Authentication;

public partial class SignIn : ContentPage
{
	public SignIn(SignInViewModel signInViewModel)
	{
		InitializeComponent();

		BindingContext = signInViewModel;
	}
}