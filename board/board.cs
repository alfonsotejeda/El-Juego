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
                    Console.Write("🏆 ");
                }
            }
            Console.WriteLine();
        }
    }
    }
}
