using Godot;
using System;

public partial class Spear : Weapon
{

    float Damage;

    //Play the attack animation

    public override void Fire()
    {
        animator.Play("Attack");

        float multiplier = Mathf.Max(CurrentCharge%25, 1.0f);

        Damage = stats.BaseDamage * multiplier;

        //The final damage should be the base damage multiplied by a value, derived from the % of CurrentCharge.




        base.Fire();
    }

    public override void Aim()
    {
        base.Aim();

    }


}
