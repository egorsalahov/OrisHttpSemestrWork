using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrisSemestrWork1.MyORMLibrary.Models
{
    public class LegalInfo
    {
        public int Id { get; set; }
        public string RegistryEntry { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string InsuranceInfo { get; set; } = string.Empty;

        // Навигационное свойство
        public List<Tour> Tours { get; set; } = new List<Tour>();
    
    }
}
