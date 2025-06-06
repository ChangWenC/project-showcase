using Godot;
using System;
using System.Collections.Generic;



public class FlyingEye : KinematicBody2D
{
    PackedScene EnemtDeathEffect = ResourceLoader.Load<PackedScene>("res://Effects/EnemyDeathEffect.tscn"); //Load scene

    Stats stats = null;
    PlayerDetectionZone playerDetectionZone = null;
    AnimatedSprite sprite = null;
    Hurtbox hurtbox = null;
    WanderController wanderController = null;
    Vector2 knockback = Vector2.Zero;
    

    int friction = 90; // for knockback
    int direction = 6; // for knockback

    [Export]
    float ACCELERATION = 300; //for enemy movement
    [Export]
    float MAX_SPEED = 50; //for enemy movement
    [Export]
    float FRICTION = 200; //for enemy movement

    enum State
    {
        IDLE,
        WANDER,
        CHASE
    }

    Vector2 velocity = Vector2.Zero;
    State state = State.CHASE;

    public override void _Ready()
    {
        stats = GetNode<Stats>("Stats");
        playerDetectionZone = GetNode<PlayerDetectionZone>("PlayerDetectionZone");
        sprite = GetNode<AnimatedSprite>("AnimatedSprite");
        hurtbox = GetNode<Hurtbox>("Hurtbox");
        wanderController = GetNode<WanderController>("WanderController");
    }

    public void _physics_process(float delta)
    {
        knockback = knockback.MoveToward(Vector2.Zero, 250 * delta);
        knockback = MoveAndSlide(knockback);

        #region knockback
        if (Input.IsActionPressed("ui_left"))
        {
            direction = 0;
        }
        if (Input.IsActionPressed("ui_right"))
        {
            direction = 1;
        }
        if (Input.IsActionPressed("ui_down"))
        {
            direction = 2;
        }
        if (Input.IsActionPressed("ui_up"))
        {
            direction = 3;
        }
        #endregion

        switch (state)
        {
            case State.IDLE:
                velocity = velocity.MoveToward(Vector2.Zero, 300 * delta);
                searching_player();

                //if (wanderController.get_Time_left() == 0)
                //{
                //    if (pick_random_state() == 1)
                //    {
                //        state = State.IDLE;
                //    }
                //    else
                //    {
                //        state = State.WANDER;
                //    }
                //    wanderController.set_wander_timer(((float)GD.RandRange(1, 3)));
                //}

                break;

            case State.CHASE:
                var player = playerDetectionZone.player;
                if(player != null)
                {
                    Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
                    velocity = velocity.MoveToward(direction * MAX_SPEED, ACCELERATION * delta);
                }
                else
                {
                    state = State.IDLE;
                }
                sprite.FlipH = velocity.x < 0;
                break;

            case State.WANDER:
                //velocity = velocity.MoveToward(Vector2.Zero, 300 * delta);
                //searching_player();

                //if (wanderController.get_Time_left() == 0)
                //{
                //    if (pick_random_state() == 1)
                //    {
                //        state = State.IDLE;
                //    }
                //    else
                //    {
                //        state = State.WANDER;
                //    }
                //    wanderController.set_wander_timer(((float)GD.RandRange(1, 3)));
                //}

                //Vector2 direction2 = (wanderController.target_position).Normalized();
                //velocity = velocity.MoveToward(direction2 * MAX_SPEED, ACCELERATION * delta);

                break;

            default:
                break;
        }
        velocity = MoveAndSlide(velocity);
    }

    public void searching_player()
    {
        if (playerDetectionZone.can_see_player())
        {
            state = State.CHASE;
        }
    }

    public void _on_Hurtbox_area_entered(Area2D area)
    {
        //var knockback_vector = area.GetPosition(knockback_vector)
        var Motion = Vector2.Zero;

        Motion.x = Input.GetActionStrength("ui_left") - Input.GetActionStrength("ui_right");
        Motion.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");



        if (Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right"))
        {

            knockback = Motion * friction;

        }
        else
        {
            if (direction == 0)
            {
                knockback = Vector2.Right * friction;
            }
            if (direction == 1)
            {
                knockback = Vector2.Left * friction;
            }
            if (direction == 2)
            {
                knockback = Vector2.Down * friction;
            }
            if (direction == 3)
            {
                knockback = Vector2.Up * friction;
            }

        }

        stats.set_health(stats.health - area.GetNode<SwordHitbox>("/root/World/YSort/Player/HitboxPivot/SwordHitbox").damage);
        hurtbox.create_hit_effect();
        GD.Print(stats.health);
    }

    public void _on_Stats_no_health()
    {
        QueueFree();
        var enemyDeathEffect = EnemtDeathEffect.Instance<Node2D>();
        GetParent().AddChild(enemyDeathEffect);
        enemyDeathEffect.GlobalPosition = GlobalPosition;
    }

    //public int pick_random_state()
    //{
    //    //Random r = new Random();
    //    //var values = new[] { 1, 2 };
    //    //int result = values[r.Next(values.Length)];
    //    //return result;
    //}

}
