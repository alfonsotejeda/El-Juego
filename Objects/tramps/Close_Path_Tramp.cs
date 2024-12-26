using P_P.board;
using P_P.characters;
using Spectre.Console;

namespace P_P.tramps
{
    class ClosePathTramp : BaseTramp
    {
        new string? trampId;
        public ClosePathTramp(string? trampId) : base(trampId ?? "defaultTrampId")
        {
            this.trampId = trampId;
        }
        public override void Interact(Shell[,] gameboard, BaseCharacter character,List<BaseCharacter> characters , List<BaseTramp> tramps)
        {
            PrintingMethods.PrintingMethods printingMethods = new PrintingMethods.PrintingMethods();
            Random random = new Random();
            int direction = random.Next(0, 4); // 0: arriba, 1: abajo, 2: izquierda, 3: derecha
            int row = character.PlayerRow;
            int column = character.PlayerColumn;

            switch (direction)
            {
                case 0: // Arriba
                    if (row > 1)
                    {
                        gameboard[row - 1, column] = new Wall("🟫");
                        printingMethods.layout["Bottom"].Update(new Panel("Se ha cerrado el camino arriba").Expand());
                        Console.ReadKey();
                    }
                    break;
                case 1:
                    if (row < gameboard.GetLength(0) - 1)
                    {
                        gameboard[row + 1, column] = new Wall("🟫");
                        printingMethods.layout["Bottom"].Update(new Panel("Se ha cerrado el camino abajo").Expand());
                        Console.ReadKey();
                    }
                    break;
                case 2: // Izquierda
                    if (column > 0)
                    {
                        gameboard[row, column - 1] = new Wall("🟫");
                        printingMethods.layout["Bottom"].Update(new Panel("Se ha cerrado el camino a la izquierda").Expand());
                        Console.ReadKey();
                    }
                    break;
                case 3: // Derecha
                    if (column < gameboard.GetLength(1) - 1)
                    {
                        gameboard[row, column + 1] = new Wall("🟫");
                        printingMethods.layout["Bottom"].Update(new Panel("Se ha cerrado el camino a la derecha").Expand());
                        Console.ReadKey();
                    }
                    break;
            }

        }

        private void EnsurePathToCenter(Shell[,] gameboard, BaseCharacter character , PrintingMethods.PrintingMethods printingMethods)
        {
            int centerRow = gameboard.GetLength(0) / 2;
            int centerColumn = gameboard.GetLength(1) / 2;
            int row = character.PlayerRow;
            int column = character.PlayerColumn;

            while (row != centerRow || column != centerColumn)
            {
                if (row < centerRow && gameboard[row + 1, column].GetType() == typeof(Wall))
                {
                    gameboard[row + 1, column] = new P_P.board.Path("⬜️");
                }
                else if (row > centerRow && gameboard[row - 1, column].GetType() == typeof(Wall))
                {
                    gameboard[row - 1, column] = new P_P.board.Path("⬜️");
                }
                else if (column < centerColumn && gameboard[row
                , column + 1].GetType() == typeof(Wall))
                {
                    gameboard[row, column + 1] = new P_P.board.Path("⬜️");
                }
                else if (column > centerColumn && gameboard[row, column - 1].GetType() == typeof(Wall))
                {
                    gameboard[row, column - 1] = new P_P.board.Path("⬜️");
                }

                if (row < centerRow) row++;
                else if (row > centerRow) row--;
                if (column < centerColumn) column++;
                else if (column > centerColumn) column--;
            }

            printingMethods.layout["Bottom"].Update(new Panel("Se ha asegurado un camino al centro del laberinto").Expand());
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
