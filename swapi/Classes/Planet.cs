using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swapi.Classes
{
    public class Planet
    {
        public string Name { get; set; }
        public string Gravity { get; set; }
        public List<Resident> Residents { get; set; } = new List<Resident>();

        public string GetPlanetInfo() 
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Name: {this.Name};   ");
            builder.AppendLine($"Gravity: {this.Gravity};");
            string residents = " ";
            if ( Residents.Count() >0)
            {
                foreach ( Resident res in Residents )
                {
                    residents += res.Name + ", ";
                }
                builder.AppendLine($"residents: {residents}");
            }

            return builder.ToString();

        }
    }
}
