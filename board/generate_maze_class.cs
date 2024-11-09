namespace P_P
{
    public class MazeGenerator
    {
        public void GenerateMaze(int startRow, int endRow, int startCol, int endCol, string wallCharacter, string[,] gameBoard)
        {
            for (int row = startRow; row < endRow; row++)
            {
                for (int col = startCol; col < endCol; col++)
                {
                    gameBoard[row, col] = wallCharacter;
                }
            }


            int currentRow = startRow + 1;
            int currentCol = startCol + 1;
            RecursiveBacktracker(startRow, startCol, startRow, endRow, startCol, endCol, gameBoard , "⬜️" , wallCharacter);
        }

        private void RecursiveBacktracker(int currentRow, int currentCol, int startRow, int endRow, int startCol, int endCol, string[,] maze , string path , string wall)
        {
            maze[currentRow , currentCol] = path;
            Random random = new Random();
            int[] rowDirections = { 0, 2, 0, -2 };
            int[] colDirections = { -2, 0, 2, 0 };

            var directions = Enumerable.Range(0,4).ToList();

            while(directions.Count() > 0)
            {
                int index = random.Next(directions.Count);
                int direction = directions[index];
                directions.RemoveAt(index);
                int newRow = currentRow + rowDirections[direction]; 
                int newCol = currentCol + colDirections[direction]; 

                if (newRow >= startRow && newRow < endRow &&
                    newCol >= startCol && newCol < endCol &&
                    maze[newRow,newCol] == wall)
                {
                    int middleRow = currentRow+rowDirections[direction]/2;
                    int middleCol = currentCol+colDirections[direction]/2;
                    maze[middleRow , middleCol] = path;
                    RecursiveBacktracker(newRow , newCol , startRow , endRow , startCol , endCol, maze , "⬜️" , wall);
                }

            }
        }
    }
}