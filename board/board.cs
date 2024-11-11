namespace P_P
{
    public class Board
    {
        private int columns;
        private int rows;
        public string[,] gameBoard;

        public string wall = "🟫";
        public Board(int columns, int rows)
        {
            this.columns = columns;
            this.rows = rows;
            this.gameBoard = new string[rows, columns];
        }
        public string[,] createBoard()
        {
            MazeGenerator mazeGenerator = new MazeGenerator();

            mazeGenerator.GenerateMaze(0, rows / 2, 0, columns / 2, wall, this.gameBoard);
            mazeGenerator.GenerateMaze(rows / 2, rows, 0, columns / 2, wall, this.gameBoard);
            mazeGenerator.GenerateMaze(rows / 2, rows, columns / 2, columns, wall, this.gameBoard);
            mazeGenerator.GenerateMaze(0, rows / 2, columns / 2, columns, wall, this.gameBoard);
            return this.gameBoard;
        }
        public void printBoard(string[,] gameBoard)
        {
            Console.Clear();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(gameBoard[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
