using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day22
{
    enum Player
    {
        Hero,
        Boss
    }

    class Info
    {
        public Player winner;
        public int manaUsed;
        public List<string> actions = new List<string>();
    }

    class Program
    {
        static Info Simulate(bool hard, Player who, int heroHP, int heroMana, int bossHP, int bossDamage, int shield = 0, int poison = 0, int recharge = 0, int availableMana = int.MaxValue)
        {
            // apply effects
            int heroArmor = 0;
            if (shield > 0)
            {
                int shieldArmor = 7;
                heroArmor += shieldArmor;
                shield--;
            }

            if (poison > 0)
            {
                int poisonDamage = 3;
                bossHP -= poisonDamage;
                if (bossHP <= 0)
                {
                    return new Info
                    {
                        winner = Player.Hero
                    };
                }
                poison--;
            }

            if (recharge > 0)
            {
                int rechargeMana = 101;
                heroMana += rechargeMana;
                recharge--;
            }

            // boss his turn
            if (who == Player.Boss)
            {
                int damage = bossDamage - heroArmor;
                if (damage < 1)
                {
                    damage = 1;
                }

                heroHP -= damage;
                if (heroHP <= 0)
                {
                    return new Info
                    {
                        winner = Player.Boss
                    };
                }

                return Simulate(hard, Player.Hero, heroHP, heroMana, bossHP, bossDamage, shield, poison, recharge, availableMana);
            }

            // hero his turn ...
            if (hard)
            {
                heroHP--;
                if (heroHP <= 0)
                {
                    return new Info
                    {
                        winner = Player.Boss
                    };
                }
            }

            Info best = null;

            // ... with magic missile 
            int magicMissileManaCost = 53;
            if (magicMissileManaCost <= availableMana && magicMissileManaCost <= heroMana)
            {
                int magicMissileDamage = 4;
                int newBossHP = bossHP - magicMissileDamage;
                if (newBossHP <= 0)
                {
                    best = new Info
                    {
                        winner = Player.Hero,
                        manaUsed = magicMissileManaCost,
                        actions = new List<string> { "Magic Missile" }
                    };
                }
                else
                {
                    int newHeroMana = heroMana - magicMissileManaCost;
                    int newAvailableMana = availableMana - magicMissileManaCost;
                    Info info = Simulate(hard, Player.Boss, heroHP, newHeroMana, newBossHP, bossDamage, shield, poison, recharge, newAvailableMana);

                    if (info.winner == Player.Hero)
                    {
                        info.manaUsed += magicMissileManaCost;
                        info.actions.Insert(0, "Magic Missile");

                        best = info;
                    }
                }
            }

            if (best != null)
            {
                availableMana = best.manaUsed - 1;
            }

            // ... with drain
            int drainManaCost = 73;
            if (drainManaCost <= availableMana && drainManaCost <= heroMana)
            {
                int drainDamage = 2;
                int drainHealing = 2;
                int newBossHP = bossHP - drainDamage;
                int newHeroHP = heroHP + drainHealing;
                if (newBossHP <= 0)
                {
                    best = new Info
                    {
                        winner = Player.Hero,
                        manaUsed = drainManaCost,
                        actions = new List<string> { "Drain" }
                    };
                }
                else
                {
                    int newHeroMana = heroMana - drainManaCost;
                    int newAvailableMana = availableMana - drainManaCost;
                    Info info = Simulate(hard, Player.Boss, newHeroHP, newHeroMana, newBossHP, bossDamage, shield, poison, recharge, newAvailableMana);

                    if (info.winner == Player.Hero)
                    {
                        info.manaUsed += drainManaCost;
                        info.actions.Insert(0, "Drain");

                        best = info;
                    }
                }
            }

            if (best != null)
            {
                availableMana = best.manaUsed - 1;
            }

            // ... with shield
            int shieldManaCost = 113;
            if (shieldManaCost <= availableMana && shieldManaCost <= heroMana && shield == 0)
            {
                int shieldDuration = 6;
                int newHeroMana = heroMana - shieldManaCost;
                int newAvailableMana = availableMana - shieldManaCost;
                Info info = Simulate(hard, Player.Boss, heroHP, newHeroMana, bossHP, bossDamage, shieldDuration, poison, recharge, newAvailableMana);

                if (info.winner == Player.Hero)
                {
                    info.manaUsed += shieldManaCost;
                    info.actions.Insert(0, "Shield");

                    best = info;
                }
            }

            if (best != null)
            {
                availableMana = best.manaUsed - 1;
            }

            // ... with poison
            int poisonManaCost = 173;
            if (poisonManaCost <= availableMana && poisonManaCost <= heroMana && poison == 0)
            {
                int poisonDuration = 6;
                int newHeroMana = heroMana - poisonManaCost;
                int newAvailableMana = availableMana - poisonManaCost;
                Info info = Simulate(hard, Player.Boss, heroHP, newHeroMana, bossHP, bossDamage, shield, poisonDuration, recharge, newAvailableMana);

                if (info.winner == Player.Hero)
                {
                    info.manaUsed += poisonManaCost;
                    info.actions.Insert(0, "Poison");

                    best = info;
                }
            }

            if (best != null)
            {
                availableMana = best.manaUsed - 1;
            }

            // ... with recharge
            int rechargeManaCost = 229;
            if (rechargeManaCost <= availableMana && rechargeManaCost <= heroMana && recharge == 0)
            {
                int rechargeDuration = 5;
                int newHeroMana = heroMana - rechargeManaCost;
                int newAvailableMana = availableMana - rechargeManaCost;
                Info info = Simulate(hard, Player.Boss, heroHP, newHeroMana, bossHP, bossDamage, shield, poison, rechargeDuration, newAvailableMana);

                if (info.winner == Player.Hero)
                {
                    info.manaUsed += rechargeManaCost;
                    info.actions.Insert(0, "Recharge");

                    best = info;
                }
            }

            if (best == null)
            {
                return new Info
                {
                    winner = Player.Boss
                };
            }

            return best;
        }

        static void Main(string[] args)
        {
            int heroHP = 50;
            int heroMana = 500;

            int bossHP = 55;
            int bossDamage = 8;

            bool hard1 = false;
            Info info1 = Simulate(hard1, Player.Hero, heroHP, heroMana, bossHP, bossDamage);
            int minManaUsed1 = info1.manaUsed;

            Console.WriteLine("Answer 1: {0}", minManaUsed1);


            bool hard2 = true;
            Info info2 = Simulate(hard2, Player.Hero, heroHP, heroMana, bossHP, bossDamage);
            int minManaUsed2 = info2.manaUsed;

            Console.WriteLine("Answer 2: {0}", minManaUsed2);

            Console.ReadKey();
        }
    }
}
