using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrisSemestrWork1.MyORMLibrary.Models
{
    public class Hotel
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        // Навигационное свойство (один отель может быть в многих турах)
        public List<Tour> Tours { get; set; } = new List<Tour>();
    }
}
