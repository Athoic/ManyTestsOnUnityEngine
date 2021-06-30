using CustomedTest.DataObjects;
using CustomedTest.Enums;
using Define.Enum;
using EventArgs.Battle;
using Project.Helper;
using Repository;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PawnData : MonoBehaviour
{
    private BattleEventSystem _battleEventSystem = BattleEventSystem.GetInstance();
    private LongRangeWeaponRepository _longRangeWeaponRepository = LongRangeWeaponRepository.GetInstance();
    private ArmorUnitRepository _armorUnitRepository = ArmorUnitRepository.GetInstance();

    public long ArmorUnitID;

    private HealthPointDO healthPointDO;
    private DamageBonusAndReductionsDO _damageBonusAndReductionsDO = new DamageBonusAndReductionsDO();

    public List<long> LongRangeWeaponIDs { get; private set; }
    public List<long> CloseCombatWeaponIDs { get; private set; }
    private Dictionary<long, WeaponDataDO> _weaponMap = new Dictionary<long, WeaponDataDO>();

    private Dictionary<EElement, float> _elementDamageBonusMap = new Dictionary<EElement, float>();
    private Dictionary<EWeaponType, float> _weaponTypeDamageBonusMap = new Dictionary<EWeaponType, float>();
    private Dictionary<EWeaponFireType, float> _weaponFireTypeDamageBonusMap = new Dictionary<EWeaponFireType, float>();

    #region 生命周期

    private void Awake()
    {
        healthPointDO = new HealthPointDO(100);

        LongRangeWeaponIDs = _armorUnitRepository.GetLongRangeWeaponIDs(ArmorUnitID);
        for (int i = 0, count = LongRangeWeaponIDs.Count; i < count; i++)
        {
            var config = _longRangeWeaponRepository.GetByPK(LongRangeWeaponIDs[i]);
            WeaponDataDO weaponDataDO = new WeaponDataDO();
            weaponDataDO.ID = config.id;
            weaponDataDO.Name = config.name;
            weaponDataDO.AmmoDamage = config.single_damage;
            weaponDataDO.SingleFireCount = config.single_amount;
            weaponDataDO.Range = config.range;
            weaponDataDO.ReloadTime = config.fill_time;
            weaponDataDO.Capacity = config.capacity;
            weaponDataDO.Remain = config.capacity;
            weaponDataDO.Element = (EElement)config.element;
            weaponDataDO.WeaponType = (EWeaponType)config.weapon_type;
            weaponDataDO.WeaponFireType = (EWeaponFireType)config.fire_type;

            _weaponMap.Add(config.id, weaponDataDO);
        }

        CloseCombatWeaponIDs = _armorUnitRepository.GetCloseCombatWeaponIDs(ArmorUnitID);
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (LongRangeWeaponIDs == null)
        {
            Debug.Log("错误的ArmorID：" + ArmorUnitID);
        }

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

        BattleSystem.GetBattleSystem().ShowDamage(this.gameObject.transform, args.DamageDO.TotalDamage);
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

    public float GetDamageBonusByElemet(EElement element)
    {
        if (_elementDamageBonusMap.ContainsKey(element))
            return _elementDamageBonusMap[element];
        else
            return 0;
    }
    public void SetDamageBonusByElemet(EElement element,float value)
    {
        if (_elementDamageBonusMap.ContainsKey(element))
            _elementDamageBonusMap[element] = value;
        else
            _elementDamageBonusMap.Add(element, value);
    }

    public float GetDamageBonusByWeaponType(EWeaponType weaponType)
    {
        if (_weaponTypeDamageBonusMap.ContainsKey(weaponType))
            return _weaponTypeDamageBonusMap[weaponType];
        else
            return 0;
    }
    public void SetDamageBonusByWeaponType(EWeaponType weaponType, float value)
    {
        if (_weaponTypeDamageBonusMap.ContainsKey(weaponType))
            _weaponTypeDamageBonusMap[weaponType] = value;
        else
            _weaponTypeDamageBonusMap.Add(weaponType, value);
    }

    public float GetDamageBonusByWeaponFireType(EWeaponFireType weaponFireType)
    {
        if (_weaponFireTypeDamageBonusMap.ContainsKey(weaponFireType))
            return _weaponFireTypeDamageBonusMap[weaponFireType];
        else
            return 0;

    }
    public void SetDamageBonusByWeaponFireType(EWeaponFireType weaponFireType, float value)
    {
        if (_weaponFireTypeDamageBonusMap.ContainsKey(weaponFireType))
            _weaponFireTypeDamageBonusMap[weaponFireType] = value;
        else
            _weaponFireTypeDamageBonusMap.Add(weaponFireType, value);
    }

    public NumericDamageDO GetNumericDamageDO(WeaponDataDO weaponData)
    {
        NumericDamageDO numericDamage = new NumericDamageDO();
        numericDamage.BaseValue = weaponData.AmmoDamage;
        numericDamage.Element = weaponData.Element;
        numericDamage.EffectType = EDamageEffectType.None;
        numericDamage.TotalDamage = DamageHelper.CalculateShotWeaponDamage(
            weaponData.AmmoDamage,
            _damageBonusAndReductionsDO.BaseDamageBonus,
            _damageBonusAndReductionsDO.ShotGunDamageBonus,
            GetDamageBonusByWeaponType(weaponData.WeaponType),
            GetDamageBonusByWeaponFireType(weaponData.WeaponFireType),
            GetDamageBonusByElemet(weaponData.Element),
            _damageBonusAndReductionsDO.FinalDamageBonus);
        return numericDamage;
    }

    #endregion

}

public class WeaponDataDO
{
    public long ID { get; set; }

    public string Name { get; set; }

    public int AmmoDamage { get; set; }

    public float Range { get; set; }

    public int SingleFireCount { get; set; }

    public float ReloadTime { get; set; }

    public int Capacity { get; set; }

    public int Remain { get; set; }

    public EElement Element { get; set; }

    public EWeaponType WeaponType { get; set; }

    public EWeaponFireType WeaponFireType { get; set; }
}
