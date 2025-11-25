using Azure;
using MiniHttpServerEgorFramework.Core;
using MiniHttpServerEgorFramework.Core.Attributes;
using MiniHttpServerEgorFramework.Core.HttpResponse;
using MyORMLibrary;
using OrisSemestrWork1.MyORMLibrary.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrisSemestrWork1.MiniHttpServerEgor.Endpoints.ForAuth
{
    [Endpoint]
    public class JoinEndpoint : EndpointBase
    {
       

        // Post /tour/join
        [HttpPost("join")]
        public IHttpResult JoinForm(string login, string password)
        {
            User user = null;
            string sessionToken = null;
            var response = Context.Response;

            try
            {
                ORMContext context = new ORMContext();

                user = context.Users.Add2(login, password, out sessionToken);

                //данные для куки в сессии
                DateTime expiration = DateTime.UtcNow.AddHours(1);
                string expiresDate = expiration.ToString("R", CultureInfo.InvariantCulture);
                string cookieValue = $"SessionToken={sessionToken}; HttpOnly; Secure; SameSite=Lax; Expires={expiresDate}";

                response.Headers.Add("Set-Cookie", cookieValue);

                return Page("MiniHttpServerEgor/Template/Page/semafterjoin.thtml", user);
            }
            catch (Exception ex)
            {
                return Page("MiniHttpServerEgor/Template/Page/semerror.thtml", user);
            }
        }
    }
}
