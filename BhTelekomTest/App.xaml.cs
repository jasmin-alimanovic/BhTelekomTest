using Firebase.Auth;

namespace BhTelekomTest
{
    public partial class App : Application
    {
        private readonly FirebaseAuthClient _authClient;

        public App(FirebaseAuthClient authClient)
        {
            _authClient = authClient;
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell(_authClient));
        }
    }
}