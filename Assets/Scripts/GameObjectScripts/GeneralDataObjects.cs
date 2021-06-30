using CustomedTest.Enums;
using Define.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomedTest.DataObjects
{
    public class BaseDamageDO
    {
        public double BaseValue;
        public EElement Element = EElement.Normal;
        public EDamageValueType ValueType;
        public EDamageEffectType EffectType = EDamageEffectType.None;
        public double TotalDamage;
    }


    public class NumericDamageDO : BaseDamageDO
    {
        public NumericDamageDO()
        {
            ValueType = EDamageValueType.Numeric;
        }
    }

    public class PercentDamageDO : BaseDamageDO
    {
        public EPercentDamageType PercentDamageType;
    }

    public class HealthPointDO
    {
        public double MaximumHP { get; private set; }
        public double CurrentHP { get; private set; }
        public double LostHP { get { return MaximumHP - CurrentHP; } }

        public HealthPointDO(double maximumHP)
        {
            MaximumHP = maximumHP;
            CurrentHP = maximumHP;
        }

        public void IncreaseHP(double amount)
        {
            CurrentHP += amount;
            CurrentHP = CurrentHP > MaximumHP ? MaximumHP : CurrentHP;
        }

        public void ReduceHP(double amount)
        {
            CurrentHP -= amount;
            CurrentHP = CurrentHP < 0 ? 0 : CurrentHP;
        }
    }

   public  class DamageReductionAttr
    {
        public float WeaponReduction;
        public float CharactorReduction;
        public float GenralReduction;
        public float FinalReduction;
    }



    public class BaseShieldDO
    {
        public EShieldType ShieldType;
        public bool IsAvailable;

        
    }

    public class NumericShieldDO : BaseShieldDO
    {
        public double Durability { get; set; }

        public NumericShieldDO(double durability)
        {
            Durability = durability;
        }

        public void ConsumeShield(double damage)
        {
            Durability -= damage;
            Durability = Durability >= 0 ? Durability : 0;

            if (Durability == 0)
                IsAvailable = false;
        }
    }

}
