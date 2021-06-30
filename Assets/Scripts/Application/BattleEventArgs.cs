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
        public long WeaponID { get; set; }

        public int WeaponIndex { get; set; }
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

}
