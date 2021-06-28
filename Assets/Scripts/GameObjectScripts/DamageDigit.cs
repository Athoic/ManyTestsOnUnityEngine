using FunctionModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDigit : MonoBehaviour
{
    [SerializeField]private float _lastTime = 1;

    public long Damage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Text>().text = Damage.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //StartCoroutine(TimerModule.SetDelayFunc(() =>
        //{
        //    Destroy(this.gameObject);
        //},
        //_lastTime));

        

    }

    public void EndDamageDigitShow()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
