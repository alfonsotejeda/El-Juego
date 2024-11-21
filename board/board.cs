using Spectre.Console;
namespace P_P
{
    public class Board
    {
        private int _columns;
        private int _rows;
        public string[,] GameBoard;
        public string[,] TrampBoard;

        
        public string Wall = "🟫";
        public Board(int columns, int rows)
        {
            this._columns = columns;
            this._rows = rows;
            this.GameBoard = new string[rows, columns];
            this.TrampBoard = new string[rows , columns];
        }
        public string[,] CreateBoard()
        {
            MazeGenerator mazeGenerator = new MazeGenerator();
            
            //1 0
            //0 0 
            mazeGenerator.GenerateMaze( 0, _rows / 2, 0, _columns / 2, Wall, this.GameBoard);

            //0 1
            //0 0
            mazeGenerator.GenerateMaze(0, _rows / 2, _columns / 2, _columns, Wall, this.GameBoard);
                    
            //0 0
            //1 0    
            mazeGenerator.GenerateMaze(_rows / 2, _rows, 0, _columns / 2, Wall, this.GameBoard);
            
            //0 0
            //0 1 
            mazeGenerator.GenerateMaze(_rows / 2, _rows, _columns / 2, _columns, Wall, this.GameBoard);

            // defining tramps
            ClosePathTramp closePathTramp = new ClosePathTramp("🔳" , 7 , "C");
            closePathTramp.CreateRandomTraps( GameBoard , 0, _rows / 2, 0, _columns / 2);
            closePathTramp.CreateRandomTraps( GameBoard ,0, _rows / 2, _columns / 2, _columns);
            closePathTramp.CreateRandomTraps( GameBoard , _rows / 2, _rows, 0, _columns / 2);
            closePathTramp.CreateRandomTraps(GameBoard, _rows / 2, _rows, _columns / 2, _columns);
            for (int i = 1; i < _rows; i++)
            {
                for (int j = 1; j < _columns; j++)
                {
                    if (GameBoard[i, j] == "🔳")
                    {
                        TrampBoard[i, j] = closePathTramp.trampId;
                        // gameBoard[i, j] = "⬜️";
                    }
                }
            } 
            return this.GameBoard;
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
        
        public void PrintBoard(string[,] gameBoar)
    {
        Console.Clear();
        for (int i = 0; i < gameBoar.GetLength(0); i++)
        {
            for (int j = 0; j < gameBoar.GetLength(1); j++)
            {
                Console.Write(gameBoar[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    }
}
