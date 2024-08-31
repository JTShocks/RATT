using Godot;
using System;

public partial class AssaultRifle : Weapon
{


    public override void Fire()
    {
        if(CurrentAmmo > 0)
        {
            //Shoot the bullet out & play the sound
            base.Fire();//Keep as it will always send the signal for the function
        }
        else if(CurrentAmmo == 0)
        {
            //Play sound of empty gun while the
            throw new Exception ("Out of Ammo");
        }


    }

    public override void Reload()
    {

        //Play the animation of reloading & start the sequence

        base.Reload(); // Keep as it will emit the signal
    }

    public override void ReloadComplete()
    {
        int bulletsUsed = stats.BaseClipSize - CurrentAmmo;

        if(RemainingAmmo > bulletsUsed)
        {

            RemainingAmmo -= bulletsUsed;
            CurrentAmmo = stats.BaseClipSize;

        }
        else
        {
            CurrentAmmo += RemainingAmmo;
            RemainingAmmo = 0;
        }
        RemainingAmmo = Mathf.Clamp(RemainingAmmo, 0, stats.MaxAmmoReserve);

        base.ReloadComplete();
    }

    public override void Shoot(Vector2 shootDirection)
    {
        //Hitscan shooting method

        //First, get the length of the shot equal to the max size of the viewpoint X value

        var rng = new RandomNumberGenerator();

        var spread = rng.RandfRange(-stats.BaseSpread,stats.BaseSpread);

        var spaceState = GetWorld2D().DirectSpaceState;
        //Create the raycast extending from the weapon's firepoint TO a point that is at the edge of the viewport size, with a random up or down variation based on the current spread
		var query = PhysicsRayQueryParameters2D.Create(firePoint.GlobalPosition, firePoint.GlobalPosition + (GlobalTransform.X * GetViewportRect().Size.X + new Vector2(0, spread)));
        
		var result = spaceState.IntersectRay(query);

        //Then check the result to register a potential hit

        if(result.Count > 0)
		{
			var contact = (GodotObject)result["collider"];
            if(contact is HurtboxComponent hit)
            {
                hit.OnGetHit(stats.BaseDamage);
               
                GD.Print("Hit " + hit.Name);
            }

            //Create a bullet tracer to hit it location
            CreateBulletTracer((Vector2)result["position"]);


            //Create a hit effect using the normal of the hit location



        }
        else
        {
            CreateBulletTracer(firePoint.GlobalPosition + (GlobalTransform.X * GetViewportRect().Size.X + new Vector2(0, spread)));
        }
        //Even with no contact do these things
        base.Shoot(shootDirection);

    }
}
