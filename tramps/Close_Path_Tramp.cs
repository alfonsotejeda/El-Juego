namespace P_P
{
    class ClosePathTramp : BaseTramp
    {
        public ClosePathTramp(int numberOfTraps , string? trampId) : base( numberOfTraps , trampId)
        {
        }
        public void ClosePath(int row, int column, Shell[,] gameBoard)
        {
            Random random = new Random();
            int direction = random.Next(0, 4); // 0: arriba, 1: abajo, 2: izquierda, 3: derecha

            switch (direction)
            {
                case 0: // Arriba
                    if (row > 1) 
                    {
                        gameBoard[row - 1, column].IsWall = true;
                        gameBoard[row - 1, column].IsPath = false;
                    }
                    break;
                case 1: // Abajo
                    if (row < gameBoard.GetLength(0) - 1)
                    {
                        gameBoard[row + 1, column].IsWall = true;
                        gameBoard[row + 1, column].IsPath = false;
                    }
                    break;
                case 2: // Izquierda
                    if (column > 0)
                    {
                        gameBoard[row, column - 1].IsWall = true;
                        gameBoard[row, column - 1].IsPath = false;
                    }
                    break;
                case 3: // Derecha
                    if (column < gameBoard.GetLength(1) - 1)
                    {
                        gameBoard[row, column + 1].IsWall = true;
                        gameBoard[row, column + 1].IsPath = false;
                    }
                    break;
            }
        }
        
    }
}
