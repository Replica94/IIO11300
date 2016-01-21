/*
* Copyright (C) JAMK/IT/Esa Salmikangas
* This file is part of the IIO11300 course project.
* Created: 12.1.2016 Modified: 13.1.2016
* Authors: Tero ,Esa Salmikangas
*/
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


namespace JAMK.IT.IIO11300
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

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            double width;
            double height;
            double frame;
            double perimeter;
            double windowarea;
            double framearea;

            try
            {
                width = double.Parse(txtWidht.Text);
                height = double.Parse(txtHeight.Text);
                frame = double.Parse(txtFrameWidth.Text);
                perimeter = BusinessLogicWindow.CalculatePerimeter(width, height, frame);
               // double result;
               // result = BusinessLogicWindow.CalculatePerimeter(1, 1);
                //Ei näin: BusinessLogicWindow.CalculatePerimeter(1, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //yield to an user that everything okay
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown();// Sovellus(käynnissä oleva) suljetaan tällä komennolla
    }

        private void btnCalculateOO_Click(object sender, RoutedEventArgs e)
        {
            //oliona avulla pita-ala, piiri ja hinta
            //luodaan olio
            BusinessLogicWindow.Ikkuna ikk = new BusinessLogicWindow.Ikkuna();
            ikk.Korkeus = double.Parse(txtHeight.Text);
            ikk.Leveys = double.Parse(txtWidht.Text);
            //pinta-alan lakeminen kutsumalla metodia
            txtWindowArea.Text = ikk.LaskePintaAla().ToString;
            //pinta-ala on olion ominaisuus
            txtWindowArea.Text = ikk.PintaAla.ToString;
          
        }
    }

  
}
