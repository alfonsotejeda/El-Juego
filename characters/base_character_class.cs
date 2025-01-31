using P_P.board;
using P_P.PrintingMethods;
using P_P;
using P_P.tramps;
using Spectre.Console;

namespace P_P.characters
{
    public class BaseCharacter
    {
        public string Icon;
        public string Ability;
        public int MovementCapacity;
        public int _movementCapacity;
        public int PlayerColumn;
        public int PlayerRow;
        public int Live = 100;
        public int Countdown;
        public int Visibility;
        private int _visibility;
        private int _countdown;
        public  PrintingMethods.PrintingMethods printingMethods = new PrintingMethods.PrintingMethods();
        public static int NumberOfPlayers { get; private set; }
        
        public BaseCharacter(string name, string ability, int movementCapacity, int playerColumn, int playerRow, int countdown, int visibility)
        {
            this.Icon = name ?? throw new ArgumentNullException(nameof(name));
            this.Ability = ability ?? throw new ArgumentNullException(nameof(ability));
            this.MovementCapacity = movementCapacity;
            this._movementCapacity = movementCapacity;
            this.PlayerColumn = playerColumn;
            this.PlayerRow = playerRow;
            this.Countdown = countdown;
            this._countdown = countdown;
            this.Visibility = visibility;
            this._visibility = visibility;
        }

        public void Move(ref int playerRow, ref int playerColumn, ref int movementCapacity, Shell[,] gameBoard, BaseCharacter character, ConsoleKeyInfo key, List<BaseCharacter> characters, List<BaseTramp> tramps)
        {
            if (Live <= 0)
            {
                ReturnToStartPosition(gameBoard, character);
                Live = 100;
                printingMethods.layout["Bottom"].Update(new Panel("¡Has perdido toda tu vida! Volviendo a la posición inicial...").Expand());
                printingMethods.PrintGameSpectre(gameBoard, character, characters, tramps);
                return;
            }

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

            if (IsValidMove(newRow, newColumn, gameBoard))
            {
                UpdatePosition(ref playerRow, ref playerColumn, newRow, newColumn, gameBoard);
                movementCapacity--;
                printingMethods.layout["Bottom"].Update(new Panel("Activa tu habilidad con H o muevete con W,A,S,D").Expand());
                printingMethods.PrintGameSpectre(gameBoard , character , characters , tramps);
            }
        }

        private bool IsValidMove(int newRow, int newColumn, Shell[,] gameBoard)
        {
            return newRow >= 0 && newRow < gameBoard.GetLength(0) &&
                   newColumn >= 0 && newColumn < gameBoard.GetLength(1) &&
                   gameBoard[newRow, newColumn].GetType() != typeof(Wall) &&
                   !gameBoard[newRow, newColumn].HasCharacter;
        }

        private void UpdatePosition(ref int playerRow, ref int playerColumn, int newRow, int newColumn, Shell[,] gameBoard)
        {
            gameBoard[playerRow, playerColumn].HasCharacter = false;
            gameBoard[playerRow, playerColumn].CharacterIcon = null;
            gameBoard[playerRow, playerColumn] = new P_P.board.Path("⬜️");

            playerRow = newRow;
            playerColumn = newColumn;
            gameBoard[playerRow, playerColumn].HasCharacter = true;
            gameBoard[playerRow, playerColumn].CharacterIcon = this.Icon;
        }

        public void PlaceCharacter(Shell[,] gameBoard, BaseCharacter character)
        {
            gameBoard[character.PlayerRow, character.PlayerColumn].HasCharacter = true;
            gameBoard[character.PlayerRow, character.PlayerColumn].CharacterIcon = character.Icon;
            
        }

        public void TakeTurn(Shell[,] gameBoard, BaseCharacter character, List<BaseTramp> tramps , List<BaseCharacter> characters)
        {
            printingMethods.layout["Bottom"].Update(new Panel("Turno de " + character.Icon + "\nPulsa C para cambiar de personaje o cualquier otra tecla para moverte").Expand());
            printingMethods.PrintGameSpectre(gameBoard , character , characters , tramps);
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.C)
            {
                HandleCharacterChange(gameBoard, character, characters, tramps);
            }
            else
            {
                HandleMovementOrAbility(gameBoard, character, tramps, characters, key);
            }
            EndTurn(gameBoard, character, characters, tramps);
        }

