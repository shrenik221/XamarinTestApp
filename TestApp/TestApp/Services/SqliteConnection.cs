using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TestApp.DTO;
using Xamarin.Essentials;

namespace TestApp.Services
{
    class SqliteConnection
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
                return;
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDB.db");
            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<User>();
        }

        public static async Task AddUser(string name, string gender, byte[] image)
        {
            await Init();
            var newUser = new User
            {
                Username = name,
                Gender = gender,
                Image = image
            };

            var id = await db.InsertAsync(newUser);
            Console.WriteLine(id);
        }

        public static async Task<IEnumerable<User>> GetUser()
        {
            await Init();
            var Users = await db.Table<User>().ToListAsync();

            return Users;
        }
    }
}
