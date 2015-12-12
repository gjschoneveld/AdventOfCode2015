using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day11
{
    class Program
    {
        static bool IsValid(string x)
        {
            // straight of three
            bool hasStraight = false;
            for (int i = 0; i < x.Length - 2; i++)
			{
                char c0 = x[i];
                char c1 = x[i + 1];
                char c2 = x[i + 2];

			    if (c1 - c0 == 1 && c2 - c1 == 1) 
                {
                    hasStraight = true;
                    break;
                }
			}

            // no forbidden i o l
            bool forbiddenChars = x.Contains('i') || x.Contains('o') || x.Contains('l');

            // at least two distinct pairs
            int pairs = 0;
            for (int i = 0; i < x.Length - 1; i++)
			{
                char c0 = x[i];
                char c1 = x[i + 1];

                if (c0 == c1) 
                {
                    pairs++;
                    i++;
                }
            }

            bool twoPairs = pairs >= 2;

            return hasStraight && !forbiddenChars && twoPairs;
        }

        static string Next(string x)
        {
            var chars = x.ToCharArray();

            int index = chars.Length - 1;
            bool increment = true;

            while (increment)
            {
                if (chars[index] == 'z')
                {
                    chars[index] = 'a';
                    index--;
                }
                else
                {
                    chars[index]++;
                    increment = false;
                }
            }

            return new string(chars);
        }

        static string NextValid(string input)
        {
            string next = Next(input);
            while (!IsValid(next))
            {
                next = Next(next);
            }

            return next;
        }

        static void Main(string[] args)
        {
            string input = "hxbxwxba";

            string next = NextValid(input);

            Console.WriteLine("Answer 1: {0}", next);


            next = NextValid(next);

            Console.WriteLine("Answer 2: {0}", next);

            Console.ReadKey();
        }
    }
}
