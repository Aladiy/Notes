using Android.App;
using Notes.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesReadPage : ContentPage
    {
        CancellationTokenSource cts; //токен отмены
        private readonly string _noteId;
        bool hideControls = true; //невидимость
        private bool isBusy;
        PersonDB person = new PersonDB();
        public NotesReadPage(string noteId)
        {
            InitializeComponent();
            _noteId = noteId;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // загрузка заметки по id и отображение на странице
            var noteId = int.Parse(_noteId);
            var note = await App.Database.GetNoteAsync(noteId); // загрузка заметки по id из базы данных
            if (note != null)
            {
                LabelName.Text = note.Name; // отображение заголовка заметки
                LabelText.Text = note.Text; // отображение текста заметки
            }
        }
        
        // Метод для невидимости слайдера и кнопки воспроизведения
        private void OnSoundNoteClicked(object sender, EventArgs e)
        {
            hideControls = !hideControls;
            slidervolume.IsVisible = !hideControls;
            btn_sound.IsVisible = !hideControls;
        }
        // Метод для невидимости элементов отправки по почте
        private void OnShareNoteClicked(object sender, EventArgs e)
        {
            hideControls = !hideControls;
            imgbtnvis.IsVisible = !hideControls;
            lblvis.IsVisible = !hideControls;
            EntryEmail.IsVisible = !hideControls;
        }
        
        //Метод воспроизведения заметки
        public async void BtnClicked(object sender, EventArgs e)
        {
            await TextToSpeech.SpeakAsync(LabelText.Text, new SpeechOptions
            {
                Volume = (float)slidervolume.Value,
                Pitch = 1.0f
            });
            
        }

        // Этот код определяет два метода SpeakNowDefaultSettings() и SpeakNowDefaultSettings2() для произнесения фразы "Hello" с помощью TextToSpeech.
        public async Task SpeakNowDefaultSettings()
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
        }


        /* Этот код запускает асинхронную задачу,
        которая произносит три различные фразы ("Hello World 1", "Hello World 2", "Hello World 3") с помощью TextToSpeech. */
        public void SpeakMultiple()
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
        }

        // Метод для отправки заметки по почте
        public async void OpenMailAppforNotes(object sender, EventArgs e)
        {
            //EntryEmail.Text = person.Email;
            try
            {
                // Получение адреса получателя из пользовательского ввода
                if (EntryEmail.Text != null)
                {
                    string recipientEmail = EntryEmail.Text; // EntryEmail - это имя элемента Entry, в котором пользователь вводит почту
                    // Проверка, что пользователь ввел адрес почты
                    /*if (string.IsNullOrWhiteSpace(recipientEmail))
                    {
                        await DisplayAlert("Ошибка", "Введите адрес электронной почты", "ОК");
                        return;
                    }*/

                    // Сравнение почты с существующей в базе данных
                    bool recipient = await App.DataRepository.GetRecipientFromPersonDB(recipientEmail);

                    if (recipient == true)
                    {
                        string subject = "Тема письма";
                        string currentTime = DateTime.Now.ToString(); // Получение текущего времени
                        string body = $"{LabelName}\n{LabelText}\n\n\n\n {currentTime}"; // Добавление времени к тексту письма

                        string uri = $"mailto:{recipientEmail}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";
                        Device.OpenUri(new Uri(uri));
                    }
                    else
                    {
                        await DisplayAlert("Ошибка", "Не удалось получить адрес получателя", "ОК");
                    }
                }
                else
                {
                    await DisplayAlert("Ошибка", "Не заполнено поле для ввода почты", "ОК");
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений
                await DisplayAlert("Ошибка", "Произошла ошибка при открытии почтового приложения: " + ex.Message, "ОК");
            }
        }
    }
    
}