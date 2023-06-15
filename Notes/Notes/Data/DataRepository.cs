using Firebase.Database;
using Firebase.Storage;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using FirebaseAdmin.Auth;
using System.Net.Http;

namespace Notes.Data
{
    public class DataRepository
    {

        // Эти объекты предоставляют доступ к сервисам Firebase Realtime Database (FRD) и Firebase Storage (FS).

        FirebaseClient firebaseClient = new FirebaseClient("https://dailynotes-736fe-default-rtdb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("dailynotes-736fe.appspot.com");


        /// Сохранение контакта.
        // Метод Save(PersonDB person) асинхронно сохраняет объект person в FRD.
        public async Task<bool> Save(PersonDB person)
        {

            // firebaseClient.Child...создает ссылку на узел в FRD, соответствующий типу PersonDB.
            // Метод PostAsync() выполняет операцию POST-запроса к указанной ссылке, отправляя сериализованные данные объекта person в виде JSON-строки.
            
            var data = await firebaseClient.Child(nameof(PersonDB)).PostAsync(JsonConvert.SerializeObject(person));
            
            if (!string.IsNullOrEmpty(data.Key)) // объект data с  уникальным ключом
            {
                return true; // успешное сохранение объекта person
            }
            return false;

        }

        /// Отображение списка контактов.
        // Метод GetAll() асинх-но извлекает все объекты типа PersonDB из FRD и возвращает их в виде списка.
        public async Task<List<PersonDB>> GetAll()
        {

            // OnceAsync<PersonDB>() выполняет операцию однократного чтения данных с указанной ссылки
            // и возвращает результат в виде объекта FirebaseObject<PersonDB>

            return (await firebaseClient.Child(nameof(PersonDB)).OnceAsync<PersonDB>()).Select(item => new PersonDB
            {
                Email = item.Object.Email,
                Name = item.Object.Name,
                Image = item.Object.Image,
                Id = item.Key
            }).ToList();

            // Каждый объект FirebaseObject<PersonDB> представляет одну запись данных из FRD.

        }


        // Отображение по Name.
        // GetAllByName(string Name) асинх-но извлекает все объекты типа PersonDB из FRD, фильтрует их по имени (Name) и возвращает соответствующий список.
        public async Task<List<PersonDB>> GetAllByName(string Name)
        {

            return (await firebaseClient.Child(nameof(PersonDB)).OnceAsync<PersonDB>()).Select(item => new PersonDB
            {
                Email = item.Object.Email,
                Name = item.Object.Name,
                Image = item.Object.Image,
                Id = item.Key
            }).Where(c=>c.Name.ToLower().Contains(Name.ToLower())).ToList();

        }


        // Отображение по id.
        // GetById(string id) асинх-но получает объект типа PersonDB из FRD по id.
        public async Task<PersonDB> GetById(string id)
        {

            return await firebaseClient.Child(nameof(PersonDB) + "/" + id).OnceSingleAsync<PersonDB>();

            // OnceSingleAsync возвращает единственный объект.

        }


        // Обновление контакта.
        // Update(PersonDB person) асинх-но обновляет объект типа PersonDB в FRD.
        public async Task<bool> Update(PersonDB person)
        {

            await firebaseClient.Child(nameof(PersonDB) + "/" + person.Id).PutAsync(JsonConvert.SerializeObject(person));
            return true;

            // PutAsync() выполняет операцию обновления данных на указанной ссылке,
            // отправляя сериализованные данные объекта person в виде JSON-строки.
        }


        // Удаление контакта.
        // Delete(string id) асинх-но удаляет объект типа PersonDB из FRD по id.
        public async Task<bool> Delete(string id)
        {

            await firebaseClient.Child(nameof(PersonDB) + "/" + id).DeleteAsync();
            return true;

        }

        // Загрузка фото.
        // Upload(Stream img, string fileName) асинх-но загружает изображение (img) в FS под указанным именем файла (fileName).
        public async Task<string> Upload(Stream img, string fileName)
        {

            var image = await firebaseStorage.Child("images").Child(fileName).PutAsync(img);
            return image;

            // PutAsync() выполняет операцию загрузки данных на указанную ссылку в FS.
        }

        // Метод для получения изображения из Firebase Storage по заданному пути
        /*public async Task<Stream> DownloadImage(string imagePath)
        {
            var imageRef = firebaseStorage.Child(imagePath);
            var downloadUrl = await imageRef.GetDownloadUrlAsync();

            using (HttpClient client = new HttpClient())
            {
                var stream = await client.GetStreamAsync(downloadUrl);
                return stream;
            }
        }*/
        
        // Метод для получения почты контакта
        public async Task<bool> GetRecipientFromPersonDB(string email)
        {
            FirebaseClient firebaseClient = new FirebaseClient("https://dailynotes-736fe-default-rtdb.firebaseio.com/");
            // Выполнение запроса к базе данных для проверки существования почты
            var person = await firebaseClient
                .Child(nameof(PersonDB))
                .OnceAsync<PersonDB>();

            foreach (var item in person)
            {
                PersonDB recipient = item.Object;
                if (recipient.Email == email)
                {
                    return true; // Почта существует в базе данных
                }
            }

            return false; // Почта не найдена в базе данных
        }
    }
}
