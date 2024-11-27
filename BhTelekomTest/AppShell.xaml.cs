using System.Windows.Input;
using Firebase.Auth;

namespace BhTelekomTest
{
    public partial class AppShell : Shell
    {
        public ICommand SignOutCommand { get; }

        private readonly FirebaseAuthClient _authClient;

        public AppShell(FirebaseAuthClient authClient)
        {
            InitializeComponent();

            _authClient = authClient;

            SignOutCommand = new Command(async () => await SignOut());

            BindingContext = this;
        }

        private async Task SignOut()
        {
            _authClient.SignOut();

            await Shell.Current.GoToAsync("//SignIn");
        }

        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            if (_authClient.User != null)
            {
                await Current.GoToAsync("//Products");
            }
        }
    }
}
