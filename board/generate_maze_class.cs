namespace P_P
{
    public class Maze_Generator{
        public void Generate_Maze(int start_position_i, int end_position_i, int start_position_j, int end_position_j, string wall, string[,] game_board)
        {
            for (int i = start_position_i; i < end_position_i; i++)
            {
                for (int j = start_position_j; j < end_position_j; j++) // Corrected the loop condition to end_position_j
                {
                    game_board[i, j] = wall; // Using the passed wall character instead of a hardcoded one
                }    
            }
        }
    }
}