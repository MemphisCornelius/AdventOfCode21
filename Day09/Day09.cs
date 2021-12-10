using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Day09 {
    internal static class Day09 {
        public static void Main(string[] args) {
            List<string> values = new();
            StreamReader sr = new("../../../input.txt");

            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            int[,] parsed = ParseInput(values);

            Console.WriteLine($"Aufgabe01: {Solve01(parsed)}");
            Console.WriteLine($"Aufgabe02: {Solve02(parsed)}");
        }

        private static int[,] ParseInput(List<string> values) {
            int[,] parsed = new int[100, 100];

            for (int i = 0; i < values.Count; i++) {
                char[] value = values[i].ToCharArray();
                for (int j = 0; j < value.Length; j++) {
                    parsed[i, j] = (value[j] - '0');
                }
            }

            return parsed;
        }

        
        static string Solve01(int[,] field) {
            int sum = 0;
            
            Lowpoints(field).ForEach(p => sum += 1 + field[p.X, p.Y]);

            return sum.ToString();
        }
        
        static List<Point> Lowpoints(int[,] field) {
            List<Point> list = new();

            for (int i = 0; i < field.GetLength(0); i++) {
                for (int j = 0; j < field.GetLength(1); j++) {
                    int up, right, down, left, current;
                    (up, right, down, left, current) = (int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, field[i,j]);

                    if (i != 0) {
                        up = field[i - 1, j];
                    }

                    if (i != field.GetLength(0) - 1) {
                        down = field[i + 1, j];
                    }

                    if (j != 0) {
                        left = field[i, j - 1];
                    }

                    if (j != field.GetLength(1) -1 ) {
                        right = field[i, j + 1];

                    }

                    if (current < up && current < down && current < left && current < right) {
                        list.Add(new Point(i, j));
                    }
                }
            }

            return list;
        }

        static string Solve02(int[,] field) {
            int[,,] fieldWithCheck = new int[field.GetLength(0), field.GetLength(1), 2];
            List<Point> lowpoints = Lowpoints(field);
            List<int> basinsizes = new();

            for (int i = 0; i < fieldWithCheck.GetLength(0); i++) {
                for (int j = 0; j < fieldWithCheck.GetLength(1); j++) {
                    fieldWithCheck[i, j, 0] = field[i, j];
                }
            }

            foreach (Point point in lowpoints) {
                
                    int tiefe = -1;
                    basinsizes.Add(CheckBasin(fieldWithCheck, point.X, point.Y));
                
            }

            basinsizes.Sort();

            return (basinsizes[^1] * 
                    basinsizes[^2] * 
                    basinsizes[^3]).ToString();
        }

        private static int CheckBasin(int[,,] field, int i, int j) {
            int basinSize = 0;
            int current = field[i, j, 0];
            
            field[i, j, 1] = 1;
            
            if (field[i, j, 0] == 9) {
                return 0;
            }
            
            if (i != 0) {
                if (field[i - 1, j, 1] == 0 && 
                    field[i - 1, j, 0] + 1 == current) {
                    //if (field[i - 1, j, 2] != 0) {
                    //    basinSize += CheckBasin(field, i - 1, j);
                    //} else {
                    //    field[i, j, 2] = 1;
                    //    basinSize += 1 + CheckBasin(field, i - 1, j);
                    //}
                    basinSize += 1 + CheckBasin(field, i - 1, j);
                }
            }

            if (i != field.GetLength(0) - 1) {
                if (field[i + 1, j, 1] == 0 &&
                    field[i + 1, j, 0] + 1 == current) {
                    //if (field[i + 1, j, 2] != 0) {
                    //    basinSize += CheckBasin(field, i + 1, j;
                    //} else {
                    //    field[i, j, 2] = 1;
                    //    basinSize += 1 + CheckBasin(field, i + 1, j);
                    //}
                    basinSize += 1 + CheckBasin(field, i + 1, j);
                }
            }

            if (j != 0) {
                if (field[i, j - 1, 1] == 0 &&
                    field[i, j - 1, 0] + 1 == current) {
                    //if (field[i , j-1, 2] != 0) {
                    //    basinSize += CheckBasin(field, i, j-1);
                    //} else {
                    //    field[i, j, 2] = 1;
                    //    basinSize += 1 + CheckBasin(field, i, j-1);
                    //}
                    basinSize += 1 + CheckBasin(field, i, j-1);
                }
            }

            if (j != field.GetLength(1) -1 ) {
                if (field[i, j + 1, 1] == 0 &&
                    field[i, j + 1, 0] + 1 == current) {
                    //if (field[i , j+1, 2] != 0) {
                    //    basinSize += CheckBasin(field, i, j+1);
                    //} else {
                    //    field[i, j, 2] = 1;
                    //    basinSize += 1 + CheckBasin(field, i, j+1);
                    //}
                    basinSize += 1 + CheckBasin(field, i, j+1);
                } 

            }

            return basinSize;
        }
    }
}