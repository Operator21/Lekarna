using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lekarna
{
    /// <summary>
    /// Interakční logika pro Cart.xaml
    /// </summary>
    public partial class Cart : Page
    {
        List<Order> list_orders = new List<Order>();
        public Cart()
        {
            InitializeComponent();
            list_orders = App.Database.GetOrdersAsync().Result;
            orders.ItemsSource = list_orders;
        }
    }
}
