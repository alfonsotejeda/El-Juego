using System.Dynamic;

namespace P_P
// Quiero creear un enum para mejor comprension de las opciones
{
    public class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.print_menu();
            System.Console.WriteLine();
            Console.Write("Elija una opción: ");
            string? userInput = Console.ReadLine();
            if (userInput != null && menu.choosen_opcion(userInput) == "1")
            {
                
                Board board = new Board(30 , 30);
                string [,] game_board = board.create_board();

            //Define characters
            int player1_start_row = 1;
            int player1_start_column = 1;
            BlueSquareCharacter blueSquareCharacter = new BlueSquareCharacter("🟦" , "defense" , 5 , ref player1_start_row , ref player1_start_column);

            int player2_start_row = 28;
            int player2_start_column = 28;
            RedSquareCharacter redSquareCharacter = new RedSquareCharacter("🟥", "atack" , 4 , ref player2_start_row , ref player2_start_column);

                game_board[blueSquareCharacter.player_start_row,blueSquareCharacter.player_start_column] = blueSquareCharacter.icon;
                // game_board[redSquareCharacter.player_start_row,redSquareCharacter.player_start_column] = redSquareCharacter.icon;
                while(true)
                {
                        board.print_board(game_board);
                        for (int i = 0 ; i < blueSquareCharacter.movement_capacity;i++)
                        {
                            Console.WriteLine($"{blueSquareCharacter.movement_capacity - i}: movements left");
                            blueSquareCharacter.Move(ref blueSquareCharacter.player_start_row,ref blueSquareCharacter.player_start_column,game_board,board);
                            board.print_board(game_board);

                        }
                        for (int i = 0 ; i < redSquareCharacter.movement_capacity;i++)
                        {
                            Console.WriteLine($"{redSquareCharacter.movement_capacity - i}: movements left");
                            redSquareCharacter.Move(ref redSquareCharacter.player_start_row,ref redSquareCharacter.player_start_column,game_board,board);
                            board.print_board(game_board);
                        }
                }
            }
            else if(userInput != null && menu.choosen_opcion(userInput) == "2"){
                System.Console.WriteLine("En contrucción");
            }
            else if(userInput != null && menu.choosen_opcion(userInput) == "3"){
                System.Console.WriteLine("En contrucción");
            }
        }
    }
}