using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lekarna
{
    public class CustomerAllergy
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int AllergyID { get; set; }
        public int CustomerID { get; set; }
    }
}
