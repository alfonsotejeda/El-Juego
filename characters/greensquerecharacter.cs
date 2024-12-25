using P_P.board;
using P_P.tramps;
using P_P.PrintingMethods;
namespace P_P.characters
{
    public class GreenSquareCharacter : BaseCharacter
    {
        public new string icon;
        public GreenSquareCharacter(string icon, string ability, ref int movementCapacity, ref int playerRow, ref int playerColumn)
            : base(icon, ability, movementCapacity, playerColumn, playerRow)
        {
            this.icon = icon ?? throw new ArgumentNullException(nameof(icon));
        }

        public override void UseAbility(Shell[,] gameboard, BaseCharacter character, List<BaseTramp> tramps, List<BaseCharacter> characters)
        {
            bool quitTrampController = false;
            PrintingMethods.PrintingMethods printingMethods = new PrintingMethods.PrintingMethods();
            if (character.PlayerColumn <= gameboard.GetLength(1)/2 && character.PlayerRow <= gameboard.GetLength(0)/2)
            {
                for (int row = 0; row < gameboard.GetLength(0)/2; row++)
                {
                    for(int column = 0 ; column < gameboard.GetLength(1)/2; column++)
                    {
                        if (gameboard[row, column].GetType() == typeof(path) && gameboard[row , column].HasObject && gameboard[row , column].ObjectType == "tramp")
                        {
                            Console.WriteLine($"Has quitado la trampa: {gameboard[row, column].ObjectId} de la posición {row} , {column}");
                            
                            gameboard[row, column].HasObject = false;
                            quitTrampController = true;
                            break;
                        }
                    }
                    if (quitTrampController)
                    {
                        break;
                    }
                }    
            }
            else if (character.PlayerColumn >= gameboard.GetLength(1)/2 && character.PlayerRow <= gameboard.GetLength(0)/2)
            {
                for (int row = 0; row < gameboard.GetLength(0)/2; row++)
                {
                    for(int column = gameboard.GetLength(1)/2 ; column < gameboard.GetLength(1); column++)
                    {
                        if (gameboard[row, column].GetType() == typeof(path) && gameboard[row , column].HasObject && gameboard[row , column].ObjectType == "tramp")
                        {
                            Console.WriteLine($"Has quitado la trampa: {gameboard[row, column].ObjectId} de la posición {row} , {column}");
                            
                            gameboard[row, column].HasObject = false;
                            quitTrampController = true;
                            break;
                        }
                    }
                    if (quitTrampController)
                    {
                        break;
                    }
                }    
            }
            else if (character.PlayerColumn <= gameboard.GetLength(1)/2 && character.PlayerRow >= gameboard.GetLength(0)/2)
            {
                for (int row = gameboard.GetLength(0)/2; row < gameboard.GetLength(0); row++)
                {
                    for(int column = 0 ; column < gameboard.GetLength(1)/2; column++)
                    {
                        if (gameboard[row, column].GetType() == typeof(path) && gameboard[row , column].HasObject && gameboard[row , column].ObjectType == "tramp")
                        {
                            Console.WriteLine($"Has quitado la trampa: {gameboard[row, column].ObjectId} de la posición {row} , {column}");
                            
                            gameboard[row, column].HasObject = false;
                            quitTrampController = true;
                            break;
                        }
                    }
                    if (quitTrampController)
                    {
                        break;
                    }
                }    
            }
            else if (character.PlayerColumn >= gameboard.GetLength(1)/2 && character.PlayerRow >= gameboard.GetLength(0)/2)
            {
                for (int row = gameboard.GetLength(0)/2; row < gameboard.GetLength(0); row++)
                {
                    for(int column = gameboard.GetLength(1)/2 ; column < gameboard.GetLength(1); column++)
                    {
                        if (gameboard[row, column].GetType() == typeof(path) && gameboard[row , column].HasObject && gameboard[row , column].ObjectType == "tramp")
                        {
                            Console.WriteLine($"Has quitado la trampa: {gameboard[row, column].ObjectId} de la posición {row} , {column}");
                            
                            gameboard[row, column].HasObject = false;
                            quitTrampController = true;
                            break;
                        }
                    }
                    if (quitTrampController)
                    {
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("No hay trampas cerca");
            }
            Console.ReadKey();
        }
    }
}