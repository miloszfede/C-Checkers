using System;

namespace Checkers
{
    public class Pawn
    {
        public bool IsWhite { get; private set; }

        public (int x, int y) Coordinates { get; set; }

        public Pawn(bool isWhite, int x, int y)
        {
            IsWhite = isWhite;
            Coordinates = (x, y);
        }
    }
}




/*class Pawn 
{
    public enum Color {
        white,
        black
    }
    public Pawn()
    {
    }
}*/