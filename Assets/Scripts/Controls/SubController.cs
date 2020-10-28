using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubController : MonoBehaviour
{
    public float turnAroundSpeed = 0.2f;
    public float moveSpeed = 20.0f;
    public float maxSpeed = 40.0f;
    public float acceleration = 2.0f;

    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveSpeed = Mathf.Clamp(moveSpeed + (acceleration * Time.deltaTime), 0.0f, maxSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveSpeed = Mathf.Clamp(moveSpeed - (acceleration * Time.deltaTime), 0.0f, maxSpeed);
        }

        // function to handle actual movement
        Move();
    }

    void Move()
    {
        Vector3 velocity = transform.forward * moveSpeed;
        cc.Move(velocity * Time.deltaTime);
    }
}
