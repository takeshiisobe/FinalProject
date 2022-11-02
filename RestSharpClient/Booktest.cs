
using System.Net;
using System.Text;
using RestSharpClient.DataModels;
using RestSharpClient.Helpers;
using Newtonsoft.Json;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace RestSharpClient.Tests
{
    [TestClass]
    public class Booktest : ApiBaseTest
    {


        [TestMethod]
        public async Task CreateBooking()
        {
            #region Arrange
            var postReponse = await BookingHelpers.CreateBooking(RestClient);
            var getReponse = await BookingHelpers.GetBooking(RestClient, postReponse.Data.Bookingid);
            var createdBookingData = postReponse.Data;
            var retrievedBookingData = getReponse.Data;
            #endregion

            #region Assertion
            Assert.AreEqual(HttpStatusCode.OK, postReponse.StatusCode, "Status code is not equal to 201");

            Assert.AreEqual(createdBookingData.Booking.Firstname, retrievedBookingData.Firstname, "First Name didn't matched");
            Assert.AreEqual(createdBookingData.Booking.Lastname, retrievedBookingData.Lastname, "Last Name didn't matched");
            Assert.AreEqual(createdBookingData.Booking.Totalprice, retrievedBookingData.Totalprice, "Total Price didn't matched");
            Assert.AreEqual(createdBookingData.Booking.Depositpaid, retrievedBookingData.Depositpaid, "Deposit Paid didn't matched");
            Assert.AreEqual(createdBookingData.Booking.Bookingdates.Checkin, retrievedBookingData.Bookingdates.Checkin, "Checkin date didn't matched");
            Assert.AreEqual(createdBookingData.Booking.Bookingdates.Checkout, retrievedBookingData.Bookingdates.Checkout, "Checkout date didn't matched");
            Assert.AreEqual(createdBookingData.Booking.Additionalneeds, retrievedBookingData.Additionalneeds, "Additional needs didn't matched");
            #endregion

            #region Cleanup
            var deleteRequest = await BookingHelpers.DeleteBooking(RestClient, createdBookingData.Bookingid);
            Assert.AreEqual(HttpStatusCode.Created, deleteRequest.StatusCode, "Status code is not equal to 201");
            #endregion
        }
        [TestMethod]
        public async Task UpdateBooking()
        {
            #region Arrange
            var postResponse = await BookingHelpers.CreateBooking(RestClient);
            var postCreatedBooking = postResponse.Data;
            Booking booking = new Booking()
            {
                Firstname = "Takeshi222",
                Lastname = "Takeshi2222",
                Totalprice = 1998,
                Depositpaid = true,
                Bookingdates = new Bookingdates()
                {
                    Checkin = DateTime.Parse("2022-11-02"),
                    Checkout = DateTime.Parse("2022-11-03")
                },
                Additionalneeds = "Free Parking"
            };
            #endregion

            #region Act
            var putResponse = await BookingHelpers.UpdateBooking(RestClient, postCreatedBooking.Bookingid, booking);
            var updatedBookingData = putResponse.Data;
            #endregion

            #region Assertion of created data
            Assert.AreEqual(HttpStatusCode.OK, putResponse.StatusCode, "Status code is not equal to 201");
            Assert.AreEqual(updatedBookingData.Firstname, booking.Firstname, "First Name didn't matched.");
            Assert.AreEqual(updatedBookingData.Lastname, booking.Lastname, "Last Name didn't matched.");
            Assert.AreEqual(updatedBookingData.Totalprice, postCreatedBooking.Booking.Totalprice, "Total price didn't matched.");
            Assert.AreEqual(updatedBookingData.Additionalneeds, postCreatedBooking.Booking.Additionalneeds, "Additional needs didn't matched.");
            Assert.AreEqual(updatedBookingData.Bookingdates.Checkin, postCreatedBooking.Booking.Bookingdates.Checkin, "Check in date didn't matched.");
            Assert.AreEqual(updatedBookingData.Bookingdates.Checkout, postCreatedBooking.Booking.Bookingdates.Checkout, "Check out date didn't matched.");
            #endregion

            #region Cleanup
            var deleteRequest = await BookingHelpers.DeleteBooking(RestClient, postCreatedBooking.Bookingid);
            Assert.AreEqual(HttpStatusCode.Created, deleteRequest.StatusCode, "Status code is not equal to 201");
            #endregion
        }
        [TestMethod]
        public async Task DeleteBooking()
        {
            #region Arrange
            var postResponse = await BookingHelpers.CreateBooking(RestClient);
            var postCreatedBooking = postResponse.Data;
            #endregion

            #region Act
            var deleteResponse = await BookingHelpers.DeleteBooking(RestClient, postCreatedBooking.Bookingid);
            #endregion

            #region Assertion status code
            Assert.AreEqual(HttpStatusCode.Created, deleteResponse.StatusCode, "Status code is not equal to 201");
            #endregion
        }
        [TestMethod]
        public async Task RandomBooking()
        {
           
        }
    }
}
