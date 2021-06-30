using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public GameObject PlayerSpawnPoint;
    public GameObject PlayerPrefab;

    public GameObject DamageDigitPrefab;

    private PawnAction _playerPawnAction;

    private static BattleSystem BATTLE_SYS_INSTANCE = null;

    private void Awake()
    {
        _playerPawnAction = PlayerPrefab.GetComponent<PawnAction>();

        if (BATTLE_SYS_INSTANCE == null)
            BATTLE_SYS_INSTANCE = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(PlayerPrefab, PlayerSpawnPoint.transform.position, PlayerSpawnPoint.transform.rotation);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDamage(Transform spawnPoint,double damage)
    {
        //Vector3 damagePos = spawnPoint.position;
        //damagePos.x += Random.Range(-0.5f, 0.5f);
        //damagePos.y += Random.Range(-0.5f, 0.5f);
        //GameObject damageObject = Instantiate(DamageDigitPrefab, spawnPoint.position, transform.rotation);
        //damageObject.GetComponentInChildren<DamageDigit>().Damage = damage;
    }

    public static BattleSystem GetBattleSystem()
    {
        return BATTLE_SYS_INSTANCE;
    }
}
