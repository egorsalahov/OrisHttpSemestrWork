using MiniHttpServerEgorFramework.Core.Attributes;
using MiniHttpServerEgorFramework.Core.HttpResponse;
using MiniHttpServerEgorFramework.Core;

namespace MiniHttpServerEgor.Endpoints
{
    [Endpoint] 
    public class RootEndpoint : EndpointBase
    {
        // для простого "/" пути
        [HttpGet]
        public IHttpResult MainSemestrovka2()
        {
            var obj = new { };

            return Page("MiniHttpServerEgor/Template/Page/sem1.thtml", obj);
        }
    }
}