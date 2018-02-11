using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.ApplicationModel;
using Windows.UI.Notifications;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace WhatsWeather
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
    
            WeatherStart();
        }        
        
        private async void WeatherStart()
        {
            var position = await LocationManager.GetPosition();
            if (position == null)
            {
                suspend();
                return;
            }

            var lat = position.Coordinate.Point.Position.Latitude;
            var lon = position.Coordinate.Point.Position.Longitude;

            RootObject myWeather = await OpenWeatherMapProxy.GetWeather(lat, lon);


            //update
            var uri = String.Format("http://whatsweatherservice.chinacloudsites.cn/?lat={0}&lon={1}", lat, lon);
            var tileContent = new Uri(uri);
            var requestedInterval = PeriodicUpdateRecurrence.HalfHour;
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.StartPeriodicUpdate(tileContent, requestedInterval);


            string icon = String.Format("ms-appx:///Assets/WeatherIcon/{0}.svg", myWeather.weather[0].icon);
            ResultImage.Source = new SvgImageSource(new Uri(icon, UriKind.Absolute));
            ResultCity.Text = myWeather.name;
            ResultTemp.Text = myWeather.main.temp +"℃";
            ResultType.Text = myWeather.weather[0].description;

        }


        private async void Loading()
        {

        }

        private async void suspend()
        {
            MessageDialog message_dialog = new MessageDialog("请打开定位", "权限");
            message_dialog.Commands.Add(new UICommand("确定", cmd => { }, "退出"));
            message_dialog.Commands.Add(new UICommand("取消", cmd => { }));
            message_dialog.DefaultCommandIndex = 0;
            message_dialog.CancelCommandIndex = 1;
            IUICommand result = await message_dialog.ShowAsync();
            if (result.Id as string == "退出")
            {
                Uri uri = new Uri("ms-settings:privacy-location");
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);
            }
        }
    }
}
