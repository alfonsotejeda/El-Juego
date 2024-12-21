using Spectre.Console;

namespace P_P.menu
{
    public class Menu
    {
        private const string TITLE = "BIENVENIDO AL JUEGO";
        private bool isSpanish = true;
        
        public Menu()
        {
        }

        public string ShowMenu()
        {
            AnsiConsole.Clear();
            
            // Create a fancy header
            var rule = new Rule($"[bold yellow]{(isSpanish ? TITLE : "WELCOME TO THE GAME")}[/]");
            rule.Style = Style.Parse("yellow");
            
            AnsiConsole.Write(rule);
            AnsiConsole.WriteLine();

            // Create the menu
            var choices = isSpanish
                ? new[] { " Iniciar Juego", " Cambiar Idioma", " Salir" }
                : new[] { " Start Game", " Change Language", " Exit" };

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(isSpanish ? "[blue]¿Qué te gustaría hacer?[/]" : "[blue]What would you like to do?[/]")
                    .PageSize(10)
                    .HighlightStyle(new Style(foreground: Color.Cyan1))
                    .AddChoices(choices));

            // Convert selection back to number
            string option = selection switch
            {
                " Iniciar Juego" => "1",
                " Cambiar Idioma" => "2",
                " Salir" => "3",
                " Start Game" => "1",
                " Change Language" => "2",
                " Exit" => "3",
                _ => "1"
            };

            ProcessOption(option);
            return option;
        }

        private void ProcessOption(string option)
        {
            switch (option)
            {
                case "2":
                    ToggleLanguage();
                    break;
                case "3":
                    if (ConfirmExit())
                        Environment.Exit(0);
                    break;
            }
        }

        private void ToggleLanguage()
        {
            isSpanish = !isSpanish;
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine(isSpanish 
                ? "[green]¡Idioma cambiado a Español![/]" 
                : "[green]Language changed to English![/]");
            Thread.Sleep(1500); // Show message briefly
        }

        private bool ConfirmExit()
        {
            return AnsiConsole.Confirm(
                isSpanish 
                    ? "¿Estás seguro que deseas salir?" 
                    : "Are you sure you want to exit?");
        }
    }
}