using FunctionModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombatWeaponAction : MonoBehaviour
{
    private Animator _animator;
    private bool _canCauseDamage { get; set; } = false;

    private int _startActionOrder = 0;
    private int _actionOrder = 0;
    private int _actionCount = 2;

    private long _lastActionTime = 0;
    private int _interval = 500;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_canCauseDamage) return;

        Debug.Log("造成近战伤害");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    public void CloseCombatBegin()
    {
        _canCauseDamage = true;

        long nowActionTime =DateTimeModule.GetNowTimeStamp();
        if(nowActionTime- _lastActionTime <_interval)
        {
            return;
        }
        else if(nowActionTime - _lastActionTime > 1000)
        {
            _actionOrder = _startActionOrder;
        }

        _lastActionTime = nowActionTime;

        if(_actionOrder== _startActionOrder)
            _animator.SetBool("GoCloseCombat", true);

        Debug.Log($"进行第{_actionOrder}个动作");

        _animator.SetInteger("CloseCombatOrder", _actionOrder++);
        if (_actionOrder == _actionCount)
            _actionOrder = _startActionOrder;


        //StartCoroutine(TimerModule.SetDelayFunc())
        //_animator.SetBool("GoCloseCombat", true);
    }



    public void CloseCombatEnd()
    {
        //_canCauseDamage = false;
        _animator.SetBool("GoCloseCombat", false);

    }
}
