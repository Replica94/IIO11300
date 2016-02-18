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
using System.Windows.Shapes;
using System.Xml;

namespace H5movies
{
    /// <summary>
    /// Interaction logic for Movies2.xaml
    /// </summary>
    public partial class Movies2 : Window
    {
        public Movies2()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //haetaan xmlDataProviderin XML-tiedoston nimi
            string filu = xdpMovies.Source.LocalPath;
            //tallennetaan XmlDocument XML-tiedoston päälle!
            xdpMovies.Document.Save(filu);
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            //Asetetaan textboxit viittaamaan pois datasta eli listasta ei ole valittuna mitään
            if (lbMovies.SelectedIndex >= 0)
            {
                lbMovies.SelectedIndex = -1;
                btnCreate.Content = "Tallenna";
            }
            else
            {
                try
                {
                    //Lisätään uusi noodi XMLDocumenttiin
                    //haetaan xmlDataProviderin XML-tiedoston nimi
                    string filu = xdpMovies.Source.LocalPath;
                    //Viittaus xml-dokumenttiin ja sen juurielementtiin
                    XmlDocument doc = xdpMovies.Document;
                    XmlNode root = doc.SelectSingleNode("/Movies");
                    //Luodaan uusi node
                    XmlNode newMovie = doc.CreateElement("Movie");
                    //Lisätään nodelle tarvittavat neljä attribuuttia
                    XmlAttribute xa1 = doc.CreateAttribute("Name");
                    xa1.Value = txtName.Text;
                    newMovie.Attributes.Append(xa1);
                    XmlAttribute xa2 = doc.CreateAttribute("Director");
                    xa2.Value = txtDirector.Text;
                    newMovie.Attributes.Append(xa2);
                    XmlAttribute xa3 = doc.CreateAttribute("Country");
                    xa3.Value = txtCountry.Text;
                    newMovie.Attributes.Append(xa3);
                    XmlAttribute xa4 = doc.CreateAttribute("Checked");
                    xa4.Value = chkChecked.IsChecked.ToString();
                    newMovie.Attributes.Append(xa4);
                    //TODO loput attribuutit
                    //Lisätään noodi juureen
                    root.AppendChild(newMovie);
                    //tallennetaan XmlDocument XML-tiedoston päälle!
                    xdpMovies.Document.Save(filu);
                    //kaikki ok
                    MessageBox.Show("Uusi elokuva lisätty onnistuneesti");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    btnCreate.Content = "Lisää uusi";
                }
                
                    

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //poistetaan käyttäjän valitsema elokuva --> poistettava Node haetaan Name-attribuutin avulla
            try
            {
                string filu = xdpMovies.Source.LocalPath;
                XmlDocument doc = xdpMovies.Document;
                XmlNode root = doc.SelectSingleNode("/Movies");
                XmlNode poistettava = null;
                //haetaan xpathilla poistettava node
                var item = doc.SelectSingleNode(string.Format("/Movies/Movie[@Name='{0}']", txtName.Text));
                if (item != null && MessageBox.Show("Poistetaanko elokuva? " + txtName.Text, "Elokuvagalleria", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    poistettava = item;
                }
                if (poistettava != null)
                {
                    //poistetaan noodi juuresta
                    root.RemoveChild(poistettava);
                    xdpMovies.Document.Save(filu);
                    //listboxin osoitin
                    lbMovies.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }
    }
}
