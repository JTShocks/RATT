using Godot;
using System;

public partial class Sleeping : EnemyState
{

    public override void OnEnter(string previousStatePath)
    {
        base.OnEnter(previousStatePath);
        enemy.IsVulnerable = true;
        
    }

    public override void StatePhysicsProcess(float delta)
    {
        enemy.Velocity = enemy.Velocity with {X = 0};
        //HandleGravity
        base.StatePhysicsProcess(delta);


        if(enemy.IsAwake)
        {
            //Go to WakeUp state
            EmitSignal("Finished", "WakeUp");
        }

    }
}
