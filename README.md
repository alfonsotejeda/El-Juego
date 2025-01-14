# 🎮 Laberinto Multijugador

Un juego de laberinto en consola desarrollado en C# que presenta generación dinámica de laberintos, múltiples personajes y mecánicas interactivas, utilizando la biblioteca Spectre.Console para una presentación visual mejorada.

## 📋 Características Principales

### 🎯 Jugabilidad
- **Multijugador**: Soporte para 1-4 jugadores
- **Generación Dinámica**: Laberinto único dividido en cuatro cuadrantes
- **Sistema de Turnos**: Movimiento estratégico por turnos
- **Interfaz Moderna**: UI mejorada con Spectre.Console

### 👾 Personajes
- **🟦 Cuadrado Azul (Defensa)**
  - Habilidad: Aumenta la vida en 10 puntos
  - Movimientos por turno: 3
  - Visibilidad: 5 casillas

- **🟥 Cuadrado Rojo (Ataque)**
  - Habilidad: Ataca a otros jugadores
  - Movimientos por turno: 5
  - Visibilidad: 3 casillas

- **🟩 Cuadrado Verde (Velocidad)**
  - Habilidad: Elimina trampas aleatorias
  - Movimientos por turno: 3
  - Visibilidad: 4 casillas

- **🟨 Cuadrado Amarillo (Salto)**
  - Habilidad: Salta sobre paredes
  - Movimientos por turno: 4
  - Visibilidad: 3 casillas

- **🟪 Cuadrado Violeta (Mejora)**
  - Habilidad: Aumenta capacidad de movimiento
  - Movimientos por turno: 3
  - Visibilidad: 3 casillas

- **🟧 Cuadrado Naranja (Cambio)**
  - Habilidad: Modifica el laberinto
  - Movimientos por turno: 3
  - Visibilidad: 6 casillas

### 🎲 Trampas y Objetos
- **Trampa de Origen**: Teletransporta al jugador al inicio
- **Trampa de Vida**: Reduce la vida del jugador
- **Trampa de Camino**: Modifica la estructura del laberinto

## 🛠️ Requisitos Técnicos

- .NET 8.0
- Spectre.Console (v0.49.1)
- Spectre.Console.Cli (v0.49.1)

## 🎮 Controles

- **W,A,S,D**: Movimiento
- **H**: Activar habilidad especial
- **C**: Cambiar de personaje
- **ESC**: Volver al menú principal

## 🏗️ Estructura del Proyecto

P_P/
├── board/
│ ├── board.cs # Lógica del tablero
│ ├── shell.cs # Clase base para celdas
│ ├── path.cs # Caminos transitables
│ └── wall.cs # Paredes del laberinto
├── characters/
│ ├── base_character_class.cs
│ ├── bluesquare_character.cs
│ ├── redsquare_character.cs
│ └── [otros personajes]
├── Objects/
│ ├── tramps/ # Sistema de trampas
│ └── interactiveobjects.cs
└── Program.cs # Punto de entrada

## 🚀 Instalación y Ejecución

1. Clonar el repositorio
2. Asegurarse de tener .NET 8.0 instalado
3. Ejecutar en terminal:

## 🎯 Próximas Mejoras

1. Sistema de puntuación
2. Límites de tiempo
3. Nuevos tipos de trampas
4. Mejoras en la UI
5. Guardado de partidas

## ⚠️ Problemas Conocidos

1. Opciones 2 y 3 del menú en construcción
2. Condiciones de victoria necesitan refinamiento
3. Colisiones entre personajes pueden mejorarse

## 🤝 Contribuciones

Las contribuciones son bienvenidas. Por favor:

1. Haz fork del proyecto
2. Crea una rama para tu feature
3. Envía un pull request

## 📝 Licencia

[Añadir información de licencia]

## 👥 Autores

[Añadir información de autores]
