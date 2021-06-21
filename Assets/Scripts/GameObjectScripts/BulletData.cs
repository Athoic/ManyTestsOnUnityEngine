using CustomedTest.DataObjects;
using CustomedTest.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour
{

    [HideInInspector]public NumericDamageDO NumericDamage;

    private void Awake()
    {
        NumericDamage = new NumericDamageDO()
        {
            BaseValue = 20,
            Element = EElement.Normal,
        };
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
