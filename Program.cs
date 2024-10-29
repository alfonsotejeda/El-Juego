namespace P_P
{
    public class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Menu menu = new Menu();
                menu.print_menu();
                System.Console.WriteLine();
                Console.Write("Elija una opción: ");
                
                if (menu.choosen_opcion(Console.ReadLine()) == "1")
                {
                    Board board = new Board(10 , 10);
                    string [,] game_board = board.create_board();
                    board.print_board(game_board);
                    BlueSquareCharacter blueSquareCharacter = new BlueSquareCharacter("🟦" , "fly" , 5);
                    blueSquareCharacter.Move(4 , 3 , game_board , blueSquareCharacter.icon);
                    board.print_board(game_board);
                }
                else if(menu.choosen_opcion(Console.ReadLine()) == "2"){System.Console.WriteLine("En contrucción");}
                else if(menu.choosen_opcion(Console.ReadLine()) == "3"){System.Console.WriteLine("En contrucción");}
            }
        }
    }
}