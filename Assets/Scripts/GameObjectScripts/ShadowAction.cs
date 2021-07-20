using Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAction : MonoBehaviour
{
    private PawnAction _pawnAction;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _pawnAction = transform.parent.GetComponent<PawnAction>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis(SystemValueDefine.AxisInputH);
        _rigidbody.velocity = new Vector2(_pawnAction.MoveSpeed * x, _rigidbody.velocity.y);

        float y = Input.GetAxis(SystemValueDefine.AxisInputV);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _pawnAction.MoveSpeed * y);

    }
}
