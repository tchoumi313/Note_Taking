using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTaking.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string PictureUrl { get; set; }
    }
}
