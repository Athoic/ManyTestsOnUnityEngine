using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().fillAmount = 0f;

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        
    }

    public void IncreaseProgress()
    {
        Debug.Log("收到点击事件");
        GetComponent<Image>().fillAmount += 0.1f;
    }
}
