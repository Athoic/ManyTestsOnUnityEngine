using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomedTest.Enums
{
    #region 枚举

    public enum EDamageValueType
    {
        Numeric,
        Percent,
    }

    public enum EDamageEffectType
    {
        Undefined,
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
