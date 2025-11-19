using Azure.Core;
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

namespace MiniHttpServerEgor.Endpoints
{
    [Endpoint]
    public class TourEndpoint : EndpointBase
    {
       

        // /tour
        [HttpGet()]
        public IHttpResult MainSemestrovka()
        {
            var obj = new { };

            return Page("MiniHttpServerEgor/Template/Page/sem1.thtml", obj);
        }

        // Post /tour/search
        [HttpPost("search")]
        public IHttpResult DataFromForm(string country, int stars, int budget)
        {

            List<Tour> tours = null;
            var data = new { Tours = tours };

            try
            {
                ORMContext context = new ORMContext();

                tours = context.Tours.GetByFilter(country, stars, budget);

                data = new { Tours = tours };

                return Page("MiniHttpServerEgor/Template/Page/sem2.thtml", data);

            }
            catch (Exception ex)
            {
                return Page("MiniHttpServerEgor/Template/Page/semerror.thtml", data);

            }

        }
    }
}
