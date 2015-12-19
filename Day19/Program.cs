using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day19
{
    class Rule
    {
        public string from;
        public string to;

        public List<string> Apply(string molecule)
        {
            List<string> result = new List<string>();

            int index = molecule.IndexOf(from);
            while (index >= 0)
            {
                string replacement = molecule.Substring(0, index) + to + molecule.Substring(index + from.Length);
                result.Add(replacement);

                index = molecule.IndexOf(from, index + 1);
            }

            return result;
        }

        public static Rule Parse(string x)
        {
            var parts = x.Split(' ');

            return new Rule
            {
                from = parts[0],
                to = parts[2]
            };
        }
    }

    class Program
    {
        static List<string> Tokenize(string x)
        {
            List<string> tokens = new List<string>();
            for (int i = 0; i < x.Length; i++)
            {
                if (i < x.Length - 1 && char.IsLower(x[i + 1]))
                {
                    tokens.Add(x.Substring(i, 2));
                    i++;
                }
                else
                {
                    tokens.Add(x.Substring(i, 1));
                }
            }

            return tokens;
        }

        class ParseInfo
        {
            public int length;
            public int steps;
        }

        static ParseInfo Parse(Dictionary<string, List<List<string>>> rules, string currentRule, List<string> tokens, int index, int depth, int maxDepth)
        {
            var result = new List<ParseInfo>();

            // return when out of tokens
            if (index >= tokens.Count)
            {
                return new ParseInfo();
            }

            // add self
            if (currentRule == tokens[index])
            {
                result.Add(new ParseInfo
                {
                    length = 1
                });
            }

            // return when no rules found or depth has been reached
            if (!rules.ContainsKey(currentRule) || depth > maxDepth)
            {
                if (result.Any())
                {
                    return result.Single();
                }

                return new ParseInfo();
            }

            // apply rules
            var innerRules = rules[currentRule];
            foreach (var innerParts in innerRules)
            {
                bool problem = false;
                int totalSteps = 0;

                int innerIndex = index;
                foreach (var inner in innerParts)
                {
                    var info = Parse(rules, inner, tokens, innerIndex, depth + 1, maxDepth);
                    totalSteps += info.steps;

                    if (info.length == 0)
                    {
                        problem = true;
                        break;
                    }

                    innerIndex += info.length;
                }

                if (!problem)
                {
                    var totalLength = innerIndex - index;
                    result.Add(new ParseInfo
                    {
                        length = totalLength,
                        steps = totalSteps + 1
                    });
                }
            }

            // return best/longest result
            if (result.Any())
            {
                int maxLength = result.Max(pi => pi.length);
                return result.First(pi => pi.length == maxLength);
            }

            return new ParseInfo();
        }

        static void Main(string[] args)
        {
            string input = "Al => ThF\nAl => ThRnFAr\nB => BCa\nB => TiB\nB => TiRnFAr\nCa => CaCa\nCa => PB\nCa => PRnFAr\nCa => SiRnFYFAr\nCa => SiRnMgAr\nCa => SiTh\nF => CaF\nF => PMg\nF => SiAl\nH => CRnAlAr\nH => CRnFYFYFAr\nH => CRnFYMgAr\nH => CRnMgYFAr\nH => HCa\nH => NRnFYFAr\nH => NRnMgAr\nH => NTh\nH => OB\nH => ORnFAr\nMg => BF\nMg => TiMg\nN => CRnFAr\nN => HSi\nO => CRnFYFAr\nO => CRnMgAr\nO => HP\nO => NRnFAr\nO => OTi\nP => CaP\nP => PTi\nP => SiRnFAr\nSi => CaSi\nTh => ThCa\nTi => BP\nTi => TiTi\ne => HF\ne => NAl\ne => OMg";

            string[] ruleStrings = input.Split('\n');
            var rules = ruleStrings.Select(r => Rule.Parse(r)).ToList();

            string molecule = "CRnCaCaCaSiRnBPTiMgArSiRnSiRnMgArSiRnCaFArTiTiBSiThFYCaFArCaCaSiThCaPBSiThSiThCaCaPTiRnPBSiThRnFArArCaCaSiThCaSiThSiRnMgArCaPTiBPRnFArSiThCaSiRnFArBCaSiRnCaPRnFArPMgYCaFArCaPTiTiTiBPBSiThCaPTiBPBSiRnFArBPBSiRnCaFArBPRnSiRnFArRnSiRnBFArCaFArCaCaCaSiThSiThCaCaPBPTiTiRnFArCaPTiBSiAlArPBCaCaCaCaCaSiRnMgArCaSiThFArThCaSiThCaSiRnCaFYCaSiRnFYFArFArCaSiRnFYFArCaSiRnBPMgArSiThPRnFArCaSiRnFArTiRnSiRnFYFArCaSiRnBFArCaSiRnTiMgArSiThCaSiThCaFArPRnFArSiRnFArTiTiTiTiBCaCaSiRnCaCaFYFArSiThCaPTiBPTiBCaSiThSiRnMgArCaF";

            var replacements = rules.SelectMany(r => r.Apply(molecule));
            var uniqueReplacements = replacements.Distinct();
            int count = uniqueReplacements.Count();

            Console.WriteLine("Answer 1: {0}", count);


            // improve rules for faster/easier lookup
            Dictionary<string, List<List<string>>> improvedRules = new Dictionary<string, List<List<string>>>();
            foreach (var r in rules)
            {
                if (!improvedRules.ContainsKey(r.from))
                {
                    improvedRules.Add(r.from, new List<List<string>>());
                }

                improvedRules[r.from].Add(Tokenize(r.to));
            }

            // tokenize molecule
            var tokens = Tokenize(molecule);

            // parse molecule
            int maxDepth = 0;
            ParseInfo info;
            do
            {
                maxDepth++;
                info = Parse(improvedRules, "e", tokens, 0, 0, maxDepth);
            } while (info.length != tokens.Count);

            Console.WriteLine("Answer 2: {0}", info.steps);

            Console.ReadKey();
        }
    }
}
