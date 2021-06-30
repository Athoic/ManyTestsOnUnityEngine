using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Helper
{
    public class DamageHelper
    {
        public static double CalculateShotWeaponDamage(long baseDamage, float generalBonus, float shotWeaponBonus, 
            float weaponTypeBonus, float WeaponFireTypeBonus, float ElemetFireTypeBonus, float finalDamageBonus)
        {
            return baseDamage * (1 + generalBonus) * (1 + shotWeaponBonus) * (1 + weaponTypeBonus) * (1 + WeaponFireTypeBonus) *
                (1 + ElemetFireTypeBonus) * (1 + finalDamageBonus);
        }
    }
}
