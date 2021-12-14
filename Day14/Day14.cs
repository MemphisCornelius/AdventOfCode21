using System.Numerics;
using System.Text;

namespace Day14 {
    static class Day14 {
        public static void Main(string[] args) {
            List<string> values = new();
            StreamReader sr = new("../../../input.txt");

            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            ParseInput(values, out string polymerTemplate, out Dictionary<string, char> pairInsertion);

            Console.WriteLine($"Aufgabe01: {Solve01(polymerTemplate, pairInsertion)}");

            Console.WriteLine($"Aufgabe02: {Solve02(polymerTemplate, pairInsertion)}");
        }

        private static void ParseInput(List<string> values, out string list, out Dictionary<string, char> dictionary) {
            list = values[0];
            dictionary = new();

            for (int i = 2; i < values.Count; i++) {
                string[] split = values[i].Split(" -> ");

                dictionary.Add(split[0], split[1][0]);
            }
        }

        private static string Solve01(string polymerTemplate, Dictionary<string, char> pairInsertion) {
            StringBuilder sb = new StringBuilder(polymerTemplate);

            for (int step = 0; step < 10; step++) {
                for (int i = 0; i < sb.Length - 1; i += 2) {
                    sb.Insert(i + 1, pairInsertion[(sb[i].ToString() + sb[i + 1].ToString())]);
                }
            }


            Dictionary<char, int> letterCount = new();
            foreach (char c in sb.ToString()) {
                if (!letterCount.ContainsKey(c)) {
                    letterCount.Add(c, 1);
                } else {
                    letterCount[c]++;
                }
            }

            letterCount = letterCount.OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);


            return (letterCount.ElementAt(letterCount.Count - 1).Value - letterCount.ElementAt(0).Value).ToString();
        }

        private static string Solve02(string polymerTemplate, Dictionary<string, char> pairInsertion) {
            Dictionary<string, BigInteger> polymer = new();

            for (int i = 0; i < polymerTemplate.Length - 1; i++) {
                string poly = polymerTemplate[i].ToString() + polymerTemplate[i + 1];
                if (!polymer.ContainsKey(poly)) {
                    polymer.Add(poly, 1);
                } else {
                    polymer[poly]++;
                }
            }

            for (int i = 0; i < 40; i++) {
                Dictionary<string, BigInteger> currentPolymer = new();
                for (int j = 0; j < polymer.Count; j++) {
                    KeyValuePair<string, BigInteger> keyValuePair = polymer.ElementAt(j);

                    string insert = keyValuePair.Key[0].ToString() + pairInsertion[keyValuePair.Key] +
                                    keyValuePair.Key[1];

                    string[] poly = { insert[0..2], insert[1..3] };
                    foreach (string s in poly) {
                        if (!currentPolymer.ContainsKey(s)) {
                            currentPolymer.Add(s, keyValuePair.Value);
                        } else {
                            currentPolymer[s] += keyValuePair.Value;
                        }
                    }
                }

                polymer = currentPolymer.ToDictionary(entry => entry.Key,
                    entry => entry.Value);
            }

            Dictionary<char, BigInteger> letterCount = new();
            foreach (KeyValuePair<string, BigInteger> keyValuePair in polymer) {
                char c = keyValuePair.Key[0];

                if (!letterCount.ContainsKey(c)) {
                    letterCount.Add(c, keyValuePair.Value);
                } else {
                    letterCount[c] += keyValuePair.Value;
                }
            }

            letterCount = letterCount.OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);

            int offset = 0;

            if (letterCount.ElementAt(0).Key == polymerTemplate[^1]) {
                offset = -1;
            }else if (letterCount.ElementAt(letterCount.Count - 1).Key == polymerTemplate[^1]) {
                offset = 1;
            }
            
            return (offset + letterCount.ElementAt(letterCount.Count - 1).Value - letterCount.ElementAt(0).Value).ToString();
        }
    }
}