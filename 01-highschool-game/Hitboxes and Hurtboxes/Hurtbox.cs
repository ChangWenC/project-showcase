using Godot;
using System;

public class Hurtbox : Area2D
{

    //[Export]
    //bool show_hit = true;
    bool invincible = false;

    PackedScene HitEffect = ResourceLoader.Load<PackedScene>("res://Effects/HitEffect.tscn"); //Load scene

    [Signal] public delegate void invesibility_started();
    [Signal] public delegate void invesibility_ended();

    Timer timer = null;
    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
    }

    public void set_invincible(bool value)
    {
        invincible = value;
        if(invincible == true)
        {
            EmitSignal("invesibility_started");
        }
        else
        {
            EmitSignal("invesibility_ended");
        }
    }

    public void start_invincibility(float duration)
    {
        this.set_invincible(true);
        timer.Start(duration);
    }

    //public void _on_Hurtbox_area_entered(Area2D area)
    //{
    //    if (show_hit == true)
    //    {
    //        Node2D effect = HitEffect.Instance<Node2D>();
    //        var main = GetTree().CurrentScene;
    //        main.AddChild(effect);
    //        effect.GlobalPosition = GlobalPosition;
    //    }
    //}
    public void create_hit_effect()
    {
        Node2D effect = HitEffect.Instance<Node2D>();
        var main = GetTree().CurrentScene;
        main.AddChild(effect);
        effect.GlobalPosition = GlobalPosition;
    }

    public void _on_Timer_timeout()
    {
        this.set_invincible(false);
    }

    public void _on_Hurtbox_invesibility_started()
    {
        SetDeferred("monitorable", false);
    }

    public void _on_Hurtbox_invesibility_ended()
    {
        SetDeferred("monitorable", true);

    }
}
