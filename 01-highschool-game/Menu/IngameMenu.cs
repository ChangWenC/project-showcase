using Godot;
using System;
using Newtonsoft.Json;

public class IngameMenu : ColorRect
{

    bool is_paused = false;

    public IngameMenu()
    {
    }

    public override void _Ready()
    {
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("pause"))
        {
            this.set_is_paused(!is_paused);
        }
    }

    public void set_is_paused(bool index)
    {
        is_paused = index;
        GetTree().Paused = is_paused;
        Visible = is_paused;
    }

    public bool get_is_paused()
    {
        return is_paused;
    }

    public void _on_Save_pressed()
    {
        //_updata();
        DataManager.Save();
    }

    public void _on_Load_pressed()
    {
        DataModel _data = new DataModel();
        _data = DataManager.Read();

        Vector2 CurrentP = Vector2.Zero;
        CurrentP.x = _data.Position_X;
        CurrentP.y = _data.Position_Y;

        GetTree().Root.GetNode("World").GetNode("YSort/Player").GetNode<Hitbox>("HitboxPivot/SwordHitbox").damage = DataManager._data.Damage;
        GetTree().Root.GetNode("World").GetNode("YSort/Player").GetNode<Stats>("PlayerStats").max_health = DataManager._data.MaxHealth;
        GetTree().Root.GetNode("World").GetNode<Player>("YSort/Player").currentHealth = DataManager._data.CurrentHealth;
        GetTree().Root.GetNode("World").GetNode<Player>("YSort/Player").Position = CurrentP;

        //GD.Print(_data.Position_X);
    }

    public void _on_Restart_pressed()
    {
        GD.Print("Hi HI");
        
        GetTree().ChangeScene("res://World.tscn");
        GetTree().Paused = false;


    }

    public void _on_Quit_pressed()
    {
        GD.Print("bye bye");
        GetTree().ChangeScene("res://Menu/StartMenu.tscn");
    }

}