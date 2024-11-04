namespace P_P
{
    public class Maze_Generator
    {
        // The main method for generating a maze. It takes the start and end positions,
        // as well as the symbols for walls, paths, and the game board.
        // Initialize all elements of the game board as walls
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
            // Create a stack of (x, y) coordinates to keep track of the cells that need to be processed.
            Stack<(int, int)> stack = new Stack<(int, int)>();
            // Push the start position onto the stack and mark it as a path.
            stack.Push((start_position_i, start_position_j));
            game_board[start_position_i, start_position_j] = path;

            // Define the possible directions to move from a cell (up, down, left, right).
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            // Initialize a random number generator for selecting a random neighbor.
            Random rand = new Random();

            // Process the cells in the stack until it's empty.
            while (stack.Count > 0 && game_board[end_position_i, end_position_j] != path) 
            {
                // Pop the current cell from the stack.
                var (x, y) = stack.Pop();
                // Create a list to store the unvisited neighbors of the current cell.
                var neighbors = new List<(int, int)>();

                // Check each possible direction from the current cell.
                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];

                    // If the neighbor is within the game board and is a wall, add it to the list of neighbors.
                    if (nx >= 0 && nx <= end_position_i && ny >= 0 && ny <= end_position_j && game_board[nx, ny] == wall)
                    {
                        neighbors.Add((nx, ny));
                    }
                }

                // If the current cell has unvisited neighbors, process them.
                if (neighbors.Count > 0)
                {
                    // Push the current cell back onto the stack.
                    stack.Push((x, y));
                    // Select a random neighbor.
                    var (nx, ny) = neighbors[rand.Next(neighbors.Count)];
                    // Mark the neighbor as a patouarh and remove the wall between the current cell and the neighbor.
                    game_board[nx, ny] = path;
                    // game_board[(x + nx) / 2, (y + ny) / 2] = path;
                    // Push the neighbor onto the stack.
                    stack.Push((nx, ny));
                }
            }

            // Mark the end position as the end point.
            //game_board[end_position_i, end_position_j] = "🏆";
        }
    }
}
