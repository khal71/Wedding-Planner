using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingPlannerDomain.Entities
{
    public class Food : Item
    {
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }

      
        public Food(int id, string name, string type, byte[] imageData, bool vegeterian, bool vegan)
            :base(id,name,type, imageData)
        {
            Vegetarian = vegeterian;
            Vegan = vegan;
        }
        public Food()
        {

        }
    }
}
