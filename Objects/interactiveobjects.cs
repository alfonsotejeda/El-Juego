using P_P.board;
using P_P.characters;
using P_P.PrintingMethods;
using P_P.tramps;

namespace P_P
{
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

        public virtual void Interact(Shell[,] gameboard, BaseCharacter character , List<BaseCharacter> characters , List<BaseTramp> tramps)
        {
            PrintingMethods.PrintingMethods printingMethods = new PrintingMethods.PrintingMethods();
        }

        public void CleanPosition(Shell[,] gameboard, BaseCharacter character)
        {
            gameboard[character.PlayerRow, character.PlayerColumn].HasCharacter = false;
            gameboard[character.PlayerRow, character.PlayerColumn].CharacterIcon = null;
            gameboard[character.PlayerRow, character.PlayerColumn].HasObject = false;
            gameboard[character.PlayerRow, character.PlayerColumn].ObjectId = null;
            gameboard[character.PlayerRow, character.PlayerColumn].ObjectType = null;
        }

        public void CleanObjectPosition(Shell[,] gameboard, BaseCharacter character)
        {
            gameboard[character.PlayerRow, character.PlayerColumn].HasObject = false;
            gameboard[character.PlayerRow, character.PlayerColumn].ObjectId = null;
            gameboard[character.PlayerRow, character.PlayerColumn].ObjectType = null;
        }
    }
}