        private void HandleCharacterChange(Shell[,] gameBoard, BaseCharacter character, List<BaseCharacter> characters, List<BaseTramp> tramps)
        {
            if (!HasEnoughPlayers())
            {
                printingMethods.layout["Bottom"].Update(new Panel("No hay suficientes jugadores para cambiar de personaje").Expand());
                printingMethods.PrintGameSpectre(gameBoard , character , characters , tramps);
                Console.ReadKey();
            }
            else
            {
                printingMethods.PrintGameSpectre(gameBoard , character , characters , tramps);
                int characterToChange = DisplayCharactersToChange(characters , character , gameBoard , tramps);
                character.ChangeWith(character , characters[characterToChange] , gameBoard);
                printingMethods.layout["Bottom"].Update(new Panel("Te has cambiado con el personaje " + characters[characterToChange].Icon).Expand());
                printingMethods.PrintGameSpectre(gameBoard , character , characters , tramps);
            }
        }

        private void HandleMovementOrAbility(Shell[,] gameBoard, BaseCharacter character, List<BaseTramp> tramps, List<BaseCharacter> characters, ConsoleKeyInfo key)
        {
            printingMethods.layout["Bottom"].Update(new Panel("Activa tu habilidad con H o muevete con W,A,S,D").Expand());
            printingMethods.PrintGameSpectre(gameBoard , character , characters , tramps);
            while (character.MovementCapacity != 0)
            {
                ConsoleKeyInfo key2 = Console.ReadKey();
                if (key2.Key == ConsoleKey.H && character.Countdown == character._countdown)
                {
                    character.UseAbility(gameBoard , character , tramps, characters);
                    printingMethods.layout["Bottom"].Update(new Panel("Habilidad usada").Expand());
                    this.Countdown = 0;
                    printingMethods.PrintGameSpectre(gameBoard, character, characters , tramps);
                }
                else
                {
                    character.Move(ref character.PlayerRow, ref character.PlayerColumn, ref character.MovementCapacity, gameBoard, character , key2 , characters , tramps);
                }
                HandleObjectInteraction(gameBoard, character, tramps, characters);
            }
        }

        private void HandleObjectInteraction(Shell[,] gameBoard, BaseCharacter character, List<BaseTramp> tramps, List<BaseCharacter> characters)
        {
            if (gameBoard[character.PlayerRow, character.PlayerColumn].HasObject)
            {
                string? objectType = gameBoard[character.PlayerRow, character.PlayerColumn].ObjectType;
                if (objectType == "tramp")
                {
                    foreach (BaseTramp tramp in tramps)
                    {
                        if (tramp.trampId == gameBoard[character.PlayerRow, character.PlayerColumn].ObjectId)
                        {
                            tramp.Interact(gameBoard, character , characters , tramps);
                        }
                    }
                }
                printingMethods.PrintGameSpectre(gameBoard, character, characters , tramps);
            }

            if (gameBoard[character.PlayerRow, character.PlayerColumn].IsCenter)
            {
                printingMethods.PrintVictoryMesagge(character);
            }
        }

        private void EndTurn(Shell[,] gameBoard, BaseCharacter character, List<BaseCharacter> characters, List<BaseTramp> tramps)
        {
            MovementCapacity = _movementCapacity;
            printingMethods.layout["Bottom"].Update(new Panel("Turno finalizado . Toca enter para pasar al siguiente jugador").Expand());
            if (character.Countdown < character._countdown)
            {
                character.Countdown ++;
            }
            printingMethods.PrintGameSpectre(gameBoard, character, characters , tramps);
            Console.ReadKey();
            
            printingMethods.PrintGameSpectre(gameBoard, character, characters , tramps);
        }

        public virtual void UseAbility(Shell[,] gameboard , BaseCharacter character , List<BaseTramp> tramps , List<BaseCharacter> characters)
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
            
            int tempRow = character.PlayerRow;
            int tempColumn = character.PlayerColumn;
        
            character.PlayerRow = charactertoChange.PlayerRow;
            character.PlayerColumn = charactertoChange.PlayerColumn;
            charactertoChange.PlayerRow = tempRow;
            charactertoChange.PlayerColumn = tempColumn;

