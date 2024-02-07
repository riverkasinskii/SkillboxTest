public static class Stats
{
    public static int Level { get; private set; } = 1;

    public static float Health { get; set; }

    public static int Coins { get; set; } = 0;

    public static int BlueGems { get; set; } = 0;
        
    public static int RedGems { get; set; } = 0;

    public static void ResetAllStats()
    {
        Level = 1;
        Coins = 0;
        BlueGems = 0;
        RedGems = 0;
    }
}
