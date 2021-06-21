using EventArgs.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


