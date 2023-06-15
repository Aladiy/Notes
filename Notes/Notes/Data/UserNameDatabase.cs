using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Models;

namespace Notes.Data
{
    public class UserNameDatabase
    {
        readonly SQLiteAsyncConnection userdatabase;


        public UserNameDatabase(string dbPath)
        {
            userdatabase = new SQLiteAsyncConnection(dbPath);
            userdatabase.CreateTableAsync<UserNameDB>().Wait();
        }


        public Task<List<Note>> GetNotesAsync()
        {
            // Отображение всех имён.
            return userdatabase.Table<Note>().ToListAsync();
        }


        public Task<UserNameDB> GetNoteAsync(int id)
        {
            // Получение по id.
            return userdatabase.Table<UserNameDB>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(UserNameDB user)
        {
            // Сохранение заметок.
            if (user.Name != "")
            {
                // Обновление существующей заметки.
                return userdatabase.UpdateAsync(user);
            }
            else
            {
                // Создание новой заметки.
                return userdatabase.InsertAsync(user);
            }
        }

        public Task<int> DeleteNoteAsync(UserNameDB user)
        {
            // Удаление заметок.
            return userdatabase.DeleteAsync(user);
        }
    }
}
