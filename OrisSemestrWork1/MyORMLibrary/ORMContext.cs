using Azure;
using Microsoft.Data.SqlClient;
using Npgsql;
using OrisSemestrWork1.MyORMLibrary.Interfaces;
using OrisSemestrWork1.MyORMLibrary.Models;
using OrisSemestrWork1.MyORMLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyORMLibrary
{
    // MyORMLibrary/ORMContext.cs
    public class ORMContext
    {
        private readonly string _connectionString;

        public ITourRepository Tours { get; }
        public IUserRepository Users { get; }

        public ORMContext(string connectionString)
        {
            _connectionString = connectionString;
            Tours = new TourRepository(connectionString);
            Users = new UserRepository(connectionString);
        }
    }
}
