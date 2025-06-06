using Godot;
using System;

public class StartMenu : Control
{

    public override void _Ready()
    {
        
    }

    public void _on_GameStartB_pressed()
    {
        GD.Print("Hi HI");
        GetTree().ChangeScene("res://World.tscn");
        GetTree().Paused = false;

    }

    public void _on_OptionB_pressed()
    {

    }

    public void _on_QuitB_pressed()
    {
        GD.Print("bye bye");
        GetTree().Quit();
    }

}
