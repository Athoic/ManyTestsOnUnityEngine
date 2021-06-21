using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollList : MonoBehaviour
{
    private ScrollRect _scrollRect;
    private GameObject _playerPawn;
    

    private void Awake()
    {
        _scrollRect=GetComponent<ScrollRect>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("现有"+_scrollRect.content.childCount+"个item");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
