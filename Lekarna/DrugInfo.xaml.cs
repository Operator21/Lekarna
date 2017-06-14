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
    /// Interakční logika pro DrugInfo.xaml
    /// </summary>
    public partial class DrugInfo : Page
    {
        List<Drug> getdrug = new List<Drug>();
        List<Drug> drugs = new List<Drug>();
        List<Ingredient> ingredients = new List<Ingredient>();
        List<DrugContent> dcontent = new List<DrugContent>();
        List<CustomerAllergy> allergy_ids = new List<CustomerAllergy>();
        List<Ingredient> allergy = new List<Ingredient>();
        List<string> writec = new List<string>();
        Customer customer;
        Drug drug;
        int ID;
        string dname;
        Frame frame;
        bool show;
        bool danger;
        bool canbuy;
        public DrugInfo(int x, Frame f, List<Drug> d)
        {
            InitializeComponent();
            ID = x;
            drugs = d;
            frame = f;

            customer = App.Database.GetActive().Result;
            if (customer == null)
            {
                buy.IsEnabled = false;
            }

            getdrug = App.Database.GetItem(ID).Result;
            foreach (Drug t in getdrug)
            {
                drug = t;
                id.Content = t.ID;
                name.Content = t.Name;
                dname = t.Name;
                price.Content = t.Price;

            }
            dcontent = App.Database.GetIngredientID(drug.ID).Result;
            int pom = 0;
            if (customer != null)
            {
                allergy_ids = App.Database.GetAllergyID(customer.ID).Result;
                foreach (CustomerAllergy c in allergy_ids)
                {
                    Debug.WriteLine(c.AllergyID);
                    allergy = App.Database.GetAllergy(c.AllergyID).Result;
                    foreach (Ingredient aler in allergy)
                    {
                        //Debug.WriteLine(aler.Name);
                        foreach (DrugContent dr in dcontent)
                        {
                            Debug.WriteLine(dr.contentID);
                            ingredients = App.Database.GetAllergy(dr.contentID).Result;
                            foreach (Ingredient ing in ingredients)
                            {
                                //Debug.WriteLine(ing.Name);
                                if (aler.Name == ing.Name)
                                {
                                    danger = true;
                                }
                            }
                        }
                    }
                }
                foreach (DrugContent dr in dcontent)
                {
                    Debug.WriteLine(dr.contentID);
                    ingredients = App.Database.GetAllergy(dr.contentID).Result;
                    foreach (Ingredient ing in ingredients)
                    {
                        //Debug.WriteLine(ing.Name);
                        if (pom + 1 < dcontent.Count())
                        {
                            content.Content += ing.Name + ", ";
                        }
                        else
                        {
                            content.Content += ing.Name;
                        }
                        pom++;
                    }
                }
            }
            else
            { 
                foreach (DrugContent dr in dcontent)
                {
                    Debug.WriteLine(dr.contentID);
                    ingredients = App.Database.GetAllergy(dr.contentID).Result;
                    foreach (Ingredient ing in ingredients)
                    {
                        //Debug.WriteLine(ing.Name);
                        if (pom + 1 < dcontent.Count())
                        {
                            content.Content += ing.Name + ", ";
                        }
                        else
                        {
                            content.Content += ing.Name;
                        }
                        pom++;
                    }
                }
            }
            
            /*foreach (Drug r in getdrug)
            {
                drug = r;
            }*/

            /*ingredients = App.Database.GetIngredientsAsync().Result;
            dcontent = App.Database.GetDrugContentAsync().Result;

            var query = from Ingredient in ingredients
                        join DrugContent in dcontent on Ingredient.ID equals DrugContent.contentID
                        join Drug in getdrug on DrugContent.drugID equals Drug.ID
                        select new { Name = Drug.Name, Ing = Ingredient.Name };

            /*var c_query = from Ingredient in ingredients
                        join DrugContent in dcontent on Ingredient.ID equals DrugContent.contentID
                        join Drug in getdrug on DrugContent.drugID equals Drug.ID
                        select new { Name = Drug.Name, Ing = Ingredient.Name };
            */
            /*int pom = 0;
            foreach (var dr in query)
            {
                Debug.WriteLine(dr.Name + " je složen z " + dr.Ing);
                if(pom+1 < query.Count())
                {
                    content.Content += dr.Ing + ", ";
                }
                else
                {
                    content.Content += dr.Ing;
                }
                
                dname = dr.Name;
                pom++;
            }*/

            
            /*try
            {
                allergies = active.Allergies.Split(',').Reverse().ToList<string>();
            }
            catch
            {
                warning_cus.Visibility = Visibility.Visible;
            }*/

            //danger = allergies.Intersect(ingredients).Any();
            if (danger)
            {
                warning.Visibility = Visibility.Visible;
            }
            else
            {
                warning.Visibility = Visibility.Collapsed;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new DrugList(frame));
        }

        private void edit_drug_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new NewDrug(frame,drug));
        }

        private void del_drug_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Lék " + dname + " byl odstraněn");
            App.Database.Delete(drug);
            frame.Navigate(new DrugList(frame));
        }

        private void buy_Click(object sender, RoutedEventArgs e)
        {
            if (danger)
            {
                var result = MessageBox.Show("Jste si jist ? Zákazník může mít alergickou reakci.","",MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Order o = new Order();
                        o.Amount = 1;
                        o.Price = o.Amount * drug.Price;
                        o.DrugName = drug.Name;
                        o.CustomerID = customer.ID;
                        o.ProductID = drug.ID;
                        App.Database.SaveItemAsync(o);
                        //MessageBox.Show("Saved");
                        break;
                }
            }
            else
            {
                Order o = new Order();
                o.Amount = 1;
                o.Price = o.Amount * drug.Price;
                o.DrugName = drug.Name;
                o.CustomerID = customer.ID;
                o.ProductID = drug.ID;
                App.Database.SaveItemAsync(o);
            }
        }
    }
}
