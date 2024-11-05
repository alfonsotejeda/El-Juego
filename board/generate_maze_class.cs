namespace P_P
{
    public class Maze_Generator
    {
        private Random rand = new Random();
        
        // Agregamos una propiedad para controlar la complejidad
        private readonly double LoopProbability = 0.08; // Reducido de 0.15
        private readonly int MinPathLength = 20; // Longitud mínima del camino

        // Método auxiliar para calcular la distancia Manhattan entre dos puntos
        private int CalculateDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
        public void Generate_Empty_Maze(string[,] game_board , string wall)
        {
            for (int i = 0; i < game_board.GetLength(0); i++)
            {
                for (int j = 0; j < game_board.GetLength(1); j++)
                {
                    game_board[i, j] = wall;
                }
            }
        }

        public void Generate_Maze(int start_position_i, int end_position_i, int start_position_j, int end_position_j, string wall, string path, string[,] game_board)
        {
            Stack<(int, int)> stack = new Stack<(int, int)>();
            Dictionary<(int, int), int> distances = new Dictionary<(int, int), int>();
            
            stack.Push((start_position_i, start_position_j));
            game_board[start_position_i, start_position_j] = path;
            distances[(start_position_i, start_position_j)] = 0;

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            while (stack.Count > 0)
            {
                var (x, y) = stack.Pop();
                var currentDistance = distances[(x, y)];

                var neighbors = new List<(int, int, int)>(); // x, y, peso
                
                // Revisamos los vecinos con pesos
                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];

                    if (nx >= 0 && nx <= end_position_i && ny >= 0 && ny <= end_position_j)
                    {
                        if (game_board[nx, ny] == wall)
                        {
                            // Calculamos el peso basado en la distancia al final
                            int distanceToEnd = CalculateDistance(nx, ny, end_position_i, end_position_j);
                            int weight = distanceToEnd + rand.Next(5); // Añadimos algo de aleatoriedad
                            neighbors.Add((nx, ny, weight));
                        }
                        // Posibilidad de crear bucles
                        else if (game_board[nx, ny] == path && rand.NextDouble() < LoopProbability)
                        {
                            if (!distances.ContainsKey((nx, ny)) || 
                                currentDistance - distances[(nx, ny)] > 4) // Evita bucles muy pequeños
                            {
                                neighbors.Add((nx, ny, 1));
                            }
                        }
                    }
                }

                if (neighbors.Count > 0)
                {
                    // Ordenamos los vecinos por peso de manera descendente para favorecer caminos más largos
                    neighbors.Sort((a, b) => b.Item3.CompareTo(a.Item3));
                    
                    // Seleccionamos preferentemente los vecinos con mayor peso
                    var selectedIndex = rand.NextDouble() < 0.85 ? 0 : rand.Next(neighbors.Count);
                    var (nx, ny, _) = neighbors[selectedIndex];

                    game_board[nx, ny] = path;
                    stack.Push((nx, ny));
                    distances[(nx, ny)] = currentDistance + 1;
                }
                if (rand.NextDouble() > 0.7) // 30% de probabilidad de continuar desde este punto
                {
                    stack.Push((x, y));
                }
            }

            // Verificamos si el camino es lo suficientemente largo
            if (!distances.ContainsKey((end_position_i, end_position_j)) || 
                distances[(end_position_i, end_position_j)] < MinPathLength)
            {
                // Si el camino es muy corto, regeneramos el laberinto
                Generate_Empty_Maze(game_board, wall);
                Generate_Maze(start_position_i, end_position_i, start_position_j, end_position_j, wall, path, game_board);
                return;
            }
        }

        public void Generate_Connected_Maze(
            List<(int, int)> startPoints,
            int centerX,
            int centerY,
            string wall,
            string path,
            string[,] game_board)
        {
            // Inicializamos con paredes
            Generate_Empty_Maze(game_board, wall);

            // Creamos caminos desde cada esquina hacia el centro
            foreach (var startPoint in startPoints)
            {
                CreatePathToCenter(startPoint.Item1, startPoint.Item2, centerX, centerY, "⬛", "⬜", game_board);
            }

            // Agregamos algunos caminos adicionales para hacer el laberinto más interesante
            AddAdditionalPaths(game_board, wall, path);
        }

        private void CreatePathToCenter(
            int startX, 
            int startY, 
            int centerX, 
            int centerY, 
            string wall, 
            string path, 
            string[,] game_board)
        {
            int currentX = startX;
            int currentY = startY;
            
            while (currentX != centerX || currentY != centerY)
            {
                game_board[currentX, currentY] = path;

                // Decidir si moverse en X o Y
                if (rand.Next(2) == 0 && currentX != centerX)
                {
                    // Moverse en X
                    currentX += currentX < centerX ? 1 : -1;
                }
                else if (currentY != centerY)
                {
                    // Moverse en Y
                    currentY += currentY < centerY ? 1 : -1;
                }
                else
                {
                    // Si Y está alineado, forzar movimiento en X
                    currentX += currentX < centerX ? 1 : -1;
                }

                // Agregar algunas ramificaciones aleatorias
                if (rand.NextDouble() < 0.2)
                {
                    AddRandomBranch(currentX, currentY, wall, path, game_board);
                }
            }
        }

        private void AddRandomBranch(int x, int y, string wall, string path, string[,] game_board)
        {
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };
            
            int steps = rand.Next(4, 10); // Aumenta la longitud de las ramificaciones
            int direction = rand.Next(4); // Dirección aleatoria
            
            int currentX = x;
            int currentY = y;
            
            for (int i = 0; i < steps; i++)
            {
                int newX = currentX + dx[direction];
                int newY = currentY + dy[direction];
                
                if (newX > 0 && newX < game_board.GetLength(0) - 1 &&
                    newY > 0 && newY < game_board.GetLength(1) - 1 &&
                    game_board[newX, newY] == wall)
                {
                    game_board[newX, newY] = path;
                    currentX = newX;
                    currentY = newY;
                }
                else
                {
                    break;
                }
            }
        }

        private void AddAdditionalPaths(string[,] game_board, string wall, string path)
        {
            // Aumentamos el número de intentos y caminos
            int maxAttempts = 300;
            int pathsAdded = 0;
            
            while (pathsAdded < 150 && maxAttempts > 0)
            {
                int x = rand.Next(1, game_board.GetLength(0) - 1);
                int y = rand.Next(1, game_board.GetLength(1) - 1);
                
                if (game_board[x, y] == "⬜")
                {
                    // Intentar crear un camino ancho
                    if (TryAddWidePath(x, y, "⬛", "⬜", game_board))
                    {
                        pathsAdded++;
                    }
                }
                maxAttempts--;
            }

            // Ensanchar caminos existentes
            WidenExistingPaths(game_board, "⬛", "⬜");
        }

        private void WidenExistingPaths(string[,] game_board, string wall, string path)
        {
            for (int i = 1; i < game_board.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < game_board.GetLength(1) - 1; j++)
                {
                    if (game_board[i, j] == "⬜")
                    {
                        // Reducir la probabilidad de ensanchar a 15%
                        if (rand.NextDouble() < 0.15)
                        {
                            // Solo ensanchar en direcciones cardinales, no en diagonales
                            for (int d = 0; d < 4; d++)
                            {
                                int dx = d < 2 ? d * 2 - 1 : 0;
                                int dy = d < 2 ? 0 : (d - 2) * 2 - 1;
                                
                                if (game_board[i + dx, j + dy] == "⬛")
                                {
                                    game_board[i + dx, j + dy] = "⬜";
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool TryAddPath(int x, int y, string wall, string path, string[,] game_board)
        {
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };
            
            int direction = rand.Next(4);
            int length = rand.Next(2, 5);
            
            List<(int, int)> newPath = new List<(int, int)>();
            int currentX = x;
            int currentY = y;
            
            for (int i = 0; i < length; i++)
            {
                int newX = currentX + dx[direction];
                int newY = currentY + dy[direction];
                
                if (newX <= 0 || newX >= game_board.GetLength(0) - 1 ||
                    newY <= 0 || newY >= game_board.GetLength(1) - 1)
                {
                    return false;
                }
                
                if (game_board[newX, newY] == "⬛")
                {
                    newPath.Add((newX, newY));
                    currentX = newX;
                    currentY = newY;
                }
                else if (game_board[newX, newY] == "⬜" && i > 1)
                {
                    // Si encontramos otro camino después de al menos 2 pasos, conectamos
                    foreach (var (px, py) in newPath)
                    {
                        game_board[px, py] = "⬜";
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
            return false;
        }

        private bool TryAddWidePath(int x, int y, string wall, string path, string[,] game_board)
        {
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };
            
            // Lista para almacenar las celdas que formarán el nuevo camino
            List<(int x, int y)> newPathCells = new List<(int x, int y)>();
            
            // Elegir una dirección aleatoria
            int direction = rand.Next(4);
            int length = rand.Next(3, 7); // Longitud del camino
            
            int currentX = x;
            int currentY = y;
            
            // Intentar crear un camino en la dirección elegida
            for (int i = 0; i < length; i++)
            {
                int newX = currentX + dx[direction];
                int newY = currentY + dy[direction];
                
                // Verificar límites y que no estemos muy cerca del borde
                if (newX <= 1 || newX >= game_board.GetLength(0) - 2 ||
                    newY <= 1 || newY >= game_board.GetLength(1) - 2)
                {
                    break;
                }
                
                // Verificar si podemos crear un camino ancho
                bool canCreateWidePath = true;
                for (int wx = -1; wx <= 1; wx++)
                {
                    for (int wy = -1; wy <= 1; wy++)
                    {
                        int checkX = newX + wx;
                        int checkY = newY + wy;
                        
                        // Si encontramos un camino existente que no es adyacente al inicio,
                        // podemos conectarnos a él
                        if (game_board[checkX, checkY] == "⬜" && 
                            (Math.Abs(checkX - x) > 1 || Math.Abs(checkY - y) > 1))
                        {
                            // Agregar todas las celdas acumuladas al camino
                            foreach (var cell in newPathCells)
                            {
                                game_board[cell.x, cell.y] = "⬜";
                            }
                            // Agregar la celda actual y sus adyacentes
                            for (int ax = -1; ax <= 1; ax++)
                            {
                                for (int ay = -1; ay <= 1; ay++)
                                {
                                    if (game_board[newX + ax, newY + ay] == "⬛")
                                    {
                                        game_board[newX + ax, newY + ay] = "⬜";
                                    }
                                }
                            }
                            return true;
                        }
                        
                        // Si encontramos algo que no es pared (excepto en el punto de inicio),
                        // no podemos crear el camino aquí
                        if ((checkX != x || checkY != y) && 
                            game_board[checkX, checkY] != "⬛")
                        {
                            canCreateWidePath = false;
                            break;
                        }
                    }
                    if (!canCreateWidePath) break;
                }
                
                if (!canCreateWidePath) break;
                
                // Agregar la celda actual a la lista de nuevo camino
                newPathCells.Add((newX, newY));
                currentX = newX;
                currentY = newY;
            }
            
            // Si el camino es lo suficientemente largo, crearlo
            if (newPathCells.Count >= 2)
            {
                foreach (var cell in newPathCells)
                {
                    // Crear un camino ancho
                    for (int wx = -1; wx <= 1; wx++)
                    {
                        for (int wy = -1; wy <= 1; wy++)
                        {
                            if (game_board[cell.x + wx, cell.y + wy] == "⬛")
                            {
                                game_board[cell.x + wx, cell.y + wy] = "⬜";
                            }
                        }
                    }
                }
                return true;
            }
            
            return false;
        }
    }
}
