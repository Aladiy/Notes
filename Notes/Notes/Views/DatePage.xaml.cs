/*using Android.Media.TV;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatePage : ContentPage
    {
        private readonly string _noteId;
        private DateTime selectedDateTime;
        public DatePage(string noteId)
        {
            InitializeComponent();
            _noteId = noteId;
        }
        public DatePage()
        {
            InitializeComponent();
            var datePicker = new DatePicker
            {
                Format = "D"
            };
            var timePicker = new TimePicker();
            datePicker.DateSelected += (sender, e) =>
            {
                var selectedDate = e.NewDate;
                var selectedTime = timePicker.Time;
                selectedDateTime = selectedDate.Date + selectedTime;
            };

            timePicker.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Time")
                {
                    var selectedDate = datePicker.Date;
                    var selectedTime = timePicker.Time;
                    selectedDateTime = selectedDate.Date + selectedTime;
                }
            };

            var saveButton = new Button
            {
                Text = "Сохранить"
            };

            saveButton.Clicked += (sender, e) =>
            {
                var note = new Note
                {
                    ReminderDate = selectedDateTime
                };

                // Сохранение заметки в локальной базе данных.
                App.Database.SaveNoteAsync(note);
            };

            Content = new StackLayout
            {
                Children = { datePicker, timePicker, saveButton },
                Padding = new Thickness(20)
            };
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // загрузка заметки по id и отображение на странице
            var noteId = int.Parse(_noteId);
            var note = await App.Database.GetNoteAsync(noteId); // загрузка заметки по id из базы данных
            //if (note != null)
            //{
              //  LabelName.Text = note.Name; // отображение заголовка заметки
              //  LabelText.Text = note.Text; // отображение текста заметки
            //}
        }
        
    }
}*/