using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lekarna
{
    public class DrugContent
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int drugID { get; set; }
        public int contentID { get; set; }
    }
}
