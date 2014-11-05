using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sodoku {
    public class Solver {
        public int[][] Board { get; set; }

        public Solver(string board) {
            Board = new int[9][];
            Load(board);
        }

        private void Load(string board) {
            var regex = new Regex(@"[^0-9_]", RegexOptions.None);
            var clean = regex.Replace(board, "");
            var i = 0;
            while (i < 9) {
                Board[i] = clean.Skip(i * 9).Take(9).Select(x => x != '_' ? Int32.Parse(x.ToString()) : 0).ToArray();
                i++;
            }
        }

        public int[] GetRow(int row) {
            return Board[row];
        }

        public int[] GetColumn(int column) {
            var result = new int[9];
            for (var i = 0; i < 9; i++) {
                result[i] = Board[i][column];
            }
            return result;
        }

        public bool Solve() {
            PreencherPosicao(0, 0);
            PrintBoard();
            return false;
        }

        private void PreencherPosicao(int rowInicio, int colInicio) {
            for (int row = rowInicio; row < 9; row++) {
                for (int column = colInicio; column < 9; column++) {
                    if (Board[row][column] == 0) {
                        var valoresPossiveis = GetPossibleValues(row, column);
                        for (int i = 0; i < valoresPossiveis.Count(); i++) {
                            Board[row][column] = valoresPossiveis[i];
                            PrintBoard();
                            Console.WriteLine();
                            Console.WriteLine();
                            PreencherPosicao(rowInicio, colInicio + 1);
                        }
                    }
                }
            }
        }

        private void PrintBoard() {
            Console.Write(String.Join(Environment.NewLine, Board.Select(row => String.Join(" ", row.Select(x => x > 0 ? x.ToString() : "_")))));
        }

        public int[] GetBox(int box) {
            int row, column;

            switch (box) {
                case 0:
                    row = 0;
                    column = 0;
                    break;
                case 1:
                    row = 0;
                    column = 3;
                    break;
                case 2:
                    row = 0;
                    column = 6;
                    break;
                case 3:
                    row = 3;
                    column = 0;
                    break;
                case 4:
                    row = 3;
                    column = 3;
                    break;
                case 5:
                    row = 3;
                    column = 6;
                    break;
                case 6:
                    row = 6;
                    column = 0;
                    break;
                case 7:
                    row = 6;
                    column = 3;
                    break;
                case 8:
                    row = 6;
                    column = 6;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new[]{                
                Board[row + 0][column + 0],
                Board[row + 0][column + 1],
                Board[row + 0][column + 2],
                Board[row + 1][column + 0],
                Board[row + 1][column + 1],
                Board[row + 1][column + 2],
                Board[row + 2][column + 0],
                Board[row + 2][column + 1],
                Board[row + 2][column + 2]
            };
        }

        public int[] GetPossibleValues(int row, int column) {
            var result = new List<int>();
            for (var i = 1; i <= 9; i++) {
                if (!GetRow(row).Contains(i)
                && !GetColumn(column).Contains(i)
                && !GetBox(GetBoxIndex(row, column)).Contains(i)) {
                    result.Add(i);
                }
            }
            return result.ToArray();
        }

        private int GetBoxIndex(int row, int column) {
            return (row - (row % 3)) + (column / 3);
        }
    }
}