using Npgsql;
using OrisSemestrWork1.MyORMLibrary.Interfaces;
using OrisSemestrWork1.MyORMLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrisSemestrWork1.MyORMLibrary.Repositories
{
    // MyORMLibrary/Repositories/UserRepository.cs
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User Add(string login, string password)
        {
            // ПЕРЕНЕСЕН ваш код из AddUser
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
INSERT INTO Users (Login, PasswordInHash, Permission)
VALUES (@Login, @PasswordInHash, @Permission)
RETURNING Id, Login, PasswordInHash, Permission";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@PasswordInHash", passwordHash);

                    if (login == "admin" && password == "admin")
                    {
                        command.Parameters.AddWithValue("@Permission", true);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Permission", false);
                    }

                   
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    Id = reader.GetInt32("Id"),
                                    Login = reader.GetString("Login"),
                                    PasswordInHash = reader.GetString("PasswordInHash"),
                                    Permission = reader.GetBoolean("Permission")
                                };
                            }
                        }
                }
            }

            throw new Exception("Не получилось добавить пользователя в базу данных");
        }

        public User CheckUser(string login, string password, out bool isNewUser)
        {
            // ПЕРЕНЕСЕН ваш код из ChekUser
            isNewUser = false;

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
SELECT Id, Login, PasswordInHash, Permission 
FROM Users 
WHERE Login = @Login";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.Add(new NpgsqlParameter("@Login", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = login });

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader.GetString("PasswordInHash");

                            if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                            {
                                return new User
                                {
                                    Id = reader.GetInt32("Id"),
                                    Login = reader.GetString("Login"),
                                    PasswordInHash = storedHash,
                                    Permission = reader.GetBoolean("Permission")
                                };
                            }
                            return null;
                        }
                    }
                }

                isNewUser = true;
                return Add(login, password);
            }
        }
    }
}
