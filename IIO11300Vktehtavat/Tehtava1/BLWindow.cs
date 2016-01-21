using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAMK.IT.IIO11300
{
    public class BusinessLogicWindow
    {
        public class Ikkuna
        {
            #region Muuttujat (variables)
            private double korkeus;
            private double leveys;
            private double pintaala;
            #endregion
            #region Ominaisuudet (properties)
            //oliosuunnittelun aikana on päätetty että pinta-ala ja hinta ovat read-only ominaisuuksia
            public double PintaAla
            {
                get
                {
                    return korkeus * leveys;
                }
            
            }
            public float Hinta
            {
                get
                {
                    return LaskeHinta();
                }
            }
            public double Korkeus
            {
                get
                {
                    return korkeus;
                }
                set
                {
                    //tässä kohdassa voisi tarvittaessa tehdä tarkistuksia
                    korkeus = value;
                }
            }
            public double Leveys
            {
                get
                {
                    return leveys;
                }
                set
                {
                    leveys = value;
                }
            }
            private float LaskeHinta()
            {
                //hinta lasketaan työn, materiaalimenekin ja katteen mukaan
                float kate = 100;
                float tyo = 200;
                float materiaali;
                materiaali = 100 * (float)(leveys * korkeus / 100000);
                return (kate + tyo + materiaali);
            }
        
            #endregion
            #region Konstruktorit (constructors)
            #endregion
            #region Metodit (methods)

            public double LaskePintaAla()
            {
                return korkeus * leveys;
            }
            #endregion
        }
        public class IkkunaVE0
        {
            //joskus tehdään näin "oikaistaan"
            //ei suositella by Esa
            public double korkeus;
            public double leveys;
            public double LaskePintaAla()
            {
                return korkeus * leveys;
            }
        }
        /// <summary>
        /// CalculatePerimeter calculates the perimeter of a window
        /// </summary>
        public static double CalculatePerimeter(double width, double height, double frame)
        {
            throw new System.NotImplementedException();//moi
        }
    }
}
