using FunctionModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDigitData : MonoBehaviour
{
    [SerializeField] private float _lastTime = 1;
    [SerializeField] private float _horizentalForceThreshold = 40;
    [SerializeField] private float _verticalForceThreshold = 60;
    //[SerializeField] private float _verticalForceThresholdMax = 60;
    //[SerializeField] private float _verticalForceThresholdMin = 40;

    public double Damage { get; private set; }

    private Rigidbody2D _rigidbody;

    #region 生命周期

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Text>().text = Damage.ToString();
        _rigidbody.AddForce(new Vector2(Random.Range(-_horizentalForceThreshold, _horizentalForceThreshold), _verticalForceThreshold));
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

    #endregion

    #region 业务逻辑

    public void Init(double damage)
    {
        Damage = damage;
    }

    public void EndDamageDigitShow()
    {
        Destroy(gameObject);
    }

    #endregion
}
