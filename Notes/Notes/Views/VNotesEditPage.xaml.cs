using Notes.Data;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.IO;

namespace Notes.Views
{
    // Страница чтения/прослушивания
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VNotesEditPage : ContentPage
    {
        PersonDB person = new PersonDB();
        
        public VNotesEditPage(PersonDB person)
        {
            InitializeComponent();

            person_image.Source = FileImageSource.FromUri(new Uri(person.Image));
            LabelName.Text = person.Name;
            LabelEmail.Text = person.Email;

        }
        public async void OpenMailApp(object sender, EventArgs e)
        {
            try
            {
                string recipient = LabelEmail.Text;
                string subject = "Тема письма";
                string currentTime = DateTime.Now.ToString(); // Получение текущего времени
                string body = $"Текст письма\n\n\n\n\n {currentTime}"; // Добавление времени к тексту письма

                string uri = $"mailto:{recipient}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";
                Device.OpenUri(new Uri(uri));
            }
            catch (Exception ex)
            {
                // Обработка исключений
                await DisplayAlert("Ошибка", "Произошла ошибка при открытии почтового приложения: " + ex.Message, "ОК");
            }
        }
        // Метод для прослушивания данных о контакте
        public async void BtnClicked(object sender, EventArgs e)
        {
            await TextToSpeech.SpeakAsync(LabelEmail.Text, new SpeechOptions
            {
                Volume = (float)slidervolume.Value,
                Pitch = 1.0f
            });
        }
        /*public async void DownloadPhotoButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                FileImageSource img = (FileImageSource)person_image.Source;
                string photoPath = ((FileImageSource)img).File;

                // Скачивание фотографии из Firebase Storage
                Stream photoStream = await App.DataRepository.DownloadImage(photoPath);

                if (photoStream != null)
                {
                    // Сохранение фотографии в локальное хранилище устройства
                    string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "photo_fromNotes.jpg");
                    using (FileStream fileStream = new FileStream(localPath, FileMode.Create))
                    {
                        await photoStream.CopyToAsync(fileStream);
                    }

                    // Отображение сообщения об успешном скачивании фотографии
                    await DisplayAlert("Успех", "Фото успешно скачано", "ОК");
                }
                else
                {
                    await DisplayAlert("Ошибка", "Не удалось скачать фото", "ОК");
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений при скачивании фотографии
                await DisplayAlert("Ошибка", "Произошла ошибка при скачивании фото: " + ex.Message, "ОК");
            }
            try
            {
                FileImageSource img = (FileImageSource)person_image.Source;
                // Получение пути к фотографии для скачивания
                string photoPath = ((FileImageSource)img).File;

                // Скачивание фотографии из Firebase Storage
                Stream photoStream = await App.DataRepository.DownloadImage(photoPath);

                // Сохранение фотографии в локальное хранилище устройства
                string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "photo_fromNotes.jpg");
                using (FileStream fileStream = new FileStream(localPath, FileMode.Create))
                {
                    await photoStream.CopyToAsync(fileStream);
                }

                // Отображение сообщения об успешном скачивании фотографии
                await DisplayAlert("Успех", "Фото успешно скачано", "ОК");
            }
            catch (Exception ex)
            {
                // Обработка исключений при скачивании фотографии
                await DisplayAlert("Ошибка", "Произошла ошибка при скачивании фото: " + ex.Message, "ОК");
            }*/
    }
}
