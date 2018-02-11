using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;

namespace WhatsWeather
{
    class LocationManager
    {
        public async static Task<Geoposition> GetPosition()
        {
         
            var accessStatus = await Geolocator.RequestAccessAsync();
            //询问是否能得到地址          

            if(accessStatus == GeolocationAccessStatus.Allowed)
            {                          
                var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 }; //对象初始化
                var position = await geolocator.GetGeopositionAsync();
                return position;
            }
            else
            {               
                return null;
            }



        }
    }


  
}
