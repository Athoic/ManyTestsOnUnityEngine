using EventArgs.Battle;
using FunctionModule;
using Project.Helper;
using Repository;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponListItem : MonoBehaviour
{
    private BattleEventSystem _battleEventSystem = BattleEventSystem.GetInstance();
    private weaponRepository _weaponRepository = weaponRepository.GetInstance();

    private const string _playerPawnGameObjectName = "GameObjectSelf(Clone)";
    private PawnData _pawnData;

    private Slider _slider;
    private Text _weaponName;
    private Text _weaponReloadProgressText;
    private Button _button;

    public string WeaponName { get; set; }

    public long WeaponID { get; set; }

    public int Index { get; set; }

    private int _keyCodeIndex { get { return Index + 1+256; } }

    private bool _isReloadingAmmo = false;
    private const float _reloadDelayTime = 0.5f;
    private const string _reloadProgressFormat = "  {0}/{1}";

    #region 生命周期

    private void Awake()
    {
        _weaponName = GameObjectHelper.FindChild(this.gameObject, "WeaponNameText").GetComponent<Text>();
        _weaponReloadProgressText = GameObjectHelper.FindChild(this.gameObject, "WeaponReloadProgressText").GetComponent<Text>();

        _slider = GetComponentInChildren<Slider>();
        _button = GetComponentInChildren<Button>();
        _battleEventSystem.WeaponFireSuccessEvent += OpenFireSuccess;


    }

    // Start is called before the first frame update
    void Start()
    {
        _pawnData = GameObject.Find(_playerPawnGameObjectName).GetComponent<PawnData>();

        _weaponName.text = WeaponName;
        UpdateReloadProgress(_weaponRepository.GetCapacity(WeaponID));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown((KeyCode)_keyCodeIndex))
        {
            OnButtonClick();
        }

        ReloadAmmo_SliderValueChange();
    }

    #endregion

    #region 监听UI事件

    public void OnButtonClick()
    {
        Debug.Log($"点击了第{Index+1}个武器{WeaponName}");

        WeaponFireEventArgs param = new WeaponFireEventArgs()
        {
            WeaponID = this.WeaponID,
        };
        _battleEventSystem.DispatchWeaponFireEvent(param);
    }

    #endregion

    #region 监听自定义事件

    public void OpenFireSuccess(WeaponFireSuccessEventArgs eventArgs)
    {
        if (eventArgs.WeaponID != this.WeaponID) return;

        _isReloadingAmmo = false;

        int capacity=_weaponRepository.GetCapacity(WeaponID);

        float rate = (float)eventArgs.Remain / (float)capacity;
        _slider.value = rate;

        UpdateReloadProgress(eventArgs.Remain);

        if (rate <= 0)
        {
            _button.interactable = false;
        }

        StartCoroutine(Timer.SetDelayFunc(DelayedReload, _reloadDelayTime));
    }

    #endregion

    #region 业务逻辑

    private void DelayedReload()
    {
        _isReloadingAmmo = true;
    }

    private void ReloadAmmo_SliderValueChange()
    {
        if (!_isReloadingAmmo) return;

        if (_slider.value == 1) return;

        int weaponCapacity = _weaponRepository.GetCapacity(WeaponID);
        int oldCount =(int)( _slider.value * weaponCapacity);
        
        float rate = (float)_weaponRepository.GetReloadVelocity(WeaponID) / (float)weaponCapacity;
        _slider.value += Time.deltaTime * rate;
        
        int newCount=(int)(_slider.value * weaponCapacity);

        if (oldCount == newCount) return;
        
        _pawnData.UpdateAmmoCount(WeaponID, newCount);
        UpdateReloadProgress(newCount);
        if (newCount >= _weaponRepository.GetSingleFireCount(WeaponID))
            _button.interactable = true;

    }

    private void UpdateReloadProgress(int newCount)
    {
        _weaponReloadProgressText.text = string.Format(_reloadProgressFormat, newCount, _weaponRepository.GetCapacity(WeaponID));

    }

    #endregion
}

