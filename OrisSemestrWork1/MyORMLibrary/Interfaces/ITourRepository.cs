using OrisSemestrWork1.MyORMLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrisSemestrWork1.MyORMLibrary.Interfaces
{
    public interface ITourRepository
    {
        List<Tour> GetByFilter(string country, int stars, int budget);
        Tour GetById(int tourId);
        List<Tour> GetByCity(string cityName);
        Tour Create(string country, string city, int stars, int price);
        Tour UpdatePrice(int tourId, int newPrice);
        Tour Delete(int tourId);
    }
}
