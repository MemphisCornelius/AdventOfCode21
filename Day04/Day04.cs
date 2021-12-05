using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day04 {
    internal static class Day04 {
        public static void Main(string[] args) {
            List<string> values = new();
            StreamReader sr = new("../../../input.txt");

            while (!sr.EndOfStream) {
                values.Add(sr.ReadLine()!);
            }

            sr.Close();

            List<int[,,]> boards = ParseInput(values, out List<int> numbers);

            Console.WriteLine($"Aufgabe01: {Solve01(numbers, boards)}");
            Console.WriteLine($"Aufgabe02: {Solve02(numbers, boards)}");
        }


        private static List<int[,,]> ParseInput(List<string> values, out List<int> numbers) {
            List<string> n = values[0].Split(',').ToList();
            numbers = new();
            foreach (string str in n) {
                numbers.Add(int.Parse(str));
            }

            List<int[,,]> boards = new();
            int[,,] board = new int[5, 5, 2];

            for (int i = 1; i < values.Count; i++) {
                if (values[i] == "") {
                    boards.Add(board);
                    board = new int[5, 5, 2];
                } else {
                    List<string> row = values[i].Split(' ').ToList();
                    row.RemoveAll(string.IsNullOrEmpty);
                    for (int j = 0; j < row.Count; j++) {
                        board[(i - boards.Count - 1) % 5, j, 0] = int.Parse(row[j]);
                        board[(i - boards.Count - 1) % 5, j, 1] = 0;
                    }
                }
            }

            boards.Add(board);

            boards.Remove(boards[0]);
            return boards;
        }

        private static string Solve01(List<int> numbers, List<int[,,]> boards) {
            foreach (int number in numbers) {
                foreach (int[,,] board in boards) {
                    for (int i = 0; i < board.GetLength(0); i++) {
                        for (int j = 0; j < board.GetLength(1); j++) {
                            if (board[i, j, 0] == number) {
                                board[i, j, 1] = 1;
                                int bingo = CheckBingo(board);

                                if (bingo != -1) {
                                    return (bingo * number).ToString();
                                }
                            }
                        }
                    }
                }
            }

            return "";
        }

        private static int CheckBingo(int[,,] board) {
            bool bingo = false;
            for (int i = 0; !bingo && i < board.GetLength(0); i++) {
                bool vertical = true, horizontal = true;
                for (int j = 0; j < board.GetLength(1); j++) {
                    if (horizontal && board[i, j, 1] == 0) {
                        horizontal = false;
                    }

                    if (vertical && board[j, i, 1] == 0) {
                        vertical = false;
                    }
                }

                bingo = vertical || horizontal;
            }

            if (bingo) {
                int sum = 0;
                for (int i = 0; i < board.GetLength(0); i++) {
                    for (int j = 0; j < board.GetLength(1); j++) {
                        sum += board[i, j, 1] == 0 ? board[i, j, 0] : 0;
                    }
                }

                return sum;
            } else {
                return -1;
            }
        }

        private static string Solve02(List<int> numbers, List<int[,,]> boards) {
            foreach (int number in numbers) {
                for (int index = 0; index < boards.Count; index++) {
                    int[,,] board = boards[index];
                    for (int i = 0; i < board.GetLength(0); i++) {
                        for (int j = 0; j < board.GetLength(1); j++) {
                            if (board[i, j, 0] == number) {
                                board[i, j, 1] = 1;
                                int bingo = CheckBingo(board);

                                if (bingo != -1) {
                                    if (boards.Count > 1) {
                                        boards.Remove(board);
                                        index--;
                                    } else {
                                        return (bingo * number).ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return "";
        }
    }
}