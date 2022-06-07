using System;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            getWeather();
        }

        double lon;
        double lat;

        void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?&q={0}&appid={1}", TBCity.Text, APIKey);
                var json = web.DownloadString(url);

                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                labCondition.Text = Info.weather[0].description;
                labSunset.Text = convertDateTime(Info.sys.sunset).ToShortTimeString();
                labSunrise.Text = convertDateTime(Info.sys.sunrise).ToShortTimeString();

                labWindSpeed.Text = Info.wind.speed.ToString() + " m/s";
                labPressure.Text = Info.main.pressure.ToString();

                lon = Info.coord.lon;
                lat = Info.coord.lat;
            }
        }

        DateTime convertDateTime(long sec)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToUniversalTime();
            day = day.AddSeconds(sec).ToLocalTime();

            return day;
        }

        ////////////////////////////////////////////////////////////////////////

        void getForcecast()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}" + "&lon=" + "{1}" + "&exclude=hourly,daily&appid={2}", lat, lon, APIKey);
                var json = web.DownloadString(url);
                

            }
        }

    }
}
