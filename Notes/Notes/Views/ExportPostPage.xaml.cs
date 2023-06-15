using Notes.Data;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExportPostPage : ContentPage
    {
        public ExportPostPage()
        {
            InitializeComponent();
            BindingContext = new Note();
        }
        /*private async void SendButton_Clicked(object sender, EventArgs e)
        {

            // Фильтрация отмеченных заметок
            List<Note> checkedNotes = await App.Database.GetCheckedNotesAsync();

            // Создание строкового представления отмеченных заметок
            StringBuilder emailBody = new StringBuilder();
            foreach (var note in checkedNotes)
            {
                emailBody.AppendLine($"Заголовок: {note.Name}");
                emailBody.AppendLine($"Содержимое: {note.Text}");
                emailBody.AppendLine();
            }*/

            // Отправка почты
            //SendEmail("example@example.com", "Отмеченные заметки", emailBody.ToString());
        }
        /*private void SendEmail(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Отправитель", "your_email@example.com"));
            message.To.Add(new MailboxAddress("Получатель", email));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.TextBody = body;
            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.example.com", 587, false);
                client.Authenticate("your_email@example.com", "your_password");
                client.Send(message);
                client.Disconnect(true);
            }
        }*/
    }

