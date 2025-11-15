using MiniHttpServerEgorFramework.Core;
using MiniHttpServerEgorFramework.Core.Attributes;
using MiniHttpServerEgorFramework.Core.HttpResponse;
using MyORMLibrary;
using OrisSemestrWork1.MyORMLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrisSemestrWork1.MiniHttpServerEgor.Endpoints.ForCitySearch
{
    [Endpoint]
    public class BycityadminEndpoint : EndpointBase
    {
        private string _connectionString = "Host=localhost;Port=5432;Database=tours_db;Username=postgres;Password=197911";

        // Post /bycity
        [HttpPost("bycity")]
        public IHttpResult BigPageAdmin(string cityName)
        {

            User user = null;
            List<Tour> tours = null;
            var data = new { Tours = tours, User = user };

            try
            {
                ORMContext context = new ORMContext(_connectionString);

                tours = context.Tours.GetByCity(cityName);

                user = new User() { Permission = true };

                data = new { Tours = tours, User = user };


                return Page("MiniHttpServerEgor/Template/Page/sembycityadmin.thtml", data);
            }
            catch (Exception ex)
            {
                return Page("MiniHttpServerEgor/Template/Page/semerror.thtml", data);

            }
        }
    }
}
