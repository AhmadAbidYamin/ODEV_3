using System.ComponentModel;

namespace odev
{
    public class TodoItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isCompleted;
        public bool IsCompleted
              {
            get => isCompleted;
            set
                 {
         if (isCompleted != value)
                {
            isCompleted = value;
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCompleted)));
                }
            }
         }

            public string Id { get; set; }
            public string Name { get; set; }
            public string Detail { get; set; }
            public DateTime Date { get; set; }
          }
      }
