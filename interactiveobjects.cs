
using P_P.board;
using P_P.characters;

namespace P_P;

    public class InteractiveObjects
    {
        public string ObjectType { get; set; }


        public InteractiveObjects(string objectType)
        {
            ObjectType = objectType;
        }
        
        public virtual bool CheckInteraction(BaseCharacter baseCharacter, Shell[,] gameBoard)
        {
            return gameBoard[baseCharacter.PlayerRow, baseCharacter.PlayerColumn].HasObject;
        }

        public virtual void Interact(Shell[,] gameboard, BaseCharacter character)
        {
        }
}
