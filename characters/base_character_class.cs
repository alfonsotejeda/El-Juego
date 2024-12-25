using P_P.board;
using P_P.PrintingMethods;
using P_P;
using P_P.tramps;

namespace P_P.characters
{
    public class BaseCharacter
    {
        public string Icon;
        public string Ability;
        public int MovementCapacity;
        public int PlayerColumn;
        public int PlayerRow;
        public int Live = 100;

        public BaseCharacter(string name, string ability, int movementCapacity, int playerColumn, int playerRow)
        {
            this.Icon = name ?? throw new ArgumentNullException(nameof(name));
            this.Ability = ability ?? throw new ArgumentNullException(nameof(ability));
            this.MovementCapacity = movementCapacity;
            this.PlayerColumn = playerColumn;
            this.PlayerRow = playerRow;
        }

        public void Move(ref int playerRow, ref int playerColumn, ref int movementCapacity, Shell[,] gameBoard,
            BaseCharacter character)
        {
            ConsoleKeyInfo key = Console.ReadKey();
            int newRow = playerRow;
            int newColumn = playerColumn;

            switch (key.Key)
            {
                case ConsoleKey.W:
                    newRow--;
                    break;
                case ConsoleKey.S:
                    newRow++;
                    break;
                case ConsoleKey.A:
                    newColumn--;
                    break;
                case ConsoleKey.D:
                    newColumn++;
                    break;
            }

            if (newRow >= 0 && newRow < gameBoard.GetLength(0) &&
                newColumn >= 0 && newColumn < gameBoard.GetLength(1) &&
                gameBoard[newRow, newColumn].GetType() != typeof(wall) &&
                !gameBoard[newRow, newColumn].HasCharacter)
            {
                // Limpiar posición anterior
                gameBoard[playerRow, playerColumn].HasCharacter = false;
                gameBoard[playerRow, playerColumn].CharacterIcon = null;
                gameBoard[playerRow, playerColumn] = new path("⬜️");

                // Actualizar nueva posición
                playerRow = newRow;
                playerColumn = newColumn;
                gameBoard[playerRow, playerColumn].HasCharacter = true;
                gameBoard[playerRow, playerColumn].CharacterIcon = this.Icon;
                // gameBoard[playerRow, playerColumn].IsPath = false;
                // Actualizar el tablero
                movementCapacity--;
                PrintingMethods.PrintingMethods printingMethods = new PrintingMethods.PrintingMethods();
                printingMethods.PrintGameSpectre(gameBoard, character);
            }
        }

        public void PlaceCharacter(Shell[,] gameBoard, BaseCharacter character)
        {
            gameBoard[character.PlayerRow, character.PlayerColumn].HasCharacter = true;
            gameBoard[character.PlayerRow, character.PlayerColumn].CharacterIcon = character.Icon;
        }

        public void TakeTurn(Shell[,] gameBoard, BaseCharacter character, List<BaseTramp> tramps , List<BaseCharacter> characters)
        {
            PrintingMethods.PrintingMethods printingMethods = new PrintingMethods.PrintingMethods();
            ConsoleKeyInfo key = Console.ReadKey();
            while (character.MovementCapacity != 0)
            {
                if (key.Key == ConsoleKey.H)
                {
                    character.UseAbility(gameBoard , character);    
                }

                if (key.Key == ConsoleKey.C)
                {
                    Console.WriteLine("Introduce el personaje con el que quieres cambiar");
                    DisplayCharactersToChange(characters , character);
                    int characterToChange = int.Parse(Console.ReadLine());
                    character.ChangeWith(character , characters[characterToChange] , gameBoard);
                }
                
                character.Move(ref character.PlayerRow, ref character.PlayerColumn, ref character.MovementCapacity,
                    gameBoard, character);
                if (gameBoard[character.PlayerRow, character.PlayerColumn].HasObject)
                {
                    string? objectType = gameBoard[character.PlayerRow, character.PlayerColumn].ObjectType;
                    {
                        if (objectType == "tramp")
                        {
                            foreach (BaseTramp tramp in tramps)
                            {
                                if (tramp.trampId == gameBoard[character.PlayerRow, character.PlayerColumn].ObjectId)
                                {
                                    tramp.Interact(gameBoard, character);
                                }
                            }
                        }
                    }
                    printingMethods.PrintGameSpectre(gameBoard, character);
                }
            }

            MovementCapacity = 5;
            Console.WriteLine("Turno finalizado . Toca enter para pasar al siguiente jugador");
            Console.ReadKey();
            printingMethods.PrintGameSpectre(gameBoard, character);
        }

        public virtual void UseAbility(Shell[,] gameboard , BaseCharacter character)
        {
            
        }
        public void ChangeWith(BaseCharacter character , BaseCharacter charactertoChange , Shell[,] gameBoard)
        {
            gameBoard[character.PlayerRow, character.PlayerColumn].HasCharacter = false;
            gameBoard[character.PlayerRow, character.PlayerColumn].CharacterIcon = null;
            gameBoard[charactertoChange.PlayerRow, charactertoChange.PlayerColumn].HasCharacter = false;
            gameBoard[charactertoChange.PlayerRow, charactertoChange.PlayerColumn].CharacterIcon = null;
            
            gameBoard[character.PlayerRow, character.PlayerColumn].HasCharacter = true;
            gameBoard[character.PlayerRow, character.PlayerColumn].CharacterIcon = charactertoChange.Icon;
            gameBoard[charactertoChange.PlayerRow, charactertoChange.PlayerColumn].HasCharacter = true;
            gameBoard[charactertoChange.PlayerRow, charactertoChange.PlayerColumn].CharacterIcon = character.Icon;

            this.MovementCapacity = 0;
        }
        private void DisplayCharactersToChange(List<BaseCharacter> characters , BaseCharacter character)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i] != character) Console.WriteLine($"Personaje {i} : {characters[i].Icon}");
            }
        }
    }
}
