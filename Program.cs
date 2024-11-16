using System.Dynamic;

namespace P_P
{
    public class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.printMenu();
            System.Console.WriteLine();
            Console.Write("Elija una opción: ");
            string? userInput = Console.ReadLine();
            if (userInput != null && menu.choosenOpcion(userInput) == "1")
            {
                int rows = 33;
                int columns = 33;
                
                Board board = new Board(columns , rows);
                string [,] gameBoard = board.createBoard();
                
                //Define tramps
                ClosePathTramp closePathTramp = new ClosePathTramp("🔳" , 7);
                closePathTramp.CreateRandomTraps( gameBoard , 0, rows / 2, 0, columns / 2);
                closePathTramp.CreateRandomTraps( gameBoard ,0, rows / 2, columns / 2, columns);
                closePathTramp.CreateRandomTraps( gameBoard , rows / 2, rows, 0, columns / 2);
                closePathTramp.CreateRandomTraps(gameBoard, rows / 2, rows, columns / 2, columns);
                bool[,] closeTrampBoard = new bool[rows,columns];

                for (int i = 1; i < rows; i++)
                {
                    for (int j = 1; j < columns; j++)
                    {
                        if (gameBoard[i, j] == "🔳")
                        {
                            closeTrampBoard[i, j] = true;
                            gameBoard[i, j] = "⬜️";
                        }
                    }
                }


                //Define characters
                int player1StartRow = 1;
                int player1StartColumn = 1;
                BlueSquareCharacter blueSquareCharacter = new BlueSquareCharacter("🟦" , "defense" , 100 , ref player1StartRow , ref player1StartColumn);

                int player2StartRow = 31;
                int player2StartColumn = 31;
                RedSquareCharacter redSquareCharacter = new RedSquareCharacter("🟥", "attack" , 4 , ref player2StartRow , ref player2StartColumn);
       
                gameBoard[blueSquareCharacter.playerStartRow,blueSquareCharacter.playerStartColumn] = blueSquareCharacter.icon;
                gameBoard[redSquareCharacter.playerStartRow,redSquareCharacter.playerStartColumn] = redSquareCharacter.icon;
                while(true)
                {
                        board.PrintBoardSpectre(gameBoard);
                        for (int i = 0; i < blueSquareCharacter.movementCapacity; i++)
                        {
                            blueSquareCharacter.Move(ref blueSquareCharacter.playerStartRow, ref blueSquareCharacter.playerStartColumn, gameBoard, board);
                            if (closeTrampBoard[blueSquareCharacter.playerStartRow,blueSquareCharacter.playerStartColumn])
                            {
                                closePathTramp.ClosePath(blueSquareCharacter.playerStartRow, blueSquareCharacter.playerStartColumn , gameBoard);
                                closeTrampBoard[blueSquareCharacter.playerStartRow, blueSquareCharacter.playerStartColumn] = false;
                                Console.WriteLine("Haz Pisado una trampa!!!!!!! \n Presiona una tecla para continuar...");
                                Console.ReadKey();
                                
                            }
                            board.PrintBoardSpectre(gameBoard);
                        }
                        for (int i = 0 ; i < redSquareCharacter.movementCapacity;i++)
                        {
                            redSquareCharacter.Move(ref redSquareCharacter.playerStartRow,ref redSquareCharacter.playerStartColumn,gameBoard,board);
                            board.PrintBoardSpectre(gameBoard);
                        }
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