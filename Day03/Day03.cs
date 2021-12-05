using System;
using System.Collections.Generic;
using System.IO;

namespace Day03 {
    internal static class Day03 {
        public static void Main(string[] args) {
            StreamReader sr = new("../../../input.txt");
            List<string> values = new List<string>();
            
            while (!sr.EndOfStream) {
                string s = sr.ReadLine()!;
                values.Add(s);
            }

            Console.WriteLine($"Aufgabe01: {Solve01(values)}");
            Console.WriteLine($"Aufgabe02: {Solve02(values)}");
        }

        private static int Solve01(List<string> values) {
            string gamma = "", epsilon = "";
            int[] ges = new int[values[0].Length];
            foreach (var t in values) {
                for (int j = 0; j < t.Length; j++) {
                    ges[j] += t[j] - '0';
                }
            }

            foreach (var t in ges) {
                if ((t / (double)values.Count) > 0.5) {
                    gamma += 1;
                    epsilon += 0;
                } else {
                    gamma += 0;
                    epsilon += 1;
                }
            }
            
            return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
        }

        private static int Solve02(List<string> values) {
            string oxygen = "", co2 = "";
            List<string> valuesO = values.GetRange(0, values.Count);
            List<string> valuesC = values.GetRange(0, values.Count);

            int countO = 0;
            int countC = 0;

            for (int j = 0; j < 12; j++) {
                foreach (string entry in valuesO) {
                    if (entry[j] == '1') {
                        countO++;
                    }
                }

                foreach (string entry in valuesC) {
                    if (entry[j] == '1') {
                        countC++;
                    }
                }
                
                bool mostCommon1  = countO / (double)(valuesO.Count) >= 0.5;
                bool leastCommon1 = countC / (double)(valuesC.Count) < 0.5;

                for (int i = 0; i < valuesO.Count; i++) {
                    string entry = valuesO[i];
                    if (mostCommon1) {
                        if (entry[j] == '0') {
                            valuesO.Remove(entry);
                            i--;
                        }
                    } else {
                        if (entry[j] == '1') {  
                            valuesO.Remove(entry);
                            i--;
                        }
                    }
                }

                for (int i = 0; i < valuesC.Count; i++) {
                    string entry = valuesC[i];
                    if (leastCommon1) {
                        if (entry[j] == '0') {
                            valuesC.Remove(entry);
                            i--;
                        }
                    } else {
                        if (entry[j] == '1') {
                            valuesC.Remove(entry);
                            i--;
                        }
                    }
                }
                
                if (valuesO.Count == 1) {
                    oxygen = valuesO[0];
                }

                if (valuesC.Count == 1) {
                    co2 = valuesC[0];
                }
                
                countO = 0;
                countC = 0;
            }
            
            return Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2);
        }
    }
}