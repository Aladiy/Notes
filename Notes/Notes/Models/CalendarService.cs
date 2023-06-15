using Android.Content;
using Notes.Interface;

namespace Notes.Models
{
    public class CalendarService : ICalendarInt
    {
        // Метод OpenCalendar() реализует функциональность открытия приложения календаря на устройствах Android.
        /*public void OpenCalendar()
        {

            // Открываем приложение календаря Android

            // Объект Intent представляет намерение выполнить операцию.
            // Устанавливается действие намерения Intent.ActionMain, указывающее на главное действие приложения.
            Intent intent = new Intent(Intent.ActionMain);

            // Добавляется категория Intent.CategoryAppCalendar, указывающая на приложение календаря.
            intent.AddCategory(Intent.CategoryAppCalendar);

            // Устанавливается флаг ActivityFlags.NewTask, указывающий на создание новой задачи для запуска активности календаря.
            intent.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(intent); // StartActivity() для запуска активности календаря.

        }*/
    }
}
