using System.Diagnostics;
using System.Drawing;

namespace Day15 {
    static class Day15 {
        public static void Main(string[] args) {
            List<string> values = new();
            StreamReader sr = new("../../../input.txt");

            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            ParseInput(values, out Dictionary<Point, Dictionary<Point, int>> graph);

            Stopwatch sw = new();
            sw.Start();
            string a1 = Solve01(graph);
            sw.Stop();
            Console.WriteLine($"Aufgabe01: {a1} in {sw.ElapsedMilliseconds}ms");

            sw.Reset();
            sw.Start();
            string a2 = Solve02(graph);
            sw.Stop();
            Console.WriteLine($"Aufgabe02: {a2} in {sw.ElapsedMilliseconds}ms");
        }

        private static void ParseInput(List<string> values, out Dictionary<Point, Dictionary<Point, int>> graph) {
            int[,] map = new int[values[0].Length, values.Count];
            int[,] biggerMap = new int[map.GetLength(0) * 5, map.GetLength(1) * 5];
            graph = new();
            

            for (int i = 0; i < map.GetLength(0); i++) {
                char[] m = values[i].ToCharArray();
                for (int j = 0; j < map.GetLength(1); j++) {
                    map[i, j] = m[j] - '0';
                }
            }

            for (int i = 0; i < map.GetLength(0); i++) {
                for (int j = 0; j < map.GetLength(1); j++) {
                    for (int k = 0; k < 5; k++) {
                        for (int l = 0; l < 5; l++) {
                            int value = map[i, j] + k + l;
                            value -= value >= 10 ? 9 : 0;
                            biggerMap[i + k * values[0].Length, j + l * values.Count] = value;
                        }
                    }
                }
            }


            for (int i = 0; i < biggerMap.GetLength(0); i++) {
                for (int j = 0; j < biggerMap.GetLength(1); j++) {
                    Dictionary<Point, int> follow = new();

                    if (i != 0) {
                        follow.Add(new Point(i - 1, j), biggerMap[i - 1, j]);
                    }

                    if (j != 0) {
                        follow.Add(new Point(i, j - 1), biggerMap[i, j - 1]);
                    }

                    if (i != biggerMap.GetLength(0) - 1) {
                        follow.Add(new Point(i + 1, j), biggerMap[i + 1, j]);
                    }

                    if (j != biggerMap.GetLength(1) - 1) {
                        follow.Add(new Point(i, j + 1), biggerMap[i, j + 1]);
                    }

                    graph.Add(new Point(i, j), follow);
                }
            }
        }

        private static string Solve01(Dictionary<Point, Dictionary<Point, int>> graph) {
            
            return Dijkstra(graph, 99, 99).ToString();
        }
        
        private static string Solve02(Dictionary<Point, Dictionary<Point, int>> graph) {
            return Dijkstra(graph, 499, 499).ToString();
        }

        static int Dijkstra(Dictionary<Point, Dictionary<Point, int>> graph, int endX, int endY) {
            Dictionary<Point, int> distanceFromStart = new();
            Queue<Point> queue = new();
            queue.Enqueue(new Point(0, 0));

            foreach (KeyValuePair<Point, Dictionary<Point, int>> node in graph) {
                distanceFromStart.Add(node.Key, int.MaxValue);
            }

            distanceFromStart[new Point(0, 0)] = 0;

            while (queue.Count > 0) {
                Point current = queue.Dequeue();

                foreach (KeyValuePair<Point, int> follow in graph[current]) {
                    int distance = (distanceFromStart[current] + follow.Value);

                    if (distance < distanceFromStart[follow.Key]) {
                        distanceFromStart[follow.Key] = distance;

                        queue.Enqueue(follow.Key);
                    }
                }
            }

            return distanceFromStart[new Point(endX, endY)];
        }
    }
}