using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.ApplicationModel;

namespace odev
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

         Routing.RegisterRoute("WelcomePage", typeof(WelcomePage));
         Routing.RegisterRoute("Doviz", typeof(Doviz));
         Routing.RegisterRoute("TodoListPage", typeof(TodoListPage));
         Routing.RegisterRoute("HavaDurumu", typeof(HavaDurumu));
         Routing.RegisterRoute("Haberler", typeof(Haberler));
         Routing.RegisterRoute("Ayarlar", typeof(Ayarlar));

                       var savedTheme = Preferences.Get("AppTheme", "Default");
                       ApplyTheme(savedTheme);

       if (Preferences.ContainsKey("UserEmail"))
               {
            MainPage = new NavigationPage(new QuickLoginPage());
               }
         else
      {
            MainPage = new NavigationPage(new Login());
             }
      }

       public void ApplyTheme(string theme)
           {
        Preferences.Set("AppTheme", theme);
         switch (theme)
          {
            case "Light":
               Application.Current.UserAppTheme = AppTheme.Light;
               Current.Resources["TextColor"] = Current.Resources["TextColorLight"];
               Current.Resources["BackgroundColor"] = Current.Resources["BackgroundColorLight"];
               Current.Resources["ShellBackgroundColor"] = Current.Resources["ShellBackgroundColorLight"];
                            break;
        case "Dark":
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    Current.Resources["TextColor"] = Current.Resources["TextColorDark"];
                    Current.Resources["BackgroundColor"] = Current.Resources["BackgroundColorDark"];
                    Current.Resources["ShellBackgroundColor"] = Current.Resources["ShellBackgroundColorDark"];
                            break;
            default:
                   Application.Current.UserAppTheme = AppTheme.Unspecified;
                          break;
               }
             }
        }
}
