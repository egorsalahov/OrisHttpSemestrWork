using Npgsql;
using NpgsqlTypes;
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
   
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        //register
        public User Add2(string login, string password, out string sessionToken)
        {
            sessionToken = null;

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

                    // Логика установки прав администратора
                    if (login == "admin" && password == "admin")
                    {
                        command.Parameters.AddWithValue("@Permission", true);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Permission", false);
                    }

                    try
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                User newUser = new User
                                {
                                    Id = reader.GetInt32("Id"),
                                    Login = reader.GetString("Login"),
                                    PasswordInHash = reader.GetString("PasswordInHash"),
                                    Permission = reader.GetBoolean("Permission")
                                };


                                reader.Close();

                                sessionToken = CreateSession(newUser.Id);

                                return newUser;
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        // Обработка других ошибок
                        throw new Exception($"Не получилось добавить пользователя в базу данных: {ex.Message}", ex);
                    }
                }
            }

            throw new Exception("Не получилось добавить пользователя в базу данных.");
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




        //login

        public User CheckUser(string login, string password, out bool isNewUser, out string sessionToken)
        {
           
            sessionToken = null;
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
                            // Пользователь найден. Проверяем пароль.
                            string storedHash = reader.GetString("PasswordInHash");

                            if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                            {
                                
                                User authenticatedUser = new User
                                {
                                    Id = reader.GetInt32("Id"),
                                    Login = reader.GetString("Login"),
                                    PasswordInHash = storedHash,
                                    Permission = reader.GetBoolean("Permission")
                                };

                               
                                sessionToken = CreateSession(authenticatedUser.Id);

                                return authenticatedUser;
                            }

                           
                            return null;
                        }
                    }
                }
            }

         
            isNewUser = true;

            User newUser = Add(login, password);

            if (newUser != null)
            {
              
                sessionToken = CreateSession(newUser.Id);
                return newUser;
            }

          
            return null;
        }

        private string CreateSession(int userId)
        {
            // 1. Генерация уникального токена (GUID)
            string sessionToken = Guid.NewGuid().ToString();

            // 2. Расчет времени истечения: 1 час с текущего момента (используем UTC)
            DateTime expiresAt = DateTime.UtcNow.AddHours(1);

            
            string sql = @"
INSERT INTO Sessions (Token, UserId, ExpiresAt) 
VALUES (@Token, @UserId, @ExpiresAt)";

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        
                        command.Parameters.Add(new NpgsqlParameter("@Token", NpgsqlDbType.Varchar) { Value = sessionToken });
                        command.Parameters.Add(new NpgsqlParameter("@UserId", NpgsqlDbType.Integer) { Value = userId });

                        // NpgsqlDbType.TimestampTz используется для TIMESTAMP WITH TIME ZONE
                        command.Parameters.Add(new NpgsqlParameter("@ExpiresAt", NpgsqlDbType.TimestampTz) { Value = expiresAt });

                       
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                  
                    Console.WriteLine($"Ошибка при создании сессии: {ex.Message}");
                   
                    return null;
                }
            }

            return sessionToken; // Возвращаем токен, который будет установлен в куки
        }

    }
}
