using SQLite;

namespace Notes.Models
{
    public class UserNameDB
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }

    }
}
