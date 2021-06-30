using Repository;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    [SerializeField] private GameObject _listContent;
    [SerializeField] private GameObject _listItem;
    [SerializeField] private GameObject _playerPawn;

    private LongRangeWeaponRepository _longRangeWeaponRepository = LongRangeWeaponRepository.GetInstance();

    private const string _playerPawnName = "GameObjectSelf(Clone)";
    private PawnData _playerPawnData;
    // Start is called before the first frame update
    void Start()
    {
        _playerPawnData =GameObject.Find(_playerPawnName).GetComponent<PawnData>();
        List<long> weaponIDs = _playerPawnData.LongRangeWeaponIDs;
        CleanList();
        for(int i=0,count= weaponIDs.Count; i < count; i++)
        {
            WeaponListItem newItem = Instantiate(_listItem, _listContent.transform).GetComponent<WeaponListItem>();
            newItem.WeaponID = weaponIDs[i];
            newItem.Index = i;
            newItem.WeaponName = _longRangeWeaponRepository.GetName(weaponIDs[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CleanList()
    {
        Transform _listContengTrans = _listContent.transform;
        for(int i=0,count= _listContengTrans.childCount; i<count; i++)
        {
            Destroy(_listContengTrans.GetChild(i).gameObject);
        }
    }

}
