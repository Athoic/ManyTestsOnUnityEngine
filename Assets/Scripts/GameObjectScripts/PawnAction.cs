using EventArgs.Battle;
using Project.Helper;
using Repository;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunctionModule;

public class PawnAction : MonoBehaviour
{
    [SerializeField] private GameObject _bulletBornPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _jumpForce = 50f;
    [SerializeField] private float _moveSpeed = 50f;
    [SerializeField] private int _jumpPointLayer = 0;

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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _battleEventSystem.WeaponFireEvent += _battleEventSystem_WeaponFireEvent;
        _pawnData = GetComponent<PawnData>();
        _closeCombatWeaponAction = GetComponentInChildren<CloseCombatWeaponAction>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetKeyUp(KeyCode.J))
        {
            //_isFiring = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) && _jumpCount < _totalJumpCount)
        {
            //Debug.Log($"第{_jumpCount}次跳跃");
            _jumpCount++;
            //transform.Translate(transform.position, Space.World);
            _rigidbody.velocity=new Vector2(_rigidbody.velocity.x, _jumpForce);
            //transform.Translate(Vector3.up * _jumpForce * Time.deltaTime, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y- _jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            _closeCombatWeaponAction.CloseCombatBegin();
            //Debug.Log("近身攻击");
        }

    }

    private void FixedUpdate()
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


        float x = Input.GetAxis(_axisInput);
        _rigidbody.velocity = new Vector2(_moveSpeed * x, _rigidbody.velocity.y);
        if (x < 0 && _isFacingRight)
            Filp();
        else if (x > 0 && !_isFacingRight)
            Filp();


    }

    private void _battleEventSystem_WeaponFireEvent(WeaponFireEventArgs args)
    {
        Fire(args.WeaponID);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("撞击到" + collision.gameObject.name);
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

    
}
