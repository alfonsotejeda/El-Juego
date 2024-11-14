using System.Dynamic;

namespace P_P
// Quiero creear un enum para mejor comprension de las opciones
{
    public class Program
    {
        static void Main(string[] args)
        {

            System.Console.WriteLine("ytreeee");
            Menu menu = new Menu();
            menu.printMenu();
            System.Console.WriteLine();
            Console.Write("Elija una opción: ");
            string? userInput = Console.ReadLine();
            if (userInput != null && menu.choosenOpcion(userInput) == "1")
            {
                int rows = 33;
                int columns = 33;
                
                Board board = new Board(rows , columns);
                string [,] gameBoard = board.createBoard();

            //Define characters
            int player1StartRow = 1;
            int player1StartColumn = 1;
            BlueSquareCharacter blueSquareCharacter = new BlueSquareCharacter("🟦" , "defense" , 5 , ref player1StartRow , ref player1StartColumn);

            int player2StartRow = 30;
            int player2StartColumn = 30;
            RedSquareCharacter redSquareCharacter = new RedSquareCharacter("🟥", "attack" , 4 , ref player2StartRow , ref player2StartColumn);


            //Define Tramps 
            ClosePathTramp closePathTramp = new ClosePathTramp("🔳" , 5);

            closePathTramp.CreateRandomTraps( gameBoard , 0, rows / 2, 0, columns / 2);
            closePathTramp.CreateRandomTraps( gameBoard ,0, rows / 2, columns / 2, columns);
            closePathTramp.CreateRandomTraps( gameBoard , rows / 2, rows, 0, columns / 2);
            closePathTramp.CreateRandomTraps( gameBoard ,rows / 2, rows, columns / 2, columns);

                gameBoard[blueSquareCharacter.playerStartRow,blueSquareCharacter.playerStartColumn] = blueSquareCharacter.icon;
                // gameBoard[redSquareCharacter.playerStartRow,redSquareCharacter.playerStartColumn] = redSquareCharacter.icon;
                while(true)
                {
                        board.PrintBoard(gameBoard);
                        // board.PrintBoard(gameBoard);
                        // Console.ReadKey();
                        // board.createBoard();
                        for (int i = 0; i < blueSquareCharacter.movementCapacity; i++)
                        {
                            blueSquareCharacter.Move(ref blueSquareCharacter.playerStartRow, ref blueSquareCharacter.playerStartColumn, gameBoard, board);
                            
                            // Verificar trampa después del movimiento
                            if (closePathTramp.CheckTrap(blueSquareCharacter, gameBoard))
                            {
                                closePathTramp.ClosePath(blueSquareCharacter.playerStartRow, blueSquareCharacter.playerStartColumn, gameBoard);
                            }
                            
                            board.PrintBoard(gameBoard);
                        }
                        // for (int i = 0 ; i < redSquareCharacter.movementCapacity;i++)
                        // {
                        //     Console.WriteLine($"{redSquareCharacter.movementCapacity - i}: movements left");
                        //     redSquareCharacter.Move(ref redSquareCharacter.playerStartRow,ref redSquareCharacter.playerStartColumn,gameBoard,board);
                        //     board.PrintBoardSpectre(gameBoard);
                        // }
                }
            }
            else if(userInput != null && menu.choosenOpcion(userInput) == "2"){
                System.Console.WriteLine("En contrucción");
            }
            else if(userInput != null && menu.choosenOpcion(userInput) == "3"){
                System.Console.WriteLine("En contrucción");
            }
        }
    }
}