using System.Dynamic;

namespace P_P
{
    /// <summary>
    /// Clase principal del programa que maneja el flujo del juego
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // Inicialización del menú y obtención de la opción del usuario
            Menu menu = new Menu();
            menu.print_menu();
            Console.Write("\nElija una opción: ");
            string? userInput = Console.ReadLine();

            // Si el usuario elige la opción 1 (Iniciar Programa)
            if (userInput != null && menu.choosen_opcion(userInput) == "1")
            {
                // Crear tablero de 30x30
                Board board = new Board(30, 30);
                string[,] game_board = board.create_board();

                // Configuración del jugador azul (posición inicial 1,1)
                int player1_start_row = 1;
                int player1_start_column = 1;
                BlueSquareCharacter blueSquareCharacter = new BlueSquareCharacter(
                    "🔷",
                    "defense",
                    5,
                    ref player1_start_row,
                    ref player1_start_column
                );

                // Configuración del jugador rojo (posición inicial 28,28)
                int player2_start_row = 28;
                int player2_start_column = 28;
                RedSquareCharacter redSquareCharacter = new RedSquareCharacter(
                    "🔶",
                    "atack",
                    4,
                    ref player2_start_row,
                    ref player2_start_column
                );

                // Colocar jugadores en el tablero
                game_board[blueSquareCharacter.player_start_row, blueSquareCharacter.player_start_column] = blueSquareCharacter.Icon;
                game_board[redSquareCharacter.player_start_row, redSquareCharacter.player_start_column] = redSquareCharacter.Icon;

                // Bucle principal del juego
                while (true)
                {
                    // Turno del jugador azul - 5 movimientos
                    for (int i = 0; i < blueSquareCharacter.movement_capacity; i++)
                    {
                        board.ShowTurnMessage("AZUL", "B", blueSquareCharacter.movement_capacity - i);
                        board.print_board(game_board);
                        blueSquareCharacter.Move(
                            ref blueSquareCharacter.player_start_row,
                            ref blueSquareCharacter.player_start_column,
                            game_board,
                            board
                        );
                    }

                    // Turno del jugador rojo - 4 movimientos
                    for (int i = 0; i < redSquareCharacter.movement_capacity; i++)
                    {
                        board.ShowTurnMessage("ROJO", "R", redSquareCharacter.movement_capacity - i);
                        board.print_board(game_board);
                        redSquareCharacter.Move(
                            ref redSquareCharacter.player_start_row,
                            ref redSquareCharacter.player_start_column,
                            game_board,
                            board
                        );
                    }
                }
            }
            // Opciones 2 y 3 en construcción
            else if (userInput != null && menu.choosen_opcion(userInput) == "2")
            {
                Console.WriteLine("En construcción");
            }
            else if (userInput != null && menu.choosen_opcion(userInput) == "3")
            {
                Console.WriteLine("En construcción");
            }
        }
    }
}