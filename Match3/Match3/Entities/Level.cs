namespace Match3.Entities
{
    class Level
    {
        public int[,] tiles { get; set; }
        public int targetScore { get; set; }
        public int moves { get; set; }
    }
}