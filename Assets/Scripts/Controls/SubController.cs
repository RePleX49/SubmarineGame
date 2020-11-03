using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubController : MonoBehaviour
{
    public float turnAroundSpeed = 0.2f;
    public float moveSpeed = 20.0f;
    float currentSpeed = 0.0f;
    float currentVelocity = 0.0f;
    public float speedSmoothTime = 1.0f;

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
        float targetSpeed = 0.0f;

        if (Input.GetKey(KeyCode.W))
        {
            targetSpeed = moveSpeed;
            
        }

        if (Input.GetKey(KeyCode.S))
        {
            targetSpeed = -moveSpeed;
        }

        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocity, speedSmoothTime);
        MoveForward();
    }

    void MoveForward()
    {
        Vector3 velocity = transform.forward * currentSpeed;
        cc.Move(velocity * Time.deltaTime);
    }

    void MoveBackward()
    {
        Vector3 velocity = -transform.forward * moveSpeed;
        cc.Move(velocity * Time.deltaTime);
    }
}
