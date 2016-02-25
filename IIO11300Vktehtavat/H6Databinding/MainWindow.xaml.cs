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

namespace H6Databinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HockeyLeague smliiga;
        List<HockeyTeam> liigajoukkueet;
        int osoitin;
        public MainWindow()
        {
            InitializeComponent();
            AlustaKontrollit();
        }
        private void AlustaKontrollit()
        {
            //Täytetään combobox listan alkioilla
            List<string> joukkueet = new List<string>();
            joukkueet.Add("Ilves");
            joukkueet.Add("JYP");
            joukkueet.Add("Kärpät");
            cbTeams.ItemsSource = joukkueet;
            //perustetaan SMLiiga
            smliiga = new HockeyLeague();
            liigajoukkueet = smliiga.GetTeams();

        }
        private void btnGetSettings_Click(object sender, RoutedEventArgs e)
        {
            //koodi lukee App.Configin asetuksen UserName
            btnGetSettings.Content = H6Databinding.Properties.Settings.Default.UserName;
            
        }

        private void btnBind_Click(object sender, RoutedEventArgs e)
        {
            //sidotaan kokoelma stackpanelin datakontekstiksi
            spLiiga.DataContext = liigajoukkueet;
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            //siirretään osoitinta kokoelmassa yhdellä eteenpäin
            osoitin++;
            spLiiga.DataContext = liigajoukkueet[osoitin];
        }

        private void btwBackward_Click(object sender, RoutedEventArgs e)
        {
            osoitin--;
            spLiiga.DataContext = liigajoukkueet[osoitin];
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            //lisätään kokoelmaan uusi joukkue
            HockeyTeam uusi = new HockeyTeam();
            liigajoukkueet.Add(uusi);
            osoitin = liigajoukkueet.Count - 1;
            spLiiga.DataContext = liigajoukkueet[osoitin];
        }
    }
}
