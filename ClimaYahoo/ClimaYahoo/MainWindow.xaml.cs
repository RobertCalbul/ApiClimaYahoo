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
using System.Xml;

namespace ClimaYahoo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Load_Clima();
        }
        //METODO QUE CARGARA CLIMA
        public void Load_Clima() {
            XmlDocument pagina = new XmlDocument();//INSTANCIA XML
            pagina.Load("http://weather.yahooapis.com/forecastrss?w=349871&u=c");//LEE LA PAGINA (API)

            XmlNamespaceManager man = new XmlNamespaceManager(pagina.NameTable);
            man.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");//AGREGA DIRECCION 
            String[] diasEng = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            String[] diasEs = { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" };

            XmlNode chanel = pagina.SelectSingleNode("rss").SelectSingleNode("channel");//OBTIENE LAS ETIQUETAS
            //OBTIENE LOS DATOS DE ACUERDO A LA API
            String build = chanel.SelectSingleNode("lastBuildDate").InnerText;
            String cuidad = chanel.SelectSingleNode("yweather:location", man).Attributes["city"].Value;
            String region = chanel.SelectSingleNode("yweather:location", man).Attributes["region"].Value;
            String country = chanel.SelectSingleNode("yweather:location", man).Attributes["country"].Value;
            String temperature = chanel.SelectSingleNode("yweather:units", man).Attributes["temperature"].Value;
            String chill = chanel.SelectSingleNode("yweather:wind", man).Attributes["chill"].Value;
            String humidity = chanel.SelectSingleNode("yweather:atmosphere", man).Attributes["humidity"].Value;
            String sunrise = chanel.SelectSingleNode("yweather:astronomy", man).Attributes["sunrise"].Value;
            String sunset = chanel.SelectSingleNode("yweather:astronomy", man).Attributes["sunset"].Value;
            String cdata = chanel.SelectSingleNode("item", man).SelectSingleNode("description").InnerText;//.InnerText;

            //SETEO DE DATOS A PANTALLA
            this.lCity.Content = cuidad + ", " + country;//CIUDAD, PAIN
            this.lDate.Content = build.Substring(0, 3) + ", " + build.Substring(16, 5);//CARGA EL DIA EN ESPAÑOL
            this.lDate.Content += "\n" + humidity;
            this.lDate.Content += "\n" + sunrise;
            this.lDate.Content += "\n" + sunset;
            this.lTemperature.Content = chill + "º" + temperature;//CARCA LA TEMPERATURA ACTUAL
            string imgTiempo = cdata.Substring(cdata.Substring(11, 40).Replace("\"/>", "").Length + 5, 2).Replace("\"/>", "");//OBTIENE LA IMAGEN DEL TIEMPO
            Console.WriteLine("><<"+imgTiempo);
            this.lImageTiempo.Source = new BitmapImage(new Uri(string.Format("https://s.yimg.com/os/mit/media/m/weather/images/icons/l/{0}n-100567.png", imgTiempo)));//CARGA IMAGEN DEL TIEMPO
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();//SE PUEDE MOVER LA PANTALLA
        }
    }
}
