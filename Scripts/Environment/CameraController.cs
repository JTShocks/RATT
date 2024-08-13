using Godot;
using System;

public partial class CameraController : Camera2D
{

	[ExportSubgroup("Screen Shake Values")]

	[Export] bool enableScreenShake = true;
	[Export] float decay = 0.8f; //How quick the shake stops
	Vector2 defaultOffset;
	[Export] Vector2 maxOffset = new Vector2(100,75); //Maximum offset for the camera in any direction
	[Export] float maxRoll = 0.1f; //How much the camera should rotate when shaking
	[Export] Node2D target;
	[Export] Vector2 shakeMinMax = new Vector2(0.0f, 1.0f); //Min and Max values for the trauma limits for the shake

	float trauma = 0.0f;
	int traumaPower = 2;

	RandomNumberGenerator rng = new RandomNumberGenerator();
	FastNoiseLite noise = new FastNoiseLite();
	float noise_y = 0;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		defaultOffset = Offset;
		GlobalSignals signals = GetTree().Root.GetNode<GlobalSignals>("GlobalSignals");
		signals.TriggerScreenShake += OnTriggerScreenShake;
		rng.Randomize();
			noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin;
			noise.Seed = (int)rng.Randi();

			noise.Frequency = 0.5f;
			noise.FractalOctaves = 2;

		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		//GlobalPosition = target.GlobalPosition.Lerp(GetGlobalMousePosition(), 0.15f);

		if(target != null)
		{
			GlobalPosition = target.GlobalPosition.Lerp(GetGlobalMousePosition(), 0.15f);
		}

		if(trauma > 0)
		{
			trauma = Mathf.Max(trauma - decay * (float)delta, shakeMinMax.X);
			Shake();
		}
	}

	public void AddTrauma(float amount)
	{
		trauma = Mathf.Min(trauma + amount, shakeMinMax.Y);
	}


	public void Shake()
	{
		if(!enableScreenShake)
		{
			return;
		}
		var amount = Mathf.Pow(trauma, traumaPower);
		noise_y += 1;
		Rotation = maxRoll * amount * noise.GetNoise2D(noise.Seed, noise_y);
		Offset = defaultOffset + new Vector2(maxOffset.X * amount * noise.GetNoise2D(noise.Seed*2, noise_y), maxOffset.Y * amount * noise.GetNoise2D(noise.Seed*3, noise_y));

	}

	public void OnTriggerScreenShake(float kick, float decay = 0.8f)
	{
		AddTrauma(kick);
		this.decay = decay;
	}

	//TODO: Make a function to trigger a smoother "kick" effect, rather than a full camera shake
}
