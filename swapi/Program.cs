using swapi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swapi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------Swapi Service----------");
            int PlanetId = 1;
            var client = new SwapiService();
            Console.WriteLine($"{PlanetId}."+ client.GetPlanet(PlanetId).Result.GetPlanetInfo());

            while (true) 
            {
                Console.WriteLine(" Use 'next','back' or 'exit' ");
                var input = Console.ReadLine().ToLower();
                switch (input)
                {
                   
                    case "next":
                        PlanetId++;
                        break;
                    case "exit":
                        return;

                    case "back" when PlanetId > 1:
                        PlanetId--;
                        break;

                    case "back" when PlanetId == 1:
                        Console.WriteLine("You're on the first page! Use 'next' to go forward.");
                        continue;

                    default:
                        Console.WriteLine("Unknown  comand");
                        break;
                }
                Console.WriteLine($"{PlanetId}." + client.GetPlanet(PlanetId).Result.GetPlanetInfo());
            }
        }
    }
}
