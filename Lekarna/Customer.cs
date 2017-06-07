using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lekarna
{
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        //public string Allergies { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
    }
}
