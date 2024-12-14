namespace Maze_Game_Project
{
    internal interface IMazeObject
    {
        char Icon { get; }
        bool IsSolid { get; }
    }

    internal class Character : IMazeObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Icon { get => (char)2; }
        public bool IsSolid { get => true; }
    }

    internal class EmptySpace : IMazeObject
    {
        public char Icon { get => ' '; }
        public bool IsSolid { get => false; }
    }

    internal class Wall : IMazeObject
    {
        public char Icon { get => '#'; }
        public bool IsSolid { get => true; }
        public char[,] WallChar { get; set; }

        public void SetArray(char[,] wallChar)
        {
            WallChar = wallChar;
        }
    }
}
