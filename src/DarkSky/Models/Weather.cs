using Microsoft.AspNetCore.Mvc;
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

        public void GetTemp(string latlng)
        {
            var client = new RestClient("https://api.darksky.net/");
            var request = new RestRequest("forecast/"+ EnvironmentVariables.DarkSkyKey + "/"+latlng, Method.GET);
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

        public void GetSummary(string latlng)
        {
            var client = new RestClient("https://api.darksky.net/");
            var request = new RestRequest("forecast/"+ EnvironmentVariables.DarkSkyKey + "/"+latlng, Method.GET);
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
        
        public static string GetLocation(string location)
        {
            var client = new RestClient("https://maps.googleapis.com/maps/api/");
            var request = new RestRequest("geocode/json?address=" + location + "&key=" + EnvironmentVariables.GoogleMapsKey);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            
            JObject[] latLong = JsonConvert.DeserializeObject<JObject[]>(jsonResponse["results"].ToString());
            JObject firstPosition = JsonConvert.DeserializeObject<JObject>(latLong[0].ToString());
            JObject geometry = JsonConvert.DeserializeObject<JObject>(firstPosition["geometry"].ToString());
            JObject finalStep = JsonConvert.DeserializeObject<JObject>(geometry["location"].ToString());
            JValue lat = JsonConvert.DeserializeObject<JValue>(finalStep["lat"].ToString());
            JValue lng = JsonConvert.DeserializeObject<JValue>(finalStep["lng"].ToString());
            string latlng = lat +","+ lng;
            return latlng;

            //Google API documentation: https://developers.google.com/maps/documentation/geocoding/get-api-key
            //https://developers.google.com/maps/documentation/geocoding/intro
        }

    }
}
