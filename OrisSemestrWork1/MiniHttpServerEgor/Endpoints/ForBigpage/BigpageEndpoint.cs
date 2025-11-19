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

namespace OrisSemestrWork1.MiniHttpServerEgor.Endpoints.ForBigpage
{
    [Endpoint]
    public class BigpageEndpoint : EndpointBase
    {
        

        // Post /tour/bigpage
        [HttpPost("bigpage")]
        public IHttpResult BigPage(int tourId)
        {
            Tour tour = null;

            try
            {
                ORMContext context = new ORMContext();

                tour = context.Tours.GetById(tourId);

                return Page("MiniHttpServerEgor/Template/Page/sembigpage.thtml", tour);

            }
            catch (Exception ex)
            {
                return Page("MiniHttpServerEgor/Template/Page/semerror.thtml", tour);

            }
        }
    }
}
