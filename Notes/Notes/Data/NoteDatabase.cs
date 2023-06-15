using Notes.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Data
{
    public class NoteDatabase
    {
        // асинхронное соединение с бд
        readonly SQLiteAsyncConnection database;

        // Конструктор класса NoteDatabase принимает путь dbPath к файлу бд в качестве аргумента
        public NoteDatabase(string dbPath)
        {
            // создается новое асинх. соед-ние с указанным путём
            database = new SQLiteAsyncConnection(dbPath);

            // Note - тип модели данных, представляющий заметку.
            // Метод CreateTableAsync создаёт таблицу в бд для хранения объектов Note.
            database.CreateTableAsync<Note>().Wait();

            // Метод Wait() - чтобы текущий поток выполнения ждал завершения асинх.операции (создания таблицы)
            // перед продолжением выполнения кода.
        }


        /// Отображение всех заметок.
        // Метод GetNotesAsync() выполняет операцию чтения из бд и возвращает список заметок в виде асинх.задачи.
        public Task<List<Note>> GetNotesAsync()
        {
            // Метод Table<Note>() возвращает объект TableQuery<Note> (таблицу с типом Note).
            // ToListAsync() преобразует результат запроса в список List<Note> асинхронно.
            return database.Table<Note>().ToListAsync();

        }


        /// Получение заметок по id.
        // Метод GetNoteAsync(int id) выполняет операцию чтения из бд и возвращает заметку с указанным id.
        public Task<Note> GetNoteAsync(int id)
        {

            // Table<Note>() возвращает объект TableQuery<Note>.
            // Where() проверяет условие i.ID == id для фильтрации записей таблицы заметок.
            // FirstOrDefaultAsync() асинхронно возвращает первую запись, удовлетворяющую условию, либо null.
            return database.Table<Note>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();

        }


        /// Сохранение (обновление/создание) заметок.
        // Метод SaveNoteAsync(Note note) выполняет операцию сохр-ния заметки в бд и возвращает целочисленный результат.
        public Task<int> SaveNoteAsync(Note note)
        {

            // Сохранение заметок.
            if (note.ID != 0)
            {
                // Обновление существующей заметки.
                return database.UpdateAsync(note);
                //обновляет запись заметки в бд асинхронно на основе переданного объекта note.
            }
            else
            {
                // Создание новой заметки.
                return database.InsertAsync(note);
                //создает новую запись заметки в бд асинхронно на основе переданного объекта note.
            }

        }

        /// Отображение заметок-событий.
        // Метод GetNotesWithReminders() возвращает список.
        public async Task<List<Note>> GetNotesWithReminders()
        {
            DateTime targetDate = new DateTime(1900, 1, 1);
            return await database.Table<Note>().Where(n => n.ReminderDate != targetDate).ToListAsync();
        }
        public async Task<List<Note>> GetNotesOnDate(DateTime selectedDate)
        {
            //return await database.Table<Note>().Where(n => n.ReminderDate.Date.Equals(selectedDate.Date)).ToListAsync();
            var selectedDateStart = selectedDate.Date;
            var selectedDateEnd = selectedDate.Date.AddDays(1);

            return await database.Table<Note>()
                .Where(n => n.ReminderDate >= selectedDateStart && n.ReminderDate < selectedDateEnd)
                .ToListAsync();
        }

        // Заметки на основке текущей даты
        public async Task<List<Note>> ShowReminderPopups()
        {
            /// Получение текущей даты и времени
            DateTime currentDateTime = DateTime.Now.Date; // Установка времени на 00:00:00 для сравнения по дате

            // Получение списка заметок
            List<Note> allNotes = await database.Table<Note>().ToListAsync();

            // Отфильтровать заметки с совпадающим ReminderDate
            List<Note> reminderNotes = allNotes.Where(n => n.ReminderDate.Date == currentDateTime).ToList();

            return reminderNotes;

        }

        /// Удаление заметок.
        // Метод DeleteNoteAsync(Note note) выполняет операцию удаления заметки из бд и возвращает целочисленный результат.
        public Task<int> DeleteNoteAsync(Note note)
        {

            // Удаление заметок.
            return database.DeleteAsync(note);

            //результат метода: кол-во удалённых записей.

        }


        /// Поиск заметок.
        // Метод SearchNotes(string searchText) выполняет операцию поиска заметок в бд по заданному тексту и возвращает список найденных заметок.
        public async Task<List<Note>> SearchNotes(string searchText)
        {

            // Table<Note>() возвращает таблицу.
            // Where() оставляет записи, у которых значение св-ва Name содержит указанный текст поиска, игнорируя регистр символов.
            // ToListAsync() асинхронно преобразует результат запроса в список List<Note>.
            var searchResults = await database.Table<Note>()
            .Where(n => n.Name.ToLower().Contains(searchText.ToLower()))
            .ToListAsync();
            return searchResults;

        }


        /// Сортировка заметок по дате создания/изменения.
        // Метод GetNotesByDate(bool ascending) выполняет операцию получения заметок из бд, отсортированных по дате создания/изменения,
        // и возвращает список заметок.
        public async Task<List<Note>> GetNotesByDate(bool ascending)
        {
            // Метод принимает булевое значение ascending, что указывает на сортировку в порядке возрастания (true) или убывания (false).

            var order = ascending ? "ASC" : "DESC"; // устанавливается в "ASC" или "DESC" в зав. от значения ascending.
            return await database.QueryAsync<Note>($"SELECT * FROM Note ORDER BY Date {order}");

            // QueryAsync<Note> выполняет SQL-запрос асинхронно и возвращает результат в виде списка объектов типа Note.

        }


        /// Отображение избранных заметок.
        // Метод GetFavoriteNotesAsync() возвращает список избранных заметок из бд.
        public Task<List<Note>> GetFavoriteNotesAsync()
        {

            // фильтрует записи, оставляя только помеченные как избранные.
            return database.Table<Note>().Where(n => n.IsFavorite == true).ToListAsync();

        }


        // Добавление заметок в избранные
        // Метод SetNoteFavoriteAsync(Note note, bool isFavorite) выполняет операцию добавления или удаления заметки из избранных.
        public Task<int> SetNoteFavoriteAsync(Note note, bool isFavorite)
        {

            note.IsFavorite = isFavorite; // устанавливается значение свойства IsFavorite
            return database.UpdateAsync(note); // обновляет запись заметки

            //результат метода: количество измененных записей.

        }

        /*// Фильтрация отмеченных заметок
        public async Task<List<Note>> GetCheckedNotesAsync()
        {
            // Выборка отмеченных заметок из базы данных
            List<Note> checkedNotes = await database.Table<Note>().Where(n => n.IsChecked).ToListAsync();
            return checkedNotes;
        }*/

        // Метод копирования заметки с добавлением 7 дней к полю ReminderDate
        public async Task<Note> CopyNoteWithAddDays(Note originalNote)
        {
            Note copiedNote = new Note
            {
                Name = originalNote.Name + "_копия",
                ReminderDate = originalNote.ReminderDate.AddDays(7),
                
            };

            // Добавление скопированной заметки в базу данных
            await database.InsertAsync(copiedNote);

            return copiedNote;
        }
    }
}