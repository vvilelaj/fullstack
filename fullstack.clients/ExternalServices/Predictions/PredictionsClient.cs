using fullstack.clients.Models;
using fullstack.shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace fullstack.clients.ExternalServices.Predictions
{
    public class PredictionsClient : IPredictionsClient
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private static class EndPoints {
            public static string POSIBLE_DEATH_DATE = "api/predictions/PossibleDeathDate";
        }
        static string GetPredictionsServiceUrl()
        {
            var predictionsUrl = string.Empty;

            predictionsUrl = ConfigurationManager.AppSettings.Get("PredictionsURL");

            return predictionsUrl;
        }
        static PredictionsClient()
        {
            var predictioncURL = GetPredictionsServiceUrl();
            if (!string.IsNullOrEmpty(predictioncURL))
            {
                httpClient.BaseAddress = new Uri(predictioncURL);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public long GetPosibleDateDeth(Client client)
        {
            long result = 0;

            var paramsString = JsonConvert.SerializeObject(client).ToString();
            var httpResponse = httpClient.PostAsync(EndPoints.POSIBLE_DEATH_DATE,new StringContent(paramsString, Encoding.UTF8,"application/json")).Result;

            if (!httpResponse.IsSuccessStatusCode) return result;

            var responseString = httpResponse.Content.ReadAsStringAsync().Result;

            try
            {
                var response = JsonConvert.DeserializeObject<JsonResultModel<long>>(responseString);
                if (response.Success) result = response.Result;
            }
            catch
            {
                //
            }

            return result;
        }
    }
}