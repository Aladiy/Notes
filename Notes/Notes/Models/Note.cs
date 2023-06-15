using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace Notes.Models
{
    public class Note: INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
        public DateTime Date { get; set; }
        public bool IsFavorite { get; set; }

        // Поля для дат

        private DateTime _reminderDate;
        private TimeSpan _reminderTime;


        // Событие изменения свойства

        public DateTime ReminderDate
        {
            get { return _reminderDate; }
            set
            {
                _reminderDate = value;
                OnPropertyChanged(nameof(ReminderDate)); // Вызов события изменения свойства
            }
        }

       
        public TimeSpan ReminderTime
        {
            get { return _reminderTime; }
            set
            {
                if (_reminderTime != value)
                {
                    _reminderTime = value;
                    OnPropertyChanged(nameof(ReminderTime)); // Вызов события изменения свойства
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // Метод для вызова события изменения свойства
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /*private bool _isTimeAndDateElapsed;
        public bool IsTimeAndDateElapsed
        {
            get { return _isTimeAndDateElapsed; }
            set
            {
                if (_isTimeAndDateElapsed != value)
                {
                    _isTimeAndDateElapsed = value;
                    OnPropertyChanged(nameof(IsTimeAndDateElapsed)); // Генерируем событие PropertyChanged
                }
            }
        }*/

        public static implicit operator Note(AsyncVoidMethodBuilder v)
        {
            throw new NotImplementedException();
        }
    }
}
