using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day14
{
    class Reindeer
    {
        public string name;

        int speed;
        int busyTime;
        int restTime;

        public int points;

        public int DistanceAfter(int seconds)
        {
            int periodTime = busyTime + restTime;

            int fullPeriods = seconds / periodTime;
            int remainingTime = seconds % periodTime;

            int totalBusyTime;
            if (remainingTime <= busyTime)
            {
                totalBusyTime = fullPeriods * busyTime + remainingTime;
            }
            else
            {
                totalBusyTime = (fullPeriods + 1) * busyTime;
            }

            int distance = totalBusyTime * speed;

            return distance;
        }

        public static Reindeer Parse(string x)
        {
            var parts = x.Split(' ');

            Reindeer result = new Reindeer
            {
                name = parts[0],
                speed = int.Parse(parts[3]),
                busyTime = int.Parse(parts[6]),
                restTime = int.Parse(parts[13]),
            };

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input = "Vixen can fly 19 km/s for 7 seconds, but then must rest for 124 seconds.\nRudolph can fly 3 km/s for 15 seconds, but then must rest for 28 seconds.\nDonner can fly 19 km/s for 9 seconds, but then must rest for 164 seconds.\nBlitzen can fly 19 km/s for 9 seconds, but then must rest for 158 seconds.\nComet can fly 13 km/s for 7 seconds, but then must rest for 82 seconds.\nCupid can fly 25 km/s for 6 seconds, but then must rest for 145 seconds.\nDasher can fly 14 km/s for 3 seconds, but then must rest for 38 seconds.\nDancer can fly 3 km/s for 16 seconds, but then must rest for 37 seconds.\nPrancer can fly 25 km/s for 6 seconds, but then must rest for 143 seconds.";

            string[] reindeerStrings = input.Split('\n');

            var reindeers = reindeerStrings.Select(rs => Reindeer.Parse(rs)).ToList();


            int totalSeconds = 2503;

            int max1 = reindeers.Max(r => r.DistanceAfter(totalSeconds));

            Console.WriteLine("Answer 1: {0}", max1);


            for (int second = 1; second <= totalSeconds; second++)
            {
                int max = reindeers.Max(r => r.DistanceAfter(second));
                var topReindeers = reindeers.Where(r => r.DistanceAfter(second) == max);

                foreach (var topper in topReindeers)
                {
                    topper.points++;
                }
            }

            int max2 = reindeers.Max(r => r.points);

            Console.WriteLine("Answer 2: {0}", max2);

            Console.ReadKey();
        }
    }
}
