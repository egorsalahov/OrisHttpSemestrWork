using Npgsql;
using OrisSemestrWork1.MyORMLibrary.Interfaces;
using OrisSemestrWork1.MyORMLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace OrisSemestrWork1.MyORMLibrary.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly string _connectionString;

        public TourRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Tour> GetByFilter(string country, int stars, int budget)
        {
            List<Tour> listResult = new List<Tour>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
SELECT t.*, 
       h.id as hotel_id, h.name as hotel_name, h.short_description, h.address
FROM Tours t
LEFT JOIN Hotels h ON t.hotel_id = h.id
WHERE t.country = @Country 
  AND t.stars = @Stars 
  AND t.price <= @Budget";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Country", country);
                    command.Parameters.AddWithValue("@Stars", stars);
                    command.Parameters.AddWithValue("@Budget", budget);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listResult.Add(new Tour
                            {
                                Id = reader.GetInt32("id"),
                                Country = reader.GetString("country"),
                                City = reader.GetString("city"),
                                Stars = reader.GetInt32("stars"),
                                Price = reader.GetInt32("price"),
                                ImagePath = reader.IsDBNull(reader.GetOrdinal("image_path")) ? null : reader.GetString("image_path"), // 👈 ДОБАВЛЕНО

                                HotelId = reader.GetInt32("hotel_id"),
                                ContactId = reader.GetInt32("contact_id"),
                                LegalInfoId = reader.GetInt32("legal_info_id"),

                                Hotel = new Hotel
                                {
                                    Id = reader.GetInt32("hotel_id"),
                                    Name = reader.GetString("hotel_name"),
                                    ShortDescription = reader.GetString("short_description"),
                                    Address = reader.GetString("address")
                                }
                            });
                        }
                    }
                }
            }

            return listResult;
        }

        public Tour GetById(int tourId)
        {
            Tour resultTour = null;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
SELECT t.*, 
       h.id as hotel_id, h.name as hotel_name, h.short_description, h.address,
       c.id as contact_id, c.phone_number, c.contact_name, c.email,
       l.id as legal_info_id, l.registry_entry, l.company_name, l.insurance_info
FROM Tours t
LEFT JOIN Hotels h ON t.hotel_id = h.id
LEFT JOIN Contacts c ON t.contact_id = c.id
LEFT JOIN Legal_Info l ON t.legal_info_id = l.id
WHERE t.id = @TourId";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@TourId", tourId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            resultTour = new Tour
                            {
                                Id = reader.GetInt32("id"),
                                Country = reader.GetString("country"),
                                City = reader.GetString("city"),
                                Stars = reader.GetInt32("stars"),
                                Price = reader.GetInt32("price"),
                                ImagePath = reader.IsDBNull(reader.GetOrdinal("image_path")) ? null : reader.GetString("image_path"), // 👈 ДОБАВЛЕНО

                                HotelId = reader.GetInt32("hotel_id"),
                                ContactId = reader.GetInt32("contact_id"),
                                LegalInfoId = reader.GetInt32("legal_info_id"),

                                Hotel = new Hotel
                                {
                                    Id = reader.GetInt32("hotel_id"),
                                    Name = reader.GetString("hotel_name"),
                                    ShortDescription = reader.GetString("short_description"),
                                    Address = reader.GetString("address")
                                },
                                Contact = new Contact
                                {
                                    Id = reader.GetInt32("contact_id"),
                                    PhoneNumber = reader.GetString("phone_number"),
                                    ContactName = reader.GetString("contact_name"),
                                    Email = reader.GetString("email")
                                },
                                LegalInfo = new LegalInfo
                                {
                                    Id = reader.GetInt32("legal_info_id"),
                                    RegistryEntry = reader.GetString("registry_entry"),
                                    CompanyName = reader.GetString("company_name"),
                                    InsuranceInfo = reader.GetString("insurance_info")
                                }
                            };
                        }
                    }
                }
            }

            return resultTour;
        }

        public List<Tour> GetByCity(string cityName)
        {
            List<Tour> listResult = new List<Tour>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
SELECT t.*, 
       h.id as hotel_id, h.name as hotel_name, h.short_description, h.address,
       c.id as contact_id, c.phone_number, c.contact_name, c.email,
       l.id as legal_info_id, l.registry_entry, l.company_name, l.insurance_info
FROM Tours t
LEFT JOIN Hotels h ON t.hotel_id = h.id
LEFT JOIN Contacts c ON t.contact_id = c.id
LEFT JOIN Legal_Info l ON t.legal_info_id = l.id
WHERE t.city = @CityName";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CityName", cityName);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listResult.Add(new Tour
                            {
                                Id = reader.GetInt32("id"),
                                Country = reader.GetString("country"),
                                City = reader.GetString("city"),
                                Stars = reader.GetInt32("stars"),
                                Price = reader.GetInt32("price"),
                                ImagePath = reader.IsDBNull(reader.GetOrdinal("image_path")) ? null : reader.GetString("image_path"), // 👈 ДОБАВЛЕНО

                                HotelId = reader.GetInt32("hotel_id"),
                                ContactId = reader.GetInt32("contact_id"),
                                LegalInfoId = reader.GetInt32("legal_info_id"),

                                Hotel = new Hotel
                                {
                                    Id = reader.GetInt32("hotel_id"),
                                    Name = reader.GetString("hotel_name"),
                                    ShortDescription = reader.GetString("short_description"),
                                    Address = reader.GetString("address")
                                },
                                Contact = new Contact
                                {
                                    Id = reader.GetInt32("contact_id"),
                                    PhoneNumber = reader.GetString("phone_number"),
                                    ContactName = reader.GetString("contact_name"),
                                    Email = reader.GetString("email")
                                },
                                LegalInfo = new LegalInfo
                                {
                                    Id = reader.GetInt32("legal_info_id"),
                                    RegistryEntry = reader.GetString("registry_entry"),
                                    CompanyName = reader.GetString("company_name"),
                                    InsuranceInfo = reader.GetString("insurance_info")
                                }
                            });
                        }
                    }
                }
            }

            return listResult;
        }

        public Tour Create(string country, string city, int stars, int price, string imagePath,
                   string hotelName, string hotelShortDescription, string hotelAddress)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                // 1. Создание нового отеля и получение его ID
                // Теперь мы передаем все обязательные поля: name, short_description и address
                int newHotelId = CreateHotel(connection, hotelName, hotelShortDescription, hotelAddress);

                // 2. Получение default ID для других связанных таблиц
                int defaultContactId = GetDefaultId(connection, "Contacts");
                int defaultLegalInfoId = GetDefaultId(connection, "Legal_Info");

                // 3. Создание тура с новым Hotel ID
                string sql = @"
