using Godot;
using System;

public class Stats : Node
{
    [Export]
    public int max_health = 1;
    public int health = 1;

    [Signal] public delegate void no_health();
    [Signal] public delegate void health_changed(int value);
    [Signal] public delegate void max_health_changed(int value);

    //public override void _Process(float delta)
    //{

    //}

    public override void _Ready()
    {
        health = max_health;
    }

    public void set_health(int value)
    {
        health = value;
        EmitSignal("health_changed", health);
        if (health <= 0)
        {
            EmitSignal("no_health");
        }
    }
    public void set_max_health(int value)
    {
        max_health = value;
        this.health = Math.Min(health, max_health);
        EmitSignal("max_health_changed");
    }
}
