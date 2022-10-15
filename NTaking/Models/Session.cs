using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTaking.Models
{
    [Table("Sessions")]
    public class Session
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [OneToMany]
        public List<Invigilator> Invigilators { get; set; }

        
    }
}
