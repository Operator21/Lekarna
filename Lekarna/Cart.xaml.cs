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
        Frame frame;
        int price;
        public Cart(Frame f)
        {
            InitializeComponent();
            list_orders = App.Database.GetOrdersAsync().Result;
            foreach(Order o in list_orders)
            {
                price += o.Price;
            }
            orders.ItemsSource = list_orders;
            frame = f;
            if(price < 1)
            {
                pay.IsEnabled = false;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Order o = ((Order)orders.SelectedItem);
            App.Database.Delete(o);
            price -= o.Price;
            price_lbl.Content = price + " Kč";
            Dispatcher.Invoke(Refresh);
        }
        private void Refresh()
        {
            list_orders = App.Database.GetOrdersAsync().Result;
            orders.ItemsSource = list_orders;
            if (price < 1)
            {
                pay.IsEnabled = false;
            }
        }

        private void pay_Click(object sender, RoutedEventArgs e)
        {
            price_lbl.Content = price + " Kč";
        }
    }
}
