# Informe del Juego de Laberinto

## 1. Cómo Ejecutar el Proyecto

### Requisitos Previos
- .NET Core/Framework instalado
- Visual Studio o Visual Studio Code
- Paquete Spectre.Console instalado

### Pasos para Ejecutar
1. Clonar el repositorio
2. Abrir la solución en Visual Studio/VS Code
3. Restaurar los paquetes NuGet
4. Compilar el proyecto
5. Ejecutar el programa

## 2. Cómo Jugar

### Inicio del Juego
1. Ejecutar el programa
2. Seleccionar la opción 1 del menú principal
3. Introducir el número de jugadores (1-4)
4. Cada jugador selecciona su personaje

### Controles
- **W**: Mover arriba
- **S**: Mover abajo
- **A**: Mover izquierda
- **D**: Mover derecha
- **H**: Usar habilidad especial
- **C**: Cambiar de personaje (requiere más de un jugador)
- **ESC**: Volver al menú principal

### Personajes Disponibles
1. 🟦 Cuadrado Azul (Defensa)
   - Habilidad: Aumenta su vida
   - Movimiento: 8 casillas
   - Visibilidad: 5 casillas

2. 🟥 Cuadrado Rojo (Ataque)
   - Habilidad: Ataca a otros jugadores (-10 vida)
   - Movimiento: 9 casillas
   - Visibilidad: 3 casillas

3. 🟩 Cuadrado Verde (Trampas)
   - Habilidad: Elimina trampas aleatorias
   - Movimiento: 7 casillas
   - Visibilidad: 4 casillas

4. 🟨 Cuadrado Amarillo (Salto)
   - Habilidad: Salta sobre paredes
   - Movimiento: 10 casillas
   - Visibilidad: 3 casillas

5. 🟪 Cuadrado Violeta (Movimiento)
   - Habilidad: Aumenta su capacidad de movimiento
   - Movimiento: 9 casillas
   - Visibilidad: 3 casillas

6. 🟧 Cuadrado Naranja (Laberinto)
   - Habilidad: Cambia el laberinto de otros jugadores
   - Movimiento: 8 casillas
   - Visibilidad: 6 casillas

### Sistema de Vida y Respawn
- Cada personaje comienza con 100 de vida
- Al perder toda la vida, el personaje vuelve a su posición inicial
- La vida se restaura al respawnear

## 3. Detalles de Implementación

### Estructura del Proyecto
- **Program.cs**: Punto de entrada y lógica principal
- **board/**: Generación y manejo del laberinto
- **characters/**: Clases de personajes y habilidades
- **tramps/**: Sistema de trampas
- **PrintingMethods/**: Métodos de visualización

### Características Principales

#### Sistema de Jugadores
- Soporte para 1-4 jugadores
- Posiciones iniciales únicas para cada jugador
- Sistema de turnos por jugador

#### Laberinto
- Generación procedural
- División en cuatro cuadrantes
- Paredes y caminos dinámicos

#### Sistema de Habilidades
- Habilidades únicas por personaje
- Cooldown específico por habilidad
- Restricciones basadas en el número de jugadores

#### Sistema de Trampas
- Trampas aleatorias en cada cuadrante
- Diferentes tipos de efectos
  - Volver al origen
  - Reducir vida
  - Bloquear camino

#### Interfaz de Usuario
- UI basada en Spectre.Console
- Paneles informativos
- Menús interactivos
- Visualización del estado del juego

### Mecánicas de Juego
1. **Movimiento**: Sistema basado en turnos con capacidad limitada
2. **Combate**: Sistema de vida y daño entre jugadores
3. **Exploración**: Visibilidad limitada por personaje
4. **Interacción**: Habilidades y efectos entre jugadores

### Sistema de Victoria
- Llegar al centro del laberinto
- Sobrevivir a las trampas y otros jugadores 