using Godot;
using System;
using System.Reflection;
using System.Management.Instrumentation;
public class Grass : Node2D
{

    public override void _Ready()
    {
        
    }

    public void create_grass_effect()
    {
        PackedScene GrassEffect = ResourceLoader.Load<PackedScene>("res://Effects/GrassEffect.tscn"); //Load scene
        Node2D grassEffect = GrassEffect.Instance<Node2D>();
        //var world = GetTree().CurrentScene;
        GetParent().AddChild(grassEffect);
        grassEffect.GlobalPosition = GlobalPosition;
        //QueueFree();

    }
    #region
    //public void _process(float delta)
    //{
    //    if (Input.IsActionJustPressed("attack"))
    //    {
    //        PackedScene GrassEffect = ResourceLoader.Load<PackedScene>("res://Effects/GrassEffect.tscn");
    //        Node2D grassEffect = GrassEffect.Instance<Node2D>();
    //        var world = GetTree().CurrentScene;
    //        world.AddChild(grassEffect);
    //        grassEffect.GlobalPosition = GlobalPosition;

    //        QueueFree();
    //    }
    //}
    #endregion
    public void _on_Hurtbox_area_entered(Area2D area)
    {
        create_grass_effect();
        QueueFree();
    }
}
