using CustomedTest.DataObjects;
using EventArgs.Battle;
using FunctionModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombatWeaponAction : MonoBehaviour
{
    private BattleEventSystem _battleEventSystem = BattleEventSystem.GetInstance();
    
    private Animator _animator;

    
    private bool _canCauseDamage { get; set; } = false;

    private int _startActionOrder = 0;
    private int _actionOrder = 0;
    private int _actionCount = 2;

    private long _lastActionTime = 0;
    private const int _interval = 500;

    private bool _isCloseCombatAnimClosed = true;


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_canCauseDamage) return;

        NumericDamageDO numericDamage = new NumericDamageDO();
        numericDamage.TotalDamage = 10;
        CauseDamageEventArgs e = new CauseDamageEventArgs();
        e.Target = collision.gameObject;
        e.DamageDO = numericDamage;
        _battleEventSystem.DispatchCauseDamageEvent(e);
        //Debug.Log("造成近战伤害");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    public void CloseCombatBegin()
    {
        if (!_isCloseCombatAnimClosed && _actionOrder == _actionCount)
            return;

        long nowActionTime =DateTimeModule.GetNowTimeStamp();
        
        if (nowActionTime - _lastActionTime > 1000)
        {
            _actionOrder = _startActionOrder;
        }


        _lastActionTime = nowActionTime;

        if (_actionOrder == _startActionOrder)
            _animator.SetBool("GoCloseCombat", true);

        Debug.Log($"进行第{_actionOrder}个动作");

        _animator.SetInteger("CloseCombatOrder", _actionOrder++);

        _isCloseCombatAnimClosed = false;
    }


    public void EnableDamageDetective()
    {
        _canCauseDamage = true;
    }

    public void DisableDamageDetective()
    {
        _canCauseDamage = false;
    }

    public void CloseCombatEnd()
    {
        if (_isCloseCombatAnimClosed) return;

        _animator.SetBool("GoCloseCombat", false);
        _actionOrder = _startActionOrder;

        _isCloseCombatAnimClosed = true;
    }
}
