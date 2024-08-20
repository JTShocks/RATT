using Godot;
using System;

public partial class Sleeping : EnemyState
{

    public override void OnEnter(string previousStatePath)
    {
        base.OnEnter(previousStatePath);
        
    }

    public override void StatePhysicsProcess(float delta)
    {
        enemy.Velocity = enemy.Velocity with {X = 0};
        //HandleGravity
        //


        base.StatePhysicsProcess(delta);


        if(enemy.IsAwake)
        {
            //Go to Awakened state
            EmitSignal("Finished", "Awake");
        }

    }
}
