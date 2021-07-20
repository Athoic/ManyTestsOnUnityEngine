using Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnForPlayer : MonoBehaviour
{
    [SerializeField]public long ArmorUnitID { get; set; }

    public PawnData _pawnData { get; private set; }

    private GameObject _playerPawn;
    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayer(GameObject playerPawn)
    {
        _playerPawn = playerPawn;
        _pawnData = _playerPawn.GetComponent<PawnData>();

    }
}
