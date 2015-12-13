using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day13
{
    class Rule
    {
        public Tuple <string,string> persons;
        public int happinessGain;

        public static Rule Parse(string x)
        {
            var parts = x.Split(' ', '.');

            int happinessGain = int.Parse(parts[3]);
            bool lose = parts[2] == "lose";
            if (lose)
            {
                happinessGain = -happinessGain;
            }

            Rule result = new Rule
            {
                persons = new Tuple<string,string>(parts[0], parts[10]),
                happinessGain = happinessGain
            };

            return result;
        }
    }

    class Program
    {
        static List<List<string>> GetAllPermutations(List<string> items)
        {
            List<List<string>> result = new List<List<string>>();

            foreach (var item in items)
            {
                var others = items.Where(i => i != item).ToList();
                var otherPermutations = GetAllPermutations(others);

                foreach (var otherPerm in otherPermutations)
                {
                    otherPerm.Add(item);
                    result.Add(otherPerm);
                }
            }

            if (!result.Any())
            {
                result.Add(new List<string>());
            }

            return result;
        }

        static int GetTotalHappiness(List<string> arrangement)
        {
            int total = 0;

            for (int i = 0; i < arrangement.Count; i++)
            {
                int leftIndex = (i + arrangement.Count - 1) % arrangement.Count;
                int rightIndex = (i + 1) % arrangement.Count;

                string me = arrangement[i];
                string left = arrangement[leftIndex];
                string right = arrangement[rightIndex];

                total += happiness[new Tuple<string, string>(me, left)];
                total += happiness[new Tuple<string, string>(me, right)];
            }

            return total;
        }

        static Dictionary<Tuple<string, string>, int> happiness;

        static void Main(string[] args)
        {
            string input = "Alice would gain 2 happiness units by sitting next to Bob.\nAlice would gain 26 happiness units by sitting next to Carol.\nAlice would lose 82 happiness units by sitting next to David.\nAlice would lose 75 happiness units by sitting next to Eric.\nAlice would gain 42 happiness units by sitting next to Frank.\nAlice would gain 38 happiness units by sitting next to George.\nAlice would gain 39 happiness units by sitting next to Mallory.\nBob would gain 40 happiness units by sitting next to Alice.\nBob would lose 61 happiness units by sitting next to Carol.\nBob would lose 15 happiness units by sitting next to David.\nBob would gain 63 happiness units by sitting next to Eric.\nBob would gain 41 happiness units by sitting next to Frank.\nBob would gain 30 happiness units by sitting next to George.\nBob would gain 87 happiness units by sitting next to Mallory.\nCarol would lose 35 happiness units by sitting next to Alice.\nCarol would lose 99 happiness units by sitting next to Bob.\nCarol would lose 51 happiness units by sitting next to David.\nCarol would gain 95 happiness units by sitting next to Eric.\nCarol would gain 90 happiness units by sitting next to Frank.\nCarol would lose 16 happiness units by sitting next to George.\nCarol would gain 94 happiness units by sitting next to Mallory.\nDavid would gain 36 happiness units by sitting next to Alice.\nDavid would lose 18 happiness units by sitting next to Bob.\nDavid would lose 65 happiness units by sitting next to Carol.\nDavid would lose 18 happiness units by sitting next to Eric.\nDavid would lose 22 happiness units by sitting next to Frank.\nDavid would gain 2 happiness units by sitting next to George.\nDavid would gain 42 happiness units by sitting next to Mallory.\nEric would lose 65 happiness units by sitting next to Alice.\nEric would gain 24 happiness units by sitting next to Bob.\nEric would gain 100 happiness units by sitting next to Carol.\nEric would gain 51 happiness units by sitting next to David.\nEric would gain 21 happiness units by sitting next to Frank.\nEric would gain 55 happiness units by sitting next to George.\nEric would lose 44 happiness units by sitting next to Mallory.\nFrank would lose 48 happiness units by sitting next to Alice.\nFrank would gain 91 happiness units by sitting next to Bob.\nFrank would gain 8 happiness units by sitting next to Carol.\nFrank would lose 66 happiness units by sitting next to David.\nFrank would gain 97 happiness units by sitting next to Eric.\nFrank would lose 9 happiness units by sitting next to George.\nFrank would lose 92 happiness units by sitting next to Mallory.\nGeorge would lose 44 happiness units by sitting next to Alice.\nGeorge would lose 25 happiness units by sitting next to Bob.\nGeorge would gain 17 happiness units by sitting next to Carol.\nGeorge would gain 92 happiness units by sitting next to David.\nGeorge would lose 92 happiness units by sitting next to Eric.\nGeorge would gain 18 happiness units by sitting next to Frank.\nGeorge would gain 97 happiness units by sitting next to Mallory.\nMallory would gain 92 happiness units by sitting next to Alice.\nMallory would lose 96 happiness units by sitting next to Bob.\nMallory would lose 51 happiness units by sitting next to Carol.\nMallory would lose 81 happiness units by sitting next to David.\nMallory would gain 31 happiness units by sitting next to Eric.\nMallory would lose 73 happiness units by sitting next to Frank.\nMallory would lose 89 happiness units by sitting next to George.";

            //string input = "Alice would gain 54 happiness units by sitting next to Bob.\nAlice would lose 79 happiness units by sitting next to Carol.\nAlice would lose 2 happiness units by sitting next to David.\nBob would gain 83 happiness units by sitting next to Alice.\nBob would lose 7 happiness units by sitting next to Carol.\nBob would lose 63 happiness units by sitting next to David.\nCarol would lose 62 happiness units by sitting next to Alice.\nCarol would gain 60 happiness units by sitting next to Bob.\nCarol would gain 55 happiness units by sitting next to David.\nDavid would gain 46 happiness units by sitting next to Alice.\nDavid would lose 7 happiness units by sitting next to Bob.\nDavid would gain 41 happiness units by sitting next to Carol.";

            string[] ruleStrings = input.Split('\n');

            var rules = ruleStrings.Select(rs => Rule.Parse(rs));

            // put rules in dictionary for faster/easier lookup
            happiness = rules.ToDictionary(r => r.persons, r => r.happinessGain);

            // get all unique persons and generate all permutations 
            var persons1 = happiness.Select(h => h.Key.Item1).Distinct().ToList();
            var permutations1 = GetAllPermutations(persons1);

            // get the maximum happiness
            var max1 = permutations1.Max(p => GetTotalHappiness(p));

            Console.WriteLine("Answer 1: {0}", max1);


            // add happiness rules for self
            string me = "me";
            foreach (var p in persons1)
            {
                happiness.Add(new Tuple<string,string>(me, p), 0);
                happiness.Add(new Tuple<string,string>(p, me), 0);
            }

            // add me to unique persons and generate all permutations 
            var persons2 = new List<string>(persons1);
            persons2.Add(me);
            var permutations2 = GetAllPermutations(persons2);

            // get the maximum happiness
            var max2 = permutations2.Max(p => GetTotalHappiness(p));

            Console.WriteLine("Answer 2: {0}", max2);

            Console.ReadKey();
        }
    }
}
