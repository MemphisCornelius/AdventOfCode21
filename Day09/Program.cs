using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Xsl;

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
                        sum += current + 1;
                    }
                }
            }

            return sum.ToString();
        }

        static string Solve02(int[,] field) {
            throw new NotImplementedException();
        }
    }
}