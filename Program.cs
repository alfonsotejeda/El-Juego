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
            if (menu.choosen_opcion(Console.ReadLine()) == "1")
            {
                Board board = new Board(20 , 20);
                string [,] game_board = board.create_board();
                while(true)
                {
                        board.print_board(game_board);

                        // Define de movment
                        Console.Write("Elija una posición en x: ");
                        int move_x = int.Parse(Console.ReadLine()) - 1;
                        Console.Write("Elija una posición en y: ");
                        int move_y = int.Parse(Console.ReadLine()) - 1;
                        BlueSquareCharacter blueSquareCharacter = new BlueSquareCharacter("🟦" , "fly" , 5);
                        blueSquareCharacter.Move(move_x , move_y , game_board , blueSquareCharacter.icon);
                        board.print_board(game_board);
                        
                }
            }
            else if(menu.choosen_opcion(Console.ReadLine()) == "2"){System.Console.WriteLine("En contrucción");}
            else if(menu.choosen_opcion(Console.ReadLine()) == "3"){System.Console.WriteLine("En contrucción");}
        }
    }
}