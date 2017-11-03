using System;
using System.Collections.Generic;
using System.Linq;

namespace PoisonedApples
{
    class Program
    {
        static void Main(string[] args)
        {
            var apples = PickApples().Take(10000).ToList();

            var poisonedAppleColours = apples.Where(a => a.Poisoned).Select(a => a.Colour).ToList();

            var poisonedApplesCount = poisonedAppleColours.Count();
            Console.WriteLine(poisonedApplesCount.ToString());

            var poisonedGreenApplesCount = poisonedAppleColours.Where(c => c == "Green").ToList().Count;
            Console.WriteLine(poisonedGreenApplesCount.ToString());

            var poisonedYellowApplesCount = poisonedAppleColours.Where(c => c == "Yellow").ToList().Count;
            Console.WriteLine(poisonedYellowApplesCount.ToString());

            var counter = 0;
            var maxConsecRedNonpoisonedApples =
                    PickApples().Take(10000).Zip(
                        PickApples().Skip(1),
                        (a, b) => a.Colour == "Red" && b.Colour == "Red" && !a.Poisoned && !b.Poisoned
                        ? (counter == 0 ? counter = 2 : ++counter)
                        : counter = 0).Max();
            Console.WriteLine(maxConsecRedNonpoisonedApples.ToString());

            var pairsofGreenApplesCount =
                    PickApples().Take(10000).Zip(
                        PickApples().Skip(1),
                        (a, b) => a.Colour == "Green" && b.Colour == "Green")
                        .Where(x => x)
                        .ToList().Count;
            Console.WriteLine(pairsofGreenApplesCount.ToString());

            var maxConsecRedNonpoisonedApplesWithTuple =
                apples.Aggregate(new Tuple<int, int>(0, 0), (acc, apple) => !apple.Poisoned && apple.Colour == "Red"
                ? new Tuple<int, int>(acc.Item1 + 1, Math.Max(acc.Item1 + 1, acc.Item2))
                : new Tuple<int, int>(0, acc.Item2)).Item2;
            Console.WriteLine(maxConsecRedNonpoisonedApplesWithTuple);
        }

        public static string GetColour(int colourIndex)
        {
            if (colourIndex % 13 == 0 || colourIndex % 29 == 0)
            {
                return "Green";
            }

            if (colourIndex % 11 == 0 || colourIndex % 19 == 0)
            {
                return "Yellow";
            }

            return "Red";
        }

        public static IEnumerable<Apple> PickApples()
        {
            int colourIndex = 1;
            int poisonIndex = 7;

            while (true)
            {
                yield return new Apple
                {
                    Colour = GetColour(colourIndex),
                    Poisoned = poisonIndex % 41 == 0
                };

                colourIndex += 5;
                poisonIndex += 37;
            }
        }
    }
}
