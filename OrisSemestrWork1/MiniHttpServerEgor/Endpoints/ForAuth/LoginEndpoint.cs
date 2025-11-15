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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrisSemestrWork1.MiniHttpServerEgor.Endpoints.ForAuth
{
    [Endpoint]
    public class LoginEndpoint : EndpointBase
    {
        private string _connectionString = "Host=localhost;Port=5432;Database=tours_db;Username=postgres;Password=197911";

        // Post /tour/login
        [HttpPost("login")]
        public IHttpResult LoginForm(string login, string password)
        {

            bool isNewUser;
            User user = null;
            var data = new {User = user};

            try
            {
                ORMContext context = new ORMContext(_connectionString);

                isNewUser = false; ; //чтобы проверить: вдруг пользователь пытается войти не зарегестрировавшись

                user = context.Users.CheckUser(login, password, out isNewUser);

                data = new { User = user };

                if (!isNewUser) //все норм пользователь не регается с нуля а уже входит в созданный аккаунт
                {
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
