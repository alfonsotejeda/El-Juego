namespace P_P
{
    abstract class BaseTramp
    {
        public string icon; // Icono del tramp
        public int[] positionRow = new int[100]; // Fila de la posición del tramp
        public int[] positionColumn = new int[100]; // Columna de la posición del trampprivate int nunmberOfTramps;
        private int numberOfTraps;
        public string? trampId;
        public BaseTramp( string icon,int numberOfTraps , string? trampId)
        {
            this.icon = icon;
            this.numberOfTraps = numberOfTraps;
            this.trampId = trampId;
        }

        public bool CheckTrap(BaseCharacter baseCharacter, Shell[,] gameBoard)
        {
            if (gameBoard[baseCharacter.playerStartRow, baseCharacter.playerStartColumn].IsTramp)
            {
                return true;
            }

            return false;
        }
        public void CreateRandomTraps(Shell[,] gameBoard , int startRow , int endRow , int startColumn , int endColumn)
        {
            Random random = new Random();
            for (int i = 0; i < numberOfTraps; i++)
            {
                int row = random.Next(startRow, endRow);
                int column = random.Next(startColumn, endColumn);
                // Asegurarse de que la posición no esté ocupada
                if (gameBoard[row, column].IsPath == true) // 
                {
                    this.positionRow[i] = row;
                    this.positionColumn[i] =  column;
                    gameBoard[row, column].IsTramp = true;
                    gameBoard[row, column].TrampIcon = icon ;
                }
                else
                {
                    i--; // Repetir si la posición ya está ocupada
                }
            }
        }
    }
}