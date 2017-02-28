using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace DarkSky.Models
{
    public class Weather
    {

        public string Temp { get; set; }
        public string Summary { get; set; }
        
        
        public Weather()
        {
        
        }

        public void GetTemp()
        {
            var client = new RestClient("https://api.darksky.net/");
            var request = new RestRequest("forecast/90da12a86306b1ec09bd65356b7e0707/45.52,-122.65", Method.GET);
            var response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            JObject daily = JsonConvert.DeserializeObject<JObject>(jsonResponse["daily"].ToString());
            JObject[] data = JsonConvert.DeserializeObject<JObject[]>(daily["data"].ToString());
            JObject firstElement = JsonConvert.DeserializeObject<JObject>(data[0].ToString());

            Temp = firstElement["temperatureMax"].ToString();
        }

        public void GetSummary()
        {
            var client = new RestClient("https://api.darksky.net/");
            var request = new RestRequest("forecast/90da12a86306b1ec09bd65356b7e0707/45.52,-122.65", Method.GET);
            var response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            JObject daily = JsonConvert.DeserializeObject<JObject>(jsonResponse["daily"].ToString());
            JObject[] data = JsonConvert.DeserializeObject<JObject[]>(daily["data"].ToString());
            JObject firstElement = JsonConvert.DeserializeObject<JObject>(data[0].ToString());

            Summary = daily["summary"].ToString();
         
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
