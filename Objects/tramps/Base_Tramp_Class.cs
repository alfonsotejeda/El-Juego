using P_P.board;
using P_P.characters;

namespace P_P.tramps
{
    public class BaseTramp : InteractiveObjects
    {
        public int[] positionRow = new int[100]; 
        public int[] positionColumn = new int[100];
        public string trampId;
        public BaseTramp(string trampId) : base("tramp")
        {
            this.trampId = trampId;
        }

        public override bool CheckInteraction(BaseCharacter baseCharacter, Shell[,] gameBoard)
        {
            return gameBoard[baseCharacter.PlayerRow, baseCharacter.PlayerColumn].HasObject && gameBoard[baseCharacter.PlayerRow, baseCharacter.PlayerColumn].ObjectType == "tramp";
        }

        public virtual void CreateRandomTraps(Shell[,] gameBoard, BaseTramp tramp, int startRow, int endRow, int startColumn, int endColumn, int numberOfTraps)
        {
            Random random = new Random();
            for (int i = 0; i < numberOfTraps; i++)
            {
                PlaceTrap(gameBoard, random, startRow, endRow, startColumn, endColumn, i);
            }
        }

        private void PlaceTrap(Shell[,] gameBoard, Random random, int startRow, int endRow, int startColumn, int endColumn, int index)
        {
            int row = random.Next(startRow, endRow);
            int column = random.Next(startColumn, endColumn);
            if (gameBoard[row, column].GetType() == typeof(P_P.board.Path) && !gameBoard[row, column].HasObject && !gameBoard[row, column].HasCharacter)
            {
                this.positionRow[index] = row;
                this.positionColumn[index] = column;
                gameBoard[row, column].HasObject = true;
                gameBoard[row, column].ObjectType = "tramp";
            }
            else
            {
                PlaceTrap(gameBoard, random, startRow, endRow, startColumn, endColumn, index);
            }
        }

        public override void Interact(Shell[,] gameboard, BaseCharacter character, List<BaseCharacter> characters, List<BaseTramp> tramps)
        {
            Console.WriteLine("Interacción con la trampa.");
        }
    }
}