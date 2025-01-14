namespace odev
   {
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            CheckUserLoggedIn();
        }

   private async void CheckUserLoggedIn()
        {
     bool isLoggedIn = Preferences.ContainsKey("Username");
         if (!isLoggedIn)
       {
         Application.Current.MainPage = new NavigationPage(new Login());
             }
          }
       }
}
