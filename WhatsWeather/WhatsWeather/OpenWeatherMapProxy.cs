using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;


namespace WhatsWeather
{
    public class OpenWeatherMapProxy
    {
        private  const string appid = "a690ac6d0e88c7e9e71ced5b84924b31";
        public async static Task<RootObject> GetWeather(double lat ,double lon)
        { 
            var http = new HttpClient();            
            var response = await http.GetAsync("http://api.openweathermap.org/data/2.5/weather?lat="+lat+"&lon="+lon+"&units=metric"+"&APPID=" + appid);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var memorystream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data =(RootObject)serializer.ReadObject(memorystream);

            return data;
        }
    }

    [DataContract]
    public class Coord
    {
        [DataMember]
        public string lon { get; set; }
        [DataMember]
        public string lat { get; set; }
    }

    [DataContract]
    public class Weather
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string main { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string icon { get; set; }
    }

    [DataContract]
    public class Main
    {
        [DataMember]
        public string temp { get; set; }
        [DataMember]
        public string pressure { get; set; }
        [DataMember]
        public string humidity { get; set; }
        [DataMember]
        public string temp_min { get; set; }
        [DataMember]
        public string temp_max { get; set; }
    }


    [DataContract]
    public class Wind
    {
        [DataMember]
        public string speed { get; set; }
        [DataMember]
        public string deg { get; set; }
    }

    [DataContract]
    public class Clouds
    {
        [DataMember]
        public string all { get; set; }
    }

    [DataContract]
    public class Sys
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string sunrise { get; set; }
        [DataMember]
        public string sunset { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public Coord coord { get; set; }
        [DataMember]
        public List<Weather> weather { get; set; }
        [DataMember]
        public string @base { get; set; }
        [DataMember]
        public Main main { get; set; }
        [DataMember]
        public string visibility { get; set; }
        [DataMember]
        public Wind wind { get; set; }
        [DataMember]
        public Clouds clouds { get; set; }
        [DataMember]
        public string dt { get; set; }
        [DataMember]
        public Sys sys { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string cod { get; set; }
    }
}
