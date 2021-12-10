using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Day10 {
    static class Day10 {
        public static void Main(string[] args) {
            List<string> values = new();
            StreamReader sr = new("../../../input.txt");

            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            Console.WriteLine($"Aufgabe01: {Solve01(values)}");
            Console.WriteLine($"Aufgabe02: {Solve02(values)}");
        }

        private static string Solve01(List<string> lines) {
            int sum = 0;
            foreach (string line in lines) {
                Stack<char> stack = new();
                bool @continue = true;

                for (var i = 0; i < line.Length && @continue; i++) {
                    char c = line[i];
                    switch (c) {
                        case '(':
                            stack.Push(')');
                            break;
                        case '[':
                            stack.Push(']');
                            break;
                        case '{':
                            stack.Push('}');
                            break;
                        case '<':
                            stack.Push('>');
                            break;
                        default:
                            if (stack.Pop() != c) {
                                @continue = false;
                                switch (c) {
                                    case ')':
                                        sum += 3;
                                        break;
                                    case ']':
                                        sum += 57;
                                        break;
                                    case '}':
                                        sum += 1197;
                                        break;
                                    case '>':
                                        sum += 25137;
                                        break;
                                }
                            }

                            break;
                    }
                }
            }

            return sum.ToString();
        }

        private static string Solve02(List<string> lines) {
            List<string> incompleteLines = new();
            List<long> sums = new();

            foreach (string line in lines) {
                Stack<char> stack = new();
                bool @continue = true;

                for (int i = 0; i < line.Length && @continue; i++) {
                    char c = line[i];
                    switch (c) {
                        case '(':
                            stack.Push(')');
                            break;
                        case '[':
                            stack.Push(']');
                            break;
                        case '{':
                            stack.Push('}');
                            break;
                        case '<':
                            stack.Push('>');
                            break;
                        default:
                            if (stack.Pop() != c) {
                                @continue = false;
                            }

                            break;
                    }

                    if (i + 1 == line.Length && stack.Count != 0) {
                        string incomplete = "";
                        foreach (char c1 in stack) {
                            incomplete += c1;
                        }

                        incompleteLines.Add(incomplete);
                    }
                }
            }

            foreach (string incompleteLine in incompleteLines) {
                long sum = 0;
                foreach (char c in incompleteLine) {
                    sum *= 5;
                    switch (c) {
                        case ')':
                            sum += 1;
                            break;
                        case ']':
                            sum += 2;
                            break;
                        case '}':
                            sum += 3;
                            break;
                        case '>':
                            sum += 4;
                            break;
                    }
                }

                sums.Add(sum);
            }

            sums.Sort();
            return (sums[(sums.Count / 2)]).ToString();
        }
    }
}