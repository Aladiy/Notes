using Notes.Models;
using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoriteNotesPage : ContentPage
    {
        public FavoriteNotesPage()
        {
            InitializeComponent();

            // устанавливает новый объект Note в качестве контекста привязки данных для текущей страницы.
            BindingContext = new Note();
        }

        protected override async void OnAppearing()
        {
            // Этот метод используется для выполнения действий,
            // которые должны произойти при отображении страницы или перед началом взаимодействия пользователя с ней.


            base.OnAppearing();
           
            // загрузка избранных заметок в collectionview
            collectionView.ItemsSource = await App.Database.GetFavoriteNotesAsync();
        }

        // Метод при нажатии на каждую из заметок.
        async void OnNotesSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Переход на страницу NoteEntryPage, используя ID в качестве параметра запроса.
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.ID}");
            }
        }

        // Метод экспорта заметки
        private async void OnShareClicked(object sender, EventArgs e)
        {
            var notes = ((SwipeItem)sender).CommandParameter.ToString(); // к элементу SwipeItem привязан ID
            var note = int.Parse(notes); // преобразуем в целое число 
            var noteid = await App.Database.GetNoteAsync(note); // получаем заметку из бд по ID

            // RequestAsync из класса Share для запроса системы на отображение диалогового окна с указанным текстом и заголовком.
            var message = new ShareTextRequest
            {
                Text = noteid.Name + "\n" + noteid.Text,
                Title = "Поделиться заметкой"
            };
            await Share.RequestAsync(message);
        }

        // Метод перехода на страницу чтения/прослушивания
        private async void OnNoteClicked(object sender, EventArgs e)
        {
            var noteId = ((SwipeItem)sender).CommandParameter.ToString(); // к элементу SwipeItem привязан ID
            await Navigation.PushAsync(new NotesReadPage(noteId)); // переход на страницу чтения/прослушивания
        }
    }
}