using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomedTest.Enums
{
    #region 枚举

    public enum EElement
    {
        Normal,
        Flame,
        Ice,
        Electric,
        Poison,
    }

    public enum EDamageValueType
    {
        Numeric,
        Percent,
    }

    public enum EDamageEffectType
    {
        None,
        Fixed,
        IgnoreNormalShield,
        IgnoreAbsoluteShield,
    }

    public enum EPercentDamageType
    {
        CurrentHP,
        MaximumHP,
        lostHP,
    }

    public enum EShieldType 
    {
        Numeric,
        Absolute,
    }


    #endregion

}
