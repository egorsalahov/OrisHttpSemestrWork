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

namespace OrisSemestrWork1.MiniHttpServerEgor.Endpoints.ForAuth
{
    [Endpoint]
    public class JoinEndpoint : EndpointBase
    {
        private string _connectionString = "Host=localhost;Port=5432;Database=tours_db;Username=postgres;Password=197911";

        // Post /tour/join
        [HttpPost("join")]
        public IHttpResult JoinForm(string login, string password)
        {
            User user = null;

            try
            {
                ORMContext context = new ORMContext(_connectionString);

                user = context.Users.Add(login, password);

                return Page("MiniHttpServerEgor/Template/Page/semafterjoin.thtml", user);
            }
            catch (Exception ex)
            {
                return Page("MiniHttpServerEgor/Template/Page/semerror.thtml", user);
            }
        }
    }
}
