using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    public class Game
    {
        int _boardSize;
        Board _board;
        Player _currentPlayer;

        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public Game(Player player1, Player player2, int boardSize)
        {
            Player1 = player1;
            Player2 = player2;
            _boardSize = boardSize;
        }

        public void Start()
        {
            Console.WriteLine();
            _board = new Board(_boardSize);
            _currentPlayer = Player1;
        }

        public bool Round()
        {
            if (EndOfGame())
                return false;

            _currentPlayer = _currentPlayer == Player1 ? Player2 : Player1;

            Program.Moves++;
            if (!ContainsBlack() || !ContainsWhite())
            {
                return false;
            }
            return true;
        }

        public bool EndOfGame()
        {
            return false;
        }

        public bool TryToMakeMove(int x1, int y1, int x2, int y2)
        {
            bool isMoveIncorrect = true;

            if (_board.Fields[x1, y1] == null)
            {
                Console.WriteLine("Error: \"you didn't choose any pawn!\" <--");
                return isMoveIncorrect;
            }
            else if (!(_board.Fields[x2, y2] == null))
            {
                Console.WriteLine("Error: \"target field is being occupied!\" <--");
                return isMoveIncorrect;
            }
            if ((_currentPlayer.IsWhite && !_board.Fields[x1, y1].IsWhite) ||
                (!_currentPlayer.IsWhite && _board.Fields[x1, y1].IsWhite))
            {
                Console.WriteLine("Error: \"this pawn belongs to your opponent!\" <--");
                return isMoveIncorrect;
            }
            else
            {
                List<int[]> possibleMovesFor = GetPossibleCoordinatesFor(x1, y1);

                int[] finalCoordinates = new int[] { x2, y2 };

                foreach (var element in possibleMovesFor)
                {
                    if ((element[0] == finalCoordinates[0]) && (element[1] == finalCoordinates[1]))
                    {
                        _board.Fields[x2, y2] = _board.Fields[x1, y1];
                        _board.Fields[x1, y1] = null;

                        if ((Math.Abs(x1 - x2) != 1))
                        {
                            _board.RemovePawn((x1 + x2) / 2, (y1 + y2) / 2);
                            Program.Moves = -1;
                        }

                        return !isMoveIncorrect;
                    }
                }
                Console.WriteLine("Error: \"this move is forbidden!\" <--");
                return isMoveIncorrect;
            }
        }

        private List<int[]> GetPossibleCoordinatesFor(int x, int y)
        {
            List<int[]> possibleMovesList = new List<int[]>();
            int[] coordinates = new int[2];

            coordinates[0] = x + 1;
            coordinates[1] = y + 1;
            if (coordinates[0] < _boardSize && coordinates[1] < _boardSize)
            {
                if (_board.Fields[coordinates[0], coordinates[1]] == null)
                {
                    int[] coordToAdd = coordinates.ToArray();
                    possibleMovesList.Add(coordToAdd);
                }
                else
                {
                    if (_board.Fields[coordinates[0], coordinates[1]].IsWhite != _currentPlayer.IsWhite)
                    {
                        coordinates[0]++;
                        coordinates[1]++;
                        if (coordinates[0] < _boardSize && coordinates[1] < _boardSize)
                        {
                            if (_board.Fields[coordinates[0], coordinates[1]] == null)
                            {
                                int[] coordToAdd = coordinates.ToArray();
                                possibleMovesList.Add(coordToAdd);
                            }
                        }
                    }
                }
            }

            coordinates[0] = x - 1;
            coordinates[1] = y - 1;
            if (coordinates[0] >= 0 && coordinates[1] >= 0)
            {
                if (_board.Fields[coordinates[0], coordinates[1]] == null)
                {
                    int[] coordToAdd = coordinates.ToArray();
                    possibleMovesList.Add(coordToAdd);
                }
                else
                {
                    if (_board.Fields[coordinates[0], coordinates[1]].IsWhite != _currentPlayer.IsWhite)
                    {
                        coordinates[0]--;
                        coordinates[1]--;
                        if (coordinates[0] >= 0 && coordinates[1] >= 0)
                        {
                            if (_board.Fields[coordinates[0], coordinates[1]] == null)
                            {
                                int[] coordToAdd = coordinates.ToArray();
                                possibleMovesList.Add(coordToAdd);
                            }
                        }
                    }
                }
            }

            coordinates[0] = x - 1;
            coordinates[1] = y + 1;
            if (coordinates[0] >= 0 && coordinates[1] < _boardSize)
            {
                if (_board.Fields[coordinates[0], coordinates[1]] == null)
                {
                    int[] coordToAdd = coordinates.ToArray();
                    possibleMovesList.Add(coordToAdd);
                }
                else
                {
                    if (_board.Fields[coordinates[0], coordinates[1]].IsWhite != _currentPlayer.IsWhite)
                    {
                        coordinates[0]--;
                        coordinates[1]++;
                        if (coordinates[0] >= 0 && coordinates[1] < _boardSize)
                        {
                            if (_board.Fields[coordinates[0], coordinates[1]] == null)
                            {
                                int[] coordToAdd = coordinates.ToArray();
                                possibleMovesList.Add(coordToAdd);
                            }
                        }
                    }
                }
            }

            coordinates[0] = x + 1;
            coordinates[1] = y - 1;
            if (coordinates[0] < _boardSize && coordinates[1] >= 0)
            {
                if (_board.Fields[coordinates[0], coordinates[1]] == null)
                {
                    int[] coordToAdd = coordinates.ToArray();
                    possibleMovesList.Add(coordToAdd);
                }
                else
                {
                    if (_board.Fields[coordinates[0], coordinates[1]].IsWhite != _currentPlayer.IsWhite)
                    {
                        coordinates[0]++;
                        coordinates[1]--;
                        if (coordinates[0] < _boardSize && coordinates[1] >= 0)
                        {
                            if (_board.Fields[coordinates[0], coordinates[1]] == null)
                            {
                                int[] coordToAdd = coordinates.ToArray();
                                possibleMovesList.Add(coordToAdd);
                            }
                        }
                    }
                }
            }

            return possibleMovesList;
        }

        public bool ContainsWhite()
        {
            for (int i = 0; i < _board.Fields.GetLength(0); i++)
            {
                for (int j = 0; j < _board.Fields.GetLength(1); j++)
                {
                    if (_board.Fields[i, j] != null && _board.Fields[i, j].IsWhite)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ContainsBlack()
        {
            for (int i = 0; i < _board.Fields.GetLength(0); i++)
            {
                for (int j = 0; j < _board.Fields.GetLength(1); j++)
                {
                    if (_board.Fields[i, j] != null && !_board.Fields[i, j].IsWhite)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Player CheckForWinner()
        {
            Player player = null;

            if (!ContainsWhite())
            {
                ASCII.BlackWon();
                player = Player1.IsWhite ? Player2 : Player1;
            }
            else if (!ContainsBlack())
            {
                ASCII.WhiteWon();
                player = Player1.IsWhite ? Player1 : Player2;
            }

            return player;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"\n   Player {_currentPlayer.Name} turn.\n");
            stringBuilder.Append(_board.ToString());

            return stringBuilder.ToString();
        }
    }
}