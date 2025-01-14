using odev.Model;
using Microsoft.Maui.Storage;

namespace odev
{
    public partial class Login : ContentPage
    {
        private readonly FireBaseServices firebaseService;

        public Login()
        {
            InitializeComponent();
            firebaseService = new FireBaseServices();
            
          }
              private void KayitLoginEkraniGoster(object sender, EventArgs e)
                      {
               if (kayitEkran.IsVisible)
                  {
                   kayitEkran.IsVisible = false;
                   loginEkran.IsVisible = true;
                   
                    }
               else
                      {
                  loginEkran.IsVisible = false;
                  kayitEkran.IsVisible = true;
                  
                }
          }

            private void OnEmailTextChanged(object sender, TextChangedEventArgs e)
                  {
            var emailEntry = sender as Entry;
            var email = emailEntry.Text;
            var emailErrorLabel = emailEntry == txtEmail ? emailError : regEmailError;

          if (!IsValidEmail(email))
                   {
          emailErrorLabel.Text = "Geçerli Bir E-posta Giriniz!";
          emailErrorLabel.IsVisible = true;
              }

            else
               {
          emailErrorLabel.IsVisible = false;
             }
        }

             private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
                 {
            var passwordEntry = sender as Entry;
            var password = passwordEntry.Text;
            var passwordErrorLabel = passwordEntry == txtPassword ? passwordError : regPasswordError;

            if (password.Length < 8)
                {
                passwordErrorLabel.Text = "Şifre en az 8 karakter olmalıdır";
                passwordErrorLabel.IsVisible = true;
             }
                 else
               {
                passwordErrorLabel.IsVisible = false;
               }
          }

         private bool IsValidEmail(string email)
           {
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
          }

           private async void RegisterClicked(object sender, EventArgs e)
                {
            var username = txtName.Text?.ToUpper();
            var email = txtREmail.Text;
            var password = txtRPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Hata", "Tüm alanları doldurunuz", "Tamam");
                return;
            }

            if (!IsValidEmail(email) || password.Length < 7)
              {
             return;
               }

            var user = new odev.Model.User { Username = username, Email = email, Password = password };
            var result = await firebaseService.AddUser(user);

            if (result != null)
               {
             await DisplayAlert("Başarılı", "Kayıt Başarlı", "Tamam");
                   }

                  else
                {
              await DisplayAlert("Hata", "Kayıt Başarısız", "Tamam");
             }
         }

            private async void LoginInClicked(object sender, EventArgs e)
                     {
            var email = txtEmail.Text;
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                     {
             await DisplayAlert("Hata", "Tüm Alanları Doldurunuz", "Tamam");

              return;
            }

            if (!IsValidEmail(email) || password.Length < 7)
            {

              return;
               }

                var user = await firebaseService.GetUserByEmailAndPassword(email, password);

                if (user != null)
                     {
                Preferences.Set("UserEmail", email);
                Preferences.Set("Username", user.Username);
                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("//WelcomePage");
               }

              else
               {

              await DisplayAlert("Hata", "Kullanıcı Adı Veya ,Şifre Hatalı", "Tamam");
            }
        }
    }
}
