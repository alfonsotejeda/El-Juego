namespace P_P.board;

public class Shell
{
    public bool IsCenter { get; set; }
    public bool IsTrophy { get; set; }
    public string? CharacterIcon { get; set; }
    public bool HasCharacter { get; set; }

    public Shell()
    {
        IsCenter = false;
        IsTrophy = false;
        CharacterIcon = null;
        HasCharacter = false;
    }
}