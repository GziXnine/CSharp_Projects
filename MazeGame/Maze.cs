namespace Maze_Game_Project
{
    public class Maze
    {
        private int _Width;
        private int _Height;
        private Wall _Wall;
        private Character _Player;
        private IMazeObject[,] _Maze;

        public Maze(int width, int height, char[,] mazeTemplate)
        {
            _Width = width;
            _Height = height;
            _Maze = new IMazeObject[_Height, _Width];
            _Wall = new Wall();
            _Wall.SetArray(mazeTemplate);
            _Player = new Character()
            {
                X = 1,
                Y = 1
            };

            InitializeMaze(mazeTemplate);
        }

        private void InitializeMaze(char[,] mazeTemplate)
        {
            for (int i = 0; i < _Height; i++)
            {
                for (int j = 0; j < _Width; j++)
                {
                    if (mazeTemplate[i, j] == ' ') // Empty space
                    {
                        _Maze[i, j] = new EmptySpace();
                    }
                    else
                    {
                        _Maze[i, j] = new Wall();
                    }
                }
            }

            _Maze[_Player.Y, _Player.X] = _Player;
        }

        public void DrawMaze()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("This maze is not as you see it, but it is like your life. Nothing in it shows its true nature.");

            for (int i = 0; i < _Height; i++)
            {
                for (int j = 0; j < _Width; j++)
                {
                    if (i == _Player.Y && j == _Player.X)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(_Player.Icon);
                    }
                    else if (_Wall.WallChar[i, j] == ' ')
                    {
                        _Maze[i, j] = new EmptySpace();
                        Console.Write(_Maze[i, j].Icon);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;

                        _Maze[i, j] = new Wall();
                        //Console.Write(_Maze[i, j].Icon);

                        if(_Wall.WallChar[i, j] == '#')
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                        else if (_Wall.WallChar[i, j] == '>')
                            Console.ForegroundColor = ConsoleColor.Green;
                        else if (_Wall.WallChar[i, j] == '*')
                            Console.ForegroundColor = ConsoleColor.DarkYellow;

                        Console.Write(_Wall.WallChar[i, j]);
                    }
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n\nIf U Can Collect All Treasures And Go Out Please Contact Me <3");
            Console.ResetColor();
        }

        public void MovePlayer()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            int newX = _Player.X;
            int newY = _Player.Y;

            switch (key.Key)
            {
                case ConsoleKey.UpArrow: newY--; break;
                case ConsoleKey.DownArrow: newY++; break;
                case ConsoleKey.LeftArrow: newX--; break;
                case ConsoleKey.RightArrow: newX++; break;
            }

            UpdatePlayer(newX, newY);
        }

        private void UpdatePlayer(int newX, int newY)
        {
            if (newX > 0 && newX < _Width - 1 && newY > 0 && newY < _Height - 1)
            {
                if (!_Maze[newX, newY].IsSolid)
                {
                    _Player.X = newX;
                    _Player.Y = newY;

                    Console.Clear();
                    DrawMaze();
                }
            }
        }
    }
}
