using Plugin.Fingerprint;
using System;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FingerPage : ContentPage
	{
        Label actionLabel;
        public FingerPage ()
		{
			InitializeComponent ();

            // Кнопка входа со свойствами
            Button alertButton = new Button {
                Text = "Войти",
                WidthRequest = 200,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                BorderWidth = 1,
                CornerRadius = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Violet,
                TextColor = Color.White
            };
            alertButton.Clicked += AlertButton_Clicked;
            actionLabel = new Label();
            Content = new StackLayout { Children = { alertButton, actionLabel } };
        }

        // Метод для аутентификации
        private async void AlertButton_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Войти: ", "Отмена", "", "по биометрическим данным");
			if (action == "по биометрическим данным")
			{
                // проверка доступности биом-их данных на уст-ве с помощью метода IsAvailableAsync.
                var availability = await CrossFingerprint.Current.IsAvailableAsync();
                if (!availability)
                {
                    await DisplayAlert("Предупреждение!", "Биометрические данные не найдены", "OK");
                    return;
                }

                var authResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Notes", "Авторизуйтесь для входа в приложение"));
                // аутентификация с помощью метода AuthenticateAsync
                if (authResult.Authenticated)
                {
                    await DisplayAlert("Юхуу!", "Вход прошёл успешно.", "Спасибо!");

                    // Переход к AppShell.
                    var AppShell = new NavigationPage(new AppShell());
                    InitializeComponent();

                    Application.Current.MainPage = new AppShell();

                }
            }

        }

    }
}