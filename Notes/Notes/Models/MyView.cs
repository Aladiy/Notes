using Xamarin.Forms;
using System.ComponentModel;
using Xamarin.Essentials;

namespace Notes.Models
{

    // Класс MyView демонстрирует модель представления с функциональностью управления темой приложения. 
    public class MyView
    {
        public event PropertyChangedEventHandler PropertyChanged; // событие для уведомления об изменении свойств класса.

        // bool DarkTheme - это свойство, которое определяет текущую тему приложения и предоставляет доступ для её изменения.
        public bool DarkTheme
        {

            // get возвращает значение свойства "Тёмная тема" из пользовательских настроек (Preferences) с помощью метода Preferences.Get().          
            get { return Preferences.Get("Тёмная тема", false); }

            // set устанавливает значение свойства "Тёмная тема" в Preferences на основе переданного значения value.
            set
            {

                if (value)
                {
                    App.Current.UserAppTheme = OSAppTheme.Dark; // изменяет тему приложения на тёмную
                }

                else
                {
                    App.Current.UserAppTheme = OSAppTheme.Light; // изменяет тему приложения на светлую
                }
                Preferences.Set("Тёмная тема", value);
                OnPropertyChanged(nameof(DarkTheme)); // метод OnPropertyChanged(nameof(DarkTheme)), чтобы уведомить об изменении свойства.

            }

        }

        // вызывает событие PropertyChanged, чтобы уведомить об изменении свойства.
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