INSERT INTO Tours (country, city, stars, price, image_path, hotel_id, contact_id, legal_info_id)
VALUES (@Country, @City, @Stars, @Price, @ImagePath, @HotelId, @ContactId, @LegalInfoId)
RETURNING id, country, city, stars, price, image_path, hotel_id, contact_id, legal_info_id";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Country", country);
                    command.Parameters.AddWithValue("@City", city);
                    command.Parameters.AddWithValue("@Stars", stars);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@ImagePath", imagePath ?? (object)DBNull.Value);

                    // Используем ID только что созданного отеля
                    command.Parameters.AddWithValue("@HotelId", newHotelId);
                    command.Parameters.AddWithValue("@ContactId", defaultContactId);
                    command.Parameters.AddWithValue("@LegalInfoId", defaultLegalInfoId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Создание объекта Tour с заполненным навигационным свойством Hotel
                            return new Tour
                            {
                                Id = reader.GetInt32("id"),
                                Country = reader.GetString("country"),
                                City = reader.GetString("city"),
                                Stars = reader.GetInt32("stars"),
                                Price = reader.GetInt32("price"),
                                ImagePath = reader.IsDBNull(reader.GetOrdinal("image_path")) ? null : reader.GetString("image_path"),

                                HotelId = reader.GetInt32("hotel_id"),
                                ContactId = reader.GetInt32("contact_id"),
                                LegalInfoId = reader.GetInt32("legal_info_id"),

                                // Заполнение связанного объекта Hotel
                                Hotel = new Hotel()
                                {
                                    Id = newHotelId,
                                    Name = hotelName,
                                    ShortDescription = hotelShortDescription,
                                    Address = hotelAddress
                                },
                                Contact = new Contact(), // Предполагается, что эти объекты заполняются отдельно или имеют дефолтные значения
                                LegalInfo = new LegalInfo()
                            };
                        }
                    }
                }
            }

            throw new Exception("Не получилось создать тур в базе данных");
        }

        /// <summary>
        /// Вставляет новую запись в таблицу Hotels и возвращает ее ID.
        /// </summary>
        private int CreateHotel(NpgsqlConnection connection, string hotelName, string shortDescription, string address)
        {
            string hotelSql = @"
INSERT INTO Hotels (name, short_description, address)
VALUES (@HotelName, @ShortDescription, @Address)
RETURNING id";

            using (var command = new NpgsqlCommand(hotelSql, connection))
            {
                command.Parameters.AddWithValue("@HotelName", hotelName);
                command.Parameters.AddWithValue("@ShortDescription", shortDescription);
                command.Parameters.AddWithValue("@Address", address); // Добавлено обязательное поле 'address'

                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
            }

            throw new Exception("Не удалось создать новый отель в базе данных");
        }

        public Tour UpdatePrice(int tourId, int newPrice)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
