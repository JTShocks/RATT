using Godot;
using System;

public partial class InputComponent : Node
{

    public float inputHorizontal = 0.0f;
	public Vector2 inputVector;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		inputHorizontal = Input.GetAxis("Left", "Right");
		inputVector = Input.GetVector("Left", "Right", "Up", "Down");
	}

    public override void _Input(InputEvent @event)
    {
        if(@event.IsActionPressed("Pause"))
        {
            GameManager game = GetTree().Root.GetNode<GameManager>("GameManager");
            game.EmitSignal(GameManager.SignalName.OnPauseGame);
		    
        }

		

        base._Input(@event);
    }

    public bool GetJumpInput()
	{
		return Input.IsActionJustPressed("Jump");
	}

	public bool GetFireInput()
	{
		return Input.IsActionJustPressed("Fire");
	}

	public bool GetInteractInput()
	{
		return Input.IsActionJustPressed("Interact");
	}

}
