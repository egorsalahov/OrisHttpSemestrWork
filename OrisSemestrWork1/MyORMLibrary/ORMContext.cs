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
        private readonly string _connectionString = "Host=localhost;Port=5432;Database=tours_db;Username=postgres;Password=197911";

        public ITourRepository Tours { get; }
        public IUserRepository Users { get; }

        public ORMContext()
        {
            Tours = new TourRepository(_connectionString);
            Users = new UserRepository(_connectionString);
        }
    }
}
