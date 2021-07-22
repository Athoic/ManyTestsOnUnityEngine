using Define.Enum;
using Define.StringKey;
using EventArgs.Battle;
using FunctionModule;
using Project.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBulletAction : MonoBehaviour
{
    [SerializeField] private GameObject _beamHitEffect;

    /// <summary>
    /// 子弹攻击的目标
    /// </summary>
    public GameObject Target { get; private set; }


    /// <summary>
    /// 子弹来源
    /// </summary>
    public GameObject SourcePawn { get; private set; }

    private Vector3 _targetPos;
    /// <summary>
    /// 子弹攻击的目标
    /// </summary>
    public Vector3 TargetPos
    {
        get
        {
            if (Target == null)
                return _targetPos;
            else
                return Target.transform.position;
        }
        set
        {
            if (Target == null)
                _targetPos = value;
        }
    }

    private BattleSystem _battleSystem = BattleSystem.GetInstance();
    private BattleEventSystem _battleEventSystem = BattleEventSystem.GetInstance();

    private BulletData _bulletData;

    private Dictionary<string, GameObject> _touchedEnemies = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> _hitEffects = new Dictionary<string, GameObject>();
    private UnityTimer _unityTimer;

    #region 生命周期

    private void Awake()
    {
        _bulletData = GetComponent<BulletData>();

        _unityTimer= new UnityTimer(() =>
            {
                var enumerator = _touchedEnemies.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    CauseDamageEventArgs eventArgs = new CauseDamageEventArgs()
                    {
                        Target = enumerator.Current.Value,
                        DamageDO = _bulletData.NumericDamage,
                    };
                    _battleEventSystem.DispatchCauseDamageEvent(eventArgs);
                }
            },
            MathHelper.GetIntervalByCountPerSecond(_bulletData.CountPerSecond));

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
            transform.rotation = MathHelper.GetQuaternion(TargetPos, SourcePawn.transform.position);
        else
            transform.rotation = MathHelper.GetQuaternion(TargetPos);

        UpdateHitEffect();

    }

    private void OnEnable()
    {
        _battleEventSystem.LockOnTargetEvent += BattleEventSystem_LockOnTargetEvent;
    }

    private void OnDisable()
    {
        _battleEventSystem.LockOnTargetEvent -= BattleEventSystem_LockOnTargetEvent;

    }
    #endregion

    #region 监听自定义事件

    private void BattleEventSystem_LockOnTargetEvent(LockOnTargetEventArgs eventArgs)
    {
        if (string.IsNullOrEmpty(eventArgs.PawnGUID)) return;

        Target = _battleSystem.AliveBattlePawn[eventArgs.PawnGUID];
    }


    #endregion

    #region 碰撞检测

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject touchedPawn = collision.gameObject;
        if (touchedPawn.tag==TagDefines.Enemy)
        {
            string guid = touchedPawn.GetComponent<PawnData>().GUID;
            if (_touchedEnemies.ContainsKey(guid))
                return;

            _touchedEnemies.Add(guid, touchedPawn);

            GameObject hitEffect = Instantiate(_beamHitEffect, touchedPawn.transform.position, transform.rotation);
            _hitEffects.Add(guid, hitEffect);
        }

        if (_unityTimer.TimerState == ETimerState.Ready)
            _unityTimer.StartLoop();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject touchedPawn = collision.gameObject;
        if (touchedPawn.tag == TagDefines.Enemy)
        {
            string guid = touchedPawn.GetComponent<PawnData>().GUID;
            if (_touchedEnemies.ContainsKey(guid))
                return;

            _touchedEnemies.Add(guid, touchedPawn);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject touchedPawn = collision.gameObject;
        if (touchedPawn.tag == TagDefines.Enemy)
        {
            string guid = touchedPawn.GetComponent<PawnData>().GUID;
            if (!_touchedEnemies.ContainsKey(guid))
                return;
            _touchedEnemies.Remove(guid);

            Destroy(_hitEffects[guid]);
            _hitEffects.Remove(guid);
        }

    }
    #endregion

    #region 业务逻辑

    private void UpdateHitEffect()
    {
        var enumrator = _hitEffects.GetEnumerator();
        while (enumrator.MoveNext())
        {
            string key = enumrator.Current.Key;
            if (!_battleSystem.AliveBattlePawn.ContainsKey(key))
                return;

            Transform hitTarget = _battleSystem.AliveBattlePawn[key].transform;
            Transform hitEffect = enumrator.Current.Value.transform;

            hitEffect.position = hitTarget.position;
            hitEffect.rotation = transform.rotation;
        }

    }

    public void Init(GameObject target, GameObject source)
    {
        Target = target;
        SourcePawn = source;
    }

    public void Init(EOrientation orientation, GameObject source)
    {
        switch (orientation)
        {
            case EOrientation.Left:
                TargetPos = Vector3.left;
                break;
            case EOrientation.Right:
                TargetPos = Vector3.right;
                break;
            case EOrientation.Up:
                TargetPos = Vector3.up;
                break;
            case EOrientation.Down:
                TargetPos = Vector3.down;
                break;
        }

        SourcePawn = source;
    }

    #endregion
}
