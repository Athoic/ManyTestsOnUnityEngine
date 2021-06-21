using CustomedTest.DataObjects;
using FunctionModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldData : MonoBehaviour
{
    private CircleCollider2D _shieldCollider;

    private NumericShieldDO _numericShield;

    private void Awake()
    {
        _shieldCollider = GetComponent<CircleCollider2D>();

        _numericShield = new NumericShieldDO(50);
    }

    // Start is called before the first frame update
    void Start()
    {
        EnableShield(50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("判定护盾");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_numericShield.IsAvailable)
            return;

        if (collision.gameObject.tag.Equals("Bullet"))
        {

            _numericShield.ConsumeShield(collision.gameObject.GetComponent<BulletData>().NumericDamage.BaseValue);
            if (!_numericShield.IsAvailable)
            {
                DisableShield();
            }

            Destroy(collision.gameObject);
        }

    }

    public void EnableShield(float duration)
    {
        Debug.Log("护盾生效");

        _numericShield.IsAvailable = true;
        _numericShield.Durability = duration;
        gameObject.SetActive(true);

        //StartCoroutine(TimerModule.SetDelayFunc(DisableShield, duration));
        //TimerModule.SetDelayFunc(DisableShield, 2f);
    }

    private void DisableShield()
    {
        Debug.Log("护盾失效");
        gameObject.SetActive(false);
    }
}
