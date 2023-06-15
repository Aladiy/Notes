using Notes.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			
			InitializeComponent ();
			BindingContext = new MyView(); // привязка данных из MyView
           
        }

        /*public async void SaveButton_Clicked (object sender, EventArgs e)
        {
            var name = (UserNameDB)BindingContext;

            if (!string.IsNullOrWhiteSpace(name.Name))
            {
                await App.UserNameDatabase.SaveNoteAsync(name);
            }
            nameEntry.Placeholder = resultLabel.Text;
        }*/


        //Изменение темы приложения в соответствии с темой устройства
        void Handle_Toggled(object sender, ToggledEventArgs e)
		{
			if (e.Value)
			{
				App.Current.UserAppTheme = OSAppTheme.Dark;
			}
			else
			{
				App.Current.UserAppTheme= OSAppTheme.Light;
			}
		}

        /*public void FontPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*string selectedFont = FontPicker.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedFont))
            {
                App.SelectedFontFamily = selectedFont;
                Application.Current.Resources["LabelStyle"] = new Style(typeof(Label))
                {
                    Setters = {
                        new Setter { Property = Label.FontFamilyProperty, Value = selectedFont }
                    }
                };
            }
            var app = new AppShell();
            var selectedFont = FontPicker.SelectedItem as string;

            switch (selectedFont)
            {
                case "Default Font":
                    app.ApplyFont(AppFonts.DefaultFont);
                    break;
                case "Bold Font":
                    app.ApplyFont(AppFonts.BoldFont);
                    break;
                case "Bold Font2":
                    app.ApplyFont(AppFonts.BoldFont2);
                    break;
                case "Bold Font3":
                    app.ApplyFont(AppFonts.BoldFont3);
                    break;
                    // Добавьте другие случаи для остальных шрифтов
            }
        }*/

        /*void ApplyFont(string fontFileName)
        {
            var fontFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), fontFileName);
            var customFont = Font.OfSize(fontFilePath, 14);

            // Примените выбранный шрифт к стилям элементов интерфейса
            var defaultStyle = new Style(typeof(Label)) { Setters = { new Setter { Property = Label.FontFamilyProperty, Value = customFont.FontFamily } } };
            // Добавьте другие стили для других элементов интерфейса

            // Примените стили к приложению
            Application.Current.Resources["DefaultLabelStyle"] = defaultStyle;
            // Примените другие стили к соответствующим ресурсам
        }*/

    }
}
