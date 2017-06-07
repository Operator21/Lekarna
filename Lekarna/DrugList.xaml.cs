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
    /// Interakční logika pro DrugList.xaml
    /// </summary>
    public partial class DrugList : Page
    {
        List<Drug> drugs = new List<Drug>();
        Frame frame;
        int id;
        public DrugList(Frame f)
        {
            InitializeComponent();
            drugs = new List<Drug>(App.Database.GetItemsAsync().Result);
            list.ItemsSource = drugs;
            frame = f;
        }
        public DrugList()
        {
            InitializeComponent();
        }
        private void ItemSelected(object sender, RoutedEventArgs e)
        {
            id = ((Drug)list.SelectedItem).ID;
            frame.Navigate(new DrugInfo(id, frame, drugs));
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            Drug item = new Drug();
            App.Database.SaveItemAsync(item);
        }

        private void add_drug_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new NewDrug(frame));
        }
    }
}
