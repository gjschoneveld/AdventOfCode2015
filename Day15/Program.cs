using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day15
{
    class Ingredient
    {
        public string name;

        public int capacity;
        public int durability;
        public int flavor;
        public int texture;
        public int calories;

        public static Ingredient Parse(string x)
        {
            var parts = x.Split(' ');

            Ingredient result = new Ingredient
            {
                name = parts[0].Trim(':'),
                capacity = int.Parse(parts[2].Trim(',')),
                durability = int.Parse(parts[4].Trim(',')),
                flavor = int.Parse(parts[6].Trim(',')),
                texture = int.Parse(parts[8].Trim(',')),
                calories = int.Parse(parts[10]),
            };

            return result;
        }
    }

    class Program
    {
        static IEnumerable<List<int>> GetAllCombinations(int size, int value)
        {
            if (size == 1)
            {
                yield return new List<int> { value };
                yield break;
            }

            for (int i = 0; i <= value; i++)
            {
                var innerCombinations = GetAllCombinations(size - 1, value - i);
                foreach (var inner in innerCombinations)
                {
                    inner.Add(i);
                    yield return inner;
                }
            }
        }

        static int GetMaximum(List<Ingredient> ingredients, int teaspoons, int? amountOfCalories = null)
        {
            int maxTotal = int.MinValue;

            var combinations = GetAllCombinations(ingredients.Count, teaspoons);
            foreach (var combi in combinations)
            {
                // determine totals
                int totalCapacity = 0;
                int totalDurability = 0;
                int totalFlavor = 0;
                int totalTexture = 0;
                int totalCalories = 0;

                for (int i = 0; i < combi.Count; i++)
                {
                    var currentIngredient = ingredients[i];
                    var count = combi[i];

                    totalCapacity += count * currentIngredient.capacity;
                    totalDurability += count * currentIngredient.durability;
                    totalFlavor += count * currentIngredient.flavor;
                    totalTexture += count * currentIngredient.texture;
                    totalCalories += count * currentIngredient.calories;
                }

                // skip if not the right amount of calories
                if (amountOfCalories.HasValue && totalCalories != amountOfCalories.Value)
                {
                    continue;
                }

                // calculate grand total
                int total;
                if (totalCapacity < 0 || totalDurability < 0 || totalFlavor < 0 || totalTexture < 0)
                {
                    total = 0;
                }
                else
                {
                    total = totalCapacity * totalDurability * totalFlavor * totalTexture;
                }

                // see if it's the maximum
                maxTotal = Math.Max(maxTotal, total);
            }

            return maxTotal;
        }

        static void Main(string[] args)
        {
            string input = "Sprinkles: capacity 2, durability 0, flavor -2, texture 0, calories 3\nButterscotch: capacity 0, durability 5, flavor -3, texture 0, calories 3\nChocolate: capacity 0, durability 0, flavor 5, texture -1, calories 8\nCandy: capacity 0, durability -1, flavor 0, texture 5, calories 8";

            string[] ingredientStrings = input.Split('\n');
            List<Ingredient> ingredients = ingredientStrings.Select(i => Ingredient.Parse(i)).ToList();

            int teaspoons = 100;
            int max1 = GetMaximum(ingredients, teaspoons);

            Console.WriteLine("Answer 1: {0}", max1);


            int totalCalories = 500;
            int max2 = GetMaximum(ingredients, teaspoons, totalCalories);

            Console.WriteLine("Answer 2: {0}", max2);

            Console.ReadKey();
        }
    }
}
