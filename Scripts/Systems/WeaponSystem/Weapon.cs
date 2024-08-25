using Godot;
using System;

[GlobalClass]
public partial class Weapon : Node2D
{
	public enum WeaponType
	{
		Melee,
		SemiAuto,
		Automatic

	}

	[Export] public WeaponStats stats;
	[Export] public WeaponType weaponType;

	[Signal]
	public delegate void OnFireEventHandler(); //Emit when the weapon is fired
	[Signal]
	public delegate void OnShootEventHandler();
	[Signal]
	public delegate void OnReloadEventHandler();
	[Signal]
	public delegate void OnReloadCompleteEventHandler();

	public int CurrentAmmo = 0;
	public int RemainingAmmo = 0;

	//Boolean values
	public bool CanFire = true;
	public bool CanCharge = true;

	public bool isReloading;


	//Charge Weapon values
	bool isCharging;
	public float CurrentCharge = 0;
	public bool fullCharge => CurrentCharge >= 100; //Weapon has full charge if the CurrentCharge is greater than 100
	

	[ExportSubgroup("Nodes")]
	[Export] public Marker2D firePoint;
	[Export] public AudioStreamPlayer2D audioSource;

	[ExportSubgroup("Sound Effects")]
	[Export] AudioStream shootSFX;
	[Export] AudioStream reloadSFX;

	[ExportSubgroup("Animations")]
	[Export] protected AnimationPlayer animator;

	public Timer AttackCooldown;

	public Timer ReloadTimer;

    public override void _Ready()
    {
        base._Ready();

		ReloadTimer = new Timer()
		{
			Name = "ReloadTimer",
			OneShot = true
		};

		AttackCooldown = new Timer()
		{
			Name = "AttackCooldown",
			OneShot = true
			
		};
		AddChild(ReloadTimer);
		AddChild(AttackCooldown);

		ReloadTimer.Timeout += ReloadComplete; //When this internal timer finishes, regardless of the animation, the weapon will be reloaded
		AttackCooldown.Timeout += Reset;

		CurrentAmmo = stats.BaseClipSize;
		RemainingAmmo = stats.MaxAmmoReserve;
    }


    public virtual void Fire()
	{
		if(CanFire && !isReloading)
		{
			EmitSignal(SignalName.OnFire);
			AttackCooldown.Start(stats.BaseRateOfFire);
			CanFire = false;
			GD.Print(Name + " has fired");

		}

	}

	public virtual void Shoot(Vector2 shootDirection)
	{
		var projectile = GD.Load<PackedScene>("res://Scenes/Weapons/Bullet_Prefabs/bullet.tscn").Instantiate() as Projectile;
            if(projectile is Projectile bullet)
            {
                bullet.AssignValues(shootDirection, 6400, stats.BaseDamage);


                GetTree().Root.AddChild(bullet);
                bullet.GlobalPosition = firePoint.GlobalPosition;
				bullet.Rotate(GetAngleTo(shootDirection));
                OnShoot += bullet.Launch;
            }

			audioSource.Stream = shootSFX;
			var rng = new RandomNumberGenerator();
			float pitchShift = rng.RandfRange(.9f, 1.1f);
			audioSource.PitchScale = pitchShift;

			audioSource.Play();

		CurrentAmmo--;
		EmitSignal(SignalName.OnShoot);
		//Emit a signal so all the fired projectiles can attack all at once
	}

	public virtual void Aim()
	{
		//For charge weapons
	}

	public virtual void Charge(float delta)
	{
		CurrentCharge += stats.ChargeRate * delta;
		CurrentCharge = Mathf.Clamp(CurrentCharge, 0.0f, 100.0f); //Weapon charge is locked between 0 and 1 and cannot go below.
	}
	void Reset()
	{
		CanFire = true;
	}

	public virtual void Reload()
	{
		if(RemainingAmmo <= 0)
        {
			GD.PrintErr( Name + " is out of Ammo");
            return; 
        }
		else if(isReloading)
		{
			GD.PrintErr("Player is already reloading.");
			return;
		}
		else if(CurrentAmmo == stats.BaseClipSize)
		{
			GD.PrintErr(Name +" is already full ammo.");
			return;
		}

		ReloadTimer.Start(stats.BaseReloadSpeed);
		isReloading = true;
		EmitSignal(SignalName.OnReload);
	}

	public virtual void ReloadComplete()
	{
		isReloading = false;
		EmitSignal(SignalName.OnReloadComplete);
		GD.Print("Reload finished");
		Reset();
	}



}
