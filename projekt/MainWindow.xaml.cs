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
using System.Text.RegularExpressions;

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
            getWeather();
            getForcecast();
        }

        string APIKey = "f06f748cb174f38b78906946a08bab92";

        //private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        //{
        //    Regex regex = new Regex("[^a-zA-Z]+");
        //    e.Handled = regex.IsMatch(e.Text);
        //}
        //PreviewTextInput="TextValidationTextBox"

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
                labSunset.Text = convertDateTime(Info.sys.sunset).ToShortTimeString();
                labSunrise.Text = convertDateTime(Info.sys.sunrise).ToShortTimeString();

                Temp.Text = Info.main.temp.ToString() + "°C";
                TempMax.Text = Info.main.temp_max.ToString() + "°C";
                TempMin.Text = Info.main.temp_min.ToString() + "°C";
                Humidity.Text = Info.main.humidity.ToString() + "%";
                labWindSpeed.Text = Info.wind.speed.ToString() + "/kph";
                labPressure.Text = Info.main.pressure.ToString() + " hPa";
                FeelsLike.Text = Info.main.feels_like.ToString() + "°C";
                dateNow.Text = DateTime.Now.ToString();

                //TBCityy.Text = Info.nameTown.name.toString();

                TBCityy.Text = TBCity.Text;

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
                string url = string.Format("https://api.openweathermap.org/data/2.5/onecall?units=metric&lat={0}" + "&lon=" + "{1}" + "&exclude=current,minutely,hourly,alerts&appid={2}", lat, lon, APIKey);
                var json = web.DownloadString(url);
                
                WeatherForecast.ForecastInfo forecastInfo = JsonConvert.DeserializeObject<WeatherForecast.ForecastInfo>(json);

                MainWeather0.Text = forecastInfo.daily[0].weather[0].main;
                uvi0.Text = forecastInfo.daily[0].uvi.ToString();
                moon_phase.Text = forecastInfo.daily[0].moon_phase.ToString();

                WeatherDescription1.Text = forecastInfo.daily[1].weather[0].description;
                labDT1.Text = convertDateTime(forecastInfo.daily[1].dt).ToShortDateString();
                MainWeather1.Text = forecastInfo.daily[1].weather[0].main;
                tempDay1.Text = forecastInfo.daily[1].temp.day.ToString() + "°C";
                tempNight1.Text = forecastInfo.daily[1].temp.night.ToString() + "°C";

                WeatherDescription2.Text = forecastInfo.daily[2].weather[0].description;
                labDT2.Text = convertDateTime(forecastInfo.daily[2].dt).ToShortDateString();
                MainWeather2.Text = forecastInfo.daily[2].weather[0].main;
                tempDay2.Text = forecastInfo.daily[2].temp.day.ToString() + "°C";
                tempNight2.Text = forecastInfo.daily[2].temp.night.ToString() + "°C";

                WeatherDescription3.Text = forecastInfo.daily[3].weather[0].description;
                labDT3.Text = convertDateTime(forecastInfo.daily[3].dt).ToShortDateString();
                MainWeather3.Text = forecastInfo.daily[3].weather[0].main;
                tempDay3.Text = forecastInfo.daily[3].temp.day.ToString() + "°C";
                tempNight3.Text = forecastInfo.daily[3].temp.night.ToString() + "°C";

                WeatherDescription4.Text = forecastInfo.daily[4].weather[0].description;
                labDT4.Text = convertDateTime(forecastInfo.daily[4].dt).ToShortDateString();
                MainWeather4.Text = forecastInfo.daily[4].weather[0].main;
                tempDay4.Text =  forecastInfo.daily[4].temp.day.ToString() + "°C";
                tempNight4.Text = forecastInfo.daily[4].temp.night.ToString() + "°C";

                WeatherDescription5.Text = forecastInfo.daily[5].weather[0].description;
                labDT5.Text = convertDateTime(forecastInfo.daily[5].dt).ToShortDateString();
                MainWeather5.Text = forecastInfo.daily[5].weather[0].main;
                tempDay5.Text = forecastInfo.daily[5].temp.day.ToString() + "°C";
                tempNight5.Text = forecastInfo.daily[5].temp.night.ToString() + "°C";

                //labWeatherDescription.Text = forecastInfo.weather[1].description;
            }
        }


    }
}
