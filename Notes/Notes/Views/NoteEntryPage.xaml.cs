using System;
using Notes.Models;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Threading;
using Notes.Views;
using Android.App;
using Android.Content;

namespace Notes.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NoteEntryPage : ContentPage
    {
        bool hideControls = true; //невидимость
        private static DateTime selectedDate;

        // CancellationTokenSource cts; //токен отмены
        // bool isBusy = false;

        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }
        public NoteEntryPage()
        {
            InitializeComponent();

            // Установка связи вводимых данных с базой данных
            BindingContext = new Note();

        }

        async void LoadNote(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Получение заметки и передача в бд
                Note note = await App.Database.GetNoteAsync(id);
                BindingContext = note;


            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка при загрузке заметки.");
            }
        }
        

        // Этот код определяет два метода SpeakNowDefaultSettings() и SpeakNowDefaultSettings2() для произнесения фразы "Hello" с помощью TextToSpeech.
        /*public async Task SpeakNowDefaultSettings()
        {
            // Этот метод использует асинхронный подход и блокируется до завершения произнесения
            cts = new CancellationTokenSource();
            await TextToSpeech.SpeakAsync("Hello");
        }

        // Метод CancelSpeech() позволяет отменить произнесение, если был передан токен отмены.
        public void CancelSpeech()
        {
            if (cts?.IsCancellationRequested ?? true)
                return;

            cts.Cancel();
        }
        public void SpeakNowDefaultSettings2()
        {
            // Этот метод использует неблокирующий подход с помощью метода ContinueWith()
            TextToSpeech.SpeakAsync("Hello").ContinueWith((t) =>
            {
                //Когда произнесение завершается, выполняется логика, переданная в метод ContinueWith().
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }*/


        /* Этот код запускает асинхронную задачу,
        которая произносит три различные фразы ("Hello World 1", "Hello World 2", "Hello World 3") с помощью TextToSpeech. */
        /*public void SpeakMultiple()
        {
            isBusy = true;
            Task.Run(async () =>
            {
                await TextToSpeech.SpeakAsync("Hello World 1");
                await TextToSpeech.SpeakAsync("Hello World 2");
                await TextToSpeech.SpeakAsync("Hello World 3");
                isBusy = false;
            });
            // После того, как все три фразы были произнесены, значение переменной isBusy устанавливается в false, 
             // что указывает на то, что задача завершена.

            // Код также использует метод Task.WhenAll для выполнения нескольких асинхронных задач одновременно
                // и продолжает выполнение кода после того, как все задачи завершились.

            // В конце выполнения всех задач значение переменной isBusy устанавливается в false.
            Task.WhenAll(
                TextToSpeech.SpeakAsync("Hello World 1"),
                TextToSpeech.SpeakAsync("Hello World 2"),
                TextToSpeech.SpeakAsync("Hello World 3"))
                .ContinueWith((t) => { isBusy = false; }, TaskScheduler.FromCurrentSynchronizationContext());
        }*/
        
        //Методы сохранения и удаления заметок
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            note.Date = DateTime.Now;
            
            if (reminderDatePicker != null && reminderTimePicker != null)
            {
                // Получение выбранной даты и времени
                DateTime selectedDate = reminderDatePicker.Date;
                TimeSpan selectedTime = reminderTimePicker.Time;

                // Комбинирование даты и времени в один объект DateTime
                DateTime reminderDateTime = selectedDate.Date + selectedTime;

                note.ReminderDate = reminderDateTime;


                // Установка значения IsTimeAndDateElapsed
                // note.IsTimeAndDateElapsed = reminderDateTime <= DateTime.Now;
                // Установка напоминания
                //await DependencyService.Get<CalendarService>().SetReminder(note.Name, note.ReminderDate);
                
            }
            else
            {
                //note.ReminderDate = DateTime.Now;
            }
            /*else
            {
                // Если не выбрана дата и время, установить значения как null
                note.ReminderDate = DateTime.Now;
                note.IsTimeAndDateElapsed = null;
            }*/

            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                await App.Database.SaveNoteAsync(note);
            }
            if (!string.IsNullOrWhiteSpace(note.Name))
            {
                await App.Database.SaveNoteAsync(note);
            }

            // Переход обратно
            await Shell.Current.GoToAsync("..");
        }
        public async void AddOneWeek(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            await App.Database.CopyNoteWithAddDays(note);
            await DisplayAlert("", "Событие продлено ещё на 7 дней", "ОК");
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {

           var note = (Note)BindingContext;
           await App.Database.DeleteNoteAsync(note);

            // Переход обратно
            await Shell.Current.GoToAsync("..");
        }


        // Метод для невидимости доп. интерфейса
        private void OnSortClicked(object sender, EventArgs e)
        {
            hideControls = !hideControls;
            reminderDatePicker.IsVisible = !hideControls;
            reminderTimePicker.IsVisible = !hideControls;
            btn_addoneweek.IsVisible = !hideControls;
          
        }

        private void OnVisibleClicked(object sender, EventArgs e)
        {
            hideControls = !hideControls;
            btn_favorite.IsVisible = !hideControls;
        }

        // Метод для добавления метки "Избранное" к текущей заметке или удаление этой метки
        async void OnFavoriteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            if (btn_notfavorite.IsVisible == true)
            {
                if(note.IsFavorite == false)
                {
                    btn_favorite.IsVisible = true;
                    btn_notfavorite.IsVisible = false;
                }

                await App.Database.SetNoteFavoriteAsync(note, !note.IsFavorite);
                note.IsFavorite = !note.IsFavorite;
                // Переход на страницу избранных заметок
                var FavoriteNotesPage = new FavoriteNotesPage();
                await Navigation.PushAsync(FavoriteNotesPage);
            }
            
        }
        async void OnNotFavoriteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            if (btn_favorite.IsVisible == true)
            {
                if (note.IsFavorite == true)
                {
                    btn_favorite.IsVisible = false;
                    btn_notfavorite.IsVisible = true;
                }
                
                note.IsFavorite = false;

                // Переход обратно
                await Shell.Current.GoToAsync("..");
            }
        }    
    }
}