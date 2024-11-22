using Spectre.Console;
namespace P_P
{
    public class Board
    {
        private int _columns;
        private int _rows;
        public Shell[,] GameBoard;
        
        public Board(int columns, int rows)
        {
            this._columns = columns;
            this._rows = rows;
            this.GameBoard = new Shell[rows, columns];
        }
        public Shell[,] CreateBoard()
        {
            // Inicializar cada celda del tablero
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    GameBoard[i, j] = new Shell();
                }
            }

            MazeGenerator mazeGenerator = new MazeGenerator();
            
            // Generar el laberinto en los cuatro cuadrantes
            mazeGenerator.GenerateMaze(0, _rows / 2, 0, _columns / 2, GameBoard);
            mazeGenerator.GenerateMaze(0, _rows / 2, _columns / 2, _columns, GameBoard);
            mazeGenerator.GenerateMaze(_rows / 2, _rows, 0, _columns / 2, GameBoard);
            mazeGenerator.GenerateMaze(_rows / 2, _rows, _columns / 2, _columns, GameBoard);
            
            

            return GameBoard;
        }
        public void PrintBoardSpectre(Shell[,] gameBoard)
        {
            AnsiConsole.Clear();
            
            // Definir el tamaño del lienzo
            int canvasWidth = gameBoard.GetLength(1);
            int canvasHeight = gameBoard.GetLength(0);
            Canvas canvas = new Canvas(canvasWidth, canvasHeight);

            // Imprimir cada celda como un "píxel" en el lienzo
            for (int i = 0; i < canvasHeight; i++)
            {
                for (int j = 0; j < canvasWidth; j++)
                {
                    switch (gameBoard[i, j])
                    {
                        case Shell shell when shell.HasCharacter:
                            canvas.SetPixel(j, i, Color.Blue);
                            break;
                        case Shell shell when shell.IsWall:
                            canvas.SetPixel(j, i, Color.Black);
                            break;
                        case Shell shell when shell.IsPath:
                            canvas.SetPixel(j, i, Color.White);
                            break;
                        case Shell shell when shell.IsTrophy:
                            canvas.SetPixel(j, i, Color.Yellow);
                            break;
                    }
                }
            }
            AnsiConsole.Write(canvas);
        }
        
        public void PrintBoard(Shell[,] gameBoard)
    {
        Console.Clear();
        for (int i = 0; i < gameBoard.GetLength(0); i++)
        {
            for (int j = 0; j < gameBoard.GetLength(1); j++)
            {
                if (gameBoard[i, j].HasCharacter)
                {
                    Console.Write(gameBoard[i, j].CharacterIcon + " ");
                }
                else if (gameBoard[i, j].IsWall)
                {
                    Console.Write(gameBoard[i, j].WallIcon + " ");
                }
                else if (gameBoard[i, j].IsPath)
                {
                    Console.Write(gameBoard[i, j].PathIcon + " ");
                }
                else if (gameBoard[i, j].IsTrophy)
                {
                    Console.Write("🏆");
                }
            }
            Console.WriteLine();
        }
    }
    }
}
