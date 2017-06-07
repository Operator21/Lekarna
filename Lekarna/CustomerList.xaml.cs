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
    /// Interakční logika pro CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Page
    {
        Frame frame;
        int id;
        List<Customer> cu = new List<Customer>();
        public CustomerList(Frame f)
        {
            InitializeComponent();
            cu = App.Database.GetCustomersAsync().Result;
            list.ItemsSource = cu;
            frame = f;
        }
        /*public CustomerList()
        {
            InitializeComponent();
        }*/

        private void ItemSelected(object sender, RoutedEventArgs e)
        {
            id = ((Customer)list.SelectedItem).ID;
            frame.Navigate(new Patient(frame, id));
        }

        private void add_pat_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new NewPatient(frame));
        }
        private void Search(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(search.Text))
            {
                cu = App.Database.GetLike(search.Text).Result;
                list.ItemsSource = cu;
            }
            else
            {
                cu = App.Database.GetCustomersAsync().Result;
                list.ItemsSource = cu;
            }

        }
    }
}
