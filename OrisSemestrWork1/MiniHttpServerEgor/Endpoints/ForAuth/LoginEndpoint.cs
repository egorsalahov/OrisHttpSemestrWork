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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrisSemestrWork1.MiniHttpServerEgor.Endpoints.ForAuth
{
    [Endpoint]
    public class LoginEndpoint : EndpointBase
    {
        

        // Post /tour/login
        [HttpPost("login")]
        public IHttpResult LoginForm(string login, string password)
        {

            bool isNewUser;
            User user = null;
            var data = new {User = user};
            string sessionToken = null;
            var response = Context.Response;

            try
            {
                ORMContext context = new ORMContext();

                user = context.Users.CheckUser(login, password, out isNewUser, out sessionToken);

                data = new { User = user };

                if (user != null && !isNewUser)
                {
                    if (sessionToken != null)
                    {
                        // Данные для добавления куки в сессию
                        DateTime expiration = DateTime.UtcNow.AddHours(1);
                        string expiresDate = expiration.ToString("R", CultureInfo.InvariantCulture);
                        string cookieValue = $"SessionToken={sessionToken}; HttpOnly; Secure; SameSite=Lax; Expires={expiresDate}";

                        response.Headers.Add("Set-Cookie", cookieValue);
                    }

                    return Page("MiniHttpServerEgor/Template/Page/semafterlogin.thtml", data);
                }
                else
                {
                    return Page("MiniHttpServerEgor/Template/Page/semerror.thtml", data);
                }
            }
            catch (Exception ex)
            {
                return Page("MiniHttpServerEgor/Template/Page/semerror.thtml", user);
            }


        }
    }
}
