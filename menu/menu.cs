namespace P_P
{
    public class Menu
    {
        private const string BORDER = "+===========================================+";
        private const string EMPTY_LINE = "|                                           |";
        
        public Menu()
        {
        }

        public void printMenu()
        {
            Console.Clear(); // Limpia la pantalla antes de mostrar el menú
            Console.ForegroundColor = ConsoleColor.Cyan; // Color para mejor visualización
            
            Console.WriteLine(BORDER);
            Console.WriteLine("|           🎮 BIENVENIDO AL JUEGO 🎮          |");
            Console.WriteLine(BORDER);
            Console.WriteLine(EMPTY_LINE);
            Console.WriteLine("|  [1] ▶ 🎯 Iniciar Juego                      |");
            Console.WriteLine("|  [2] ▶ 🌍 Cambiar Idioma                     |");
            Console.WriteLine("|  [3] ▶ 🚪 Salir                              |");
            Console.WriteLine(EMPTY_LINE);
            Console.WriteLine(BORDER);
            Console.WriteLine("\n🎲 Por favor, seleccione una opción (1-3): ");
            
            Console.ResetColor(); // Restaura el color original
        }

        public string choosenOpcion(string opcion)
        {
            // Validación básica de la entrada
            while (!new[] {"1", "2", "3"}.Contains(opcion))
            {
                Console.WriteLine("Opción no válida. Por favor, intente nuevamente (1-3): ");
                opcion = Console.ReadLine();
            }
            return opcion;
        }
    }
}