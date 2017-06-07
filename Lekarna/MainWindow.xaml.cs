using FontAwesome.WPF;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private ObservableCollection<Drug> itemsFromDb;
        private ObservableCollection<Customer> customersDb;
        public List<Drug> items = new List<Drug>();
        public List<Drug> drugs = new List<Drug>();
        public List<Customer> customers = new List<Customer>();
        string random;
        bool isempty;
        int r;
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
            drugs = new List<Drug>(App.Database.GetItemsAsync().Result);
            customers = new List<Customer>(App.Database.GetCustomersAsync().Result);
            warning_text.Visibility = Visibility.Collapsed;
            warning.Visibility = Visibility.Collapsed;
            for (int x = 0; x < 10; x++)
            /*{
                Drug item = new Drug();
                item.Name = "Drug" + x;
                r = rnd.Next(1,5);
                switch (r)
                {
                    case 1:
                        random = "Wood";
                        break;
                    case 2:
                        random = "Bees";
                        break;
                    case 3:
                        random = "Coal";
                        break;
                    case 4:
                        random = "Stone";
                        break;
                    case 5:
                        random = "Water";
                        break;
                }
                item.Content = random;
                App.Database.SaveItemAsync(item);
            }*/
            frame.Navigate(new DrugList(frame));
            dlist.IsEnabled = false;
            /*Customer item = new Customer();
            item.Name = "Tonda";
            item.LastName = "Vondru";
            item.Allergies = "Citron, Merunka";
            App.Database.SaveCustomerAsync(item);*/
            /*isempty = !customers.Any();
            if (isempty)
            {
                warning.Visibility = Visibility.Visible;
            }
            else
            {
                //warning.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Flag, Brushes.Black);
            }*/
        }

        private void dlist_Click(object sender, RoutedEventArgs e)
        {
            clist.IsEnabled = true;
            dlist.IsEnabled = false;
            cart.IsEnabled = true;
            frame.Navigate(new DrugList(frame));
        }

        private void clist_Click(object sender, RoutedEventArgs e)
        {
            dlist.IsEnabled = true;
            clist.IsEnabled = false;
            cart.IsEnabled = true;
            frame.Navigate(new CustomerList(frame));
        }
        private void Warning_info(object sender, RoutedEventArgs e)
        {
            warning_text.Visibility = Visibility.Visible;
        }
        private void Warning_hide(object sender, RoutedEventArgs e)
        {
            warning_text.Visibility = Visibility.Collapsed;
        }

        private void cart_Click(object sender, RoutedEventArgs e)
        {
            dlist.IsEnabled = true;
            clist.IsEnabled = true;
            cart.IsEnabled = false;
            frame.Navigate(new Cart());
        }
    }
}
