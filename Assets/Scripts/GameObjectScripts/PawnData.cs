using CustomedTest.DataObjects;
using EventArgs.Battle;
using Repository;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnData : MonoBehaviour
{
    private BattleEventSystem _battleEventSystem = BattleEventSystem.GetInstance();
    private weaponRepository _weaponRepository = weaponRepository.GetInstance();

    private HealthPointDO healthPointDO;

    private Dictionary<long, WeaponDataDO> _weaponMap = null;
    private List<long> weaponIDs = new List<long>() { 1, 2, 3, 4, 5, 6, 7 };

    #region 生命周期

    private void Awake()
    {
        healthPointDO = new HealthPointDO(100);
        _weaponMap = new Dictionary<long, WeaponDataDO>();

        for (int i=0,count= _weaponRepository.Count; i < count; i++)
        {
            var config = _weaponRepository.GetByIndex(i);
            WeaponDataDO weaponDataDO = new WeaponDataDO();
            weaponDataDO.ID = config.id;
            weaponDataDO.Name = config.name;
            weaponDataDO.SingleFireCount = config.single_amount;
            weaponDataDO.Range = config.range;
            weaponDataDO.ReloadTime = config.fill_time;
            weaponDataDO.Capacity = config.capacity;
            weaponDataDO.Remain = config.capacity;

            _weaponMap.Add(config.id, weaponDataDO);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        _battleEventSystem.CauseDamageEvent += BattleEventSystem_CauseDamageEvent;

    }

    private void OnDisable()
    {
        _battleEventSystem.CauseDamageEvent -= BattleEventSystem_CauseDamageEvent;

    }

    #endregion

    #region 监听自定义事件

    private void BattleEventSystem_CauseDamageEvent(CauseDamageEventArgs args)
    {
        if (args.Target != this.gameObject) return;

        BattleSystem.GetBattleSystem().ShowDamage(this.gameObject.transform, args.Damage);
        Debug.Log("敌人受到伤害");
    }


    #endregion

    #region 业务逻辑

    public WeaponDataDO GetWeaponDataDO(long weaponID)
    {
        if (_weaponMap.ContainsKey(weaponID))
            return _weaponMap[weaponID];
        else
            return null;
    }

    public void GotDamage()
    {

    }

    public void UpdateAmmoCount(long weaponID,int newCount)
    {
        WeaponDataDO weaponDataDO = null;
        if (!_weaponMap.TryGetValue(weaponID, out weaponDataDO))
            return;

        weaponDataDO.Remain = newCount;
    }

    #endregion

}

public class WeaponDataDO
{
    public long ID { get; set; }

    public string Name { get; set; }

    public int Type { get; set; }

    public float Range { get; set; }

    public int SingleFireCount { get; set; }

    public float ReloadTime { get; set; }

    public int Capacity { get; set; }

    public int Remain { get; set; }
}
