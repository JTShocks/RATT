using Godot;
using System;

public partial class Running : PlayerState
{
    //Find the Movement and Gravity components



    public override void OnEnter(string previousStatePath)
    {
        base.OnEnter(previousStatePath);


    }

    public override void StatePhysicsProcess(float delta)
    {
        base.StatePhysicsProcess(delta);
        player.movementComponent.HandleHorizontalMovement(player, player.inputComponent.inputHorizontal);

        player.MoveAndSlide();

        if(!player.IsOnFloor())
        {
            //EmitSignal(SignalName.Finished, "Falling");
        }
        else if(player.inputComponent.GetJumpInput())
        {
            //EmitSignal(SignalName.Finished, "Jumping");
        }
        else if(Mathf.IsEqualApprox(player.inputComponent.inputVector.X, 0.0f))
        {
            EmitSignal("Finished", "Running");
        }
        
    }
}
