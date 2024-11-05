namespace P_P
{
    /// <summary>
    /// Representa al personaje del jugador azul
    /// Hereda de BaseCharacter y se especializa en defensa
    /// </summary>
    public class BlueSquareCharacter : BaseCharacter
    {
        /// <summary>
        /// Constructor del personaje azul
        /// </summary>
        public BlueSquareCharacter(string icon, string ability, int await_time, 
            ref int player_start_row, ref int player_start_column) 
            : base("🔷", ability, await_time, player_start_row, player_start_column)
        {
            // No necesitamos sobrescribir el icono aquí
        }
    }
}