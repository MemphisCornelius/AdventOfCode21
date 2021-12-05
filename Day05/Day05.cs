using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Day05 {
    internal static class Day05 {
        public static void Main(string[] args) {
            List<string> values = new();
            StreamReader sr = new("../../../input.txt");


            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            Dictionary<Point, Point> lines = ParseInput(values);

            Console.WriteLine($"Aufgabe01: {Solve01(lines)}");
            Console.WriteLine($"Aufgabe02: {Solve01(lines, true)}");
        }

        private static Dictionary<Point, Point> ParseInput(List<string> values) {
            Dictionary<Point, Point> lines = new();
            foreach (string value in values) {
                string[] entrys = value.Split("->");
                string[] start = entrys[0].Split(',');
                string[] end = entrys[1].Split(',');

                lines.Add(new Point(int.Parse(start[0].Trim()), int.Parse(start[1].Trim())),
                    new Point(int.Parse(end[0].Trim()), int.Parse(end[1].Trim())));
            }

            return lines;
        }

        private static string Solve01(Dictionary<Point, Point> lines, bool sol2 = false) {
            int[,] diagram = new int[1000, 1000];
            int counter = 0;

            foreach (Point point in lines.Keys) {
                if (point.X == lines[point].X) {
                    int length = Math.Abs(lines[point].Y - point.Y);
                    int lower = point.Y < lines[point].Y ? point.Y : lines[point].Y;

                    for (int y = lower; y <= lower + length; y++) {
                        diagram[y, point.X]++;
                    }
                } else if (point.Y == lines[point].Y) {
                    int length = Math.Abs(lines[point].X - point.X);
                    int lower = point.X < lines[point].X ? point.X : lines[point].X;

                    for (int x = lower; x <= lower + length; x++) {
                        diagram[point.Y, x]++;
                    }
                } else if (sol2) {
                    int lenghtY = Math.Abs(lines[point].Y - point.Y);
                    Point lowerPoint = point.Y < lines[point].Y ? point : lines[point];
                    Point higherPoint = point.Y > lines[point].Y ? point : lines[point];

                    int direction = lowerPoint.X < higherPoint.X ? 1 : -1;

                    for (int y = lowerPoint.Y, x = lowerPoint.X;
                        y <= lowerPoint.Y + lenghtY;
                        y++, x += direction) {
                        diagram[y, x]++;
                    }
                }
            }

            for (int i = 0; i < diagram.GetLength(0); i++) {
                for (int j = 0; j < diagram.GetLength(1); j++) {
                    if (diagram[i, j] >= 2) {
                        counter++;
                    }
                }
            }

            return counter.ToString();
        }
    }
}