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
       

        // Post /bycity
        [HttpPost("bycity")]
        public IHttpResult ByCities(string cityName)
        {
            List<Tour> tours = null;
            var data = new { Tours = tours };

            try
            {
                ORMContext context = new ORMContext();

                tours = context.Tours.GetByCity(cityName);

                data = new { Tours = tours };

                return Page("MiniHttpServerEgor/Template/Page/sembycity.thtml", data);

            }
            catch (Exception ex)
            {
                return Page("MiniHttpServerEgor/Template/Page/semerror.thtml", data);

            }

        }
    }
}
