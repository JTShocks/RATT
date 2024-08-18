using Godot;
using System;

public partial class Sliding : PlayerState
{

    [Export] float slideFriction = 0.5f;
    public override void OnEnter(string previousStatePath)
    {
        base.OnEnter(previousStatePath);
    }

    public override void StatePhysicsProcess(float delta)
    {
        base.StatePhysicsProcess(delta);

        player.Velocity = player.Velocity with {X = Mathf.Lerp(player.Velocity.X, 0, slideFriction)};

        player.MoveAndSlide();

        if(Mathf.Abs(player.Velocity.X) <= 50)
        {
            //If the absolute value of the player's velocity is less than 50, just stop them
            EmitSignal("Finished", "Prone");
        }
        else if(Mathf.IsEqualApprox(player.inputComponent.inputVector.Y, 0.0f))
        {
            EmitSignal("Finished", "Idle");
        }
    }
}
