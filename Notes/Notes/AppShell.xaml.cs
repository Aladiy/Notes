using Notes.Views;
using Plugin.Fingerprint;
using System;
using System.IO;
using Xamarin.Forms;


namespace Notes
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {

            InitializeComponent();
            Routing.RegisterRoute(nameof(NoteEntryPage), typeof(NoteEntryPage)); //регистрация маршрута для страницы NoteEntryPage
        }

    }
}