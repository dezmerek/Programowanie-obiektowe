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
            getForcecast();
        }

        double lon;
        double lat;

        void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?&units=metric&q={0}&appid={1}", TBCity.Text, APIKey);
                var json = web.DownloadString(url);

                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                labCondition.Text = Info.weather[0].description;
                //labSunset.Text = convertDateTime(Info.sys.sunset).ToShortTimeString();
                //labSunrise.Text = convertDateTime(Info.sys.sunrise).ToShortTimeString();

                Temp.Text = Info.main.temp.ToString() + "°C";
                TempMax.Text = Info.main.temp_max.ToString() + "°C";
                TempMin.Text = Info.main.temp_min.ToString() + "°C";
                Humidity.Text = Info.main.humidity.ToString() + "%";
                labWindSpeed.Text = Info.wind.speed.ToString() + "/kph";
                labPressure.Text = Info.main.pressure.ToString() + " hPa";
                FeelsLike.Text = Info.main.feels_like.ToString() + "°C";

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
                string url = string.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}" + "&lon=" + "{1}" + "&exclude=current,minutely,hourly,alerts&appid={2}", lat, lon, APIKey);
                var json = web.DownloadString(url);
                
                WeatherForecast.ForecastInfo forecastInfo = JsonConvert.DeserializeObject<WeatherForecast.ForecastInfo>(json);

                //ForecastUC FUC;
                //for (int i = 0; 1 < 8; i++)
                //{
                //    FUC = new ForecastUC();
                //    FUC.labMainWeather.Text = forecastInfo.daily[i].weather[0].main;
                //    FUC.labWeatherDescription.Text = forecastInfo.daily[i].weather[0].description;
                //    FUC.labDT.Text = convertDateTime(forecastInfo.daily[i].dt).DayOfWeek.ToString();

                //    FLP.Controls.Add(FUC);
                //}

                //labMainWeather.Text = forecastInfo.daily[1].weather[0].main;
                //labWeatherDescription.Text = forecastInfo.daily[1].weather[0].description;
                //labDT.Text = convertDateTime(forecastInfo.daily[1].dt).DayOfWeek.ToString();

                string ImageUrl = "http://openweathermap.org/img/wn/" + forecastInfo.daily[1].weather[0].icon + ".png";
                System.Net.WebRequest request = default(System.Net.WebRequest);
                request = WebRequest.Create(ImageUrl);

                //labWeatherDescription.Text = forecastInfo.weather[1].description;
            }
        }

    }
}
