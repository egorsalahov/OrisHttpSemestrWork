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
    public class BycityEndpoint : EndpointBase
    {
        private string _connectionString = "Host=localhost;Port=5432;Database=tours_db;Username=postgres;Password=197911";

        // Post /bycity
        [HttpPost("bycity")]
        public IHttpResult BigPage(string cityName)
        {
            List<Tour> tours = null;
            var data = new { Tours = tours };

            try
            {
                ORMContext context = new ORMContext(_connectionString);

                tours = context.Tours.GetByCity(cityName);

                data = new { Tours = tours };

                return Page("C:\\Users\\egors\\source\\repos\\OrisSemestrWork1\\OrisSemestrWork1\\MiniHttpServerEgor\\Template\\Page\\sembycity.thtml", data);

            }
            catch (Exception ex)
            {
                return Page("C:\\Users\\egors\\source\\repos\\OrisSemestrWork1\\OrisSemestrWork1\\MiniHttpServerEgor\\Template\\Page\\semerror.thtml", data);

            }

        }
    }
}
