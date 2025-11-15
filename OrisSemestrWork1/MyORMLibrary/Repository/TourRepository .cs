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
    // MyORMLibrary/Repositories/TourRepository.cs
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

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
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

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Country", country);
                    command.Parameters.AddWithValue("@Stars", stars);
                    command.Parameters.AddWithValue("@Budget", budget);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tour tour = new Tour
                            {
                                Id = reader.GetInt32("id"),
                                Country = reader.GetString("country"),
                                City = reader.GetString("city"),
                                Stars = reader.GetInt32("stars"),
                                Price = reader.GetInt32("price"),
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
                            };
                            listResult.Add(tour);
                        }
                    }
                }
            }

            return listResult;
        }

        public Tour GetById(int tourId)
        {
            // ПЕРЕНЕСЕН ваш код из GetTourById
            Tour resultTour = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
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

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@TourId", tourId);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
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
            // ПЕРЕНЕСЕН ваш код из GetToursByCity
            List<Tour> listResult = new List<Tour>();

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
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

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CityName", cityName);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tour tour = new Tour
                            {
                                Id = reader.GetInt32("id"),
                                Country = reader.GetString("country"),
                                City = reader.GetString("city"),
                                Stars = reader.GetInt32("stars"),
                                Price = reader.GetInt32("price"),
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
                            listResult.Add(tour);
                        }
                    }
                }
            }

            return listResult;
        }

        public Tour Create(string country, string city, int stars, int price)
        {
            // ПЕРЕНЕСЕН ваш код из CreateTour
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                int defaultHotelId = GetDefaultId(connection, "Hotels");
                int defaultContactId = GetDefaultId(connection, "Contacts");
                int defaultLegalInfoId = GetDefaultId(connection, "Legal_Info");

                string sql = @"
INSERT INTO Tours (country, city, stars, price, hotel_id, contact_id, legal_info_id)
VALUES (@Country, @City, @Stars, @Price, @HotelId, @ContactId, @LegalInfoId)
RETURNING id, country, city, stars, price, hotel_id, contact_id, legal_info_id";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Country", country);
                    command.Parameters.AddWithValue("@City", city);
                    command.Parameters.AddWithValue("@Stars", stars);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@HotelId", defaultHotelId);
                    command.Parameters.AddWithValue("@ContactId", defaultContactId);
                    command.Parameters.AddWithValue("@LegalInfoId", defaultLegalInfoId);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
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

            throw new Exception("Не получилось создать тур в базе данных");
        }

        public Tour UpdatePrice(int tourId, int newPrice)
        {
            // ПЕРЕНЕСЕН ваш код из EditTour
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
UPDATE Tours 
SET price = @NewPrice 
WHERE id = @TourId
RETURNING id, country, city, stars, price, hotel_id, contact_id, legal_info_id";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@NewPrice", newPrice);
                    command.Parameters.AddWithValue("@TourId", tourId);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
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
            // ПЕРЕНЕСЕН ваш код из DeleteTour
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                Tour tourToDelete = GetById(tourId);

                if (tourToDelete == null)
                {
                    throw new Exception($"Тур с ID {tourId} не найден");
                }

                string sql = @"DELETE FROM Tours WHERE id = @IdTour";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdTour", tourId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return tourToDelete;
                    }
                }
            }

            throw new Exception("Не получилось удалить тур из базы данных");
        }

        private int GetDefaultId(NpgsqlConnection connection, string tableName)
        {
            // Вспомогательный метод
            string sql = $"SELECT id FROM {tableName} ORDER BY id LIMIT 1";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
            {
                var result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }

                return CreateDefaultRecord(connection, tableName);
            }
        }

        private int CreateDefaultRecord(NpgsqlConnection connection, string tableName)
        {
            // Вспомогательный метод
            string sql;

            switch (tableName)
            {
                case "Hotels":
                    sql = @"
INSERT INTO Hotels (name, short_description, address)
VALUES ('Отель по умолчанию', 'Базовый отель для туров', 'Адрес не указан')
RETURNING id";
                    break;

                case "Contacts":
                    sql = @"
INSERT INTO Contacts (phone_number, contact_name, email)
VALUES ('+79990000000', 'Контакт по умолчанию', 'default@email.com')
RETURNING id";
                    break;

                case "Legal_Info":
                    sql = @"
INSERT INTO Legal_Info (registry_entry, company_name, insurance_info)
VALUES ('REG000001', 'Компания по умолчанию', 'Страховка по умолчанию')
RETURNING id";
                    break;

                default:
                    throw new Exception($"Неизвестная таблица: {tableName}");
            }

            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
            {
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
    }
}
