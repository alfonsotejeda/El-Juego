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
        public string[,] create_board()
        {
            Maze_Generator maze_Generator = new Maze_Generator();
            maze_Generator.Generate_Empty_Maze(this.gameBoard, wall);
            
            // Centro del tablero
            int centerX = rows / 2;
            int centerY = columns / 2;
            
            // Crear puntos de inicio en las esquinas
            var startPoints = new List<(int, int)>
            {
                (0, 0),                    // Esquina superior izquierda
                (0, columns-1),            // Esquina superior derecha
                (rows-1, 0),               // Esquina inferior izquierda
                (rows-1, columns-1)        // Esquina inferior derecha
            };

            // Generar un único laberinto conectado
            maze_Generator.Generate_Connected_Maze(
                startPoints,
                centerX,
                centerY,
                wall,
                "⬜️",
                this.gameBoard
            );
            
            // Marcar el centro como objetivo
            this.gameBoard[centerX, centerY] = "🏆";
            
            // Asegurar que las esquinas sean caminos
            foreach (var (x, y) in startPoints)
            {
                this.gameBoard[x, y] = "⬜️";
            }

            return this.gameBoard;
        }
        public void print_board(string [,] gameboard)
        {
            Console.Clear();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(gameBoard[i,j] + " ");
                }
                Console.WriteLine();
            }
            Thread.Sleep(100); 
        }
    }
}
