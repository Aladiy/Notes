using Notes.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    // Добавление нового контакта
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VNotesEntryPage : ContentPage
    {
        DataRepository repository = new DataRepository();
        public VNotesEntryPage()
        {
            InitializeComponent();
        }

        // Метод создания нового контакта
        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            string email = TxtEmail.Text;
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите Имя", "OK");
                return;

            }
            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите Email", "OK");
                return;
            }
            PersonDB person = new PersonDB
            {
                Name = name,
                Email = email
            };
            var isSaved = await repository.Save(person);
            if (isSaved)
            {
                await DisplayAlert("Информация", "Контакт сохранён.", "OK");
                Clear();
            }
            else
            {
                await DisplayAlert("Ошибка", "Контакт не удалось сохранить.", "ОК");
            }
        }
        // Метод для очищения полей Entry
        public void Clear()
        {
            TxtName.Text = string.Empty;
            TxtEmail.Text = string.Empty;
        }
    }
}