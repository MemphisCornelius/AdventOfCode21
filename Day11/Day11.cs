using System;
using System.Collections.Generic;
using System.IO;

namespace Day11 {
    static class Day11 {
        public static void Main(string[] args) {
            List<string> values = new();
            StreamReader sr = new("../../../input.txt");

            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            int[,] energyLevels = ParseInput(values);

            Console.WriteLine($"Aufgabe01: {Solve01(energyLevels)}");


            Console.WriteLine($"Aufgabe02: {Solve02(energyLevels)}");
        }


        private static int[,] ParseInput(List<string> values) {
            int[,] parsed = new int[10, 10];

            for (int i = 0; i < values.Count; i++) {
                char[] split = values[i].ToCharArray();
                for (int j = 0; j < split.Length; j++) {
                    parsed[i, j] = split[j] - '0';
                }
            }

            return parsed;
        }

        private static string Solve01(int[,] energyLevels) {
            int flashes = 0;

            for (int step = 0; step < 100; step++) {
                int height = energyLevels.GetLength(0);
                int width = energyLevels.GetLength(1);
                
                for (int i = 0; i < height; i++) {
                    for (int j = 0; j < width; j++) {
                        UpdateAdjacent(energyLevels, i, j);
                    }
                }


                for (int i = 0; i < height; i++) {
                    for (int j = 0; j < width; j++) {
                        if (energyLevels[i, j] > 9) {
                            flashes++;
                            energyLevels[i, j] = 0;
                        }
                    }
                }
            }

            return flashes.ToString();
        }

        private static string Solve02(int[,] energyLevels) {
            int counter = 100;
            while (!AllZero(energyLevels)) {
                counter++;
                
                int height = energyLevels.GetLength(0);
                int width = energyLevels.GetLength(1);
                
                for (int i = 0; i < height; i++) {
                    for (int j = 0; j < width; j++) {
                        UpdateAdjacent(energyLevels, i, j);
                    }
                }


                for (int i = 0; i < height; i++) {
                    for (int j = 0; j < width; j++) {
                        if (energyLevels[i, j] > 9) {
                            energyLevels[i, j] = 0;
                        }
                    }
                }
            }

            return counter.ToString();
        }

        private static void UpdateAdjacent(int[,] energyLevels, int i, int j) {
            if (++energyLevels[i, j] == 10) {
                foreach (var (item1, item2) in
                    Neighbours(i, j, energyLevels.GetLength(0), energyLevels.GetLength(1))) {
                    UpdateAdjacent(energyLevels, item1, item2);
                }
            }
        }

        private static List<(int, int)> Neighbours(int i, int j, int width, int height) {
            List<(int, int)> neighbours = new();

            if (i != 0) {
                neighbours.Add((i - 1, j));
            }

            if (i != height - 1) {
                neighbours.Add((i + 1, j));
            }

            if (j != 0) {
                neighbours.Add((i, j - 1));
            }

            if (j != width - 1) {
                neighbours.Add((i, j + 1));
            }

            if (i != 0 && j != 0) {
                neighbours.Add((i - 1, j - 1));
            }

            if (i != height - 1 && j != 0) {
                neighbours.Add((i + 1, j - 1));
            }

            if (i != 0 && j != width - 1) {
                neighbours.Add((i - 1, j + 1));
            }

            if (i != height - 1 && j != width - 1) {
                neighbours.Add((i + 1, j + 1));
            }

            return neighbours;
        }

        private static bool AllZero(int[,] energyLevels) {
            for (int i = 0; i < energyLevels.GetLength(0); i++) {
                for (int j = 0; j < energyLevels.GetLength(1); j++) {
                    if (energyLevels[i, j] != 0) {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}