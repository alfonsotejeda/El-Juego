using P_P.characters;
using P_P.board;
namespace P_P.tramps
{
    class GoToOriginTramp : BaseTramp
    {
        public GoToOriginTramp(int numberOfTraps , string? trampId) : base( numberOfTraps , trampId)
        {
        }
        public override void Interact(Shell[,] gameboard, BaseCharacter character)
        {
            character.PlayerRow = 1;
            character.PlayerColumn = 1;
        }
    }
    
}