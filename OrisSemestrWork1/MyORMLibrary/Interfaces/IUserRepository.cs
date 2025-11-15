using OrisSemestrWork1.MyORMLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrisSemestrWork1.MyORMLibrary.Interfaces
{
    public interface IUserRepository
    {
        User Add(string login, string password);
        User CheckUser(string login, string password, out bool isNewUser);
    }
}
