using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android;
using Notes.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.Fingerprint;

namespace Notes.Views
{
    public partial class NotesPage : ContentPage
    {
        bool hideControls = true;

        //public ObservableCollection<string> MyItems { get; set; }
        public NotesPage()
        {
            InitializeComponent();

            BindingContext = new Note();
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.Database.GetNotesAsync();
        }

        // Метод при нажатии на каждую из заметок для перехода к её редактированию
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Переход на страницу создания/обновления заметки
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.ID}");
            }
        }

        // Метод для перехода на страницу создания для добавления новой заметки
        async void OnAddClicked(object sender, EventArgs e)
        {
            // Переход на страницу для создания заметки
            await Shell.Current.GoToAsync(nameof(NoteEntryPage));
        }


        // Методы для поиска заметок
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


        // Метод для сортировки заметок
        private async void OnSortPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var ascending = sortPicker.SelectedIndex == 0;
            var sortedNotes = await App.Database.GetNotesByDate(ascending);
            collectionView.ItemsSource = sortedNotes; // обновление отображения коллекции заметок на экране.
        }


        // Метод для отображения скрытого интерфейса
        private void OnSortButtonClicked(object sender, EventArgs e)
        {
            hideControls = !hideControls;
            sortPicker.IsVisible = !hideControls;
            NoteSearch.IsVisible = !hideControls;

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
    }
}
