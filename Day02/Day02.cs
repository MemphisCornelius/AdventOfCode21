using System;
using System.IO;

namespace Day02 {
    internal static class Day02 {
        public static void Main(string[] args) {
            StreamReader sr = new("../../../input.txt");
            string[] dirs = new string[1000];
            int[] values = new int[1000];

            int i = 0;
            while (!sr.EndOfStream) {
                string s = sr.ReadLine()!;
                string[] sa = s.Split(' ');
                dirs[i] = sa[0];
                values[i] = int.Parse(sa[1]);
                i++;
            }
            sr.Close();

            Console.WriteLine($"Aufgabe01: {Solve01(dirs, values)}");
            Console.WriteLine($"Aufgabe02: {Solve02(dirs, values)}");
        }

        private static int Solve01(string[] dirs, int[] values) {
            int horizontal = 0, depth = 0;

            for (int i = 0; i < dirs.Length; i++) {
                switch (dirs[i]) {
                    case "forward":
                        horizontal += values[i];
                        break;
                    case "down":
                        depth += values[i];
                        break;
                    case "up":
                        depth -= values[i];
                        break;
                }
            }

            return horizontal * depth;
        }

        private static int Solve02(string[] dirs, int[] values) {
            int horizontal = 0, depth = 0, aim = 0;

            for (int i = 0; i < dirs.Length; i++) {
                switch (dirs[i]) {
                    case "forward":
                        horizontal += values[i];
                        depth += aim * values[i];
                        break;
                    case "down":
                        aim += values[i];
                        break;
                    case "up":
                        aim -= values[i];
                        break;
                }
            }

            return horizontal * depth;
        }
    }
}