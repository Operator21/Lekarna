using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        List<CustomerAllergy> allergy_ids = new List<CustomerAllergy>();
        List<Ingredient> allergy = new List<Ingredient>();

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

            allergy_ids = App.Database.GetAllergyID(customer.ID).Result;
            int pom = 0;
            foreach(CustomerAllergy c in allergy_ids)
            {
                Debug.WriteLine(c.AllergyID);
                allergy = App.Database.GetAllergy(c.AllergyID).Result;
                foreach (Ingredient ing in allergy)
                {
                    Debug.WriteLine(ing.Name);
                    if (pom + 1 < allergy_ids.Count)
                    {
                        aler.Content += ing.Name + ", ";
                    }
                    else
                    {
                        aler.Content += ing.Name;
                    }
                    pom++;
                }
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
            var result = MessageBox.Show(customer.Name + " bude odstraněn", "Odstranit ?",MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                App.Database.Delete(customer);
                frame.Navigate(new CustomerList(frame));
            }
            
        }

        private void activate_Click(object sender, RoutedEventArgs e)
        {
            App.Database.AllDeactivate();
            Customer c = new Customer();
            c.ID = ID;
            c.Name = customer.Name;
            c.LastName = customer.LastName;
            //c.Allergies = customer.Allergies;
            c.Active = true;
            c.Address = customer.Address;
            c.BirthDate = customer.BirthDate;
            App.Database.SaveCustomerAsync(c);
            activate.IsEnabled = false;
        }
    }
}
