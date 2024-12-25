using P_P.characters;
using P_P.board;
namespace P_P.tramps
{
    class ReduceLiveTramp : BaseTramp
    {
        public ReduceLiveTramp( string? trampId) : base(trampId)
        {
        }

        public override void Interact(Shell[,] gameboard, BaseCharacter character ,List<BaseCharacter> characters , List<BaseTramp> tramps)
        {
            Random random = new Random();
            int liveToReduce = random.Next(1, 50);
            character.Live -= liveToReduce;
            Console.WriteLine($"You have lost {liveToReduce} points of life. You have {character.Live} points of life left.");
            if (liveToReduce > 40)
            {
                Console.WriteLine("Critical tramp!");
            }
            CleanObjectPosition(gameboard , character);
            Console.ReadKey();
        }

        public override void CreateRandomTraps(Shell[,] gameBoard, BaseTramp tramp, int startRow, int endRow,
            int startColumn, int endColumn, int numberOfTraps)
        {
            Random random = new Random();
            for (int i = 0; i < numberOfTraps; i++)
            {
                int row = random.Next(startRow, endRow);
                int column = random.Next(startColumn, endColumn);
                if (gameBoard[row, column].GetType() == typeof(path))
                {
                    this.positionRow[i] = row;
                    this.positionColumn[i] = column;
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