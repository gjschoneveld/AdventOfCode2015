using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day10
{
    class Program
    {
        static string Encode(string x)
        {
            StringBuilder result = new StringBuilder();

            char runCharStart = '\0';
            char runChar = runCharStart;
            int lengthSoFar = 0;

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != runChar)
                {
                    if (runChar != runCharStart)
                    {
                        result.Append(lengthSoFar);
                        result.Append(runChar);
                    }

                    runChar = x[i];
                    lengthSoFar = 0;
                }

                lengthSoFar++;
            }

            result.Append(lengthSoFar);
            result.Append(runChar);

            return result.ToString();
        }

        static void Main(string[] args)
        {
            string input = "1113122113";

            int times = 40;

            string temp = input;
            for (int i = 0; i < times; i++)
            {
                temp = Encode(temp);
            }

            Console.WriteLine("Answer 1: {0}", temp.Length);


            times = 50;

            temp = input;
            for (int i = 0; i < times; i++)
            {
                temp = Encode(temp);
            }

            Console.WriteLine("Answer 2: {0}", temp.Length);

            Console.ReadKey();
        }
    }
}
