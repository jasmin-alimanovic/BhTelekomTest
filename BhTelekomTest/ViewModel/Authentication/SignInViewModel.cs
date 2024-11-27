using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;

namespace BhTelekomTest.ViewModel.Authentication;

public partial class SignInViewModel(FirebaseAuthClient authClient) : ObservableObject
{
    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;

    [RelayCommand]
    private async Task SignIn()
    {
        try
        {
            await authClient.SignInWithEmailAndPasswordAsync(Email, Password);

            await Shell.Current.GoToAsync("//Products");
        }
        catch (FirebaseAuthException ex)
        {
            await Shell.Current.DisplayAlert(ex.Reason.ToString(), null, "Cancel");
        }
    }

    [RelayCommand]
    private async Task NavigateSignUp()
    {
        await Shell.Current.GoToAsync("//SignUp");
    }
}
