using Godot;
using System;
using Newtonsoft.Json;

public class DataManager : Node
{

    public static DataModel _data = new DataModel();

    static int Damage;
    static int MaxHealth;
    static int CurrentHealth;
    static float CurrentPosition_X;
    static float CurrentPosition_Y;


    public override void _Ready()
    {
        ReadSaveFile();
    }

    public void _physics_process(float delta)
    {
        try
        {
            Vector2 P = GetTree().Root.GetNode("World").GetNode<Player>("YSort/Player").Position;

            Damage = GetTree().Root.GetNode("World").GetNode("YSort/Player").GetNode<Hitbox>("HitboxPivot/SwordHitbox").damage;
            MaxHealth = GetTree().Root.GetNode("World").GetNode("YSort/Player").GetNode<Stats>("PlayerStats").max_health;
            CurrentHealth = GetTree().Root.GetNode("World").GetNode<Player>("YSort/Player").currentHealth;
            CurrentPosition_X = P.x;
            CurrentPosition_Y = P.y;

        }
        catch
        {
            GetTree().ChangeScene("res://Menu/StartMenu.tscn");
        }
    }


    private const string PATH = "res://Debug/Save/save.json";

    public static void Save()
    {
        update();
        WriteSaveFile();
    }

    public static DataModel Read()
    {
        ReadSaveFile();
        return _data;
    }

    public static void ReadSaveFile()
    {
        string jsonString = null;
        var saveFile = OpenSaveFile(File.ModeFlags.Read);
        if (saveFile != null)
        {
            jsonString = saveFile.GetLine();
            try
            {
                _data = Deserialize(jsonString);
            }
            catch
            {
                _data = new DataModel();
            }
            saveFile.Close();
        }
    }

    public static void WriteSaveFile()
    {
        var saveFile = OpenSaveFile(File.ModeFlags.Write);
        if (saveFile != null)
        {
            var json = JsonConvert.SerializeObject(_data);
            saveFile.StoreLine(json);
            saveFile.Close();
        }

    }

    private static File OpenSaveFile(File.ModeFlags flag = File.ModeFlags.Read)
    {
        var saveFile = new File();
        var err = saveFile.Open(PATH, flag);
        if (err == 0)
        {
            return saveFile;
        }
        return null;
        
    }

    private static DataModel Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<DataModel>(json);
    }

    public static void update()
    {
        _data.Damage = Damage;
        _data.MaxHealth = MaxHealth;
        _data.CurrentHealth = CurrentHealth;
        _data.Position_X = CurrentPosition_X;
        _data.Position_Y = CurrentPosition_Y;

    }

    public static void SetHealth()
    {
        _data.MaxHealth = MaxHealth;
    }
}
