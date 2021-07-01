using AppNode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public GameObject PlayerSpawnPoint;
    public GameObject PlayerPrefab;
    public GameObject EnemySpawnPoint;
    //public GameObject EnemyPrefab;
    public GameObject DamageDigitPrefab;

    private static BattleSystem BATTLE_SYS_INSTANCE = null;

    public Dictionary<string, GameObject> AliveBattlePawn = new Dictionary<string, GameObject>();
    private HashSet<string> _aliveOwnSideBattlePawn = new HashSet<string>();
    private HashSet<string> _aliveEnemyBattlePawn = new HashSet<string>();
    private List<string> PositionSortedAliveBattlePawns = new List<string>();

    private GameObject _playerPawn;

    private void Awake()
    {

        if (BATTLE_SYS_INSTANCE == null)
            BATTLE_SYS_INSTANCE = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //生成玩家
        _playerPawn = Instantiate(PlayerPrefab, PlayerSpawnPoint.transform.position, PlayerSpawnPoint.transform.rotation);
        PawnData playerPawnData = _playerPawn.GetComponent<PawnData>();
        AliveBattlePawn.Add(playerPawnData.GUID, _playerPawn);
        _aliveOwnSideBattlePawn.Add(playerPawnData.GUID);

        GenTestEnemy(EnemySpawnPoint.transform.position + new Vector3(-20, 0, 0), EnemySpawnPoint.transform.rotation);
        GenTestEnemy(EnemySpawnPoint.transform.position + new Vector3(-10, 0, 0), EnemySpawnPoint.transform.rotation);
        GenTestEnemy(EnemySpawnPoint.transform.position + new Vector3(10, 0, 0), EnemySpawnPoint.transform.rotation);
        GenTestEnemy(EnemySpawnPoint.transform.position + new Vector3(20, 0, 0), EnemySpawnPoint.transform.rotation);

        StartCoroutine("FinishLoadBattleSystem");
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
        GameObject damageObject = Instantiate(DamageDigitPrefab, spawnPoint.position, transform.rotation);
        damageObject.GetComponentInChildren<DamageDigit>().Damage = damage;
    }

    private IEnumerator FinishLoadBattleSystem()
    {
        yield return new WaitForSeconds(0.5f);

        AppEventSystem.GetInstance().DispatchBattleSystemLoadedEvent();
    }

    public static BattleSystem GetBattleSystem()
    {
        return BATTLE_SYS_INSTANCE;
    }

    public GameObject GetNextClosestAliveEnemyPawn(GameObject lastTarget)
    {
        if (_aliveEnemyBattlePawn.Count == 0)
            return null;

        float closestRight = float.MaxValue;
        if (lastTarget == null)
            lastTarget = _playerPawn;
        GameObject targetPawn = lastTarget;

        var enumerator= _aliveEnemyBattlePawn.GetEnumerator();
        while (enumerator.MoveNext())
        {
            GameObject pawn = AliveBattlePawn[enumerator.Current];
            
            float difference = pawn.transform.position.x - lastTarget.transform.position.x;
            if (difference > 0 && difference < closestRight)
            {
                closestRight = difference;
                targetPawn = pawn;
            }
        }

        return targetPawn;

    }

    public GameObject GetLastClosestAliveEnemyPawn(GameObject lastTarget)
    {
        if (_aliveEnemyBattlePawn.Count == 0)
            return null;

        float closestLeft = float.MaxValue;
        if (lastTarget == null)
            lastTarget = _playerPawn;
        GameObject targetPawn = lastTarget;

        var enumerator = _aliveEnemyBattlePawn.GetEnumerator();
        while (enumerator.MoveNext())
        {
            GameObject pawn = AliveBattlePawn[enumerator.Current];

            float difference = -(pawn.transform.position.x - lastTarget.transform.position.x);
            if (difference > 0 && difference < closestLeft)
            {
                closestLeft = difference;
                targetPawn = pawn;
            }
        }

        return targetPawn;

    }



    private void GenTestEnemy(Vector3 postion, Quaternion quaternion)
    {
        GameObject enemy = Instantiate(PlayerPrefab, postion, quaternion);
        enemy.tag = "Enemy";
        enemy.layer = 11;
        enemy.name = "Enemy(Clone)";
        PawnData enemyData = enemy.GetComponent<PawnData>();
        enemyData.ArmorUnitID = 2;
        AliveBattlePawn.Add(enemyData.GUID, enemy);
        _aliveEnemyBattlePawn.Add(enemyData.GUID);
    }
}
