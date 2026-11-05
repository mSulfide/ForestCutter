public class GameState
{
    public const string DefaultName = "UnknownSave";

    private string _name = DefaultName;
    private EGameLevel _level = EGameLevel.OakForest;
    private bool _victory = false;

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public EGameLevel Level
    {
        get => _level;
        set => _level = value;
    }

    public bool Victory
    {
        get => _victory;
        set => _victory = value;
    }
}