using EventArgs.Battle;
using Project.Helper;
using Repository;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunctionModule;
using UnityEngine.UI;

public class PawnAction : MonoBehaviour
{
    [SerializeField] private GameObject _bulletBornPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _jumpForce = 50f;
    [SerializeField] private float _moveSpeed = 50f;
    [SerializeField] private int _jumpPointLayer = 0;

    private BattleSystem _battleSystem;
    private BattleEventSystem _battleEventSystem = BattleEventSystem.GetInstance();
    private LongRangeWeaponRepository _longRangeWeaponRepository = LongRangeWeaponRepository.GetInstance();

    public List<GameObject> RangedWeaponList;
    private PawnData _pawnData;
    private Rigidbody2D _rigidbody;
    private CloseCombatWeaponAction _closeCombatWeaponAction;
    
    private const int _totalJumpCount = 1;
    private int _jumpCount;
    private bool _isTouchingJumpPoint=false;

    private const string _axisInput = "Horizontal";
    //private int _horizentalInput = 0;

    private bool _isFacingRight = true;
    private int _orientationValue
    {
        get
        {
            if (_isFacingRight)
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
        _closeCombatWeaponAction = GetComponentInChildren<CloseCombatWeaponAction>();
        _battleSystem = BattleSystem.GetBattleSystem();

        _selectedMark = GameObjectHelper.FindChild(this.gameObject, "SelectedMark");
        _selectedMark.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _battleEventSystem.WeaponFireEvent += BattleEventSystem_WeaponFireEvent;
        _battleEventSystem.LockOnTargetEvent += BattleEventSystem_LockOnTargetEvent;
    }


    private void OnDisable()
    {
        _battleEventSystem.WeaponFireEvent -= BattleEventSystem_WeaponFireEvent;
    }

    // Update is called once per frame
    void Update()
    {
        Update_ControlPawn();
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
        if (eventArgs.PawnGUID != _pawnData.GUID)
        {
            _selectedMark.SetActive(false);
        }
        else
        {
            _selectedMark.SetActive(true);
        }
    }


    #endregion

    #region 业务逻辑

    private void Update_ControlPawn()
    {
        if (gameObject.tag == "Enemy")
            return;

        _isTouchingJumpPoint = _rigidbody.IsTouchingLayers(_jumpPointLayer);
        if (_isTouchingJumpPoint)
        {
            _jumpCount = 0;
        }

        if (InputHelper.OnGameMouseBtnDown(0) || Input.GetKeyDown(KeyCode.J))
        {
            //_isFiring = true;
            //Fire();
        }

        if(Input.GetKeyDown(KeyCode.A))

        if (Input.GetKeyUp(KeyCode.J))
        {
            //_isFiring = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) && _jumpCount < _totalJumpCount)
        {
            //Debug.Log($"第{_jumpCount}次跳跃");
            _jumpCount++;
            //transform.Translate(transform.position, Space.World);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            //transform.Translate(Vector3.up * _jumpForce * Time.deltaTime, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y - _jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            _closeCombatWeaponAction.CloseCombatBegin();
            //Debug.Log("近身攻击");
        }

        if (Input.GetKeyDown(KeyCode.RightBracket) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            _lockOnTarget=_battleSystem.GetNextClosestAliveEnemyPawn(_lockOnTarget);
            if (_lockOnTarget != null)
            {
                PawnData pawnData = _lockOnTarget.GetComponent<PawnData>();
                _battleEventSystem.DispatchLockOnTargetEvent(pawnData.GUID);
                
                if (_lockOnTarget.transform.position.x > transform.position.x && !_isFacingRight)
                    Filp();

            }
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            _lockOnTarget = _battleSystem.GetLastClosestAliveEnemyPawn(_lockOnTarget);
            if (_lockOnTarget != null)
            {
                PawnData pawnData = _lockOnTarget.GetComponent<PawnData>();
                _battleEventSystem.DispatchLockOnTargetEvent(pawnData.GUID);
                
                if (_lockOnTarget.transform.position.x < transform.position.x && _isFacingRight)
                    Filp();

            }
        }
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            _lockOnTarget = null;
            _battleEventSystem.DispatchLockOnTargetEvent(string.Empty);

        }
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

        float x = Input.GetAxis(_axisInput);
        _rigidbody.velocity = new Vector2(_moveSpeed * x, _rigidbody.velocity.y);
        if (x < 0 && _isFacingRight && _lockOnTarget==null)
            Filp();
        else if (x > 0 && !_isFacingRight && _lockOnTarget == null)
            Filp();

    }

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
            bulletAction.HorizentalDirect = _orientationValue;
            
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

    private void Filp()
    {
        _isFacingRight = !_isFacingRight;

        Vector3 oldScale = gameObject.transform.localScale;
        float scaleX = oldScale.x * (-1);
        gameObject.transform.localScale = new Vector3(scaleX, oldScale.y, oldScale.z);
    }

    #endregion
}
