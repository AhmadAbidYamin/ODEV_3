using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace odev
{
    public partial class AddTaskPage : ContentPage
    {
        private ObservableCollection<TodoItem> _tasks;
        private TodoItem _task;
        private bool _isNewTask;

        public AddTaskPage(ObservableCollection<TodoItem> tasks, TodoItem task = null)
          {
            InitializeComponent();
            _tasks = tasks;
            _isNewTask = task == null;
            _task = task ?? new TodoItem { Date = DateTime.Now };
            BindingContext = _task;
        }

        private async void SaveTask_Clicked(object sender, EventArgs e)
               {
            var selectedDate = TaskDatePicker.Date;
            var selectedTime = TaskTimePicker.Time;
            _task.Date = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, selectedTime.Hours, selectedTime.Minutes, 0);

            var firebase = new FirebaseClient("https://gorsel3-ac5bc-default-rtdb.firebaseio.com/");

            if (_isNewTask)
                         {
            var result = await firebase
            .Child("yapilacaklar")
            .PostAsync(_task);
            _task.Id = result.Key;
            _tasks.Add(_task);
                           }
             else
                     {
             await firebase
             .Child("yapilacaklar")
             .Child(_task.Id)
             .PutAsync(_task);

                           var existingTask = _tasks.FirstOrDefault(t => t.Id == _task.Id);
                              if (existingTask != null)
                                            {
                            var index = _tasks.IndexOf(existingTask);
                           _tasks[index] = _task;
                               }
                             }
                          await Navigation.PopAsync();
                         }

              private async void CancelTask_Clicked(object sender, EventArgs e)
                     {
              await Navigation.PopAsync();
              }
            }
         }
