using System;

namespace Day01 {
    internal static class Day01 {
        public static void Main(string[] args) {
            string[] lines = System.IO.File.ReadAllLines("../../..//input.txt");

            Console.WriteLine($"Aufgabe01: {Solve01(lines)}");
            Console.WriteLine($"Aufgabe02: {Solve02(lines)}");
        }

        static string Solve01(string[] lines) {
            int counter = 0;
            for (int i = 0; i < lines.Length - 1; i++) {
                if (int.Parse(lines[i]) < int.Parse(lines[i + 1])) {
                    counter++;
                }
            }

            return counter.ToString();
        }

        static string Solve02(string[] lines) {
            int counter = 0;
            for (int i = 0; i < lines.Length - 3; i++) {
                if (int.Parse(lines[i]) + int.Parse(lines[i + 1]) + int.Parse(lines[i + 2]) <
                    (int.Parse(lines[i + 1]) + int.Parse(lines[i + 2]) + int.Parse(lines[i + 3]))) {
                    counter++;
                }
            }

            return counter.ToString();
        }
    }
}