using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Day17
{
    class Program
    {
        static List<int> GetCombination(List<int> choices, int number)
        {
            var bits = new BitArray(new int[] { number });

            return choices.Where((v, i) => bits[i]).ToList();
        }

        static void Main(string[] args)
        {
            string input = "11\n30\n47\n31\n32\n36\n3\n1\n5\n3\n32\n36\n15\n11\n46\n26\n28\n1\n19\n3";

            string[] choiceStrings = input.Split('\n');
            List<int> choices = choiceStrings.Select(c => int.Parse(c)).ToList();

            int combinationCount = 1 << choices.Count;
            var combinations = Enumerable.Range(0, combinationCount).Select(i => GetCombination(choices, i));

            int neededSum = 150;
            var correctCombinations = combinations.Where(c => c.Sum() == neededSum).ToList();
            var count1 = correctCombinations.Count();

            Console.WriteLine("Answer 1: {0}", count1);


            int minimumCount = correctCombinations.Min(c => c.Count);
            var count2 = correctCombinations.Count(c => c.Count == minimumCount);

            Console.WriteLine("Answer 2: {0}", count2);

            Console.ReadKey();
        }
    }
}
