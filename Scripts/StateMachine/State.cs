using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class State : Node
{
    [Signal]
    public delegate void FinishedEventHandler(string nextStatePath);

	public bool holdState; //Boolean to note if the state is allowed to be cancelled early or should be held
	public virtual void OnEnter(string previousStatePath)
	{
        GD.Print("Entered state:" + Name);
	}

    // 
	public virtual void OnExit()
	{
        GD.Print("Exited state:" + Name);
	}

	public virtual void StateProcess(float delta)
	{

	}

    public virtual void StatePhysicsProcess(float delta)
    {

    }

	public virtual void OnAnimationTreeAnimationFinished(StringName animationName)
	{

	}
	

}

