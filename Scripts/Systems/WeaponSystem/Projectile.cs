using Godot;
using System;

[GlobalClass]
public partial class Projectile : AnimatableBody2D
{    
	Texture2D sprite;
    Vector2 Velocity;
	Vector2 Direction;
	float Speed;
	int Damage;

	bool IsActive = false;

	[Export] CpuParticles2D bulletTrail;

	public Projectile(){}

	public Projectile(Vector2 direction, float speed, int damage, float rotation)
	{	
		Direction = direction;
		Speed = speed;
		Damage = damage;
		Rotation = rotation;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        SyncToPhysics = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);


      KinematicCollision2D hit = MoveAndCollide(Velocity * (float)delta);
        if(hit != null && IsActive)
        {
            OnHit(hit);
        }
    }

	void OnHit(KinematicCollision2D target)
	{

		if(target.GetCollider() is HurtboxComponent hurtbox)
		{
			hurtbox.OnGetHit(Damage);
		}
		QueueFree();
	}

	 public void Launch()
    {
        this.Velocity = Direction * Speed;
		
		IsActive = true;
        
    }

	public void AssignValues(Vector2 direction, float speed, int damage)
	{
		Direction = direction;
		Speed = speed;
		Damage = damage;

	}

}
