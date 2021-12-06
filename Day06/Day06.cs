using System;
using System.Collections.Generic;
using System.IO;

namespace Day06 {
    internal static class Day06 {
        public static void Main(string[] args) {
            List<string> values = new();
            StreamReader sr = new("../../../input.txt");
            
            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            List<int> fishes = ParseInput(values);

            Console.WriteLine($"Aufgabe01: {Solve01(fishes)}");
            Console.WriteLine($"Aufgabe02: {Solve02(fishes)}");
        }

        private static List<int> ParseInput(List<string> values) {
            string[] str = values[0].Split(',');
            List<int> list = new();
            foreach (string s in str) {
                list.Add(int.Parse(s));
            }

            return list;
        }

        private static string Solve01(List<int> fishes) {
            List<int> fish = fishes.GetRange(0, fishes.Count);
            for (int i = 0; i < 80; i++) {
                int fishesCount = fish.Count;
                for (int j = 0; j < fishesCount; j++) {
                    if (fish[j] == 0) {
                        fish.Add(8);
                        fish[j] = 6;
                    } else {
                        fish[j]--;
                    }
                }
            }

            return fish.Count.ToString();
        }

        private static string Solve02(List<int> fishes) {
            List<int> fish = fishes.GetRange(0, fishes.Count);
            long[] fishAges = new long[9];

            foreach (int i in fish) {
                fishAges[i]++;
            }

            for (int i = 0; i < 256; i++) {
                long pregnant = fishAges[0];
                for (int j = 1; j < fishAges.Length; j++) {
                    fishAges[j - 1] = fishAges[j];
                }

                fishAges[6] += pregnant;
                fishAges[8] = pregnant;
            }

            long fishCounter = 0;
            foreach (long number in fishAges) {
                fishCounter += number;
            }

            return fishCounter.ToString();
        }
    }
}