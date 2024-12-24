using P_P.board;
using P_P.characters;

namespace P_P.tramps
{
    public class BaseTramp : InteractiveObjects
    {
        public int[] positionRow = new int[100]; 
        public int[] positionColumn = new int[100]; 
        private int numberOfTraps;
        public string? trampId;
        public BaseTramp(int numberOfTraps , string? trampId) : base("tramp")
        {
            this.numberOfTraps = numberOfTraps;
            this.trampId = trampId;
        }

        public override bool CheckInteraction(BaseCharacter baseCharacter, Shell[,] gameBoard)
        {
            return gameBoard[baseCharacter.PlayerRow, baseCharacter.PlayerColumn].HasObject && gameBoard[baseCharacter.PlayerRow, baseCharacter.PlayerColumn].ObjectType == "tramp";
        }
        public void CreateRandomTraps(Shell[,] gameBoard ,BaseTramp tramp, int startRow , int endRow , int startColumn , int endColumn)
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
                   
                }
                else
                {
                    i--;
                }
            }
        }
        public override void Interact(Shell[,] gameboard, BaseCharacter character)
        {
            Console.WriteLine("Interacción con la trampa.");
        }
    }
}