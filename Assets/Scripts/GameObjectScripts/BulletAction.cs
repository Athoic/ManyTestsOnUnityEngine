using CustomedTest.DataObjects;
using Define.Enum;
using EventArgs.Battle;
using Project.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    public float MoveSpeed=2;
    public float ScatteringAngle = 30;

    [HideInInspector] public Vector3 Target;

    private BattleEventSystem _battleEventSystem = BattleEventSystem.GetInstance();

    private Rigidbody2D _bulletRigidBody;
    private BulletData _bulletData;

    private Vector3 _direct;

    
    #region 生命周期

    private void Awake()
    {
        _bulletRigidBody = GetComponent<Rigidbody2D>();
        _bulletData = GetComponent<BulletData>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitBulletDirection();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _bulletRigidBody.velocity = _direct * MoveSpeed;
        //transform.position += _bulletDirect * MoveSpeed * Time.deltaTime;

    }

    #endregion

    #region 监听碰撞事件
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                CauseDamageEventArgs eventArgs = new CauseDamageEventArgs();
                eventArgs.Target = collision.gameObject;
                eventArgs.DamageDO = _bulletData.NumericDamage;
                _battleEventSystem.DispatchCauseDamageEvent(eventArgs);
                Destroy(gameObject);
                break;
            case "Ground":
                Destroy(gameObject);
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("子弹触及到：" + collision.gameObject.tag);

        //if(collision.gameObject.tag=="Enemy")
        //    Destroy(gameObject);

    }

    #endregion

    #region 业务逻辑

    private void InitBulletDirection()
    {
        if (Target == Vector3.right || Target == Vector3.left)
            _direct = Target;
        else
            _direct = Target - transform.position;

        if (_bulletData.WeaponFireType == EWeaponFireType.Shotgun)
        {
            _direct = MathHelper.RotateVector(_direct, Random.Range(-ScatteringAngle / 2, ScatteringAngle / 2));
        }

        _direct = _direct.normalized;
     }
    #endregion
}
