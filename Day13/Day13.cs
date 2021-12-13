using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;

namespace Day13 {
    static class Day13 {
        public static void Main(string[] args) {
            List<string> values = new();
            StreamReader sr = new("../../../input.txt");

            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            List<(int, int )> paper = new();

            ParseInput(values, paper, out List<string> instructions);

            Console.WriteLine($"Aufgabe01: {Solve01(paper, instructions)}");

            Console.WriteLine($"Aufgabe02: {Solve02(paper, instructions)}");
        }

        private static string Solve02(List<(int x, int y)> paper, List<string> instructions) {
            foreach (string instruction in instructions) {
                string[] split = instruction.Split('=');
                string direction = split[0];
                int value = int.Parse(split[1]);

                Fold(paper, direction, value);
            }

            bool[,] array = new bool[6, 40];
            foreach ((int x, int y) point in paper) {
                array[point.y, point.x] = true;
            }

            for (int i = 0; i < array.GetLength(0); i++) {
                for (int j = 0; j < array.GetLength(1); j++) {
                    if (array[i, j]) {
                        Console.Write('#');
                    } else {
                        Console.Write(' ');
                    }
                }

                Console.WriteLine("");
            }

            return "";
        }

        private static string Solve01(List<(int x, int y)> paper, List<string> instructions) {
            string[] split = instructions[0].Split('=');
            string direction = split[0];
            int value = int.Parse(split[1]);

            Fold(paper, direction, value);

            instructions.RemoveAt(0);

            return paper.Count.ToString();
        }

        private static void Fold(List<(int x, int y)> paper, string direction, int value) {
            if (direction == "x") {
                for (int i = 0; i < paper.Count; i++) {
                    (int x, int y) tuple = paper[i];
                    if (tuple.x > value) {
                        int differenceToEdge = tuple.x - value;

                        (int x, int y) newPoint = (value - differenceToEdge, tuple.y);

                        if (!paper.Contains(newPoint)) {
                            paper.Add(newPoint);
                        }

                        paper.Remove(tuple);
                        i--;
                    }
                }
            } else if (direction == "y") {
                for (int i = 0; i < paper.Count; i++) {
                    (int x, int y) tuple = paper[i];
                    if (tuple.y > value) {
                        int differenceToEdge = tuple.y - value;

                        (int x, int y) newPoint = (tuple.x, value - differenceToEdge);

                        if (!paper.Contains(newPoint)) {
                            paper.Add(newPoint);
                        }

                        paper.Remove(tuple);
                        i--;
                    }
                }
            } else {
                Console.WriteLine("HELP!");
            }
        }

        private static void ParseInput(List<string> values, List<(int x, int y)> paper, out List<string> list) {
            list = new();

            foreach (string s in values) {
                if (s == "") continue;
                if (s.StartsWith("fold along")) {
                    list.Add(s.Replace("fold along ", ""));
                } else {
                    string[] split = s.Split(',');
                    paper.Add((int.Parse(split[0]), int.Parse(split[1])));
                }
            }
        }
    }
}