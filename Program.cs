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
                string [,] gameBoard = board.CreateBoard();
                string [,] trampBoard = board.TrampBoard;
                
                //Define Tramps
                ClosePathTramp closePathTramp = new ClosePathTramp("🔳" , 7 , "C");
                
                //Define characters
                int player1StartRow = 1;
                int player1StartColumn = 1;
                BlueSquareCharacter blueSquareCharacter = new BlueSquareCharacter("🟦" , "defense" , 5 , ref player1StartRow , ref player1StartColumn);

                int player2StartRow = 31;
                int player2StartColumn = 31;
                RedSquareCharacter redSquareCharacter = new RedSquareCharacter("🟥", "attack" , 5 , ref player2StartRow , ref player2StartColumn);

                gameBoard[blueSquareCharacter.playerStartRow,blueSquareCharacter.playerStartColumn] = blueSquareCharacter.icon;
                gameBoard[redSquareCharacter.playerStartRow,redSquareCharacter.playerStartColumn] = redSquareCharacter.icon;
                while(true)
                {
                        board.PrintBoardSpectre(gameBoard);
                        for (int i = 0; i < blueSquareCharacter.movementCapacity; i++)
                        {
                            blueSquareCharacter.Move(ref blueSquareCharacter.playerStartRow, ref blueSquareCharacter.playerStartColumn, gameBoard, board);
                            if (trampBoard[blueSquareCharacter.playerStartRow, blueSquareCharacter.playerStartColumn] != " ")
                            {
                                if (trampBoard[blueSquareCharacter.playerStartRow, blueSquareCharacter.playerStartColumn] == closePathTramp.trampId)
                                {
                                    closePathTramp.ClosePath(blueSquareCharacter.playerStartRow, blueSquareCharacter.playerStartColumn , gameBoard);
                                    trampBoard[blueSquareCharacter.playerStartRow, blueSquareCharacter.playerStartColumn] = null;
                                    Console.WriteLine("Haz caido en un trampa");
                                    Console.ReadKey();
                                }
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