using CustomedTest.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EventArgs.Battle
{
    

    public class WeaponFireEventArgs
    {
        /// <summary>
        /// 开火单位
        /// </summary>
        public string PawnID { get; set; }

        /// <summary>
        /// 开火的武器
        /// </summary>
        public long WeaponID { get; set; }

    }

    public class WeaponFireSuccessEventArgs
    {
        public long WeaponID { get; set; }

        public int ListItemIndex { get; set; }

        public int Remain { get; set; }

    }

    public class CauseDamageEventArgs
    {
        /// <summary>
        /// 受到伤害的对象
        /// </summary>
        public GameObject Target { get; set; }

        public BaseDamageDO DamageDO { get; set; }
    }

    public class LockOnTargetEventArgs
    {
        public string PawnGUID { get; set; }
    }
}
