using P_P.board;

namespace P_P.tramps
{
    class ClosePathTramp : BaseTramp
    {
        string? trampId;
        public ClosePathTramp(string? trampId) : base( trampId)
        {
            this.trampId = trampId;
        }
        public void ClosePath(int row, int column, Shell[,] gameBoard)
        {
            // Random random = new Random();
            // int direction = random.Next(0, 4); // 0: arriba, 1: abajo, 2: izquierda, 3: derecha
            //
            // switch (direction)
            // {
            //     case 0: // Arriba
            //         if (row > 1) 
            //         {
            //             gameBoard[row - 1, column].IsWall = true;
            //             gameBoard[row - 1, column].IsPath = false;
            //         }
            //         break;
            //     case 1: // Abajo
            //         if (row < gameBoard.GetLength(0) - 1)
            //         {
            //             gameBoard[row + 1, column].IsWall = true;
            //             gameBoard[row + 1, column].IsPath = false;
            //         }
            //         break;
            //     case 2: // Izquierda
            //         if (column > 0)
            //         {
            //             gameBoard[row, column - 1].IsWall = true;
            //             gameBoard[row, column - 1].IsPath = false;
            //         }
            //         break;
            //     case 3: // Derecha
            //         if (column < gameBoard.GetLength(1) - 1)
            //         {
            //             gameBoard[row, column + 1].IsWall = true;
            //             gameBoard[row, column + 1].IsPath = false;
            //         }
            //         break;
            // }
        }
        public virtual void CreateRandomTraps(Shell[,] gameBoard ,BaseTramp tramp, int startRow , int endRow , int startColumn , int endColumn , int numberOfTraps)
        {
            Random random = new Random();
            for (int i = 0; i < numberOfTraps; i++)
            {
                int row = random.Next(startRow, endRow);
                int column = random.Next(startColumn, endColumn);
                if (gameBoard[row, column].GetType() == typeof(path))
                {
                    this.positionRow[i] = row;
                    this.positionColumn[i] =  column;
                    gameBoard[row, column].HasObject = true;
                    gameBoard[row, column].ObjectType = "tramp";
                    gameBoard[row, column].ObjectId = trampId;
                   
                }
                else
                {
                    i--;
                }
            }
        }
        
    }
}
