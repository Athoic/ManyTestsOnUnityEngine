using CustomedTest.DataObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    [SerializeField]public float MoveSpeed=2;


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

    private void FixedUpdate()
    {
        transform.position += transform.right * MoveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("子弹撞击到：" + collision.gameObject.tag);

        if(collision.gameObject.tag=="Enemy")
            Destroy(gameObject);

    }
}
