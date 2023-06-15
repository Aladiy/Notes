using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Data
{
    // Класс PersonDB представляет модель данных с информацией о контакте в бд.
    public class PersonDB
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
    }
}
