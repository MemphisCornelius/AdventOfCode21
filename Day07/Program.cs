using System;
using System.Collections.Generic;
using System.IO;

namespace Day07 {
    internal static class Day07 {
        public static void Main(string[] args) {
            List<string> values = new();
            StreamReader sr = new("../../../input.txt");

            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            List<int> crabPositions = ParseInput(values);

            Console.WriteLine($"Aufgabe01: {Solve01(crabPositions)}");
            Console.WriteLine($"Aufgabe02: {Solve02(crabPositions)}");
        }

        private static List<int> ParseInput(List<string> values) {
            string[] str = values[0].Split(',');
            List<int> list = new();
            foreach (string s in str) {
                list.Add(int.Parse(s));
            }

            return list;
        }

        private static string Solve01(List<int> crabPositions) {
            List<int> sortedPositions = crabPositions.GetRange(0, crabPositions.Count);
            sortedPositions.Sort();

            int minFuel = Int32.MaxValue;

            for (int i = sortedPositions[0]; i < sortedPositions[^1]; i++) {
                int fuel = 0;
                sortedPositions.ForEach(k => fuel += Math.Abs(k - i));

                minFuel = fuel < minFuel ? fuel : minFuel;
            }

            return minFuel.ToString();
        }

        private static string Solve02(List<int> crabPositions) {
            List<int> sortedPositions = crabPositions.GetRange(0, crabPositions.Count);
            sortedPositions.Sort();

            int minFuel = Int32.MaxValue;

            for (int i = sortedPositions[0]; i < sortedPositions[^1]; i++) {
                int fuel = 0;

                sortedPositions.ForEach(k => {
                    int steps = Math.Abs(k - i);

                    fuel += (int) (0.5 * steps * (steps + 1));
                });

                minFuel = fuel < minFuel ? fuel : minFuel;
            }

            return minFuel.ToString();
        }
    }
}