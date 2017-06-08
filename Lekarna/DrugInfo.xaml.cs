﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        List<string> writec = new List<string>();
        Customer active;
        Drug drug;
        int ID;
        string dname;
        Frame frame;
        bool show;
        bool danger;
        public DrugInfo(int x, Frame f, List<Drug> d)
        {
            InitializeComponent();
            ID = x;
            drugs = d;
            frame = f;

            getdrug = App.Database.GetItem(ID).Result;

            foreach (Drug r in getdrug)
            {
                drug = r;
            }

            ingredients = App.Database.GetIngredientsAsync().Result;
            dcontent = App.Database.GetDrugContentAsync().Result;

            var query = from Ingredient in ingredients
                        join DrugContent in dcontent on Ingredient.ID equals DrugContent.contentID
                        join Drug in getdrug on DrugContent.drugID equals Drug.ID
                        select new { Name = Drug.Name, Ing = Ingredient.Name, Price = Drug.Price };

            
            int count = query.Count() - 1;
            foreach (var dr in query)
            {
                Debug.WriteLine(dr.Name + " je složen z " + dr.Ing);
                if(count >= 1)
                {
                    content.Content += CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr.Ing.ToLower()) + ", ";
                }
                else
                {
                    content.Content += CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr.Ing.ToLower());
                }
                name.Content = dr.Name;
                price.Content = dr.Price;
                dname = dr.Name;
                count--;
            }
            active = App.Database.GetActive().Result;
            /*foreach (Drug t in getdrug)
            {
                drug = t;
                id.Content = t.ID;
                name.Content = t.Name;
                dname = t.Name;
                price.Content = t.Price;
                
            }*/
            /*try
            {
                allergies = active.Allergies.Split(',').Reverse().ToList<string>();
            }
            catch
            {
                warning_cus.Visibility = Visibility.Visible;
            }*/

            /*danger = allergies.Intersect(ingredients).Any();
            if (danger)
            {
                warning.Visibility = Visibility.Visible;
            }
            else
            {
                warning.Visibility = Visibility.Collapsed;
            }*/
            /*var c_query = from Ingredient in ingredients
                        join DrugContent in dcontent on Ingredient.ID equals DrugContent.contentID
                        join Drug in getdrug on DrugContent.drugID equals Drug.ID
                        select new { Name = Drug.Name, Ing = Ingredient.Name };
            */
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
            MessageBox.Show(dname + " byl odstraněn");
            App.Database.Delete(drug);
            frame.Navigate(new DrugList(frame));
        }
    }
}