using Notes.Data;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Graphics.ImageDecoder;

namespace Notes.Views
{
    // Обновление информации о контакте
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VNotesEditorPage : ContentPage
    {
        MediaFile file;
        DataRepository dataRepository = new DataRepository();
        public VNotesEditorPage(PersonDB person)
        {
            InitializeComponent();
            TxtEmail.Text = person.Email;
            TxtName.Text = person.Name;
            TxtId.Text = person.Id;
        }

        // Метод для обновления контакта
        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            string email = TxtEmail.Text;
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите Имя", "ОК");
                return;

            }
            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите Email", "ОК");
                return;
            }
            PersonDB person = new PersonDB();
            person.Id = TxtId.Text;
            person.Name = name;
            person.Email = email;
            if (file != null)
            {
                string image = await dataRepository.Upload(file.GetStream(), Path.GetFileName(file.Path));
                person.Image = image;
            }
            bool isUpdated = await dataRepository.Update(person);
            if (isUpdated)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Ошибка", "Не удалось обновить контакт", "Выйти");
                return;
            }
     
        }

        // Метод для установки фото
        private async void Image_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize(); // CrossMedia для работы с медиафайлами
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                {
                    return;
                }
                // устанавливает источник изображения DataImage.Source в качестве потока данных из выбранного файла
                DataImage.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", "Не удалось добавить фото.", "Выйти");
                return;
            }
        }
    }
}