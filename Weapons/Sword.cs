using Godot;
using System;

public partial class Sword : Area2D
{
    private AnimationPlayer anim_player;
    private int damage = 1;
    
    public void Attack()
    {
        anim_player.Play("swing");
    }
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
         anim_player = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
