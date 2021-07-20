using EventArgs.Battle;
using Project.Helper;
using Repository;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunctionModule;
using UnityEngine.UI;
using Define;
using Define.Enum;
using Application.GameSettings;

public class PawnAction : MonoBehaviour
{
    public float JumpForce = 50f;
    public float MoveSpeed = 50f;
    [SerializeField] private GameObject _bulletBornPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private int _jumpPointLayer = 0;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private GameObject _deadMark;

    private BattleSystem _battleSystem;
    private PawnForPlayer _playerPawn;
    private BattleEventSystem _battleEventSystem = BattleEventSystem.GetInstance();
    private LongRangeWeaponRepository _longRangeWeaponRepository = LongRangeWeaponRepository.GetInstance();

    public List<GameObject> RangedWeaponList;
    private PawnData _pawnData;
    private Rigidbody2D _rigidbody;
    private CloseCombatWeaponAction _closeCombatWeaponAction;
    private BoxCollider2D _boxCollider;
    
    private const int _totalJumpCount = 1;
    private int _jumpCount;
    private bool _isTouchingJumpPoint=false;

    //private int _horizentalInput = 0;

    public bool IsFacingRight = true;
    private int _orientationValue
    {
        get
        {
            if (IsFacingRight)
                return 1;
            else
                return -1;
        }
    }

    private GameObject _lockOnTarget = null;
    private GameObject _selectedMark;

    #region 生命周期

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _pawnData = GetComponent<PawnData>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _closeCombatWeaponAction = GetComponentInChildren<CloseCombatWeaponAction>();
        _battleSystem = BattleSystem.GetBattleSystem();
        _playerPawn = _battleSystem.GetComponent<PawnForPlayer>();

