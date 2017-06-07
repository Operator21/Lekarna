using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lekarna
{
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Amount { get; set; }
        public int CustomerID { get; set; }
        public bool Ordered { get; set; }
    }
}
