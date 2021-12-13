using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12 {
    static class Day12 {
        public static void Main(string[] args) {
            StreamReader sr = new StreamReader("../../../input.txt");
            List<string> values = new();

            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            ParseInput(values, out Dictionary<string, List<string>> graph);

            Console.WriteLine($"Aufgabe01: {Solve01(graph)}");
            Console.WriteLine($"Aufgabe02: {Solve02(graph)}");
        }

        private static void ParseInput(List<string> values, out Dictionary<string, List<string>> graph) {
            graph = new();
            foreach (string value in values) {
                string[] split = value.Split('-');

                if (!graph.ContainsKey(split[0])) {
                    graph.Add(split[0], new List<string>());
                }

                graph[split[0]].Add(split[1]);

                if (!graph.ContainsKey(split[1])) {
                    graph.Add(split[1], new List<string>());
                }

                graph[split[1]].Add(split[0]);
            }
        }

        private static string Solve01(Dictionary<string, List<string>> graph) {
            List<List<string>> allPaths = new();
            List<string> path = new() { "start" };

            FindEnd(graph, "start", path, allPaths, false);

            return allPaths.Count.ToString();
        }

        private static string Solve02(Dictionary<string, List<string>> graph) {
            List<List<string>> allPaths = new();
            List<string> path = new() { "start" };

            FindEnd(graph, "start", path, allPaths, true);
            
            return allPaths.Count.ToString();
        }

        private static void FindEnd(Dictionary<string, List<string>> graph, string currentNode, List<string> path,
            List<List<string>> allPaths, bool visitTwice) {
            foreach (string nextNode in graph[currentNode]) {
                if (nextNode == "start") {
                } else if (nextNode == "end") {
                    path.Add(nextNode);
                    allPaths.Add(path);
                } else if (nextNode.All(char.IsUpper)) {
                    List<string> newPath = path.GetRange(0, path.Count);
                    newPath.Add(nextNode);
                    FindEnd(graph, nextNode, newPath, allPaths, visitTwice);
                } else if (nextNode.All(char.IsLower)) {
                    if (!path.Contains(nextNode)) {
                        List<string> newPath = path.GetRange(0, path.Count);
                        newPath.Add(nextNode);
                        FindEnd(graph, nextNode, newPath, allPaths, visitTwice);
                    } else if (visitTwice) {
                        List<string> newPath = path.GetRange(0, path.Count);
                        newPath.Add(nextNode);
                        FindEnd(graph, nextNode, newPath, allPaths, false);
                    }
                }
            }
        }
    }
}