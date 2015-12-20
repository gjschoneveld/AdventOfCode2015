using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day20
{
    class Program
    {
        static List<int> Visitors(int house, int maxHouses = int.MaxValue)
        {
            List<int> result = new List<int>();

            for (int i = 1; i <= Math.Sqrt(house); i++)
            {
                if (house % i == 0)
                {
                    int j = house / i;

                    if (j <= maxHouses)
                    {
                        result.Add(i);
                    }

                    if (i <= maxHouses)
                    {
                        result.Add(j);
                    }
                }
            }

            return result.Distinct().ToList();
        }

        static void Main(string[] args)
        {
            int requiredPresents = 36000000;

            int house1 = 0;
            int presents;
            do
            {
                house1++;
                var visitors = Visitors(house1);
                presents = visitors.Sum() * 10;
            } while (presents < requiredPresents);

            Console.WriteLine("Answer 1: {0}", house1);


            int house2 = 0;
            do
            {
                house2++;
                var visitors = Visitors(house2, 50);
                presents = visitors.Sum() * 11;
            } while (presents < requiredPresents);

            Console.WriteLine("Answer 2: {0}", house2);

            Console.ReadKey();
        }
    }
}
