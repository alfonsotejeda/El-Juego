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
                if (gameBoard[i, j].GetType() == typeof(Wall))
                {
                    PrintPixel(canvas , i , j , Color.Black);
                }
                else if (gameBoard[i, j].GetType() == typeof(P_P.board.Path))
                {
                    PrintPixel(canvas , i , j , Color.White);
                }
                if(gameBoard[i, j].IsCenter)
                {
                    PrintPixel(canvas , i , j , Color.Violet);
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
                            PrintCharacterrVisibility(gameBoard , baseCharacter , characters , tramps , baseCharacter.Visibility , canvas);
                            break;
                        case "🟥":
                            PrintMulticolorPixel(canvas , i , j , Color.Red, Color.MediumVioletRed);
                            PrintCharacterrVisibility(gameBoard , baseCharacter , characters , tramps , baseCharacter.Visibility , canvas);
                            break;
                        case "🟩":
                            PrintMulticolorPixel(canvas , i , j , Color.Green , Color.Green3);
                            PrintCharacterrVisibility(gameBoard , baseCharacter , characters , tramps , baseCharacter.Visibility , canvas);
                            break;
                        case "🟨":
                            PrintMulticolorPixel(canvas , i , j , Color.Yellow , Color.Gold1);
                            PrintCharacterrVisibility(gameBoard , baseCharacter , characters , tramps , baseCharacter.Visibility , canvas);
                            break;
                        case "🟪":
                            PrintMulticolorPixel(canvas , i , j , Color.Purple , Color.MediumPurple);
                            PrintCharacterrVisibility(gameBoard , baseCharacter , characters , tramps , baseCharacter.Visibility , canvas);
                            break;
                        case "🟧":
                            PrintMulticolorPixel(canvas, i, j, Color.Orange1, Color.DarkOrange);
                            PrintCharacterrVisibility(gameBoard , baseCharacter , characters , tramps , baseCharacter.Visibility , canvas);
                            break;
                    }
                }
            }
            foreach (BaseCharacter character in characters)
            {
                PrintCharacterrVisibility(gameBoard , character , characters , tramps , character.Visibility , canvas);
            }
        }

        layout["Left"].Update(canvas);
        layout["Top"].Update(new BarChart()
            .Width(60)
            .Label(baseCharacter.Icon)
            .CenterLabel()
            .AddItem("PlayerLive", baseCharacter.Live, Color.Green)
            .AddItem("PlayerMovements", baseCharacter.MovementCapacity, Color.Blue)
            .AddItem("PlayerCountDonw", baseCharacter.Countdown, Color.Red));

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

        public void PrintVictoryMesagge(BaseCharacter character)
        {
            Console.Clear();
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("Victoria!")
                    .Centered()
                    .Color(Color.Green));

            AnsiConsole.Write(
                new FigletText(character.Icon)
                    .Centered()
                    .Color(Color.Gold1));

            AnsiConsole.Write(
                    new Markup($"[bold green]Felicidades {character.Icon}, has ganado el juego![/]")
                        .Centered());
            
        }
        public void PrintCharacterrVisibility(Shell[,] gameBoard , BaseCharacter character , List<BaseCharacter> characters , List<BaseTramp> tramps , int characterrVisibility , Canvas canvas)
        {
            if(character.IsInFirstQuadrant(character , gameBoard)) PrintFristCuadrant(gameBoard , character , characters , tramps , characterrVisibility , canvas);
            if(character.IsInSecondQuadrant(character , gameBoard)) PrintSecondQuadrant(gameBoard , character , characters , tramps , characterrVisibility , canvas);
            if(character.IsInThirdQuadrant(character , gameBoard)) PrintThirdQuadrant(gameBoard , character , characters , tramps , characterrVisibility , canvas);
            if(character.IsInFourthQuadrant(character , gameBoard)) PrintFourthQuadrant(gameBoard , character , characters , tramps , characterrVisibility , canvas);

        }
        private void PrintFristCuadrant(Shell[,] gameBoard , BaseCharacter character , List<BaseCharacter> characters , List<BaseTramp> tramps , int characterrVisibility , Canvas canvas)
        {
            for(int row = 1 ; row < gameBoard.GetLength(0)/2 ; row ++ ){
                for(int column = 1; column < gameBoard.GetLength(1)/2 ; column ++)
                {
                    if(Math.Abs(row - character.PlayerRow) + Math.Abs(column - character.PlayerColumn) <= characterrVisibility)
                    {
                        if (gameBoard[row, column].GetType() == typeof(Wall))
                        {
                            PrintPixel(canvas , row , column , Color.Black);
                        }
                        else if (gameBoard[row, column].GetType() == typeof(P_P.board.Path))
                        {
                            PrintPixel(canvas , row , column , Color.White);
                        }
                        if(gameBoard[row, column].IsCenter)
                        {
                            PrintPixel(canvas , row , column , Color.Violet);
                        }
                        if (gameBoard[row, column].HasObject)
                        {
                            switch (gameBoard[row, column].ObjectType)
                            {
                                case "tramp":
                                    PrintPixel(canvas , row , column , Color.DarkRed);
                                    break;
                            }
                        }
                        if (gameBoard[row, column].HasCharacter)
                        {
                            switch (gameBoard[row, column].CharacterIcon)
                            {
                                case "🟦":
                                    PrintMulticolorPixel(canvas , row , column , Color.Blue , Color.CadetBlue);
                                    break;
                                case "🟥":
                                    PrintMulticolorPixel(canvas , row , column , Color.Red, Color.MediumVioletRed);
                                    break;
                                case "🟩":
                                    PrintMulticolorPixel(canvas , row , column , Color.Green , Color.Green3);
                                    break;
                                case "🟨":
                                    PrintMulticolorPixel(canvas , row , column , Color.Yellow , Color.Gold1);
                                    break;
                                case "🟪":
                                    PrintMulticolorPixel(canvas , row , column , Color.Purple , Color.MediumPurple);
                                    break;
                                case "🟧":
                                    PrintMulticolorPixel(canvas, row, column, Color.Orange1, Color.DarkOrange);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        PrintPixel(canvas , row , column , Color.Grey);
                    }
                }
            }
        }
        private void PrintSecondQuadrant(Shell[,] gameBoard , BaseCharacter character , List<BaseCharacter> characters , List<BaseTramp> tramps , int characterrVisibility , Canvas canvas)
        {
            for(int row = 1 ; row < gameBoard.GetLength(0)/2 ; row ++ ){
                for(int column = gameBoard.GetLength(1)/2+1 ; column < gameBoard.GetLength(1)-1 ; column ++)
                {
                    if(Math.Abs(row - character.PlayerRow) + Math.Abs(column - character.PlayerColumn) <= characterrVisibility)
                    {
                        if (gameBoard[row, column].GetType() == typeof(Wall))
                        {

                            PrintPixel(canvas , row , column , Color.Black);
                        }
                        else if (gameBoard[row, column].GetType() == typeof(P_P.board.Path))
                        {
                            PrintPixel(canvas , row , column , Color.White);
                        }
                        if(gameBoard[row, column].IsCenter)
                        {
                            PrintPixel(canvas , row , column , Color.Violet);
                        }
                        if (gameBoard[row, column].HasObject)
                        {
                            switch (gameBoard[row, column].ObjectType)
                            {
                                case "tramp":
                                    PrintPixel(canvas , row , column , Color.DarkRed);
                                    break;
                            }
                        }
                        if (gameBoard[row, column].HasCharacter)
                        {
                            switch (gameBoard[row, column].CharacterIcon)
                            {
                                case "🟦":
                                    PrintMulticolorPixel(canvas , row , column , Color.Blue , Color.CadetBlue);
                                    break;
                                case "🟥":
                                    PrintMulticolorPixel(canvas , row , column , Color.Red, Color.MediumVioletRed);
                                    break;
                                case "🟩":
                                    PrintMulticolorPixel(canvas , row , column , Color.Green , Color.Green3);
                                    break;
                                case "🟨":
                                    PrintMulticolorPixel(canvas , row , column , Color.Yellow , Color.Gold1);
                                    break;
                                case "🟪":
                                    PrintMulticolorPixel(canvas , row , column , Color.Purple , Color.MediumPurple);
                                    break;
                                case "🟧":
                                    PrintMulticolorPixel(canvas, row, column, Color.Orange1, Color.DarkOrange);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        PrintPixel(canvas , row , column , Color.Grey);
                    }
                }
            }
        }
        private void PrintThirdQuadrant(Shell[,] gameBoard , BaseCharacter character , List<BaseCharacter> characters , List<BaseTramp> tramps , int characterrVisibility , Canvas canvas)
        {
            for(int row = gameBoard.GetLength(0)/2+1 ; row < gameBoard.GetLength(0)-1 ; row ++ ){
                for(int column = 1 ; column < gameBoard.GetLength(1)/2 ; column ++)
                {
                    if(Math.Abs(row - character.PlayerRow) + Math.Abs(column - character.PlayerColumn) <= characterrVisibility)
                    {
                        if (gameBoard[row, column].GetType() == typeof(Wall))
                        {
                            PrintPixel(canvas , row , column , Color.Black);
                        }
                        else if (gameBoard[row, column].GetType() == typeof(P_P.board.Path))
                        {
                            PrintPixel(canvas , row , column , Color.White);
                        }
                        if(gameBoard[row, column].IsCenter)
                        {
                            PrintPixel(canvas , row , column , Color.Violet);
                        }
                        if (gameBoard[row, column].HasObject)
                        {
                            switch (gameBoard[row, column].ObjectType)
                            {
                                case "tramp":
                                    PrintPixel(canvas , row , column , Color.DarkRed);
                                    break;
                            }
                        }
                        if (gameBoard[row, column].HasCharacter)
                        {
                            switch (gameBoard[row, column].CharacterIcon)
                            {
                                case "🟦":
                                    PrintMulticolorPixel(canvas , row , column , Color.Blue , Color.CadetBlue);
                                    break;
                                case "🟥":
                                    PrintMulticolorPixel(canvas , row , column , Color.Red, Color.MediumVioletRed);
                                    break;
                                case "🟩":
                                    PrintMulticolorPixel(canvas , row , column , Color.Green , Color.Green3);
                                    break;
                                case "🟨":
                                    PrintMulticolorPixel(canvas , row , column , Color.Yellow , Color.Gold1);
                                    break;
                                case "🟪":
                                    PrintMulticolorPixel(canvas , row , column , Color.Purple , Color.MediumPurple);
                                    break;
                                case "🟧":
                                    PrintMulticolorPixel(canvas, row, column, Color.Orange1, Color.DarkOrange);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        PrintPixel(canvas , row , column , Color.Grey);
                    }
                }
            }
        }
        private void PrintFourthQuadrant(Shell[,] gameBoard , BaseCharacter character  , List<BaseCharacter> characters , List<BaseTramp> tramps , int characterrVisibility , Canvas canvas)
        {
            for(int row = gameBoard.GetLength(0)/2 + 1 ; row < gameBoard.GetLength(0)-1 ; row ++ ){
                for(int column = gameBoard.GetLength(1)/2 +1  ; column < gameBoard.GetLength(1)-1 ; column ++)
                {
                    if(Math.Abs(row - character.PlayerRow) + Math.Abs(column - character.PlayerColumn) <= characterrVisibility)
                    {
                        if (gameBoard[row, column].GetType() == typeof(Wall))
                        {
                            PrintPixel(canvas , row , column , Color.Black);
                        }
                        else if (gameBoard[row, column].GetType() == typeof(P_P.board.Path))
                        {
                            PrintPixel(canvas , row , column , Color.White);
                        }
                        if(gameBoard[row, column].IsCenter)
                        {
                            PrintPixel(canvas , row , column , Color.Violet);
                        }
                        if (gameBoard[row, column].HasObject)
                        {
                            switch (gameBoard[row, column].ObjectType)
                            {
                                case "tramp":
                                    PrintPixel(canvas , row , column , Color.DarkRed);
                                    break;
                            }
                        }
                        if (gameBoard[row, column].HasCharacter)
                        {
                            switch (gameBoard[row, column].CharacterIcon)
                            {
                                case "🟦":
                                    PrintMulticolorPixel(canvas , row , column , Color.Blue , Color.CadetBlue);
                                    break;
                                case "🟥":
                                    PrintMulticolorPixel(canvas , row , column , Color.Red, Color.MediumVioletRed);
                                    break;
                                case "🟩":
                                    PrintMulticolorPixel(canvas , row , column , Color.Green , Color.Green3);
                                    break;
                                case "🟨":
                                    PrintMulticolorPixel(canvas , row , column , Color.Yellow , Color.Gold1);
                                    break;
                                case "🟪":
                                    PrintMulticolorPixel(canvas , row , column , Color.Purple , Color.MediumPurple);
                                    break;
                                case "🟧":
                                    PrintMulticolorPixel(canvas, row, column, Color.Orange1, Color.DarkOrange);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        PrintPixel(canvas , row , column , Color.Grey);
                    }
                }
            }
        }

}