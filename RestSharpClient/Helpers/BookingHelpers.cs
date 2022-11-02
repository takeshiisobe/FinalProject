using RestSharpClient.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharpClient.DataModels;
using RestSharpClient.Tests.TestData;
using Newtonsoft.Json;



namespace RestSharpClient.Helpers
{
    public class BookingHelpers
    {
        public static async Task<string> CreateToken(RestClient restClient)
        {
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            #region Post Request
            var postRequest = new RestRequest("https://restful-booker.herokuapp.com/auth").AddJsonBody(TokenData.userTokenDetails());
            var postResponse = await restClient.ExecutePostAsync<Token1>(postRequest);
            #endregion

            return postResponse.Data.Token;
        }
        public static async Task<RestResponse<Booking>> GetBooking(RestClient restClient, long id)
        {
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            #region Get Request
            var getRequest = new RestRequest(Endpoints.GetUri(Endpoints.BookingEndpoint) + "/" + id);
            return await restClient.ExecuteGetAsync<Booking>(getRequest);
            #endregion
        }
        public static async Task<RestResponse<Booking>> CreateBooking(RestClient restClient)
        {
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            #region Post Method
            var postRequest = new RestRequest(Endpoints.GetURL(Endpoints.BookingEndpoint)).AddJsonBody(BookingData.CreateBooking());
            return await restClient.ExecutePostAsync<Booking>(postRequest);
            #endregion
        }

        public static async Task<RestResponse<Booking>> UpdateBooking(RestClient restClient, long id, Booking objectModel)
        {
            restClient = new RestClient();
            var token = await CreateToken(restClient);

            restClient.AddDefaultHeader("Accept", "application/json");
            restClient.AddDefaultHeader("Cookie", "token=" + token);

            #region Put Method
            var putRequest = new RestRequest(Endpoints.GetUri(Endpoints.BookingEndpoint) + "/" + id).AddJsonBody(objectModel);
            return await restClient.ExecutePutAsync<Booking>(putRequest);
            #endregion

        }
        public static async Task<RestResponse> DeleteBooking(RestClient restClient, long id)
        {
            restClient = new RestClient();
            var token = await CreateToken(restClient);

            restClient.AddDefaultHeader("Accept", "application/json");
            restClient.AddDefaultHeader("Cookie", "token=" + token);

            #region Delete Method
            var deleteRequest = new RestRequest(Endpoints.GetUri(Endpoints.BookingEndpoint) + "/" + id);
            return await restClient.DeleteAsync(deleteRequest);
            #endregion

        }
        

    }
}
