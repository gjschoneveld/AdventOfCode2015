using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day24
{
    class Program
    {
        static List<List<int>> GetCombinations(List<int> choices, HashSet<int> used, int neededValue, int numberOfItems, int startIndex = 0)
        {
            if (numberOfItems == 1)
            {
                int maxSoFar = int.MinValue;
                if (used.Any())
                {
                    maxSoFar = used.Max();
                }

                if (neededValue > maxSoFar && choices.Contains(neededValue))
                {
                    return new List<List<int>> { new List<int> { neededValue } };
                }

                return null;
            }

            List<List<int>> results = new List<List<int>>();
            for (int i = startIndex; i < choices.Count; i++)
            {
                var choice = choices[i];

                if (used.Contains(choice))
                {
                    continue;
                }

                if (neededValue <= choice)
                {
                    continue;
                }

                used.Add(choice);

                var innerResults = GetCombinations(choices, used, neededValue - choice, numberOfItems - 1, i + 1);
                if (innerResults != null)
                {
                    foreach (var inner in innerResults)
                    {
                        inner.Insert(0, choice);
                        results.Add(inner);
                    }
                }

                used.Remove(choice);
            }

            if (results.Any())
            {
                return results;
            }

            return null;
        }

        static bool IsValid(List<int> choices, int groups, List<int> combi)
        {
            if (groups == 2)
            {
                return true;
            }

            List<int> leftovers = choices.Except(combi).ToList();

            var inner = GetValidCombinationsWithLowestAmountOfItems(leftovers, groups - 1);
            if (inner != null)
            {
                return true;
            }

            return false;
        }

        static List<List<int>> GetValidCombinationsWithLowestAmountOfItems(List<int> choices, int groups)
        {
            int maxDepth = choices.Count / groups;
            int depth = 0;
            List<List<int>> validResults = new List<List<int>>();
            do
            {
                depth++;
                var combis = GetCombinations(choices, new HashSet<int>(), choices.Sum() / groups, depth);

                if (combis != null)
                {
                    // validity check for completeness, although never seen an invalid one
                    // replace next line with "validResults = combis;" for speed up
                    validResults = combis.Where(c => IsValid(choices, groups, c)).ToList();
                }

            } while (depth <= maxDepth && !validResults.Any());

            if (validResults.Any())
            {
                return validResults;
            }

            return null;
        }

        static long QuantumEntanglement(List<int> combi)
        {
            long result = combi.Aggregate(1L, (a, b) => a * b);

            return result;
        }

        static void Main(string[] args)
        {
            string input = "1\n2\n3\n7\n11\n13\n17\n19\n23\n31\n37\n41\n43\n47\n53\n59\n61\n67\n71\n73\n79\n83\n89\n97\n101\n103\n107\n109\n113";

            List<int> choices = input.Split('\n').Select(s => int.Parse(s)).ToList();

            int groups1 = 3;
            var combis1 = GetValidCombinationsWithLowestAmountOfItems(choices, groups1);
            long answer1 = combis1.Min(c => QuantumEntanglement(c));

            Console.WriteLine("Answer 1: {0}", answer1);


            int groups2 = 4;
            var combis2 = GetValidCombinationsWithLowestAmountOfItems(choices, groups2);
            long answer2 = combis2.Min(c => QuantumEntanglement(c));

            Console.WriteLine("Answer 2: {0}", answer2);

            Console.ReadKey();
        }
    }
}
