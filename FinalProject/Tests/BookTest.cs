using Newtonsoft.Json;
using System.Net;
using HTTPClient.DataModels;
using HTTPClient.Tests.TestData;
using HTTPClient.Resources;
using HTTPClient.Helpers;
using RestSharp;


[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]

namespace HTTPClient.Tests
{
    [TestClass]
    public class BookTest
    {
        [TestMethod]
        public async Task CreateBooking()
        {
            #region Arrange
            var CreateBooking = await BookingHelpers.CreateBooking();
            #endregion

            #region Act
            var CreatedBooking = JsonConvert.DeserializeObject<BookingID>(CreateBooking.Content.ReadAsStringAsync().Result);
            var getResponse = await BookingHelpers.GetBooking(CreatedBooking.Bookingid);
            var BookingData = JsonConvert.DeserializeObject<Booking>(getResponse.Content.ReadAsStringAsync().Result);
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode, "Status code is not equal to 201");
            Assert.AreEqual(BookingData.Firstname, CreatedBooking.Booking.Firstname, "First Name didn't matched.");
            Assert.AreEqual(BookingData.Lastname, CreatedBooking.Booking.Lastname, "Last Name didn't matched.");
            Assert.AreEqual(BookingData.Totalprice, CreatedBooking.Booking.Totalprice, "Total price didn't matched.");
            Assert.AreEqual(BookingData.Additionalneeds, CreatedBooking.Booking.Additionalneeds, "Additional needs didn't matched.");
            Assert.AreEqual(BookingData.Bookingdates.Checkin, CreatedBooking.Booking.Bookingdates.Checkin, "Check in date didn't matched.");
            Assert.AreEqual(BookingData.Bookingdates.Checkout, CreatedBooking.Booking.Bookingdates.Checkout, "Check out date didn't matched.");
            #endregion

            #region Cleanup
            var Delete = await BookingHelpers.DeleteBooking(CreatedBooking.Bookingid);
            Assert.AreEqual(HttpStatusCode.Created, Delete.StatusCode, "Status code is not equal to 201");
            #endregion
           

        }

        [TestMethod]
        public async Task UpdateBooking()
        {
            #region Arrange
            var CreateBooking = await BookingHelpers.CreateBooking();
            var CreatedBooking = JsonConvert.DeserializeObject<BookingID>(CreateBooking.Content.ReadAsStringAsync().Result);
            Booking Update = new Booking()
            {
                Firstname = "Takeshi222",
                Lastname = "Isobe222",
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
            var UpdateBooking = await BookingHelpers.UpdateBooking(CreatedBooking.Bookingid, Update); ;
            var UpdatedBookingData = JsonConvert.DeserializeObject<Booking>(UpdateBooking.Content.ReadAsStringAsync().Result);
            
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.OK, UpdateBooking.StatusCode, "Status code is not equal to 201");
            Assert.AreEqual(UpdatedBookingData.Firstname, Update.Firstname, "First Name didn't matched.");
            Assert.AreEqual(UpdatedBookingData.Lastname, Update.Lastname, "Last Name didn't matched.");
            Assert.AreEqual(UpdatedBookingData.Totalprice, CreatedBooking.Booking.Totalprice, "Total price didn't matched.");
            Assert.AreEqual(UpdatedBookingData.Additionalneeds, CreatedBooking.Booking.Additionalneeds, "Additional needs didn't matched.");
            Assert.AreEqual(UpdatedBookingData.Bookingdates.Checkin, CreatedBooking.Booking.Bookingdates.Checkin, "Check in date didn't matched.");
            Assert.AreEqual(UpdatedBookingData.Bookingdates.Checkout, CreatedBooking.Booking.Bookingdates.Checkout, "Check out date didn't matched.");
            #endregion

            #region Cleanup
            var Delete = await BookingHelpers.DeleteBooking(CreatedBooking.Bookingid);
            Assert.AreEqual(HttpStatusCode.Created, Delete.StatusCode, "Status code is not equal to 201");
            #endregion

        }

        [TestMethod]
        public async Task DeleteBooking()
        {
            #region Arrange
            var CreateBooking = await BookingHelpers.CreateBooking();
            var CreatedBooking = JsonConvert.DeserializeObject<BookingID>(CreateBooking.Content.ReadAsStringAsync().Result);
            #endregion

            #region Act
            var Delete = await BookingHelpers.DeleteBooking(CreatedBooking.Bookingid);
            #endregion

            #region Act
            Assert.AreEqual(HttpStatusCode.Created, Delete.StatusCode, "Status code is not equal to 201");
            #endregion


        }

        [TestMethod]
        public async Task RandomBooking()
        {
            #region Act
            var GetBooking = await BookingHelpers.GetBooking(10928309);
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.NotFound, GetBooking.StatusCode, "Status code does not match.");
            #endregion
        }
    }
}