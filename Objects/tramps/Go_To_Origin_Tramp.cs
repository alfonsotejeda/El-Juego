using P_P.characters;
using P_P.board;
namespace P_P.tramps
{
    class GoToOriginTramp : BaseTramp
    {
        new string? trampId;
        public GoToOriginTramp(string? trampId) : base(trampId ?? throw new ArgumentNullException(nameof(trampId)))
        {
            this.trampId = trampId ?? throw new ArgumentNullException(nameof(trampId));
        }
        public override void Interact(Shell[,] gameboard, BaseCharacter character,List<BaseCharacter> characters , List<BaseTramp> tramps)
        {
            PrintingMethods.PrintingMethods printingMethods = new PrintingMethods.PrintingMethods();
            if (character.PlayerColumn <= gameboard.GetLength(1)/2 && character.PlayerRow <= gameboard.GetLength(0)/2)
            {
                CleanPosition(gameboard, character);
                character.PlayerRow = 1;
                character.PlayerColumn = 1;
                gameboard[1, 1].HasCharacter = true;
                gameboard[1, 1].CharacterIcon = character.Icon;
                printingMethods.PrintGameSpectre(gameboard , character , characters , tramps);
                Console.WriteLine("Al origen");
                
            }
            else if (character.PlayerColumn >= gameboard.GetLength(1)/2 && character.PlayerRow <= gameboard.GetLength(0)/2)
            {
                CleanPosition(gameboard, character);
                character.PlayerRow = 1;
                character.PlayerColumn = gameboard.GetLength(1) - 2;
                gameboard[1, gameboard.GetLength(1) - 2].HasCharacter = true;
                gameboard[1, gameboard.GetLength(1) - 2].CharacterIcon = character.Icon;
                printingMethods.PrintGameSpectre(gameboard , character , characters , tramps);
                Console.WriteLine("Al origen");
            }
            else if (character.PlayerColumn <= gameboard.GetLength(1)/2 && character.PlayerRow >= gameboard.GetLength(0)/2)
            {
                CleanPosition(gameboard, character);
                character.PlayerRow = gameboard.GetLength(0) - 2;
                character.PlayerColumn = 1;
                gameboard[gameboard.GetLength(0) - 2, 1].HasCharacter = true;
                gameboard[gameboard.GetLength(0) - 2, 1].CharacterIcon = character.Icon;
                printingMethods.PrintGameSpectre(gameboard , character , characters , tramps);
                Console.WriteLine("Al origen");
            }
            else
            {
                CleanPosition(gameboard, character);
                character.PlayerRow = gameboard.GetLength(0) - 2;
                character.PlayerColumn = gameboard.GetLength(1) - 2;
                gameboard[gameboard.GetLength(0) - 2, gameboard.GetLength(1) - 2].HasCharacter = true;
                gameboard[gameboard.GetLength(0) - 2, gameboard.GetLength(1) - 2].CharacterIcon = character.Icon;
                printingMethods.PrintGameSpectre(gameboard , character , characters , tramps);
                Console.WriteLine("Al origen");
                
            }
            Console.ReadKey();
        }
        public override void CreateRandomTraps(Shell[,] gameBoard ,BaseTramp tramp, int startRow , int endRow , int startColumn , int endColumn , int numberOfTraps)
        {
            Random random = new Random();
            for (int i = 0; i < numberOfTraps; i++)
            {
                int row = random.Next(startRow, endRow);
                int column = random.Next(startColumn, endColumn);
                if (gameBoard[row, column].GetType() == typeof(P_P.board.Path))
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