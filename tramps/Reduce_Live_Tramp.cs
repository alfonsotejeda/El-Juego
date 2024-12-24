using P_P.characters;
using P_P.board;
namespace P_P.tramps
{
    class ReduceLiveTramp : BaseTramp
    {
        public ReduceLiveTramp(int numberOfTraps , string? trampId) : base( numberOfTraps , trampId)
        {
        }

        public override void Interact(Shell[,] gameboard, BaseCharacter character)
        {
            Random random = new Random();
            int liveToReduce = random.Next(1, 50);
            character.Live -= liveToReduce;

            if (liveToReduce > 40)
            {
                Console.WriteLine("Critical tramp!");
            }
        }
        
    }
    
}