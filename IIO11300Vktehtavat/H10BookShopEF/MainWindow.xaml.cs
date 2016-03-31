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
using System.Data.Entity;
using System.Collections.ObjectModel; //for ObservableCollection
using System.ComponentModel;

namespace H10BookShopEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BookShopEntities ctx;
        ObservableCollection<Book> localBooks;
        ICollectionView view; //filtteröintiä varten
        Boolean isBooks;
        public MainWindow()
        {
            InitializeComponent();
            IniMyStuff();
        }

        private void IniMyStuff()
        {
            //tänne kaikki tarvittavat alustukset
            ctx = new BookShopEntities();
            ctx.Books.Load();
            localBooks = ctx.Books.Local;
            //comboboxin täyttäminen eri mailla
            cbCountries.DataContext = localBooks.Select(n => n.country).Distinct();
            // view kirjojen filtteröintiä varten
            view = CollectionViewSource.GetDefaultView(localBooks);
        }
        private void btnGetCustomers_Click(object sender, RoutedEventArgs e)
        {
            
            var customers = ctx.Customers.ToList();
            dgBooks.DataContext = customers;
            isBooks = false;
            
            
            
        }

        private void btnGetEFBooks_Click(object sender, RoutedEventArgs e)
        {
            dgBooks.DataContext = localBooks;
            isBooks = true;
            cbCountries.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //tallennetaan kirjaan tehdyt muutokset kantaan
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            //luodaan uusi kirja-olio ensin kontekstiin ja sitten kantaan
            Book newBook;
            if (btnNew.Content.ToString() == "Uusi")
            {
                //luodaan uusi book-olio
                newBook = new Book();
                newBook.name = "Anna kirjan nimi";
                spBooks.DataContext = newBook;
                btnNew.Content = "Tallenna kantaan";
            }
            else
            {
                //lisätään uusi kirja ensin kontekstiin ja sieltä kantaan
                newBook = (Book)spBooks.DataContext;
                ctx.Books.Add(newBook);
                ctx.SaveChanges();
                btnNew.Content = "Uusi";
                MessageBox.Show("Kirja tallennettu");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //poistetaan valittu kirja-olio kontekstista ja sitten kannasta
            Book current = (Book)spBooks.DataContext;
            var retval = MessageBox.Show("Haluatko poistaa kirjan " + current.DisplayName + "?", "Wanhat kirjat kysyy", MessageBoxButton.YesNo);
            if (retval == MessageBoxResult.Yes)
            {
                ctx.Books.Remove(current);
                ctx.SaveChanges();
            }
        }

        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //valittu item (tässä tapauksessa Customer-olio) asetetaan stackpanelin datakontekstiksi
            if (isBooks)
                spBooks.DataContext = dgBooks.SelectedItem;
            else
            spCustomer.DataContext = dgBooks.SelectedItem;
        }

        private void btnGetOrders_Click(object sender, RoutedEventArgs e)
        {
            //haetaan valitun asiakkaan tilaukset navigation properties avulla
            string msg = "";
            Customer current = (Customer)spCustomer.DataContext;
            msg += string.Format("Asiakkaalla {0} on {1} tilausta:\n", current.DisplayName, current.OrderCount);
            foreach (var item in current.Orders)
            {
                msg += string.Format("Tilaus {0} sisältää {1} tilausriviä:\n", item.odate, item.Orderitems.Count);
                //kunkin tilauksen rivit ja sitä vastaava kirja
                Decimal summa = 0;
                foreach (var item2 in item.Orderitems)
                {
                    msg += string.Format("- kirja: {0} : {1} kappaletta\n", item2.Book.name, item2.count);
                    summa += item2.count * item2.Book.price.Value;
                    
                }
                msg += string.Format("-- Tilaus yhteensä: {0}\n", summa);
            }
            MessageBox.Show(msg);
        }

        private void cbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //suodatetaan kirjat
            //suodatus tehdään ns. predikaatti-funktiolla
            view.Filter = MyCountryFilter;
        }
        private bool MyCountryFilter(object item)
        {
            if (cbCountries.SelectedIndex == -1)
                return true;
            else
                return (item as Book).country.Contains(cbCountries.SelectedItem.ToString());
        }
    }
}
