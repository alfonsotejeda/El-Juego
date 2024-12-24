using P_P;
using P_P.tramps;

namespace P_P.board;

public class Shell
{
    public string? CharacterIcon { get; set; }
    
    public string? ObjectType { get; set; }
    public string? ObjectId { get; set; }
    public bool HasCharacter { get; set; }
    
    public bool HasObject { get; set; }

    public Shell()
    {
        CharacterIcon = null;
        HasCharacter = false;
        HasObject = false;
        ObjectType = null;
        ObjectId = null;
    }
  
}