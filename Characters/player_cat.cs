using Godot;
using System;

public partial class player_cat : CharacterBody2D
{
    //public const float Speed = 100.0f;
    [Export]
    public float Speed= 100.0f;
    
    [Export]
    public Vector2 starting_direction = new Vector2(0, -1);

    private Vector2 input_direction = new Vector2();
    private AnimationTree animation_tree;
    private AnimationNodeStateMachinePlayback state_machine;
    
    public override void _Ready()
    {
        animation_tree = GetNode<AnimationTree>("AnimationTree");
        state_machine = animation_tree.Get("parameters/playback").As<AnimationNodeStateMachinePlayback>();
    }
    
    public override void _PhysicsProcess(double delta)
    {
        // Get direction from controls
        input_direction = new Vector2(
            Input.GetActionStrength("right")-Input.GetActionStrength("left"),
            Input.GetActionStrength("down")-Input.GetActionStrength("up")
        );
        GD.Print(input_direction);
        
        UpdateAnimationParameters(input_direction);
        PickNewState();
        
        Velocity = input_direction*Speed;
        MoveAndSlide();
    }
    
    private void UpdateAnimationParameters(Vector2 move_input)
    {
        if (move_input!=Vector2.Zero){
            animation_tree.Set("parameters/Idle/blend_position", move_input);
            animation_tree.Set("parameters/Walk/blend_position", move_input);
        }
    }
    private void PickNewState()
    {
        if (Velocity!=Vector2.Zero){
            state_machine.Travel("Walk");
        } else{
          state_machine.Travel("Idle");  
        }
    }
    
}
