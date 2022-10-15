using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTaking.Models
{
    [Table("Invigilators")]
    public class Invigilator
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int RoomNumber { get; set; }
        public string Function { get; set; }
        public int PhoneNumber { get; set; }
        public string Observation { get; set; }

        [ForeignKey(typeof(Session))]
        public int SessionId { get; set; }

        [ManyToOne]
        public Session Session { get; set; }
        
    }
}
