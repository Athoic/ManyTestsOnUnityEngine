using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public GameObject PlayerSpawnPoint;
    public GameObject PlayerPrefab;

    private PawnAction _playerPawnAction;

    private void Awake()
    {
        _playerPawnAction = PlayerPrefab.GetComponent<PawnAction>();
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
}
