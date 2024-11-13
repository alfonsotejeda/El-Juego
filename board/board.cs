using Spectre.Console;
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
                // Create a canvas
            var canvas = new Canvas(gameBoard.GetLength(0), gameBoard.GetLength(1));

            // Draw some shapes
            for(int row = 0; row < canvas.Width; row++)
            {
                for (int column = 0 ; column < canvas.Height; column++)
                {
                    switch (gameBoard[row, column])
                    {
                        case "🟫":
                            canvas.SetPixel(row, column, Color.Black);
                            break;
                        case "⬜️":
                            canvas.SetPixel(row, column, Color.White);
                            break;
                        case "🟦":
                            canvas.SetPixel(row, column, Color.Blue);
                            break;
                    }
                    
                }
            }
            AnsiConsole.Write(canvas);

        }
    }
}
