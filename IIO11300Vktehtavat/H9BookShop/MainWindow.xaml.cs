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

namespace H9BookShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetTestBooks_Click(object sender, RoutedEventArgs e)
        {
            dgBooks.DataContext = Bookshop.GetTestBooks();
        }

        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            spBooks.DataContext = dgBooks.SelectedItem;
        }

        private void btnGetServerBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //haetaan kirjat BL-kerroksesta
                dgBooks.DataContext = Bookshop.GetBooks(true);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //mistä tiedetään mitä muokata --> Book-olion ID:stä!
            try
            {
                Book current = (Book)spBooks.DataContext;
                if(Bookshop.UpdateBook(current) > 0)
                {
                    tbMessage.Text = string.Format("Kirja {0} päivitetty onnistuneesti", current.ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            if (btnNew.Content.ToString() == "Uusi")
            {
                //luodaan uusi kirja -olio
                Book newBook = new Book(0);
                newBook.Name = "Anna kirjan nimi";
                spBooks.DataContext = newBook;
                btnNew.Content = "Tallenna uusi kantaan";
            }
            else
            {
                //tallennetaa
                Book current = (Book)spBooks.DataContext;
                Bookshop.InsertBook(current);
                dgBooks.DataContext = Bookshop.GetBooks(true);
                tbMessage.Text = string.Format("Kirja {0} tallennettu kantaan onnistuneesti", current.ToString());
                btnNew.Content = "Uusi";
            }
           
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Book current = (Book)spBooks.DataContext;
                var retval = MessageBox.Show("Haluatko varmasti poistaa kirjan" + current.ToString(), "Wanhat kirjat kysyy", MessageBoxButton.YesNo);
                if (retval == MessageBoxResult.Yes)
                {
                    Bookshop.Deletebook(current);
                    dgBooks.DataContext = Bookshop.GetBooks(true);
                    tbMessage.Text = string.Format("Kirja poistettu");
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                    
            }
        }
    }
}
