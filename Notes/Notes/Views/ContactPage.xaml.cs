/*using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : ContentPage
    {
        public List<Contact> Contacts { get; set; }

        public ContactPage()
        {
            InitializeComponent();
            //Contacts = GetContacts();
            BindingContext = this;
        }

        /* private List<Contact> GetContacts()
         {
             var contacts = new List<Contact>();

             var contactList = Contacts.GetAllAsync().GetAwaiter().GetResult();

             foreach (var contact in contactList)
             {
                 foreach (var phone in contact.Phones)
                 {
                     contacts.Add(new Contact
                     {
                         Name = contact.DisplayName,
                         PhoneNumber = phone.PhoneNumber
                     });
                 }
             }

             return contacts;
         }
    }
}*/