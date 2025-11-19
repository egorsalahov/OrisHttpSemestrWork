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

namespace OrisSemestrWork1.MiniHttpServerEgor.Endpoints.ForCrud
{
    [Endpoint]
    public class DeleteEndpoint : EndpointBase
    {
       

        [HttpPost]
        public IHttpResult Delete(int tourId)
        {
            Tour tour = null;
            User user = null;
            var data = new { Tour = tour, User = user };

            try
            {
                ORMContext context = new ORMContext();

                tour = context.Tours.Delete(tourId);

                user = new User() { Permission = true };

                data = new { Tour = tour, User = user };

                return Page("MiniHttpServerEgor/Template/Page/semafterdelete.thtml", data);
            }
            catch (Exception ex)
            {
                return Page("MiniHttpServerEgor/Template/Page/semaerror.thtml", data);

            }
        }
    }
}
