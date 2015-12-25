using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Day25_second_try
{
    class Program
    {
        static int ModMult(int x, int y, int mod)
        {
            return (int)((long)x * y % mod);
        }

        static int ModPower(int b, int e, int mod)
        {
            BitArray ebits = new BitArray(new int[] { e });

            int res = 1;
            int x = b;

            for (int i = 0; i < ebits.Count; i++)
            {
                if (ebits[i])
                {
                    res = ModMult(res, x, mod);
                }
                x = ModMult(x, x, mod);
            }

            return res;
        }

        static void Main(string[] args)
        {
            int neededRow = 2981;
            int neededColumn = 3075;

            int diagonal = neededRow + neededColumn - 1;

            int number = (diagonal * diagonal - diagonal) / 2 + neededColumn;

            int c = 20151125;
            int b = 252533;
            int mod = 33554393;

            int tmp = ModPower(b, number - 1, mod);
            int res = ModMult(c, tmp, mod);

            Console.WriteLine("Answer: {0}", res);

            Console.ReadKey();
        }
    }
}
