using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace odev
{
    public partial class TodoListPage : ContentPage
    {
        private ObservableCollection<TodoItem> gorevler;

        public TodoListPage()
        {
            InitializeComponent();
            gorevler = new ObservableCollection<TodoItem>();
            TasksListView.ItemsSource = gorevler;
            LoadTasks();
        }

        private async void LoadTasks()
        {
            try
            {
                var firebase = new FirebaseClient("https://gorsel3-ac5bc-default-rtdb.firebaseio.com/");
                var tasks = await firebase
                  .Child("yapilacaklar")
                  .OnceAsync<TodoItem>();

                gorevler.Clear();
                foreach (var task in tasks)
                {
                    task.Object.Id = task.Key;
                    gorevler.Add(task.Object);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void AddTask_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTaskPage(gorevler));
        }

        private async void EditTask_Clicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            var task = button?.CommandParameter as TodoItem;

            if (task != null)
            {
                await Navigation.PushAsync(new AddTaskPage(gorevler, task));
            }
        }

        private async void DeleteTask_Clicked(object sender, EventArgs e)
            {
            var button = sender as ImageButton;
            var task = button?.CommandParameter as TodoItem;

            if (task != null)
            {
                bool isConfirmed = await DisplayAlert("Silinsin Mi? ", " Silmeyi Onyala", "Evet", "Hayır");
                if (isConfirmed)
                {
                    gorevler.Remove(task);
                    var firebase = new FirebaseClient("https://gorsel3-ac5bc-default-rtdb.firebaseio.com/");
                    await firebase
                      .Child("yapilacaklar")
                      .Child(task.Id)
                      .DeleteAsync();
                }
            }
        }

            private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
               {
            var checkBox = sender as CheckBox;
            var task = checkBox?.BindingContext as TodoItem;

                if (task != null)
                   {
                task.IsCompleted = e.Value;

                var firebase = new FirebaseClient("https://gorsel3-ac5bc-default-rtdb.firebaseio.com/");
                await firebase
                  .Child("yapilacaklar")
                  .Child(task.Id)
                  .PutAsync(task);
                 }
            }
       }
}
