using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Notes.Views
{
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
            BindingContext = new Note();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            collectionView.ItemsSource = await App.Database.ShowReminderPopups();

        }
        private async void DatePicker_DateSelected(object sender, EventArgs e)
        {
            // Получение выбранной даты из DatePicker
            DateTime selectedDate = reminderDatePicker.Date;
            base.OnAppearing();
            // Установка выбранной даты в свойство ReminderDate
            ((Note)BindingContext).ReminderDate = selectedDate;

            // Получение списка заметок с совпадающей ReminderDate
            List<Note> filteredNotes = await App.Database.GetNotesOnDate(selectedDate);
            
            // Присвоение отфильтрованного списка заметок свойству ItemsSource коллекции collectionView
            collectionView.ItemsSource = filteredNotes;
            //collectionView.ItemsSource = await App.Database.GetNotesOnDate(selectedDate); ;
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.ID}");
            }
        }
    }
}