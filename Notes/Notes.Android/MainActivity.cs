using System;
using Notes;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Plugin.Fingerprint;
using Firebase;


namespace Notes.Droid
{
    [Activity(Label = "Notes", Icon = "@drawable/xamarin_logo", MainLauncher = true, Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // инициализирует Firebase в приложении, используя контекст приложения 
            FirebaseApp.InitializeApp(Application.Context);

            base.OnCreate(savedInstanceState);

            // инициализирует платформу Xamarin.Essentials, которая предоставляет доступ к различным функциям устройства, таким как камера, геолокация, контакты и т.д.
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            // устанавливает текущую активность для использования при аутентификации по отпечатку пальца с помощью библиотеки CrossFingerprint.
            CrossFingerprint.SetCurrentActivityResolver(() => Xamarin.Essentials.Platform.CurrentActivity);

            // инициализирует фреймворк Xamarin.Forms, подготавливая его к использованию в приложении.
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App()); // загружает и запускает экземпляр класса App
            //CreateNotificationChannel();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
   
            FirebaseApp.InitializeApp(Application.Context);
        }
        
    }
}