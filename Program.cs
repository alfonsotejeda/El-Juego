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
                Shell [,] gameBoard = board.CreateBoard();
                
                board.PrintBoard(gameBoard);
                //Define Tramps
                ClosePathTramp closePathTramp = new ClosePathTramp("🔳" , 7 , "C");
                
                int player1StartRow = 1;
                int player1StartColumn = 1;
                BlueSquareCharacter blueSquareCharacter = new BlueSquareCharacter("🟦" , "defense" , 5 , ref player1StartRow , ref player1StartColumn);
                
                int player2StartRow = 31;
                int player2StartColumn = 31;
                RedSquareCharacter redSquareCharacter = new RedSquareCharacter("🟥", "attack" , 5 , ref player2StartRow , ref player2StartColumn);
                
                gameBoard[blueSquareCharacter.playerStartRow, blueSquareCharacter.playerStartColumn].HasCharacter = true;
                gameBoard[blueSquareCharacter.playerStartRow, blueSquareCharacter.playerStartColumn].CharacterIcon = blueSquareCharacter.icon;
                gameBoard[blueSquareCharacter.playerStartRow, blueSquareCharacter.playerStartColumn].IsPath = false;

                gameBoard[redSquareCharacter.playerStartRow, redSquareCharacter.playerStartColumn].HasCharacter = true;
                gameBoard[redSquareCharacter.playerStartRow, redSquareCharacter.playerStartColumn].CharacterIcon = redSquareCharacter.icon;
                gameBoard[redSquareCharacter.playerStartRow, redSquareCharacter.playerStartColumn].IsPath = false;

                while(true)
                {
                    blueSquareCharacter.Move(ref blueSquareCharacter.playerStartRow, ref blueSquareCharacter.playerStartColumn , gameBoard , board);
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