        _selectedMark = GameObjectHelper.FindChild(this.gameObject, "SelectedMark");
        _selectedMark.SetActive(false);
        _deadMark.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        _battleEventSystem.WeaponFireEvent += BattleEventSystem_WeaponFireEvent;
        _battleEventSystem.LockOnTargetEvent += BattleEventSystem_LockOnTargetEvent;
        _battleEventSystem.CauseDamageEvent += BattleEventSystem_CauseDamageEvent;
        _battleEventSystem.PawnDeadEvent += BattleEventSystem_PawnDeadEvent;
    }


    private void OnDisable()
    {
        _battleEventSystem.WeaponFireEvent -= BattleEventSystem_WeaponFireEvent;
        _battleEventSystem.LockOnTargetEvent -= BattleEventSystem_LockOnTargetEvent;
        _battleEventSystem.CauseDamageEvent -= BattleEventSystem_CauseDamageEvent;

    }

    // Update is called once per frame
    void Update()
    {
        Update_ControlPawn();

        if (_lockOnTarget != null)
        {
            Vector3 targetPos = _lockOnTarget.transform.position;
            if (targetPos.x < transform.position.x && IsFacingRight)
                Flip();
            else if (targetPos.x > transform.position.x && !IsFacingRight)
                Flip();

        }

    }

    private void FixedUpdate()
    {
        FixedUpdate_ControlPawn();


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("撞击到" + collision.gameObject.name);
    }


    #endregion

    #region 监听自定义事件

    private void BattleEventSystem_WeaponFireEvent(WeaponFireEventArgs args)
    {
        if (args.PawnID != _pawnData.GUID) return;
            Fire(args.WeaponID);
    }

    private void BattleEventSystem_LockOnTargetEvent(LockOnTargetEventArgs eventArgs)
    {
        if (eventArgs.PawnGUID == _pawnData.GUID)
        {
            _selectedMark.SetActive(true);

        }
        else
        {
            _selectedMark.SetActive(false);

        }
    }
    private void BattleEventSystem_CauseDamageEvent(CauseDamageEventArgs args)
    {
        if (args.Target != this.gameObject) return;

        UpdateHealthBar();
    }

    private void BattleEventSystem_PawnDeadEvent(PawnDeadEventArgs eventArgs)
    {
        //死亡的游戏棋子的死亡结算
        if (eventArgs.PawnGUID != _pawnData.GUID) return;

        _deadMark.SetActive(true);
        _boxCollider.isTrigger = true;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = new Vector3(transform.position.x, transform.position.y, 10f);


        if (_battleSystem.PlayerPawnData.TargetGuid == eventArgs.PawnGUID)
        {
            if (GameSettings.IsAutoLockOn)
            {
                if (_battleSystem.PlayerPawnAction.IsFacingRight)
                {
                    if (!_battleSystem.PlayerPawnAction.LockOnNextClosestAliveEnemyPawn(EOrientation.Right))
                        _battleSystem.PlayerPawnAction.CancelLockOn();
                }
                else
                {
                    if (!_battleSystem.PlayerPawnAction.LockOnNextClosestAliveEnemyPawn(EOrientation.Left))
                        _battleSystem.PlayerPawnAction.CancelLockOn();
                }
            }
            else
            {
                _battleSystem.PlayerPawnAction.CancelLockOn();
            }
        }

    }


    #endregion

    #region 业务逻辑

    #region 监听输入
    private void Update_ControlPawn()
    {
        if (gameObject.tag == "Enemy")
            return;

        //跳跃
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce));// .velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            //_isInAir = true;
        }
        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) && _jumpCount < _totalJumpCount)
        //{
        //    //Debug.Log($"第{_jumpCount}次跳跃");
        //    _jumpCount++;
        //    //transform.Translate(transform.position, Space.World);
        //    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        //    //transform.Translate(Vector3.up * _jumpForce * Time.deltaTime, Space.World);
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y - _jumpForce);
        //}

        //近身攻击
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            _closeCombatWeaponAction.CloseCombatBegin();
            //Debug.Log("近身攻击");
        }

        //锁定
        if (Input.GetKeyDown(KeyCode.RightBracket) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            LockOnNextClosestAliveEnemyPawn(EOrientation.Right);
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LockOnNextClosestAliveEnemyPawn(EOrientation.Left);
        }
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            CancelLockOn();
        }


        //if (Input.GetKey(KeyCode.W))
        //{
        //    _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, MoveSpeed);
        //}
    }

    private void FixedUpdate_ControlPawn()
    {
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    if (_isTouchingJumpPoint || _canSecondJump)
        //    {
        //        _rigidbody.AddForce(new Vector2(0, _jumpForce));
        //        if (!_isTouchingJumpPoint)
        //            _canSecondJump = false;
        //    }
        //}

        if (gameObject.tag == "Enemy")
            return;

        float x = Input.GetAxis(SystemValueDefine.AxisInputH);
        _rigidbody.velocity = new Vector2(MoveSpeed * x, _rigidbody.velocity.y);
        if (x < 0 && IsFacingRight && _lockOnTarget==null)
            Flip();
        else if (x > 0 && !IsFacingRight && _lockOnTarget == null)
            Flip();

        //float y= Input.GetAxis(SystemValueDefine.AxisInputV);
        //_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, MoveSpeed * y);
    }

    #endregion
    
    private void Fire(long weaponID)
    {
        //if (!_isFiring) return;

        WeaponDataDO weaponDataDO = _pawnData.GetWeaponDataDO(weaponID);
        if (weaponDataDO.Remain < _longRangeWeaponRepository.GetSingleFireCount(weaponID)) 
            return;

        Timer timer = new Timer(() => 
        {
            Vector3 rawSpawnPos = _bulletBornPoint.transform.position;
            Vector3 bulletSpawnPoint = new Vector3(rawSpawnPos.x, rawSpawnPos.y + Random.Range(-0.2f, 0.2f), rawSpawnPos.z);
            GameObject bullet = Instantiate(_bullet, bulletSpawnPoint, _bulletBornPoint.transform.rotation);
            
            BulletAction bulletAction = bullet.GetComponent<BulletAction>();
            if (_lockOnTarget != null)
            {
                bulletAction.Target = _lockOnTarget.transform.position;
            }
            else
            {
                if (IsFacingRight)
                    bulletAction.Target = Vector3.right;
                else
                    bulletAction.Target = Vector3.left;
            }
            
            BulletData bulletData = bullet.GetComponent<BulletData>();
            bulletData.NumericDamage = _pawnData.GetNumericDamageDO(weaponDataDO);
            bulletData.WeaponType = weaponDataDO.WeaponType;
            bulletData.WeaponFireType = weaponDataDO.WeaponFireType;
            bulletData.Element = weaponDataDO.Element;
        },
        _longRangeWeaponRepository.GetSingleInterval(weaponID),
        weaponDataDO.SingleFireCount);

        timer.StartLoop();

        weaponDataDO.Remain-= weaponDataDO.SingleFireCount;

        WeaponFireSuccessEventArgs args = new WeaponFireSuccessEventArgs();
        args.WeaponID = weaponID;
        args.Remain = weaponDataDO.Remain;
        _battleEventSystem.DispatchWeaponFireSuccessEvent(args);


    }

    private void Flip()
    {
        IsFacingRight = !IsFacingRight;

        Vector3 oldScale = gameObject.transform.localScale;
        float scaleX = oldScale.x * (-1);
        gameObject.transform.localScale = new Vector3(scaleX, oldScale.y, oldScale.z);
    }

    private void UpdateHealthBar()
    {
        _healthBar.transform.localScale = new Vector3(GameValueDefine.HealthBarBaseScale * (float)(_pawnData.HealthPointDO.CurrentHP / _pawnData.HealthPointDO.MaximumHP),
            _healthBar.transform.localScale.y,
            _healthBar.transform.localScale.z);

    }

    #region 攻击目标锁定
    public bool LockOnNextClosestAliveEnemyPawn(EOrientation orientation)
    {
        switch (orientation)
        {
            case EOrientation.Right:
                return LockOnTarget(_battleSystem.GetRightClosestAliveEnemyPawn(_lockOnTarget));
            case EOrientation.Left:
                return LockOnTarget(_battleSystem.GetLeftClosestAliveEnemyPawn(_lockOnTarget));
            default:
                return false;
        }


    }

    public void CancelLockOn()
    {
        _lockOnTarget = null;
        _pawnData.TargetGuid = string.Empty;
        _battleEventSystem.DispatchLockOnTargetEvent(string.Empty);
    }
    
    public bool LockOnTarget(GameObject target)
    {
        if (target == _battleSystem.PlayerPawn)
            return false;

        if (target == null)
        {
            CancelLockOn();
            return false;
        }

        _lockOnTarget = target;
        PawnData pawnData = target.GetComponent<PawnData>();
        _battleEventSystem.DispatchLockOnTargetEvent(pawnData.GUID);

        if (target.transform.position.x > transform.position.x && !IsFacingRight)
            Flip();
        else if (target.transform.position.x < transform.position.x && IsFacingRight)
            Flip();

        _pawnData.TargetGuid = pawnData.GUID;

        return true;

    }
    
    public void SwitchLockOnState()
    {
        if (_battleSystem.PlayerPawnData.TargetGuid == this._pawnData.GUID)
            _battleSystem.PlayerPawnAction.CancelLockOn();
        else
        {
            _battleSystem.PlayerPawnAction.LockOnTarget(this.gameObject);
        }
    }
    #endregion
    #endregion
}
