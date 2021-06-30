using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomedTest.DataObjects
{
    public class DamageBonusAndReductionsDO
    {
        #region 通用的伤害加成与减免

        /// <summary>
        /// 基础伤害加成
        /// </summary>
        public float BaseDamageBonus { get; set; } = 0;
        /// <summary>
        /// 基础伤害减免
        /// </summary>
        public float BaseDamageReduction { get; set; } = 0;

        /// <summary>
        /// 最终伤害加成
        /// </summary>
        public float FinalDamageBonus { get; set; } = 0;
        /// <summary>
        /// 最终属性伤害减免
        /// </summary>
        public float FinalDamageReduction { get; set; } = 0;

        /// <summary>
        /// 近战伤害加成
        /// </summary>
        public float CloseCombatDamageBonus { get; set; } = 0;
        /// <summary>
        /// 近战伤害减免
        /// </summary>
        public float CloseCombatDamageReduction { get; set; } = 0;

        /// <summary>
        /// 射击武器伤害加成
        /// </summary>
        public float ShootDamageBonus { get; set; } = 0;
        /// <summary>
        /// 射击武器伤害减免
        /// </summary>
        public float ShootDamageReduction { get; set; } = 0;

        #endregion

        #region 属性伤害的加成与减免

        /// <summary>
        /// 无属性伤害加成
        /// </summary>
        public float NormalDamageBonus { get; set; }
        /// <summary>
        /// 无属性伤害减免
        /// </summary>
        public float NormalDamageReduction { get; set; }

        /// <summary>
        /// 火属性伤害加成
        /// </summary>
        public float FlameDamageBonus { get; set; }
        /// <summary>
        /// 火属性伤害减免
        /// </summary>
        public float FlameDamageReduction { get; set; }

        /// <summary>
        /// 冰属性伤害加成
        /// </summary>
        public float IceDamageBonus { get; set; }
        /// <summary>
        /// 冰属性伤害减免
        /// </summary>
        public float IceDamageReduction { get; set; }

        /// <summary>
        /// 电属性伤害加成
        /// </summary>
        public float ElectricDamageBonus { get; set; }
        /// <summary>
        /// 电属性伤害减免
        /// </summary>
        public float ElectricDamageReduction { get; set; }

        /// <summary>
        /// 腐蚀属性伤害加成
        /// </summary>
        public float CorrosionDamageBonus { get; set; }
        /// <summary>
        /// 腐蚀属性伤害减免
        /// </summary>
        public float CorrosionDamageReduction { get; set; }

        #endregion

        #region 武器类型伤害的加成与减免

        /// <summary>
        /// 主炮伤害加成
        /// </summary>
        public float MainArmamentDamageBonus { get; set; } = 0;
        /// <summary>
        /// 主炮伤害减免
        /// </summary>
        public float MainArmamentDamageReduction { get; set; } = 0;

        /// <summary>
        /// 飞弹伤害加成
        /// </summary>
        public float MissileDamageBonus { get; set; } = 0;
        /// <summary>
        /// 飞弹伤害减免
        /// </summary>
        public float MissileDamageReduction { get; set; } = 0;

        /// <summary>
        /// 副炮伤害加成
        /// </summary>
        public float SecondArtilleryDamageBonus { get; set; } = 0;
        /// <summary>
        /// 副炮伤害减免
        /// </summary>
        public float SecondArtilleryDamageReduction { get; set; } = 0;

        /// <summary>
        /// 近战武器伤害加成
        /// </summary>
        public float CloseCombatWeaponDamageBonus { get; set; } = 0;
        /// <summary>
        /// 近战武器伤害减免
        /// </summary>
        public float CloseCombatWeaponDamageReduction { get; set; } = 0;


        #endregion

        #region 武器发射类型的伤害加成与减免

        /// <summary>
        /// 连发武器伤害加成
        /// </summary>
        public float RepeaterDamageBonus { get; set; } = 0;
        /// <summary>
        /// 连发武器伤害减免
        /// </summary>
        public float RepeaterDamageReduction { get; set; } = 0;

        /// <summary>
        /// 单发武器伤害加成
        /// </summary>
        public float SingleShotDamageBonus { get; set; } = 0;
        /// <summary>
        /// 单发武器伤害减免
        /// </summary>
        public float SingleShotDamageReduction { get; set; } = 0;

        /// <summary>
        /// 爆射武器伤害加成
        /// </summary>
        public float SprayDamageBonus { get; set; } = 0;
        /// <summary>
        /// 爆射武器伤害减免
        /// </summary>
        public float SprayDamageReduction { get; set; } = 0;

        /// <summary>
        /// 霰弹武器伤害加成
        /// </summary>
        public float ShotGunDamageBonus { get; set; } = 0;
        /// <summary>
        /// 霰弹武器伤害减免
        /// </summary>
        public float ShotGunDamageReduction { get; set; } = 0;


        #endregion
    }

}
