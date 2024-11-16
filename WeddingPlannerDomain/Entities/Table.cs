using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingPlannerDomain.Entities
{
    public class Table: Item
    {
        public string Size { get; set; }
        public int NrOfChairs { get; set; }
        public Table(int id, string name, string type, byte[] imageData, string size, int nrOfChairs)
            :base(id,name,type, imageData)
        {
            Size = size;
            NrOfChairs = nrOfChairs;
        }
    }
}
