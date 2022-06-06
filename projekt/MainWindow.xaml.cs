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
using System.Net;
using Newtonsoft.Json;
using System.Globalization;

namespace projekt
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

        string APIKey = "f06f748cb174f38b78906946a08bab92";

        void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?&q={0}&appid={1}", TBCity.Text, APIKey);
                var json = web.DownloadString(url);

                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                labCondition.Text = Info.weather[0].description;
                labSunset.Text = Info.sys.sunset.ToString();
                labSunrise.Text = Info.sys.sunrise.ToString();

                labWindSpeed.Text = Info.wind.speed.ToString() + " m/s";
                labPressure.Text = Info.main.pressure.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            getWeather();
        }

    }
}
