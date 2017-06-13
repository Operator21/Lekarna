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
    /// Interakční logika pro NewDrug.xaml
    /// </summary>
    public partial class NewDrug : Page
    {
        List<Drug> drugs = new List<Drug>();
        List<Ingredient> ing = new List<Ingredient>();
        List<string> drugcontent = new List<string>();
        Frame frame;
        Ingredient check;
        Drug drug;
        int ID;
        bool edit;
        int lastId;
        public NewDrug(Frame f, Drug d)
        {
            InitializeComponent();
            edit = true;
            drug = d;
            cd_button.Content = "Upravit";
            frame = f;
            ID = d.ID;
            name.Text = d.Name;
            price.Text = d.Price.ToString();
            ing = App.Database.GetIngredientsAsync().Result;
        }
        public NewDrug(Frame f)
        {
            InitializeComponent();
            cd_button.Content = "Vytvořit";
            frame = f;
            edit = false;
            ing = App.Database.GetIngredientsAsync().Result;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (edit)
            {
                frame.Navigate(new DrugInfo(ID, frame, drugs));
            }
            else
            {
                frame.Navigate(new DrugList(frame));
            }
            
        }

        private void Cd_button_Click(object sender, RoutedEventArgs e)
        {
            Drug d = new Drug();
            if (edit)
            {
                d.ID = ID;
            }
            d.Name = name.Text;

            try
            {
                d.Price = Int32.Parse(price.Text);
                App.Database.SaveItemAsync(d);
                //List<string> drugcontent = content.Text.Split(',').Reverse().ToList<string>();
                ing = App.Database.GetIngredientsAsync().Result;
                foreach (string s in drugcontent)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        /*List<Ingredient> exist = ing.Where(p => p.Name == s).ToList();
                        foreach(Ingredient ig in exist)
                        {
                            MessageBox.Show(ig.Name);
                        }*/
                        //MessageBox.Show(s);
                        Ingredient i = new Ingredient();
                        i.Name = s.ToUpper();
                        App.Database.IngredientSave(i);
                        GetId(d);

                        //CustomerAllergy c = new CustomerAllergy();
                        //c.AllergyID
                    }        
                }


                if (edit)
                {
                    frame.Navigate(new DrugInfo(ID, frame, drugs));
                }
                else
                {
                    frame.Navigate(new DrugList(frame));
                }
            }
            catch
            {
                MessageBox.Show("Hodnota byla špatně zadána");
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
        private async void GetId(Drug d)
        {
            long lastIdLong = await App.Database.GetLastID();
            int lastId = (Int32)lastIdLong;
            MessageBox.Show(lastId.ToString());

            DrugContent c = new DrugContent();
            c.drugID = d.ID;
            c.contentID = lastId;
            await App.Database.DrugIngredientSave(c);
        }

        private void content_Click(object sender, RoutedEventArgs e)
        {
            if (edit)
            {
                frame.Navigate(new AddContent(frame, drug));
            }
            else
            {
                MessageBox.Show("Nejdříve musí být lék vytvořen");
            }
            
        }
    }
}
