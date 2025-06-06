using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    int MAXSPEED = 100;

    [Export]
    int Acceleration = 600;

    [Export]
    int Friction = 600;

    public int currentHealth = 6;
    public Vector2 Motion = Vector2.Zero;

    AnimationPlayer animationPlayer = null;
    AnimationTree animationTree = null;
    AnimationNodeStateMachinePlayback animationState = null;
    Stats states = null;
    Hurtbox hurtbox = null;
    

    State state = State.MOVE;
    Vector2 Velocity = Vector2.Zero;


    enum State
    {
        MOVE,
        ATTACK
    }

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationTree = GetNode<AnimationTree>("AnimationTree");
        animationState = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
        states = GetNode<Stats>("/root/PlayerStats");
        hurtbox = GetNode<Hurtbox>("Hurtbox");

        states.Connect("no_health", this, nameof(queue_free));

        animationTree.Active = true;

    }

    public void _physics_process(float delta)
	{
        switch (state)
        {
            case State.MOVE:
                Move_State(delta); break;
            case State.ATTACK:
                Attack_State(delta); break;
            default:
                break;
        }

        //GD.Print(GetTree().Root.GetNode("World").GetNode("YSort/Player").GetNode<Stats>("PlayerStats").max_health);
        //GD.Print(Position);

    }

    public void Move_State(float delta)
    {

        //var Motion = Vector2.Zero;
        Motion.x = Input.GetActionStrength("ui_left") - Input.GetActionStrength("ui_right");
        Motion.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        Motion = Motion.Normalized();
        
        if (Motion != Vector2.Zero)
        {

            animationTree.Set("parameters/Idle/blend_position", Motion);
            animationTree.Set("parameters/Run/blend_position", Motion);
            animationTree.Set("parameters/Attack/blend_position", Motion);

            animationState.Travel("Run");
            Velocity = Velocity.MoveToward(Motion * MAXSPEED, Acceleration * delta);
        }
        else
        {

            animationState.Travel("Idle");
            Velocity = Velocity.MoveToward(Vector2.Zero, Friction * delta);

        }

        Velocity = MoveAndSlide(Velocity);

        if (Input.IsActionJustPressed("attack"))
        {
            state = State.ATTACK;
        }
    }

    public void Attack_State(float delta)
    {
        Velocity = Vector2.Zero;
        animationState.Travel("Attack");
    }

    public void Attack_Animation_Finished()
    {
        state = State.MOVE;
    }

    public void _on_Hurtbox_area_entered(Area2D area)
    {
        //states.health -= 1;
        currentHealth = states.health - 1;
        states.set_health(currentHealth);
        hurtbox.start_invincibility(0.5f);
        hurtbox.create_hit_effect();
        //GD.Print(states.health);
    }

    public void queue_free()
    {
        //GD.Print(states.health);
        QueueFree();

    }

}
