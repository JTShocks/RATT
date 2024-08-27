using Godot;
using Godot.Collections;

[Tool]
public partial class HurtboxController : Node2D
{

	//[ExportGroup("Hurtbox")] 
	public Array<HurtboxRef> Hurtboxes = [];


    public override Array<Dictionary> _GetPropertyList()
    {
		var properties = new Array<Dictionary>();

		/*properties.Add({
			"name" : "Hurtboxes",
			"type" : Variant.Type.Color,
			"usage" : 
		});*/
        return properties;
    }


    public override void _Ready()
    {
        base._Ready();

		Hurtboxes.Resize(GetChildCount());

		foreach(HurtboxComponent hurtbox in GetChildren())
		{
			HurtboxRef newRef = new HurtboxRef()
			{
				Hurtbox = hurtbox,
				DebugArea = hurtbox.GetNode<CollisionObject2D>("CollisionObject2D"),
			};


			Hurtboxes.Add(newRef);

		}
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{

		if(Engine.IsEditorHint())
		{
			foreach(HurtboxRef hurtbox in Hurtboxes)
			{
				
			}
		}
	}


}
