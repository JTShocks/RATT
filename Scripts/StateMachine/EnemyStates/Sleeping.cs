using Godot;
using System;

public partial class Sleeping : EnemyState
{

    public override void OnEnter(string previousStatePath)
    {
        base.OnEnter(previousStatePath);
        enemy.IsVulnerable = true;

        //Disable the collision for the bounding box while enemy is sleeping, so player can sneak past them
        enemy.SetCollisionLayerValue(3, false);
        
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

    public override void OnExit()
    {
        base.OnExit();

        //Re-enable the collision for the enemy when they leave the Sleeping state, for any reason
        enemy.SetCollisionLayerValue(3, true);
    }
}
