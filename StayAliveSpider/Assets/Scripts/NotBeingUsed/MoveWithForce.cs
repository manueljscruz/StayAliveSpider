using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithForce : MonoBehaviour
{

    private Rigidbody _rigidbody;

    [SerializeField] private float _movementForce = 10f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Right
        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody.AddForce(_movementForce * Vector3.right);
        }

        // Left
        if (Input.GetKey(KeyCode.A))
        {
            _rigidbody.AddForce(_movementForce * Vector3.left);
        }

        // Up
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddForce(_movementForce * Vector3.forward);
        }

        // Down
        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddForce(_movementForce * Vector3.back);
        }
    }
}
