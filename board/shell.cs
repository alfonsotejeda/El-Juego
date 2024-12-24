using P_P;
using P_P.tramps;

namespace P_P.board;

public class Shell
{
    public bool IsCenter { get; set; }
    public bool IsTrophy { get; set; }
    public string? CharacterIcon { get; set; }
    
    public string? ObjectType { get; set; }
    public bool HasCharacter { get; set; }
    
    public bool HasObject { get; set; }

    public Shell()
    {
        IsCenter = false;
        IsTrophy = false;
        CharacterIcon = null;
        HasCharacter = false;
        HasObject = false;
    }
    public virtual InteractiveObjects CreateObject(string objectType)
    {
        return objectType switch
        {
            "tramp" => new BaseTramp(0, null),
            _ => new InteractiveObjects(objectType)
        };
    }
}