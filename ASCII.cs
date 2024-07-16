using System;

namespace Checkers
{
    public class ASCII
    {
        public static void Welcome()
        {
            var title = @"
----------------------------------------------------------------------------------


                                @ 2024 TW MF PK


                                POLISH DRAUGHTS
";
            Console.WriteLine(title);
        }

        public static void WaitForKey()
        {
            string wait = @"

                                PRESS ANY KEY TO CONTINUE...

";
            Console.WriteLine(wait);
            Console.ReadKey();
        }

        public static void BlackWon()
        {
            var blackWon = @"
        
                    B L A C K   W O N 
";
            Console.WriteLine(blackWon);
        }

        public static void WhiteWon()
        {
            var whiteWon = @"

                    W H I T E   W O N
";
            Console.WriteLine(whiteWon);
        }

        public static void Player1()
        {
            var player1 = @"

                    P L A Y E R  1
";
            Console.WriteLine(player1);
        }

        public static void Player2()
        {
            var player2 = @"

                    P L A Y E R  2
";
            Console.WriteLine(player2);
        }
    }
}