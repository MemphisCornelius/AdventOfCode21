using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Day08 {
    internal static class Day08 {
        public static void Main(string[] args) {
            List<string> values = new();
            StreamReader sr = new("../../../test.txt");

            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            Dictionary<List<string>, List<string>> parsed = ParseInput(values);

            Console.WriteLine($"Aufgabe01: {Solve01(parsed)}");
            Console.WriteLine($"Aufgabe02: {Solve02(parsed)}");
        }

        private static Dictionary<List<string>, List<string>> ParseInput(List<string> values) {
            Dictionary<List<string>, List<string>> parsed = new();

            foreach (string value in values) {
                string[] split = value.Split(" | ");
                List<string> notes = new();
                List<string> output = new();

                notes.AddRange(split[0].Split(' '));
                output.AddRange(split[1].Split(' '));

                parsed.Add(notes, output);
            }

            return parsed;
        }

        private static string Solve01(Dictionary<List<string>, List<string>> values) {
            int counter = 0;

            foreach (List<string> val in values.Values) {
                val.ForEach(s => {
                    if (s.Length is 2 or 3 or 4 or 7) {
                        counter++;
                    }
                });
            }

            return counter.ToString();
        }

        //does not work and i dont know why
        private static string Solve02(Dictionary<List<string>, List<string>> values) {
            int sum = 0;

            foreach (KeyValuePair<List<string>, List<string>> keyValuePair in values) {
                Dictionary<string, int> sevenSegmentToDecimal = SevenSegmentToDecimal(keyValuePair.Key);

                List<string> listSorted = new();

                keyValuePair.Value.ForEach(s => listSorted.Add(SortString(s)));


                string number = "";
                foreach (string s in listSorted) {
                    number += sevenSegmentToDecimal[s];
                }

                sum += int.Parse(number);
            }

            return sum.ToString();
        }
        
        private static Dictionary<string, int> SevenSegmentToDecimal(List<string> displays) {
            Dictionary<string, int> map = new();

            for (var i = 0; i < displays.Count; i++) {
                var s = displays[i];
                switch (s.Length) {
                    case 2: // 1
                        map.Add(s, 1);
                        displays.Remove(s);
                        i--;
                        break;
                    case 4: // 4
                        map.Add(s, 4);
                        displays.Remove(s);
                        i--;
                        break;
                    case 3: // 7
                        map.Add(s, 7);
                        displays.Remove(s);
                        i--;
                        break;
                    case 7: // 8
                        map.Add(s, 8);
                        displays.Remove(s);
                        i--;
                        break;
                }
            }

            for (var i = 0; i < displays.Count; i++) {
                var s = displays[i];
                switch (Difference(s, KeyByValue(map, 1)).Length, Difference(s, KeyByValue(map, 4)).Length) {
                    case (4, 4):
                        map.Add(s, 0);
                        displays.Remove(s);
                        i--;
                        break;
                    case (5, 5):
                        map.Add(s, 2);
                        displays.Remove(s);
                        i--;
                        break;
                    case (3, 3):
                        map.Add(s, 3);
                        displays.Remove(s);
                        i--;
                        break;
                    case (5, 3):
                        map.Add(s, 5);
                        displays.Remove(s);
                        i--;
                        break;
                    case (6, 4):
                        map.Add(s, 6);
                        displays.Remove(s);
                        i--;
                        break;
                    case (4, 2):
                        map.Add(s, 9);
                        displays.Remove(s);
                        i--;
                        break;
                }
            }

            Dictionary<string, int> mapSorted = new();

            foreach (KeyValuePair<string, int> keyValuePair in map) {
                mapSorted.Add(SortString(keyValuePair.Key), keyValuePair.Value);
            }

            return mapSorted;
        }

        static string Difference(string a, string b) {
            string difference = "";
            string lamps = "abcdefg";

            foreach (char c in lamps) {
                if (a.Contains(c) != b.Contains(c)) {
                    difference += c;
                }
            }

            return difference;
        }

        private static T KeyByValue<T, W>(this Dictionary<T, W> dict, W val) {
            T key = default;
            foreach (KeyValuePair<T, W> pair in dict) {
                if (EqualityComparer<W>.Default.Equals(pair.Value, val)) {
                    key = pair.Key;
                    break;
                }
            }

            return key;
        }

        private static string SortString(string s) {
            string output = "";
            for (int i = 'a'; i <= 'g'; i++) {
                if (s.Contains((char)i)) {
                    output += (char)i;
                }
            }

            return output;
        }
    }
}