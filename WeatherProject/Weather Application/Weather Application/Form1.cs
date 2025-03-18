using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

namespace Weather_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string APIkey = "162e72664391e3a07bd2f605a368385e";

        private void label1_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void label3_Click(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e) { }

        private void label4_Click(object sender, EventArgs e) { }

        private void label5_Click(object sender, EventArgs e) { }

        private void label6_Click(object sender, EventArgs e) { }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            getWeather(); // Call the function instead of defining it inside
            getForecast();
        }

        double lon;
        double lat;

        void getWeather()
        {
            using (WebClient webClient = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}",TBCity.Text, APIkey);
                var json = webClient.DownloadString(url);
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                picIcon.ImageLocation = "https://openweathermap.org/img/wn/" + Info.weather[0].icon + ".png";

                labCondition.Text = Info.weather[0].main;
                labDetails.Text = Info.weather[0].main;
                labSunset.Text = convertDateTime(Info.sys.sunset).ToShortTimeString();
                labSunrise.Text = convertDateTime(Info.sys.sunrise).ToShortTimeString();
                labWindSpeed.Text = Info.wind.speed.ToString();
                labPressure.Text = Info.main.pressure.ToString();

                lon = Info.coord.lon;
                lat = Info.coord.lat;
            }
        }

        DateTime convertDateTime(long sec)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(sec).ToLocalTime();
            return day;
        }

        void getForecast()
        {

            using (WebClient webClient = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/3.0/onecall?lat={0}&lon={1}&exclude=current,minutely,hourly,alerts&appid={2}",lat, lon, APIkey);

                var json = webClient.DownloadString(url);
                WeatherForecast.ForecastInfo forecastInfo = JsonConvert.DeserializeObject<WeatherForecast.ForecastInfo>(json);
                ForecastUC FUC;
                for (int i = 0; i < 8; i++) 
                {
                    FUC = new ForecastUC();
                    FUC.picWeatherIcon.ImageLocation = "https://openweathermap.org/img/wn/" + forecastInfo.Daily[i].Weather[0].Icon + ".png";
                    FUC.labMainWeather.Text = forecastInfo.Daily[i].Weather[0].Main;
                    FUC.labWeatherDescription.Text = forecastInfo.Daily[i].Weather[0].Description;
                    FUC.labDT.Text = convertDateTime(forecastInfo.Daily[i].Dt).DayOfWeek.ToString();

                    FLP.Controls.Add(FUC);

                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labSunrise_Click(object sender, EventArgs e)
        {

        }
    }
}
