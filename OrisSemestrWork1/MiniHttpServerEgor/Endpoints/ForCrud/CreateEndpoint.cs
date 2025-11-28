using MiniHttpServerEgorFramework.Core;
using MiniHttpServerEgorFramework.Core.Attributes;
using MiniHttpServerEgorFramework.Core.HttpResponse;
using MyORMLibrary;
using OrisSemestrWork1.MyORMLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OrisSemestrWork1.MiniHttpServerEgor.Endpoints.ForCrud
{
    [Endpoint]
    public class CreateEndpoint : EndpointBase
    {
       

        [HttpPost]
        public IHttpResult Create(string country, string city, int stars, int price, string imagePath,
                   string hotelName, string hotelShortDescription, string hotelAddress)
        {
            //декодирование отеля
            string decodedHotelName = HttpUtility.UrlDecode(hotelName);
            string decodedHotelShortDescription = HttpUtility.UrlDecode(hotelShortDescription);
            string decodedHotelAddress = HttpUtility.UrlDecode(hotelAddress);

            Tour tour = null;
            User user = null;
            var data = new {Tour = tour, User = user};
           

            try
            {
                ORMContext context = new ORMContext();

                tour = context.Tours.Create(country, city, stars, price, imagePath,
                    decodedHotelName, decodedHotelShortDescription, decodedHotelAddress);

                user = new User() { Permission = true };

                data = new { Tour = tour, User = user };

                return Page("MiniHttpServerEgor/Template/Page/semaftercreate.thtml", data);
            }
            catch (Exception ex)
            {
                return Page("MiniHttpServerEgor/Template/Page/semerror.thtml", data);

            }
        }

    }
}
