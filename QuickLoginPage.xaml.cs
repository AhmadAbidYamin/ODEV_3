using Microsoft.Maui.Storage;
using odev.Model;

namespace odev
{
    public partial class QuickLoginPage : ContentPage
    {
        private readonly FireBaseServices firebaseService;

        public QuickLoginPage()
        {
            InitializeComponent();
            firebaseService = new FireBaseServices();
            lblEmail.Text = Preferences.Get("Username", ""); 
        }

        private async void LoginInClicked(object sender, EventArgs e)
        {
            var email = Preferences.Get("UserEmail", "");
            var password = txtPassword.Text;

            var user = await firebaseService.GetUserByEmailAndPassword(email, password);

            if (user != null)
            {
                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("//WelcomePage");
            }
            else
            {
                await DisplayAlert("Hata", "Şifre hatalı", "Tamam");
            }
        }

        private void LogoutClicked(object sender, EventArgs e)
        {
            Preferences.Remove("UserEmail");
            Preferences.Remove("Username"); 
            Application.Current.MainPage = new NavigationPage(new Login());
        }
    }
}
