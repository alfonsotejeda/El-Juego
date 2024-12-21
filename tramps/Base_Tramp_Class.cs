using P_P.board;
using P_P.characters;

namespace P_P.tramps
{
    public class BaseTramp : Shell
    {
        public int[] positionRow = new int[100]; // Fila de la posición del tramp
        public int[] positionColumn = new int[100]; // Columna de la posición del trampprivate int nunmberOfTramps;
        private int numberOfTraps;
        public string? trampId;
        public BaseTramp(int numberOfTraps , string? trampId) : base()
        {
            this.numberOfTraps = numberOfTraps;
            this.trampId = trampId;
        }

        public bool CheckTrap(BaseCharacter baseCharacter, Shell[,] gameBoard)
        {
            if (gameBoard[baseCharacter.PlayerRow, baseCharacter.PlayerColumn].GetType() == typeof(BaseTramp))
            {
                return true;
            }

            return false;
        }
        // public void CreateRandomTraps(Shell[,] gameBoard ,BaseTramp tramp, int startRow , int endRow , int startColumn , int endColumn)
        // {
        //     Random random = new Random();
        //     for (int i = 0; i < numberOfTraps; i++)
        //     {
        //         int row = random.Next(startRow, endRow);
        //         int column = random.Next(startColumn, endColumn);
        //         // Asegurarse de que la posición no esté ocupada
        //         if (gameBoard[row, column].GetType() == typeof(path))
        //         {
        //             this.positionRow[i] = row;
        //             this.positionColumn[i] =  column;
        //            
        //         }
        //         else
        //         {
        //             i--; // Repetir si la posición ya está ocupada
        //         }
        //     }
        // }
    }
}