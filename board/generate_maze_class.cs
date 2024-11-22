namespace P_P
{
    public class MazeGenerator
    {
        public void GenerateMaze(int startRow, int endRow, int startCol, int endCol, Shell[,] gameBoard)
        {
            for (int row = startRow; row < endRow; row++)
            {
                for (int col = startCol; col < endCol; col++)
                { 
                    gameBoard[row, col].IsWall = true;                  
                }
            }


            int currentRow = startRow + 1;
            int currentCol = startCol + 1;
            GenerateRecursiveMaze(currentRow, currentCol, startRow, endRow, startCol, endCol, gameBoard);
            RandomizeWalls(startRow, endRow, startCol, endCol, gameBoard);

            //center
            gameBoard[gameBoard.GetLength(0)/2 , gameBoard.GetLength(1)/2].IsTrophy = true;
            //across the center
            for (int row = gameBoard.GetLength(0)/2 - 1; row <= gameBoard.GetLength(0)/2 + 1; row ++)
            {
                for(int column = gameBoard.GetLength(1)/2 - 1; column <= gameBoard.GetLength(1)/2 + 1; column ++)
                {
                    if(!gameBoard[row , column].IsTrophy)
                    {
                        gameBoard[row , column].IsWall = false;
                        gameBoard[row , column].IsPath = true;
                    }
                }

            }
        }

        private void GenerateRecursiveMaze(int currentRow, int currentCol, int startRow, int endRow, int startCol, int endCol, Shell[,] maze )
        {
            maze[currentRow , currentCol].IsWall = false;
            maze[currentRow , currentCol].IsPath = true;
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
                    maze[newRow,newCol].IsWall)
                {
                    int middleRow = currentRow+rowDirections[direction]/2;
                    int middleCol = currentCol+colDirections[direction]/2;
                    maze[middleRow, middleCol].IsWall = false;
                    maze[middleRow , middleCol].IsPath = true;
                    GenerateRecursiveMaze(newRow , newCol , startRow , endRow , startCol , endCol, maze);
                }

            }
        }

        private void RandomizeWalls(int startRow, int endRow, int startCol, int endCol, Shell[,] gameBoard)
        {
            Random random = new Random();
            for (int row = startRow + 1; row < endRow - 1; row++)
            {
                for (int col = startCol + 1; col < endCol - 1; col++)
                {
                    if (gameBoard[row, col].IsWall)
                    {
                        int chance = 10;
                        if (random.Next(0, 100) < chance)
                        {
                            gameBoard[row, col].IsWall = false;
                            gameBoard[row, col].IsPath = true;
                        }
                    }
                }
            }
        }
    }
}