using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SMS1_CSharp_Client
{
    class SendModel
    {
        public string Message { get; set; }
        public string Recipient { get; set; }
    }

    class PatternSendModel
    {
        public int PatternId { get; set; }
        public string Recipient { get; set; }
        public Dictionary<string, string> Pairs { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            // SMS1 API base URL
            string apiBaseUrl = "https://SubDomain.Domain:Port/api/service/";

            // The API name according to SMS1.ir
            // string apiName = "send";
            string apiName = "patternSend";

            // Token is received from SMS1.ir
            string token = "YOUR_TOKEN";

            // Sample input model for API 'send'
            var sendModel = new SendModel()
            {
                Message = "سلام",
                Recipient = "09120000000"
            };

            // Sample input model for API 'patternSend'
            var patternSendModel = new PatternSendModel()
            {
                PatternId = 7, // Sample input model for API 'patternSend'
                Recipient = "09120000000",
                Pairs = new Dictionary<string, string>()
            };

            // Variables that exist in the approved pattern of your Token
            patternSendModel.Pairs["variable_1"] = "value_1";
            patternSendModel.Pairs["variable_2"] = "value_2";

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(apiBaseUrl);
            // Setting the HTTP 'Accept' header to JSON
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Setting the HTTP 'Authorization' header equal to the received Token from SMS1.ir
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // For API 'send', you should use:
            // var content = new StringContent(JsonSerializer.Serialize(sendModel), Encoding.UTF8, "application/json");
            var content = new StringContent(JsonSerializer.Serialize(patternSendModel), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiName, content);

            Console.WriteLine("HTTP response body: \n" + await response.Content.ReadAsStringAsync());
            Console.WriteLine("HTTP status code: " + response.StatusCode);
        }
    }
}
