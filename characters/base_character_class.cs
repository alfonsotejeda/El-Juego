namespace P_P
{
    /// <summary>
    /// Clase base para todos los personajes del juego
    /// Define las propiedades y comportamientos comunes
    /// </summary>
    public class BaseCharacter
    {
        // Propiedades básicas del personaje
        protected string icon;                // Cambiado a protected para que las clases hijas puedan acceder
        public string ability;             
        public int movement_capacity;      
        public int player_start_column;    
        public int player_start_row;       

        public string Icon => icon;  // Propiedad pública de solo lectura para acceder al icono

        /// <summary>
        /// Constructor de la clase base para personajes
        /// </summary>
        /// <param name="icon">Ícono del personaje</param>
        /// <param name="ability">Habilidad especial</param>
        /// <param name="movement_capacity">Capacidad de movimiento</param>
        /// <param name="player_start_column">Columna inicial</param>
        /// <param name="player_start_row">Fila inicial</param>
        public BaseCharacter(string icon, string ability, int movement_capacity, 
            int player_start_column, int player_start_row)
        {
            this.icon = icon ?? throw new ArgumentNullException(nameof(icon));
            this.ability = ability ?? throw new ArgumentNullException(nameof(ability));
            this.movement_capacity = movement_capacity;
            this.player_start_column = player_start_column;
            this.player_start_row = player_start_row;
        }

        /// <summary>
        /// Maneja el movimiento del personaje en el tablero
        /// </summary>
        public void Move(ref int player_start_row, ref int player_start_column, 
            string[,] game_board, Board board)
        {
            // Captura la tecla presionada por el usuario
            ConsoleKeyInfo key = Console.ReadKey();
            int new_row = player_start_row;
            int new_column = player_start_column;

            // Determina la nueva posición según la tecla presionada
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    new_row--;
                    break;
                case ConsoleKey.DownArrow:
                    new_row++;
                    break;
                case ConsoleKey.LeftArrow:
                    new_column--;
                    break;
                case ConsoleKey.RightArrow:
                    new_column++;
                    break;
            }

            // Verificar si el movimiento es válido
            if (new_row >= 0 && new_row < game_board.GetLength(0) && 
                new_column >= 0 && new_column < game_board.GetLength(1) && 
                game_board[new_row, new_column] != board.wall)
            {
                string previousCell = game_board[player_start_row, player_start_column];
                string nextCell = game_board[new_row, new_column];
                bool trapActivated = false;

                // Verificar si hay una trampa en la nueva posición
                foreach (var trap in board.traps)
                {
                    if (game_board[new_row, new_column] == trap.icon)
                    {
                        // Guardar posición temporal
                        int temp_row = new_row;
                        int temp_col = new_column;
                        
                        // Restaurar posición anterior
                        new_row = player_start_row;
                        new_column = player_start_column;
                        
                        // Aplicar efecto de la trampa
                        trap.ApplyEffect(ref movement_capacity);
                        trapActivated = true;
                        
                        // Mostrar mensaje de trampa activada
                        Console.SetCursorPosition(0, board.gameBoard.GetLength(0) + 3);
                        Console.WriteLine($"¡Has caído en una trampa! ⚡ Pierdes el turno.");
                        Thread.Sleep(1000); // Pausa para mostrar el mensaje
                        break;
                    }
                }

                if (!trapActivated)
                {
                    // Verificar portales solo si no se activó una trampa
                    foreach (var portal in board.portals)
                    {
                        if (new_row == portal.portal1.row && new_column == portal.portal1.col ||
                            new_row == portal.portal2.row && new_column == portal.portal2.col)
                        {
                            var (teleportRow, teleportCol) = portal.Teleport(new_row, new_column);
                            new_row = teleportRow;
                            new_column = teleportCol;
                            break;
                        }
                    }
                }

                // Restaurar la celda anterior
                if (previousCell == icon)
                {
                    game_board[player_start_row, player_start_column] = "⬜";
                }

                // Actualizar posición
                player_start_row = new_row;
                player_start_column = new_column;

                // Colocar el personaje en la nueva posición
                game_board[player_start_row, player_start_column] = icon;
            }
        }
    }
}
