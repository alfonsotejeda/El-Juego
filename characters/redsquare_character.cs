using P_P.board;
using P_P.tramps;

namespace P_P.characters
{
    public class RedSquareCharacter : BaseCharacter
    {
        public new string icon;
        public RedSquareCharacter(string icon, string ability, ref int movementCapacity, ref int playerRow, ref int playerColumn)
            : base(icon, ability, movementCapacity, playerColumn, playerRow)
        {
            this.icon = icon ?? throw new ArgumentNullException(nameof(icon));
        }

        public override void UseAbility(Shell[,] gameboard, BaseCharacter character , List<BaseTramp> tramps,List<BaseCharacter> characters)
        {
            Console.WriteLine("Introduce el personaje que quieres Atacar");
            DisplayCharactersToChange(characters , character);
            int characterToAttack = int.Parse(Console.ReadLine());
            characters[characterToAttack].Live -= 10;
        }
    }
} 