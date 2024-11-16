namespace P_P
{
    abstract class BaseTramp
    {
        public string icon; // Icono del tramp
        public int[] positionRow = new int[100]; // Fila de la posición del tramp
        public int[] positionColumn = new int[100]; // Columna de la posición del trampprivate int nunmberOfTramps;
        private int numberOfTraps;
        public BaseTramp( string icon,int numberOfTraps)
        {
            this.icon = icon;
            this.numberOfTraps = numberOfTraps;
        }

        public bool CheckTrap(BaseCharacter baseCharacter, string[,] gameBoard , bool[,] trampBoard)
        {
            return trampBoard[baseCharacter.playerStartRow, baseCharacter.playerStartColumn];
        }
        public void CreateRandomTraps(string[,] gameBoard , int startRow , int endRow , int startColumn , int endColumn)
        {
            Random random = new Random();
            for (int i = 0; i < numberOfTraps; i++)
            {
                int row = random.Next(startRow, endRow);
                int column = random.Next(startColumn, endColumn);
                // Asegurarse de que la posición no esté ocupada
                if (gameBoard[row, column] == "⬜️") // Suponiendo que "⬜️" es un espacio vacío
                {
                    this.positionRow[i] = row;
                    this.positionColumn[i] =  column;
                    gameBoard[row, column] = icon; // Colocar una trampa
                }
                else
                {
                    i--; // Repetir si la posición ya está ocupada
                }
            }
        }
    }
}