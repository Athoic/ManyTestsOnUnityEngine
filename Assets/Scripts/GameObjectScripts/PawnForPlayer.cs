using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnForPlayer : MonoBehaviour
{
    [SerializeField]public long ArmorUnitID { get; set; }

    private PawnData _pawnData;

    private void Awake()
    {
        _pawnData = GetComponent<PawnData>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