            this.MovementCapacity = 0;
        }
        public virtual int DisplayCharactersToChange(List<BaseCharacter> characters, BaseCharacter character, Shell[,] gameBoard, List<BaseTramp> tramps)
        {
            // Crear las opciones de personajes
            var posibleChangeCharacters = new List<string>();
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i] != character)
                {
                    posibleChangeCharacters.Add($"Personaje {i} : {characters[i].Icon}");
                }
            }

            int selectedIndex = 0; // Índice del personaje seleccionado

            // Usar AnsiConsole.Live para manejar las actualizaciones dinámicas
            AnsiConsole.Live(printingMethods.layout).Start(ctx =>
            {
                bool selectionMade = false;

                while (!selectionMade)
                {
                    // Actualizar el layout["Bottom"] con las opciones del menú
                    var menuContent = new Panel(
                        $"Elige al jugador con el que quieres cambiar:\n\n" +
                        string.Join("\n", posibleChangeCharacters.Select((option, index) =>
                            index == selectedIndex
                                ? $"[green]> {option}[/]" // Opción seleccionada
                                : $"  {option}"          // Opciones no seleccionadas
                        ))
                    ).Expand();

                    printingMethods.layout["Bottom"].Update(menuContent);
                    ctx.Refresh();

                    // Capturar la entrada del usuario
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedIndex = (selectedIndex - 1 + posibleChangeCharacters.Count) % posibleChangeCharacters.Count;
                            break;
                        case ConsoleKey.DownArrow:
                            selectedIndex = (selectedIndex + 1) % posibleChangeCharacters.Count;
                            break;
                        case ConsoleKey.Enter:
                            selectionMade = true;
                            break;
                    }
                }

                // Acción tras seleccionar un personaje
                var selectedCharacter = characters[selectedIndex];
                printingMethods.layout["Bottom"].Update(
                    new Panel($"Te has cambiado con el personaje {selectedCharacter.Icon}").Expand()
                );
                ctx.Refresh();

                // Volver a imprimir el juego con la selección hecha
                printingMethods.PrintGameSpectre(gameBoard, character, characters, tramps);
            });
            string selectedCharacter = posibleChangeCharacters[selectedIndex];
            int selectedCharacterIndex = int.Parse(selectedCharacter.Split(' ')[1]);
            return selectedCharacterIndex;
        }
        public bool IsInFirstQuadrant(BaseCharacter character, Shell[,] gameboard)
        {
            return character.PlayerColumn <= gameboard.GetLength(1) / 2 && character.PlayerRow <= gameboard.GetLength(0) / 2;
        }

        public bool IsInSecondQuadrant(BaseCharacter character, Shell[,] gameboard)
        {
            return character.PlayerColumn >= gameboard.GetLength(1) / 2 && character.PlayerRow <= gameboard.GetLength(0) / 2;
        }

        public bool IsInThirdQuadrant(BaseCharacter character, Shell[,] gameboard)
        {
            return character.PlayerColumn <= gameboard.GetLength(1) / 2 && character.PlayerRow >= gameboard.GetLength(0) / 2;
        }

        public bool IsInFourthQuadrant(BaseCharacter character, Shell[,] gameboard)
        {
            return character.PlayerColumn >= gameboard.GetLength(1) / 2 && character.PlayerRow >= gameboard.GetLength(0) / 2;
        }

        // Método estático para establecer el número de jugadores
        public static void SetNumberOfPlayers(int players)
        {
            if (players < 1 || players > 4)
            {
                throw new ArgumentException("El número de jugadores debe estar entre 1 y 4");
            }
            NumberOfPlayers = players;
        }

        // Método para verificar si hay suficientes jugadores para una acción
        protected bool HasEnoughPlayers()
        {
            return NumberOfPlayers > 1;
        }

        private void ReturnToStartPosition(Shell[,] gameBoard, BaseCharacter character)
        {
            gameBoard[PlayerRow, PlayerColumn].HasCharacter = false;
            gameBoard[PlayerRow, PlayerColumn].CharacterIcon = null;
            gameBoard[PlayerRow, PlayerColumn] = new P_P.board.Path("⬜️");

            if (PlayerColumn == 1 && PlayerRow == 1)
            {
                PlayerRow = 1;
                PlayerColumn = 1;
            }
            else if (PlayerColumn == gameBoard.GetLength(1) - 2 && PlayerRow == 1)
            {
                PlayerRow = 1;
                PlayerColumn = gameBoard.GetLength(1) - 2;
            }
            else if (PlayerColumn == 1 && PlayerRow == gameBoard.GetLength(0) - 2)
            {
                PlayerRow = gameBoard.GetLength(0) - 2;
                PlayerColumn = 1;
            }
            else
            {
                PlayerRow = gameBoard.GetLength(0) - 2;
                PlayerColumn = gameBoard.GetLength(1) - 2;
            }

            gameBoard[PlayerRow, PlayerColumn].HasCharacter = true;
            gameBoard[PlayerRow, PlayerColumn].CharacterIcon = Icon;
        }
    }
}
