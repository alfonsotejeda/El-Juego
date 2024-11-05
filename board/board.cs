namespace P_P
{
    /// <summary>
    /// Clase que representa el tablero de juego y sus operaciones
    /// </summary>
    public class Board
    {
        // Propiedades privadas del tablero
        private int columns;
        private int rows;
        private string currentTurnMessage = "";
        
        // Propiedades públicas
        public string[,] gameBoard;
        public string wall = "⬛";

        public List<BaseTrap> traps { get; private set; } = new List<BaseTrap>();
        public List<TeleportationPortal> portals { get; private set; } = new List<TeleportationPortal>();

        /// <summary>
        /// Constructor que inicializa un nuevo tablero con dimensiones específicas
        /// </summary>
        public Board(int columns, int rows)
        {
            this.columns = columns;
            this.rows = rows;
            this.gameBoard = new string[rows, columns];
        }

        /// <summary>
        /// Crea y configura el tablero inicial con el laberinto
        /// </summary>
        public string[,] create_board()
        {
            Maze_Generator maze_Generator = new Maze_Generator();
            
            // Inicializar tablero vacío
            maze_Generator.Generate_Empty_Maze(this.gameBoard, wall);
            
            // Calcular punto central
            int centerX = rows / 2;
            int centerY = columns / 2;
            
            // Definir puntos de inicio en las esquinas
            var startPoints = new List<(int, int)>
            {
                (0, 0),                    // Esquina superior izquierda
                (0, columns-1),            // Esquina superior derecha
                (rows-1, 0),               // Esquina inferior izquierda
                (rows-1, columns-1)        // Esquina inferior derecha
            };

            // Generar laberinto conectado
            maze_Generator.Generate_Connected_Maze(
                startPoints,
                centerX,
                centerY,
                wall,
                " ",
                this.gameBoard
            );
            
            // Colocar objetivo en el centro
            this.gameBoard[centerX, centerY] = "🏆";
            
            // Asegurar que las esquinas sean caminos válidos
            foreach (var (x, y) in startPoints)
            {
                this.gameBoard[x, y] = "⬜";
            }

            // Generar trampas y portales
            Random random = new Random();
            
            // Agregar 5 pares de portales
            for (int i = 0; i < 5; i++)
            {
                var validPositions = GetRandomValidPositions(2);
                var portal = new TeleportationPortal(validPositions[0], validPositions[1]);
                portals.Add(portal);
                gameBoard[portal.portal1.row, portal.portal1.col] = portal.icon;
                gameBoard[portal.portal2.row, portal.portal2.col] = portal.icon;
            }

            // Agregar 8 trampas de parada
            for (int i = 0; i < 8; i++)
            {
                var pos = GetRandomValidPositions(1)[0];
                var trap = new StopTurnTrap();
                traps.Add(trap);
                gameBoard[pos.row, pos.col] = trap.icon;
            }

            return this.gameBoard;
        }

        private List<(int row, int col)> GetRandomValidPositions(int count)
        {
            Random random = new Random();
            var positions = new List<(int row, int col)>();
            
            while (positions.Count < count)
            {
                int row = random.Next(1, rows - 1);
                int col = random.Next(1, columns - 1);

                // Verificar que la posición sea un camino válido y no esté ocupada
                if (gameBoard[row, col] == "⬜")
                {
                    // Asegurar que haya suficiente espacio alrededor para esquivar
                    bool hasPathAround = false;
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            if (row + dx >= 0 && row + dx < rows && 
                                col + dy >= 0 && col + dy < columns &&
                                gameBoard[row + dx, col + dy] == "⬜")
                            {
                                hasPathAround = true;
                                break;
                            }
                        }
                    }

                    // Verificar que no esté cerca de las esquinas o el centro
                    bool isFarFromCorners = row > 3 && row < rows - 4 && col > 3 && col < columns - 4;
                    bool isFarFromCenter = Math.Abs(row - rows/2) > 3 || Math.Abs(col - columns/2) > 3;
                    
                    if (isFarFromCorners && isFarFromCenter && hasPathAround)
                    {
                        positions.Add((row, col));
                    }
                }
            }
            
            return positions;
        }

        public void print_board(string[,] gameboard)
        {
            Console.SetCursorPosition(0, 0);
            
            // Mensaje del turno
            Console.WriteLine(currentTurnMessage);
            Console.WriteLine();

            // Imprimir el tablero sin coordenadas
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(gameBoard[i,j]);
                }
                Console.WriteLine();
            }
        }

        // Método para mostrar el mensaje de turno con los movimientos restantes
        public void ShowTurnMessage(string playerColor, string emoji, int movementsLeft)
        {
            string playerEmoji = playerColor == "AZUL" ? "🔷" : "🔶";
            currentTurnMessage = $@"╔════════════════════════════════════════════╗
║                                            ║
║     🎮 TURNO DEL JUGADOR {playerColor} {playerEmoji}        ║
║     🎯 Movimientos restantes: {movementsLeft}          ║
║                                            ║
╚════════════════════════════════════════════╝";
        }
    }
}
