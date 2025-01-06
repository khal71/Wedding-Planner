using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingPlannerDomain.Entities
{
    public class Location : Item
    {
        public string StreetName { get; set; }
        public string City { get; set; }
        public string Region { get; set; }

        public int PostalCode { get; set; }
        public string Country { get; set; }

        public Location(int id, string name, string type, byte[] imageData, string streetName, string city, string region, int postalCode, string country )
            :base( id, name, type, imageData)   
        {
            StreetName = streetName;
            City = city;    
            Region = region;
            PostalCode = postalCode;
            Country = country;
                    
        }
        public Location()
        {
            
        }
    }
    
}
