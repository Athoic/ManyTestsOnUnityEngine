using AppNode;
using Define;
using EventArgs.Battle;
using Project.Helper;
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

    private BattleEventSystem _battleEventSystem;

    private static BattleSystem BATTLE_SYS_INSTANCE = null;

    public Dictionary<string, GameObject> AliveBattlePawn = new Dictionary<string, GameObject>();
    private HashSet<string> _aliveOwnSideBattlePawn = new HashSet<string>();
    private HashSet<string> _aliveEnemyBattlePawn = new HashSet<string>();
    private List<string> PositionSortedAliveBattlePawns = new List<string>();

    public GameObject PlayerPawn { get; private set; }
    public PawnData PlayerPawnData { get; private set; }
    public PawnAction PlayerPawnAction { get; private set; }
    public string PlayerGuid { get; private set; }

    private GameObject _camera = null;
    public GameObject Camera
    { 
        get
        {
            if (_camera == null)
                _camera = GameObject.Find(GameValueDefine.MAIN_CAMERA_NAME);

            return _camera;
        }
    }

    #region 生命周期

    private void Awake()
    {

        if (BATTLE_SYS_INSTANCE == null)
            BATTLE_SYS_INSTANCE = this;

        _battleEventSystem = BattleEventSystem.GetInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        //生成玩家
        PlayerPawn = Instantiate(PlayerPrefab, PlayerSpawnPoint.transform.position, PlayerSpawnPoint.transform.rotation);
        PlayerPawnData = PlayerPawn.GetComponent<PawnData>();
        PlayerPawnAction = PlayerPawn.GetComponent<PawnAction>();
        PlayerGuid = PlayerPawnData.GUID;

        AliveBattlePawn.Add(PlayerGuid, PlayerPawn);
        _aliveOwnSideBattlePawn.Add(PlayerGuid);

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

    private void OnEnable()
    {
        _battleEventSystem.PawnDeadEvent += BattleEventSystem_PawnDeadEvent;
    }

    private void OnDisable()
    {
        _battleEventSystem.PawnDeadEvent -= BattleEventSystem_PawnDeadEvent;

    }

    #endregion

    #region 监听自定义事件
    private void BattleEventSystem_PawnDeadEvent(PawnDeadEventArgs eventArgs)
    {
        string pawnGuid = eventArgs.PawnGUID;
        if (AliveBattlePawn.ContainsKey(pawnGuid))
        {
            AliveBattlePawn.Remove(pawnGuid);
        }
        if (_aliveOwnSideBattlePawn.Contains(pawnGuid))
        {
            _aliveOwnSideBattlePawn.Remove(pawnGuid);
        }
        if (_aliveEnemyBattlePawn.Contains(pawnGuid))
        {
            _aliveEnemyBattlePawn.Remove(pawnGuid);
        }
    }

    #endregion

    #region 业务逻辑

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

    public GameObject GetRightClosestAliveEnemyPawn(GameObject lastTarget)
    {
        if (_aliveEnemyBattlePawn.Count == 0)
            return null;

        float closestRight = float.MaxValue;
        if (lastTarget == null || !PawnHelper.IsInCameraView(lastTarget))
            lastTarget = PlayerPawn;
        GameObject targetPawn = lastTarget;

        var enumerator= _aliveEnemyBattlePawn.GetEnumerator();
        while (enumerator.MoveNext())
        {
            GameObject pawn = AliveBattlePawn[enumerator.Current];
            if (!PawnHelper.IsInCameraView(pawn))
                continue;

            float difference = pawn.transform.position.x - lastTarget.transform.position.x;
            if (difference > 0 && difference < closestRight)
            {
                closestRight = difference;
                targetPawn = pawn;
            }
        }

        if (lastTarget == targetPawn && !_aliveEnemyBattlePawn.Contains(lastTarget.GetComponent<PawnData>().GUID))
            return null;

        return targetPawn;

    }

    public GameObject GetLeftClosestAliveEnemyPawn(GameObject lastTarget)
    {
        if (_aliveEnemyBattlePawn.Count == 0)
            return null;

        float closestLeft = float.MaxValue;
        if (lastTarget == null || !PawnHelper.IsInCameraView(lastTarget))
            lastTarget = PlayerPawn;
        GameObject targetPawn = lastTarget;

        var enumerator = _aliveEnemyBattlePawn.GetEnumerator();
        while (enumerator.MoveNext())
        {
            GameObject pawn = AliveBattlePawn[enumerator.Current];
            if (!PawnHelper.IsInCameraView(pawn))
                continue;

            float difference = -(pawn.transform.position.x - lastTarget.transform.position.x);
            if (difference > 0 && difference < closestLeft)
            {
                closestLeft = difference;
                targetPawn = pawn;
            }
        }

        if (lastTarget == targetPawn && !_aliveEnemyBattlePawn.Contains(lastTarget.GetComponent<PawnData>().GUID))
            return null;


        return targetPawn;

    }

    private void GenTestEnemy(Vector3 postion, Quaternion quaternion)
    {
        GameObject enemy = Instantiate(PlayerPrefab, postion, quaternion);
        //GameObject enemyBody = enemy.transform.gameObject;
        enemy.tag = "Enemy";
        enemy.layer = 11;
        enemy.name = "Enemy(Clone)";
        PawnData enemyData = enemy.GetComponent<PawnData>();
        enemyData.ArmorUnitID = 2;
        AliveBattlePawn.Add(enemyData.GUID, enemy);
        _aliveEnemyBattlePawn.Add(enemyData.GUID);
    }

   

    #endregion
}