UPDATE Tours 
SET price = @NewPrice 
WHERE id = @TourId
RETURNING id, country, city, stars, price, image_path, hotel_id, contact_id, legal_info_id";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@NewPrice", newPrice);
                    command.Parameters.AddWithValue("@TourId", tourId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Tour
                            {
                                Id = reader.GetInt32("id"),
                                Country = reader.GetString("country"),
                                City = reader.GetString("city"),
                                Stars = reader.GetInt32("stars"),
                                Price = reader.GetInt32("price"),
                                ImagePath = reader.IsDBNull(reader.GetOrdinal("image_path")) ? null : reader.GetString("image_path"), // 👈 ДОБАВЛЕНО

                                HotelId = reader.GetInt32("hotel_id"),
                                ContactId = reader.GetInt32("contact_id"),
                                LegalInfoId = reader.GetInt32("legal_info_id"),

                                Hotel = new Hotel(),
                                Contact = new Contact(),
                                LegalInfo = new LegalInfo()
                            };
                        }
                    }
                }
            }

            throw new Exception("Не получилось обновить цену тура");
        }

        public Tour Delete(int tourId)
        {
            // ❗ Delete не читает image_path — не трогаем
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var tourToDelete = GetById(tourId);

                if (tourToDelete == null)
                    throw new Exception($"Тур с ID {tourId} не найден");

                string sql = @"DELETE FROM Tours WHERE id = @IdTour";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdTour", tourId);

                    if (command.ExecuteNonQuery() > 0)
                        return tourToDelete;
                }
            }

            throw new Exception("Не получилось удалить тур");
        }

        private int GetDefaultId(NpgsqlConnection connection, string tableName)
        {
            string sql = $"SELECT id FROM {tableName} ORDER BY id LIMIT 1";

            using (var command = new NpgsqlCommand(sql, connection))
            {
                var result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                    return Convert.ToInt32(result);

                return CreateDefaultRecord(connection, tableName);
            }
        }

        private int CreateDefaultRecord(NpgsqlConnection connection, string tableName)
        {
            string sql = tableName switch
            {
                "Hotels" =>
                    @"INSERT INTO Hotels (name, short_description, address)
                      VALUES ('Отель по умолчанию', 'Базовый отель', 'Адрес')
                      RETURNING id",

                "Contacts" =>
                    @"INSERT INTO Contacts (phone_number, contact_name, email)
                      VALUES ('+79990000000', 'Контакт', 'default@email')
                      RETURNING id",

                "Legal_Info" =>
                    @"INSERT INTO Legal_Info (registry_entry, company_name, insurance_info)
                      VALUES ('REG0001', 'Компания', 'Страховка')
                      RETURNING id",

                _ => throw new Exception("Неизвестная таблица")
            };

            using (var command = new NpgsqlCommand(sql, connection))
                return Convert.ToInt32(command.ExecuteScalar());
        }
    }
}