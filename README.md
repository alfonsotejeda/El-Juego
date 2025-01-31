# Maze Game Project

A C# console-based maze game featuring a dynamic maze generator, multiple characters, and interactive gameplay using the Spectre.Console library for enhanced visual presentation.

## 🎮 Game Overview

This is a two-player maze game where players navigate through a procedurally generated maze. The game features:
- A Blue Square Character (Defense)
- A Red Square Character (Attack)
- A Green Square Character (Speed)
- Dynamically generated maze divided into four quadrants
- Special traps and interactive elements
- Modern console-based UI using Spectre.Console

## 🏗️ Project Structure

- `Program.cs`: Main entry point and game loop
- `board/board.cs`: Board generation and display logic
- Additional character and trap classes

## 🚀 Features

- **Dynamic Maze Generation**: Uses a maze generator to create unique layouts in four quadrants
- **Character System**: Three distinct characters with different properties
  - Blue Square (Defense): Starting position (1,1)
  - Red Square (Attack): Starting position (31,31)
  - Green Square (Speed): Starting position (15,15)
- **Interactive UI**: Uses Spectre.Console for enhanced visual presentation
- **Menu System**: Main menu with multiple options (some under construction)
- **Trap System**: Includes close path traps and other interactive elements

## 💡 Suggestions for Implementation

1. **Game Mechanics Enhancement**:
   - Implement scoring system
   - Add time limits for maze completion
   - Create power-ups and special abilities for characters

2. **UI Improvements**:
   - Add color-coded status indicators
   - Implement a more detailed HUD
   - Add animation effects for movement and actions

3. **Gameplay Features**:
   - Add multiplayer support
   - Implement different difficulty levels
   - Create various maze generation algorithms

4. **Code Structure**:
   - Implement dependency injection
   - Add unit tests for core functionality
   - Create separate configuration files

5. **Additional Features**:
   - Save/Load game state
   - High score system
   - Different game modes
   - Sound effects and background music

## 🛠️ Technical Requirements

- .NET Core/Framework
- Spectre.Console package
- C# 7.0 or higher

## 🎯 Future Improvements

1. Complete the construction of menu options 2 and 3
2. Implement proper exception handling throughout the game
3. Add player statistics and progression system
4. Enhance character movement and interaction mechanics
5. Add more trap types and obstacles

## 🎲 How to Play

1. Run the program
2. Select option 1 from the main menu to start the game
3. Use movement controls to navigate through the maze
4. Press ESC to return to the main menu

## 🔍 Code Architecture

The project follows a modular design with separate classes for:
- Board management (`Board` class)
- Character control (`BlueSquareCharacter`, `RedSquareCharacter`, and `GreenSquareCharacter` classes)
- Trap system (`ClosePathTramp` class)
- Menu handling (`Menu` class)

## ⚠️ Known Issues

1. Menu options 2 and 3 are currently under construction
2. Need to implement proper game ending conditions
3. Character collision handling could be improved

## 🤝 Contributing

Feel free to contribute to this project by:
1. Implementing suggested features
2. Fixing known issues
3. Improving code documentation
4. Adding new game mechanics

## 📝 License

[Add your license information here]
