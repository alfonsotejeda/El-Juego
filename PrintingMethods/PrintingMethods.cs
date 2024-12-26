using P_P.board;
using Spectre.Console;
using P_P.characters;
using P_P.tramps;

namespace P_P.PrintingMethods;

public class PrintingMethods
{
    public string consoleMessages = "";
    public Layout layout;

    public PrintingMethods()
    {
        layout = new Layout("Root")
            .SplitColumns(
                new Layout("Left"),
                new Layout("Right")
                    .SplitRows(
                        new Layout("Top"),
                        new Layout("Bottom")));
        layout["Left"].Size(133);
        layout["Top"].Size(10);
        layout["Bottom"].Size(10);
    }
    public void PrintGameSpectre(Shell[,] gameBoard , BaseCharacter baseCharacter , List<BaseCharacter> characters , List<BaseTramp> tramps )
    {
        AnsiConsole.Clear();
        // Definir el tamaño del lienzo
        int canvasWidth = gameBoard.GetLength(1)*2;
        int canvasHeight = gameBoard.GetLength(0)*2;
        Canvas canvas = new Canvas(canvasWidth, canvasHeight);

        // Imprimir cada celda como un "píxel" en el lienzo
        for (int i = 0; i < canvasHeight/2; i++)
        {
            for (int j = 0; j < canvasWidth/2; j++)
            {
                if (gameBoard[i, j].GetType() == typeof(wall))
                {
                    PrintPixel(canvas , i , j , Color.Black);
                }
                else if (gameBoard[i, j].GetType() == typeof(path))
                {
                    PrintPixel(canvas , i , j , Color.White);
                }
                if (gameBoard[i, j].HasObject)
                {
                    switch (gameBoard[i, j].ObjectType)
                    {
                        case "tramp":
                            PrintPixel(canvas , i , j , Color.DarkRed);
                            break;
                    }
                }
                if (gameBoard[i, j].HasCharacter)
                {
                    switch (gameBoard[i, j].CharacterIcon)
                    {
                        case "🟦":
                            PrintMulticolorPixel(canvas , i , j , Color.Blue , Color.CadetBlue);
                            break;
                        case "🟥":
                            PrintMulticolorPixel(canvas , i , j , Color.Red, Color.MediumVioletRed);
                            break;
                        case "🟩":
                            PrintMulticolorPixel(canvas , i , j , Color.Green , Color.Green3);
                            break;
                        case "🟨":
                            PrintMulticolorPixel(canvas , i , j , Color.Yellow , Color.Gold1);
                            break;
                    }
                }
            }
        }
        layout["Left"].Update(canvas);
        layout["Top"].Update(new BarChart()
            .Width(60)
            .Label(baseCharacter.Icon)
            .CenterLabel()
            .AddItem("PlayerLive", baseCharacter.Live, Color.Green)
            .AddItem("PlayerMovements", baseCharacter.MovementCapacity, Color.Blue)
            .AddItem("PlayerCountDonw", 4, Color.Red));
       
        AnsiConsole.Write(layout);
            
        }
        private void PrintPixel(Canvas canvas , int i , int j ,Color color)
        {
            canvas.SetPixel(j * 2, i * 2, color);
            canvas.SetPixel(j * 2 + 1, i * 2, color);
            canvas.SetPixel(j * 2, i * 2 + 1, color);
            canvas.SetPixel(j * 2 + 1, i * 2 + 1, color );
        }
        private void PrintMulticolorPixel(Canvas canvas , int i , int j , Color color1 , Color color2)
        {
            canvas.SetPixel(j * 2, i * 2, color1);
            canvas.SetPixel(j * 2 + 1, i * 2, color2);
            canvas.SetPixel(j * 2, i * 2 + 1, color2);
            canvas.SetPixel(j * 2 + 1, i * 2 + 1, color1);
        }
}