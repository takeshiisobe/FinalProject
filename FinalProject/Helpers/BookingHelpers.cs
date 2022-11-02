using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTTPClient.DataModels;
using HTTPClient.Tests.TestData;
using HTTPClient.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;



namespace HTTPClient.Helpers
{
    public class BookingHelpers
    {
        public static async Task<string> CreateToken()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var request = JsonConvert.SerializeObject(TokenData.userTokenDetails());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            #region Post Request
            var httpResponse = await httpClient.PostAsync("https://restful-booker.herokuapp.com/auth", postRequest);

            var token = JsonConvert.DeserializeObject<Token1>(httpResponse.Content.ReadAsStringAsync().Result);
            #endregion

            return token.Token;
        }
        
        public static async Task<HttpResponseMessage> GetBooking(long id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            #region Get Request
            return await httpClient.GetAsync(Endpoints.GetUri(Endpoints.BookingEndpoint) + "/" + id);
            #endregion
        }

        public static async Task<HttpResponseMessage> CreateBooking()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var request = JsonConvert.SerializeObject(BookingData.CreateBooking());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            #region Post Request
            return await httpClient.PostAsync(Endpoints.GetURL(Endpoints.BookingEndpoint), postRequest);
            #endregion

        }

        public static async Task<HttpResponseMessage> UpdateBooking(long id, Booking objectModel)
        {
            var httpClient = new HttpClient();
            var token = await CreateToken();

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Cookie", "token=" + token);

            var request = JsonConvert.SerializeObject(objectModel);
            var putRequest = new StringContent(request, Encoding.UTF8, "application/json");

            #region Sending Put Request
            return await httpClient.PutAsync(Endpoints.GetUri(Endpoints.BookingEndpoint) + "/" + id, putRequest);
            #endregion

        }
        public static async Task<HttpResponseMessage> DeleteBooking(long id)
        {
            var httpClient = new HttpClient();
            var token = await CreateToken();

            #region Sending Delete Request
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Cookie", "token=" + token);

            return await httpClient.DeleteAsync(Endpoints.GetUri(Endpoints.BookingEndpoint) + "/" + id);
            #endregion
        }

    }
}
