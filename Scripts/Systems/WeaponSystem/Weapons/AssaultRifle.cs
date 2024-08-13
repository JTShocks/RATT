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
        base.Shoot(shootDirection);
    }
}
