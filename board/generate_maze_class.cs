using Random = System.Random;

namespace P_P.board
{
    public class MazeGenerator
    {
        public void GenerateMaze(int startRow, int endRow, int startCol, int endCol, Shell[,] gameBoard)
        {
            for (int row = startRow; row < endRow; row++)
            {
                for (int col = startCol; col < endCol; col++)
                { 
                    gameBoard[row, col] = new wall("🟫");                  
                }
            }


            int currentRow = startRow + 1;
            int currentCol = startCol + 1;
            GenerateRecursiveMaze(currentRow, currentCol, startRow, endRow, startCol, endCol, gameBoard);
            RandomizeWalls(startRow, endRow, startCol, endCol, gameBoard);
        }

        private void GenerateRecursiveMaze(int currentRow, int currentCol, int startRow, int endRow, int startCol, int endCol, Shell[,] maze )
        {
            maze[currentRow , currentCol] = new path("⬜️");
            Random random = new Random();
            int[] rowDirections = { 0, 2, 0, -2 };
            int[] colDirections = { -2, 0, 2, 0 };

            var directions = Enumerable.Range(0,4).ToList();

            while(directions.Count > 0)
            {
                int index = random.Next(directions.Count);
                int direction = directions[index];
                directions.RemoveAt(index);
                int newRow = currentRow + rowDirections[direction]; 
                int newCol = currentCol + colDirections[direction]; 

                if (newRow >= startRow && newRow < endRow &&
                    newCol >= startCol && newCol < endCol &&
                    maze[newRow,newCol].GetType() == typeof(wall))
                {
                    int middleRow = currentRow+rowDirections[direction]/2;
                    int middleCol = currentCol+colDirections[direction]/2;
                    maze[middleRow, middleCol] = new path("⬜️");
                    GenerateRecursiveMaze(newRow , newCol , startRow , endRow , startCol , endCol, maze);
                }

            }
        }

        private static void RandomizeWalls(int startRow, int endRow, int startCol, int endCol, Shell[,] gameBoard)
        {
            Random random = new Random();
            for (int row = startRow + 1; row < endRow - 1; row++)
            {
                for (int col = startCol + 1; col < endCol - 1; col++)
                {
                    if (gameBoard[row, col].GetType() == typeof(wall))
                    {
                        int chance = 10;
                        if (random.Next(0, 100) < chance)
                        {
                            gameBoard[row, col] = new path("⬜️");

                        }
                    }
                }
            }
        }
    }
}