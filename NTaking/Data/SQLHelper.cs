using NTaking.Models;
using NTaking.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NTaking.Data
{
    public class SQLHelper
    {
        private readonly SQLiteAsyncConnection db;
        public SQLHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<User>().Wait();
            db.CreateTableAsync<Invigilator>().Wait();
            db.CreateTableAsync<Session>().Wait();
        }
        //Users
        /// <summary>
        /// Create a new user 
        /// </summary>
        /// <param name="user"></param>
        /// <returns> the number of rows added to the table User</returns>
        public Task<int> CreateUser(User user)
        {
            return db.InsertAsync(user);
        }
        public Task<int> UpdateUser(User user)
        {
            return db.UpdateAsync(user);
        }
        public Task<int> DeleteUser(User user)
        {
            return db.DeleteAsync(user);
        }
        public Task<List<User>> GetUsers()
        {
            return db.Table<User>().ToListAsync();
        }
        public Task<User> GetUser(int Id)
        {
            return db.GetAsync<User>(Id);
        }
        //Invigilators
        public Task<int> CreateInvigilator(Invigilator invigilator )
        {
            return db.InsertAsync(invigilator);
        }
        public Task<int> UpdateInvigilator(Invigilator invigilator)
        {
            return db.UpdateAsync(invigilator);
        }
        public Task<int> DeleteInvigilator(Invigilator invigilator)
        {
            return db.DeleteAsync(invigilator);
        }
        async public Task<List<Invigilator>> GetInvigilators(string Id)
        {
            int id = Convert.ToInt32(Id);
            Console.WriteLine("alert", $"{id}", "ok");
            return await db.Table<Invigilator>().Where(inv => inv.SessionId == id).OrderBy(inv => inv.RoomNumber).ToListAsync();
        }

        public Task<Invigilator> GetInvigilator(int Id)
        {
            return db.GetAsync<Invigilator>(Id);
        }
       
        // Session
        public Task<int> CreateSession(Session session)
        {
            return db.InsertAsync(session);
        }
        public Task<int> UpdateSession(Session session)
        {
            return db.UpdateAsync(session);
        }
        async public void DeleteSession(Session session)
        {
            List <Invigilator> list = await App.MyDB.GetInvigilators(session.Id.ToString());
            foreach (Invigilator inv in list)
            {
                await App.MyDB.DeleteInvigilator(inv);
            }
            await db.DeleteAsync(session);
        }

        public Task<Session> GetSession(int Id) 
        {
            return db.GetAsync<Session>(Id);
        }
        public Task<List<Session>> GetSessions()
        {
            return db.Table<Session>().ToListAsync();
        }

        async public  Task ExportToExcel(ExcelServices ExcelServices,string fileName, List<Invigilator> Invigilators, string date)
        {

            string filepath = ExcelServices.GenerateExcel(fileName, date);

            var data = new ExcelStructure
            {
                Headers = new List<string>() { "Salle", "Nom et Prenom", "Fonction", "Numero de telephone", "Observation" }
            };



            foreach (var item in Invigilators)
            {
                data.Values.Add(new List<string>() { item.RoomNumber.ToString(), item.FirstName + " " + item.SecondName, item.Function, item.PhoneNumber.ToString(), item.Observation });
            }

            ExcelServices.InsertDataIntoSheet(filepath, date, data);

            await Launcher.OpenAsync(new OpenFileRequest()
            {
                File = new ReadOnlyFile(filepath)
            });
        }


    }
}
