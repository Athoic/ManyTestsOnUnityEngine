using CustomedTest.DataObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    [SerializeField]public float MoveSpeed=2;

    [HideInInspector] public int HorizentalDirect=1;

    private Rigidbody2D _bulletRigidBody;

    private static readonly Vector2 _bulletRightDirect = new Vector2(1, 0);
    private static readonly Vector2 _bulletLeftDirect = new Vector2(-1, 0);

    private Vector2 _bulletDirect 
    { 
        get 
        {
            if (HorizentalDirect >= 0)
                return _bulletRightDirect;
            else
                return _bulletLeftDirect;
        }
    }

    private void Awake()
    {
        _bulletRigidBody = GetComponent<Rigidbody2D>();
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
        _bulletRigidBody.velocity = _bulletDirect * MoveSpeed;
        //transform.position += _bulletDirect * MoveSpeed * Time.deltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("子弹撞击到：" + collision.gameObject.tag);

        switch (collision.gameObject.tag)
        {
            case "Enemy":
            case "Ground":
                Destroy(gameObject);
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("子弹撞击到：" + collision.gameObject.tag);

        //if(collision.gameObject.tag=="Enemy")
        //    Destroy(gameObject);

    }
}
