using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingPlannerDomain
{
    public class Flower : Item
    {
        public string Color { get; set; }

        public Flower(int id, string name, string type, string color)
            : base(id, name, type) 
        {
            Color = color;
        }
    }
}
