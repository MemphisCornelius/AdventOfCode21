using System.Text;

namespace Day14 {
    static class Day14 {
        public static void Main(string[] args) {
            List<string> values = new();
            StreamReader sr = new("../../../input.txt");

            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            StringBuilder sb = new();
            Console.WriteLine(sb.MaxCapacity);
            
            ParseInput(values, out string polymerTemplate, out Dictionary<string, char> pairInsertion);

            Console.WriteLine($"Aufgabe01: {Solve01(polymerTemplate, pairInsertion)}");

            Console.WriteLine($"Aufgabe02: {Solve02(polymerTemplate, pairInsertion)}");
        }

        private static void ParseInput(List<string> values, out string list, out Dictionary<string, char> dictionary) {
            list = values[0];
            dictionary = new();

            for (int i = 2; i < values.Count; i++) {
                string[] split = values[i].Split(" -> ");

                dictionary.Add(split[0], split[1][0]);
            }
        }
        private static string Solve01(string polymerTemplate, Dictionary<string, char> pairInsertion) {
            StringBuilder sb = new StringBuilder(polymerTemplate);

            for (int step = 0; step < 10; step++) {
                for (int i = 0; i < sb.Length - 1; i += 2) {
                    sb.Insert(i + 1, pairInsertion[(sb[i].ToString() + sb[i + 1].ToString())]);
                }
            }
            
            
            Dictionary<char, int> letterCount = new();
            foreach (char c in sb.ToString()) {
                if (!letterCount.ContainsKey(c)) {
                    letterCount.Add(c, 1);
                } else {
                    letterCount[c]++;
                }
            }

            letterCount = letterCount.OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);


            return (letterCount.ElementAt(letterCount.Count-1).Value - letterCount.ElementAt(0).Value).ToString();
        }

        private static string Solve02(string polymerTemplate, Dictionary<string,char> pairInsertion) {
            throw new NotImplementedException();
        }
        
    }
}