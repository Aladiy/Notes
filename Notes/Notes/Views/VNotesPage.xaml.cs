using Notes.Data;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VNotesPage : ContentPage
    {
        // Объект класса DataRepository
        DataRepository dataRepository = new DataRepository();   
        public VNotesPage()
        {

            InitializeComponent();


            // RefreshCommand вызовет метод OnAppearing(), что приведет к обновлению содержимого страницы 
            VNotes.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            var persons = await dataRepository.GetAll();

            //Очистка источника данных элемента VNotes
            VNotes.ItemsSource = null;
            VNotes.ItemsSource = persons; // установка нового источника данных persons
            VNotes.IsRefreshing = false;
            // установка свойства IsRefreshing элемента VNotes в значение false, чтобы указать, что процесс обновления завершен
        }

        // Метод для перехода на страницу создания нового контакта
        public void OnAddClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new VNotesEntryPage());

        }

        // Метод для перехода на страницу чтения/прослушивания контакта
        private void VNotes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var person = e.Item as PersonDB;
            Navigation.PushModalAsync(new VNotesEditPage(person));
            ((ListView)sender).SelectedItem = null;

        }
        
        // Метод удаления контакта
        private async void Delete_Tapped(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Удаление контакта", "Вы точно хотите удалить данный контакт?", "Да", "Нет");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await dataRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Информация", "Контакт был удалён.", "ОК");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Информация", "Контакт не был удалён.", "ОК");
                }
            }
        }

        // Метод для перехода на страницу обновления контакта
        private async void EditMenu_Clicked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var person = await dataRepository.GetById(id);
            if (person == null)
            {
                await DisplayAlert("Внимание!", "Данные не найдены", "OК");
            }
            person.Id = id;
            await Navigation.PushModalAsync(new VNotesEditorPage(person));
        }

        // Методы для поиска контакта
        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var persons = await dataRepository.GetAllByName(searchValue);

                VNotes.ItemsSource = null;
                VNotes.ItemsSource = persons;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void Search_Pressed(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var persons = await dataRepository.GetAllByName(searchValue);
            }
            else
            {
                OnAppearing();
            }
        }
    }
}