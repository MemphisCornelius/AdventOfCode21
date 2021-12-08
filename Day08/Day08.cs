using System;
using System.Collections.Generic;
using System.IO;

namespace Day08 {
    internal static class Day08 {
        public static void Main(string[] args) {
            List<string> values = new();
            StreamReader sr = new("../../../input.txt");

            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            List<string> notes = ParseInput(values, out List<string> outputs);


            Console.WriteLine($"Aufgabe01: {Solve01(notes, outputs)}");
            Console.WriteLine($"Aufgabe02: {Solve02(notes, outputs)}");
        }

        private static List<string> ParseInput(List<string> values, out List<string> outputs) {
            List<string> notes = new();
            outputs = new();

            foreach (string value in values) {
                string[] split = value.Split(" | ");
                notes.AddRange(split[0].Split(' '));
                outputs.AddRange(split[1].Split(' '));
            }

            return notes;
        }

        private static string Solve01(List<string> notes, List<string> outputs) {
            int counter = 0;

            outputs.ForEach(s => {
                if (s.Length is 2 or 3 or 4 or 7) {
                    counter++;
                }
            });

            return counter.ToString();
        }


        private static string Solve02(List<string> notes, List<string> outputs) {
            throw new NotImplementedException();
        }
    }
}