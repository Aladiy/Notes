using System;
using System.IO;
using Notes.Data;
using Xamarin.Forms;
using Notes.Views;

namespace Notes
{
    public partial class App : Application
    {
        static NoteDatabase database;
        static DataRepository datarepository;
        //static UserNameDatabase userdatabase;
        public static int Height { get; set; }
        public static int Width { get; set; }

        public static NoteDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return database;
            }
        }
        public static DataRepository DataRepository
        {
            get
            {
                if (datarepository == null)
                {
                    database = new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return datarepository;
            }
        }
        /*public static UserNameDatabase UserNameDatabase
        {
            get
            {
                if (userdatabase == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserNotes.db3");
                    userdatabase = new UserNameDatabase(dbPath);
                }
                return userdatabase;
            }
        }*/
        public App()
        {

            InitializeComponent();
            MainPage = new NavigationPage(new FingerPage()); // первая страница - FingerPage.
            Device.SetFlags(new string[] { "SettingsPage_Experimental" }); //предоставляет доступ к функциям, связанным со страницей настроек
            //Device.SetFlags(new string[] { "Brush_Experimental" });

        }
        
        protected override void OnStart()
        {
        }


        protected override void OnSleep()
        {
        }


        protected override void OnResume()
        {
        }

    }
}