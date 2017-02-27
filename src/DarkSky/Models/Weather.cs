using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DarkSky.Models
{
    public class Weather
    {

        public string Summary { get; set; }
        //public string TemperatureMax { get; set; }
        
        

        public Weather()
        {

        }

        public void GetWeather()
        {
            var client = new RestClient("https://api.darksky.net/");
            var request = new RestRequest("forecast/90da12a86306b1ec09bd65356b7e0707/37.8267,-122.4233", Method.GET);
            var response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            JObject daily = JsonConvert.DeserializeObject<JObject>(jsonResponse["daily"].ToString());
            JObject[] data = JsonConvert.DeserializeObject<JObject[]>(daily["data"].ToString());
            JObject firstElement = JsonConvert.DeserializeObject<JObject>(data[0].ToString());


            //Weather data = JsonConvert.DeserializeObject<Weather>(daily["data"].ToString());


            Console.WriteLine("Summary: {0} ", firstElement["temperatureMax"]);
            //Console.WriteLine("Body: {0}", message.Body); 
            //Console.WriteLine("Status: {0}", message.Status);
            
            Console.ReadLine();

        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }

    }
}
