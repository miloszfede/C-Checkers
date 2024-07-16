using System;

namespace Checkers
{
    class Program
    {
        public static int Moves { get; set; }

        static void Main()
        {
            Player player1 = null;
            Player player2 = null;
            int boardSize = 8;

            Console.Clear();
            ASCII.Welcome();
            ASCII.WaitForKey();

            boardSize = AskUserAboutBoardSize();

            Console.Clear();
            ASCII.Welcome();
            ASCII.Player1();
            player1 = AskUserAboutPlayer(null);

            Console.Clear();
            ASCII.Welcome();
            ASCII.Player2();
            player2 = AskUserAboutPlayer(!player1.IsWhite);

            var game = new Game(player1, player2, boardSize);

            game.Start();

            do
            {
                Console.Clear();
                ASCII.Welcome();
                Console.WriteLine($"{game.ToString()}");

                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;
                do
                {
                    (x1, y1, x2, y2) = AskForMove(boardSize);
                } while (game.TryToMakeMove(x1, y1, x2, y2));

            } while (game.Round());

            Console.Clear();
            Console.WriteLine(game.ToString());

            var winner = game.CheckForWinner();

            PrintWinner(winner);
            ASCII.WaitForKey();
            AskForNewGame();
        }

        private static (int, int, int, int) AskForMove(int boardSize)
        {
            int x1 = 0;
            int x2 = 0;
            int y1 = 0;
            int y2 = 0;
            bool isCorrect = false;

            while (!isCorrect)
            {
                if (CheckForDraw(boardSize))
                {
                    AskForNewGame();
                }
                Console.WriteLine(@"
        < Correct input format is e.g: 'a1 b2', where 'a1' is coordinate for pawn you want to move,
           and 'b2' is coordinate where you want to move pawn >



    |   PROVIDE YOUR MOVE ('m' for menu):
    |
    v
");
                var input = Console.ReadLine().ToLower();
                var parts = input.Split(' ');

                if (parts.Length == 2 &&
                    TryParseCoordinate(parts[0], out x1, out y1) &&
                    TryParseCoordinate(parts[1], out x2, out y2))
                {
                    isCorrect = true;
                }
                else
                {
                    Console.WriteLine("Incorrect input. Please try again.");
                }
            }

            return (x1, y1, x2, y2);
        }

        private static bool TryParseCoordinate(string input, out int x, out int y)
        {
            x = 0;
            y = 0;

            if (input.Length == 2 &&
                char.IsLetter(input[0]) &&
                char.IsDigit(input[1]))
            {
                x = char.ToUpper(input[0]) - 'A';
                y = int.Parse(input[1].ToString()) - 1;

                return true;
            }

            return false;
        }

        private static int AskUserAboutBoardSize()
        {
            int boardSize = 8;
            bool isCorrect = false;

            while (!isCorrect)
            {
                Console.WriteLine("Please provide board size (10-20): ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out boardSize) && boardSize >= 10 && boardSize <= 20)
                {
                    isCorrect = true;
                }
                else
                {
                    Console.WriteLine("Incorrect board size. Please try again.");
                }
            }

            return boardSize;
        }

        private static Player AskUserAboutPlayer(bool? isWhite)
        {
            Console.WriteLine("Please provide player name: ");
            var name = Console.ReadLine();

            if (isWhite == null)
            {
                Console.WriteLine("Is the player playing white? (yes/no): ");
                var colorInput = Console.ReadLine().ToLower();
                isWhite = colorInput == "yes";
            }

            return new Player { Name = name, IsWhite = isWhite.Value };
        }

        private static void PrintWinner(Player winner)
        {
            Console.WriteLine($"\nThe winner is {winner.Name}");
        }

        private static bool CheckForDraw(int boardSize)
        {
            return Moves >= boardSize * boardSize;
        }

        private static void AskForNewGame()
        {
            Console.WriteLine("Do you want to start a new game? (yes/no): ");
            var input = Console.ReadLine().ToLower();

            if (input == "yes")
            {
                Main();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}







/*class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Console.ReadKey();
    }
}
*/
