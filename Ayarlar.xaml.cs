using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

namespace odev
{
    public partial class Ayarlar : ContentPage
    {
        public Ayarlar()
        {
            InitializeComponent();

            var savedTheme = Preferences.Get("AppTheme", "Default");
            themeSwitch.IsToggled = savedTheme == "Dark";
        }

        private void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
        {
            var selectedTheme = e.Value ? "Dark" : "Light";
            ((App)Application.Current).ApplyTheme(selectedTheme);
        }
        private void LogoutClicked(object sender, EventArgs e)
        {
            Preferences.Remove("UserEmail");
            Preferences.Remove("Username");
            Application.Current.MainPage = new NavigationPage(new Login());
        }
    }
}
