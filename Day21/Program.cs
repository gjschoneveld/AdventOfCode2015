using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day21
{
    class Item
    {
        public string name;

        public int cost;
        public int damage;
        public int armor;

        public override string ToString()
        {
            return name;
        }

        public static Item Parse(string x)
        {
            var parts = x.Split('\t');

            var result = new Item
            {
                name = parts[0],
                cost = int.Parse(parts[1]),
                damage = int.Parse(parts[2]),
                armor = int.Parse(parts[3])
            };

            return result;
        }
    }

    class Configuration
    {
        public List<Item> items;

        public int Cost()
        {
            return items.Sum(i => i.cost);
        }

        public int Damage()
        {
            return items.Sum(i => i.damage);
        }

        public int Armor()
        {
            return items.Sum(i => i.armor);
        }

        private static List<Configuration> GetWeaponConfigurations(List<Item> weapons)
        {
            List<Configuration> result = new List<Configuration>();

            // single weapon
            foreach (Item weapon in weapons)
            {
                result.Add(new Configuration
                {
                    items = new List<Item> { weapon }
                });
            }

            return result;
        }

        private static List<Configuration> GetArmorConfigurations(List<Item> armor)
        {
            List<Configuration> result = new List<Configuration>();

            // no armor
            result.Add(new Configuration
            {
                items = new List<Item>()
            });

            // single armor
            foreach (Item a in armor)
            {
                result.Add(new Configuration
                {
                    items = new List<Item> { a }
                });
            }

            return result;
        }

        private static List<Configuration> GetRingConfigurations(List<Item> rings)
        {
            List<Configuration> result = new List<Configuration>();

            // no rings
            result.Add(new Configuration
            {
                items = new List<Item>()
            });

            for (int i = 0; i < rings.Count; i++)
            {
                // single ring
                result.Add(new Configuration
                {
                    items = new List<Item> { rings[i] }
                });

                for (int j = i + 1; j < rings.Count; j++)
                {
                    // two rings
                    result.Add(new Configuration
                    {
                        items = new List<Item> { rings[i], rings[j] }
                    });
                }
            }

            return result;
        }

        private static List<Configuration> CombineConfigurations(List<List<Configuration>> configurations)
        {
            if (configurations.Count == 1)
            {
                return configurations.First();
            }

            List<Configuration> result = new List<Configuration>();
            var innerConfigs = CombineConfigurations(configurations.Take(configurations.Count - 1).ToList());
            var lastConfigs = configurations.Last();
            foreach (var configA in innerConfigs)
            {
                foreach (var configB in lastConfigs)
                {
                    var combinedItems = new List<Item>();
                    combinedItems.AddRange(configA.items);
                    combinedItems.AddRange(configB.items);

                    result.Add(new Configuration
                    {
                        items = combinedItems
                    });
                }
            }

            return result;
        }

        public static List<Configuration> GetAllConfigurations(List<Item> weapons, List<Item> armor, List<Item> rings)
        {
            var weaponConfigs = GetWeaponConfigurations(weapons);
            var armorConfigs = GetArmorConfigurations(armor);
            var ringConfigs = GetRingConfigurations(rings);

            var result = CombineConfigurations(new List<List<Configuration>> 
            {
                weaponConfigs,
                armorConfigs,
                ringConfigs
            });

            return result;
        }
    }

    class Program
    {
        static int RoundsTillDeath(int hitpoints, int armor, int damage)
        {
            int roundDamage = damage - armor;
            if (roundDamage < 1)
            {
                roundDamage = 1;
            }

            int rounds = hitpoints / roundDamage;
            int remainder = hitpoints % roundDamage;
            if (remainder > 0)
            {
                rounds++;
            }

            return rounds;
        }

        static void Main(string[] args)
        {
            string[] weaponStrings = {
                "Dagger\t8\t4\t0",
                "Shortsword\t10\t5\t0",
                "Warhammer\t25\t6\t0",
                "Longsword\t40\t7\t0",
                "Greataxe\t74\t8\t0"
            };

            string[] armorStrings = {
                "Leather\t13\t0\t1",
                "Chainmail\t31\t0\t2",
                "Splintmail\t53\t0\t3",
                "Bandedmail\t75\t0\t4",
                "Platemail\t102\t0\t5"
            };

            string[] ringStrings = {
                "Damage +1\t25\t1\t0",
                "Damage +2\t50\t2\t0",
                "Damage +3\t100\t3\t0",
                "Defense +1\t20\t0\t1",
                "Defense +2\t40\t0\t2",
                "Defense +3\t80\t0\t3"
            };

            List<Item> weapons = weaponStrings.Select(s => Item.Parse(s)).ToList();
            List<Item> armor = armorStrings.Select(s => Item.Parse(s)).ToList();
            List<Item> rings = ringStrings.Select(s => Item.Parse(s)).ToList();

            var configs = Configuration.GetAllConfigurations(weapons, armor, rings);

            int myHitPoints = 100;

            int bossHitPoints = 109;
            int bossDamage = 8;
            int bossArmor = 2;

            int minCostWinning = int.MaxValue;
            int maxCostLosing = int.MinValue;
            foreach (var conf in configs)
            {
                int myDeath = RoundsTillDeath(myHitPoints, conf.Armor(), bossDamage);
                int bossDeath = RoundsTillDeath(bossHitPoints, bossArmor, conf.Damage());

                bool myVictory = myDeath >= bossDeath;
                if (myVictory)
                {
                    minCostWinning = Math.Min(minCostWinning, conf.Cost());
                }
                else
                {
                    maxCostLosing = Math.Max(maxCostLosing, conf.Cost());
                }
            }

            Console.WriteLine("Answer 1: {0}", minCostWinning);
            Console.WriteLine("Answer 2: {0}", maxCostLosing);

            Console.ReadKey();
        }
    }
}
