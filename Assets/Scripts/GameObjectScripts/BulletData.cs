using CustomedTest.DataObjects;
using CustomedTest.Enums;
using Define.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    /// <summary>
    /// 【光束】每秒判定次数
    /// </summary>
    public int CountPerSecond;

    [HideInInspector] public NumericDamageDO NumericDamage { get; set; }
    [HideInInspector] public EWeaponType WeaponType { get; set; }
    [HideInInspector] public EWeaponFireType WeaponFireType { get; set; }
    [HideInInspector] public EElement Element { get; set; }
    [HideInInspector]public long WeaponID { get; set; }
    
    private void Awake()
    {
        //NumericDamage = new NumericDamageDO()
        //{
        //    BaseValue = 20,
        //    Element = EElement.Normal,
        //};
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
