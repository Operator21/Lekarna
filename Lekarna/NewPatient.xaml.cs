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
    /// Interaction logic for NewPatient.xaml
    /// </summary>
    public partial class NewPatient : Page
    {
        Frame frame;
        bool edit;
        Customer customer;
        int ID;

        public NewPatient(Frame f)
        {
            InitializeComponent();
            frame = f;
            edit = false;
            cd_button.Content = "Vytvořit";
        }
        public NewPatient(Frame f, Customer c)
        {
            InitializeComponent();
            frame = f;
            customer = c;
            edit = true;
            ID = c.ID;
            name.Text = c.Name;
            last.Text = c.LastName;
            //aler.Text = c.Allergies;
            cd_button.Content = "Upravit";
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (edit)
            {
                frame.Navigate(new Patient(frame,ID));
            }
            else
            {
                frame.Navigate(new CustomerList(frame));
            }
            
        }

        private void cd_button_Click(object sender, RoutedEventArgs e)
        {
            Customer c = new Customer();
            if (edit)
            {
                c.ID = ID;
            }
            c.Name = name.Text;
            c.LastName = last.Text;
            //c.Allergies = aler.Text;
            App.Database.SaveCustomerAsync(c);
            if (edit)
            {
                frame.Navigate(new Patient(frame,ID));
            }
            else
            {
                frame.Navigate(new CustomerList(frame));
            }
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            base.OnPreviewKeyDown(e);

        }
    }
}
