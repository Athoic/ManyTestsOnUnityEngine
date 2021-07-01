using EventArgs.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunctionModule;

public class BattleEventSystem
{
    #region 武器开火

    public delegate void WeaponFireEventHandler(WeaponFireEventArgs args);
    public event WeaponFireEventHandler WeaponFireEvent;

    public void DispatchWeaponFireEvent(WeaponFireEventArgs args)
    {
        WeaponFireEvent.Invoke(args);
    }

    #endregion

    #region 武器开火成功

    public delegate void WeaponFireSuccessEventHandler(WeaponFireSuccessEventArgs args);
    public event WeaponFireSuccessEventHandler WeaponFireSuccessEvent;

    public void DispatchWeaponFireSuccessEvent(WeaponFireSuccessEventArgs args)
    {
        WeaponFireSuccessEvent.Invoke(args);
    }

    #endregion

    #region 造成伤害

    public delegate void CauseDamageEventHandler(CauseDamageEventArgs args);
    public event CauseDamageEventHandler CauseDamageEvent;

    public void DispatchCauseDamageEvent(CauseDamageEventArgs args)
    {
        CauseDamageEvent.Invoke(args);
    }


    #endregion

    #region 锁定攻击目标

    public delegate void LockOnTargetEventHandler(LockOnTargetEventArgs eventArgs);
    public event LockOnTargetEventHandler LockOnTargetEvent;

    public void DispatchLockOnTargetEvent(string pawnGUID)
    {
        LockOnTargetEventArgs e = new LockOnTargetEventArgs();
        e.PawnGUID = pawnGUID;
        LockOnTargetEvent.Invoke(e);
    }

    #endregion

    #region 限制此类为单例

    private BattleEventSystem() { }

    private static BattleEventSystem _eventSystem;

    public static BattleEventSystem GetInstance()
    {
        if (_eventSystem == null)
            _eventSystem = new BattleEventSystem();

        return _eventSystem;
    }

    #endregion
}


