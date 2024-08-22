using Godot;
using Godot.Collections;
using System;

public partial class LaserSight : Line2D
{

	[Export] float maxLineLength;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		var spaceState = GetWorld2D().DirectSpaceState;
		var query = PhysicsRayQueryParameters2D.Create(GlobalPosition, GlobalPosition + GlobalTransform.X * maxLineLength);
		var result = spaceState.IntersectRay(query);


		if(result.Count > 0)
		{
			
			Vector2 hitPoint = (Vector2)result["position"];
			float magnitude = GlobalPosition.DistanceTo(hitPoint);
			SetPointPosition(1, new Vector2( magnitude,0));
		}
		else
		{
			SetPointPosition(1, new Vector2(maxLineLength, 0));
		}

	
	}
}
