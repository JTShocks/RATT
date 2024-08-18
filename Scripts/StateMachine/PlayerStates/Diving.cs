using Godot;
using System;

public partial class Diving : PlayerState
{

    [Export] float diveForce = 800f;

    public override void OnEnter(string previousStatePath)
    {
        base.OnEnter(previousStatePath);
        player.Velocity = player.Velocity with {X = player.movementComponent.movementDirection * diveForce};
    }

    public override void StatePhysicsProcess(float delta)
    {
        base.StatePhysicsProcess(delta);
        player.MoveAndSlide();


        if(player.IsOnFloor())
        {
            if(player.Velocity.X != 0)
            {
                EmitSignal("Finished", "Sliding");
            }
            else
            {
                EmitSignal("Finished", "Prone");
            }

        }
    }
}
