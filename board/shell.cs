namespace P_P;

public class Shell
{
    public bool IsPath { get; set; }
    public bool IsWall { get; set; }
    public bool IsTramp { get; set; }
    
    public string? TrampIcon { get; set; }
    public bool IsCenter { get; set; }
    public bool IsTrophy { get; set; }

    public string PathIcon = "⬜️";
    public string WallIcon = "🟫";
    public string? CharacterIcon { get; set; }
    public bool HasCharacter { get; set; }

    public Shell()
    {
        IsPath = false;
        IsWall = false;
        IsTramp = false;
        TrampIcon = null;
        IsCenter = false;
        IsTrophy = false;
        CharacterIcon = null;
        HasCharacter = false;
    }
}