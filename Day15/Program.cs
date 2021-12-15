using System.Drawing;

namespace Day14 {
    static class Day14 {
        public static void Main(string[] args) {
            List<string> values = new();
            StreamReader sr = new("../../../input.txt");

            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            ParseInput(values, out List<(Point, Dictionary<Point, int>)> graph);

            //Console.WriteLine($"Aufgabe01: {Solve01()}");

            //Console.WriteLine($"Aufgabe02: {Solve02()}");
        }

        private static void ParseInput(List<string> values, out List<(Point, Dictionary<Point, int>)> graph) {
            int[,] map = new int[100, 100];
            graph = new();

            for (int i = 0; i < map.GetLength(0); i++) {
                char[] m = values[i].ToCharArray();
                for (int j = 0; j < map.GetLength(1); j++) {
                    map[i, j] = m[j] - '0';
                }
            }

            for (int i = 0; i < map.GetLength(0); i++) {
                for (int j = 0; j < map.GetLength(1); j++) {
                    Dictionary<Point, int> follow = new();
                    
                    if (i != 0) {
                        follow.Add(new Point(i-1, j), map[i-1, j]);
                    }
                    if (j != 0) {
                        follow.Add(new Point(i, j-1), map[i, j-1]);
                    }
                    if (i != map.GetLength(0) - 1) {
                        follow.Add(new Point(i+1, j), map[i+1, j]);
                    }
                    if (j != map.GetLength(1) -1) {
                        follow.Add(new Point(i, j+1), map[i, j+1]);
                    }
                    
                    graph.Add((new Point(i, j), follow));
                }
            }
        }
        
    }
}