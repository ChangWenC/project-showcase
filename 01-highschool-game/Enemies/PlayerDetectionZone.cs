using Godot;
using System;

public class PlayerDetectionZone : Area2D
{
    public Node2D player = null;

    public bool can_see_player()
    {
        return (player != null);
    }

    public void _on_PlayerDetectionZone_body_entered(Node2D body)
    {
        player = body;
    }

    public void _on_PlayerDetectionZone_body_exited(Node2D body)
    {
        player = null;
    }
}
