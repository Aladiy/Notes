using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReminderDatePage : ContentPage
    {
        bool hideControls = true;
        public ReminderDatePage()
        {
            InitializeComponent();
            BindingContext = new Note();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            collectionView.ItemsSource = await App.Database.GetNotesWithReminders();

        }
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.ID}");
            }
        }
        // Метод экспорта заметки
        private async void OnShareClicked(object sender, EventArgs e)
        {
            var notes = ((SwipeItem)sender).CommandParameter.ToString();
            var note = int.Parse(notes);
            var noteid = await App.Database.GetNoteAsync(note);
            var message = new ShareTextRequest
            {
                Text = noteid.Name + "\n" + noteid.Text,
                Title = "Поделиться заметкой"
            };
            await Share.RequestAsync(message);
        }
        // Метод перехода на страницу чтения
        private async void OnNoteClicked(object sender, EventArgs e)
        {
            var noteId = ((SwipeItem)sender).CommandParameter.ToString();
            await Navigation.PushAsync(new NotesReadPage(noteId));
        }
        private async void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = NoteSearch.Text;
            var searchResults = await App.Database.SearchNotes(searchText);
            collectionView.ItemsSource = searchResults;
        }
        private async void OnSearchButtonPressed(object sender, EventArgs e)
        {
            var searchText = NoteSearch.Text;
            var searchResults = await App.Database.SearchNotes(searchText);
            collectionView.ItemsSource = searchResults;
        }
        private void OnSortButtonClicked(object sender, EventArgs e)
        {
            hideControls = !hideControls;
            //sortPicker.IsVisible = !hideControls;
            NoteSearch.IsVisible = !hideControls;
        }
        private async void OnCalendarButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarPage());

        }
        private async void OnReminderButtonClickedAsync(object sender, EventArgs e)
        {
            // Получение списка заметок с совпадающим ReminderDate
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.ShowReminderPopups();
        }
    }
}