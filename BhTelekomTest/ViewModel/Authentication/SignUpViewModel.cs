using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;

namespace BhTelekomTest.ViewModel.Authentication;

public partial class SignUpViewModel(FirebaseAuthClient authClient) : ObservableObject
{

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _username;

    [ObservableProperty]
    private string _password;

    [RelayCommand]
    private async Task SignUp()
    {
        try
        {
            await authClient.CreateUserWithEmailAndPasswordAsync(Email, Password, Username);

            await Shell.Current.GoToAsync("//SignIn");
        }
        catch (Exception ex) 
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Cancel");
        }
    }

    [RelayCommand]
    private async Task NavigateSignIn()
    {
        await Shell.Current.GoToAsync("//SignIn");
    }
}
