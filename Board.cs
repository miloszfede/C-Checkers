namespace Checkers;

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
}   
    
