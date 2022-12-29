using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.0f;

    private Rigidbody rb;

    void Start()
    {
        TryGetComponent(out rb);
    }

    void Update()
    {
        
    }

    void FixedUpdate() {
        Move();    
    }

    private void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(x, rb.velocity.y, z) * moveSpeed;
    }
}
