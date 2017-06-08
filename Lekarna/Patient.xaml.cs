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
    /// Interakční logika pro Patient.xaml
    /// </summary>
    public partial class Patient : Page
    {
        Frame frame;
        Customer customer;
        Customer a;
        int ID;
        public Patient(Frame f, int i)
        {
            InitializeComponent();
            frame = f;
            ID = i;
            customer = App.Database.GetCustomerAsync(ID).Result;
            name.Content = customer.Name;
            last.Content = customer.LastName;
            //aler.Content = customer.Allergies;
            id.Content = ID;
            if (customer.Active)
            {
                activate.IsEnabled = false;
            } else
            {
                activate.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new CustomerList(frame));
        }

        private void edit_pat_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new NewPatient(frame,customer));
        }

        private void del_pat_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Pacient " + customer.Name + " byl odstraněn");
            App.Database.Delete(customer);
            frame.Navigate(new CustomerList(frame));
        }

        private void activate_Click(object sender, RoutedEventArgs e)
        {
            App.Database.AllDeactivate();
            Customer c = new Customer();
            c.ID = ID;
            c.Name = customer.Name;
            c.LastName = customer.LastName;
            c.Address = customer.Address;
            c.BirthDate = customer.BirthDate;
            //c.Allergies = customer.Allergies;
            c.Active = true;
            App.Database.SaveCustomerAsync(c);
            activate.IsEnabled = false;
        }
    }
}
