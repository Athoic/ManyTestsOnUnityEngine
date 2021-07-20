using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Define.Enum
{
    /// <summary>
    /// 射击武器发射类型
    /// </summary>
    public enum EWeaponFireType
    {
        Undefined,

        /// <summary>
        /// 单发
        /// </summary>
        SingleShot,

        /// <summary>
        /// 连发
        /// </summary>
        Repeater,

        /// <summary>
        /// 爆射
        /// </summary>
        Spray,

        /// <summary>
        /// 霰弹
        /// </summary>
        Shotgun,

        /// <summary>
        /// 浮游
        /// </summary>
        Funnel,
    }

    /// <summary>
    /// 武器类型
    /// </summary>
    public enum EWeaponType
    {
        Undefined,

        /// <summary>
        /// 主炮武器
        /// </summary>
        MainArmament,

        /// <summary>
        /// 飞弹武器
        /// </summary>
        Missile,

        /// <summary>
        /// 副炮武器
        /// </summary>
        SecondArtillry,

        /// <summary>
        /// 格斗武器
        /// </summary>
        CloseCombat,
    }

    /// <summary>
    /// 武器元素属性
    /// </summary>
    public enum EElement
    {
        Undefined,

        /// <summary>
        /// 普通
        /// </summary>
        Normal,

        /// <summary>
        /// 火属性
        /// </summary>
        Flame,

        /// <summary>
        /// 冰属性
        /// </summary>
        Ice,

        /// <summary>
        /// 电属性
        /// </summary>
        Electric,

        /// <summary>
        /// 腐蚀属性
        /// </summary>
        Corrosion,
    }

    public enum ECloseCombatDamageType
    {
        Undefined,

        /// <summary>
        /// 钝击伤害
        /// </summary>
        Blunt,

        /// <summary>
        /// 刺击伤害
        /// </summary>
        Sharp,
    }

    /// <summary>
    /// 方位
    /// </summary>
    public enum EOrientation
    {
        Undefined,
        Top,
        Right,
        Botton,
        Left,
    }
}
