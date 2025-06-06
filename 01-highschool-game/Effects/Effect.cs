using Godot;
using System;

public class Effect : AnimatedSprite
{


    public override void _Ready()
    {

        Connect("animation_finished", this, nameof(_on_animation_finished));

        Frame = 0;
        Play("Animate");
 
    }

    public void _on_animation_finished()
    {
        QueueFree();
    }
}
  