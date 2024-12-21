namespace P_P.board
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
    }
}
