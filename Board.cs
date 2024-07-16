using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers
{
    class Board
    {
        public Pawn[,] Fields { get; private set; }

        private string[] columnLetters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U" };

        public Board(int n)
        {
            Fields = new Pawn[n, n];
            int rowWithPaws = (n - 2) / 2;
            for (int row = 0; row < n; row++)
            {
                bool? isWhite;
                if (row < rowWithPaws)
                    isWhite = true;
                else if (row >= n - rowWithPaws)
                    isWhite = false;
                else
                    isWhite = null;

                if (isWhite.HasValue)
                {
                    int shift = row % 2;
                    for (int col = 0; col < n; col++)
                    {
                        if (col % 2 == 0 && col + shift < n)
                            Fields[row, col + shift] = new Pawn(isWhite.Value, row, col + shift);
                    }
                }
            }
        }

        public void RemovePawn(int x, int y)
        {
            Fields[x, y] = null;
        }

        public void MovePawn(int x1, int y1, int x2, int y2)
        {
            Fields[x2, y2] = Fields[x1, y1];
            Fields[x1, y1] = null;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("     ");
            for (int i = 0; i < Fields.GetLength(0); i++)
            {
                stringBuilder.Append($"{columnLetters[i]} ");
            }
            stringBuilder.Append("\n");
            for (int row = 0; row < Fields.GetLength(0); row++)
            {
                int rowNumber = row + 1;

                if (rowNumber < 10)
                {
                    stringBuilder.Append($" {rowNumber} ");
                }
                else
                {
                    stringBuilder.Append($" {rowNumber} ");
                }
                for (int col = 0; col < Fields.GetLength(1); col++)
                {
                    stringBuilder.Append("|");
                    if (Fields[row, col] == null)
                    {
                        stringBuilder.Append("_");
                    }
                    else if (Fields[row, col].IsWhite)
                    {
                        stringBuilder.Append("W");
                    }
                    else
                    {
                        stringBuilder.Append("B");
                    }
                    stringBuilder.Append("|");
                }
                stringBuilder.Append("\n");
            }
            return stringBuilder.ToString();
        }
    }
}








/*
public sealed class Board
{
    public static int N
        {
            get => N;
            set => N = value >= 10 && value <= 20
                ? value
                : throw new ArgumentOutOfRangeException("N has to be in range 10 - 20.");
        }

    private static Board? instance = null;
    private static readonly object padlock = new object();
    public static Board Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Board();
                }
                return instance;
            }
        }
    }
    public Board()
    {
    }
}   */

