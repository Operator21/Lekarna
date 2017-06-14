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
    /// Interaction logic for AddContent.xaml
    /// </summary>
    public partial class AddContent : Page
    {
        Frame frame;
        Drug drug;
        Customer customer;
        bool Content;
        Random rnd = new Random();
        List<Ingredient> ingredients = new List<Ingredient>();
        List<DrugContent> selected = new List<DrugContent>();

        //Pro pridavani slozeni
        public AddContent(Frame f, Drug d)
        {
            InitializeComponent();
            drug = d;
            frame = f;
            list.ItemsSource = App.Database.GetIngredientsAsync().Result;
            /*selected = App.Database.GetSelected(drug.ID).Result;
            foreach(Ingredient i in ingredients)
            {
                foreach(DrugContent dc in selected)
                {
                    if(dc.drugID == i.ID)
                    {
                        list.SelectedItems.Add(dc);
                    }
                }
            }*/
            Content = true;
        }
        //Pro pridavani alergii
        public AddContent(Frame f, Customer c)
        {
            InitializeComponent();
            customer = c;
            frame = f;
            list.ItemsSource = App.Database.GetIngredientsAsync().Result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Content)
            {
                App.Database.EraseContent(drug.ID);
                foreach (Ingredient i in list.SelectedItems)
                {
                    DrugContent d = new DrugContent();
                    d.drugID = drug.ID;
                    d.contentID = i.ID;
                    App.Database.SaveDrugContentAsync(d);
                }
                frame.Navigate(new NewDrug(frame, drug));
            }
            else
            {
                App.Database.EraseAllergy(customer.ID);
                foreach (Ingredient i in list.SelectedItems)
                {
                    CustomerAllergy d = new CustomerAllergy();
                    d.AllergyID = i.ID;
                    d.CustomerID = customer.ID;
                    App.Database.AllergySave(d);
                }
                frame.Navigate(new NewPatient(frame, customer));
            }
            
        }

        private void add_ing_Click(object sender, RoutedEventArgs e)
        {
            Ingredient i = new Ingredient();
            i.Name = "ingredint" + rnd.Next(1,800000); ;
            App.Database.IngredientSave(i);
            Dispatcher.Invoke(Refresh);
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Ingredient o = ((Ingredient)list.SelectedItem);
            App.Database.Delete(o);
            Dispatcher.Invoke(Refresh);
        }
        private void Refresh()
        {
            list.ItemsSource = App.Database.GetIngredientsAsync().Result;
        } 
    }
}
