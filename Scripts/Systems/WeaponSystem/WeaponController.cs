using Godot;
using System;

public partial class WeaponController : Node2D
{

	[Export] public Weapon ActiveWeapon;
	[Export] AnimationTree animator; // For animations

	//Need to make sure that the component accounts for both ranged and melee weapons

	[Signal]
	public delegate void OnUpdateWeaponUIEventHandler(Weapon weapon);

	[Signal]
	public delegate void OnWeaponUsedEventHandler(Weapon weapon);

	[Signal]
	public delegate void OnSwitchWeaponsEventHandler(Weapon weapon);

	[Signal]
	public delegate void OnWeaponReloadEventHandler(Weapon weapon);
	[Signal]
	public delegate void OnWeaponFinishReloadEventHandler();
	bool IsHoldingAttackButton;
	bool IsTryingToAttack;
	bool IsHoldingAltFire;
	bool ReleasedFire;


	Vector2 shootAngle;






	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetActiveWeapon(ActiveWeapon);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// try to Fire the active weapon

		IsHoldingAttackButton = Input.IsActionPressed("Fire");
		IsTryingToAttack = Input.IsActionJustPressed("Fire");
		ReleasedFire = Input.IsActionJustReleased("Fire");

		if(Input.IsActionJustPressed("NextWeapon"))
		{
			SwitchWeapons();
		}

		switch(ActiveWeapon.weaponType)
		{
			case Weapon.WeaponType.Automatic:

				//If is holding down the fire button
				if(IsHoldingAttackButton)
				{
					try
					{
						ActiveWeapon.Fire();
					}
					catch(Exception)
					{

					}
				}
			break;

			case Weapon.WeaponType.SemiAuto:

				if(IsTryingToAttack)
				{
					try
					{
						ActiveWeapon.Fire();
					}
					catch(Exception)
					{

					}
				}
			break;

			case Weapon.WeaponType.Melee:
				if(IsHoldingAttackButton)
				{
					ActiveWeapon.Aim(); //Charge the melee weapon
					ActiveWeapon.Charge((float)delta);
					UpdateWeaponUI();
				}

				if(ReleasedFire)
				{
						ActiveWeapon.Fire(); //Strike with the weapon
					

				}
			break;
		}

		//Rotate the gun to match where the player is aiming

		LookAt(GetGlobalMousePosition());

		if(Input.IsActionJustPressed("Reload"))
		{

			ActiveWeapon.Reload();
		}
	}


	public void OnFire()
	{
		//Set the animation for the weapon properly
		//Compare against the weapon types again, see what needs to be done



		if(ActiveWeapon.weaponType != Weapon.WeaponType.Melee)
		{
					Vector2 mousePosition = GetGlobalMousePosition();

			//The weapon spread is adding an amount to the mousePosition either + or - the spread value
			var rng = new RandomNumberGenerator();

			Vector2 spread = new Vector2(rng.RandfRange(-ActiveWeapon.stats.BaseSpread, ActiveWeapon.stats.BaseSpread),rng.RandfRange(-ActiveWeapon.stats.BaseSpread, ActiveWeapon.stats.BaseSpread));
			Vector2 direction = ((mousePosition + spread) - GlobalPosition).Normalized();
			GD.Print(direction + " : Bullet Direction");
			ActiveWeapon.Shoot(direction);
			GlobalSignals signals = GetTree().Root.GetNode<GlobalSignals>("GlobalSignals");
			signals.EmitSignal(GlobalSignals.SignalName.TriggerScreenShake, ActiveWeapon.stats.WeaponShakeForce, ActiveWeapon.stats.ScreenShakeDecay);
		}



	}

	public void OnShoot()
	{
		UpdateWeaponUI();
	}

	public void OnReload()
	{
		EmitSignal(SignalName.OnWeaponReload, ActiveWeapon);
	}
	public void OnReloadComplete()
	{
		UpdateWeaponUI();
		EmitSignal(SignalName.OnWeaponFinishReload);
	}

	public void SwitchWeapons()
	{
		int current = ActiveWeapon.GetIndex();
		Weapon nextWeapon;
		if(current + 1 > GetChildCount() - 1)
		{
			nextWeapon = GetChild<Weapon>(0);
		}
		else
		{
			nextWeapon = GetChild<Weapon>(current + 1);
			
		}

		UnequipWeapon();
		SetActiveWeapon(nextWeapon);

	}

	void UpdateWeaponUI()
	{
		EmitSignal(SignalName.OnUpdateWeaponUI, ActiveWeapon);
	}

	public void UnequipWeapon()
	{
			ActiveWeapon.Visible = false;
			ActiveWeapon.OnFire -= OnFire;
			ActiveWeapon.OnReload -= OnReload;
			ActiveWeapon.OnReloadComplete -= OnReloadComplete;
			ActiveWeapon.OnShoot -= OnShoot;
	}

	public void SetActiveWeapon(Weapon weapon)
	{
		ActiveWeapon = weapon;
		ActiveWeapon.Visible = true;

		//Subscribe to all the signals of the new weapon
			ActiveWeapon.OnFire += OnFire;
			ActiveWeapon.OnReload += OnReload;
			ActiveWeapon.OnReloadComplete += OnReloadComplete;
			ActiveWeapon.OnShoot += OnShoot;
		
		UpdateWeaponUI();
		



	}
}
