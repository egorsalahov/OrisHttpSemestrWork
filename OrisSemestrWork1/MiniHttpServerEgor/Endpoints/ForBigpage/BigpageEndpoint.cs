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
        private string _connectionString = "Host=localhost;Port=5432;Database=tours_db;Username=postgres;Password=197911";

        // Post /tour/bigpage
        [HttpPost("bigpage")]
        public IHttpResult BigPage(int tourId)
        {
            Tour tour = null;

            try
            {
                ORMContext context = new ORMContext(_connectionString);

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
