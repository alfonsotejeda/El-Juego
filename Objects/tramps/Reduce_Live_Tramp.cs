using P_P.characters;
using P_P.board;
using Spectre.Console;
namespace P_P.tramps
{
    class ReduceLiveTramp : BaseTramp
    {
        public ReduceLiveTramp(string? trampId) : base(trampId ?? throw new ArgumentNullException(nameof(trampId)))
        {
        }

        public override void Interact(Shell[,] gameboard, BaseCharacter character ,List<BaseCharacter> characters , List<BaseTramp> tramps)
        {
            PrintingMethods.PrintingMethods printingMethods = new PrintingMethods.PrintingMethods();
            Random random = new Random();
            int liveToReduce = random.Next(1, 50);
            character.Live -= liveToReduce;
            
            printingMethods.layout["Bottom"].Update(new Panel($"Has perdido {liveToReduce} puntos de vida. Te quedan {character.Live} puntos de vida.").Expand());
            printingMethods.PrintGameSpectre(gameboard , character , characters , tramps);
            if (liveToReduce > 40)
            {
                printingMethods.layout["Bottom"].Update(new Panel("Trampa crítica!").Expand());
                printingMethods.PrintGameSpectre(gameboard , character , characters , tramps);
            }
            CleanObjectPosition(gameboard , character);
            Console.ReadKey();
        }

        public override void CreateRandomTraps(Shell[,] gameBoard, BaseTramp tramp, int startRow, int endRow, int startColumn, int endColumn, int numberOfTraps)
        {
            Random random = new Random();
            int centerRow = gameBoard.GetLength(0) / 2;
            int centerColumn = gameBoard.GetLength(1) / 2;
            for (int i = 0; i < numberOfTraps; i++)
            {
                int row = random.Next(startRow, endRow);
                int column = random.Next(startColumn, endColumn);
                if (gameBoard[row, column].GetType() == typeof(P_P.board.Path) && !(row == centerRow && column == centerColumn))
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