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
            
            //1 0
            //0 0 
            mazeGenerator.GenerateMaze( 0, rows / 2, 0, columns / 2, wall, this.gameBoard);

            //0 1
            //0 0
            mazeGenerator.GenerateMaze(0, rows / 2, columns / 2, columns, wall, this.gameBoard);
                    
            //0 0
            //1 0    
            mazeGenerator.GenerateMaze(rows / 2, rows, 0, columns / 2, wall, this.gameBoard);
            
            //0 0
            //0 1 
            mazeGenerator.GenerateMaze(rows / 2, rows, columns / 2, columns, wall, this.gameBoard);

            // defining tramps
            
            return this.gameBoard;
        }
        public void PrintBoardSpectre(string[,] gameBoard)
        {
            Console.Clear();
                // Create a canvas
            var canvas = new Canvas(gameBoard.GetLength(0), gameBoard.GetLength(1));

            // Draw some shapes
            for (int column = 0 ; column < canvas.Height; column++)
            {
                for(int row = 0; row < canvas.Width; row++)
                {
                    switch (gameBoard[column, row])
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
                        case "🏆":
                            canvas.SetPixel(row , column, Color.DeepPink4_1);
                            break;
                        case "🔲":
                            canvas.SetPixel(row , column , Color.Black);
                            break;
                        case "🟥":
                            canvas.SetPixel(row , column , Color.Red);
                            break;
                        case "🔳":
                            canvas.SetPixel(row , column , Color.BlueViolet);
                            break;
                    }
                    
                }
            }
            AnsiConsole.Write(canvas);
        }
        
        public void PrintBoard(string[,] game_boar)
    {
        Console.Clear();
        for (int i = 0; i < game_boar.GetLength(0); i++)
        {
            for (int j = 0; j < game_boar.GetLength(1); j++)
            {
                Console.Write(game_boar[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    }
}
