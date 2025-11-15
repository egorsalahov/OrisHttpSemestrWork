using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrisSemestrWork1.MyORMLibrary.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Навигационное свойство
        public List<Tour> Tours { get; set; } = new List<Tour>();
    }
}
