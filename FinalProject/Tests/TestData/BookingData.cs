using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTTPClient.DataModels;

namespace HTTPClient.Tests.TestData
{
    internal class BookingData
    {
        public static Booking CreateBooking()
        {
            return new Booking
            {
                Firstname = "Takeshi",
                Lastname = "Isobe",
                Totalprice = 1998,
                Depositpaid = true,
                Bookingdates = new Bookingdates()
                {
                    Checkin = DateTime.Parse("2022-11-02"),
                    Checkout = DateTime.Parse("2022-11-03")
                },
                Additionalneeds = "Free Parking"
            };
        }
    }
}
