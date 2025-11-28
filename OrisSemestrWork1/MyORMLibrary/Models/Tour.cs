using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrisSemestrWork1.MyORMLibrary.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int Stars { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; } = string.Empty;


        // Внешние ключи
        public int HotelId { get; set; }
        public int ContactId { get; set; }  
        public int LegalInfoId { get; set; }

        // Навигационные свойства (для связей)
        public Hotel Hotel { get; set; } = null!;
        public Contact Contact { get; set; } = null!;
        public LegalInfo LegalInfo { get; set; } = null!;
    }
}
