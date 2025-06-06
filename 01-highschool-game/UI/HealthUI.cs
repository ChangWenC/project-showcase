using Godot;
using System;

public class HealthUI : Control
{
    int hearts = 4;
    int max_hearts = 4;

    Stats playerstates = null;
    TextureRect heartUIFull = null;
    TextureRect heartUIEmpty = null;
    

    public override void _Ready()
    {
        playerstates = GetNode<Stats>("/root/PlayerStats");
        heartUIFull = GetNode<TextureRect>("HeartUIFull");
        heartUIEmpty = GetNode<TextureRect>("HeartUIEmpty");

        this.max_hearts = playerstates.max_health;
        this.hearts = playerstates.health;
        playerstates.Connect("health_changed", this, "set_hearts");
        playerstates.Connect("max_health_changed", this, "set_max_hearts");

        heartUIFull.RectSize = Vector2.Right * hearts * 15;
        heartUIEmpty.RectSize = Vector2.Right * hearts * 15;


    }


    public void set_hearts(int value)
    {
        hearts = Mathf.Clamp(value, 0, max_hearts);
        if(heartUIFull != null)
        {
            
            heartUIFull.RectSize = Vector2.Right * hearts * 15;
            if(hearts == 1)
            {
                heartUIFull.Expand = true;
            }
        }

    }

    public void set_max_hearts(int value)
    {
        max_hearts = Math.Max(value, 1);
        this.hearts = Math.Min(hearts, max_hearts);
        if (heartUIEmpty != null)
        {
            heartUIEmpty.RectSize = Vector2.Right * hearts * 15;
            
        }
    }


}
