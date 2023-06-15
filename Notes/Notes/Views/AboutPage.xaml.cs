using System;
using Xamarin.Forms;

namespace Notes.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        async void OnInfoButtonClicked(object sender, EventArgs e)
        {
            // Переход на InfoPage.
            var InfoPage = new InfoPage();
            await Navigation.PushAsync(InfoPage);
        }
    }
}